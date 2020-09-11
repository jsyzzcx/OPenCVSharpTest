using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCVSharpTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Mat sourceMat;//当前主窗体图像
        public Mat source2Mat;//当前主窗体图像
        public Mat tagetMat;//当前主窗体图像
        public int selectIndex;
        public enum lb1_list//记录操作方法
        {
            /// <summary>
            /// 灰度
            /// </summary>
            gray,
            /// <summary>
            /// 反色
            /// </summary>
            reverse,
            /// <summary>
            /// 二值化
            /// </summary>
            binary,
            /// <summary>
            /// 高斯滤波
            /// </summary>
            gaussianBlur,
            /// <summary>
            /// 均值滤波
            /// </summary>
            blur,
            /// <summary>
            /// 中值滤波
            /// </summary>
            medianBlur,
            /// <summary>
            /// 双边滤波
            /// </summary>
            bilateralFilter,
            /// <summary>
            /// 边缘检测-sobel
            /// </summary>
            sobel,
            /// <summary>
            /// 边缘检测-scharr
            /// </summary>
            scharr,
            /// <summary>
            /// 边缘检测-canny
            /// </summary>
            canny,
            /// <summary>
            /// 边缘检测-laplacian
            /// </summary>
            Laplacian,
            /// <summary>
            /// 浮雕
            /// </summary>
            carve,
            /// <summary>
            /// 左右反转
            /// </summary>
            rever_lr,
            /// <summary>
            /// 上下反转
            /// </summary>
            rever_tb,
            /// <summary>
            /// 毛玻璃
            /// </summary>
            frost,
            /// <summary>
            /// 锐化
            /// </summary>
            sharp,
            /// <summary>
            /// 膨胀
            /// </summary>
            dilate,
            /// <summary>
            /// 腐蚀
            /// </summary>
            erode,
            /// <summary>
            /// 顶帽
            /// </summary>
            tophat,
            /// <summary>
            /// 黑帽
            /// </summary>
            blackhat,
            /// <summary>
            /// gamma 暗部增强
            /// </summary>
            gamma,
            /// <summary>
            /// log 亮部增强
            /// </summary>
            log,
            /// <summary>
            /// 直方图
            /// </summary>
            hist,
            /// <summary>
            /// 绘制轮廓
            /// </summary>
            findContours,
            /// <summary>
            /// 模板匹配
            /// </summary>
            template,
            /// <summary>
            /// 最小外包矩形
            /// </summary>
            drawRect,

        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();//创建打开文件夹窗体
            ofd1.Title = "请选择输入的图像";
            ofd1.InitialDirectory = @"";//初始化目录
            ofd1.Multiselect = false;//不可多选
            ofd1.Filter = "图像文件|*.jpg;*.png;*.bmp|全部文件|*.*";
            ofd1.ShowDialog();

            if (ofd1.FileName != string.Empty)
            {
                try
                {
                    this.PictureBox_Source.Load(ofd1.FileName);
                    sourceMat = new Mat(ofd1.FileName);
                    tagetMat = new Mat(ofd1.FileName);
                }
                catch (Exception ex )
                {
                    MessageBox.Show("打开失败，请检查文件格式是否符合");
                }

            }
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tagetMat != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "保存文件";
                sfd.Filter = "JPGE图像|*.jpg|PNG图像|*.png|BMP图像|*.bmp|所有文件|*.*";
                sfd.InitialDirectory = Environment.CurrentDirectory;
                sfd.ShowDialog();
                if (sfd.FileName != null)
                {
                    try
                    {
                        PictureBox_Target.Image.Save(sfd.FileName);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                MessageBox.Show("无可保存图片");
            }
        }

        private void openBtn2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();//创建打开文件夹窗体
            ofd1.Title = "请选择输入的图像";
            ofd1.InitialDirectory = @"";//初始化目录
            ofd1.Multiselect = false;//不可多选
            ofd1.Filter = "图像文件|*.jpg;*.png;*.bmp|全部文件|*.*";
            ofd1.ShowDialog();

            if (ofd1.FileName != string.Empty)
            {
                try
                {
                    source2Mat = new Mat(ofd1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开失败，请检查文件格式是否符合");
                }

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            selectIndex = listBox1.SelectedIndex;
            handle(selectIndex);
        }

        public void handle(int selectIndex)
        {
            switch (selectIndex)
            {
                case (int)lb1_list.gray:
                    #region 转为灰度图
                    if (sourceMat.Channels() == 3)
                    {
                        Cv2.CvtColor(sourceMat, tagetMat, ColorConversionCodes.BGR2GRAY);
                        this.PictureBox_Target.Image = tagetMat.ToBitmap();
                    }
                    else
                    {
                        MessageBox.Show("已是灰度图像，不要重复操作");
                    }
                    #endregion
                    break;
                case (int)lb1_list.reverse:
                    #region 反色
                    tagetMat = ~sourceMat;
                    PictureBox_Target.Image = tagetMat.ToBitmap();
                    #endregion
                    break;
                case (int)lb1_list.binary:
                    #region 二值化处理
                    binary(this.trackBar1.Value+3, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.gaussianBlur:
                    #region 高斯滤波
                    gaussianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blur:
                    #region 均值滤波
                    blur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.medianBlur:
                    #region 中值滤波
                    medianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.bilateralFilter:
                    #region 双边滤波
                    bilateralFilter(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.sobel:
                    #region 边缘检测-sobel
                    sobel(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.scharr:
                    #region 边缘检测-scharr
                    scharr(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.canny:
                    #region 边缘检测-canny
                    canny(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.Laplacian:
                    #region 边缘检测-Laplacian
                    laplacian(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.carve:
                    #region 浮雕
                    carve(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.rever_lr:
                    #region 左右反转
                    rever_lr(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.rever_tb:
                    #region 上下反转
                    rever_tb(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.frost:
                    #region 毛玻璃
                    frost(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.sharp:
                    #region 锐化
                    sharp(tagetMat);
                    #endregion
                    break;
                case (int)lb1_list.dilate:
                    #region 膨胀
                    dilate(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.erode:
                    #region 腐蚀
                    erode(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.tophat:
                    #region 顶帽处理
                    tophat(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blackhat:
                    #region 黑帽处理
                    blackhat(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.gamma:
                    #region gamma 暗部增强
                    gamma(sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.log:
                    #region gamma log亮部增强
                    log(sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.hist:
                    #region 直方图
                    hist(sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.findContours:
                    #region 轮廓绘制
                    findContours(sourceMat,50,150);
                    #endregion
                    break;
                case (int)lb1_list.template:
                    #region 模板匹配
                    template(sourceMat,source2Mat);
                    #endregion
                    break;
                case (int)lb1_list.drawRect:
                    #region 最小外包矩形
                    drawRect(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                default:
                    break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            switch (selectIndex)
            {
                case (int)lb1_list.binary:
                    #region 二值化处理
                    binary(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.gaussianBlur:
                    #region 高斯滤波
                    gaussianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blur:
                    #region 均值滤波
                    blur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.medianBlur:
                    #region 中值滤波
                    medianBlur(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.bilateralFilter:
                    #region 双边滤波
                    bilateralFilter(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.canny:
                    #region 边缘检测-canny
                    canny(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.Laplacian:
                    #region 边缘检测-Laplacian
                    laplacian(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.dilate:
                    #region 膨胀
                    dilate(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.erode:
                    #region 腐蚀
                    erode(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;

                case (int)lb1_list.tophat:
                    #region 顶帽处理
                    tophat(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.blackhat:
                    #region 黑帽处理
                    blackhat(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                case (int)lb1_list.drawRect:
                    #region 最小外包矩形
                    drawRect(this.trackBar1.Value, sourceMat);
                    #endregion
                    break;
                default:
                    break;

            }
          
        }
        /// <summary>
        /// 二值化
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void binary(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.Threshold(tempMat, tempMat, pos * 10, 255, ThresholdTypes.Binary);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 高斯滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void gaussianBlur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.GaussianBlur(tempMat, tempMat, new OpenCvSharp.Size(pos * 2 + 1, pos * 2 + 1), 5);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 均值滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void blur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.Blur(tempMat, tempMat, new OpenCvSharp.Size(pos + 1, pos + 1));
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 中值滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void medianBlur(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Cv2.MedianBlur(tempMat, tempMat, pos * 2 + 1);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 双边滤波
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void bilateralFilter(int pos, Mat mat)
        {
            Mat tempMat1 = new Mat();
            Mat tempMat2 = new Mat();
            mat.CopyTo(tempMat1);

            Cv2.BilateralFilter(tempMat1, tempMat2, pos * 5 + 1, 100, 100);
            tempMat2.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat1.Release();
            tempMat2.Release();
        }
        /// <summary>
        /// 边缘检测-sobel
        /// </summary>
        /// <param name="mat"></param>
        private void sobel(Mat mat)
        {
            Mat sobelx = new Mat();
            Mat sobely = new Mat();
            Cv2.Sobel(mat, sobelx, -1, 1, 0);
            Cv2.Sobel(mat, sobely, -1, 0, 1);
            Cv2.AddWeighted(sobelx, 0.5, sobely, 0.5, 0, mat);//gamma为加到结果上的值
            PictureBox_Target.Image = mat.ToBitmap();
            sobelx.Release();
            sobelx.Release();
        }
        /// <summary>
        /// 边缘检测-scharr
        /// </summary>
        /// <param name="mat"></param>
        private void scharr(Mat mat)
        {
            Mat scharrx = new Mat();
            Mat scharry = new Mat();
            Cv2.Scharr(mat, scharrx, -1, 1, 0);
            Cv2.Scharr(mat, scharry, -1, 0, 1);
            Cv2.AddWeighted(scharrx, 0.5, scharry, 0.5, 0, mat);
            PictureBox_Target.Image = mat.ToBitmap();
            scharrx.Release();
            scharry.Release();
        }
        /// <summary>
        /// 边缘检测-canny
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="userdata"></param>
        public void canny(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            if (tempMat.Channels() == 3)
            {
                Cv2.CvtColor(tempMat, tempMat, ColorConversionCodes.BGR2GRAY);
            }

            Cv2.Canny(tempMat, tempMat, pos * 25, pos * 25);
            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
            Thread.Sleep(50);
        }
        /// <summary>
        /// 边缘检测-Laplacian
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void laplacian(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);
            if (pos * 2 + 1 <=31)
            {
                Cv2.Laplacian(tempMat, tempMat, -1, pos * 2 + 1);
                tempMat.CopyTo(tagetMat);
                PictureBox_Target.Image = tagetMat.ToBitmap();
            }

            tempMat.Release();
        }
        /// <summary>
        /// 浮雕
        /// </summary>
        /// <param name="mat"></param>
        private void carve(Mat mat)
        {
            if (mat.Channels() != 1)
            {
                Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2GRAY);
            }

            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    int newP = 2 * mat.Get<byte>(i, j) - mat.Get<byte>(i, j + 1) - mat.Get<byte>(i + 1, j) + 100;

                    if (newP > 255)
                    {
                        newP = 255;
                    }
                    else if (newP < 0)
                    {
                        newP = 0;
                    }
                    mat.Set(i, j, (byte)newP);
                }
            }
            this.PictureBox_Target.Image = mat.ToBitmap();
        }
        /// <summary>
        /// 左右反转
        /// </summary>
        /// <param name="mat"></param>
        private void rever_lr(Mat mat)
        {
            Mat imx_lr = new Mat(mat.Size(), MatType.CV_32FC1);
            Mat imy_lr = new Mat(mat.Size(), MatType.CV_32FC1);
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    imx_lr.Set(i, j, (float)(mat.Cols - j - 1));
                    imy_lr.Set(i, j, (float)i);
                }
            }
            Cv2.Remap(mat, mat, imx_lr, imy_lr);

            PictureBox_Target.Image = mat.ToBitmap();
            imx_lr.Release();
            imx_lr.Release();
        }
        /// <summary>
        /// 上下反转
        /// </summary>
        /// <param name="mat"></param>
        private void rever_tb(Mat mat)
        {
            Mat imx_tb = new Mat(mat.Size(), MatType.CV_32FC1);
            Mat imy_tb = new Mat(mat.Size(), MatType.CV_32FC1);
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    imx_tb.Set(i, j, (float)j);
                    imy_tb.Set(i, j, (float)(mat.Rows - i - 1));
                }
            }
            Cv2.Remap(mat, mat, imx_tb, imy_tb);

            PictureBox_Target.Image = mat.ToBitmap();
            imx_tb.Release();
            imy_tb.Release();
        }
        /// <summary>
        /// 毛玻璃
        /// </summary>
        /// <param name="mat"></param>
        private void frost(Mat mat)
        {
            Mat imx_frost = new Mat(mat.Size(), MatType.CV_32FC1);
            Mat imy_frost = new Mat(mat.Size(), MatType.CV_32FC1);
            Random rm = new Random();
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    imx_frost.Set(i, j, (float)(j + rm.Next(-5, 5)));
                    imy_frost.Set(i, j, (float)(i + rm.Next(-5, 5)));
                }
            }
            Cv2.Remap(mat, mat, imx_frost, imy_frost);

            PictureBox_Target.Image = mat.ToBitmap();
            imx_frost.Release();
            imy_frost.Release();
        }
        /// <summary>
        /// 锐化
        /// </summary>
        /// <param name="mat"></param>
        private void sharp(Mat mat)
        {
            Mat mask = new Mat(new OpenCvSharp.Size(3, 3), MatType.CV_32FC1);
            mask.Set<float>(0, 1, -1); mask.Set<float>(1, 0, -1); mask.Set<float>(1, 1, 5); mask.Set<float>(1, 2, -1); mask.Set<float>(2, 1, -1);
    
            Cv2.Filter2D(mat, mat, -1, mask);
            Cv2.WaitKey(500);

            PictureBox_Target.Image = mat.ToBitmap();
            mask.Release();
        }
        /// <summary>
        /// 膨胀处理
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void dilate(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Mat structureElement = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(pos * 2 + 1, pos * 2 + 1));
            Cv2.Dilate(tempMat, tempMat, structureElement);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 腐蚀处理
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void erode(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Mat structureElement = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(pos * 2 + 1, pos * 2 + 1));
            Cv2.Erode(tempMat, tempMat, structureElement);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 顶帽处理
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void tophat(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Mat stuctureElement = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(pos + 1, pos + 1), new OpenCvSharp.Point(-1, -1));
            Cv2.MorphologyEx(tempMat, tempMat, MorphTypes.TopHat, stuctureElement);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 黑帽处理
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void blackhat(int pos, Mat mat)
        {
            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);

            Mat stuctureElement = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(pos + 1, pos + 1), new OpenCvSharp.Point(-1, -1));
            Cv2.MorphologyEx(tempMat, tempMat, MorphTypes.BlackHat, stuctureElement);

            tempMat.CopyTo(tagetMat);
            PictureBox_Target.Image = tagetMat.ToBitmap();

            tempMat.Release();
        }
        /// <summary>
        /// 直方图
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void hist(Mat mat)
        {
            if (mat.Channels() == 1)
            {
                Mat[] image = { mat };
                Mat temp_hist = mat;
                int[] channels = new int[] { 0 };
                int[] histsize = new int[] { 256 };
                Mat mask_hist = new Mat();
                Mat hist = new Mat();

                //Rangef[] range = new Rangef[1];
                //range[0].Start = 0f;
                //range[0].End = 256f;

                Rangef[] range = { new Rangef(0f, 256f) };

                Cv2.CalcHist(image, channels, mask_hist, hist, 1, histsize, range);
                Mat histImage = new Mat(256, 256, MatType.CV_8UC3);
                double minValue, maxValue;
                Cv2.MinMaxLoc(hist, out minValue, out maxValue);
                for (int i = 0; i < 256; i++)
                {
                    int len = (int)(hist.Get<float>(i) / maxValue * 256);
                    Cv2.Line(histImage, i, histImage.Rows, i, histImage.Rows - len, Scalar.White, 2);
                }
                Cv2.ImShow("灰度图像直方图", histImage);
            }
            else
            {
                Mat[] images = mat.Split();
                Mat[] bImage = { images[0] };
                Mat[] gImage = { images[1] };
                Mat[] rImage = { images[2] };

                Mat mask_hist = new Mat();
                Mat[] hists = { new Mat(), new Mat(), new Mat() };
                int[] channels = { 0 };
                int[] histSize = { 256 };
                //Rangef[] range = new Rangef[1];
                //range[0].Start = 0f;
                //range[0].End = 256f;

                Rangef[] range = { new Rangef(0f, 256f) };

                Cv2.CalcHist(bImage, channels, mask_hist, hists[0], 1, histSize, range);//dim为需要统计直方图通道的个数
                Cv2.CalcHist(gImage, channels, mask_hist, hists[1], 1, histSize, range);
                Cv2.CalcHist(rImage, channels, mask_hist, hists[2], 1, histSize, range);

                Mat[] histImage = { new Mat(256, 256, MatType.CV_8UC3), new Mat(256, 256, MatType.CV_8UC3), new Mat(256, 256, MatType.CV_8UC3) };
                Scalar[] color = { Scalar.Blue, Scalar.Green, Scalar.Red };
                for (int i = 0; i < 3; i++)
                {
                    double minVal = 0f;
                    double maxVal = 0f;
                    Cv2.MinMaxLoc(hists[i], out minVal, out maxVal);
                    for (int j = 0; j < 256; j++)
                    {
                        int len = (int)(hists[i].Get<float>(j) / maxVal * 256);
                        Cv2.Line(histImage[i], j, histImage[0].Rows, j, histImage[0].Rows - len, color[i], 2);
                    }
                }
                Cv2.ImShow("b", histImage[0]);
                Cv2.ImShow("g", histImage[1]);
                Cv2.ImShow("r", histImage[2]);
            }
        }
        /// <summary>
        /// gamma 暗部增强
        /// </summary>
        /// <param name="mat"></param>
        private void gamma(Mat mat)
        {
            if (mat.Channels() == 1)
            {
                Mat temp_gray = new Mat(mat.Size(), MatType.CV_16UC1);
                for (int i = 0; i < mat.Rows; i++)
                {
                    for (int j = 0; j < mat.Cols; j++)
                    {
                        temp_gray.Set(i, j, Math.Abs(mat.Get<byte>(i, j) * mat.Get<byte>(i, j)));//因为pictureMain为8UC1，所以这里一定要使用<byte>,不能使用其他类型
                    }
                }
                Cv2.Normalize(temp_gray, temp_gray, 0, 255, NormTypes.MinMax);
                Cv2.ConvertScaleAbs(temp_gray, temp_gray);
                temp_gray.CopyTo(mat);
                PictureBox_Target.Image = mat.ToBitmap();
                temp_gray.Release();
            }
            else
            {
                Mat temp_gamma = new Mat(mat.Size(), MatType.CV_32FC3);
                Vec3f channels = new Vec3f();
                for (int i = 0; i < mat.Rows; i++)
                {
                    for (int j = 0; j < mat.Cols; j++)
                    {
                        channels.Item0 = Math.Abs(mat.Get<Vec3b>(i, j).Item0 * mat.Get<Vec3b>(i, j).Item0);
                        channels.Item1 = Math.Abs(mat.Get<Vec3b>(i, j).Item1 * mat.Get<Vec3b>(i, j).Item1);
                        channels.Item2 = Math.Abs(mat.Get<Vec3b>(i, j).Item2 * mat.Get<Vec3b>(i, j).Item2);
                        temp_gamma.Set(i, j, channels);
                    }
                }
                Cv2.Normalize(temp_gamma, temp_gamma, 0, 255, NormTypes.MinMax);
                Cv2.ConvertScaleAbs(temp_gamma, temp_gamma);
                temp_gamma.CopyTo(mat);
                PictureBox_Target.Image = mat.ToBitmap();
                temp_gamma.Release();
            }
        }
        /// <summary>
        /// gamma 暗部增强
        /// </summary>
        /// <param name="mat"></param>
        private void log(Mat mat)
        {
            Mat[] temp_log = mat.Split();
            for (int i = 0; i < mat.Rows; i++)
            {
                for (int j = 0; j < mat.Cols; j++)
                {
                    temp_log[0].Set(i, j, Math.Log(temp_log[0].Get<float>(i, j), 1.2));
                    temp_log[1].Set(i, j, Math.Log(temp_log[1].Get<float>(i, j), 1.2));
                    temp_log[2].Set(i, j, Math.Log(temp_log[2].Get<float>(i, j), 1.2));
                }
            }
            Cv2.Merge(temp_log, mat);
            Cv2.Normalize(mat, mat, 0, 255, NormTypes.MinMax);
            PictureBox_Target.Image = mat.ToBitmap();


            ////Mat temp_log = new Mat(pictureMain.Size(), MatType.CV_8UC3);
            ////pictureMain.CopyTo(temp_log);
            ////if (pictureMain.Channels() == 3)
            ////{
            ////    Vec3i channels = new Vec3i();
            ////    for (int i = 0; i < pictureMain.Rows; i++)
            ////    {
            ////        for (int j = 0; j < pictureMain.Cols; j++)
            ////        {
            ////            //float a = pictureMain.Get<Vec3b>(i, j).Item0;
            ////            //float b = pictureMain.Get<Vec3b>(i, j).Item1;
            ////            //float c = pictureMain.Get<Vec3b>(i, j).Item2;
            ////            channels.Item0 = (int)Math.Log(pictureMain.Get<Vec3b>(i, j).Item0, 1.2);
            ////            channels.Item1 = (int)Math.Log(pictureMain.Get<Vec3b>(i, j).Item1, 1.2);
            ////            channels.Item2 = (int)Math.Log(pictureMain.Get<Vec3b>(i, j).Item2, 1.2);
            ////            temp_log.Set<Vec3i>(i, j, channels);
            ////        }
            ////    }//这个操作有毒，不能归一化，还把GR通道置0,放弃
            ////    temp_log.Normalize(255, 0, NormTypes.MinMax);
            ////    float e1 = temp_log.Get<Vec3b>(0, 0).Item0;
            ////    float f = temp_log.Get<Vec3b>(0, 0).Item1;
            ////    float g = temp_log.Get<Vec3b>(0, 0).Item2;
            ////    Cv2.Normalize(temp_log, temp_log, 0, 255, NormTypes.MinMax);
            ////    Cv2.ConvertScaleAbs(temp_log, temp_log);
            ////    temp_log.CopyTo(pictureMain);
            ////    pictureShow.Image = pictureMain.ToBitmap();
            ////    temp_log.Release();

            ////    display("log亮部增强");
            ////}
        }
        /// <summary>
        /// 绘制轮廓
        /// </summary>
        /// <param name="mat"></param>
        private void findContours(Mat mat, double threshold1, double threshold2)
        {
            MessageBox.Show("假如卡死了，是计算量太大的缘故，请重新调整canny参数一参数二，默认参数一50，参数二150");

            Mat tempMat = new Mat();
            if (mat.Channels() == 3)
            {
                Cv2.CvtColor(mat, tempMat, ColorConversionCodes.BGR2GRAY);
            }

            Cv2.Canny(tempMat, tempMat, threshold1, threshold2);

            HierarchyIndex[] hierarchy;
            OpenCvSharp.Point[][] coutours;
            Cv2.FindContours(tempMat, out coutours, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxNone);
            for (int i = 0; i < coutours.Length; i++)
            {
                Cv2.DrawContours(tagetMat, coutours, i, Scalar.RandomColor(), 1);
            }
 
            PictureBox_Target.Image = tagetMat.ToBitmap();
            tempMat.Release();
        }
        /// <summary>
        /// 最小外包矩形
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="mat"></param>
        private void drawRect(int pos, Mat mat)
        {
            Mat tempSourceMat = new Mat();
            mat.CopyTo(tempSourceMat);


            Mat tempMat = new Mat();
            mat.CopyTo(tempMat);
            if (tempMat.Channels() == 3)
            {
                Cv2.CvtColor(tempMat, tempMat, ColorConversionCodes.BGR2GRAY);
            }

            Cv2.Threshold(~tempMat, tempMat, pos * 25, 255, ThresholdTypes.Binary);
            //Cv2.Canny(a, a, pos*10, 255);
            HierarchyIndex[] hierarchy;
            OpenCvSharp.Point[][] coutours;
            Cv2.FindContours(tempMat, out coutours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            OpenCvSharp.Point[][] contours_ploy = new OpenCvSharp.Point[coutours.Length][];
            RotatedRect[] RotatedRect_ploy = new RotatedRect[coutours.Length];
            Rect[] rect_poly = new Rect[coutours.Length];
            for (int i = 0; i < coutours.Length; i++)
            {
                contours_ploy[i] = Cv2.ApproxPolyDP(coutours[i], 10, true);//计算凸包
                rect_poly[i] = Cv2.BoundingRect(coutours[i]);//最小外接矩形，我们不用，
                if (contours_ploy[i].Length > 5)//拟合的线条数不少于5
                {
                    RotatedRect temp1 = Cv2.MinAreaRect(contours_ploy[i]);//最小外接矩形，能旋转
                    RotatedRect_ploy[i] = temp1;//将该矩形放入集合中
                }
            }

            Point2f[] pot = new Point2f[4];
            for (int i = 0; i < RotatedRect_ploy.Length; i++)
            {
                pot = RotatedRect_ploy[i].Points();
                double line1 = Math.Sqrt((pot[1].Y - pot[0].Y) * (pot[1].Y - pot[0].Y) + (pot[1].X - pot[0].X) * (pot[1].X - pot[0].X));
                double line2 = Math.Sqrt((pot[3].Y - pot[0].Y) * (pot[3].Y - pot[0].Y) + (pot[3].X - pot[0].X) * (pot[3].X - pot[0].X));
                if (line1 * line2 < 9000)//太小直接pass
                {
                    continue;
                }

                for (int j = 0; j < 4; j++)
                {
                    Cv2.Line(tempSourceMat, (OpenCvSharp.Point)pot[j], (OpenCvSharp.Point)pot[(j + 1) % 4], Scalar.Green, 3);
                }
            }

            PictureBox_Source.Image = tempSourceMat.ToBitmap();
            PictureBox_Target.Image = tempMat.ToBitmap();
            tempMat.Release();

            //tempSourceMat.CopyTo(sourceMat);
            tempSourceMat.Release();
        }
        /// <summary>
        /// 模板匹配
        /// </summary>
        /// <param name="mat"></param>
        private void template(Mat mat ,Mat mat2)
        {
            if (mat2 != null && mat.Rows > mat2.Rows && mat.Cols > mat2.Cols)
            {
                Mat result = new Mat(mat.Cols - mat2.Cols, mat.Rows - mat2.Cols, MatType.CV_32FC1);
                Cv2.MatchTemplate(mat, mat2, result, TemplateMatchModes.CCoeff);
                double maxVal, minVal;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.Normalize(result, result, 0, 1, NormTypes.MinMax);
                Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);
                Cv2.ImShow("匹配的图像", result);
                mat.Rectangle(maxLoc, new OpenCvSharp.Point(maxLoc.X + mat2.Cols, maxLoc.Y + mat2.Rows), Scalar.Red);
                PictureBox_Target.Image = mat.ToBitmap();
            }
            else if (mat2 == null)
            {
                MessageBox.Show("请在窗口二中传入匹配图像");
            }
            else
            {
                MessageBox.Show("匹配图像需要比原始图像小");
            }
        }

    }
}
