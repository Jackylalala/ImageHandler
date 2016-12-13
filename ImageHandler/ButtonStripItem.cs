using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ImageHandler
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                           ToolStripItemDesignerAvailability.ContextMenuStrip |
                           ToolStripItemDesignerAvailability.StatusStrip)]
    public class ButtonStripItem : MyCustomToolStripControlHost
    {
        private Button button;

        public ButtonStripItem () : base (new Button())
        {
            button = this.Control as Button;
            button.AutoSize = true;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            button.Font = new System.Drawing.Font("Calibri", 8,System.Drawing.FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Paint += button_Paint;
        }

        private void button_Paint(object sender, PaintEventArgs e)
        {
            //GraphicsPath grPath = new GraphicsPath();
            //grPath.AddEllipse(new Rectangle(1, 1, button.ClientSize.Width-2, button.ClientSize.Height-2));
            //button.Region = new System.Drawing.Region(grPath);
        }
    }
}
