using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Point tempPoint;
        Pen myPen;
        SolidBrush mySolidBrush;
        public Form1()
        {
            InitializeComponent();
            saveFileDialog.Filter = openFileDialog.Filter = "BMP|*.bmp|PNG|*.png|JPG|*.jpg";
            myPen = new Pen(buttonColor.BackColor, (float)numericUpDownWidth.Value);
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            mySolidBrush = new SolidBrush(buttonFillColor.BackColor);

            nowyPlikToolStripMenuItem_Click(null, null);

        }

        private void nowyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxEdit.Image = new Bitmap(pictureBoxEdit.Width, pictureBoxEdit.Height);
            graphics = Graphics.FromImage(pictureBoxEdit.Image);
            graphics.Clear(Color.White);
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {   
                pictureBoxEdit.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBoxEdit.Image);
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);

                ImageFormat imageFormat = ImageFormat.Bmp;

                switch(extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }
                pictureBoxEdit.Image.Save(saveFileDialog.FileName, imageFormat);
            }
        }

        private void pictureBoxEdit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tempPoint = e.Location;
            }
        }

        private void pictureBoxEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                    pictureBoxEdit.Refresh();
                    tempPoint = e.Location;
                }
            }
        }

        private void pictureBoxEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if (radioButtonLine.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }

                else if (radioButtonOff.Checked)
                {
                    if (radioButtonRectangle.Checked)
                    {
                        graphics.DrawRectangle(myPen,
                                            Math.Min(tempPoint.X, e.X),
                                            Math.Min(tempPoint.Y, e.Y),
                                            Math.Abs(tempPoint.X - e.X),
                                            Math.Abs(tempPoint.Y - e.Y)
                                            );
                    }
                    else if (radioButtonEllipse.Checked)
                    {
                        graphics.DrawEllipse(myPen,
                                            Math.Min(tempPoint.X, e.X),
                                            Math.Min(tempPoint.Y, e.Y),
                                            Math.Abs(tempPoint.X - e.X),
                                            Math.Abs(tempPoint.Y - e.Y)
                                            );
                    }
                }
                else
                {
                    if (radioButtonRectangle.Checked)
                    {
                        graphics.FillRectangle(mySolidBrush,
                                            Math.Min(tempPoint.X, e.X),
                                            Math.Min(tempPoint.Y, e.Y),
                                            Math.Abs(tempPoint.X - e.X),
                                            Math.Abs(tempPoint.Y - e.Y)
                                            );
                    }
                    else if (radioButtonEllipse.Checked)
                    {
                        graphics.FillEllipse(mySolidBrush,
                                            Math.Min(tempPoint.X, e.X),
                                            Math.Min(tempPoint.Y, e.Y),
                                            Math.Abs(tempPoint.X - e.X),
                                            Math.Abs(tempPoint.Y - e.Y)
                                            );
                    }
                }
                pictureBoxEdit.Refresh();
            }
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = (float)numericUpDownWidth.Value;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog.Color;
                myPen.Color = colorDialog.Color;
            }
        }
        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonFillColor.BackColor = colorDialog.Color;
                mySolidBrush.Color = colorDialog.Color;
            }
        }
    }
}
