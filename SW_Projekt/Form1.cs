using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

//CTRL + M + L = expand all
//CTRL + M + O = collapse all

namespace SW_Projekt
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> pB1_image;
        Image<Bgr, byte> pB2_image;

        Image<Bgr, byte> pB1_image_virtual;
        Image<Bgr, byte> pB2_image_virtual;

        double a;
        char symbol;
        double b;
        double result;

        enum LastButton
        {
            nothing,
            fromFile,
            fromCamera
        }

        LastButton lastButton = LastButton.nothing;
        VideoCapture camera;
        bool isCapturingVideo = false;

        public Form1()
        {
            InitializeComponent();
            pB1_image = new Image<Bgr, byte>(pB1.Size);
            pB2_image = new Image<Bgr, byte>(pB2.Size);

            try
            {
                camera = new VideoCapture(0);
                camera.ImageGrabbed += UpdateFrame;
            }
            catch (Exception)
            {
                MessageBox.Show("Lack of camera");
                throw;
            }
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            Mat temp = new Mat();
            camera.Retrieve(temp);
            CvInvoke.Resize(temp, temp, pB1_image.Size);
            pB1_image = temp.ToImage<Bgr, byte>();
            pB1_image_virtual = temp.ToImage<Bgr, byte>();
            pB1.Image = pB1_image.Bitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //-----------------------------------------------------------------------------------------------------------
        #region 1) Interface

        private void bFromCamera_Click(object sender, EventArgs e)
        {
            Mat temp = new Mat();
            isCapturingVideo = !isCapturingVideo;
            if (isCapturingVideo)
            {
                camera.Start();
            }
            else
            {
                camera.Stop();
                lastButton = LastButton.fromCamera;
            }
            //pB1_image = temp.ToImage<Bgr, byte>();
            //pB1.Image = pB1_image.Bitmap;

        }

        private void bFromFile_click(object sender, EventArgs e)
        {
            pB1.Image = get_image_bitmap_from_file(tBImagePathPB1.Text, ref pB1_image);
            lastButton = LastButton.fromFile;
        }

        private void bBrowseFilesPB1_click(object sender, EventArgs e)
        {
            tBImagePathPB1.Text = get_image_path();
        }

        private void bRight_Click(object sender, EventArgs e)
        {
            bool adjusted = false;

            if (lastButton == LastButton.fromFile)
            {
                for (int i = 99; i >= 1; i = i - 2)
                {
                    if (adjusted) break;
                    adjusted = true;
                    try
                    {
                        thresholdRGB(pB1_image, pB2_image, pB2, i);
                        removeBlackStrips(pB2_image, pB2_image, pB2);
                        CvInvoke.Rectangle(pB2_image, new Rectangle(new Point(0, 0),
                                    new Size(pB2_image.Width, pB2_image.Height)), new MCvScalar(255, 255, 255), 10);
                        erode(pB2_image, pB2_image, pB2);
                        calculate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Threshold = (" + i + "; 255)");
                        adjusted = false;
                    }
                }

                if (adjusted)
                {
                    pB2.Image = pB2_image.Bitmap;
                }
                else
                {
                    //clear_image(pB1, pB1_image);
                    clear_image(pB2, pB2_image);
                }
            }

            if (lastButton == LastButton.fromCamera)
            {
                for (int i = 0; i <= 99; i = i + 2)
                {
                    if (adjusted) break;
                    adjusted = true;
                    try
                    {
                        thresholdRGB(pB1_image, pB2_image, pB2, i);
                        removeBlackStrips(pB2_image, pB2_image, pB2);
                        CvInvoke.Rectangle(pB2_image, new Rectangle(new Point(0, 0),
                                    new Size(pB2_image.Width, pB2_image.Height)), new MCvScalar(255, 255, 255), 10);
                        erode(pB2_image, pB2_image, pB2);
                        calculate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Threshold = (" + i + "; 255)");
                        adjusted = false;
                    }
                }

                if (adjusted)
                {
                    pB2.Image = pB2_image.Bitmap;
                }
                else
                {
                    //clear_image(pB1, pB1_image);
                    clear_image(pB2, pB2_image);
                }
            }
        }

        private void bCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                calculate();
            }
            catch (Exception ex)
            {

            }
        }

        private void bCheck_Click(object sender, EventArgs e)
        {
            pB2_image_virtual = new Image<Bgr, byte>(pB2.Size);
            pB2_image_virtual.Data = pB2_image.Data;
            int xMin;
            int xMax = 0;
            int yMin;
            int yMax;
            int yMinGlobally = 0;
            int yMaxGlobally = pB2.Height - 1;
            String str = "";
            List<int[]> paramsOfTheElements = new List<int[]>();

            checkConstraintsGloballyY(out yMinGlobally, out yMaxGlobally);
            for (int x0 = 0; x0 < pB2.Width - 1; x0++)
            {
                x0 = xMax;
                if (checkConstraintsLocally(out xMin, out xMax, out yMin, out yMax,
                    yMinGlobally, yMaxGlobally, x0))
                {
                    paramsOfTheElements.Add(new int[] { xMin, xMax, yMin, yMax });
                    CvInvoke.Rectangle(pB2_image, new Rectangle(new Point(xMin, yMin),
                        new Size(xMax - xMin, yMax - yMin)), new MCvScalar(0, 0, 255));
                }
            }
            for (int i = 0; i < paramsOfTheElements.Count() - 1; i++)
            {
                xMin = paramsOfTheElements[i][0];
                xMax = paramsOfTheElements[i][1];
                yMin = paramsOfTheElements[i][2];
                yMax = paramsOfTheElements[i][3];
                str = str + checkElement(xMin, xMax, yMin, yMax);
            }
            str = str + " = ";
            tBResult.Text = str;
            pB2.Image = pB2_image.Bitmap;

            //clean up
            tBResult2_Decimal.Text = "";
            paramsOfTheElements.Clear();
            pB2_image.Data = pB2_image_virtual.Data;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            contrast(pB1_image_virtual, pB1_image, pB1, (int)numericUpDown1.Value);
            brightness(pB1_image, pB1_image, pB1, (int)numericUpDown2.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            brightness(pB1_image_virtual, pB1_image, pB1, (int)numericUpDown2.Value);
            contrast(pB1_image, pB1_image, pB1, (int)numericUpDown1.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            thresholdRGB(pB1_image, pB2_image, pB2, (int)numericUpDown3.Value);
            removeBlackStrips(pB2_image, pB2_image, pB2);
            CvInvoke.Rectangle(pB2_image, new Rectangle(new Point(0, 0),
                        new Size(pB2_image.Width, pB2_image.Height)), new MCvScalar(255, 255, 255), 10);
            pB2.Image = pB2_image.Bitmap;
        }

        private void bErode_Click(object sender, EventArgs e)
        {
            erode(pB2_image, pB2_image, pB2);
            pB2.Image = pB2_image.Bitmap;
        }

        private void bRotate_Click(object sender, EventArgs e)
        {
            rotate(pB2_image, pB2_image, pB2);
            erode(pB2_image, pB2_image, pB2);
        }


        #endregion
        //-----------------------------------------------------------------------------------------------------------
        #region 2) Picture Modification

        private void invert(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb)
        {
            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_dest.Data;



            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    temp2[y, x, 0] = (byte)(255 - temp2[y, x, 0]);
                    temp2[y, x, 1] = (byte)(255 - temp2[y, x, 1]);
                    temp2[y, x, 2] = (byte)(255 - temp2[y, x, 2]);
                }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }

        private void dilate(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb)
        {
            var strechedImage = strechImageFrame(image_source);
            byte[,,] temp1 = strechedImage.Data;
            //byte[, ,] temp1 = image_buffers[1].Data;
            byte[,,] temp2 = image_dest.Data;

            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    for (int chl = 0; chl < 3; chl++)
                    {
                        int currentMax = int.MinValue;
                        for (int y_p = -1; y_p <= 1; y_p++)
                        {
                            for (int x_p = -1; x_p <= 1; x_p++)
                            {
                                currentMax = Math.Max(currentMax, temp1[y + y_p + 1, x + x_p + 1, chl]);
                            }
                        }
                        temp2[y, x, chl] = (byte)currentMax;
                    }
                }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }

        private void erode(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb)
        {
            var strechedImage = strechImageFrame(image_source);
            byte[,,] temp1 = strechedImage.Data;
            byte[,,] temp2 = image_dest.Data;

            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    for (int chl = 0; chl < 3; chl++)
                    {
                        int currentMin = int.MaxValue;
                        for (int y_p = -1; y_p <= 1; y_p++)
                        {
                            for (int x_p = -1; x_p <= 1; x_p++)
                            {
                                currentMin = Math.Min(currentMin, temp1[y + y_p + 1, x + x_p + 1, chl]);
                            }
                        }
                        temp2[y, x, chl] = (byte)currentMin;
                    }
                }
            }
            image_dest.Data = temp2;
            //pb.Image = image_dest.Bitmap;
        }

        private void thresholdRGB(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb, int minVal)
        {
            int maxVal = 255;

            int[] consR = { minVal, maxVal };
            int[] consG = { minVal, maxVal };
            int[] consB = { minVal, maxVal };

            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_dest.Data;

            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    if (
                       (temp1[y, x, 2] >= consR[0] && temp1[y, x, 2] <= consR[1]) &&
                       (temp1[y, x, 1] >= consG[0] && temp1[y, x, 1] <= consG[1]) &&
                       (temp1[y, x, 0] >= consB[0] && temp1[y, x, 0] <= consB[1])
                       )
                    {
                        temp2[y, x, 0] = temp2[y, x, 1] = temp2[y, x, 2] = 255;
                    }
                    else
                    {
                        temp2[y, x, 0] = temp2[y, x, 1] = temp2[y, x, 2] = 0;
                    }
                }
            }
            image_dest.Data = temp2;
            //pb.Image = image_dest.Bitmap;
        }

        private void contrast(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb, int P1)
        {
            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_dest.Data;

            P1 = (int)clamp(P1, 0, 127);

            double tgAlfa = 255 / (255.0 - 2 * P1);

            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (temp1[y, x, i] < P1)
                            temp2[y, x, i] = 0;
                        else if (temp1[y, x, i] > (255 - P1))
                            temp2[y, x, i] = 255;
                        else
                        {
                            temp2[y, x, i] = (byte)(tgAlfa * (temp1[y, x, i] - P1));
                        }
                    }
                }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }

        private void brightness(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb, int P1)
        {
            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_dest.Data;

            for (int x = 0; x < image_source.Width; x++)
            {
                for (int y = 0; y < image_source.Height; y++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        temp2[y, x, i] = (byte)clamp(temp1[y, x, i] + P1, 0, 255);
                    }
                }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }

        private void rotate(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb)
        {
            double F, Sx, Sy, x0, y0;
            double Jx0, Jy0, Jx0y0, Jx, Jy, Jxy, Je_0, Jt_0;
            double alfa_e, alfa_t, alfa_e_deg, alfa_t_deg;
            F = Sx = Sy = Jx0 = Jy0 = Jx0y0 = Jx = Jy = Jxy = Je_0 = Jt_0 = alfa_e = alfa_t = alfa_e_deg = alfa_t_deg = x0 = y0 = 0;

            Image<Bgr, byte> image_white = new Image<Bgr, byte>(pB2.Size);
            image_white.SetValue(new Bgr(255, 255, 255));
            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_white.Data;

            #region Kat obrotu
            //Wyliczenie momentow 1 i 2 stopnia

            for (int x = 0; x < pB2.Width; x++)
            {
                for (int y = 0; y < pB2.Height; y++)
                {
                    if (temp1[y, x, 0] == 0x00 && temp1[y, x, 1] == 0x00 && temp1[y, x, 2] == 0x00)
                    {
                        F = F + 1;
                        Sx = Sx + y;
                        Sy = Sy + x;
                        Jx = Jx + Math.Pow(y, 2);
                        Jy = Jy + Math.Pow(x, 2);
                        Jxy = Jxy + x * y;
                    }
                }
            }
            //Obliczenie środka cieżkości
            if (F > 0)
            {
                x0 = Sy / F;
                y0 = Sx / F;
            }
            //Obliczenie momentów centralnych
            Jx0 = Jx - F * Math.Pow(y0, 2);
            Jy0 = Jy - F * Math.Pow(x0, 2);
            Jx0y0 = Jxy - F * x0 * y0;

            Je_0 = (Jx0 + Jy0) / 2 + Math.Sqrt(0.25 * Math.Pow(Jy0 - Jx0, 2) + Math.Pow(Jx0y0, 2));
            Jt_0 = (Jx0 + Jy0) / 2 - Math.Sqrt(0.25 * Math.Pow(Jy0 - Jx0, 2) + Math.Pow(Jx0y0, 2));

            if (Jy0 != Je_0)
                alfa_e = Math.Atan(Jx0y0 / (Jy0 - Je_0));
            else
                alfa_e = Math.PI / 2;

            if (Jy0 != Jt_0)
                alfa_t = Math.Atan(Jx0y0 / (Jy0 - Jt_0));
            else
                alfa_t = Math.PI / 2;

            alfa_e_deg = alfa_e * 180.0 / Math.PI;
            alfa_t_deg = alfa_t * 180.0 / Math.PI;

            Console.WriteLine("alfa_e = " + alfa_e_deg);
            Console.WriteLine("alfa_t = " + alfa_t_deg);

            #endregion

            int[,] yR = new int[pB2.Width, pB2.Height];
            int[,] xR = new int[pB2.Width, pB2.Height];

            double ang = -alfa_t;
            int xMin = 0;
            int xMax = pB1_image.Width - 1;
            int yMin = 0;
            int yMax = pB1_image.Height - 1;

            for (int x = 0; x < pB2.Width; x++)
            {
                for (int y = 0; y < pB2.Height; y++)
                {
                    xR[x, y] = Convert.ToInt32(x * Math.Cos(ang) - y * Math.Sin(ang));
                    yR[x, y] = Convert.ToInt32(x * Math.Sin(ang) + y * Math.Cos(ang));
                    if (temp1[y, x, 2] == 0 && temp1[y, x, 1] == 0 && temp1[y, x, 0] == 0)
                    {
                        if (xR[x, y] < xMin) xMin = xR[x, y];
                        if (xR[x, y] > xMax) xMax = xR[x, y];
                        if (yR[x, y] < yMin) yMin = yR[x, y];
                        if (yR[x, y] > yMax) yMax = yR[x, y];
                    }
                }
            }

            Console.WriteLine("xMin = " + xMin);
            Console.WriteLine("xMax = " + xMax);
            Console.WriteLine("yMin = " + yMin);
            Console.WriteLine("yMax = " + yMax);

            int xTr = 0;
            int yTr = 0;
            int xEps = 10;
            int yEps = 10; 
            if (xMin < 0) xTr = Math.Abs(xMin) + xEps;
            if (xMax > pB2.Width - 1) xTr = (pb.Width- 1) -xMax - xEps;
            if (yMin < 0) yTr = Math.Abs(yMin) + yEps;
            if (yMax > pB2.Height - 1) yTr = (pb.Height - 1) -yMax - yEps;

            Console.WriteLine("xTr = " + xTr);
            Console.WriteLine("yTr = " + yTr);




            for (int x = 0; x < pB2.Width; x++)
            {
                for (int y = 0; y < pB2.Height; y++)
                {
                    if (temp1[y, x, 2] == 0 && temp1[y, x, 1] == 0 && temp1[y, x, 0] == 0)
                        for (int i = 0; i < 3; i++)
                        {
                            temp2[yR[x,y] + yTr, xR[x,y] + xTr, i] = temp1[y, x, i];
                        }
                }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------
        #region  3) Recognition and Calculation

        private void calculate()
        {
            pB2_image_virtual = new Image<Bgr, byte>(pB2.Size);
            pB2_image_virtual.Data = pB2_image.Data;
            int xMin;
            int xMax = 0;
            int yMin;
            int yMax;
            int yMinGlobally = 0;
            int yMaxGlobally = pB2.Height - 1;
            String str = "";
            List<int[]> paramsOfTheElements = new List<int[]>();

            checkConstraintsGloballyY(out yMinGlobally, out yMaxGlobally);
            for (int x0 = 0; x0 < pB2.Width - 1; x0++)
            {
                x0 = xMax;
                if (checkConstraintsLocally(out xMin, out xMax, out yMin, out yMax,
                    yMinGlobally, yMaxGlobally, x0))
                {
                    paramsOfTheElements.Add(new int[] { xMin, xMax, yMin, yMax });
                    //CvInvoke.Rectangle(pB2_image, new Rectangle(new Point(xMin, yMin),
                    //new Size(xMax - xMin, yMax - yMin)), new MCvScalar(0, 0, 255));
                }
            }
            for (int i = 0; i < paramsOfTheElements.Count() - 1; i++)
            {
                xMin = paramsOfTheElements[i][0];
                xMax = paramsOfTheElements[i][1];
                yMin = paramsOfTheElements[i][2];
                yMax = paramsOfTheElements[i][3];
                str = str + checkElement(xMin, xMax, yMin, yMax);
            }
            parseString(str);
            giveResult();
            str = str + " = ";
            tBResult.Text = str;
            tBResult2_Decimal.Text = a + " " + symbol + " " + b + " = " + result;
            pB2.Image = pB2_image.Bitmap;

            //clean up
            paramsOfTheElements.Clear();
            pB2_image.Data = pB2_image_virtual.Data;
        }

        private void removeBlackStrips(Image<Bgr, byte> image_source, Image<Bgr, byte> image_dest, PictureBox pb)
        {

            byte[,,] temp1 = image_source.Data;
            byte[,,] temp2 = image_dest.Data;

            int counter;

            for (int y = 0; y < image_source.Height; y++)
            {
                counter = 0;
                for (int x = 0; x < image_source.Width; x++)
                {
                    if (temp1[y, x, 2] == 0 && temp1[y, x, 1] == 0 && temp1[y, x, 0] == 0) counter++;
                }
                if (counter == image_source.Width)
                    for (int x = 0; x < image_source.Width; x++)
                    {
                        temp2[y, x, 2] = 255;
                        temp2[y, x, 1] = 255;
                        temp2[y, x, 0] = 255;
                    }
            }
            image_dest.Data = temp2;
            pb.Image = image_dest.Bitmap;
        }

        private void checkConstraintsGloballyY(out int yMinGlobally, out int yMaxGlobally)
        {

            yMinGlobally = 0;
            yMaxGlobally = pB2.Height - 1;
            byte[,,] temp = pB2_image.Data;
            bool black = false;

            for (int i = 0; i < pB1.Height; i++)
            {
                yMinGlobally = i;
                for (int x = 0; x < pB1.Width; x++)
                {
                    if ((temp[i, x, 0] == 0) &&
                        (temp[i, x, 1] == 0) &&
                        (temp[i, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }

            for (int i = pB1.Height - 1; i > 0; i--)
            {
                yMaxGlobally = i;
                for (int x = 0; x < pB1.Width; x++)
                {
                    if ((temp[i, x, 0] == 0) &&
                        (temp[i, x, 1] == 0) &&
                        (temp[i, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }
        }

        private void checkConstraintsGloballyX(out int xMinGlobally, out int xMaxGlobally)
        {

            xMinGlobally = 0;
            xMaxGlobally = pB2.Width - 1;
            byte[,,] temp = pB2_image.Data;
            bool black = false;

            for (int x = 0; x < pB1.Width; x++)
            {
                xMinGlobally = x;
                for (int y = 0; y < pB1.Height; y++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }

            for (int x = pB1.Width - 1; x > 0; x--)
            {
                xMaxGlobally = x;
                for (int y = 0; y < pB1.Height; y++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }
        }

        private bool checkConstraintsLocally(out int xMin, out int xMax,
                                  out int yMin, out int yMax,
                                  int yMinGlobally, int yMaxGlobally,
                                  int x0)
        {
            byte[,,] temp = pB2_image.Data;
            xMin = 0;
            xMax = pB2.Width - 1;
            yMin = 0;
            yMax = pB2.Height - 1;
            bool black = false;
            int notBlackCounter = 0;

            for (int i = x0; i < pB1.Width; i++)
            {
                xMin = i;
                for (int y = yMinGlobally; y < yMaxGlobally; y++)
                {
                    if ((temp[y, i, 0] == 0) &&
                        (temp[y, i, 1] == 0) &&
                        (temp[y, i, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }

            for (int i = xMin; i < pB1.Width; i++)
            {
                xMax = i;
                notBlackCounter = 0;
                for (int y = yMinGlobally; y < yMaxGlobally; y++)
                {
                    if (!((temp[y, i, 0] == 0) &&
                        (temp[y, i, 1] == 0) &&
                        (temp[y, i, 2] == 0)))
                    {
                        notBlackCounter++;
                    }
                }
                if (notBlackCounter == yMaxGlobally - yMinGlobally)
                {
                    break;
                }
            }
            if (xMax == pB2.Width - 1)
            {
                return false;
            }

            for (int i = 0; i < pB1.Height; i++)
            {
                yMin = i;
                for (int x = xMin; x < xMax; x++)
                {
                    if ((temp[i, x, 0] == 0) &&
                        (temp[i, x, 1] == 0) &&
                        (temp[i, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }

            for (int i = pB1.Height - 1; i > 0; i--)
            {
                yMax = i;
                for (int x = xMin; x < xMax; x++)
                {
                    if ((temp[i, x, 0] == 0) &&
                        (temp[i, x, 1] == 0) &&
                        (temp[i, x, 2] == 0))
                    {
                        black = true;
                    }
                }
                if (black)
                {
                    black = false;
                    break;
                }
            }
            return true;
        }

        private void giveResult()
        {
            switch (symbol)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case 'x':
                    result = a * b;
                    break;
                case '/':
                    result = a / b;
                    break;
                default:
                    result = 0;
                    break;
            }
        }

        private void parseString(String str)
        {
            String[] temp = str.Split(' ');
            if (temp.Length != 3) throw new Exception();
            a = Convert.ToInt32(temp[0], 2);
            symbol = Convert.ToChar(temp[1]);
            b = Convert.ToInt32(temp[2], 2);
        }

        private String checkElement(int xMin, int xMax, int yMin, int yMax)
        {
            if (checkIfOperator(xMin, xMax, yMin, yMax))
            {
                if (checkIfMinus(xMin, xMax, yMin, yMax)) return " - ";
                else if (checkIfTimes(xMin, xMax, yMin, yMax)) return " x ";
                else if (checkIfDiv(xMin, xMax, yMin, yMax)) return " / ";
                else if (checkIfPlus(xMin, xMax, yMin, yMax)) return " + ";
                else return "N";
            }
            else
            {
                if (checkIfZero(xMin, xMax, yMin, yMax)) return "0";
                else return "1";
            }
        }

        private bool checkIfOperator(int xMin, int xMax, int yMin, int yMax)
        {
            int epsilon = (xMax - xMin) / 5;
            if ((xMax - xMin) + epsilon >= (yMax - yMin)) return true;
            else return false;
        }

        private bool checkIfDiv(int xMin, int xMax, int yMin, int yMax)
        {
            byte[,,] temp = pB2_image.Data;
            int changedColorCounter;
            int currentColor = 0;
            int previousColor = 0;
            int epsilon = (xMax - xMin) / 8;
            int x;

            for (int i = -epsilon; i <= epsilon; i++)
            {
                x = xMin + (xMax - xMin) / 2 + i;
                changedColorCounter = 0;
                for (int y = yMin; y <= yMax; y++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        currentColor = 0;
                    }
                    else
                    {
                        currentColor = 255;
                    }
                    if (y == yMin)
                    {
                        previousColor = currentColor;
                    }
                    if (currentColor != previousColor)
                    {
                        changedColorCounter++;
                    }
                    previousColor = currentColor;
                }
                //CvInvoke.Line(pB2_image, new Point(x, yMin), new Point(x, yMax), new MCvScalar(0, 255, 0));
                //pB2.Image = pB2_image.Bitmap;
                //Console.WriteLine(changedColorCounter);
                if (changedColorCounter >= 4)
                    return true;
            }
            return false;
        }

        private bool checkIfMinus(int xMin, int xMax, int yMin, int yMax)
        {
            if (2 * (yMax - yMin) < xMax - xMin) return true;
            return false;
        }

        private bool checkIfPlus(int xMin, int xMax, int yMin, int yMax)
        {
            byte[,,] temp = pB2_image.Data;
            int changedColorCounter = 0;
            int currentColor = 0;
            int previousColor = 0;
            int epsilon = (yMax - yMin) / 6;

            int y = yMin + epsilon;
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        currentColor = 0;
                    }
                    else
                    {
                        currentColor = 255;
                    }
                    if (x == xMin)
                    {
                        previousColor = currentColor;
                    }
                    if (currentColor != previousColor)
                    {
                        changedColorCounter++;
                    }
                    previousColor = currentColor;
                }
            }
            if (changedColorCounter == 2)
                return true;
            return false;
        }

        private bool checkIfTimes(int xMin, int xMax, int yMin, int yMax)
        {
            byte[,,] temp = pB2_image.Data;
            int changedColorCounter = 0;
            int currentColor = 0;
            int previousColor = 0;
            int epsilon = (yMax - yMin) / 6;

            int y = yMin + epsilon;
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        currentColor = 0;
                    }
                    else
                    {
                        currentColor = 255;
                    }
                    if (x == xMin)
                    {
                        previousColor = currentColor;
                    }
                    if (currentColor != previousColor)
                    {
                        changedColorCounter++;
                    }
                    previousColor = currentColor;
                }
            }
            if (changedColorCounter >= 4)
                return true;
            return false;
        }

        private bool checkIfZero(int xMin, int xMax, int yMin, int yMax)
        {
            byte[,,] temp = pB2_image.Data;
            int changedColorCounter = 0;
            int currentColor = 0;
            int previousColor = 0;

            int y = yMax - Math.Abs(yMax - yMin) / 3;
            {
                for (int x = xMin; x <= xMax; x++)
                {
                    if ((temp[y, x, 0] == 0) &&
                        (temp[y, x, 1] == 0) &&
                        (temp[y, x, 2] == 0))
                    {
                        currentColor = 0;
                    }
                    else
                    {
                        currentColor = 255;
                    }
                    if (x == xMin)
                    {
                        previousColor = currentColor;
                    }
                    if (currentColor != previousColor)
                    {
                        changedColorCounter++;
                    }
                    previousColor = currentColor;
                }
            }
            if (changedColorCounter == 2)
                return false;
            return true;
        }

        #endregion
        //-----------------------------------------------------------------------------------------------------------
        #region 4) Tools

        private void clear_image(PictureBox pB, Image<Bgr, byte> image)
        {
            image.SetZero();
            pB.Image = image.Bitmap;
        }

        private string get_image_path()
        {
            string ret = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Obrazy|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Wybierz obrazek.";
            //Jeśli wszystko przebiegło ok to pobiera nazwę pliku
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = openFileDialog1.FileName;
            }

            return ret;
        }

        private Bitmap get_image_bitmap_from_file(string path, ref Image<Bgr, byte> Data)
        {
            try
            {
                Mat temp = CvInvoke.Imread(path);
                CvInvoke.Resize(temp, temp, pB1.Size);
                Data = temp.ToImage<Bgr, byte>();
                pB1_image_virtual = temp.ToImage<Bgr, byte>();
                return Data.Bitmap;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return new Mat().Bitmap;
            }
        }

        private Image<Bgr, byte> strechImageFrame(Image<Bgr, byte> src)
        {
            Size size = src.Size;
            var resizedImage = new Image<Bgr, byte>(size.Width + 2, size.Height + 2);
            byte[,,] dst = resizedImage.Data;

            for (int X = 0; X < resizedImage.Width; X++)
            {
                for (int Y = 0; Y < resizedImage.Height; Y++)
                {
                    if (
                        X > 0 && X < resizedImage.Width - 1 &&
                        Y > 0 && Y < resizedImage.Height - 1
                    )
                    {
                        dst[Y, X, 2] = src.Data[Y - 1, X - 1, 2];
                        dst[Y, X, 1] = src.Data[Y - 1, X - 1, 1];
                        dst[Y, X, 0] = src.Data[Y - 1, X - 1, 0];
                    }
                    else if (X == 0 || X == resizedImage.Width - 1)
                    {
                        int X_org = X == 0 ? 0 : size.Width - 1;
                        int Y_org = (int)clamp(Y - 1, 0, size.Height - 1);

                        dst[Y, X, 2] = src.Data[Y_org, X_org, 2];
                        dst[Y, X, 1] = src.Data[Y_org, X_org, 1];
                        dst[Y, X, 0] = src.Data[Y_org, X_org, 0];
                    }
                    else if (Y == 0 || Y == resizedImage.Height - 1)
                    {
                        int X_org = (int)clamp(X - 1, 0, size.Width - 1);
                        int Y_org = Y == 0 ? 0 : size.Height - 1;

                        dst[Y, X, 2] = src.Data[Y_org, X_org, 2];
                        dst[Y, X, 1] = src.Data[Y_org, X_org, 1];
                        dst[Y, X, 0] = src.Data[Y_org, X_org, 0];
                    }

                }
            }

            return resizedImage;
        }

        private int clamp(int value, int min, int max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        #endregion
        //-----------------------------------------------------------------------------------------------------------
    }
}
