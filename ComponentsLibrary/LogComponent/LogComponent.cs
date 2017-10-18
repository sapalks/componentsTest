using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComponentsLibrary.LogComponent {
    public class LogComponent : UserControl {
        //Буфер для отображения сообщений
        private ListBox logBox;
        //Конструктор
        public LogComponent() {
            InitializeComponent();
        }
        private void InitializeComponent() {
            this.logBox = new ListBox();
            this.SuspendLayout();

            logBox.Dock = DockStyle.Fill;
            logBox.Location = new Point(0, 0);
            logBox.Name = "logList";
            logBox.Size = new Size(200, 95);
            logBox.TabIndex = 0;

            Controls.Add(logBox);
            Name = "LogComponent";
            Size = new Size(200, 100);
            ResumeLayout(false);
        }
        public override ISite Site {
            get => base.Site;
            set {
                ClearChangeNotification();
                base.Site = value;
                RegisterChangeNotification();
            }
        }
        //Удаление обработчиков событий
        private void ClearChangeNotification() {
            IComponentChangeService changeService =
                (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (changeService != null) {
                changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                changeService.ComponentChanging -= new ComponentChangingEventHandler(OnComponentChanging);
                changeService.ComponentAdded -= new ComponentEventHandler(OnComponentAdded);
                changeService.ComponentAdding -= new ComponentEventHandler(OnComponentAdding);
                changeService.ComponentRemoved -= new ComponentEventHandler(OnComponentRemoved);
                changeService.ComponentRemoving-= new ComponentEventHandler(OnComponentRemoving);
                changeService.ComponentRename -= new ComponentRenameEventHandler(OnComponentRename);
            }
        }
        private void RegisterChangeNotification() {
            IComponentChangeService changeService =
                (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (changeService != null) {
                changeService.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                changeService.ComponentChanging += new ComponentChangingEventHandler(OnComponentChanging);
                changeService.ComponentAdded += new ComponentEventHandler(OnComponentAdded);
                changeService.ComponentAdding += new ComponentEventHandler(OnComponentAdding);
                changeService.ComponentRemoved += new ComponentEventHandler(OnComponentRemoved);
                changeService.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
                changeService.ComponentRename += new ComponentRenameEventHandler(OnComponentRename);
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce) {
            if (ce.Component != null && (ce.Component as IComponent).Site != null && ce.Member != null)
                DoLog($"Поле {ce.Member.Name} компонента {(ce.Component as IComponent).Site.Name} изменено");
        }
        private void OnComponentChanging(object sender, ComponentChangingEventArgs ce) {
            if (ce.Component != null && (ce.Component as IComponent).Site != null && ce.Member != null)
                DoLog($"Поле {ce.Member.Name} компонента {(ce.Component as IComponent).Site.Name} будет изменено");
        }
        private void OnComponentAdded(object sender, ComponentEventArgs ce) {
            DoLog($"Добавлен компонент {ce.Component.Site.Name}");
        }
        private void OnComponentAdding(object sender, ComponentEventArgs ce) {
            DoLog($"Будет добавлен компонент типа {ce.Component.GetType().FullName}");
        }

        private void OnComponentRemoved(object sender, ComponentEventArgs ce) {
            DoLog($"Компонент {ce.Component.Site.Name} удален");
        }
        private void OnComponentRemoving(object sender, ComponentEventArgs ce) {
            DoLog($"Компонент {ce.Component.Site.Name} будет удален");
        }
        private void OnComponentRename(object sender, ComponentRenameEventArgs ce) {
            DoLog($"Компонент {ce.OldName} переименован в {ce.NewName}");
        }
        private void DoLog(string text) {
            logBox.Items.Add(text);
            logBox.SelectedIndex = logBox.Items.Count - 1;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                ClearChangeNotification();
            }
            base.Dispose(disposing);
        }
    }
}
