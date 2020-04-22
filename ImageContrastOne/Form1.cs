using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageContrastOne
{
    public partial class Form1 : Form
    {

        Bitmap bitmapNow;
        Image fileOne;
        Boolean opened = false;
        float contrast = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult diaR = openFileDialog1.ShowDialog();

            if(diaR == DialogResult.OK)
            {
                fileOne = Image.FromFile(openFileDialog1.FileName);
                bitmapNow = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = fileOne;
                opened = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult diaR = saveFileDialog1.ShowDialog();

            if(diaR == DialogResult.OK)
            {
                if(opened)
                {
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "bmp") {
                        fileOne.Save(saveFileDialog1.FileName, ImageFormat. Bmp);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {
                        fileOne.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "png")
                    {
                        fileOne.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "gif")
                    {
                        fileOne.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                    }
                }
                else
                {
                    MessageBox.Show("Please Open an image first!!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int i = 0;i < bitmapNow.Width; i++)
            {
                for(int j=0; j< bitmapNow.Height;j++)
                {
                    Color freshColor = bitmapNow.GetPixel(i,j);

                    int grayScale = (int)((freshColor.R * .3) + (freshColor.G * .59) + (freshColor.B * .11));

                    Color modifiedColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    bitmapNow.SetPixel(i,j,modifiedColor);
                }
            }

            pictureBox1.Image = bitmapNow;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();

            contrast = 0.04f * trackBar1.Value;

            Bitmap bmp = new Bitmap(bitmapNow.Width, bitmapNow.Height);

            Graphics gr = Graphics.FromImage(bmp);
            ImageAttributes iatt = new ImageAttributes();

            ColorMatrix cmx = new ColorMatrix(new float[][] { new float[] { contrast,0f,0f,0f,0f},
                new float[] { 0f,contrast,0f,0f,0f},
                new float[] { 0f,0f,contrast,0f,0f},
                new float[] { 0f,0f,0f,1f,0f},

                new float[] { 0.001f,0.001f,0.001f,0f,1f}
            });

            iatt.SetColorMatrix(cmx);

            gr.DrawImage(bitmapNow,new Rectangle(0,0,bitmapNow.Width,bitmapNow.Height), 0, 0,bitmapNow.Width,bitmapNow.Height,GraphicsUnit.Pixel,iatt);
            gr.Dispose();
            iatt.Dispose();
            pictureBox1.Image = bmp;
        }
    }
}
