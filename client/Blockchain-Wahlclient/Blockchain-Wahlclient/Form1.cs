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
            CandidateList candidateList = new CandidateList();

            Candidate a = new Candidate("christian", "linder", "fdp");
            Candidate b = new Candidate("angela", "merkel", "cdu");
            Candidate c = new Candidate("giro-olaf", "scholz", "spd");
            Candidate d = new Candidate("anton", "hofreiter", "grüne");
            Candidate g = new Candidate("gregor", "gysi", "linke");
            Candidate f = new Candidate("alexander", "gauland", "afd");

            candidateList.AddCandidate(a);
            candidateList.AddCandidate(b);
            candidateList.AddCandidate(c);
            candidateList.AddCandidate(d);
            candidateList.AddCandidate(g);
            candidateList.AddCandidate(f);

            AlternativeVoting av = new AlternativeVoting();
            av.SetCandList(candidateList);
            av.Show();
        }
    }
}
