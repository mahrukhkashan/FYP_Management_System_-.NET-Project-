namespace ProjectA_2022_CS_45_.Advisor
{
    partial class AddAdvisor
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.AdvisorName_textBox = new System.Windows.Forms.TextBox();
            this.Salary_textBox = new System.Windows.Forms.TextBox();
            this.FirstName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.advisor_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.designation_comboBox = new System.Windows.Forms.ComboBox();
            this.DOB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LastName = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.Contact = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.Email = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.MaleButton = new System.Windows.Forms.RadioButton();
            this.advisorEmail_textBox = new System.Windows.Forms.TextBox();
            this.FemaleButton = new System.Windows.Forms.RadioButton();
            this.advisorContact_textBox = new System.Windows.Forms.TextBox();
            this.Gender = new System.Windows.Forms.Label();
            this.AdvisorLastname_textBox = new System.Windows.Forms.TextBox();
            this.AdvisorList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.AdvisorList)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(205, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(333, 39);
            this.label4.TabIndex = 118;
            this.label4.Text = "FYP Management System";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Algerian", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(214, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(327, 43);
            this.label5.TabIndex = 117;
            this.label5.Text = "Advisor Information";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(703, 417);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 116;
            this.button1.Text = "BACK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdvisorName_textBox
            // 
            this.AdvisorName_textBox.BackColor = System.Drawing.Color.White;
            this.AdvisorName_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvisorName_textBox.Location = new System.Drawing.Point(25, 117);
            this.AdvisorName_textBox.Name = "AdvisorName_textBox";
            this.AdvisorName_textBox.Size = new System.Drawing.Size(167, 20);
            this.AdvisorName_textBox.TabIndex = 106;
            // 
            // Salary_textBox
            // 
            this.Salary_textBox.Location = new System.Drawing.Point(440, 212);
            this.Salary_textBox.Name = "Salary_textBox";
            this.Salary_textBox.Size = new System.Drawing.Size(160, 20);
            this.Salary_textBox.TabIndex = 115;
            // 
            // FirstName
            // 
            this.FirstName.AutoSize = true;
            this.FirstName.BackColor = System.Drawing.Color.Transparent;
            this.FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FirstName.Location = new System.Drawing.Point(437, 140);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(120, 16);
            this.FirstName.TabIndex = 96;
            this.FirstName.Text = "Select Designation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(437, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 97;
            this.label1.Text = "Enter Salary amount";
            // 
            // advisor_dateTimePicker
            // 
            this.advisor_dateTimePicker.Location = new System.Drawing.Point(213, 159);
            this.advisor_dateTimePicker.Name = "advisor_dateTimePicker";
            this.advisor_dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.advisor_dateTimePicker.TabIndex = 114;
            // 
            // designation_comboBox
            // 
            this.designation_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.designation_comboBox.FormattingEnabled = true;
            this.designation_comboBox.Items.AddRange(new object[] {
            "Professor",
            "Associate Professor",
            "Assisstant Professor",
            "Lecturer",
            "Industry Professional"});
            this.designation_comboBox.Location = new System.Drawing.Point(440, 159);
            this.designation_comboBox.Name = "designation_comboBox";
            this.designation_comboBox.Size = new System.Drawing.Size(121, 21);
            this.designation_comboBox.TabIndex = 98;
            this.designation_comboBox.Text = "Designations";
            // 
            // DOB
            // 
            this.DOB.AutoSize = true;
            this.DOB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DOB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DOB.Location = new System.Drawing.Point(216, 140);
            this.DOB.Name = "DOB";
            this.DOB.Size = new System.Drawing.Size(65, 16);
            this.DOB.TabIndex = 113;
            this.DOB.Text = "Birth Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(22, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 99;
            this.label3.Text = "First Name";
            // 
            // LastName
            // 
            this.LastName.AutoSize = true;
            this.LastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LastName.Location = new System.Drawing.Point(209, 98);
            this.LastName.Name = "LastName";
            this.LastName.Size = new System.Drawing.Size(72, 16);
            this.LastName.TabIndex = 100;
            this.LastName.Text = "Last Name";
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.Linen;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.DeleteButton.Location = new System.Drawing.Point(676, 167);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 112;
            this.DeleteButton.Text = "DELETE";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // Contact
            // 
            this.Contact.AutoSize = true;
            this.Contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contact.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Contact.Location = new System.Drawing.Point(22, 140);
            this.Contact.Name = "Contact";
            this.Contact.Size = new System.Drawing.Size(52, 16);
            this.Contact.TabIndex = 101;
            this.Contact.Text = "Contact";
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.Linen;
            this.UpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.UpdateButton.Location = new System.Drawing.Point(676, 138);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 111;
            this.UpdateButton.Text = "UPDATE";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Email.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Email.Location = new System.Drawing.Point(22, 193);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(41, 16);
            this.Email.TabIndex = 102;
            this.Email.Text = "Email";
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.Color.Linen;
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ForeColor = System.Drawing.SystemColors.Desktop;
            this.AddButton.Location = new System.Drawing.Point(676, 109);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 110;
            this.AddButton.Text = "ADD";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MaleButton
            // 
            this.MaleButton.AutoSize = true;
            this.MaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaleButton.ForeColor = System.Drawing.Color.Black;
            this.MaleButton.Location = new System.Drawing.Point(440, 112);
            this.MaleButton.Name = "MaleButton";
            this.MaleButton.Size = new System.Drawing.Size(55, 20);
            this.MaleButton.TabIndex = 103;
            this.MaleButton.TabStop = true;
            this.MaleButton.Text = "Male";
            this.MaleButton.UseVisualStyleBackColor = true;
            // 
            // advisorEmail_textBox
            // 
            this.advisorEmail_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.advisorEmail_textBox.Location = new System.Drawing.Point(25, 212);
            this.advisorEmail_textBox.Name = "advisorEmail_textBox";
            this.advisorEmail_textBox.Size = new System.Drawing.Size(347, 20);
            this.advisorEmail_textBox.TabIndex = 109;
            // 
            // FemaleButton
            // 
            this.FemaleButton.AutoSize = true;
            this.FemaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FemaleButton.ForeColor = System.Drawing.Color.Black;
            this.FemaleButton.Location = new System.Drawing.Point(515, 112);
            this.FemaleButton.Name = "FemaleButton";
            this.FemaleButton.Size = new System.Drawing.Size(71, 20);
            this.FemaleButton.TabIndex = 104;
            this.FemaleButton.TabStop = true;
            this.FemaleButton.Text = "Female";
            this.FemaleButton.UseVisualStyleBackColor = true;
            // 
            // advisorContact_textBox
            // 
            this.advisorContact_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.advisorContact_textBox.Location = new System.Drawing.Point(25, 159);
            this.advisorContact_textBox.Name = "advisorContact_textBox";
            this.advisorContact_textBox.Size = new System.Drawing.Size(167, 20);
            this.advisorContact_textBox.TabIndex = 108;
            // 
            // Gender
            // 
            this.Gender.AutoSize = true;
            this.Gender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gender.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Gender.Location = new System.Drawing.Point(437, 93);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(52, 16);
            this.Gender.TabIndex = 105;
            this.Gender.Text = "Gender";
            // 
            // AdvisorLastname_textBox
            // 
            this.AdvisorLastname_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AdvisorLastname_textBox.Location = new System.Drawing.Point(212, 117);
            this.AdvisorLastname_textBox.Name = "AdvisorLastname_textBox";
            this.AdvisorLastname_textBox.Size = new System.Drawing.Size(160, 20);
            this.AdvisorLastname_textBox.TabIndex = 107;
            // 
            // AdvisorList
            // 
            this.AdvisorList.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.AdvisorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AdvisorList.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AdvisorList.Location = new System.Drawing.Point(25, 250);
            this.AdvisorList.Name = "AdvisorList";
            this.AdvisorList.Size = new System.Drawing.Size(753, 161);
            this.AdvisorList.TabIndex = 95;
            this.AdvisorList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdvisorList_CellContentClick);
            // 
            // AddAdvisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AdvisorName_textBox);
            this.Controls.Add(this.Salary_textBox);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.advisor_dateTimePicker);
            this.Controls.Add(this.designation_comboBox);
            this.Controls.Add(this.DOB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LastName);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.Contact);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.MaleButton);
            this.Controls.Add(this.advisorEmail_textBox);
            this.Controls.Add(this.FemaleButton);
            this.Controls.Add(this.advisorContact_textBox);
            this.Controls.Add(this.Gender);
            this.Controls.Add(this.AdvisorLastname_textBox);
            this.Controls.Add(this.AdvisorList);
            this.Name = "AddAdvisor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advisor";
            this.Load += new System.EventHandler(this.Advisor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AdvisorList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox AdvisorName_textBox;
        private System.Windows.Forms.TextBox Salary_textBox;
        private System.Windows.Forms.Label FirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker advisor_dateTimePicker;
        private System.Windows.Forms.ComboBox designation_comboBox;
        private System.Windows.Forms.Label DOB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LastName;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label Contact;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.RadioButton MaleButton;
        private System.Windows.Forms.TextBox advisorEmail_textBox;
        private System.Windows.Forms.RadioButton FemaleButton;
        private System.Windows.Forms.TextBox advisorContact_textBox;
        private System.Windows.Forms.Label Gender;
        private System.Windows.Forms.TextBox AdvisorLastname_textBox;
        private System.Windows.Forms.DataGridView AdvisorList;
    }
}