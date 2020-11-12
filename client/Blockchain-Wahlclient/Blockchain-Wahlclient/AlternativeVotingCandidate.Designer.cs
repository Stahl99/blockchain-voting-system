namespace Blockchain_Wahlclient
{
    partial class AlternativeVotingCandidate
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.name = new System.Windows.Forms.Label();
            this.rankChooser = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.party = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rankChooser)).BeginInit();
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
            // rankChooser
            // 
            this.rankChooser.Location = new System.Drawing.Point(302, 30);
            this.rankChooser.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rankChooser.Name = "rankChooser";
            this.rankChooser.Size = new System.Drawing.Size(37, 23);
            this.rankChooser.TabIndex = 1;
            this.rankChooser.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rank:";
            // 
            // party
            // 
            this.party.AutoSize = true;
            this.party.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.party.Location = new System.Drawing.Point(46, 40);
            this.party.Name = "party";
            this.party.Size = new System.Drawing.Size(38, 13);
            this.party.TabIndex = 3;
            this.party.Text = "label2";
            // 
            // AlternativeVotingCandidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.party);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rankChooser);
            this.Controls.Add(this.name);
            this.Name = "AlternativeVotingCandidate";
            this.Size = new System.Drawing.Size(368, 76);
            ((System.ComponentModel.ISupportInitialize)(this.rankChooser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.NumericUpDown rankChooser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label party;
    }
}
