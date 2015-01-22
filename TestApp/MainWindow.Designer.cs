namespace TestApp
{
    partial class MainWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cubeFractalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripView = new System.Windows.Forms.ToolStripMenuItem();
            this.viewWireframeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointsOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(496, 261);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appsToolStripMenuItem,
            this.toolStripView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appsToolStripMenuItem
            // 
            this.appsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cubeFractalToolStripMenuItem});
            this.appsToolStripMenuItem.Name = "appsToolStripMenuItem";
            this.appsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.appsToolStripMenuItem.Text = "Apps";
            // 
            // cubeFractalToolStripMenuItem
            // 
            this.cubeFractalToolStripMenuItem.Name = "cubeFractalToolStripMenuItem";
            this.cubeFractalToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.cubeFractalToolStripMenuItem.Text = "Cube Fractal";
            this.cubeFractalToolStripMenuItem.Click += new System.EventHandler(this.cubeFractalToolStripMenuItem_Click);
            // 
            // toolStripView
            // 
            this.toolStripView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewWireframeMenuItem,
            this.pointsOffToolStripMenuItem});
            this.toolStripView.Name = "toolStripView";
            this.toolStripView.Size = new System.Drawing.Size(44, 20);
            this.toolStripView.Text = "View";
            // 
            // viewWireframeMenuItem
            // 
            this.viewWireframeMenuItem.Name = "viewWireframeMenuItem";
            this.viewWireframeMenuItem.Size = new System.Drawing.Size(149, 22);
            this.viewWireframeMenuItem.Text = "Wireframe Off";
            // 
            // pointsOffToolStripMenuItem
            // 
            this.pointsOffToolStripMenuItem.Name = "pointsOffToolStripMenuItem";
            this.pointsOffToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.pointsOffToolStripMenuItem.Text = "Points Off";
            this.pointsOffToolStripMenuItem.Click += new System.EventHandler(this.pointsOffToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 66);
            this.textBox1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 261);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripView;
        private System.Windows.Forms.ToolStripMenuItem viewWireframeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointsOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cubeFractalToolStripMenuItem;

    }
}