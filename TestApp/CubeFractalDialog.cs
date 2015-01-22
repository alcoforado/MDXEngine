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
    public partial class CubeFractalDialog : Form
    {
        public float[,] Matrix;
        public CubeFractalDialog()
        {
            InitializeComponent();
            string[] colnames = new string[2];
            for (int c = 0; c < 2; c++)
            {
                colnames[c] = string.Format("C:{0}", c);
            }
           
            this.comboBoxN.SelectedIndex = 1;
            this.comboBoxM.SelectedIndex = 1;
            Matrix = new float[,] { { 0.5f, 1f }, { 1f, 0.2f } };
            this.matrixGridView.DataSource = new Array2DDataView(Matrix, colnames);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CubeFractalDialog_Load(object sender, EventArgs e)
        {

        }

       

        private void comboBoxN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Matrix == null)
                return;
            int m=Convert.ToInt32(comboBoxM.SelectedItem);
            int n=Convert.ToInt32(comboBoxN.SelectedItem);

            float[,] aux = new float [m,n];

            for (int i=0;i<m;i++)
                for (int j=0;j<n;j++)
                {
                    if (i<Matrix.GetLength(0) && j<Matrix.GetLength(1))
                        aux[i,j]=Matrix[i,j];
                }
            Matrix=aux;

            string[] colnames=new string[n];
            for (int c = 0; c < n; c++)
            {
                colnames[c] = string.Format("C:{0}", c+1);
            }
            this.matrixGridView.DataSource = new Array2DDataView(Matrix, colnames);
        }

        private void MatrixGridView_Error(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(String.Format("Position at {0} {1} is not a valid number",e.RowIndex,e.ColumnIndex),"Parsing Error");
                        
        }
    }
}
