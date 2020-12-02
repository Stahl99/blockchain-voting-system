using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    public partial class ElectionPickerForm : Form
    {
        ElectionPickerModel model;

        public ElectionPickerForm(Backend backend)
        {
            InitializeComponent();

            this.model = new ElectionPickerModel();
            this.model.Backend = backend;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (model.ValidatePick())
            {
                this.Hide();

                // load alternative voting form
                if (model.GetVotingType() == 0)
                {
                    var AltVotingForm = new AlternativeVoting(this.model.Backend);
                    AltVotingForm.ShowDialog();
                    this.Close();
                }

                // load standard voting form
                if (model.GetVotingType() == 1)
                {
                    var FPTPForm = new FirstPastThePostForm(this.model.Backend);
                    FPTPForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void ElectionPickerForm_Load(object sender, EventArgs e)
        {
            var task = Task.Run(async () => {await model.LoadElections(); } );
            task.Wait();
            model.ShowElections(ref flowLayoutPanel1);
        }
    }
}
