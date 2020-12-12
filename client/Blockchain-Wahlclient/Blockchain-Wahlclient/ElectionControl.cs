using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Blockchain_Wahlclient
{
    /// <summary>
    /// User Control that represents a election and can be displayed on the frontend
    /// </summary>
    public partial class ElectionControl : UserControl
    {
        private int id;
        public ElectionControl()
        {
            InitializeComponent();
        }

        public void SetName(string name)
        {
            this.name.Text = name;
        }

        public void SetTimestamp(string timestamp)
        {
            this.timestamp.Text = timestamp;
        }

        public String GetName()
        {
            return this.name.Text;
        }

        public String GetTimestamp()
        {
            return this.timestamp.Text;
        }

        public bool GetChecked()
        {
            return this.votingCheckbox.Checked;
        }

        public void SetId(int id)
        {
            this.id = id; 
        }

        public int GetId()
        {
            return this.id;
        }
    }
}
