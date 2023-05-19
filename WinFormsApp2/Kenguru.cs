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
        public Roo form;
        public int posX { get; set; }
        public int posY { get; set; }

        public List<Bitmap> currentSprites = new List<Bitmap>(4);
        public List<Bitmap> kenguruSprites = new List<Bitmap>(4);
        public List<Bitmap> arrowSprites = new List<Bitmap>();

        public enum directions { U, D, L, R };
        public directions currentDirection = directions.R;
        public int fieldHeight { get; set; }
        public int fieldWidth { get; set; }
        public int speed = 5;
        public Kenguru(int _width, int _height)
        {

            posX = 32;
            posY = 32;
            fieldHeight = _height;
            fieldWidth = _width;
            kenguruSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.CkenguruLeft, 82, 82));
            kenguruSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.CkenguruRight, 82, 82));
            kenguruSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.CkenguruDown, 82, 82));
            kenguruSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.CkenguruUp, 82, 82));

            arrowSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.spriteLeft, 41, 41));
            arrowSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.spriteRight, 41, 41));
            arrowSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.spriteDown, 41, 41));
            arrowSprites.Add(new Bitmap(WinFormsApp2.Properties.Resources.spriteUp, 41, 41));

            currentSprites.Add(kenguruSprites[0]);
            currentSprites.Add(kenguruSprites[1]);
            currentSprites.Add(kenguruSprites[2]);
            currentSprites.Add(kenguruSprites[3]);
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
                    bm = currentSprites[0];
                    break;
                case directions.D:
                    currentDirection = directions.R;
                    bm = currentSprites[1];
                    break;
                case directions.L:
                    currentDirection = directions.D;
                    bm = currentSprites[2];
                    break;
                case directions.R:
                    currentDirection = directions.U;
                    bm = currentSprites[3];

                    break;
            }
            bm.MakeTransparent(Color.White);
        }

        public void Rotate(ref Bitmap bm, directions dir) 
        {
            switch (dir)
            {

                case directions.U:
                    currentDirection = directions.U;
                    bm = currentSprites[3];
                    break;
                case directions.D:
                    currentDirection = directions.D;
                    bm = currentSprites[2];
                    break;
                case directions.L:
                    currentDirection = directions.L;
                    bm = currentSprites[0];
                    break;
                case directions.R:
                    currentDirection = directions.R;
                    bm = currentSprites[1];

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
                        Thread.Sleep(speed);
                        moveUp();
                        form.Draw();
                    }
                    break;
                case directions.D:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(speed);
                        moveDown();
                        form.Draw();
                    }
                    break;
                case directions.L:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(speed);
                        moveLeft();
                        form.Draw();
                    }
                    break;
                case directions.R:
                    for (int i = 0; i < 10; ++i)
                    {
                        Thread.Sleep(speed);
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
