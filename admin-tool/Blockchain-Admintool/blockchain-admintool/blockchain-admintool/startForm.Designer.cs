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
            this.electionkind_picker = new System.Windows.Forms.ComboBox();
            this.candList = new System.Windows.Forms.FlowLayoutPanel();
            this.candidate1 = new blockchain_admintool.candidate();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addCand = new System.Windows.Forms.Button();
            this.reduceCand = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.adminadress = new System.Windows.Forms.TextBox();
            this.day_start = new System.Windows.Forms.DateTimePicker();
            this.clock_start = new System.Windows.Forms.DateTimePicker();
            this.clock_stop = new System.Windows.Forms.DateTimePicker();
            this.day_stop = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bcurl = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.contractAddr = new System.Windows.Forms.TextBox();
            this.voterCount = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.fileLocPicker = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.prKey = new System.Windows.Forms.TextBox();
            this.candList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voterCount)).BeginInit();
            this.SuspendLayout();
            // 
            // electionkind_picker
            // 
            this.electionkind_picker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.electionkind_picker.FormattingEnabled = true;
            this.electionkind_picker.Items.AddRange(new object[] {
            "Alternative Voting",
            "First Past the Post"});
            this.electionkind_picker.Location = new System.Drawing.Point(22, 150);
            this.electionkind_picker.Name = "electionkind_picker";
            this.electionkind_picker.Size = new System.Drawing.Size(121, 23);
            this.electionkind_picker.TabIndex = 0;
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
            // adminadress
            // 
            this.adminadress.Location = new System.Drawing.Point(789, 788);
            this.adminadress.Name = "adminadress";
            this.adminadress.Size = new System.Drawing.Size(192, 23);
            this.adminadress.TabIndex = 7;
            // 
            // day_start
            // 
            this.day_start.CustomFormat = "";
            this.day_start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.day_start.Location = new System.Drawing.Point(22, 319);
            this.day_start.Name = "day_start";
            this.day_start.Size = new System.Drawing.Size(97, 23);
            this.day_start.TabIndex = 8;
            // 
            // clock_start
            // 
            this.clock_start.CustomFormat = "H:mm";
            this.clock_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.clock_start.Location = new System.Drawing.Point(125, 319);
            this.clock_start.Name = "clock_start";
            this.clock_start.ShowUpDown = true;
            this.clock_start.Size = new System.Drawing.Size(80, 23);
            this.clock_start.TabIndex = 9;
            // 
            // clock_stop
            // 
            this.clock_stop.CustomFormat = "H:mm";
            this.clock_stop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.clock_stop.Location = new System.Drawing.Point(125, 401);
            this.clock_stop.Name = "clock_stop";
            this.clock_stop.ShowUpDown = true;
            this.clock_stop.Size = new System.Drawing.Size(80, 23);
            this.clock_stop.TabIndex = 9;
            // 
            // day_stop
            // 
            this.day_stop.CustomFormat = "";
            this.day_stop.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.day_stop.Location = new System.Drawing.Point(22, 401);
            this.day_stop.Name = "day_stop";
            this.day_stop.Size = new System.Drawing.Size(97, 23);
            this.day_stop.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(22, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "Gültigkeitszeitraum:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Von:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Bis:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(22, 536);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(183, 84);
            this.name.TabIndex = 12;
            this.name.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(22, 493);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 25);
            this.label8.TabIndex = 2;
            this.label8.Text = "Beschreibung:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(369, 791);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "Blockchain-URL:";
            // 
            // bcurl
            // 
            this.bcurl.Location = new System.Drawing.Point(469, 788);
            this.bcurl.Name = "bcurl";
            this.bcurl.Size = new System.Drawing.Size(192, 23);
            this.bcurl.TabIndex = 14;
            this.bcurl.Text = "http://127.0.0.1:7545";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 791);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "Contract Adresse:";
            // 
            // contractAddr
            // 
            this.contractAddr.Location = new System.Drawing.Point(145, 788);
            this.contractAddr.Name = "contractAddr";
            this.contractAddr.Size = new System.Drawing.Size(192, 23);
            this.contractAddr.TabIndex = 16;
            this.contractAddr.Text = "0x173677665DB87d1e01df0344B11386A8d9640d50";
            // 
            // voterCount
            // 
            this.voterCount.Location = new System.Drawing.Point(22, 712);
            this.voterCount.Name = "voterCount";
            this.voterCount.Size = new System.Drawing.Size(120, 23);
            this.voterCount.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(19, 663);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(194, 25);
            this.label11.TabIndex = 2;
            this.label11.Text = "Zugelassene Wähler:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(813, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 42);
            this.button2.TabIndex = 18;
            this.button2.Text = "check account balance";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // prKey
            // 
            this.prKey.Location = new System.Drawing.Point(930, 75);
            this.prKey.Name = "prKey";
            this.prKey.Size = new System.Drawing.Size(147, 23);
            this.prKey.TabIndex = 19;
            // 
            // startForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 850);
            this.Controls.Add(this.prKey);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.voterCount);
            this.Controls.Add(this.contractAddr);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bcurl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.day_stop);
            this.Controls.Add(this.clock_stop);
            this.Controls.Add(this.clock_start);
            this.Controls.Add(this.day_start);
            this.Controls.Add(this.adminadress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reduceCand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addCand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.candList);
            this.Controls.Add(this.electionkind_picker);
            this.Name = "startForm";
            this.Text = "Admintool Blockchainwahl 0.1";
            this.candList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.voterCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox electionkind_picker;
        private System.Windows.Forms.FlowLayoutPanel candList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addCand;
        private System.Windows.Forms.Button reduceCand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private candidate candidate1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox adminadress;
        private System.Windows.Forms.DateTimePicker day_start;
        private System.Windows.Forms.DateTimePicker clock_start;
        private System.Windows.Forms.DateTimePicker clock_stop;
        private System.Windows.Forms.DateTimePicker day_stop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox name;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox bcurl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox contractAddr;
        private System.Windows.Forms.NumericUpDown voterCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FolderBrowserDialog fileLocPicker;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox prKey;
    }
}

