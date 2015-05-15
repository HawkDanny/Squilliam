namespace DifficultyEditor
{
    partial class difficultyEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(difficultyEditor));
            this.factionPic = new System.Windows.Forms.PictureBox();
            this.aggressionLabel = new System.Windows.Forms.Label();
            this.defenseLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.factionButton4 = new System.Windows.Forms.RadioButton();
            this.factionButton3 = new System.Windows.Forms.RadioButton();
            this.factionButton2 = new System.Windows.Forms.RadioButton();
            this.factionButton1 = new System.Windows.Forms.RadioButton();
            this.factionTypeLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.aggressionTrackBar = new System.Windows.Forms.TrackBar();
            this.defenseTrackBar = new System.Windows.Forms.TrackBar();
            this.abilitiesTrackBar = new System.Windows.Forms.TrackBar();
            this.cowardiceTrackBar = new System.Windows.Forms.TrackBar();
            this.abilitiesLabel = new System.Windows.Forms.Label();
            this.cowardiceLabel = new System.Windows.Forms.Label();
            this.informationLabel = new System.Windows.Forms.Label();
            this.toggleFactionLabel = new System.Windows.Forms.Label();
            this.aggressionPercentLabel = new System.Windows.Forms.Label();
            this.defensePercentLabel = new System.Windows.Forms.Label();
            this.abilitiesPercentLabel = new System.Windows.Forms.Label();
            this.cowardicePercentLabel = new System.Windows.Forms.Label();
            this.createButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.factionPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aggressionTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.defenseTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.abilitiesTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cowardiceTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // factionPic
            // 
            this.factionPic.Image = ((System.Drawing.Image)(resources.GetObject("factionPic.Image")));
            this.factionPic.Location = new System.Drawing.Point(346, 82);
            this.factionPic.Name = "factionPic";
            this.factionPic.Size = new System.Drawing.Size(74, 74);
            this.factionPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.factionPic.TabIndex = 0;
            this.factionPic.TabStop = false;
            // 
            // aggressionLabel
            // 
            this.aggressionLabel.AutoSize = true;
            this.aggressionLabel.Location = new System.Drawing.Point(16, 73);
            this.aggressionLabel.Name = "aggressionLabel";
            this.aggressionLabel.Size = new System.Drawing.Size(59, 13);
            this.aggressionLabel.TabIndex = 1;
            this.aggressionLabel.Text = "Aggression";
            this.aggressionLabel.MouseEnter += new System.EventHandler(this.aggressionLabel_MouseEnter);
            this.aggressionLabel.MouseLeave += new System.EventHandler(this.aggressionLabel_MouseLeave);
            // 
            // defenseLabel
            // 
            this.defenseLabel.AutoSize = true;
            this.defenseLabel.Location = new System.Drawing.Point(16, 97);
            this.defenseLabel.Name = "defenseLabel";
            this.defenseLabel.Size = new System.Drawing.Size(47, 13);
            this.defenseLabel.TabIndex = 2;
            this.defenseLabel.Text = "Defense";
            this.defenseLabel.MouseEnter += new System.EventHandler(this.defenseLabel_MouseEnter);
            this.defenseLabel.MouseLeave += new System.EventHandler(this.defenseLabel_MouseLeave);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(16, 50);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Name";
            // 
            // factionButton4
            // 
            this.factionButton4.AutoSize = true;
            this.factionButton4.Location = new System.Drawing.Point(406, 175);
            this.factionButton4.Name = "factionButton4";
            this.factionButton4.Size = new System.Drawing.Size(14, 13);
            this.factionButton4.TabIndex = 4;
            this.factionButton4.TabStop = true;
            this.factionButton4.UseVisualStyleBackColor = true;
            this.factionButton4.CheckedChanged += new System.EventHandler(this.factionButton4_CheckedChanged);
            // 
            // factionButton3
            // 
            this.factionButton3.AutoSize = true;
            this.factionButton3.Location = new System.Drawing.Point(386, 175);
            this.factionButton3.Name = "factionButton3";
            this.factionButton3.Size = new System.Drawing.Size(14, 13);
            this.factionButton3.TabIndex = 3;
            this.factionButton3.UseVisualStyleBackColor = true;
            this.factionButton3.CheckedChanged += new System.EventHandler(this.factionButton3_CheckedChanged);
            // 
            // factionButton2
            // 
            this.factionButton2.AutoSize = true;
            this.factionButton2.Location = new System.Drawing.Point(366, 175);
            this.factionButton2.Name = "factionButton2";
            this.factionButton2.Size = new System.Drawing.Size(14, 13);
            this.factionButton2.TabIndex = 2;
            this.factionButton2.UseVisualStyleBackColor = true;
            this.factionButton2.CheckedChanged += new System.EventHandler(this.factionButton2_CheckedChanged);
            // 
            // factionButton1
            // 
            this.factionButton1.AutoSize = true;
            this.factionButton1.Checked = true;
            this.factionButton1.Location = new System.Drawing.Point(346, 175);
            this.factionButton1.Name = "factionButton1";
            this.factionButton1.Size = new System.Drawing.Size(14, 13);
            this.factionButton1.TabIndex = 1;
            this.factionButton1.TabStop = true;
            this.factionButton1.UseVisualStyleBackColor = true;
            this.factionButton1.CheckedChanged += new System.EventHandler(this.factionButton1_CheckedChanged);
            // 
            // factionTypeLabel
            // 
            this.factionTypeLabel.AutoSize = true;
            this.factionTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.factionTypeLabel.Location = new System.Drawing.Point(105, 7);
            this.factionTypeLabel.Name = "factionTypeLabel";
            this.factionTypeLabel.Size = new System.Drawing.Size(179, 37);
            this.factionTypeLabel.TabIndex = 8;
            this.factionTypeLabel.Text = "Good Guys";
            this.factionTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(91, 47);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(210, 20);
            this.nameTextBox.TabIndex = 10;
            // 
            // aggressionTrackBar
            // 
            this.aggressionTrackBar.AutoSize = false;
            this.aggressionTrackBar.Location = new System.Drawing.Point(91, 73);
            this.aggressionTrackBar.Name = "aggressionTrackBar";
            this.aggressionTrackBar.Size = new System.Drawing.Size(210, 20);
            this.aggressionTrackBar.TabIndex = 11;
            this.aggressionTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.aggressionTrackBar.Value = 10;
            this.aggressionTrackBar.ValueChanged += new System.EventHandler(this.aggressionTrackBar_ValueChanged);
            this.aggressionTrackBar.MouseEnter += new System.EventHandler(this.aggressionTrackBar_MouseEnter);
            this.aggressionTrackBar.MouseLeave += new System.EventHandler(this.aggressionTrackBar_MouseLeave);
            // 
            // defenseTrackBar
            // 
            this.defenseTrackBar.AutoSize = false;
            this.defenseTrackBar.Location = new System.Drawing.Point(91, 97);
            this.defenseTrackBar.Name = "defenseTrackBar";
            this.defenseTrackBar.Size = new System.Drawing.Size(210, 20);
            this.defenseTrackBar.TabIndex = 12;
            this.defenseTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.defenseTrackBar.Value = 10;
            this.defenseTrackBar.ValueChanged += new System.EventHandler(this.defenseTrackBar_ValueChanged);
            this.defenseTrackBar.MouseEnter += new System.EventHandler(this.defenseTrackBar_MouseEnter);
            this.defenseTrackBar.MouseLeave += new System.EventHandler(this.defenseTrackBar_MouseLeave);
            // 
            // abilitiesTrackBar
            // 
            this.abilitiesTrackBar.AutoSize = false;
            this.abilitiesTrackBar.Location = new System.Drawing.Point(91, 124);
            this.abilitiesTrackBar.Name = "abilitiesTrackBar";
            this.abilitiesTrackBar.Size = new System.Drawing.Size(210, 20);
            this.abilitiesTrackBar.TabIndex = 13;
            this.abilitiesTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.abilitiesTrackBar.Value = 10;
            this.abilitiesTrackBar.ValueChanged += new System.EventHandler(this.abilitiesTrackBar_ValueChanged);
            this.abilitiesTrackBar.MouseEnter += new System.EventHandler(this.abilitiesTrackBar_MouseEnter);
            this.abilitiesTrackBar.MouseLeave += new System.EventHandler(this.abilitiesTrackBar_MouseLeave);
            // 
            // cowardiceTrackBar
            // 
            this.cowardiceTrackBar.AutoSize = false;
            this.cowardiceTrackBar.Location = new System.Drawing.Point(91, 148);
            this.cowardiceTrackBar.Name = "cowardiceTrackBar";
            this.cowardiceTrackBar.Size = new System.Drawing.Size(210, 20);
            this.cowardiceTrackBar.TabIndex = 14;
            this.cowardiceTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.cowardiceTrackBar.Value = 10;
            this.cowardiceTrackBar.ValueChanged += new System.EventHandler(this.cowardiceTrackBar_ValueChanged);
            this.cowardiceTrackBar.MouseEnter += new System.EventHandler(this.cowardiceTrackBar_MouseEnter);
            this.cowardiceTrackBar.MouseLeave += new System.EventHandler(this.cowardiceTrackBar_MouseLeave);
            // 
            // abilitiesLabel
            // 
            this.abilitiesLabel.AutoSize = true;
            this.abilitiesLabel.Location = new System.Drawing.Point(16, 124);
            this.abilitiesLabel.Name = "abilitiesLabel";
            this.abilitiesLabel.Size = new System.Drawing.Size(42, 13);
            this.abilitiesLabel.TabIndex = 15;
            this.abilitiesLabel.Text = "Abilities";
            this.abilitiesLabel.MouseEnter += new System.EventHandler(this.abilitiesLabel_MouseEnter);
            this.abilitiesLabel.MouseLeave += new System.EventHandler(this.abilitiesLabel_MouseLeave);
            // 
            // cowardiceLabel
            // 
            this.cowardiceLabel.AutoSize = true;
            this.cowardiceLabel.Location = new System.Drawing.Point(16, 148);
            this.cowardiceLabel.Name = "cowardiceLabel";
            this.cowardiceLabel.Size = new System.Drawing.Size(57, 13);
            this.cowardiceLabel.TabIndex = 16;
            this.cowardiceLabel.Text = "Cowardice";
            this.cowardiceLabel.MouseEnter += new System.EventHandler(this.cowardiceLabel_MouseEnter);
            this.cowardiceLabel.MouseLeave += new System.EventHandler(this.cowardiceLabel_MouseLeave);
            // 
            // informationLabel
            // 
            this.informationLabel.Location = new System.Drawing.Point(16, 196);
            this.informationLabel.Name = "informationLabel";
            this.informationLabel.Size = new System.Drawing.Size(404, 13);
            this.informationLabel.TabIndex = 17;
            // 
            // toggleFactionLabel
            // 
            this.toggleFactionLabel.AutoSize = true;
            this.toggleFactionLabel.Location = new System.Drawing.Point(343, 159);
            this.toggleFactionLabel.Name = "toggleFactionLabel";
            this.toggleFactionLabel.Size = new System.Drawing.Size(78, 13);
            this.toggleFactionLabel.TabIndex = 18;
            this.toggleFactionLabel.Text = "Toggle Faction";
            // 
            // aggressionPercentLabel
            // 
            this.aggressionPercentLabel.AutoSize = true;
            this.aggressionPercentLabel.Location = new System.Drawing.Point(307, 73);
            this.aggressionPercentLabel.Name = "aggressionPercentLabel";
            this.aggressionPercentLabel.Size = new System.Drawing.Size(33, 13);
            this.aggressionPercentLabel.TabIndex = 19;
            this.aggressionPercentLabel.Text = "100%";
            this.aggressionPercentLabel.MouseEnter += new System.EventHandler(this.aggressionPercentLabel_MouseEnter);
            this.aggressionPercentLabel.MouseLeave += new System.EventHandler(this.aggressionPercentLabel_MouseLeave);
            // 
            // defensePercentLabel
            // 
            this.defensePercentLabel.AutoSize = true;
            this.defensePercentLabel.Location = new System.Drawing.Point(307, 97);
            this.defensePercentLabel.Name = "defensePercentLabel";
            this.defensePercentLabel.Size = new System.Drawing.Size(33, 13);
            this.defensePercentLabel.TabIndex = 20;
            this.defensePercentLabel.Text = "100%";
            this.defensePercentLabel.MouseEnter += new System.EventHandler(this.defensePercentLabel_MouseEnter);
            this.defensePercentLabel.MouseLeave += new System.EventHandler(this.defensePercentLabel_MouseLeave);
            // 
            // abilitiesPercentLabel
            // 
            this.abilitiesPercentLabel.AutoSize = true;
            this.abilitiesPercentLabel.Location = new System.Drawing.Point(307, 124);
            this.abilitiesPercentLabel.Name = "abilitiesPercentLabel";
            this.abilitiesPercentLabel.Size = new System.Drawing.Size(33, 13);
            this.abilitiesPercentLabel.TabIndex = 21;
            this.abilitiesPercentLabel.Text = "100%";
            this.abilitiesPercentLabel.MouseEnter += new System.EventHandler(this.abilitiesPercentLabel_MouseEnter);
            this.abilitiesPercentLabel.MouseLeave += new System.EventHandler(this.abilitiesPercentLabel_MouseLeave);
            // 
            // cowardicePercentLabel
            // 
            this.cowardicePercentLabel.AutoSize = true;
            this.cowardicePercentLabel.Location = new System.Drawing.Point(307, 148);
            this.cowardicePercentLabel.Name = "cowardicePercentLabel";
            this.cowardicePercentLabel.Size = new System.Drawing.Size(33, 13);
            this.cowardicePercentLabel.TabIndex = 22;
            this.cowardicePercentLabel.Text = "100%";
            this.cowardicePercentLabel.MouseEnter += new System.EventHandler(this.cowardicePercentLabel_MouseEnter);
            this.cowardicePercentLabel.MouseLeave += new System.EventHandler(this.cowardicePercentLabel_MouseLeave);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(19, 170);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(321, 23);
            this.createButton.TabIndex = 23;
            this.createButton.Text = "Create Enemy Character";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // difficultyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 216);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.cowardicePercentLabel);
            this.Controls.Add(this.abilitiesPercentLabel);
            this.Controls.Add(this.defensePercentLabel);
            this.Controls.Add(this.aggressionPercentLabel);
            this.Controls.Add(this.toggleFactionLabel);
            this.Controls.Add(this.informationLabel);
            this.Controls.Add(this.cowardiceLabel);
            this.Controls.Add(this.abilitiesLabel);
            this.Controls.Add(this.cowardiceTrackBar);
            this.Controls.Add(this.abilitiesTrackBar);
            this.Controls.Add(this.defenseTrackBar);
            this.Controls.Add(this.aggressionTrackBar);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.factionTypeLabel);
            this.Controls.Add(this.factionButton1);
            this.Controls.Add(this.factionButton2);
            this.Controls.Add(this.factionButton3);
            this.Controls.Add(this.factionButton4);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.defenseLabel);
            this.Controls.Add(this.aggressionLabel);
            this.Controls.Add(this.factionPic);
            this.Name = "difficultyEditor";
            this.Text = "Individual Enemy Creator";
            ((System.ComponentModel.ISupportInitialize)(this.factionPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aggressionTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.defenseTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.abilitiesTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cowardiceTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox factionPic;
        private System.Windows.Forms.Label aggressionLabel;
        private System.Windows.Forms.Label defenseLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.RadioButton factionButton4;
        private System.Windows.Forms.RadioButton factionButton3;
        private System.Windows.Forms.RadioButton factionButton2;
        private System.Windows.Forms.RadioButton factionButton1;
        private System.Windows.Forms.Label factionTypeLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TrackBar aggressionTrackBar;
        private System.Windows.Forms.TrackBar defenseTrackBar;
        private System.Windows.Forms.TrackBar abilitiesTrackBar;
        private System.Windows.Forms.TrackBar cowardiceTrackBar;
        private System.Windows.Forms.Label abilitiesLabel;
        private System.Windows.Forms.Label cowardiceLabel;
        private System.Windows.Forms.Label informationLabel;
        private System.Windows.Forms.Label toggleFactionLabel;
        private System.Windows.Forms.Label aggressionPercentLabel;
        private System.Windows.Forms.Label defensePercentLabel;
        private System.Windows.Forms.Label abilitiesPercentLabel;
        private System.Windows.Forms.Label cowardicePercentLabel;
        private System.Windows.Forms.Button createButton;
    }
}

