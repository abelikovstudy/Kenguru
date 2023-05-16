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
        bool draw, tabulation, elseCondition, whileCondition, endCondition, textInput, isPositioning;
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
                     drawingMatrix.Add(new Tuple<int, int>(kenguru.posX + 25, kenguru.posY + 25));
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
                    functions[0] = () => { textBox1.Text += "шаг" + Environment.NewLine; };
                    functions[1] = () => { textBox1.Text += "прыжок" + Environment.NewLine; };
                    functions[2] = () => { textBox1.Text += "поворот" + Environment.NewLine; };
                    functions[3] = () => { textBox1.Text += "если"; };
                    functions[4] = () => { textBox1.Text += "иначе" + Environment.NewLine; };
                    functions[5] = () => { textBox1.Text += "пока" + Environment.NewLine; };
                    functions[6] = () => { textBox1.Text += "сделай" + Environment.NewLine; };
                    functions[7] = () => { textBox1.Text += "процедура" + Environment.NewLine; };
                    functions[8] = () => { textBox1.Text += "конец" + Environment.NewLine; };
                    break;
                case MenuControl.ElseControl:
                    functions[0] = () => { textBox1.Text += "если впереди край то," + Environment.NewLine; };
                    functions[1] = () => { textBox1.Text += "если впереди не край то, " + Environment.NewLine; };
                    functions[2] = () => { };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;
                case MenuControl.WhileControl:
                    functions[0] = () => { textBox1.Text += "пока впереди край то, " + Environment.NewLine; };
                    functions[1] = () => { textBox1.Text += "пока впереди не край то, " + Environment.NewLine; };
                    functions[2] = () => { };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;

                case MenuControl.EndCommandControl:
                    functions[0] = () => { textBox1.Text += "конец ветвления " + Environment.NewLine; };
                    functions[1] = () => { textBox1.Text += "конец цикла " + Environment.NewLine; };
                    functions[2] = () => { textBox1.Text += "конец процедуры " + Environment.NewLine; };
                    functions[3] = () => { };
                    functions[4] = () => { };
                    functions[5] = () => { };
                    functions[6] = () => { };
                    functions[7] = () => { };
                    functions[8] = () => { };
                    break;
                case MenuControl.TabControl:
                    functions[0] = () => { Intrepretator.execute(textBox1.Text, ref kenguru, ref bm, ref pictureBox1); };
                    functions[1] = () => { MessageBox.Show("2"); };
                    functions[2] = () => {
                        isPositioning = true;
                        for (int i = 0; i < 4; i++)
                        {
                            kenguru.currentSprites[i] = kenguru.arrowSprites[i];
                            turn();
                        }
                    };
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
            if (tabulation)
                setMenus(MenuControl.TabControl, "Пуск F1", "Отладка F2", "Установка F3", "Разное F4", "Результат F5", "", "", "", "");
            else 
            {
                if (textBox1.Text != "" || textInput)
                {
                    setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
                }
                else 
                {

                    setMenus(MenuControl.MoveControl, "Шаг F1", "Прыжок F2", "Поворот F3", "", "", "", "", "", "");
                }
            }

        }
        public Form1()
        {
            drawingMatrix = new List<Tuple<int, int>>();

            draw = false;
            tabulation = false;
            elseCondition = false;
            whileCondition = false;
            elseCondition = false;
            textInput = false;
            isPositioning = false;

            bm = new Bitmap(WinFormsApp2.Properties.Resources.CkenguruRight, 82, 82);
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

            setMenus(MenuControl.MoveControl, "Шаг F1", "Прыжок F2", "Поворот F3", "", "", "", "", "", "");
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
                //intp.execute(textBox1.Text, ref kenguru, ref bm);
                Draw();
                render();
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            functions[0]();
            if (elseCondition)
            {
                elseCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
            if (whileCondition)
            {
                whileCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
            if (endCondition)
            {
                endCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            functions[1]();
            if (elseCondition)
            {
                elseCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
            if (whileCondition)
            {
                whileCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
            if (endCondition)
            {
                endCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            functions[2]();
            if (endCondition)
            {
                endCondition = false;
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            elseCondition = !elseCondition;
            if (elseCondition)
            {
                setMenus(MenuControl.ElseControl, "впереди край то F1", "впереди не край то F2", "", "", "", "", "", "", "");
            }
            else
            {
                setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }

            functions[3]();

        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            functions[4]();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            whileCondition = !whileCondition;
            if (whileCondition)
            {
                setMenus(MenuControl.WhileControl, "впереди край то F1", "впереди не край то F2", "", "", "", "", "", "", "");
            }
            else
            {
                setMenus(MenuControl.WhileControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
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
            endCondition = !endCondition;
            if (endCondition)
            {
                setMenus(MenuControl.EndCommandControl, "ветвления F1", "цикла F2", "процедуры F3", "", "", "", "", "", "");
            }
            else
            {
                setMenus(MenuControl.EndCommandControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
            }
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
                case Keys.Right:
                    if (isPositioning) 
                    {
                        kenguru.currentDirection = Kenguru.directions.R;
                        jump();
                        
                    }
                    break;
                case Keys.Left:
                    if (isPositioning)
                    {
                        kenguru.currentDirection = Kenguru.directions.L;
                        jump();

                    }
                    break;
                case Keys.Down:
                    if (isPositioning)
                    {
                        kenguru.currentDirection = Kenguru.directions.D;
                        jump();

                    }
                    break;
                case Keys.Up:
                    if (isPositioning)
                    {
                        kenguru.currentDirection = Kenguru.directions.U;
                        jump();

                    }
                    break;
                case Keys.Enter:
                    for (int i = 0; i < 4; i++)
                    {
                        kenguru.currentSprites[i] = kenguru.kenguruSprites[i];
                        turn();
                    }
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            PerformeToolsMenuItemClick(e.KeyCode);
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textInput = true;
            setMenus(MenuControl.TextControl, "Шаг F1", "Прыжок F2", "Поворот F3", "Если F4", "Иначе F5", "Пока F6", "Сделай F7", "Процедура F8", "Конец F9");
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show("Ай-яй-яй!");
            }
            else
            {
                textInput = false;
                setMenus(MenuControl.MoveControl, "Шаг F1", "Прыжок F2", "Поворот F3", "", "", "", "", "", "");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}