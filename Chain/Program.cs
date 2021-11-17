using System;
using System.Text;

namespace Chain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Настройка цепочки обязанности
            Approver vasya = new Director();
            Approver petya = new VicePresident();
            Approver ivan = new President();
            vasya.SetSuccessor(petya);
            petya.SetSuccessor(ivan);
            // Создание и обработка запросов на покупку
            Purchase p = new Purchase(2034, 350.00, "Supplies");
            vasya.ProcessRequest(p);
            p = new Purchase(2035, 32590.10, "Project X");
            vasya.ProcessRequest(p);
            p = new Purchase(2036, 122100.00, "Project Y");
            vasya.ProcessRequest(p);

            Console.ReadKey();
        }
    }

    public abstract class Approver
    {
        protected Approver successor;
        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }
        public abstract void ProcessRequest(Purchase purchase);
    }

    public class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} одобренный запрос# {1}",
                    this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    public class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} одобренный запрос# {1}",
                    this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    public class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} одобренный запрос# {1}",
                    this.GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine(
                    "Запрос# {0} требуется исполнительная встреча!",
                    purchase.Number);
            }
        }
    }
    /// <summary>
    /// Класс детали запроса
    /// </summary>
    public class Purchase
    {
        int number;
        double amount;
        string purpose;
        // Конструктор
        public Purchase(int number, double amount, string purpose)
        {
            this.number = number;
            this.amount = amount;
            this.purpose = purpose;
        }
        // Получаем и задаем номер покупки
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        // Получаем и задаем цену покупки
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        // Получаем и задаем цель покупки
        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }
    }
}