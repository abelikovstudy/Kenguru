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
        bool draw, textInput = true, elseTextInput = false, whileTextInput = false, endTextInput = false, tabCommandInput = false;
        enum MenuControl { MoveControl, TextControl, ElseControl, WhileControl, TabControl };
        List<Action> functions = new List<Action>();
        List<Tuple<int, int>> drawingMatrix;
        Kenguru kenguru;
        Intrepretator intp;
        Bitmap bm;
        Pen pen;


        private void step() 
        {
            draw = true;
            kenguru.step();
            Draw();
            render();
        }


        private void setMenus(MenuControl control,params string[] menuLabels)
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
                    break;
                case MenuControl.TextControl:
                    functions[0] = () => { textBox1.Text += "��� "; };
                    break;
                case MenuControl.ElseControl:
                    functions[0] = () => { textBox1.Text += "������� ���� ��,  "; };
                    break;
                case MenuControl.WhileControl:
                    functions[0] = () => { textBox1.Text += "������� ���� ��,  "; };
                    break;
                case MenuControl.TabControl:
                    functions[0] = () => { };
                    break;

            }

        }
        public Form1()
        {
            drawingMatrix = new List<Tuple<int, int>>();
            draw = false;
            bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteRight, 32, 32);
            bm.MakeTransparent(Color.White);
            pen = new Pen(Color.Red, 1);
            InitializeComponent();
            kenguru = new Kenguru(pictureBox1.Size.Width, pictureBox1.Size.Height);
            intp = new Intrepretator();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0;i < 9;i++) 
            {
                functions.Add(() => { });
            }

            setMenus(MenuControl.MoveControl, "��� F1", "������ F2", "������� F3", "", "", "", "", "", "");
            kenguru.form = this;
        }

        public void render()
        {
            pictureBox1.Refresh();
        }
        public void Draw()
        {
            if (draw)
                drawingMatrix.Add(new Tuple<int, int>(kenguru.posX, kenguru.posY));
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            functions[0]();
            /*
            if (elseTextInput || whileTextInput)
            {
                textBox1.Text += "������� ���� ��, ";

            }
            else if (endTextInput)
            {
                textBox1.Text += "��������� ";
            }
            else if (tabCommandInput)
            {
                MessageBox.Show("1");
            }
            else if (textInput)
            {
                textBox1.Text += "��� ";
            }
            else
            {

                draw = true;
                kenguru.step();
                Draw();
                render();
            }
            */


        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {


            if (elseTextInput || whileTextInput)
            {
                textBox1.Text += "������� �� ���� ��, ";


            }
            else if (endTextInput)
            {
                textBox1.Text += "����� ";
            }
            else if (tabCommandInput)
            {
                MessageBox.Show("2");

            }
            else if (textInput)
            {
                textBox1.Text += "������ ";
            }
            else
            {

                draw = false;
                kenguru.jump();
                Draw();
                render();
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (endTextInput)
            {
                textBox1.Text += "��������� ";
            }
            else if (tabCommandInput)
            {

            }
            else if (textInput)
            {
                textBox1.Text += "������� ";
            }
            else
            {
                kenguru.Rotate(ref bm);
                Draw();
                render();
            }




        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (tabCommandInput)
            {

            }
            else if (textInput)
            {
                elseTextInput = true;
                textBox1.Text += "���� ";
                ;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Text = "������� ���� F1";
                toolStripMenuItem2.Text = "������� �� ���� F2";

            }


        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

            if (tabCommandInput)
            {


            }
            else
            {
                textBox1.Text += "����� ";
            }


        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

            if (textInput)
            {
                whileTextInput = true;
                textBox1.Text += "���� ";
                ;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Text = "������� ���� F1";
                toolStripMenuItem2.Text = "������� �� ���� F2";

            }
        }


        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "������ ";

        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "��������� ";

        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (textInput)
            {
                endTextInput = true;
                textBox1.Text += "����� ";
                toolStripMenuItem1.Text = "��������� F1";
                toolStripMenuItem2.Text = "����� F2";
                toolStripMenuItem3.Text = "��������� F3";

            }

        }

        private void performeTabControl()
        {

            if (!tabCommandInput)
            {
                toolStripMenuItem1.Text = "���� F1";
                toolStripMenuItem2.Text = "�������  F2";
                toolStripMenuItem3.Text = "��������� F3";
                toolStripMenuItem4.Text = "������ F4";
                toolStripMenuItem5.Text = "��������� F5";
            }
            else
            {
            }
            tabCommandInput = !tabCommandInput;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
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
            setMenus(MenuControl.ElseControl, "��� F1", "������ F2", "������� F3", "���� F4", "����� F5", "���� F6", "������ F7", "��������� F8", "����� F9");
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        }
    }
}