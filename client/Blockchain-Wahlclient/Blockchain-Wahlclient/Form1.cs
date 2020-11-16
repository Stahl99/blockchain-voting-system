using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            //initialize candidatlist

            Candidate a = new Candidate("christian", "linder", "fdp");
            Candidate b = new Candidate("angela", "merkel", "cdu");
            Candidate c = new Candidate("giro-olaf", "scholz", "spd");
            Candidate d = new Candidate("anton", "hofreiter", "grüne");
            Candidate g = new Candidate("gregor", "gysi", "linke");
            Candidate f = new Candidate("alexander", "gauland", "afd");

            FirstPastThePostForm FPTPform = new FirstPastThePostForm();
            FPTPform.Model.AddCandidate(a);
            FPTPform.Model.AddCandidate(b);
            FPTPform.Model.AddCandidate(c);
            FPTPform.Model.AddCandidate(d);
            FPTPform.Model.AddCandidate(g);
            FPTPform.Model.AddCandidate(f);


            FPTPform.Show();
            /*
            candidateList.AddCandidate(a);
            candidateList.AddCandidate(b);
            candidateList.AddCandidate(c);
            candidateList.AddCandidate(d);
            candidateList.AddCandidate(g);
            candidateList.AddCandidate(f);

            /*
            FirstPastThePostForm av = new FirstPastThePostForm();
            av.SetCandList(candidateList);
            av.Show();
            */
        }
    }
}
