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
using TestApp.Services;
namespace TestApp
{

    



    public partial class MainWindow : Form, TestApp.IMainWindow, IAppStateProvider
    {
        //TextBoxStreamWriter _boxWriter;
        private IAppState _currentApp;
        private IFactory<IAppState> _menuActionFactory;

        internal DxApp _dxApp;
        private DxControl _dx;
       
        
        public MainWindow(IFactory<IAppState> menuActionFactory)
        {
            InitializeComponent();
           // _boxWriter = new TextBoxStreamWriter(textBox1);
            
            splitContainer1.Panel1.MouseLeave += (sender, e) => this.Focus();
            splitContainer1.Panel1.MouseEnter += (sender, e) => splitContainer1.Panel1.Focus();
            splitContainer1.Panel2Collapsed = true;
            //_browserRight = new MWebBrowser();
            //splitContainer1.Panel2.Controls.Add(_browserRight);
            //splitContainer1.Panel2.Dock = DockStyle.Fill;
                
            this.KeyPreview=true;
            _currentApp = null;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Properties.Settings.Default.FormPosition;
            this.Size =     Properties.Settings.Default.FormSize;

            _menuActionFactory = menuActionFactory;
            this.SetMenuActions();

        }
        public T GetAppState<T> () where T:class,IAppState
        {
            var state = _currentApp as T;
            if (state != null)
                return state;
            else
               throw new Exception(String.Format("Application State {0} is not the current App State",typeof(T).Name));
        
        }

        public MenuStrip GetMenus()
        {
            return this.menuStrip1;
        }

        public void SetDxApp(DxApp app) { _dxApp = app; _dx = _dxApp.DxControl; }
        

        internal void RemoveCurrentApp()
        {
            if (_currentApp != null)
                _currentApp.Dispose();
            _currentApp = null;
            _dx.Reset();
            _dx.Display();
        }

         internal void SetCurrentApp(IAppState app)
        {
            if (_currentApp != null)
                throw new Exception("Before Assign a new IApp make sure you remove the old one by calling MainWindow.RemoveCurrentApp");
            _currentApp = app;
        }

       

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        //public TextWriter getTextBoxWriter() { return _boxWriter; }

        public Control RenderControl() { return splitContainer1.Panel1; }
       

        private void OnMainWindowMenuItemClick(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            this.RemoveCurrentApp();
            var action = this._menuActionFactory.Resolve(String.Join("",menu.Text.Split(' ')));
            this.SetCurrentApp(action);
        }


        private void SetMenuActions()
        {

            Action<ToolStripMenuItem> proc = null;
            proc = (ToolStripMenuItem menuItem) =>
            {
                if (menuItem == null)
                    return;
                if (menuItem.DropDownItems.Count == 0)
                {
                    menuItem.Click += OnMainWindowMenuItemClick;
                }
                else
                {
                    foreach (var child in menuItem.DropDownItems)
                    {

                        proc(child as ToolStripMenuItem);
                    }
                }
            };
            foreach (var menu in this.GetMenus().Items)
            {
                proc((ToolStripMenuItem)menu);
            }

        }



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
             Properties.Settings.Default.FormPosition = this.Location;
             Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Save();
            SettingsService.PersistSettings();
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
            var app = new TestApp.Actions.CubeColor3D(_dx);
            this.SetCurrentApp(app);
        }

      
        private void wireCube3DToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.RemoveCurrentApp();
            var app = new TestApp.Actions.WireCube3D(_dx);
            this.SetCurrentApp(app);
        }

        private void rGBTriangleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

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
