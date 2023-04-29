using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Xml.Serialization;

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
        bool draw, textInput = true, elseTextInput = false;
        List<Tuple<int, int>> drawingMatrix;
        Kenguru kenguru;
        Intrepretator intp;
        Bitmap bm;
        Pen pen;


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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!elseTextInput)
            {
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
            }
            else
            {
                textBox1.Text += "впереди край то, ";
                elseTextInput = false;
                changeVisability(); // В процедуру 
                toolStripMenuItem3.Visible = true;
                toolStripMenuItem1.Text = "шаг F1";
                toolStripMenuItem2.Text = "прыжок F2";
            }



        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!elseTextInput)
            {
                if (textInput)
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
            else
            {
                textBox1.Text += "впереди не край то, ";
                elseTextInput = false;
                changeVisability();   // В процедуру
                toolStripMenuItem3.Visible = true;
                toolStripMenuItem1.Text = "шаг F1";
                toolStripMenuItem2.Text = "прыжок F2";
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (textInput)
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                toolStripMenuItem3.PerformClick();
                toolStripMenuItem3.Select();
            }
            else if (e.KeyCode == Keys.F1)
            {
                draw = true;
                toolStripMenuItem1.PerformClick();
                toolStripMenuItem1.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                draw = false;
                toolStripMenuItem2.PerformClick();
                toolStripMenuItem2.Select();
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            kenguru.form = this;
            changeVisability();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!textInput)
                changeVisability();
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

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (textInput)
            {
                elseTextInput = true;
                textBox1.Text += "если ";
                changeVisability();
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Text = "впереди край F1";
                toolStripMenuItem2.Text = "впереди не край F2";

            }

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
}