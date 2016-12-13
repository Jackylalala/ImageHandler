using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;

namespace ImageHandler
{
    class ImageHandler
    {
        #region | Fields |

        private Bitmap bmp;
        private int width;
        private int height;
        private int[, ,] rgbData;
        private string filename; //include path
        private System.IO.FileInfo imgInfo; //file infomation
        private float dpiX;
        private float dpiY;
        private bool draft=false;

        #endregion

        #region | Constructors |

        public ImageHandler(string filename)
        {
            this.filename = filename;
            //create a copy(avoid locking file)
            using (Image bmpTemp = Image.FromFile(filename))
            {
                bmp = new Bitmap(bmpTemp);
                dpiX = bmpTemp.HorizontalResolution;
                dpiY = bmpTemp.VerticalResolution;
            }
            width = bmp.Width;
            height = bmp.Height;
            imgInfo = new System.IO.FileInfo(filename);
        }

        public ImageHandler(int width, int height)
        {
            filename = "Untitled";
            draft = true;
            bmp = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(bmp);
            gr.FillRectangle(Brushes.White,new Rectangle(0,0,bmp.Width,bmp.Height));
            gr.Dispose();
            this.width = bmp.Width;
            this.height = bmp.Height;
            dpiX = 72;
            dpiY = 72;
        }

        #endregion

        #region | Properties |

        public Bitmap Bmp
        {
            get
            {
                if (bmp != null)
                    return bmp;
                else
                    return new Bitmap(1, 1);
            }
            set { bmp = value; }
        }
        public int[, ,] RGBData
        {
            get
            {
                GetRGBData();
                return rgbData;
            }
        }
        public int Width { get { return width; } set { width=value;}}
        public int Height { get { return height; } set { height=value;}}
        public float DPIX { get { return dpiX; } }
        public float DPIY { get { return dpiY; } }
        public string Filename { get { return filename; } }
        public System.IO.FileInfo ImgInfo { get { return imgInfo; } }

        #endregion

        #region | Static methods |

        private static Point RotatePoint(Point srcCoord, double angle)
        {
            Point dstCoord = new Point();
            //define sin and cos
            double sin = Math.Sin(angle * Math.PI / 180);
            double cos = Math.Cos(angle * Math.PI / 180);
            dstCoord.X = (int)Math.Round(((double)srcCoord.X * Math.Cos(angle * Math.PI / 180) - (double)srcCoord.Y * Math.Sin(angle * Math.PI / 180)), 0, MidpointRounding.AwayFromZero);
            dstCoord.Y = (int)Math.Round(((double)srcCoord.X * Math.Sin(angle * Math.PI / 180) + (double)srcCoord.Y * Math.Cos(angle * Math.PI / 180)), 0, MidpointRounding.AwayFromZero);
            return dstCoord;
        }

        #endregion

        #region | Methods |

        public bool isDraft()
        {
            return draft;
        }

        /// <summary>
        /// Read rgb information into 3-dimensional array via pointer
        /// </summary>
        private unsafe void GetRGBData()
        {
            rgbData = new int[width, height, 3];
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            byte* p = (byte*)(void*)bmpData.Scan0;
            int stride = bmpData.Stride;
            Parallel.For(0, height, i =>
            {
                Parallel.For(0, width, j =>
                {
                    //RGB information in GDI+ is allign with the format "BGR" instead of "RGB".
                    int k = 3 * j + i * stride;
                    rgbData[j, i, 2] = p[k];
                    rgbData[j, i, 1] = p[k + 1];
                    rgbData[j, i, 0] = p[k + 2];
                });
            });
            bmp.UnlockBits(bmpData);
        }

        /// <summary>
        /// Constrtct bitmap from 3-dimensional rgb array via pointer
        /// </summary>
        private unsafe void SetRGBData()
        {
            width = rgbData.GetLength(0);
            height = rgbData.GetLength(1);
            Rectangle rect = new Rectangle(0, 0, width, height);
            bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            byte* p = (byte*)(void*)dstBmpData.Scan0;
            //int offset = dstBmpData.Stride - width * 3;
            int stride = dstBmpData.Stride;
            Parallel.For(0, height, i =>
            {
                Parallel.For(0, width, j =>
                {
                    int k = 3 * j + i * stride;
                    p[k] = (byte)rgbData[j, i, 2];
                    p[k + 1] = (byte)rgbData[j, i, 1];
                    p[k + 2] = (byte)rgbData[j, i, 0];
                });
            });
            bmp.UnlockBits(dstBmpData);
        }


        /// <summary>
        /// Change to grayscale
        /// </summary>
        public unsafe void ToGray()
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte* p = (byte*)bmpData.Scan0.ToPointer();
            int stride = bmpData.Stride;
            Parallel.For(0, height, i =>
            {
                Parallel.For(0, width, j =>
                {
                    //RGB information in GDI+ is allign with the format "BGR" instead of "RGB".
                    int k = 3 * j + i * stride;
                    int temp = (int)(0.299 * p[k + 2] + 0.587 * p[k + 1] + 0.114 * p[k]);
                    p[k] = (byte)temp;
                    p[k + 1] = (byte)temp;
                    p[k + 2] = (byte)temp;
                });
            });
            bmp.UnlockBits(bmpData);
        }

        /// <summary>
        /// Rotate with a specific angle
        /// </summary>
        /// <param name="angle">specific angle</param>
        public unsafe void Rotate(float angle)
        {
            /*
             * right, down = positive
             * p1------------p2
             * |            | 
             * p4------------p3
             * 
             * p1(0,0), p2(width-1,0), p3(width-1,height-1), p4(0,height-1)
             * 
             * In this coordinate system(left-handed coordinate system), clockwise rotation matrix (theta): (cos -sin 
             *                                                                                               sin  cos)
             * 
             */
            //Calculate new vertex
            Point p1 = RotatePoint(new Point(0, 0), angle);
            Point p2 = RotatePoint(new Point(width - 1, 0), angle);
            Point p3 = RotatePoint(new Point(width - 1, height - 1), angle);
            Point p4 = RotatePoint(new Point(0, height - 1), angle);
            //Calculate new size
            int dstWidth = Math.Max(Math.Abs(p3.X - p1.X) + 1, Math.Abs(p4.X - p2.X) + 1);
            int dstHeight = Math.Max(Math.Abs(p3.Y - p1.X) + 1, Math.Abs(p4.Y - p2.Y) + 1);
            /*
             * Calculate offset between old and new coordinate system
             * left-top point in new coordiante system -> (0,0)
             * 
             */
            int offsetX = -new int[4] { p1.X, p2.X, p3.X, p4.X }.Min();
            int offsetY = -new int[4] { p1.Y, p2.Y, p3.Y, p4.Y }.Min();
            //create bmp
            Bitmap dstBitmap = new Bitmap(dstWidth, dstHeight, PixelFormat.Format32bppArgb);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            Rectangle dstRect = new Rectangle(0, 0, dstWidth, dstHeight);
            BitmapData srcBmpData = bmp.LockBits(srcRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstBmpData = dstBitmap.LockBits(dstRect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            //define sin and cos
            double sin = Math.Sin(angle * Math.PI / 180);
            double cos = Math.Cos(angle * Math.PI / 180);
            int srcStride = srcBmpData.Stride;
            int dstStride = dstBmpData.Stride;
            //define pointer
            byte* srcP = (byte*)srcBmpData.Scan0.ToPointer();
            byte* dstP = (byte*)dstBmpData.Scan0.ToPointer();
            Parallel.For(0,dstHeight,i=>
            {
                Parallel.For(0, dstWidth, j =>
                {
                    //don't forget coordinate system is difference from usual
                    /* Normal coordinate system: right and up are positive
                     * Current: right and down are positive
                     * Therefore using (dstHeight-1-i) or something like this formula to correct     
                     */
                    int k = 4 * j + i * dstStride;
                    //Calculate corresponding point in old coordinate system
                    Point oldPoint = RotatePoint(new Point(j - offsetX, i - offsetY), -angle);
                    if (oldPoint.X >= 0 && oldPoint.X < width && oldPoint.Y >= 0 && oldPoint.Y < height)
                    {
                        dstP[k] = srcP[4 * oldPoint.X + srcStride * oldPoint.Y];
                        dstP[k + 1] = srcP[4 * oldPoint.X + srcStride * oldPoint.Y + 1];
                        dstP[k + 2] = srcP[4 * oldPoint.X + srcStride * oldPoint.Y + 2];
                        dstP[k + 3] = srcP[4 * oldPoint.X + srcStride * oldPoint.Y + 3];
                    }
                    else
                    {
                        dstP[k] = dstP[k + 1] = dstP[k + 2] = 0xff;
                        dstP[k + 3] = 0x0;
                    }
                });
            });
            bmp.UnlockBits(srcBmpData);
            dstBitmap.UnlockBits(dstBmpData);
            bmp = (Bitmap)dstBitmap.Clone();
            dstBitmap.Dispose();
            width = bmp.Width;
            height = bmp.Height;
        }

        public unsafe void ChangeBrightness(int brightness)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* p = (byte*)bmpData.Scan0.ToPointer();
            int stride = bmpData.Stride;
            Parallel.For(0, height, i =>
            {
                Parallel.For(0, width, j =>
                {
                    //ARGB information in GDI+ is allign with the format "BGRA" instead of "ARGB".
                    int k = 4 * j + i * stride;
                    for (int n = 0; n < 3; n++) //Ignore alpha channel
                    {
                        if ((p[k + n] + brightness) > 255)
                            p[k + n] = 255;
                        else if ((p[k + n] + brightness) < 0)
                            p[k + n] = 1;
                        else
                            p[k + n] = (byte)(p[k + n] + brightness);
                    }
                });
            });
            bmp.UnlockBits(bmpData);
        }

        /// <summary>
        /// Using Graphic.Drawing method implement resizing
        /// </summary>
        /// <param name="nWidth">New width</param>
        /// <param name="nHeight">New height</param>
        public void Resize(int nWidth, int nHeight)
        {
            if (nWidth > 9999 || nHeight > 9999 || nWidth < 1 || nHeight < 1)
            {
                MessageBox.Show("Image resolution is out of range (width and height shoule between 1-9999 pixels).", "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //create bmp
            Bitmap dstBitmap = new Bitmap(nWidth, nHeight,PixelFormat.Format32bppArgb);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            Rectangle dstRect = new Rectangle(0, 0, nWidth, nHeight);
            
            using (var gr = Graphics.FromImage(dstBitmap))
            {
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.CompositingMode = CompositingMode.SourceCopy;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(bmp, dstRect, 0, 0, width, height, GraphicsUnit.Pixel);
            }
            bmp = (Bitmap)dstBitmap.Clone();
            dstBitmap.Dispose();
            width = bmp.Width;
            height = bmp.Height;
        }

        /// <summary>
        /// Using manually memory control method implement resizing
        /// </summary>
        /// <param name="nWidth">New width</param>
        /// <param name="nHeight">New height</param>
        public unsafe void Resize_Manually(int nWidth, int nHeight)
        {
            if (nWidth > 9999 || nHeight > 9999 || nWidth < 1 || nHeight < 1)
            {
                MessageBox.Show("Image resolution is out of range (width and height shoule between 1-9999 pixels).", "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            float scaleX = (float)nWidth / width;
            float scaleY = (float)nHeight / height;
            //create bmp
            Bitmap dstBitmap = new Bitmap(nWidth, nHeight, PixelFormat.Format24bppRgb);
            Rectangle srcRect = new Rectangle(0, 0, width, height);
            Rectangle dstRect = new Rectangle(0, 0, nWidth, nHeight);
            BitmapData srcBmpData = bmp.LockBits(srcRect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = dstBitmap.LockBits(dstRect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            int srcStride = srcBmpData.Stride;
            int dstStride = dstBmpData.Stride;
            //define pointer
            byte* srcP = (byte*)srcBmpData.Scan0.ToPointer();
            byte* dstP = (byte*)dstBmpData.Scan0.ToPointer();
            Parallel.For(0, nHeight, i =>
            {
                Parallel.For(0, nWidth, j =>
                {
                    //RGB information in GDI+ is allign with the format "BGR" instead of "RGB".
                    int k = 3 * j + i * dstStride;
                    //avoid over source bounds
                    int srcX = Math.Min((int)(j / scaleX), width - 1);
                    int srcY = Math.Min((int)(i / scaleY), height - 1);
                    int k2 = 3 * srcX + srcY * srcStride;
                    dstP[k] = srcP[k2];
                    dstP[k + 1] = srcP[k2 + 1];
                    dstP[k + 2] = srcP[k2 + 2];
                });
            });
            bmp.UnlockBits(srcBmpData);
            dstBitmap.UnlockBits(dstBmpData);
            bmp = (Bitmap)dstBitmap.Clone();
            dstBitmap.Dispose();
            width = bmp.Width;
            height = bmp.Height;
        }

        #endregion
    }
}
