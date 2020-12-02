namespace Blockchain_Wahlclient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.blockchainUrlTB = new System.Windows.Forms.TextBox();
            this.blockchainURLLabel = new System.Windows.Forms.Label();
            this.electionAdressTB = new System.Windows.Forms.TextBox();
            this.electionAdressLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // blockchainUrlTB
            // 
            this.blockchainUrlTB.Location = new System.Drawing.Point(251, 60);
            this.blockchainUrlTB.Name = "blockchainUrlTB";
            this.blockchainUrlTB.Size = new System.Drawing.Size(302, 23);
            this.blockchainUrlTB.TabIndex = 1;
            // 
            // blockchainURLLabel
            // 
            this.blockchainURLLabel.AutoSize = true;
            this.blockchainURLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.blockchainURLLabel.Location = new System.Drawing.Point(248, 40);
            this.blockchainURLLabel.Name = "blockchainURLLabel";
            this.blockchainURLLabel.Size = new System.Drawing.Size(115, 18);
            this.blockchainURLLabel.TabIndex = 2;
            this.blockchainURLLabel.Text = "Blockchain URL";
            // 
            // electionAdressTB
            // 
            this.electionAdressTB.Location = new System.Drawing.Point(251, 140);
            this.electionAdressTB.Name = "electionAdressTB";
            this.electionAdressTB.Size = new System.Drawing.Size(302, 23);
            this.electionAdressTB.TabIndex = 3;
            // 
            // electionAdressLabel
            // 
            this.electionAdressLabel.AutoSize = true;
            this.electionAdressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.electionAdressLabel.Location = new System.Drawing.Point(250, 120);
            this.electionAdressLabel.Name = "electionAdressLabel";
            this.electionAdressLabel.Size = new System.Drawing.Size(111, 18);
            this.electionAdressLabel.TabIndex = 4;
            this.electionAdressLabel.Text = "Election Adress";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(249, 191);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(305, 40);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Connect";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 450);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.electionAdressLabel);
            this.Controls.Add(this.electionAdressTB);
            this.Controls.Add(this.blockchainURLLabel);
            this.Controls.Add(this.blockchainUrlTB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox blockchainUrlTB;
        private System.Windows.Forms.Label blockchainURLLabel;
        private System.Windows.Forms.TextBox electionAdressTB;
        private System.Windows.Forms.Label electionAdressLabel;
        private System.Windows.Forms.Button submitButton;
    }
}

