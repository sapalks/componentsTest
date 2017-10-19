using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace ComponentsLibrary.LogComponent {
    public class LogComponent : UserControl {
        //Буфер для отображения сообщений
        private ListBox logBox;
        public ContainerControl ContainerControl {
            get { return _containerControl; }
            set { _containerControl = value; }
        }
        private ContainerControl _containerControl = null;
        //Конструктор
        public LogComponent() {
            InitializeComponent();
        }
        private void InitializeComponent() {
            logBox = new ListBox();
            SuspendLayout();

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
        /// <summary>
        /// Свойство Site устанавливается дизайнером только
        /// в режиме разработки в момент установки связи между
        /// дизайнером и компонентом. Именно в этот момент мы
        /// будем регистрировать обработчики событий
        /// </summary>
        public override ISite Site {
            get => base.Site;
            set {
                //очищаем предыдущие обработчики событий, если они были установлены
                ClearChangeNotification();
                //сохраняем значение Site
                base.Site = value;
                //регистрируем свои обработчики уведомлений об изменениях
                RegisterChangeNotification();
                IDesignerHost host = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null) {
                    IComponent componentHost = host.RootComponent;
                    if (componentHost is ContainerControl) {
                        ContainerControl = componentHost as ContainerControl;
                    }
                }
            }
        }

        /// <summary>
        /// Удаление обработчиков событий
        /// </summary>
        private void ClearChangeNotification() {
            IComponentChangeService changeService =
                (IComponentChangeService)GetService(typeof(IComponentChangeService));
            //Если сервис не null, значит это режим разработки
            if (changeService != null) {
                changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                changeService.ComponentChanging -= new ComponentChangingEventHandler(OnComponentChanging);
                changeService.ComponentAdded -= new ComponentEventHandler(OnComponentAdded);
                changeService.ComponentAdding -= new ComponentEventHandler(OnComponentAdding);
                changeService.ComponentRemoved -= new ComponentEventHandler(OnComponentRemoved);
                changeService.ComponentRemoving -= new ComponentEventHandler(OnComponentRemoving);
                changeService.ComponentRename -= new ComponentRenameEventHandler(OnComponentRename);
            }
        }
        /// <summary>
        /// Добавление обработчиков событий
        /// </summary>
        private void RegisterChangeNotification() {
            IComponentChangeService changeService =
                (IComponentChangeService)GetService(typeof(IComponentChangeService));
            //Если сервис не null, значит это режим разработки
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

        #region Events
        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce) {
            if (ce.Component != null && (ce.Component as IComponent).Site != null && ce.Member != null)
                DoLog($"Поле {ce.Member.Name} компонента {(ce.Component as IComponent).Site.Name} изменено");
        }
        private void OnComponentChanging(object sender, ComponentChangingEventArgs ce) {
            if (ce.Component != null && (ce.Component as IComponent).Site != null && ce.Member != null)
                DoLog($"Поле {ce.Member.Name} компонента {(ce.Component as IComponent).Site.Name} будет изменено");
        }
        private void OnComponentAdded(object sender, ComponentEventArgs ce) {
            DoLog($"Добавлен компонент {ce.Component.Site.Name} типа {ce.Component.GetType()}");
            if (ce.Component.GetType() == typeof(LogComponent)) {
                var log = ce.Component as LogComponent;

                DoLog($"{log.ContainerControl.Name} is parent");
            }
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
        #endregion

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