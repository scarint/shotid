using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Imaging.Filters;

namespace ImageAnalysis
{
    public partial class Form1 : Form
    {
        private Bitmap[] images = new Bitmap[15];
        private Bitmap baseImage;
        private int numImages = 0;
        private int activeImage = 0;
        private Point[] points = new Point[4];
        
        private string[] openText = { "Open the base image.",
                                      "Open the second image.",
                                      "Open the third image.",
                                      "Open the fourth image.",
                                      "Open the fifth image." };


        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            ClearPoints();

            if (numImages > 14)
            {
                MessageBox.Show("Too many images open.");
                return;
            }

            if (numImages > openText.Length)
                openFileDialog.Title = "Open an image.";
            else
                openFileDialog.Title = openText[numImages];

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                try
                {
                    images[numImages] = (Bitmap)Image.FromFile(fileName);

                    canvas.SizeMode = PictureBoxSizeMode.Zoom;

                    canvas.Image = images[numImages];
                    DisplayThumb();

                    numImages++;
                    
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void CompareButton_Click(object sender, EventArgs e)
        {

            Bitmap original = baseImage.Clone() as Bitmap;
            Bitmap next = canvas.Image as Bitmap;
            Crop crop;
            Bitmap newImage;

            FiltersSequence seq = new FiltersSequence();
            seq.Add(Grayscale.CommonAlgorithms.BT709);
            seq.Add(new OtsuThreshold());
            original = seq.Apply(baseImage);
            next = seq.Apply(canvas.Image as Bitmap);

            Accord.Imaging.ExhaustiveTemplateMatching templateMatching = new Accord.Imaging.ExhaustiveTemplateMatching(0.2f);
            
            Accord.Imaging.TemplateMatch[] results = templateMatching.ProcessImage(next, original);

            if (results[0] != null)
            {
                crop = new Crop(results[0].Rectangle);
                newImage = crop.Apply((Bitmap)canvas.Image);
            }
            else
                newImage = original;

            

            canvas.Image = newImage;

            images[activeImage] = newImage;
            thumbnail[activeImage].Image = newImage;
            thumbnail[activeImage].Invalidate();

            //ClearPoints();

            //Grayscale gray = new Grayscale(0.2125, 0.7154, 0.0721);
            //image1 = gray.Apply(image1);
            //image2 = gray.Apply(image2);

            //Difference differenceFilter = new Difference();
            //IFilter thresholdFilter = new Threshold(15);
            //differenceFilter.OverlayImage = image1;
            //image2 = differenceFilter.Apply(image2);
            //image2 = thresholdFilter.Apply(image2);

            //IFilter erosionFilter = new Erosion();
            //image2 = erosionFilter.Apply(image2);

            //canvas3.SizeMode = PictureBoxSizeMode.Zoom;
            //canvas3.Image = image2;
            //canvas.Visible = thumbnail.Visible = false;
            //canvas3.Visible = true;
            //CompareButton.Visible = false;

            return;
        }

        private void GetCoords(int x, int y)
        {
            if (points[0].X == 0)
            {
                points[0].X = x;
                points[0].Y = y;
                Write("1", points[0]);

            } else if (points[1].X == 0)
            {
                points[1].X = x;
                points[1].Y = y;
                Write("2", points[1]);
            } else if (points[2].X == 0)
            {
                points[2].X = x;
                points[2].Y = y;
                Write("3", points[2]);
            } else if (points[3].X == 0)
            {
                points[3].X = x;
                points[3].Y = y;
                cropButton.Visible = true;
                Write("4", points[3]);
            }
        }

        private void Canvas_Click(object sender, MouseEventArgs e)
        {
            GetCoords(e.X, e.Y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CropButton_Click(object sender, EventArgs e)
        {
            cropButton.Visible = false;
            int x, y, right, down;

            x = MapCoords(MinX(points), canvas.Width, canvas.Image.Width);
            y = MapCoords(MinY(points), canvas.Height, canvas.Image.Height);
            right = MapCoords(MaxX(points), canvas.Width, canvas.Image.Width) - x;
            down = MapCoords(MaxY(points), canvas.Height, canvas.Image.Height) - y;

            ClearPoints();

            Rectangle rect = new Rectangle(x, y, right, down);

            //MessageBox.Show("Rectangle: " + x + ", " + y + ", " + right + ", " + down + ".");

            Bitmap original = (Bitmap)canvas.Image;

            Crop target = new Crop(rect);

            Bitmap newImage = target.Apply(original);

            canvas.Image = newImage;

            images[activeImage] = newImage;
            thumbnail[activeImage].Image = newImage;
            thumbnail[activeImage].Invalidate();

        }
        private void Thumbnail_DoubleClick(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;

            activeImage = Convert.ToInt32(box.AccessibleName);

            canvas.Image = box.Image;
        }

        private void Write(String text, Point point)
        {
            using (Graphics g = Graphics.FromHwnd(canvas.Handle))
            {
                using (Font myFont = new Font("Tahoma", 12))
                {
                    g.DrawString(text, myFont, Brushes.Red, point);
                }
            }
        }

        private int MinX(Point[] compare)
        {
            int minimum = compare[01].X;

            foreach (Point i in compare)
            {
                if (i.X < minimum)
                    minimum = i.X;
            }
            //MessageBox.Show("MinX = " + minimum);

            return minimum;
        }

        private int MaxX(Point[] compare)
        {
            int maximum = compare[01].X;

            foreach (Point i in compare)
            {
                if (i.X > maximum)
                    maximum = i.X;
            }

            //MessageBox.Show("MaxX = " + maximum);

            return maximum;
        }

        private int MinY(Point[] compare)
        {
            int minimum = compare[01].Y;

            foreach (Point i in compare)
            {
                if (i.Y < minimum)
                    minimum = i.Y;
            }

            //MessageBox.Show("MinY = " + minimum);

            return minimum;
        }

        private int MaxY(Point[] compare)
        {
            int maximum = compare[01].Y;

            foreach (Point i in compare)
            {
                if (i.Y > maximum)
                    maximum = i.Y;
            }

            //MessageBox.Show("MaxY = " + maximum);

            return maximum;
        }

        private Bitmap Scale(Bitmap image, PictureBox canvas)
        {

            float widthRatio = (float)canvas.Width / (float)image.Width,
                  heightRatio = (float)canvas.Height / (float)image.Height;

            float ratio = Math.Min(widthRatio, heightRatio);

            float finalWidth = (image.Width * ratio),
                  finalHeight = (image.Height * ratio);

            ResizeBicubic filter = new ResizeBicubic((int) finalWidth, (int)finalHeight);
            Bitmap newImage = filter.Apply(image);

            return newImage;

        }

        private int MapCoords(float coordinate, float canvasSize, float imageSize)
        {
            coordinate = (coordinate * imageSize) / canvasSize;

            return (int)coordinate;
        }

        private void ClearPoints()
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].X = points[i].Y = 0;
            }

            cropButton.Visible = false;
        }

        private void DisplayThumb()
        {
            thumbnail[numImages] = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(thumbnail[numImages])).BeginInit();
            thumbnail[numImages].Location = new System.Drawing.Point((15 + (215 * numImages)), 929);
            thumbnail[numImages].Name = "thumbnail{0}, numImages";
            thumbnail[numImages].Size = new System.Drawing.Size(200, 100);
            thumbnail[numImages].TabIndex = 0;
            thumbnail[numImages].TabStop = false;
            thumbnail[numImages].SizeMode = PictureBoxSizeMode.Zoom;
            thumbnail[numImages].AccessibleName = numImages.ToString();
            thumbnail[numImages].DoubleClick += new System.EventHandler(Thumbnail_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(thumbnail[numImages])).EndInit();

            thumbnail[numImages].Image = images[numImages];
            thumbnail[numImages].Visible = true;
            Controls.Add(thumbnail[numImages]);
        }

        private void SetBaseButton_Click(object sender, EventArgs e)
        {
            baseImage = (Bitmap)canvas.Image;

            CompareButton.Visible = true;

        }
    }
}