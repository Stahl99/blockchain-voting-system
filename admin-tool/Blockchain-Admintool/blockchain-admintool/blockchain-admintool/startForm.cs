﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainVotingSystem.Contracts.bvs_backend.ContractDefinition;
using System.Windows.Forms;
using Nethereum.Web3;
using Nethereum.Model;
using Nethereum.Web3.Accounts;


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
            List<Candidate> candidates = new List<Candidate>();

            Backend backend = new Backend();

            if (backend.InitService(this.bcurl.Text, this.contractAddr.Text, this.adminadress.Text))
            {
                int index = 0;
                foreach(candidate c in candList.Controls)
                {
                    var tmp = c.GetCandObj(index);
                    if (tmp is null)
                    {
                        MessageBox.Show("Bitte Kandidat Nummer " + index.ToString() + " ausfüllen.");
                        return;
                    }
                    candidates.Add(tmp);

                    index++;
                }

                int votingSys = 0;

                if(electionkind_picker.SelectedIndex == 0)
                {
                    votingSys = 1;
                }
                else if (electionkind_picker.SelectedIndex == 1)
                {
                    votingSys = 0;
                }
                else if(electionkind_picker.SelectedItem is null)
                {
                    MessageBox.Show("Eine Wahlart muss ausgewählt werden");
                    return;
                }

                if (fileLocPicker.ShowDialog() == DialogResult.OK)
                {
                    backend.CreateElection(adminadress.Text, votingSys, candidates, GetStartDate(), GetStopDate(), name.Text, (int)voterCount.Value, fileLocPicker.SelectedPath);
                }

                

            }

           
        }

        private DateTime GetStartDate()
        {
            DateTime dt = new DateTime(day_start.Value.Year, day_start.Value.Month, day_start.Value.Day, clock_start.Value.Hour, clock_start.Value.Minute, 0).ToUniversalTime();

            return dt;
        }

        private DateTime GetStopDate()
        {
            DateTime dt = new DateTime(day_stop.Value.Year, day_stop.Value.Month, day_stop.Value.Day, clock_stop.Value.Hour, clock_stop.Value.Minute, 0).ToUniversalTime();


            return dt;
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

        private async void button2_Click(object sender, EventArgs e)
        {
            var privateKey = "0x" + prKey.Text;
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var web3 = new Web3("http://localhost:7545");
            //var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(publicKey);
            var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
            var etherAmount = Web3.Convert.FromWei(balance.Value);

            MessageBox.Show(etherAmount.ToString() + " : " + balance.Value); 
        }
    }
}
