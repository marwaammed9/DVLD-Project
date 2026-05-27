namespace DVLD_Project
{
    partial class Detain_License
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
            this.components = new System.ComponentModel.Container();
            this.filterLicense1 = new DVLD_Project.FilterLicense();
            this.linkLabelLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.linkLabelLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCreatedID = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbDetainID = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // filterLicense1
            // 
            this.filterLicense1.LicenseID = -1;
            this.filterLicense1.Location = new System.Drawing.Point(19, 49);
            this.filterLicense1.Name = "filterLicense1";
            this.filterLicense1.Size = new System.Drawing.Size(895, 492);
            this.filterLicense1.TabIndex = 41;
            // 
            // linkLabelLicenseInfo
            // 
            this.linkLabelLicenseInfo.AutoSize = true;
            this.linkLabelLicenseInfo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelLicenseInfo.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkLabelLicenseInfo.LinkColor = System.Drawing.Color.Navy;
            this.linkLabelLicenseInfo.Location = new System.Drawing.Point(248, 766);
            this.linkLabelLicenseInfo.Name = "linkLabelLicenseInfo";
            this.linkLabelLicenseInfo.Size = new System.Drawing.Size(126, 20);
            this.linkLabelLicenseInfo.TabIndex = 38;
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
            this.linkLabelLicenseHistory.Location = new System.Drawing.Point(57, 766);
            this.linkLabelLicenseHistory.Name = "linkLabelLicenseHistory";
            this.linkLabelLicenseHistory.Size = new System.Drawing.Size(185, 20);
            this.linkLabelLicenseHistory.TabIndex = 37;
            this.linkLabelLicenseHistory.TabStop = true;
            this.linkLabelLicenseHistory.Text = "Show License History";
            this.linkLabelLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLicenseHistory_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.lblLicenseID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblCreatedID);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblAppDate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lbDetainID);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 547);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(895, 204);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detain Info ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(144, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 27);
            this.textBox1.TabIndex = 20;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.ForeColor = System.Drawing.Color.Maroon;
            this.lblLicenseID.Location = new System.Drawing.Point(485, 65);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(39, 20);
            this.lblLicenseID.TabIndex = 19;
            this.lblLicenseID.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "License ID :";
            // 
            // lblCreatedID
            // 
            this.lblCreatedID.AutoSize = true;
            this.lblCreatedID.ForeColor = System.Drawing.Color.Maroon;
            this.lblCreatedID.Location = new System.Drawing.Point(488, 113);
            this.lblCreatedID.Name = "lblCreatedID";
            this.lblCreatedID.Size = new System.Drawing.Size(39, 20);
            this.lblCreatedID.TabIndex = 13;
            this.lblCreatedID.Text = "???";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(369, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 20);
            this.label14.TabIndex = 12;
            this.label14.Text = "Created By :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(42, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "Fine Fees :";
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.ForeColor = System.Drawing.Color.Maroon;
            this.lblAppDate.Location = new System.Drawing.Point(159, 113);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(39, 20);
            this.lblAppDate.TabIndex = 3;
            this.lblAppDate.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Detain Date :";
            // 
            // lbDetainID
            // 
            this.lbDetainID.AutoSize = true;
            this.lbDetainID.ForeColor = System.Drawing.Color.Maroon;
            this.lbDetainID.Location = new System.Drawing.Point(155, 65);
            this.lbDetainID.Name = "lbDetainID";
            this.lbDetainID.Size = new System.Drawing.Size(39, 20);
            this.lbDetainID.TabIndex = 1;
            this.lbDetainID.Text = "???";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Detain ID :";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Maroon;
            this.lblTitle.Location = new System.Drawing.Point(360, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 32);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "Detain License ";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(650, 762);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 37);
            this.btnClose.TabIndex = 48;
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
            this.btnSave.Location = new System.Drawing.Point(763, 762);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 37);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Detain";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Detain_License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 813);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.filterLicense1);
            this.Controls.Add(this.linkLabelLicenseInfo);
            this.Controls.Add(this.linkLabelLicenseHistory);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTitle);
            this.Name = "Detain_License";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detain_License";
            this.Load += new System.EventHandler(this.Detain_License_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FilterLicense filterLicense1;
        private System.Windows.Forms.LinkLabel linkLabelLicenseInfo;
        private System.Windows.Forms.LinkLabel linkLabelLicenseHistory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCreatedID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbDetainID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}