using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Single Responsibility Principle 
namespace TestConsole.NoSolid.S {
    #region пример 1
    /// <summary>
    /// 2 ответственности
    /// 1 - навигация по отчету
    /// 2 - вывод отчета
    /// </summary>
    class Report {
        public string Text { get; set; }
        public void GoToFirstPage() {
            Console.WriteLine("Переход к первой странице");
        }

        public void GoToLastPage() {
            Console.WriteLine("Переход к последней странице");
        }

        public void GoToPage(int pageNumber) {
            Console.WriteLine("Переход к странице {0}", pageNumber);
        }

        public void Print() {
            Console.WriteLine("Печать отчета");
            Console.WriteLine(Text);
        }

    }
    #endregion
    #region пример 2
    class Phone {
        public string Model { get; set; }
        public int Price { get; set; }
    }
    /// <summary>
    /// божественный класс
    /// классы-боги
    /// </summary>
    class MobileStore {
        List<Phone> phones = new List<Phone>();
        public void Process() {
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите цену:");
            int price = 0;
            bool result = Int32.TryParse(Console.ReadLine(), out price);

            if (result == false || price <= 0 || String.IsNullOrEmpty(model)) {
                throw new Exception("Некорректно введены данные");
            } else {
                phones.Add(new Phone { Model = model, Price = price });
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter("store.txt", true)) {
                    writer.WriteLine(model);
                    writer.WriteLine(price);
                }
                Console.WriteLine("Данные успешно обработаны");
            }
        }
    }
    #endregion
}
#endregion
#region Open/Closed Principle
namespace TestConsole.NoSolid.O {
    class Cook {
        public string Name { get; set; }
        public Cook(string name) {
            this.Name = name;
        }

        public void MakeDinner() {
            Console.WriteLine("Чистим картошку");
            Console.WriteLine("Ставим почищенную картошку на огонь");
            Console.WriteLine("Сливаем остатки воды, разминаем варенный картофель в пюре");
            Console.WriteLine("Посыпаем пюре специями и зеленью");
            Console.WriteLine("Картофельное пюре готово");
        }
    }
}
#endregion
#region Liskov Substitution Principle
namespace TestConsole.NoSolid.L {
    #region пример 1
    class Rectangle {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int GetArea() {
            return Width * Height;
        }
    }

    class Square : Rectangle {
        public override int Width {
            get {
                return base.Width;
            }

            set {
                base.Width = value;
                base.Height = value;
            }
        }

        public override int Height {
            get {
                return base.Height;
            }

            set {
                base.Height = value;
                base.Width = value;
            }
        }
    }
    class Program_ {
        static void Main(string[] args) {
            Rectangle rect = new Square();
            TestRectangleArea(rect);
            TestRectangleArea1(rect);

            Console.Read();
        }

        public static void TestRectangleArea(Rectangle rect) {
            rect.Height = 5;
            rect.Width = 10;
            if (rect.GetArea() != 50)
                throw new Exception("Некорректная площадь!");
        }
        public static void TestRectangleArea1(Rectangle rect) {
            if (rect is Square) {
                rect.Height = 5;
                if (rect.GetArea() != 25)
                    throw new Exception("Неправильная площадь!");
            } else if (rect is Rectangle) {
                rect.Height = 5;
                rect.Width = 10;
                if (rect.GetArea() != 50)
                    throw new Exception("Неправильная площадь!");
            }
        }
    }
    #endregion
}
namespace TestConsole.NoSolid.L1 {

    /// <summary>
    /// Предусловия (Preconditions) не могут быть усилены в подклассе. 
    /// Другими словами подклассы не должны создавать больше предусловий,
    /// чем это определено в базовом классе, для выполнения некоторого поведения
    /// </summary>
    class Account {
        public int Capital { get; protected set; }

        public virtual void SetCapital(int money) {
            if (money < 0)
                throw new Exception("Нельзя положить на счет меньше 0");
            this.Capital = money;
        }
    }

    class MicroAccount : Account {
        public override void SetCapital(int money) {
            if (money < 0)
                throw new Exception("Нельзя положить на счет меньше 0");

            if (money > 100)
                throw new Exception("Нельзя положить на счет больше 100");

            this.Capital = money;
        }
    }
    class Program {
        static void Main(string[] args) {
            Account acc = new MicroAccount();
            InitializeAccount(acc);

            Console.Read();
        }

        public static void InitializeAccount(Account account) {
            account.SetCapital(200);
            Console.WriteLine(account.Capital);
        }
    }

}
namespace TestConsole.NoSolid.L2 {
    /// <summary>
    /// Постусловия (Postconditions) не могут быть ослаблены в 
    /// подклассе. То есть подклассы должны выполнять все 
    /// постусловия, которые определены в базовом классе.
    /// </summary>
    class Account {
        public virtual decimal GetInterest(decimal sum, int month, int rate) {
            // предусловие
            if (sum < 0 || month > 12 || month < 1 || rate < 0)
                throw new Exception("Некорректные данные");

            decimal result = sum;
            for (int i = 0; i < month; i++)
                result += result * rate / 100;

            // постусловие
            if (sum >= 1000)
                result += 100; // добавляем бонус

            return result;
        }
    }

    class MicroAccount : Account {
        public override decimal GetInterest(decimal sum, int month, int rate) {
            if (sum < 0 || month > 12 || month < 1 || rate < 0)
                throw new Exception("Некорректные данные");

            decimal result = sum;
            for (int i = 0; i < month; i++)
                result += result * rate / 100;

            return result;
        }
    }
    class Program {
        public static void CalculateInterest(Account account) {
            decimal sum = account.GetInterest(1000, 1, 10); // 1000 + 1000 * 10 / 100 + 100 (бонус)
            if (sum != 1200) // ожидаем 1200
            {
                throw new Exception("Неожиданная сумма при вычислениях");
            }
        }
        static void Main(string[] args) {
            Account acc = new MicroAccount();
            CalculateInterest(acc); // получаем 1100 без бонуса 100

            Console.Read();
        }
    }
}
namespace TestConsole.NoSolid.L3 {
    /// <summary>
    /// Инварианты (Invariants) — все условия базового класса - 
    /// также должны быть сохранены и в подклассе
    /// 
    /// Инварианты - это некоторые условия, которые остаются 
    /// истинными на протяжении всей жизни объекта. Как правило, 
    /// инварианты передают внутреннее состояние объекта. 
    /// </summary>
    class Account {
        protected int capital;
        public Account(int sum) {
            if (sum < 100)
                throw new Exception("Некорректная сумма");
            this.capital = sum;
        }

        public virtual int Capital {
            get { return capital; }
            set {
                if (value < 100)
                    throw new Exception("Некорректная сумма");
                capital = value;
            }
        }
    }

    class MicroAccount : Account {
        public MicroAccount(int sum) : base(sum) {
        }

        public override int Capital {
            get { return capital; }
            set {
                capital = value;
            }
        }
    }
}
#endregion
#region Interface Segregation Principle
namespace TestConsole.NoSolid.I {
    interface IMessage {
        void Send();
        string Text { get; set; }
        string Subject { get; set; }
        string ToAddress { get; set; }
        string FromAddress { get; set; }
    }
    class EmailMessage : IMessage {
        public string Subject { get; set; }
        public string Text { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        public void Send() {
            Console.WriteLine("Отправляем по Email сообщение: {0}", Text);
        }
    }
    class SmsMessage : IMessage {
        public string Text { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        public string Subject {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public void Send() {
            Console.WriteLine("Отправляем по Sms сообщение: {0}", Text);
        }
    }
    //class VoiceMessage : IMessage {
    //    public string ToAddress { get; set; }
    //    public string FromAddress { get; set; }
    //    public byte[] Voice { get; set; }

    //    public string Text {
    //        get {
    //            throw new NotImplementedException();
    //        }

    //        set {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public string Subject {
    //        get {
    //            throw new NotImplementedException();
    //        }

    //        set {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public void Send() {
    //        Console.WriteLine("Передача голосовой почты");
    //    }
    //}
}
#endregion
#region Dependency Inversion Principle
namespace TestConsole.NoSolid.D {
    class Book {
        public string Text { get; set; }
        public ConsolePrinter Printer { get; set; }

        public void Print() {
            Printer.Print(Text);
        }
    }

    class ConsolePrinter {
        public void Print(string text) {
            Console.WriteLine(text);
        }
    }
}
#endregion