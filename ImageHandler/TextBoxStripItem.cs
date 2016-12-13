using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ImageHandler
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                           ToolStripItemDesignerAvailability.ContextMenuStrip |
                           ToolStripItemDesignerAvailability.StatusStrip)]
    public class TextBoxStripItem : MyCustomToolStripControlHost
    {
        private TextBox textBox;

        public TextBoxStripItem()
            : base(new TextBox())
        {
            textBox = this.Control as TextBox;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.Font = new System.Drawing.Font("Times New Roman", 10);
            textBox.Width = 60;
            textBox.KeyPress += new KeyPressEventHandler(keyPress);
        }

        private void keyPress(object sender, KeyPressEventArgs e)
        {
            //only allow integer (no decimal point)
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != 8))
                e.Handled = true;
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }
    }

    public class MyCustomToolStripControlHost : ToolStripControlHost
    {
        public MyCustomToolStripControlHost()
            : base(new Control())
        {
        }
        public MyCustomToolStripControlHost(Control c)
            : base(c)
        {
        }
    }
}
