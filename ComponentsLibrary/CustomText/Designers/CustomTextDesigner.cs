﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design;

namespace ComponentsLibrary.CustomText.Designers {
    public class CustomTextDesigner : ControlDesigner {
        IMenuCommandService mcs;
        MenuCommand ms;
        DesignerVerb dv;
        public override void Initialize(IComponent component) {
            base.Initialize(component);
            mcs = (IMenuCommandService)component.Site.GetService(typeof(IMenuCommandService));

            dv = new DesignerVerb("Global menu", 
                new EventHandler(OnClickGlobalMenu));
            dv.Enabled = true;
            dv.Visible = true;
            dv.Supported = true;
            mcs.RemoveVerb(dv);
            mcs.AddVerb(dv);

            ms = new MenuCommand(new EventHandler(OnKeyHome),
                MenuCommands.KeyHome);
            ms.Enabled = true;
            ms.Visible = true;
            ms.Supported = true;
            mcs.RemoveCommand(ms);
            mcs.AddCommand(ms);
        }

        [DesignerSerializationVisibility
            (DesignerSerializationVisibility.Hidden),
        Description("Виртуальное свойство")]
        public ContentAlignment VirtualProp {
            get { return (Component as CustomText).TextAlignment; }
            set { GetPropertyByName("TextAlignment").SetValue(Component, value); }
        }

        protected override void PreFilterEvents(IDictionary events) {
            base.PreFilterEvents(events);
        }

        protected override void PreFilterProperties(IDictionary properties) {
            base.PreFilterProperties(properties);
            properties.Remove("TextAlignment");
            PropertyDescriptor oldDesc = TypeDescriptor.GetProperties(this)["VirtualProp"];
            Attribute[] attr = new Attribute[oldDesc.Attributes.Count];
            oldDesc.Attributes.CopyTo(attr, 0);
            PropertyDescriptor virtualPropDescriptor
                = TypeDescriptor.CreateProperty
                (GetType(), "VirtualProp", typeof(ContentAlignment), attr);
            properties["VirtualProp"] = virtualPropDescriptor;
        }

        private DesignerVerbCollection verbs;
        public override DesignerVerbCollection Verbs {
            get {
                if (verbs == null) {
                    verbs = new TestVerbCollection(Component);
                }
                return verbs;
            }
        }

        private DesignerActionListCollection actionListCollection;
        public override DesignerActionListCollection ActionLists {
            get {
                if (actionListCollection == null) {
                    actionListCollection = new DesignerActionListCollection();
                    actionListCollection.Add(new TestActionList(this.Component));
                }
                return actionListCollection;
            }
        }

        private void OnClickGlobalMenu(object sender, EventArgs args) {
            System.Windows.Forms.MessageBox.Show("click 'Global menu'");
        }
        private void OnKeyHome(object sender, EventArgs args) {
            System.Windows.Forms.MessageBox.Show("key 'HOME'");
        }

        private PropertyDescriptor GetPropertyByName(string propName) {
            PropertyDescriptor prop = TypeDescriptor.GetProperties((Component as CustomText))[propName];
            if (prop == null)
                throw new ArgumentException("Свойство не существует", propName);
            return prop;
        }
    }

    public class TestVerbCollection : DesignerVerbCollection {
        private CustomText customText;
        public TestVerbCollection(IComponent component) {
            customText = component as CustomText;
            Add(new DesignerVerb("Обратная привязка", new EventHandler(InverceAlignment)));
        }

        private void InverceAlignment(object sender, EventArgs e) {
            var textAlignmentProperty = GetPropertyByName("TextAlignment");
            ContentAlignment textAlignment = (ContentAlignment)(textAlignmentProperty.GetValue(customText));
            switch (textAlignment) {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                    textAlignmentProperty.SetValue(customText, ContentAlignment.MiddleRight);
                    break;
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                case ContentAlignment.TopRight:
                    textAlignmentProperty.SetValue(customText, ContentAlignment.MiddleLeft);
                    break;
            }
        }

        private PropertyDescriptor GetPropertyByName(string propName) {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(customText)[propName];
            if (prop == null)
                throw new ArgumentException("Свойство не существует", propName);
            return prop;
        }
    }
}
