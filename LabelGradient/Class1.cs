using System.Windows.Markup;
using System.Drawing;
using System.Windows.Forms;

namespace LabelGradient {
    public class GradientLB:Label {
        private Color startColor = Color.DarkBlue;
        private Color endColor = Color.Red;
        public Color StartColor => startColor;
        public Color EndColor => endColor;

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Color color1 = Color.FromArgb(100, startColor);
            Color color2 = Color.FromArgb(100, endColor);
            Brush brush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, color1, color2, 10);
            e.Graphics.FillRectangle(brush, ClientRectangle);
            brush.Dispose();
        }

        public GradientLB() {
            //InitializeComponent();
        }
    }
}
