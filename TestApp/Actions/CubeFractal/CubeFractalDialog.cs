using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Awesomium.Core;
using Awesomium.Windows.Forms;

namespace TestApp.Actions.CubeFractal
{
    public partial class CubeFractalDialog : Form
    {
        private WebControl webControl;

        public CubeFractalDialog()
        {
            InitializeComponent();
            webControl = new WebControl();
            this.Controls.Add(webControl);
            webControl.Dock = System.Windows.Forms.DockStyle.Fill;
            webControl.Source = new Uri("http://www.google.com");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            webControl.Dispose();
            WebCore.Shutdown();
        }
    }
}
