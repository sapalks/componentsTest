using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ComponentsLibrary.TestControl {
    public class TestControl : Control {
        //...
        private int customProperty;
        [Category("CustomCategory"),
            Description("Description of CustomProperty.")]
        public int CustomProperty {
            get { return customProperty; }
            set {
                customProperty = value;
                Invalidate();
            }
        }
        //...
        private event EventHandler customEvent;
        public event EventHandler CustomEvent {
            add { customEvent += value; }
            remove { customEvent -= value; }
        }
        //...
        public void Log() {
            Console.WriteLine("current value: {0}", this.customProperty);
            number = 5;
            // number = 5
            // sqrt = 0
            GetPropertyByName("Number").SetValue(this, 5);
            // number = 5
            // sqrt = 25

        }
        //...

        private int number;
        public int Number {
            get { return number; }
            set { number = value; }
        }
        private int sqrt;
        public int Sqrt {
            get { return sqrt; }
            private set { sqrt = (int)Math.Pow(number, 2); }
        }

        private PropertyDescriptor GetPropertyByName(string propName) {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(this)[propName];
            if (prop == null)
                throw new ArgumentException("Свойство не существует", propName);
            return prop;
        }

    }

    public class Visual : Control { }

    public class NoVisual : Component { }
}
