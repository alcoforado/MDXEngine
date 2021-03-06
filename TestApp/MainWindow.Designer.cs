﻿namespace TestApp
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fractalCubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBTriangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helloWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cubeColor3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireCube3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binPackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lightColor3DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(481, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fractalCubeToolStripMenuItem,
            this.rGBTriangleToolStripMenuItem,
            this.loadTextureToolStripMenuItem,
            this.helloWorldToolStripMenuItem,
            this.cubeColor3DToolStripMenuItem,
            this.wireCube3DToolStripMenuItem,
            this.binPackToolStripMenuItem,
            this.lightColor3DToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.testsToolStripMenuItem.Text = "Tests";
            // 
            // fractalCubeToolStripMenuItem
            // 
            this.fractalCubeToolStripMenuItem.Name = "fractalCubeToolStripMenuItem";
            this.fractalCubeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fractalCubeToolStripMenuItem.Text = "Fractal Cube";
            // 
            // rGBTriangleToolStripMenuItem
            // 
            this.rGBTriangleToolStripMenuItem.Name = "rGBTriangleToolStripMenuItem";
            this.rGBTriangleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rGBTriangleToolStripMenuItem.Text = "Color Triangle";
            this.rGBTriangleToolStripMenuItem.Click += new System.EventHandler(this.rGBTriangleToolStripMenuItem_Click_1);
            // 
            // loadTextureToolStripMenuItem
            // 
            this.loadTextureToolStripMenuItem.Name = "loadTextureToolStripMenuItem";
            this.loadTextureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadTextureToolStripMenuItem.Text = "LoadTexture";
            // 
            // helloWorldToolStripMenuItem
            // 
            this.helloWorldToolStripMenuItem.Name = "helloWorldToolStripMenuItem";
            this.helloWorldToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helloWorldToolStripMenuItem.Text = "Hello World";
            // 
            // cubeColor3DToolStripMenuItem
            // 
            this.cubeColor3DToolStripMenuItem.Name = "cubeColor3DToolStripMenuItem";
            this.cubeColor3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cubeColor3DToolStripMenuItem.Text = "Cube Color 3D";
            // 
            // wireCube3DToolStripMenuItem
            // 
            this.wireCube3DToolStripMenuItem.Name = "wireCube3DToolStripMenuItem";
            this.wireCube3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wireCube3DToolStripMenuItem.Text = "Wire Cube 3D";
            // 
            // binPackToolStripMenuItem
            // 
            this.binPackToolStripMenuItem.Name = "binPackToolStripMenuItem";
            this.binPackToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.binPackToolStripMenuItem.Text = "Bin Pack";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(481, 367);
            this.splitContainer1.SplitterDistance = 335;
            this.splitContainer1.TabIndex = 0;
            // 
            // lightColor3DToolStripMenuItem
            // 
            this.lightColor3DToolStripMenuItem.Name = "lightColor3DToolStripMenuItem";
            this.lightColor3DToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lightColor3DToolStripMenuItem.Text = "Light Color 3D";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 391);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fractalCubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBTriangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helloWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cubeColor3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireCube3DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binPackToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem lightColor3DToolStripMenuItem;

    }
}