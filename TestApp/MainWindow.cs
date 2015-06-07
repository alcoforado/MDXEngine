using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MDXEngine;
namespace TestApp
{

    



    public partial class MainWindow : Form
    {
        TextBoxStreamWriter _boxWriter;
        private IApp _currentApp;
        
        internal DxApp _dxApp;
        private DxControl _dx;

        public MainWindow()
        {
            InitializeComponent();
            _boxWriter = new TextBoxStreamWriter(textBox1);
            
            splitContainer1.Panel1.MouseLeave += (sender, e) => this.Focus();
            splitContainer1.Panel1.MouseEnter += (sender, e) => splitContainer1.Panel1.Focus();
            this.KeyPreview=true;
            _currentApp = null;
        }


        public void SetDxApp(DxApp app) { _dxApp = app; _dx = _dxApp.DxControl; }
        

        private void RemoveCurrentApp()
        {
            if (_currentApp != null)
                _currentApp.Dispose();
            _currentApp = null;
            _dx.Reset();
            _dx.Display();
        }

        private void SetCurrentApp(IApp app)
        {
            if (_currentApp != null)
                throw new Exception("Before Assign a new IApp make sure you remove the old one by calling MainWindow.RemoveCurrentApp");
            _currentApp = app;
        }

       

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        public TextWriter getTextBoxWriter() { return _boxWriter; }

        public Control RenderControl() { return splitContainer1.Panel1; }
        /*
        public void setDXApp(DXApp dxApp)
        {
            _dxApp=dxApp;
            //Make the link of dxApp with the events of the GUI.
            this.viewWireframeMenuItem.Click += (sender, e) =>
            {
                if (this.viewWireframeMenuItem.Text == "Wireframe Off")
                {
                    _dxApp.setWireframe(true);
                    this.viewWireframeMenuItem.Text = "Wireframe On";
                }
                else
                {
                    _dxApp.setWireframe(false);
                    this.viewWireframeMenuItem.Text = "Wireframe Off";
                }

            };



        }
        */

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pointsOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (this.pointsOffToolStripMenuItem.Text == "Points Off")
            {
                //_dxApp.setPointsOnly(true);
                this.pointsOffToolStripMenuItem.Text = "Points On";
            }
            else
            {
                //_dxApp.setPointsOnly(false);
                this.pointsOffToolStripMenuItem.Text = "Points Off";
            }
             */ 
        }

        private void fractalCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveCurrentApp();
            var app = new TestApp.Actions.CubeFractal.CubeFractal(_dx);
            this.SetCurrentApp(app);
        }

        private void rGBTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveCurrentApp();
            var app = new TestApp.Actions.ColorTriangle(_dx);
            this.SetCurrentApp(app);
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void loadTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveCurrentApp();
            var app = new TestApp.Actions.TextureDisplay(_dx);
            this.SetCurrentApp(app);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void helloWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveCurrentApp();
            var app = new TestApp.Actions.HelloWorld(_dx);
            this.SetCurrentApp(app);
        }

      
        private void cubeColor3DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveCurrentApp();
            var app = new TestApp.Actions.ColorTriangle3D(_dx);
            this.SetCurrentApp(app);
        }

      
       

      
    }

    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString()); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }

}
