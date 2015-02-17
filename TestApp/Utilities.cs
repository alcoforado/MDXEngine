using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MDXEngine.Textures;

namespace TestApp
{
    public class Utilities
    {

        static public string TextureSelect()
        {
            var file = new FileInfo(GetSetting("TextureFile"));
            string startDir;
            var dir = file.Directory;
            if (dir.Exists)
                startDir = dir.FullName;
            else
                startDir = ".\\";

            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = startDir;
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SetSetting("TextureFile", dialog.FileName);
                return dialog.FileName;
            }
            else
                return "";
        }

        static public string GetSetting(string key)
        {
            var file = Properties.Settings.Default[key];
            if (file == null)
                return "";
            else if (file is String)
                return file.ToString();
            else
                throw new Exception(String.Format("Key Settings {0} is not a string", key));
        }
        static public void SetSetting(string key, string value)
        {
            Properties.Settings.Default[key] = value;
        }



    }
}
