/*

(c) 2004, Marc Clifton
All Rights Reserved

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, 
this list of conditions and the following disclaimer. 

Redistributions in binary form must reproduce the above copyright notice, 
this list of conditions and the following disclaimer in the documentation 
and/or other materials provided with the distribution. 

Neither the name of Marc Clifton nor the names of its contributors may 
be used to endorse or promote products derived from this software without 
specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/
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
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms.Design;
using System.Reflection;
using System.IO;

namespace ImageHandler
{
    public partial class frmImageHandler : Form, IMessageFilter
    {

        #region | Win32 APIs |

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        #endregion

        #region | Fields |

        private List<string> allowedExt=new List<string>(new string[]{".jpg",".jpeg",".bmp",".tif",".tiff",".png",".gif"});
        //Image
        private ImageHandler img;
        private Bitmap backupBitmap; //backup image for undo action
        private Bitmap srcBitmap; //source bitmap
        private Bitmap selectBitmap; //selection buffer
        private Bitmap buffBitmap; //Buffer map during drawing
        //Handle mouse wheel event(directly pass to panel)
        private bool mFiltering;
        //Scale variable
        private float scaleFactor;
        //Toolbar items
        private ToolStripRadioButtonMenuItem[] radZoom = new ToolStripRadioButtonMenuItem[2]; //Zoom option button set
        private TextBoxStripItem txtScale = new TextBoxStripItem();
        private ToolStripLabel lblScale = new ToolStripLabel();
        private ButtonStripItem btnNarrow = new ButtonStripItem();
        private ButtonStripItem btnEnlarge = new ButtonStripItem();
        //Calcd. time waste
        private System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch(); //calcd. processing time
        //Click position with/without scale factor
        private List<Point> unScaledPoints = new List<Point>();
        private List<Point> scaledPoints = new List<Point>();
        //Start draw/selection
        private bool isMouseDown;
        //Bg and foreground color
        private Color foreColor;
        private Color backColor; //backaground color
        //Selection variables
        private GraphicsPath selectionPath = new GraphicsPath();
        private Rectangle selectionRect; //selection rectangle
        private bool selectionMove; //During selction area moving
        private bool selectionFromSelf; //Selection area come from self
        private Point movingCenter; //Center point during moving
        private Point clickPointOffset; //Offset from click point to the center of selection area
        //Variables for thumbnail during drag&drop
        private Thread getImageThread;
        public delegate void AssignImageDlgt();
        private string lastFilename="";
        private Image thumbnailImage;
        private bool validData;
        private int lastX=0;
        private int lastY=0;
        //Keyboard action
        private bool shiftKeyDown=false;
        //Modify flag
        private bool modified = false;
        #endregion

        #region | Constrctors |

        public frmImageHandler()
        {
            InitializeComponent();
            Application.AddMessageFilter(this); //in order to perform mouse wheel on picturebox(panel), watch message of form
            //initiate color
            foreColor = Color.Black;
            backColor = Color.White;
            //initiate draw style
            cboDrawStyle.SelectedIndex = 0;
            cboLineWeight.SelectedIndex = 0;
            //panel scroll
            panelMain.AutoScroll = true;
            backColor = Color.White;
            //Tooltip
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnBackColor, "Background Color");
            tooltip.SetToolTip(btnForeColor, "Foreground Color");
            //Scale setter
            txtScale.AutoSize = false;
            txtScale.Text = "100.00";
            txtScale.Margin = new Padding(0,0, 0, 0);
            txtScale.KeyPress += new KeyPressEventHandler(txtScale_KeyPress);
            txtScale.LostFocus += new EventHandler(txtScale_LostFocus);
            txtScale.Visible = false;
            btnNarrow.Text = "–";
            btnNarrow.Width = 10;
            btnNarrow.Click += new EventHandler(btnNarrow_Click);
            btnNarrow.Visible = false;
            statusStrip.Items.Add(btnNarrow);
            statusStrip.Items.Add(txtScale);
            lblScale.Text = "%";
            lblScale.Visible = false;
            statusStrip.Items.Add(lblScale);
            btnEnlarge.Text = "+";
            btnEnlarge.Width = 5;
            btnEnlarge.Click += new EventHandler(btnEnlarge_Click);
            btnEnlarge.Visible = false;
            statusStrip.Items.Add(btnEnlarge);
            //Set scale factor
            scaleFactor = 1;

        }

        #endregion

        #region | Methods |

        public void openFile(string fileName)
        {
            img = new ImageHandler(fileName);
            initiateImage();
        }

        /// <summary>
        /// Open file with predifined window size
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        public void openFile(string fileName, int width, int height)
        {
            img = new ImageHandler(fileName);
            initiateImage(width,height);
        }

        private void setScaleFactor(float scaleFactor)
        {
            txtScale.LostFocus -= new EventHandler(txtScale_LostFocus);
            //Check scale factor
            float minimumFactor=(float)Math.Round(Math.Max(Math.Max(10F/img.Width,10F/img.Height),0.0001F),4,MidpointRounding.AwayFromZero);
            float maximumFactor=(float)Math.Round(Math.Min(9999F/img.Width,9999F/img.Height),0,MidpointRounding.AwayFromZero);
            if (scaleFactor > maximumFactor || scaleFactor < minimumFactor)
            {
                MessageBox.Show("Scale factor should between " + minimumFactor*100 + " % and " + maximumFactor*100+" %");
                if (scaleFactor > maximumFactor)
                    scaleFactor = maximumFactor;
                else
                    scaleFactor = minimumFactor;
            }
            else
                scaleFactor = (float)Math.Round(scaleFactor, 2, MidpointRounding.AwayFromZero);
            //Set
            this.scaleFactor = scaleFactor;
            //Feedback to display
            txtScale.Text = String.Format("{0:0.00}",this.scaleFactor*100);
            txtScale.LostFocus += new EventHandler(txtScale_LostFocus);
            updateImage();
            //Clear selection
            selectionMove = false;
            selectionPath.Reset();
        }

        private void initiateImage()
        {
            modified = false;
            //Backup initial image
            srcBitmap = (Bitmap)img.Bmp.Clone();
            //enable all function
            foreach (var item in menuMain.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menu = (ToolStripMenuItem)item;
                    for (int i = 0; i < menu.DropDownItems.Count; i++)
                    {
                        if (menu.DropDownItems[i] is ToolStripMenuItem)
                            menu.DropDownItems[i].Enabled = true;
                    }
                }
            }
            txtScale.Visible = true;
            lblScale.Visible = true;
            btnNarrow.Visible = true;
            btnEnlarge.Visible = true;
            //Find the best scale factor
            scaleFactor = Math.Min(panelMain.Width * 0.95F / img.Width, panelMain.Height * 0.95F / img.Height);
            txtScale.Text = String.Format("{0:#.00}", Math.Round(scaleFactor * 100, 2, MidpointRounding.AwayFromZero));
            updateImage();
            this.Text = "ImageHandler - " + img.Filename;
            //Clear selection
            selectionMove = false;
            selectionPath.Reset();
        }

        /// <summary>
        /// Initiate image with predefined windwo size
        /// </summary>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        private void initiateImage(int width, int height)
        {
            modified = false;
            //Backup initial image
            srcBitmap = (Bitmap)img.Bmp.Clone();
            //enable all function
            foreach (var item in menuMain.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menu = (ToolStripMenuItem)item;
                    for (int i = 0; i < menu.DropDownItems.Count; i++)
                    {
                        if (menu.DropDownItems[i] is ToolStripMenuItem)
                            menu.DropDownItems[i].Enabled = true;
                    }
                }
            }
            txtScale.Visible = true;
            lblScale.Visible = true;
            btnNarrow.Visible = true;
            btnEnlarge.Visible = true;
            //Find the best scale factor
            scaleFactor = Math.Min(width * 0.95F / img.Width, height * 0.95F / img.Height);
            txtScale.Text = String.Format("{0:#.00}", Math.Round(scaleFactor * 100, 2, MidpointRounding.AwayFromZero));
            updateImage();
            this.Text = "ImageHandler - " + img.Filename;
            //Clear selection
            selectionMove = false;
            selectionPath.Reset();
        }

        protected void loadImage()
        {
            //Avoid occupy resource
            using (Image bmpTemp = Image.FromFile(lastFilename))
            {
                thumbnailImage = new Bitmap(bmpTemp);
            }
            this.Invoke(new AssignImageDlgt(assignImage));
        }

        protected void assignImage()
        {
            picThumbnail.Width = 100;
            // 100 iWidth
            // ---- = ------
            // tHeight iHeight
            picThumbnail.Height = thumbnailImage.Height * 100 / thumbnailImage.Width;
            setThumbnailLocation(this.PointToClient(new Point(lastX, lastY)));
            picThumbnail.Image = thumbnailImage;
        }

        protected void setThumbnailLocation(Point p)
        {
            if (picThumbnail.Image == null)
            {
                picThumbnail.Visible = false;
            }
            else
            {
                //Click point is the center of thumbnail
                p.X -= picThumbnail.Width / 2;
                p.Y -= picThumbnail.Height / 2;
                picThumbnail.Location = p;
                picThumbnail.Visible = true;
                //picThumbnail.BringToFront();
            }
        }

        protected bool getFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileNameW") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = System.IO.Path.GetExtension(filename).ToLower();
                        if (allowedExt.Contains(ext))
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private void processingStart(bool showWaitBar,bool resetSelection)
        {
            this.Text += "*";
            modified = true;
            //Backup, but don't create backup during selection area move
            if (resetSelection)
            {
                if (backupBitmap != null)
                    backupBitmap.Dispose();
                backupBitmap = (Bitmap)img.Bmp.Clone();
                //Clear selection
                selectionMove = false;
                selectionPath.Reset();
                picMain.Invalidate();
                updateImage();
            }
            lblStatus.Text = "Processing...";
            if (showWaitBar)
            {
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Visible = false;
                pgbProcessing.Style = ProgressBarStyle.Marquee;
                pgbProcessing.Visible = true;
                Application.DoEvents();
            }
        }

        private void processingEnd()
        {
            //Implement selection area move
            if (selectionMove)
            {
                img.Bmp.Dispose();
                img.Bmp = (Bitmap)buffBitmap.Clone();
            }
            updateImage();
            lblStatus.Text = "Ready";
            lblStatus.Visible = true;
            pgbProcessing.Visible = false;
            this.Cursor = Cursors.Default;
        }

        private void updateImage()
        {
            if (picMain.Image != null)
                picMain.Image.Dispose();
            picMain.Image = new Bitmap((Bitmap)img.Bmp.Clone(),
                (int)(Math.Round(img.Width * scaleFactor,0,MidpointRounding.AwayFromZero)),
                (int)(Math.Round(img.Height * scaleFactor, 0, MidpointRounding.AwayFromZero)));
            //Set scroll bar
            panelMain.AutoScrollMinSize = new Size(picMain.Image.Width, picMain.Image.Height); 
            //Set picMain location
            if (picMain.Image.Width < panelMain.Width && picMain.Image.Height < panelMain.Height)
            {
                picMain.Dock = DockStyle.None;
                picMain.Left = (panelMain.Width - picMain.Width) / 2;
                picMain.Top = (panelMain.Height - picMain.Height) / 2;
            }
            else if (picMain.Image.Width < panelMain.Width)
            {
                picMain.Dock = DockStyle.None;
                picMain.Left = (panelMain.Width - picMain.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth) / 2;
                picMain.Top = panelMain.AutoScrollPosition.Y;
            }
            else if (picMain.Image.Height < panelMain.Height)
            {
                picMain.Dock = DockStyle.None;
                picMain.Left = panelMain.AutoScrollPosition.X;
                picMain.Top = (panelMain.Height - picMain.Height - System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight) / 2;
            }
            else
                picMain.Dock = DockStyle.Fill;
            picMain.Invalidate();
            lblStatus.Tag = "Ready";
            lblInfo.Text = img.Width + " x " + img.Height + "; " + String.Format("{0:0.00}",Math.Round(img.DPIX,2,MidpointRounding.AwayFromZero).ToString()) + " DPI ";
        }

        private Point unScale(Point scaledP)
        {
            return new Point((int)Math.Round(scaledP.X / scaleFactor, 0, MidpointRounding.AwayFromZero), (int)Math.Round(scaledP.Y / scaleFactor, 0, MidpointRounding.AwayFromZero));
        }

        private Rectangle unScale(Rectangle scaledRect)
        {
            return new Rectangle(
                (int)Math.Round(scaledRect.X / scaleFactor, 0, MidpointRounding.AwayFromZero),
                (int)Math.Round(scaledRect.Y / scaleFactor, 0, MidpointRounding.AwayFromZero),
                (int)Math.Round(scaledRect.Right / scaleFactor, 0, MidpointRounding.AwayFromZero) - (int)Math.Round(scaledRect.X / scaleFactor, 0, MidpointRounding.AwayFromZero),
                (int)Math.Round(scaledRect.Bottom / scaleFactor, 0, MidpointRounding.AwayFromZero) - (int)Math.Round(scaledRect.Y / scaleFactor, 0, MidpointRounding.AwayFromZero));
        }

        #endregion

        #region | Events |

        /// <summary>
        /// force mouse wheel message to be processed by picturebox(panel)
        /// </summary>
        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A; //mouse wheel message
            if (m.Msg == WM_MOUSEWHEEL && !mFiltering)
            {
                mFiltering = true;
                SendMessage(this.panelMain.Handle, m.Msg, m.WParam, m.LParam);
                m.Result = IntPtr.Zero; //do not pass to the parent window
                mFiltering = false;
                return true; //do not pass to focused control
            }
            else
                return false;
        }

        private void frmImageHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modified)
            {
                if (MessageBox.Show("File already modified, do you wanna to save?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    tsmSaveAs_Click(tsmSaveAs, new EventArgs());
            }
            Application.RemoveMessageFilter(this); //remove message filter
        }

        private void frmImageHandler_Resize(object sender, EventArgs e)
        {
            //Adjust the size of panalMain
            panelMain.Width = this.ClientSize.Width - panelMain.Left;
            panelMain.Height = this.ClientSize.Height - panelMain.Top - statusStrip.Height;
            if (picMain.Image != null)
                updateImage();
            //Adjust button position
            btnForeColor.Top = this.ClientRectangle.Height - 93;
            btnBackColor.Top = this.ClientRectangle.Height - 78;
        }

        private void frmImageHandler_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = getFilename(out filename, e);
            if (validData)
            {
                if (lastFilename != filename)
                {
                    picThumbnail.Image = null;
                    picThumbnail.Visible = false;
                    lastFilename = filename;
                    getImageThread = new Thread(new ThreadStart(loadImage));
                    getImageThread.Start();
                }
                else
                {
                    picThumbnail.Visible = true;
                }
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void frmImageHandler_DragOver(object sender, DragEventArgs e)
        {
            if (validData)
            {
                if ((e.X != lastX) || (e.Y != lastY))
                {
                    setThumbnailLocation(this.PointToClient(new Point(e.X, e.Y)));
                }
            }
        }

        private void frmImageHandler_DragLeave(object sender, EventArgs e)
        {
            picThumbnail.Visible = false;
        }

        private void frmImageHandler_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
            {
                while (getImageThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(0);
                }
                picThumbnail.Visible = false;
                img = new ImageHandler(lastFilename);
                initiateImage();
            }
        }

        private void tsmNew_Click(object sender, EventArgs e)
        {
            img = new ImageHandler(picMain.Width, picMain.Height);
            initiateImage();
        }

        private void tsmOpen_Click(object sender, EventArgs e)
        {
            if (modified)
            {
                if (MessageBox.Show("File already modified, do you wanna to save?","Warning",MessageBoxButtons.YesNo)==DialogResult.Yes)
                    tsmSaveAs_Click(tsmSaveAs, new EventArgs());
            }
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = @"Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg,*.jpeg)|*.jpg;*.jpeg|GIF(*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif;*.tiff
                |All supported format(*.bmp,*.jpg,*.jpeg,*.gif,*.png,*.tif,*.tiff)|*.bmp;*.jpg;*.gif;*.png;*.jpeg;*.tif;*.tiff";
                ofd.FilterIndex = 6;
                ofd.RestoreDirectory = true;
                if (DialogResult.OK == ofd.ShowDialog())
                {
                    img = new ImageHandler(ofd.FileName);
                    initiateImage();
                }
            }
        }

        private void tsmSave_Click(object sender, EventArgs e)
        {
            if (img.isDraft())
                tsmSaveAs_Click(tsmSaveAs,new EventArgs());
            else
            {
                //Implement selection move
                if (selectionMove)
                {
                    using (Graphics gr = Graphics.FromImage(img.Bmp))
                    {
                        if (selectionFromSelf)
                            gr.FillRectangle(new SolidBrush(backColor), unScale(selectionRect));
                        gr.DrawImage(selectBitmap, unScale(selectionRect), 0, 0, selectBitmap.Width, selectBitmap.Height, GraphicsUnit.Pixel);
                    }
                    updateImage();
                }
                img.Bmp.SetResolution(img.DPIX, img.DPIY);
                img.Bmp.Save(img.Filename, img.Bmp.RawFormat);
                img = new ImageHandler(img.Filename);
                initiateImage();
            }
        }

        private void tsmSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = @"Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg,*.jpeg)|*.jpg;*.jpeg|GIF(*.gif)|*.gif|PNG(*.png)|*.png|TIFF(*.tif,*.tiff)|*.tif;*.tiff";
                sfd.RestoreDirectory = true;
                if (img.isDraft())
                {
                    sfd.FileName = img.Filename;
                    sfd.DefaultExt = ".jpg";
                    sfd.FilterIndex = 2;
                }
                else
                {
                    sfd.FileName = img.ImgInfo.Name; //default name (original name)
                    sfd.DefaultExt = img.ImgInfo.Extension; //default extension
                    switch (img.ImgInfo.Extension.ToLower())
                    {
                        case ".bmp":
                            sfd.FilterIndex = 1;
                            break;
                        case ".jpg":
                        case ".jpeg":
                            sfd.FilterIndex = 2;
                            break;
                        case ".gif":
                            sfd.FilterIndex = 3;
                            break;
                        case ".png":
                            sfd.FilterIndex = 4;
                            break;
                        case ".tif":
                        case ".tiff":
                            sfd.FilterIndex = 5;
                            break;
                        default:
                            sfd.FilterIndex = 2;
                            break;
                    }
                }
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                        ImageFormat format = ImageFormat.Jpeg;
                        switch (ext)
                        {
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                            case ".jpg":
                            case ".jpeg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".gif":
                                format = ImageFormat.Gif;
                                break;
                            case ".png":
                                format = ImageFormat.Png;
                                break;
                            case ".tif":
                            case ".tiff":
                                format = ImageFormat.Tiff;
                                break;
                            default:
                                MessageBox.Show("Invalid format.", "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                        }
                        //Implement selection move
                        if (selectionMove)
                        {
                            using (Graphics gr = Graphics.FromImage(img.Bmp))
                            {
                                if (selectionFromSelf)
                                    gr.FillRectangle(new SolidBrush(backColor), unScale(selectionRect));
                                gr.DrawImage(selectBitmap, unScale(selectionRect), 0, 0, selectBitmap.Width, selectBitmap.Height, GraphicsUnit.Pixel);
                            }
                            updateImage();
                        }
                        img.Bmp.SetResolution(img.DPIX, img.DPIY);
                        img.Bmp.Save(sfd.FileName, format);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.StackTrace + ": " + ex.Message, "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //MessageBox.Show("Save file success.", "ImageHandler", MessageBoxButtons.OK);
                    img = new ImageHandler(sfd.FileName);
                    srcBitmap = (Bitmap)img.Bmp.Clone();
                    updateImage();
                    this.Text = "ImageHandler - " + img.Filename;
                }
            }
        }

        private void tsmReload_Click(object sender, EventArgs e)
        {
            img.Bmp = (Bitmap)srcBitmap.Clone();
            updateImage();
            //Clear selection
            selectionMove = false;
            selectionPath.Reset();
        }

        private void tsmToGray_Click(object sender, EventArgs e)
        {
            processingStart(true, true);
            img.ToGray();
            processingEnd();
        }

        private void tsmRotate_Click(object sender, EventArgs e)
        {

            float angle;
            if (!float.TryParse(Interaction.InputBox("Please input the angle (clockwise): "), out angle))
            {
                MessageBox.Show("Not an angle.", "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processingStart(true, true); ;
            angle = angle % 360;
            img.Rotate(angle);
            processingEnd();
        }

        private void tsmBrightness_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            int brightness;
            if (!int.TryParse(Interaction.InputBox("Please input the angle: "), out brightness))
            {
                MessageBox.Show("Please input an integer.", "ImageHandler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            processingStart(true, true);
            img.ChangeBrightness(brightness);
            processingEnd();
        }

        private void tsmResize_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            using (ResizeDialog nDialog = new ResizeDialog(img.Width, img.Height))
            {
                nDialog.StartPosition = FormStartPosition.CenterParent;
                if (nDialog.ShowDialog() == DialogResult.OK)
                {
                    processingStart(true, true);
                    img.Resize(nDialog.NewWidth, nDialog.NewHeight);
                }
                else
                    return;
            }
            processingEnd();
        }

        private void tsmUndo_Click(object sender, EventArgs e)
        {
            if (backupBitmap != null)
            {
                img.Bmp = (Bitmap)backupBitmap.Clone();
                img.Width = img.Bmp.Width;
                img.Height = img.Bmp.Height;
                updateImage();
                selectionMove = false;
            }
        }

        private void tsmCut_Click(object sender, EventArgs e)
        {
            if (backupBitmap != null)
                backupBitmap.Dispose();
            backupBitmap = (Bitmap)img.Bmp.Clone();
            if (selectBitmap != null)
                selectBitmap.Dispose();
            if (selectionRect.Width > 0 && selectionRect.Height > 0)
                selectBitmap = img.Bmp.Clone(unScale(selectionRect), PixelFormat.Format32bppArgb);
            else
                selectBitmap = img.Bmp; //Select all
            Clipboard.Clear();
            Clipboard.SetImage((Bitmap)selectBitmap.Clone());
            using (Graphics gr = Graphics.FromImage(img.Bmp))
            {
                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                    gr.FillRectangle(new SolidBrush(backColor), unScale(selectionRect));
                else
                    gr.FillRectangle(new SolidBrush(backColor), 0,0,img.Width,img.Height);
            }
            updateImage();
        }

        private void tsmCopy_Click(object sender, EventArgs e)
        {
            if (selectBitmap != null)
                selectBitmap.Dispose();
            if (selectionRect.Width > 0 && selectionRect.Height > 0)
                selectBitmap = img.Bmp.Clone(unScale(selectionRect), PixelFormat.Format32bppArgb);
            else
                selectBitmap = img.Bmp; //Select all
            Clipboard.Clear();
            Clipboard.SetImage((Bitmap)selectBitmap.Clone());
        }

        private void tsmCutImage_Click(object sender, EventArgs e)
        {
            processingStart(true, false);
            if (selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                img.Bmp = (Bitmap)selectBitmap.Clone();
                img.Width = selectBitmap.Width;
                img.Height = selectBitmap.Height;
                //Clear selection
                selectionMove = false;
                selectionPath.Reset();
                picMain.Invalidate();
            }
            processingEnd();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutDialog = new frmAbout();
            aboutDialog.ShowDialog();
        }

        private void tsmPaste_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
                return;
            Image clipImg = Clipboard.GetImage();
            if (selectBitmap != null)
                selectBitmap.Dispose();
            selectBitmap = (Bitmap)clipImg.Clone();
            //Define selection path
            selectionPath.Reset();
            selectionPath.AddLine(new Point(0, 0), new Point(selectBitmap.Width, selectBitmap.Height));
            movingCenter = new Point(selectBitmap.Width / 2, selectBitmap.Height / 2);
            selectionMove = true;
            selectionFromSelf = false;
            picMain.Invalidate();
        }

        private void picMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isMouseDown && img != null)
            {
                isMouseDown = true;
                if (btnSelect.Checked && selectionRect.Width != 0 && selectionRect.Height != 0 && selectionRect.Contains(new Point(e.X, e.Y)))
                {
                    processingStart(false,false);
                    selectionMove = true; //Enable selection move
                    clickPointOffset = new Point(e.X - selectionRect.Left - selectionRect.Width / 2, e.Y - selectionRect.Top - selectionRect.Height / 2);
                }
                else
                {
                    processingStart(false, true);
                    //Rest selection
                    selectionMove = false;
                    selectionPath.Reset();
                    unScaledPoints.Clear();
                    scaledPoints.Clear();
                    scaledPoints.Add(new Point(e.X, e.Y));
                    unScaledPoints.Add(unScale(new Point(e.X, e.Y)));
                    picMain.Invalidate();
                }
            }
        }

        private void picMain_MouseMove(object sender, MouseEventArgs e)
        {
            //Set cursor
            if (btnDraw.Checked)
            {
                switch (cboDrawStyle.SelectedIndex)
                {
                    case 0:
                        this.Cursor = new Cursor(new MemoryStream(Properties.Resources.pencil));
                        break;
                    case 1:
                    case 2:
                    case 3:
                        this.Cursor = Cursors.Cross;
                        break;
                }
            }
            else if (selectionRect.Height != 0 && selectionRect.Contains(new Point(e.X, e.Y)))
                this.Cursor = Cursors.SizeAll;
            else if (btnSelect.Checked)
                this.Cursor = Cursors.Cross;
            else
                this.Cursor = Cursors.Default;
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                if (selectionMove)
                {
                    movingCenter = new Point(e.X-clickPointOffset.X, e.Y-clickPointOffset.Y);
                    picMain.Invalidate();
                }
                else
                {
                    scaledPoints.Add(new Point(e.X, e.Y));
                    unScaledPoints.Add(unScale(new Point(e.X, e.Y)));
                    if (btnDraw.Checked) //draw
                    {
                        GC.Collect();
                        if (buffBitmap != null)
                            buffBitmap.Dispose();
                        buffBitmap = (Bitmap)backupBitmap.Clone();
                        using (Graphics gr = Graphics.FromImage(buffBitmap))
                        {
                            SolidBrush brush = new SolidBrush(foreColor);
                            Pen pen = new Pen(brush, float.Parse(cboLineWeight.SelectedItem.ToString()));
                            switch (cboDrawStyle.SelectedIndex)
                            {
                                case 0: //pen
                                    for (int i = 0; i < unScaledPoints.Count - 1; i++)
                                        gr.DrawLine(pen, unScaledPoints[i], unScaledPoints[i + 1]);
                                    break;
                                case 1: //line
                                    if (shiftKeyDown)
                                    {
                                        if (unScaledPoints[unScaledPoints.Count - 1].X - unScaledPoints[0].X > unScaledPoints[unScaledPoints.Count - 1].Y - unScaledPoints[0].Y)
                                            gr.DrawLine(pen, unScaledPoints[0], new Point(unScaledPoints[unScaledPoints.Count - 1].X,unScaledPoints[0].Y));
                                        else
                                            gr.DrawLine(pen, unScaledPoints[0], new Point(unScaledPoints[0].X, unScaledPoints[unScaledPoints.Count - 1].Y));
                                    }
                                    else
                                        gr.DrawLine(pen, unScaledPoints[0], unScaledPoints[unScaledPoints.Count - 1]);
                                    break;
                                case 2: //Rectangle
                                case 3: //Circle
                                    if (shiftKeyDown)
                                    {
                                        int length = Math.Min(
                                            Math.Abs(unScaledPoints[unScaledPoints.Count - 1].X - unScaledPoints[0].X),
                                            Math.Abs(unScaledPoints[unScaledPoints.Count - 1].Y - unScaledPoints[0].Y));
                                        int x0, y0;
                                        if (unScaledPoints[0].X < unScaledPoints[unScaledPoints.Count - 1].X)
                                            x0 = unScaledPoints[0].X;
                                        else
                                            x0 = Math.Max(unScaledPoints[unScaledPoints.Count - 1].X, unScaledPoints[0].X - length);
                                        if (unScaledPoints[0].Y < unScaledPoints[unScaledPoints.Count - 1].Y)
                                            y0 = unScaledPoints[0].Y;
                                        else
                                            y0 = Math.Max(unScaledPoints[unScaledPoints.Count - 1].Y, unScaledPoints[0].Y - length);
                                        if (cboDrawStyle.SelectedIndex == 2)
                                            gr.DrawRectangle(pen, new Rectangle(x0, y0, length, length));
                                        else
                                            gr.DrawEllipse(pen, new Rectangle(x0, y0, length, length));
                                    }
                                    else
                                    {
                                        if (cboDrawStyle.SelectedIndex == 2)
                                            gr.DrawRectangle(pen, new Rectangle(Math.Min(unScaledPoints[0].X, unScaledPoints[unScaledPoints.Count - 1].X),
                                                Math.Min(unScaledPoints[0].Y, unScaledPoints[unScaledPoints.Count - 1].Y),
                                                Math.Abs(unScaledPoints[unScaledPoints.Count - 1].X - unScaledPoints[0].X),
                                                Math.Abs(unScaledPoints[unScaledPoints.Count - 1].Y - unScaledPoints[0].Y)));
                                        else
                                            gr.DrawEllipse(pen, new Rectangle(Math.Min(unScaledPoints[0].X, unScaledPoints[unScaledPoints.Count - 1].X),
                                                Math.Min(unScaledPoints[0].Y, unScaledPoints[unScaledPoints.Count - 1].Y),
                                                Math.Abs(unScaledPoints[unScaledPoints.Count - 1].X - unScaledPoints[0].X),
                                                Math.Abs(unScaledPoints[unScaledPoints.Count - 1].Y - unScaledPoints[0].Y)));
                                    }
                                    break;
                            }
                            if (picMain.Image != null)
                                picMain.Image.Dispose(); //release current buff bitmap
                            picMain.Image = new Bitmap((Bitmap)buffBitmap.Clone(),
                                (int)(Math.Round(img.Width * scaleFactor, 0, MidpointRounding.AwayFromZero)),
                                (int)(Math.Round(img.Height * scaleFactor, 0, MidpointRounding.AwayFromZero)));
                            pen.Dispose();
                            brush.Dispose();
                        }
                    }
                    if (btnSelect.Checked)
                    {
                        selectionPath.Reset();
                        //Modift points which out of bound
                        for (int i = 0; i < unScaledPoints.Count; i++)
                        {
                            if (unScaledPoints[i].X < 0)
                                unScaledPoints[i] = new Point(0, unScaledPoints[i].Y);
                            if (unScaledPoints[i].Y < 0)
                                unScaledPoints[i] = new Point(unScaledPoints[i].X, 0);
                            if (unScaledPoints[i].X > img.Width - 1)
                                unScaledPoints[i] = new Point(img.Width - 1, unScaledPoints[i].Y);
                            if (unScaledPoints[i].Y > img.Height - 1)
                                unScaledPoints[i] = new Point(unScaledPoints[i].X, img.Height - 1);
                        }
                        for (int i = 0; i < scaledPoints.Count; i++)
                        {
                            if (scaledPoints[i].X < 0)
                                scaledPoints[i] = new Point(0, scaledPoints[i].Y);
                            if (scaledPoints[i].Y < 0)
                                scaledPoints[i] = new Point(scaledPoints[i].X, 0);
                            if (scaledPoints[i].X > picMain.Width - 1)
                                scaledPoints[i] = new Point(picMain.Width - 1, scaledPoints[i].Y);
                            if (scaledPoints[i].Y > picMain.Height - 1)
                                scaledPoints[i] = new Point(scaledPoints[i].X, picMain.Height - 1);
                        }
                        //diagonal line of selction
                        selectionPath.AddLine(scaledPoints[0], scaledPoints[scaledPoints.Count - 1]);
                        selectionFromSelf = true;
                        picMain.Invalidate();
                    }
                }
            }
        }

        private void picMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                isMouseDown = false;
                if (btnDraw.Checked)
                {
                    img.Bmp.Dispose();
                    img.Bmp = (Bitmap)buffBitmap.Clone();
                    updateImage();
                }
                if (btnSelect.Checked && selectionRect.Width != 0 && selectionRect.Height != 0 && !selectionMove) //Has selection area
                {
                    //Create selection image (not show selction picturebox now)
                    if (selectBitmap != null)
                        selectBitmap.Dispose();
                    selectBitmap = img.Bmp.Clone(unScale(selectionRect), PixelFormat.Format32bppArgb);
                }
                processingEnd();
            }
        }

        private void picMain_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void picMain_Paint(object sender, PaintEventArgs e)
        {
            if (img != null && !(selectionRect = Rectangle.Round(new Region(selectionPath).GetBounds(e.Graphics))).IsEmpty)
            {
                Debug.WriteLine(DateTime.Now.ToString()+":Paint");
                if (selectionRect.Width != 0 && selectionRect.Height != 0)
                {
                    if (selectionMove)
                    {
                        GC.Collect();
                        //Create bufferBitmap, which using for display selction area moving
                        if (buffBitmap != null)
                            buffBitmap.Dispose();
                        buffBitmap = (Bitmap)backupBitmap.Clone();
                        if (selectionFromSelf)
                        {
                            using (Graphics gr = Graphics.FromImage(buffBitmap))
                                gr.FillRectangle(new SolidBrush(backColor), unScale(selectionRect));
                        }
                        //Calcd. new rectangle after move
                        selectionRect = new Rectangle(
                            movingCenter.X - selectionRect.Width / 2,
                            movingCenter.Y - selectionRect.Height / 2,
                            selectionRect.Width,
                            selectionRect.Height);
                        using (Graphics gr = Graphics.FromImage(buffBitmap))
                            gr.DrawImage(selectBitmap, unScale(selectionRect), 0, 0, selectBitmap.Width, selectBitmap.Height, GraphicsUnit.Pixel);
                        if (picMain.Image != null)
                            picMain.Image.Dispose(); //release current buff bitmap
                        picMain.Image = new Bitmap((Bitmap)buffBitmap.Clone(),
                            (int)(Math.Round(img.Width * scaleFactor, 0, MidpointRounding.AwayFromZero)),
                            (int)(Math.Round(img.Height * scaleFactor, 0, MidpointRounding.AwayFromZero)));
                    }
                    Pen pen = new Pen(Color.Black);
                    pen.DashPattern = new float[] { 5.0F, 2.0F }; //dash line
                    e.Graphics.DrawRectangle(pen, selectionRect);
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (btnDraw.Checked)
                btnDraw.Checked = false;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (btnSelect.Checked)
                btnSelect.Checked = false;
        }

        private void btnForeColor_Paint(object sender, PaintEventArgs e)
        {
            using (Pen p = new Pen(foreColor))
            {
                e.Graphics.FillRectangle(p.Brush, ClientRectangle);
            }
            using (Pen p = new Pen(Color.Black, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            }
        }

        private void btnBackColor_Paint(object sender, PaintEventArgs e)
        {
            using (Pen p = new Pen(backColor))
            {
                e.Graphics.FillRectangle(p.Brush, ClientRectangle);
            }
            using (Pen p = new Pen(Color.Black, 1))
            {
                e.Graphics.DrawRectangle(p, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                foreColor = colorDialog1.Color;
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                backColor = colorDialog1.Color;
        }

        private void txtScale_LostFocus(object sender, EventArgs e)
        {
            setScaleFactor(float.Parse(txtScale.Text) / 100);
        }

        private void txtScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //Enter
            {
                setScaleFactor(float.Parse(txtScale.Text) / 100);
                e.Handled = true;
            }
        }

        private void btnEnlarge_Click(object sender, EventArgs e)
        {
            setScaleFactor(scaleFactor + 0.1F);
        }

        private void btnNarrow_Click(object sender, EventArgs e)
        {
            setScaleFactor(scaleFactor - 0.1F);
        }

        #endregion

        private void frmImageHandler_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    shiftKeyDown = true;
                    break;
                case Keys.Add:
                    setScaleFactor(scaleFactor + 0.1F);
                    break;
                case Keys.Subtract:
                    setScaleFactor(scaleFactor - 0.1F);
                    break;
            }
        }

        private void frmImageHandler_KeyUp(object sender, KeyEventArgs e)
        {
            shiftKeyDown = false;
        }
    }
}


