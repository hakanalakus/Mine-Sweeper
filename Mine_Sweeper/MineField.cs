using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Mine_Sweeper
{
    class MineField : FlowLayoutPanel
    {
        public static Mine[,] MineMap;
        public static int numberOfFlag;
        public enum Level { Beginner,Medium,Hard}


        public MineField(Level level)
        {
            int row=8, column=8;
            switch (level)
            {
                case Level.Beginner:
                    row = 8; column = 8;
                    break;
                case Level.Medium:
                    row = 10;column = 10;
                    break;
                case Level.Hard:
                    row = 12; column = 12;
                    break;
                default:
                    break;
            }


            this.Location = new Point(0, 0);
            this.Width = row * (Mine.size);
            this.Height = column * (Mine.size);
            this.BackColor = Color.DarkOrange;

            MineMap = GetMines(row, column);
            SelectRandomMine(MineMap);
            FindDangerLevel(MineMap);

            foreach (var item in MineMap)
            {
                this.Controls.Add(item);
            }


        }

        private Mine[,] GetMines(int row, int column)
        {
            Mine[,] Mines = new Mine[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Mine m = new Mine();
                    m.rowNum = i; m.columnNum = j;
                    Mines[i, j] = m;
                }

            }
            return Mines;

        }

        private void SelectRandomMine(Mine[,] Mines)
        {
            Random rnd = new Random();
            //%15 Mine
            int mineCount = (int)((Mines.GetLength(0) * Mines.GetLength(1) * 10) / 100);
            numberOfFlag = mineCount;
            while (mineCount != 0)
            {
                int rowNum = rnd.Next(0, Mines.GetLength(0));
                int colNum = rnd.Next(0, Mines.GetLength(1));

                if (!Mines[rowNum, colNum].isMine)
                {
                    Mines[rowNum, colNum].isMine = true;
                    mineCount--;

                }
            }

        }

        private void FindDangerLevel(Mine[,] Mines)
        {

            foreach (var m in Mines)
            {
                int counter = 0;

                for (int i = m.rowNum - 1; i <= m.rowNum + 1; i++)
                {
                    for (int j = m.columnNum - 1; j <= m.columnNum + 1; j++)
                    {
                        try
                        {
                            if (Mines[i, j].isMine == true)
                            {
                                counter++;
                            }

                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                m.DangerLevel = counter;

            }

        }





    }
}
