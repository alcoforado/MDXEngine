using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class SecWindow : Form
    {
        MWebBrowser webBrowser;
        public SecWindow(MWebBrowser browser)
        {
            this.webBrowser = browser;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Size = new Size(1, 1);
            this.webBrowser.ClientSize = new Size(1, 1);
            this.webBrowser.AutoSize = true;
            this.webBrowser.MinimumSize = new Size(0, 0);
            this.webBrowser.Width = 10;
            this.webBrowser.Height = 10;
            this.webBrowser.ScrollBarsEnabled = false;
            //this.webBrowser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.StartPosition = FormStartPosition.Manual;
            
            this.Controls.Add(webBrowser);
            InitializeComponent();

            this.Location = Properties.Settings.Default.SecWindowPosition;
            this.Size = Properties.Settings.Default.SecWindowSize;
            
            
            

            this.HandleDestroyed+= SecWindow_HandleDestroyed;
          
        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        } 

        public MWebBrowser Browser { get { return webBrowser; } }
       
        public void PositionOnRight(Form parentWindow,int width)
        {
            
            StartPosition = FormStartPosition.Manual;
            Location = new Point(parentWindow.Location.X+parentWindow.ClientSize.Width,parentWindow.Location.Y);
            Size = new Size(width,parentWindow.Height);
            Owner=parentWindow;
            Show();
        }

        private void SecWindow_HandleDestroyed(object sender, EventArgs e)
        {
         
            Properties.Settings.Default.SecWindowPosition = this.Location;
            Properties.Settings.Default.SecWindowSize = this.Size;
            Properties.Settings.Default.Save();
            this.webBrowser.Dispose();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
