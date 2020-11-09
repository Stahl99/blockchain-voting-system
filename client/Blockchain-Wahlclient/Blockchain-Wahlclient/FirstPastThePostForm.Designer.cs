namespace Blockchain_Wahlclient
{
    partial class FirstPastThePostForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.VotingAdressLbl = new System.Windows.Forms.Label();
            this.VoteButton = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 49);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(306, 436);
            this.checkedListBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 23);
            this.textBox1.TabIndex = 1;
            // 
            // VotingAdressLbl
            // 
            this.VotingAdressLbl.AutoSize = true;
            this.VotingAdressLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.VotingAdressLbl.Location = new System.Drawing.Point(10, 0);
            this.VotingAdressLbl.Name = "VotingAdressLbl";
            this.VotingAdressLbl.Size = new System.Drawing.Size(83, 15);
            this.VotingAdressLbl.TabIndex = 2;
            this.VotingAdressLbl.Text = "Voting Adress";
            // 
            // VoteButton
            // 
            this.VoteButton.Location = new System.Drawing.Point(451, 20);
            this.VoteButton.Name = "VoteButton";
            this.VoteButton.Size = new System.Drawing.Size(107, 23);
            this.VoteButton.TabIndex = 3;
            this.VoteButton.Text = "VOTE";
            this.VoteButton.UseVisualStyleBackColor = true;
            this.VoteButton.Click += new System.EventHandler(this.VoteButton_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(324, 49);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.ErrorLabel.TabIndex = 4;
            this.ErrorLabel.Visible = false;
            // 
            // FirstPastThePostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 543);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.VoteButton);
            this.Controls.Add(this.VotingAdressLbl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "FirstPastThePostForm";
            this.Text = "Voting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label VotingAdressLbl;
        private System.Windows.Forms.Button VoteButton;
        private System.Windows.Forms.Label ErrorLabel;
    }
}