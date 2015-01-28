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
        CubeFractalDialog _fracDiag;
        
        internal DxApp _dxApp;
        public MainWindow()
        {
            InitializeComponent();
            _boxWriter = new TextBoxStreamWriter(textBox1);
            
            splitContainer1.Panel1.MouseLeave += (sender, e) => this.Focus();
            splitContainer1.Panel1.MouseEnter += (sender, e) => splitContainer1.Panel1.Focus();
            this.KeyPreview=true;

        }


        public void SetDxApp(DxApp app) { _dxApp = app; }
        
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

        private void cubeFractalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_fracDiag == null)
                _fracDiag = new CubeFractalDialog();
            //if (_fracDiag.ShowDialog(this) == DialogResult.OK)
                
                //_dxApp.SetCubeFractal(_fracDiag.Matrix);            

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
