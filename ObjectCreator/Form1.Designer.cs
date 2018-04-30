namespace ObjectCreator
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listOfEnemyTextures = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EnemyTexturePreview = new System.Windows.Forms.PictureBox();
            this.enemyTextureID = new System.Windows.Forms.TextBox();
            this.enemyIDTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.enemyNameTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.enemyDamageListBox = new System.Windows.Forms.ListBox();
            this.enemyDamageTypeDPL = new System.Windows.Forms.ComboBox();
            this.enemyDamageNumber = new System.Windows.Forms.NumericUpDown();
            this.enemyAddDamageButton = new System.Windows.Forms.Button();
            this.enemyRemoveDamageButton = new System.Windows.Forms.Button();
            this.enemyDPL = new System.Windows.Forms.ComboBox();
            this.enemyLoad = new System.Windows.Forms.Button();
            this.enemyRemoveDefenceButton = new System.Windows.Forms.Button();
            this.enemyAddDefenceButton = new System.Windows.Forms.Button();
            this.enemyDefenceNumber = new System.Windows.Forms.NumericUpDown();
            this.enemyDefenceDPL = new System.Windows.Forms.ComboBox();
            this.enemyDefenceListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnemyTexturePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyDamageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyDefenceNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.enemyRemoveDefenceButton);
            this.tabPage1.Controls.Add(this.enemyAddDefenceButton);
            this.tabPage1.Controls.Add(this.enemyDefenceNumber);
            this.tabPage1.Controls.Add(this.enemyDefenceDPL);
            this.tabPage1.Controls.Add(this.enemyDefenceListBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.enemyLoad);
            this.tabPage1.Controls.Add(this.enemyDPL);
            this.tabPage1.Controls.Add(this.enemyRemoveDamageButton);
            this.tabPage1.Controls.Add(this.enemyAddDamageButton);
            this.tabPage1.Controls.Add(this.enemyDamageNumber);
            this.tabPage1.Controls.Add(this.enemyDamageTypeDPL);
            this.tabPage1.Controls.Add(this.enemyDamageListBox);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.enemyNameTextbox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.enemyIDTextbox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.enemyTextureID);
            this.tabPage1.Controls.Add(this.EnemyTexturePreview);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listOfEnemyTextures);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Enemy";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Items";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listOfEnemyTextures
            // 
            this.listOfEnemyTextures.FormattingEnabled = true;
            this.listOfEnemyTextures.Location = new System.Drawing.Point(169, 72);
            this.listOfEnemyTextures.Name = "listOfEnemyTextures";
            this.listOfEnemyTextures.Size = new System.Drawing.Size(120, 21);
            this.listOfEnemyTextures.TabIndex = 0;
            this.listOfEnemyTextures.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Textures:";
            // 
            // EnemyTexturePreview
            // 
            this.EnemyTexturePreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EnemyTexturePreview.Location = new System.Drawing.Point(6, 6);
            this.EnemyTexturePreview.Name = "EnemyTexturePreview";
            this.EnemyTexturePreview.Size = new System.Drawing.Size(64, 64);
            this.EnemyTexturePreview.TabIndex = 2;
            this.EnemyTexturePreview.TabStop = false;
            // 
            // enemyTextureID
            // 
            this.enemyTextureID.Location = new System.Drawing.Point(63, 73);
            this.enemyTextureID.Name = "enemyTextureID";
            this.enemyTextureID.Size = new System.Drawing.Size(100, 20);
            this.enemyTextureID.TabIndex = 3;
            // 
            // enemyIDTextbox
            // 
            this.enemyIDTextbox.Location = new System.Drawing.Point(63, 99);
            this.enemyIDTextbox.Name = "enemyIDTextbox";
            this.enemyIDTextbox.Size = new System.Drawing.Size(100, 20);
            this.enemyIDTextbox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "ID:";
            // 
            // enemyNameTextbox
            // 
            this.enemyNameTextbox.Location = new System.Drawing.Point(63, 125);
            this.enemyNameTextbox.Name = "enemyNameTextbox";
            this.enemyNameTextbox.Size = new System.Drawing.Size(100, 20);
            this.enemyNameTextbox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Damage:";
            // 
            // enemyDamageListBox
            // 
            this.enemyDamageListBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.enemyDamageListBox.FormattingEnabled = true;
            this.enemyDamageListBox.Location = new System.Drawing.Point(62, 151);
            this.enemyDamageListBox.Name = "enemyDamageListBox";
            this.enemyDamageListBox.Size = new System.Drawing.Size(100, 121);
            this.enemyDamageListBox.TabIndex = 9;
            // 
            // enemyDamageTypeDPL
            // 
            this.enemyDamageTypeDPL.FormattingEnabled = true;
            this.enemyDamageTypeDPL.Location = new System.Drawing.Point(168, 154);
            this.enemyDamageTypeDPL.Name = "enemyDamageTypeDPL";
            this.enemyDamageTypeDPL.Size = new System.Drawing.Size(121, 21);
            this.enemyDamageTypeDPL.TabIndex = 10;
            // 
            // enemyDamageNumber
            // 
            this.enemyDamageNumber.Location = new System.Drawing.Point(169, 182);
            this.enemyDamageNumber.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.enemyDamageNumber.Name = "enemyDamageNumber";
            this.enemyDamageNumber.Size = new System.Drawing.Size(120, 20);
            this.enemyDamageNumber.TabIndex = 11;
            // 
            // enemyAddDamageButton
            // 
            this.enemyAddDamageButton.Location = new System.Drawing.Point(169, 209);
            this.enemyAddDamageButton.Name = "enemyAddDamageButton";
            this.enemyAddDamageButton.Size = new System.Drawing.Size(120, 23);
            this.enemyAddDamageButton.TabIndex = 12;
            this.enemyAddDamageButton.Text = "Add damage";
            this.enemyAddDamageButton.UseVisualStyleBackColor = true;
            this.enemyAddDamageButton.Click += new System.EventHandler(this.enemyAddDamageButton_Click);
            // 
            // enemyRemoveDamageButton
            // 
            this.enemyRemoveDamageButton.Location = new System.Drawing.Point(169, 238);
            this.enemyRemoveDamageButton.Name = "enemyRemoveDamageButton";
            this.enemyRemoveDamageButton.Size = new System.Drawing.Size(120, 23);
            this.enemyRemoveDamageButton.TabIndex = 13;
            this.enemyRemoveDamageButton.Text = "Remove damage";
            this.enemyRemoveDamageButton.UseVisualStyleBackColor = true;
            this.enemyRemoveDamageButton.Click += new System.EventHandler(this.enemyRemoveDamageButton_Click);
            // 
            // enemyDPL
            // 
            this.enemyDPL.FormattingEnabled = true;
            this.enemyDPL.Location = new System.Drawing.Point(76, 6);
            this.enemyDPL.Name = "enemyDPL";
            this.enemyDPL.Size = new System.Drawing.Size(120, 21);
            this.enemyDPL.TabIndex = 14;
            // 
            // enemyLoad
            // 
            this.enemyLoad.Location = new System.Drawing.Point(76, 33);
            this.enemyLoad.Name = "enemyLoad";
            this.enemyLoad.Size = new System.Drawing.Size(120, 23);
            this.enemyLoad.TabIndex = 15;
            this.enemyLoad.Text = "Load";
            this.enemyLoad.UseVisualStyleBackColor = true;
            this.enemyLoad.Click += new System.EventHandler(this.enemyLoad_Click);
            // 
            // enemyRemoveDefenceButton
            // 
            this.enemyRemoveDefenceButton.Location = new System.Drawing.Point(169, 363);
            this.enemyRemoveDefenceButton.Name = "enemyRemoveDefenceButton";
            this.enemyRemoveDefenceButton.Size = new System.Drawing.Size(120, 23);
            this.enemyRemoveDefenceButton.TabIndex = 21;
            this.enemyRemoveDefenceButton.Text = "Remove defence";
            this.enemyRemoveDefenceButton.UseVisualStyleBackColor = true;
            // 
            // enemyAddDefenceButton
            // 
            this.enemyAddDefenceButton.Location = new System.Drawing.Point(169, 334);
            this.enemyAddDefenceButton.Name = "enemyAddDefenceButton";
            this.enemyAddDefenceButton.Size = new System.Drawing.Size(120, 23);
            this.enemyAddDefenceButton.TabIndex = 20;
            this.enemyAddDefenceButton.Text = "Add defence";
            this.enemyAddDefenceButton.UseVisualStyleBackColor = true;
            this.enemyAddDefenceButton.Click += new System.EventHandler(this.enemyAddDefenceButton_Click);
            // 
            // enemyDefenceNumber
            // 
            this.enemyDefenceNumber.Location = new System.Drawing.Point(169, 307);
            this.enemyDefenceNumber.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.enemyDefenceNumber.Name = "enemyDefenceNumber";
            this.enemyDefenceNumber.Size = new System.Drawing.Size(120, 20);
            this.enemyDefenceNumber.TabIndex = 19;
            // 
            // enemyDefenceDPL
            // 
            this.enemyDefenceDPL.FormattingEnabled = true;
            this.enemyDefenceDPL.Location = new System.Drawing.Point(168, 279);
            this.enemyDefenceDPL.Name = "enemyDefenceDPL";
            this.enemyDefenceDPL.Size = new System.Drawing.Size(121, 21);
            this.enemyDefenceDPL.TabIndex = 18;
            // 
            // enemyDefenceListBox
            // 
            this.enemyDefenceListBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.enemyDefenceListBox.FormattingEnabled = true;
            this.enemyDefenceListBox.Location = new System.Drawing.Point(62, 276);
            this.enemyDefenceListBox.Name = "enemyDefenceListBox";
            this.enemyDefenceListBox.Size = new System.Drawing.Size(100, 121);
            this.enemyDefenceListBox.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Defence:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EnemyTexturePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyDamageNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyDefenceNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox EnemyTexturePreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listOfEnemyTextures;
        private System.Windows.Forms.TextBox enemyTextureID;
        private System.Windows.Forms.Button enemyAddDamageButton;
        private System.Windows.Forms.NumericUpDown enemyDamageNumber;
        private System.Windows.Forms.ComboBox enemyDamageTypeDPL;
        private System.Windows.Forms.ListBox enemyDamageListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox enemyNameTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox enemyIDTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button enemyRemoveDamageButton;
        private System.Windows.Forms.Button enemyLoad;
        private System.Windows.Forms.ComboBox enemyDPL;
        private System.Windows.Forms.Button enemyRemoveDefenceButton;
        private System.Windows.Forms.Button enemyAddDefenceButton;
        private System.Windows.Forms.NumericUpDown enemyDefenceNumber;
        private System.Windows.Forms.ComboBox enemyDefenceDPL;
        private System.Windows.Forms.ListBox enemyDefenceListBox;
        private System.Windows.Forms.Label label5;
    }
}

