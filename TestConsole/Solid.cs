using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Single Responsibility Principle 
namespace NoSolid.S {
    #region пример 1
    interface IPrinter {
        void Print(string text);
    }

    class ConsolePrinter : IPrinter {
        public void Print(string text) {
            Console.WriteLine(text);
        }
    }

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
        public void Print(IPrinter printer) {
            printer.Print(this.Text);
        }
    }
    #endregion
    #region пример 2

    class Phone {
        public string Model { get; set; }
        public int Price { get; set; }
    }

    class MobileStore {
        List<Phone> phones = new List<Phone>();

        public IPhoneReader Reader { get; set; }
        public IPhoneBinder Binder { get; set; }
        public IPhoneValidator Validator { get; set; }
        public IPhoneSaver Saver { get; set; }

        public MobileStore(IPhoneReader reader, IPhoneBinder binder, IPhoneValidator validator, IPhoneSaver saver) {
            this.Reader = reader;
            this.Binder = binder;
            this.Validator = validator;
            this.Saver = saver;
        }

        public void Process() {
            string[] data = Reader.GetInputData();
            Phone phone = Binder.CreatePhone(data);
            if (Validator.IsValid(phone)) {
                phones.Add(phone);
                Saver.Save(phone, "store.txt");
                Console.WriteLine("Данные успешно обработаны");
            } else {
                Console.WriteLine("Некорректные данные");
            }
        }
    }

    interface IPhoneReader {
        string[] GetInputData();
    }
    class ConsolePhoneReader : IPhoneReader {
        public string[] GetInputData() {
            Console.WriteLine("Введите модель:");
            string model = Console.ReadLine();
            Console.WriteLine("Введите цену:");
            string price = Console.ReadLine();
            return new string[] { model, price };
        }
    }

    interface IPhoneBinder {
        Phone CreatePhone(string[] data);
    }
    class GeneralPhoneBinder : IPhoneBinder {
        public Phone CreatePhone(string[] data) {
            if (data.Length >= 2) {
                int price = 0;
                if (Int32.TryParse(data[1], out price)) {
                    return new Phone { Model = data[0], Price = price };
                } else {
                    throw new Exception("Ошибка привязчика модели Phone. Некорректные данные для свойства Price");
                }
            } else {
                throw new Exception("Ошибка привязчика модели Phone. Недостаточно данных для создания модели");
            }
        }
    }

    interface IPhoneValidator {
        bool IsValid(Phone phone);
    }
    class GeneralPhoneValidator : IPhoneValidator {
        public bool IsValid(Phone phone) {
            if (String.IsNullOrEmpty(phone.Model) || phone.Price <= 0)
                return false;

            return true;
        }
    }

    interface IPhoneSaver {
        void Save(Phone phone, string fileName);
    }
    class TextPhoneSaver : IPhoneSaver {
        public void Save(Phone phone, string fileName) {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, true)) {
                writer.WriteLine(phone.Model);
                writer.WriteLine(phone.Price);
            }
        }
    }
    #endregion
}
#endregion
#region Open/Closed Principle
namespace Solid.O.E1 {
    #region паттерн Стратегия
    class Cook {
        public string Name { get; set; }

        public Cook(string name) {
            this.Name = name;
        }

        public void MakeDinner(IMeal meal) {
            meal.Make();
        }
    }

    interface IMeal {
        void Make();
    }
    class PotatoMeal : IMeal {
        public void Make() {
            Console.WriteLine("Чистим картошку");
            Console.WriteLine("Ставим почищенную картошку на огонь");
            Console.WriteLine("Сливаем остатки воды, разминаем варенный картофель в пюре");
            Console.WriteLine("Посыпаем пюре специями и зеленью");
            Console.WriteLine("Картофельное пюре готово");
        }
    }
    class SaladMeal : IMeal {
        public void Make() {
            Console.WriteLine("Нарезаем помидоры и огурцы");
            Console.WriteLine("Посыпаем зеленью, солью и специями");
            Console.WriteLine("Поливаем подсолнечным маслом");
            Console.WriteLine("Салат готов");
        }
    }
    #endregion
}
namespace Solid.O.E2 {
    #region паттерн Шаблонов
    abstract class MealBase {
        public void Make() {
            Prepare();
            Cook();
            FinalSteps();
        }
        protected abstract void Prepare();
        protected abstract void Cook();
        protected abstract void FinalSteps();
    }
    class PotatoMeal : MealBase {
        protected override void Cook() {
            Console.WriteLine("Ставим почищенную картошку на огонь");
            Console.WriteLine("Варим около 30 минут");
            Console.WriteLine("Сливаем остатки воды, разминаем варенный картофель в пюре");
        }

        protected override void FinalSteps() {
            Console.WriteLine("Посыпаем пюре специями и зеленью");
            Console.WriteLine("Картофельное пюре готово");
        }

        protected override void Prepare() {
            Console.WriteLine("Чистим и моем картошку");
        }
    }
    class SaladMeal : MealBase {
        protected override void Cook() {
            Console.WriteLine("Нарезаем помидоры и огурцы");
            Console.WriteLine("Посыпаем зеленью, солью и специями");
        }

        protected override void FinalSteps() {
            Console.WriteLine("Поливаем подсолнечным маслом");
            Console.WriteLine("Салат готов");
        }

        protected override void Prepare() {
            Console.WriteLine("Моем помидоры и огурцы");
        }
    }
    class Cook {
        public string Name { get; set; }

        public Cook(string name) {
            this.Name = name;
        }

        public void MakeDinner(MealBase[] menu) {
            foreach (MealBase meal in menu)
                meal.Make();
        }
    }
    #endregion
}
#endregion
#region Liskov Substitution Principle
namespace Solid.L {
}
#endregion
#region Interface Segregation Principle
namespace TestConsole.Solid.I {
    interface IMessage {
        void Send();
        string ToAddress { get; set; }
        string FromAddress { get; set; }
    }
    interface IVoiceMessage : IMessage {
        byte[] Voice { get; set; }
    }
    interface ITextMessage : IMessage {
        string Text { get; set; }
    }

    interface IEmailMessage : ITextMessage {
        string Subject { get; set; }
    }

    class VoiceMessage : IVoiceMessage {
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }

        public byte[] Voice { get; set; }
        public void Send() {
            Console.WriteLine("Передача голосовой почты");
        }
    }
    class EmailMessage : IEmailMessage {
        public string Text { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }

        public void Send() {
            Console.WriteLine("Отправляем по Email сообщение: {0}", Text);
        }
    }

    class SmsMessage : ITextMessage {
        public string Text { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public void Send() {
            Console.WriteLine("Отправляем по Sms сообщение: {0}", Text);
        }
    }
}
#endregion
#region Dependency Inversion Principle
namespace TestConsole.Solid.D {
    interface IPrinter {
        void Print(string text);
    }

    class Book {
        public string Text { get; set; }
        public IPrinter Printer { get; set; }

        public Book(IPrinter printer) {
            this.Printer = printer;
        }

        public void Print() {
            Printer.Print(Text);
        }
    }

    class ConsolePrinter : IPrinter {
        public void Print(string text) {
            Console.WriteLine("Печать на консоли");
        }
    }

    class HtmlPrinter : IPrinter {
        public void Print(string text) {
            Console.WriteLine("Печать в html");
        }
    }
}
#endregion