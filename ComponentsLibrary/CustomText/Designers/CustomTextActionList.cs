using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace ComponentsLibrary.CustomText.Designers {
    public class TestActionList : DesignerActionList {
        private CustomText customText;
        private DesignerActionUIService service;
        //конструктор
        public TestActionList(IComponent component) : base(component) {
            //сохраняем ссылку на редактируемый компонент
            customText = component as CustomText;
            service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }
        public ContentAlignment TextAlignment {
            get { return customText.TextAlignment; }
            set { GetPropertyByName("TextAlignment").SetValue(customText, value); }
        }
        public void InverceAlignment() {
            switch (customText.TextAlignment) {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                    customText.TextAlignment = ContentAlignment.MiddleRight;
                    break;
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                case ContentAlignment.TopRight:
                    customText.TextAlignment = ContentAlignment.MiddleLeft;
                    break;
            }
            //обновляем диалог тега
            service.Refresh(customText);
        }

        public override DesignerActionItemCollection GetSortedActionItems() {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            //добавляем группы
            items.Add(new DesignerActionHeaderItem("Свойства", "Properties"));
            items.Add(new DesignerActionHeaderItem("Методы", "Methods"));
            items.Add(new DesignerActionHeaderItem("Информация", "Info"));
            //описываем свойство
            items.Add(new DesignerActionPropertyItem("TextAlignment", "Выравнивание", "Properties"));
            //описываем метод
            items.Add(new DesignerActionMethodItem
                (this, "InverceAlignment", "Обратная привязка", "Methods"));
            //добавляем произвольное описание
            items.Add(new DesignerActionTextItem("Оно действительно работает!!", "Info"));
            return items;
        }

        private PropertyDescriptor GetPropertyByName(string propName) {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(customText)[propName];
            if (prop == null)
                throw new ArgumentException("Свойство не существует", propName);
            return prop;
        }
    }
}
