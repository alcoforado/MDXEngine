namespace TestApp
{
    partial class CubeFractalDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CubeFractalDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxN = new System.Windows.Forms.ComboBox();
            this.comboBoxM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.matrixGridView = new System.Windows.Forms.DataGridView();
            this.cancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrixGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Read Me";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxN);
            this.groupBox2.Controls.Add(this.comboBoxM);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(0, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 41);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Matrix Dimensions";
            // 
            // comboBoxN
            // 
            this.comboBoxN.FormattingEnabled = true;
            this.comboBoxN.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxN.Location = new System.Drawing.Point(174, 14);
            this.comboBoxN.Name = "comboBoxN";
            this.comboBoxN.Size = new System.Drawing.Size(67, 21);
            this.comboBoxN.TabIndex = 6;
            this.comboBoxN.SelectedIndexChanged += new System.EventHandler(this.comboBoxN_SelectedIndexChanged);
            // 
            // comboBoxM
            // 
            this.comboBoxM.FormattingEnabled = true;
            this.comboBoxM.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxM.Location = new System.Drawing.Point(52, 14);
            this.comboBoxM.Name = "comboBoxM";
            this.comboBoxM.Size = new System.Drawing.Size(67, 21);
            this.comboBoxM.TabIndex = 4;
            this.comboBoxM.SelectedIndexChanged += new System.EventHandler(this.comboBoxN_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "N:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "M:";
            // 
            // matrixGridView
            // 
            this.matrixGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrixGridView.Location = new System.Drawing.Point(0, 162);
            this.matrixGridView.Name = "matrixGridView";
            this.matrixGridView.Size = new System.Drawing.Size(463, 230);
            this.matrixGridView.TabIndex = 3;
            this.matrixGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.MatrixGridView_Error);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(108, 8);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(87, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOk.FlatAppearance.BorderSize = 5;
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonOk.Location = new System.Drawing.Point(12, 8);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonOk);
            this.panel1.Controls.Add(this.cancel);
            this.panel1.Location = new System.Drawing.Point(238, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 34);
            this.panel1.TabIndex = 6;
            // 
            // CubeFractalDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 433);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.matrixGridView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CubeFractalDialog";
            this.Tag = "hhjj";
            this.Text = "CubeFractalDialog";
            this.Load += new System.EventHandler(this.CubeFractalDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrixGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxM;
        private System.Windows.Forms.ComboBox comboBoxN;
        private System.Windows.Forms.DataGridView matrixGridView;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Panel panel1;
    }
}