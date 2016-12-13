using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageHandler
{
    public partial class ResizeDialog : Form
    {
        #region | Fields |

        private int srcWidth;
        private int srcHeight;

        #endregion

        #region | Constructors |

        public ResizeDialog(int width, int height)
        {
            InitializeComponent();
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            cboWidth.SelectedIndex = 0;
            cboHeight.SelectedIndex = 0;
            srcWidth = width;
            srcHeight = height;
            txtWidth.Text = srcWidth.ToString();
            txtHeight.Text = srcHeight.ToString();
            ContextMenu blankContextMenu = new ContextMenu(); //blnk context menu
            txtWidth.ContextMenu = blankContextMenu;
            txtHeight.ContextMenu = blankContextMenu;
            txtHeight.LostFocus += new EventHandler(this.txtHeight_LostFocus);
            txtWidth.LostFocus += new EventHandler(this.txtWidth_LostFocus);
            chkRatio.Checked = true;
        }

        #endregion

        #region | Properties |

        public int NewWidth
        {
            get
            {
                int temp;
                if (int.TryParse(txtWidth.Text, out temp))
                {
                    if (cboWidth.SelectedIndex == 1 && temp > 0)
                        return srcWidth * temp / 100;
                    else
                        return temp;
                }
                return 0;
            }
        }

        public int NewHeight
        {
            get
            {
                int temp;
                if (int.TryParse(txtHeight.Text, out temp))
                {
                    if (cboHeight.SelectedIndex == 1 && temp > 0)
                        return srcHeight * temp / 100;
                    else
                        return temp;
                }
                return 0;
            }
        }

        #endregion

        #region | Events |

        private void cboHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboWidth.SelectedIndex = cboHeight.SelectedIndex;
            if (cboHeight.SelectedIndex == 0)
                txtHeight.Text = srcHeight.ToString();
            else
                txtHeight.Text = "100";
        }

        private void cboWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboHeight.SelectedIndex = cboWidth.SelectedIndex;
            if (cboWidth.SelectedIndex == 0)
                txtWidth.Text = srcWidth.ToString();
            else
                txtWidth.Text = "100";
        }

        private void txtWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow integer (no decimal point)
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != 8))
                e.Handled = true;
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow integer (no decimal point)
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != 8))
                e.Handled = true;
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtHeight_LostFocus(object sender, EventArgs e)
        {
            if (chkRatio.Checked)
            {
                if (cboHeight.SelectedIndex == 0) //pixels
                    txtWidth.Text = (srcWidth * int.Parse(txtHeight.Text) / srcHeight).ToString();
                else
                    txtWidth.Text = txtHeight.Text;
            }
        }

        private void txtWidth_LostFocus(object sender, EventArgs e)
        {
            if (chkRatio.Checked)
            {
                if (cboHeight.SelectedIndex == 0) //pixels
                    txtHeight.Text = (srcHeight * int.Parse(txtWidth.Text) / srcWidth).ToString();
                else
                    txtHeight.Text = txtWidth.Text;
            }
        }
        #endregion
    }
}