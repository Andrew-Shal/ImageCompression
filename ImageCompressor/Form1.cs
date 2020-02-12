using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageCompressor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                try{
                    string tifPath = openFileDialog1.FileName; //get tif file path
                    string fileName = Path.GetFileNameWithoutExtension(tifPath); // get file name isolated
                    string fileNameWithExt = Path.GetFileName(tifPath); // get file name with extension isolated
                    string renamedFile = fileName + "_compressed"; // rename filename

                    string updatedPath = tifPath.Replace(fileNameWithExt, renamedFile); // update the path for saving with new file name
                    string jpgPath = Path.ChangeExtension(updatedPath, ".jpg"); // update imae file format in path to jpg
                    FileStream stream = new FileStream(tifPath, FileMode.Open,FileAccess.Read); // create stream so that we could close when finished process with image
                    Image tifImg = Image.FromStream(stream); // open image from stream
                    tifImg.Save(jpgPath, ImageFormat.Jpeg); // save a compressed version of the tif image in jpg format
                    stream.Close(); // close the stream so that we could release the file

                    // image successfully copied, remove tif image from origin location
                    File.Delete(tifPath);

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Format (*.tif)|*.tif;";
        }
    }
}
