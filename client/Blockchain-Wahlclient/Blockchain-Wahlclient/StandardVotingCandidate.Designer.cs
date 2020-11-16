namespace Blockchain_Wahlclient
{
    partial class StandardVotingCandidate
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.name = new System.Windows.Forms.Label();
            this.party = new System.Windows.Forms.Label();
            this.votingCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name.Location = new System.Drawing.Point(25, 20);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(50, 20);
            this.name.TabIndex = 0;
            this.name.Text = "label1";
            // 
            // party
            // 
            this.party.AutoSize = true;
            this.party.Location = new System.Drawing.Point(46, 40);
            this.party.Name = "party";
            this.party.Size = new System.Drawing.Size(38, 15);
            this.party.TabIndex = 1;
            this.party.Text = "label2";
            // 
            // votingCheckbox
            // 
            this.votingCheckbox.AutoSize = true;
            this.votingCheckbox.Location = new System.Drawing.Point(300, 26);
            this.votingCheckbox.Name = "votingCheckbox";
            this.votingCheckbox.Size = new System.Drawing.Size(15, 14);
            this.votingCheckbox.TabIndex = 2;
            this.votingCheckbox.UseVisualStyleBackColor = true;
            // 
            // StandardVotingCandidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.votingCheckbox);
            this.Controls.Add(this.party);
            this.Controls.Add(this.name);
            this.Name = "StandardVotingCandidate";
            this.Size = new System.Drawing.Size(366, 74);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label party;
        private System.Windows.Forms.CheckBox votingCheckbox;
    }
}
