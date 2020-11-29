namespace blockchain_admintool
{
    partial class startForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.candList = new System.Windows.Forms.FlowLayoutPanel();
            this.candidate1 = new blockchain_admintool.candidate();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addCand = new System.Windows.Forms.Button();
            this.reduceCand = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.candList.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Alternative Voting",
            "First Past the Post"});
            this.comboBox1.Location = new System.Drawing.Point(22, 150);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // candList
            // 
            this.candList.AutoScroll = true;
            this.candList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candList.Controls.Add(this.candidate1);
            this.candList.Location = new System.Drawing.Point(281, 150);
            this.candList.Name = "candList";
            this.candList.Size = new System.Drawing.Size(750, 569);
            this.candList.TabIndex = 1;
            // 
            // candidate1
            // 
            this.candidate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidate1.Location = new System.Drawing.Point(3, 3);
            this.candidate1.Name = "candidate1";
            this.candidate1.Size = new System.Drawing.Size(742, 103);
            this.candidate1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(22, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wahlart:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(281, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kandidaten:";
            // 
            // addCand
            // 
            this.addCand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addCand.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.addCand.Location = new System.Drawing.Point(1112, 319);
            this.addCand.Name = "addCand";
            this.addCand.Size = new System.Drawing.Size(30, 68);
            this.addCand.TabIndex = 3;
            this.addCand.Text = "+";
            this.addCand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.addCand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addCand.UseCompatibleTextRendering = true;
            this.addCand.UseVisualStyleBackColor = true;
            this.addCand.Click += new System.EventHandler(this.addCand_Click);
            // 
            // reduceCand
            // 
            this.reduceCand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.reduceCand.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.reduceCand.Location = new System.Drawing.Point(1112, 428);
            this.reduceCand.Name = "reduceCand";
            this.reduceCand.Size = new System.Drawing.Size(30, 68);
            this.reduceCand.TabIndex = 3;
            this.reduceCand.Text = "-";
            this.reduceCand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.reduceCand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.reduceCand.UseCompatibleTextRendering = true;
            this.reduceCand.UseVisualStyleBackColor = true;
            this.reduceCand.Click += new System.EventHandler(this.reduceCand_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(22, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(281, 37);
            this.label3.TabIndex = 4;
            this.label3.Text = "Neue Wahl erstellen:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1032, 777);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 42);
            this.button1.TabIndex = 5;
            this.button1.Text = "Wahl speichern";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(698, 791);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Adminadresse:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(789, 788);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 23);
            this.textBox1.TabIndex = 7;
            // 
            // startForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 850);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reduceCand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addCand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.candList);
            this.Controls.Add(this.comboBox1);
            this.Name = "startForm";
            this.Text = "Admintool Blockchainwahl 0.1";
            this.candList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FlowLayoutPanel candList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addCand;
        private System.Windows.Forms.Button reduceCand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private candidate candidate1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
    }
}

