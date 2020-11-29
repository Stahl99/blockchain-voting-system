using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace blockchain_admintool
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void addCand_Click(object sender, EventArgs e)
        {
            candidate c = new candidate();

            if (this.candList.Controls.Count == 5)
            {
                Size s = new Size();
                s.Height = candList.Size.Height;
                s.Width = candList.Size.Width + 18;

                candList.Size = s;

            }

            this.candList.Controls.Add(c);
        }

        private void reduceCand_Click(object sender, EventArgs e)
        {
            if (this.candList.Controls.Count == 6)
            {
                Size s = new Size();
                s.Height = candList.Size.Height;
                s.Width = candList.Size.Width - 18;

                candList.Size = s;

            }

            if (this.candList.Controls.Count > 0)
            {
                this.candList.Controls.RemoveAt(this.candList.Controls.Count - 1);
            }
        }
    }
}
