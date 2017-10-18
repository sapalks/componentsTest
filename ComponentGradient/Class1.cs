using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace ComponentGradient {
    public partial class GradientLabel : Label {
        private Color startColor = Color.FromArgb(255, 255, 0, 0);
        private Color endColor = Color.FromArgb(255, 0, 0, 255);
        public Color StartColor => startColor;
        public Color EndColor => endColor;

        protected override void OnPain
    }
}
