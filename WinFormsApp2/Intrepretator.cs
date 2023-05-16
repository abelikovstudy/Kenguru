using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    internal class Intrepretator
    {
        public Intrepretator() 
        {
        }

        private enum Conditions { Execute, InIfStatment, InWhileStatement, InElseStatment, InProcedure }


        static public void execute(string commandInput, ref Kenguru ken, ref Bitmap bm, ref PictureBox pb)
        {

            Conditions currenctCondition = Conditions.Execute;
            string[] commands = commandInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < commands.Length; ++i)
            {
                commands[i] = commands[i].Trim();
            }

            int cycleBegin = 0;

            for (int i = 0; i <  commands.Length; ++i) 
            {

                switch(commands[i]) 
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

                        if (ken.isEdge())
                        {
                            currenctCondition = Conditions.InIfStatment;
                        }
                        else
                        {
                            currenctCondition = Conditions.InElseStatment;
                            while (commands[i] != "конец ветвления" && commands[i] != "иначе")
                            {
                                i++;
                                if (i == commands.Length) break; 

                            }
                        }

                        break;
                    case "если впереди не край то,":
                        if (!ken.isEdge())
                        {
                            currenctCondition = Conditions.InIfStatment;
                        }
                        else
                        {
                            currenctCondition = Conditions.InElseStatment;
                            while (commands[i] != "конец ветвления" && commands[i] != "иначе")
                            {
                                i++;
                            }
                        }
                        break;
                    case "иначе":
                        if (currenctCondition == Conditions.InElseStatment) break;
                        while (commands[i] != "конец ветвления")
                        {
                            i++;
                        }
                        break;
                    case "конец ветвления":
                        currenctCondition = Conditions.Execute;
                        break;


                    case "пока впереди край то,":
                        cycleBegin = i;
                        if (ken.isEdge())
                        {
                            currenctCondition = Conditions.InWhileStatement;
                        }
                        else
                        {
                            while (commands[i] != "конец цикла")
                            {
                                i++;
                                if (i == commands.Length) break;

                            }
                        }
                        break;

                    case "пока впереди не край то,":
                        cycleBegin = i;
                        if (!ken.isEdge())
                        {
                            currenctCondition = Conditions.InWhileStatement;
                        }
                        else
                        {
                            while (commands[i] != "конец цикла")
                            {
                                i++;
                                if (i == commands.Length) break;

                            }
                            
                        }
                        break;

                    case "конец цикла":
                        if(currenctCondition == Conditions.InWhileStatement) 
                        {
                            i = cycleBegin - 1;
                        }
                        break;

                    default:
                        break;
                }
                pb.Refresh();
            }
        }

    }

}
