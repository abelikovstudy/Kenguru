using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    class Kenguru
    {
        public Form1 form;
        public int posX { get; set; }
        public int posY { get; set; }


        public enum directions { U, D, L, R };
        public directions currentDirection = directions.R;
        public int fieldHeight {  get; set; }
        public int fieldWidth { get; set; }

        public Kenguru(int _width, int _height) 
        {

            posX = 32;
            posY = 32;
            fieldHeight = _height;
            fieldWidth = _width;


        }

        public bool isEdge() 
        {
            switch (currentDirection) 
            {
                case directions.U:
                    if (posY <= 32) return true;
                    else return false;
                case directions.D:
                    if (posY >= fieldHeight - 32) return true;
                    else return false;
                case directions.L:
                    if (posX <= 32) return true;
                    else return false;
                case directions.R:
                    if (posX >= fieldWidth - 32) return true;
                    else return false;
                default: return false;
            }
        }

        public void Rotate(ref Bitmap bm) 
        {
            switch (currentDirection) {

                case directions.U:
                    currentDirection = directions.L;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.kenguruLeft, 48, 48);
                    break; 
                case directions.D:
                    currentDirection = directions.R;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.kenguruRight, 48, 48);
                    break; 
                case directions.L:
                    currentDirection= directions.D;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.kenguruDown, 48, 48);
                    break;
                case directions.R:
                    currentDirection = directions.U;
                    posX -= 8;
                    posY -= 28;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.kenguruUp, 48, 48);

                    break;
            }
            bm.MakeTransparent(Color.White);
        }

        public void step() 
        {
            switch (currentDirection)
            {

                case directions.U:
                    for(int i = 0; i < 10; ++i) 
                    {
                        Thread.Sleep(5);
                        moveUp();
                        form.Draw();
                    }
                    break;
                case directions.D:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(5);
                        moveDown();
                        form.Draw();
                    }
                    break;
                case directions.L:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(5);
                        moveLeft();
                        form.Draw();
                    }
                    break;
                case directions.R:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(5);
                        moveRight();
                        form.Draw();
                    }
                    break;
            }
        }
        public void jump()
        {
            switch (currentDirection)
            {

                case directions.U:
                    for (int i = 0; i < 10; ++i)
                    {

                        moveUp();
                    }
                    break;
                case directions.D:
                    for (int i = 0; i < 10; ++i)
                    {
                        moveDown();
                    }
                    break;
                case directions.L:
                    for (int i = 0; i < 10; ++i)
                    {
                         moveLeft();
                    }
                    break;
                case directions.R:
                    for (int i = 0; i < 10; ++i)
                    {
                       moveRight();
                    }
                    break;
            }
        }
        public bool moveUp() 
        {
            if (posY > 32)
            {
                posY -= 1;
                return true;
            }
            return false;
        }
        public bool moveDown()
        {
            if (posY < fieldHeight - 32)
            {
                posY += 1;
                return true;
            }
            return false;
        }
        public bool moveLeft()
        {
            if (posX > 32)
            {
                posX -= 1;
                return true;
            }
            return false;
        }
        public bool moveRight()
        {
            if (posX < fieldWidth - 32)
            {
                posX += 1;
                return true;
            }
            return false;

        }

    }

}
