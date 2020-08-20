using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.Util;

namespace TextRecognizer_EMGU_
{
    public partial class Form1 : Form
    {
        private string path = string.Empty;
        private string language = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;

                pictureBox1.Image = Image.FromFile(path);
            }
            else
            {
                MessageBox.Show("You need to choose a picture!");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path))
                {
                    throw new Exception("Picture not selected");
                }
                else if (toolStripComboBox1.SelectedItem == null)
                {
                    throw new Exception("Language not selected");
                }
                else
                {
                    Tesseract tesseract = new Tesseract(@"D:\(4)Codding\C#Projects\TextRecognizer\TextRecognizer(EMGU)\TrainedData", language, OcrEngineMode.TesseractLstmCombined);

                    tesseract.SetImage(new Image<Bgr, byte>(path));

                    tesseract.Recognize();

                    richTextBox1.Text = tesseract.GetUTF8Text();

                    tesseract.Dispose();

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                case 0:
                    language = "rus";
                    break;
                case 1:
                    language = "eng";
                    break;
                case 2:
                    language = "deu";
                    break;
            }
        }
    }
}
