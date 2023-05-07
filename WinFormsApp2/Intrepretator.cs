using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    internal class Intrepretator
    {
        public Intrepretator() 
        {
        }

        private enum Conditions { InIfStatment, InWhileStatement, InElseStatment, InProcedure }

        static public void execute(string commandInput, ref Kenguru ken, ref Bitmap bm, ref PictureBox pb)
        {
            foreach (string cmd in commandInput.Split(new string[] { Environment.NewLine },StringSplitOptions.None)) 
            {
                switch(cmd) 
                {
                    case "шаг":
                        ken.step();
                        break;
                    case "прыжок":
                        ken.jump(); 
                        break;
                    case "поворот":
                        ken.Rotate(ref bm); 
                        break;
                    case "если впереди край то,":
                        continue;
                    default:
                        break;
                }
                pb.Refresh();
            }
        }
    }
}
