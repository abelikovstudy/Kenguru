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
        private directions currentDirection = directions.R;
        public int fieldHeight {  get; set; }
        public int fieldWidth { get; set; }

        public Kenguru(int _width, int _height) 
        {

            posX = 32;
            posY = 32;
            fieldHeight = _height;
            fieldWidth = _width;


        }

        public void Rotate(ref Bitmap bm) 
        {
            switch (currentDirection) {

                case directions.U:
                    currentDirection = directions.R;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteRight, 32, 32);
                    break; 
                case directions.D:
                    currentDirection = directions.L;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteLeft, 32, 32);
                    break; 
                case directions.L:
                    currentDirection= directions.U;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteUp, 32, 32);
                    break;
                case directions.R:
                    currentDirection = directions.D;
                    bm = new Bitmap(WinFormsApp2.Properties.Resources.spriteDown, 32, 32);
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
                        
                        moveUp();
                        form.Draw();
                    }
                    break;
                case directions.D:
                    for (int i = 0; i < 10; ++i)
                    {
                        moveDown();
                        form.Draw();
                    }
                    break;
                case directions.L:
                    for (int i = 0; i < 10; ++i)
                    {
                        moveLeft();
                        form.Draw();
                    }
                    break;
                case directions.R:
                    for (int i = 0; i < 10; ++i)
                    {
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
