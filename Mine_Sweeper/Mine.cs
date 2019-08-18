using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Mine_Sweeper
{
    class Mine : Button
    {
        public bool isMine;
        public bool isFlagged;
        public bool isChecked;


        private int dangerLevel;
        public int DangerLevel
        {
            get
            {

                {
                    return dangerLevel;
                }
            }
            set
            {
                if (isMine == true)
                {
                    dangerLevel = -1;
                }
                else
                {
                    dangerLevel = value;
                }
            }
        }


        public int rowNum;
        public int columnNum;

        public static readonly int size = 25;
        static Size s = new Size(size, size);
        Padding p = new Padding(0, 0, 0, 0);

        public Mine()
        {
           
            this.Size = s;
            this.Margin = p;
            this.Padding = p;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.DarkOrange;

            this.MouseUp += new MouseEventHandler(MineClick);
        }

        private void MineClick(object sender, MouseEventArgs e)
        {
            Mine m = sender as Mine;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (m.isMine&&!m.isFlagged)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                        player.Stream = Resource.Bomb;
                        player.Play();
                        
                        m.Text = Convert.ToString((char)8226);
                        var result = MessageBox.Show("Oops you stepped on a Mine !\nDo you want to play again ?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            Application.Restart();
                        }
                        else if (result == DialogResult.No)
                        {
                            Application.Exit();
                        }
                    }

                    else if(!m.isFlagged)
                    {
                        
                        LookAround(m);

                    }
                    break;

                case MouseButtons.Right:


                    if (!m.isFlagged&&MineField.numberOfFlag>0)
                    {
                        m.isFlagged = true;
                        m.Text = Convert.ToString((char)9668);
                        MineField.numberOfFlag--;
                       

                    }
                    else if(m.isFlagged)
                    {
                        m.isFlagged = false;
                        m.Text = String.Empty;
                        MineField.numberOfFlag++;

                    }
                    if (MineField.numberOfFlag==0&& Control(MineField.MineMap))
                    {
                        var result= MessageBox.Show("Congratulations you won!!!\n","GOOD JOB",MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk);
                        if (result == DialogResult.Yes) { Application.Restart(); }
                        else if (result == DialogResult.No) { Application.Exit(); }
                    }

                    break;


            }


        }

        private void LookAround(Mine m)
        {


            if (m.dangerLevel == 0)
            {
                Queue _queue = new Queue();
                _queue.Enqueue(m);

                while (_queue.Count > 0)
                {
                    var mine = _queue.Dequeue() as Mine;

                    for (int i = mine.rowNum - 1; i <= mine.rowNum + 1; i++)
                    {
                        for (int j = mine.columnNum - 1; j <= mine.columnNum + 1; j++)
                        {

                            try
                            {
                                if (MineField.MineMap[i, j].DangerLevel == 0 && MineField.MineMap[i, j].isChecked == false)
                                {
                                    OpenMine(MineField.MineMap[i, j]);
                                    _queue.Enqueue(MineField.MineMap[i, j]);
                                }
                                else if (MineField.MineMap[i, j].DangerLevel > 0 && MineField.MineMap[i, j].isChecked == false)
                                {
                                    OpenMine(MineField.MineMap[i, j]);

                                }
                                else
                                {
                                    continue;
                                }


                            }
                            catch (Exception)
                            {
                            }

                        }
                    }


                }




            }
            else
            {
                OpenMine(m);
            }




        }

        private void OpenMine(Mine m)
        {
            if (m.DangerLevel > 0)
            {
                m.Text = m.DangerLevel.ToString();
            }

            else
            {
                m.Text = String.Empty;
            }

            m.BackColor = Color.DarkGray;
            m.isChecked = true;
            m.Enabled = false;

        }

        private bool Control(Mine[,] M)
        {
            foreach (var item in M)
            {
                if (item.isMine && item.isFlagged)
                {
                    continue;  
                }
                else if(item.isMine && !item.isFlagged)
                {
                    return false;
                }

            }
            return true;

        }
    }
}
