using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ComponentsLibrary.CustomText.Designers;

namespace ComponentsLibrary.CustomText {
    [DesignerAttribute(typeof(CustomTextDesigner))]
    //[DesignerAttribute("ComponentsLibrary.CustomText.Designers.CustomTextDesigner")]
    [DefaultEvent("OnChangeAlignment")]
    public class CustomText : Control {
        private ContentAlignment alignmentValue = ContentAlignment.MiddleLeft;
        [Category("Alignment"), Description("Specifies the alignment of text.")]
        public ContentAlignment TextAlignment {
            get { return alignmentValue; }
            set {
                if (alignmentValue != value && onChangeAlignment != null) //проверка для того чтобы предотвратить лишние вызовы собития
                    onChangeAlignment(this, new EventArgs()); //вызов нового события
                alignmentValue = value;
                //Перерисовка компонента при изменении значения
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            StringFormat style = new StringFormat();
            style.Alignment = StringAlignment.Near;
            switch (alignmentValue) {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                    style.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                case ContentAlignment.TopRight:
                    style.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.TopCenter:
                    style.Alignment = StringAlignment.Center;
                    break;
            }
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), ClientRectangle, style);
        }
        private event EventHandler onChangeAlignment;
        [Category("Alignment"), Description("Event on change alignment")]
        public event EventHandler OnChangeAlignment {
            add {
                onChangeAlignment += value;
            }
            remove {
                onChangeAlignment -= value;
            }
        }
    }
}
