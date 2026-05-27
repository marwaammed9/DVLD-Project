namespace DVLD_Project
{
    partial class ManageLocalLinceseApps
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.CBFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AppDetailesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CnacleAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechdualVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechdualWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sechdualStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IssuseLicenseStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicensesHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.Maroon;
            this.lblRecords.Location = new System.Drawing.Point(154, 696);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(32, 23);
            this.lblRecords.TabIndex = 18;
            this.lblRecords.Text = "??";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(42, 696);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 23);
            this.label3.TabIndex = 17;
            this.label3.Text = "#Records :";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(305, 209);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(158, 24);
            this.txtFilter.TabIndex = 16;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // CBFilter
            // 
            this.CBFilter.FormattingEnabled = true;
            this.CBFilter.Items.AddRange(new object[] {
            "Full Name",
            "L.D.L.AppID",
            "National No.",
            "None",
            "Status"});
            this.CBFilter.Location = new System.Drawing.Point(140, 209);
            this.CBFilter.Name = "CBFilter";
            this.CBFilter.Size = new System.Drawing.Size(158, 24);
            this.CBFilter.Sorted = true;
            this.CBFilter.TabIndex = 15;
            this.CBFilter.SelectedIndexChanged += new System.EventHandler(this.CBFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(33, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 23);
            this.label2.TabIndex = 14;
            this.label2.Text = "Filter By :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Firebrick;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.Location = new System.Drawing.Point(12, 239);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.Size = new System.Drawing.Size(1407, 439);
            this.dataGridView1.TabIndex = 13;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AppDetailesToolStripMenuItem,
            this.toolStripSeparator2,
            this.CnacleAppToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.TestsToolStripMenuItem,
            this.IssuseLicenseStripMenuItem,
            this.showLicenseToolStripMenuItem,
            this.showPersonLicensesHistoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(342, 304);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // AppDetailesToolStripMenuItem
            // 
            this.AppDetailesToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Applications_64;
            this.AppDetailesToolStripMenuItem.Name = "AppDetailesToolStripMenuItem";
            this.AppDetailesToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.AppDetailesToolStripMenuItem.Text = "Show Application Detailes";
            this.AppDetailesToolStripMenuItem.Click += new System.EventHandler(this.AppDetailesToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(338, 6);
            // 
            // CnacleAppToolStripMenuItem
            // 
            this.CnacleAppToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.folder;
            this.CnacleAppToolStripMenuItem.Name = "CnacleAppToolStripMenuItem";
            this.CnacleAppToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.CnacleAppToolStripMenuItem.Text = "Cnacel Applicatoin";
            this.CnacleAppToolStripMenuItem.Click += new System.EventHandler(this.CnacleAppToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.edit_32;
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.updateToolStripMenuItem.Text = "Edit";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Delete_32_2;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.deleteToolStripMenuItem.Text = "Delete Application";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(338, 6);
            // 
            // TestsToolStripMenuItem
            // 
            this.TestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sechdualVisionTestToolStripMenuItem,
            this.sechdualWrittenTestToolStripMenuItem,
            this.sechdualStreetTestToolStripMenuItem});
            this.TestsToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.exam_time;
            this.TestsToolStripMenuItem.Name = "TestsToolStripMenuItem";
            this.TestsToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.TestsToolStripMenuItem.Text = "sechdual Tests";
            // 
            // sechdualVisionTestToolStripMenuItem
            // 
            this.sechdualVisionTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.eye_scan;
            this.sechdualVisionTestToolStripMenuItem.Name = "sechdualVisionTestToolStripMenuItem";
            this.sechdualVisionTestToolStripMenuItem.Size = new System.Drawing.Size(269, 36);
            this.sechdualVisionTestToolStripMenuItem.Text = "Sechdual Vision Test";
            this.sechdualVisionTestToolStripMenuItem.Click += new System.EventHandler(this.sechdualVisionTestToolStripMenuItem_Click);
            // 
            // sechdualWrittenTestToolStripMenuItem
            // 
            this.sechdualWrittenTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.notes;
            this.sechdualWrittenTestToolStripMenuItem.Name = "sechdualWrittenTestToolStripMenuItem";
            this.sechdualWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(269, 36);
            this.sechdualWrittenTestToolStripMenuItem.Text = "Sechdual Written Test";
            this.sechdualWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.sechdualWrittenTestToolStripMenuItem_Click);
            // 
            // sechdualStreetTestToolStripMenuItem
            // 
            this.sechdualStreetTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.driving_test;
            this.sechdualStreetTestToolStripMenuItem.Name = "sechdualStreetTestToolStripMenuItem";
            this.sechdualStreetTestToolStripMenuItem.Size = new System.Drawing.Size(269, 36);
            this.sechdualStreetTestToolStripMenuItem.Text = "Sechdual Street Test";
            this.sechdualStreetTestToolStripMenuItem.Click += new System.EventHandler(this.sechdualStreetTestToolStripMenuItem_Click);
            // 
            // IssuseLicenseStripMenuItem
            // 
            this.IssuseLicenseStripMenuItem.Image = global::DVLD_Project.Properties.Resources.identification_card;
            this.IssuseLicenseStripMenuItem.Name = "IssuseLicenseStripMenuItem";
            this.IssuseLicenseStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.IssuseLicenseStripMenuItem.Text = "Issuse Driving License[First Time]";
            this.IssuseLicenseStripMenuItem.Click += new System.EventHandler(this.IssuseLicenseStripMenuItem_Click);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Driver_License_48;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.showLicenseToolStripMenuItem.Text = "Show License";
            this.showLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // showPersonLicensesHistoryToolStripMenuItem
            // 
            this.showPersonLicensesHistoryToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.file;
            this.showPersonLicensesHistoryToolStripMenuItem.Name = "showPersonLicensesHistoryToolStripMenuItem";
            this.showPersonLicensesHistoryToolStripMenuItem.Size = new System.Drawing.Size(341, 36);
            this.showPersonLicensesHistoryToolStripMenuItem.Text = "Show Person Licenses History";
            this.showPersonLicensesHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicensesHistoryToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(469, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(520, 34);
            this.label1.TabIndex = 12;
            this.label1.Text = "Local Driving License Applications";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1287, 686);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 37);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::DVLD_Project.Properties.Resources.New_Application_64;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Location = new System.Drawing.Point(1317, 173);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 60);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(631, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // ManageLocalLinceseApps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 749);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.CBFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ManageLocalLinceseApps";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManageLocalLinceseApps";
            this.Load += new System.EventHandler(this.ManageLocalLinceseApps_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox CBFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AppDetailesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CnacleAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IssuseLicenseStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechdualVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechdualWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sechdualStreetTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicensesHistoryToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
    }
}