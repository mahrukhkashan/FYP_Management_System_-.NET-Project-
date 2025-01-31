namespace ProjectA_2022_CS_45_.GroupFormation
{
    partial class AssignProject
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
            this.label3 = new System.Windows.Forms.Label();
            this.FirstName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupID_comboBox = new System.Windows.Forms.ComboBox();
            this.ProjectTitle_comboBox = new System.Windows.Forms.ComboBox();
            this.assign_button = new System.Windows.Forms.Button();
            this.back_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Monotype Corsiva", 24F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(206, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(333, 39);
            this.label4.TabIndex = 20;
            this.label4.Text = "FYP Management System";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Algerian", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(217, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 30);
            this.label3.TabIndex = 19;
            this.label3.Text = "Project Assignment";
            // 
            // FirstName
            // 
            this.FirstName.AutoSize = true;
            this.FirstName.BackColor = System.Drawing.Color.Transparent;
            this.FirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FirstName.Location = new System.Drawing.Point(297, 142);
            this.FirstName.Name = "FirstName";
            this.FirstName.Size = new System.Drawing.Size(101, 16);
            this.FirstName.TabIndex = 110;
            this.FirstName.Text = "Select Group ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(297, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 111;
            this.label1.Text = "Select Project Title";
            // 
            // GroupID_comboBox
            // 
            this.GroupID_comboBox.FormattingEnabled = true;
            this.GroupID_comboBox.Location = new System.Drawing.Point(300, 162);
            this.GroupID_comboBox.Name = "GroupID_comboBox";
            this.GroupID_comboBox.Size = new System.Drawing.Size(121, 21);
            this.GroupID_comboBox.TabIndex = 112;
            // 
            // ProjectTitle_comboBox
            // 
            this.ProjectTitle_comboBox.FormattingEnabled = true;
            this.ProjectTitle_comboBox.Location = new System.Drawing.Point(300, 231);
            this.ProjectTitle_comboBox.Name = "ProjectTitle_comboBox";
            this.ProjectTitle_comboBox.Size = new System.Drawing.Size(121, 21);
            this.ProjectTitle_comboBox.TabIndex = 113;
            // 
            // assign_button
            // 
            this.assign_button.Location = new System.Drawing.Point(327, 295);
            this.assign_button.Name = "assign_button";
            this.assign_button.Size = new System.Drawing.Size(75, 23);
            this.assign_button.TabIndex = 114;
            this.assign_button.Text = "Assign";
            this.assign_button.UseVisualStyleBackColor = true;
            this.assign_button.Click += new System.EventHandler(this.assign_button_Click);
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(327, 340);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(75, 23);
            this.back_button.TabIndex = 115;
            this.back_button.Text = "BACK";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // AssignProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.back_button);
            this.Controls.Add(this.assign_button);
            this.Controls.Add(this.ProjectTitle_comboBox);
            this.Controls.Add(this.GroupID_comboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FirstName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "AssignProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssignProject";
            this.Load += new System.EventHandler(this.AssignProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label FirstName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox GroupID_comboBox;
        private System.Windows.Forms.ComboBox ProjectTitle_comboBox;
        private System.Windows.Forms.Button assign_button;
        private System.Windows.Forms.Button back_button;
    }
}