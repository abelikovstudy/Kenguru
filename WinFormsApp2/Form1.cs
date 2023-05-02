using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

/*
 
1) Создать функцию toggleText для того чтобы после "если" все нормально возвращалось ( тут 2 блока одинаковые в одну функцию нужно)
                elseTextInput = false;
                changeVisability(); // В процедуру 
                toolStripMenuItem3.Visible = true;
                toolStripMenuItem1.Text = "шаг F1";
                toolStripMenuItem2.Text = "прыжок F2";
2) Создать функции куда поместить такой код 

                if (textInput)
                {
                    textBox1.Text += "шаг ";
                }
                else
                {
                    draw = true;
                    kenguru.step();
                    Draw();
                    render();
                }
 */

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        int posX = 0, posY = 0;
        bool draw, textInput = true, elseTextInput = false, whileTextInput = false, endTextInput = false, tabCommandInput = false;
        List<Tuple<int, int>> drawingMatrix;
        Kenguru kenguru;
        Intrepretator intp;
        Bitmap bm;
        Pen pen;

        /// <summary>
        /// Для использования всегда указывается 9 строк, если строка пустая, кнопка в меню скрывается, иначе ей присваивается текст
        /// Применение: setMenus("Шаг F1", "Прыжок F2", "Поворот F3")
        /// </summary>
        /// <param name="menuLabels">Перечисление надписей для меню</param>
       private void setMenus(params string[] menuLabels) 
       {
            int i = 1;
            foreach (string text in menuLabels)
            {
                ToolStripMenuItem item = menuStrip1.Items
                .Find("toolStripMenuItem" + i.ToString(), true)
                .OfType<ToolStripMenuItem>()
                .Single();
                if (text == "")
                {
                    item.Visible = false;
                }
                else 
                {
                    item.Visible = true;
                }
                item.Text = text;
                i += 1;
            }
        }

        /*public static Bitmap Resize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        */
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

        public void render()
        {
            pictureBox1.Refresh();
        }

        public void Draw()
        {
            if (draw)
                drawingMatrix.Add(new Tuple<int, int>(kenguru.posX, kenguru.posY));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (kenguru.moveUp())
            {
                Draw();
                render();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (kenguru.moveDown())
            {
                Draw();
                render();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kenguru.moveLeft())
            {
                Draw();
                render();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (kenguru.moveRight())
            {
                Draw();
                render();
            }
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
        private void SwapNames(int type)
        {
            switch (type)
            {
                case 2:
                    toolStripMenuItem1.Text = "Шаг F1";
                    toolStripMenuItem2.Text = "Прыжок F2";
                    break;
                case 3:
                    toolStripMenuItem1.Text = "Шаг F1";
                    toolStripMenuItem2.Text = "Прыжок F2";
                    toolStripMenuItem3.Text = "Поворот F3";
                    break;
                case 5:
                    toolStripMenuItem1.Text = "Шаг F1";
                    toolStripMenuItem2.Text = "Прыжок F2";
                    toolStripMenuItem3.Text = "Поворот F3";
                    toolStripMenuItem4.Text = "Если F4";
                    toolStripMenuItem5.Text = "Иначе F5";
                    break;

            }
        }

        private void toolStripMenuPasteText()
        {
            elseTextInput = false;
            whileTextInput = false;
            changeVisability(); // В процедуру 
            toolStripMenuItem3.Visible = true;
            SwapNames(2);
        }
        private void toolStripMenuPasteText(bool isEnd)
        {
            endTextInput = false;
            changeVisability(); // В процедуру 
            toolStripMenuItem3.Visible = true;
            SwapNames(3);
        }
        private void toolStripTabChangeText()
        {
            tabCommandInput = !tabCommandInput;

            SwapNames(5);
            changeVisability(4, 5);
        }
        ///<summary>
        /// 1 - Если / Пока
        /// 2 - Конец
        /// 3 - Tab
        ///</summary>
        private void unifiedChangeMenu(int type)
        {
            switch (type)
            {
                case 1:
                    toolStripMenuPasteText();
                    break;
                case 2:
                    toolStripMenuPasteText(true);
                    break;
                case 3:
                    toolStripTabChangeText();
                    break;
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (elseTextInput || whileTextInput)
            {
                textBox1.Text += "впереди край то, ";
                unifiedChangeMenu(1);

            }
            else if (endTextInput)
            {
                textBox1.Text += "ветвления ";
                unifiedChangeMenu(2);
            }
            else if (tabCommandInput)
            {
                unifiedChangeMenu(3);
                MessageBox.Show("1");
            }
            else if (textInput)
            {
                textBox1.Text += "шаг ";
            }
            else
            {

                draw = true;
                kenguru.step();
                Draw();
                render();
            }



        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {


            if (elseTextInput || whileTextInput)
            {
                textBox1.Text += "впереди не край то, ";
                unifiedChangeMenu(1);


            }
            else if (endTextInput)
            {
                textBox1.Text += "цикла ";
                unifiedChangeMenu(2);
            }
            else if (tabCommandInput)
            {
                unifiedChangeMenu(3);
                MessageBox.Show("2");
                
            }
            else if (textInput)
            {
                textBox1.Text += "прыжок ";
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
                textBox1.Text += "процедуры ";
                unifiedChangeMenu(2);
            }
            else if (tabCommandInput)
            {
                unifiedChangeMenu(3);
            }
            else if (textInput)
            {
                textBox1.Text += "поворот ";
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
                unifiedChangeMenu(3);
            }
            else if (textInput)
            {
                elseTextInput = true;
                textBox1.Text += "если ";
                changeVisability();
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Text = "впереди край F1";
                toolStripMenuItem2.Text = "впереди не край F2";

            }


        }
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

            if (tabCommandInput)
            {
                unifiedChangeMenu(3);

            }
            else
            {
                textBox1.Text += "иначе ";
            }


        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

            if (textInput)
            {
                whileTextInput = true;
                textBox1.Text += "пока ";
                changeVisability();
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Text = "впереди край F1";
                toolStripMenuItem2.Text = "впереди не край F2";

            }
        }


        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "сделай ";

        }
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "процедура ";

        }
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (textInput)
            {
                endTextInput = true;
                textBox1.Text += "конец ";
                changeVisability();
                toolStripMenuItem1.Text = "Ветвления F1";
                toolStripMenuItem2.Text = "Цикла F2";
                toolStripMenuItem3.Text = "Процедуры F3";

            }

        }

        private void performeTabControl()
        {

            if (!tabCommandInput)
            {
                toolStripMenuItem1.Text = "Пуск F1";
                toolStripMenuItem2.Text = "Отладка  F2";
                toolStripMenuItem3.Text = "Установка F3";
                toolStripMenuItem4.Text = "Разное F4";
                toolStripMenuItem5.Text = "Результат F5";
            }
            else
            {
                SwapNames(5);
            }
            tabCommandInput = !tabCommandInput;
            changeVisability(4, 5);
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
                    if (textInput)
                    {
                        changeVisability();
                    }
                    performeTabControl();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            PerformeToolsMenuItemClick(e.KeyCode);


        }

        private void changeVisability()
        {
            textInput = !textInput;
            toolStripMenuItem4.Visible = !toolStripMenuItem4.Visible;
            toolStripMenuItem5.Visible = !toolStripMenuItem5.Visible;
            toolStripMenuItem6.Visible = !toolStripMenuItem6.Visible;
            toolStripMenuItem7.Visible = !toolStripMenuItem7.Visible;
            toolStripMenuItem8.Visible = !toolStripMenuItem8.Visible;
            toolStripMenuItem9.Visible = !toolStripMenuItem9.Visible;

        }

        private void changeVisability(params int[] toolMenus)
        {
            foreach (int menu in toolMenus)
            {
                ToolStripMenuItem item = menuStrip1.Items
                .Find("toolStripMenuItem" + menu.ToString(), true)
                .OfType<ToolStripMenuItem>()
                .Single();
                item.Visible = !item.Visible;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kenguru.form = this;
            changeVisability(); //
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!textInput)
                changeVisability(); //
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (textInput)
            {
                if (textBox1.Text != "")
                {
                    MessageBox.Show("Ай-яй-яй!");
                }
                else
                {
                    changeVisability();
                }

            }


        }



        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

    }
}