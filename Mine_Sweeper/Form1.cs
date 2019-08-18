using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine_Sweeper
{
    public partial class Form1 : Form
    {

      
        public Form1()
        {
            InitializeComponent();

            MineField mf = new MineField(MineField.Level.Medium);
            this.Controls.Add(mf);
            

            this.Width = mf.Width + 20;
            this.Height = mf.Height + 40;
            


        }

    }
}
