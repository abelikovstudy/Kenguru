using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        bool draw, tabulation;
        enum MenuControl { MoveControl, TextControl, ElseControl, WhileControl, EndCommandControl, TabControl };
        List<Action> functions = new List<Action>();
        List<Tuple<int, int>> drawingMatrix;
        Kenguru kenguru;
        Intrepretator intp;
        Bitmap bm;
        Pen pen;

        public void render()
        {
            pictureBox1.Refresh();
        }
        public void Draw()
        {
            if (draw)
                drawingMatrix.Add(new Tuple<int, int>(kenguru.posX, kenguru.posY));
        }
        private void step()
        {
            draw = true;
            kenguru.step();
            Draw();
            render();
        }
        private void jump()
        {
            draw = false;
            kenguru.jump();
            Draw();
            render();
        }
        private void turn()
        {
            kenguru.Rotate(ref bm);
            Draw();
            render();

        }
        private void setMenus(MenuControl control, params string[] menuLabels)
        {
            int i = 0;
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (menuLabels[i] == "")
                {
                    item.Visible = false;
                }
                else
                {
                    item.Visible = true;
                    item.Text = menuLabels[i];
                }
                i += 1;
            }

            switch (control)
            {
                case MenuControl.MoveControl:
                    functions[0] = step;
                    functions[1] = jump;
                    functions[2] = turn;
                    break;
                case MenuControl.TextControl:
                    functions[0] = () => { textBox1.Text += "��� "; };
                    functions[1] = () => { textBox1.Text += "������ "; };
                    functions[2] = () => { textBox1.Text += "������� "; };
                    functions[3] = () => { textBox1.Text += "���� "; };
                    functions[4] = () => { textBox1.Text += "����� "; };
                    functions[5] = () => { textBox1.Text += "���� "; };
                    functions[6] = () => { textBox1.Text += "������ "; };
                    functions[7] = () => { textBox1.Text += "��������� "; };
                    functions[8] = () => { textBox1.Text += "����� "; };
                    break;
                case MenuControl.ElseControl:
                    functions[0] = () => { textBox1.Text += "������� ���� ��, "; };
                    functions[1] = () => { textBox1.Text += "������� �� ���� ��, "; };
                    functions[2] = () => { };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;
                case MenuControl.WhileControl:
                    functions[0] = () => { textBox1.Text += "������� ���� ��, "; };
                    functions[1] = () => { textBox1.Text += "������� �� ���� ��, "; };
                    functions[2] = () => { };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;

                case MenuControl.EndCommandControl:
                    functions[0] = () => { textBox1.Text += "��������� "; };
                    functions[1] = () => { textBox1.Text += "����� "; };
                    functions[2] = () => { textBox1.Text += "��������� "; };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;
                case MenuControl.TabControl:
                    functions[0] = () => { MessageBox.Show("1"); };
                    functions[1] = () => { MessageBox.Show("2"); };
                    functions[2] = () => { MessageBox.Show("3"); };
                    functions[3] = () => { MessageBox.Show("4"); };
                    functions[4] = () => { MessageBox.Show("5"); };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;

            }

        }
        private void performeTabControl()
        {
            tabulation = !tabulation;
            if(tabulation)
                setMenus(MenuControl.TabControl, "���� F1", "������� F2", "��������� F3", "������ F4", "��������� F5", "", "", "", "");
            else
                setMenus(MenuControl.TextControl, "��� F1", "������ F2", "������� F3", "���� F4", "����� F5", "���� F6", "������ F7", "��������� F8", "����� F9");
        }
        public Form1()
        {
            drawingMatrix = new List<Tuple<int, int>>();

            draw = false;
            tabulation = false;

            bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteRight, 32, 32);
            bm.MakeTransparent(Color.White);
            pen = new Pen(Color.Red, 1);
            InitializeComponent();
            kenguru = new Kenguru(pictureBox1.Size.Width, pictureBox1.Size.Height);
            intp = new Intrepretator();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                functions.Add(() => { });
            }

            setMenus(MenuControl.MoveControl, "��� F1", "������ F2", "������� F3", "", "", "", "", "", "");
            kenguru.form = this;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImage(bm, new Point(kenguru.posX - 16, kenguru.posY - 16));
            foreach (Tuple<int, int> point in drawingMatrix)
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(point.Item1, point.Item2, 1, 1));
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            draw = !draw;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                intp.execute(textBox1.Text, ref kenguru, ref bm);
                Draw();
                render();
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            functions[0]();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            functions[1]();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            functions[2]();
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            functions[3]();
        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            functions[4]();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            functions[5]();
        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            functions[6]();
        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            functions[7]();
        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            functions[8]();
        }
        private void PerformeToolsMenuItemClick(Keys key)
        {
            switch (key)
            {
                case Keys.F1:
                    draw = true;
                    toolStripMenuItem1.PerformClick();
                    toolStripMenuItem1.Select();
                    break;
                case Keys.F2:
                    draw = false;
                    toolStripMenuItem2.PerformClick();
                    toolStripMenuItem2.Select();
                    break;
                case Keys.F3:
                    toolStripMenuItem3.PerformClick();
                    toolStripMenuItem3.Select();
                    break;
                case Keys.F4:
                    toolStripMenuItem4.PerformClick();
                    toolStripMenuItem4.Select();
                    break;
                case Keys.F5:
                    toolStripMenuItem5.PerformClick();
                    toolStripMenuItem5.Select();
                    break;
                case Keys.F6:
                    toolStripMenuItem6.PerformClick();
                    toolStripMenuItem6.Select();
                    break;
                case Keys.F7:
                    toolStripMenuItem7.PerformClick();
                    toolStripMenuItem7.Select();
                    break;
                case Keys.F8:
                    toolStripMenuItem8.PerformClick();
                    toolStripMenuItem8.Select();
                    break;
                case Keys.F9:
                    toolStripMenuItem9.PerformClick();
                    toolStripMenuItem9.Select();
                    break;
                case Keys.Tab:
                    performeTabControl();
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            PerformeToolsMenuItemClick(e.KeyCode);
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            setMenus(MenuControl.TextControl, "��� F1", "������ F2", "������� F3", "���� F4", "����� F5", "���� F6", "������ F7", "��������� F8", "����� F9");
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show("��-��-��!");
            }
            else
            {
                setMenus(MenuControl.MoveControl, "��� F1", "������ F2", "������� F3", "", "", "", "", "", "");
            }

        }
    }
}