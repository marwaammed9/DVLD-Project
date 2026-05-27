namespace DVLD_Project
{
    partial class Relase_DetainLicense
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
            this.filterLicense1 = new DVLD_Project.FilterLicense();
            this.linkLabelLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.linkLabelLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAppID = new System.Windows.Forms.Label();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.lblAppFees = new System.Windows.Forms.Label();
            this.lblFineFees = new System.Windows.Forms.Label();
            this.lblReLicenseID = new System.Windows.Forms.Label();
            this.lblCreatedID = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterLicense1
            // 
            this.filterLicense1.LicenseID = -1;
            this.filterLicense1.Location = new System.Drawing.Point(19, 52);
            this.filterLicense1.Name = "filterLicense1";
            this.filterLicense1.Size = new System.Drawing.Size(895, 514);
            this.filterLicense1.TabIndex = 48;
            this.filterLicense1.LicenseIDChanged += new System.EventHandler(this.filterLicense1_LicenseIDChanged);
            // 
            // linkLabelLicenseInfo
            // 
            this.linkLabelLicenseInfo.AutoSize = true;
            this.linkLabelLicenseInfo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelLicenseInfo.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabelLicenseInfo.LinkColor = System.Drawing.Color.Navy;
            this.linkLabelLicenseInfo.Location = new System.Drawing.Point(247, 823);
            this.linkLabelLicenseInfo.Name = "linkLabelLicenseInfo";
            this.linkLabelLicenseInfo.Size = new System.Drawing.Size(126, 20);
            this.linkLabelLicenseInfo.TabIndex = 45;
            this.linkLabelLicenseInfo.TabStop = true;
            this.linkLabelLicenseInfo.Text = "Show License ";
            this.linkLabelLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLicenseInfo_LinkClicked);
            // 
            // linkLabelLicenseHistory
            // 
            this.linkLabelLicenseHistory.AutoSize = true;
            this.linkLabelLicenseHistory.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelLicenseHistory.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabelLicenseHistory.LinkColor = System.Drawing.Color.Navy;
            this.linkLabelLicenseHistory.Location = new System.Drawing.Point(56, 823);
            this.linkLabelLicenseHistory.Name = "linkLabelLicenseHistory";
            this.linkLabelLicenseHistory.Size = new System.Drawing.Size(185, 20);
            this.linkLabelLicenseHistory.TabIndex = 44;
            this.linkLabelLicenseHistory.TabStop = true;
            this.linkLabelLicenseHistory.Text = "Show License History";
            this.linkLabelLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLicenseHistory_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblAppID);
            this.groupBox2.Controls.Add(this.lblTotalFees);
            this.groupBox2.Controls.Add(this.lblAppFees);
            this.groupBox2.Controls.Add(this.lblFineFees);
            this.groupBox2.Controls.Add(this.lblReLicenseID);
            this.groupBox2.Controls.Add(this.lblCreatedID);
            this.groupBox2.Controls.Add(this.lblAppDate);
            this.groupBox2.Controls.Add(this.lblDetainID);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 572);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 235);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detain Info ";
            // 
            // label6
            // 
            this.label6.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(400, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 42);
            this.label6.TabIndex = 33;
            this.label6.Text = "Release App ID :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Image = global::DVLD_Project.Properties.Resources.money_32;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(56, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 42);
            this.label5.TabIndex = 32;
            this.label5.Text = "Total Fees :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Image = global::DVLD_Project.Properties.Resources.Man_32;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(400, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 28);
            this.label7.TabIndex = 31;
            this.label7.Text = "Created By :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(400, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "License ID :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Image = global::DVLD_Project.Properties.Resources.money_32;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(400, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 42);
            this.label4.TabIndex = 29;
            this.label4.Text = "Fine Fees :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Image = global::DVLD_Project.Properties.Resources.money_32;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(11, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 42);
            this.label2.TabIndex = 28;
            this.label2.Text = "Application Fees :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Image = global::DVLD_Project.Properties.Resources.Calendar_32;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(53, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 36);
            this.label1.TabIndex = 27;
            this.label1.Text = "Detain Date :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAppID
            // 
            this.lblAppID.AutoSize = true;
            this.lblAppID.ForeColor = System.Drawing.Color.Maroon;
            this.lblAppID.Location = new System.Drawing.Point(587, 197);
            this.lblAppID.Name = "lblAppID";
            this.lblAppID.Size = new System.Drawing.Size(39, 20);
            this.lblAppID.TabIndex = 26;
            this.lblAppID.Text = "???";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.ForeColor = System.Drawing.Color.Maroon;
            this.lblTotalFees.Location = new System.Drawing.Point(229, 197);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(39, 20);
            this.lblTotalFees.TabIndex = 24;
            this.lblTotalFees.Text = "???";
            // 
            // lblAppFees
            // 
            this.lblAppFees.AutoSize = true;
            this.lblAppFees.ForeColor = System.Drawing.Color.Maroon;
            this.lblAppFees.Location = new System.Drawing.Point(229, 146);
            this.lblAppFees.Name = "lblAppFees";
            this.lblAppFees.Size = new System.Drawing.Size(39, 20);
            this.lblAppFees.TabIndex = 22;
            this.lblAppFees.Text = "???";
            // 
            // lblFineFees
            // 
            this.lblFineFees.AutoSize = true;
            this.lblFineFees.ForeColor = System.Drawing.Color.Maroon;
            this.lblFineFees.Location = new System.Drawing.Point(587, 149);
            this.lblFineFees.Name = "lblFineFees";
            this.lblFineFees.Size = new System.Drawing.Size(39, 20);
            this.lblFineFees.TabIndex = 20;
            this.lblFineFees.Text = "???";
            // 
            // lblReLicenseID
            // 
            this.lblReLicenseID.AutoSize = true;
            this.lblReLicenseID.ForeColor = System.Drawing.Color.Maroon;
            this.lblReLicenseID.Location = new System.Drawing.Point(587, 40);
            this.lblReLicenseID.Name = "lblReLicenseID";
            this.lblReLicenseID.Size = new System.Drawing.Size(39, 20);
            this.lblReLicenseID.TabIndex = 19;
            this.lblReLicenseID.Text = "???";
            // 
            // lblCreatedID
            // 
            this.lblCreatedID.AutoSize = true;
            this.lblCreatedID.ForeColor = System.Drawing.Color.Maroon;
            this.lblCreatedID.Location = new System.Drawing.Point(587, 98);
            this.lblCreatedID.Name = "lblCreatedID";
            this.lblCreatedID.Size = new System.Drawing.Size(39, 20);
            this.lblCreatedID.TabIndex = 13;
            this.lblCreatedID.Text = "???";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblAppDate.Location = new System.Drawing.Point(229, 94);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(39, 20);
            this.lblAppDate.TabIndex = 3;
            this.lblAppDate.Text = "???";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.ForeColor = System.Drawing.Color.Maroon;
            this.lblDetainID.Location = new System.Drawing.Point(229, 40);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(39, 20);
            this.lblDetainID.TabIndex = 1;
            this.lblDetainID.Text = "???";
            // 
            // label10
            // 
            this.label10.Image = global::DVLD_Project.Properties.Resources.Number_32;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(70, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Detain ID :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Maroon;
            this.lblTitle.Location = new System.Drawing.Point(301, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(344, 32);
            this.lblTitle.TabIndex = 42;
            this.lblTitle.Text = "Release Detain License ";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(646, 815);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 37);
            this.btnClose.TabIndex = 50;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(759, 815);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 37);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "Release";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Relase_DetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 866);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.filterLicense1);
            this.Controls.Add(this.linkLabelLicenseInfo);
            this.Controls.Add(this.linkLabelLicenseHistory);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTitle);
            this.Name = "Relase_DetainLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relase_DetainLicense";
            this.Load += new System.EventHandler(this.Relase_DetainLicense_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FilterLicense filterLicense1;
        private System.Windows.Forms.LinkLabel linkLabelLicenseInfo;
        private System.Windows.Forms.LinkLabel linkLabelLicenseHistory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblReLicenseID;
        private System.Windows.Forms.Label lblCreatedID;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAppID;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label lblAppFees;
        private System.Windows.Forms.Label lblFineFees;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}