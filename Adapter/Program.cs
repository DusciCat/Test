using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return $"Id #{Id}  Имя: {FirstName}  Фамилия: {LastName}  Отдел: {Department}";
        }
    }
    /// <summary>
    /// Этот класс представляет класс Adaptee. 
    /// Предполагая, что это устаревший код, который поддерживает список сотрудников и имеет только одну
    /// функцию - для возврата всех сотрудников сразу.
    /// </summary>
    public class RecordServer
    {
        private List<Employee> _employees;

        public RecordServer()
        {
            InitializeEmployees();
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        private void InitializeEmployees()
        {
            _employees = new List<Employee>
        {
            new Employee { Id = 1001, FirstName = "Иван" , LastName = "Иванов", Department = "Финансовый"},
            new Employee { Id = 1002, FirstName = "Петр" , LastName = "Петров", Department = "Разработка"},
            new Employee { Id = 1003, FirstName = "Михаил" , LastName = "Михайлов", Department = "Тестирование"},
            new Employee { Id = 1004, FirstName = "Василий" , LastName = "Васильев", Department = "Тестирование"},
            new Employee { Id = 1005, FirstName = "Максим" , LastName = "Максимов", Department = "Разработка"}
        };
        }
    }
    /// <summary>
    /// Представляет IAdapter
    /// Интерфейс позволит клиенту выбрать сотрудника с помощью employeeId
    /// </summary>
    public interface IEmployeeService
    {
        Employee GetEmployee(int employeeId);
    }
    /// <summary>
    /// Представляет класс Adapter.
    /// Этот класс создает экземпляр класса RecordServer и обслуживает клиента возвращая сотрудника на основе id.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        RecordServer recordServer;

        public EmployeeService()
        {
            recordServer = new RecordServer();
        }

        /// Этот метод извлекает список сотрудников с сервера записи
        /// и возвращает сотрудника на основе employeeId
        public Employee GetEmployee(int employeeId)
        {
            var allEmployees = recordServer.GetEmployees();
            return allEmployees.FirstOrDefault(e => e.Id == employeeId);
        }
    }

    //использует IEmployeeService и выбирает сотрудника на основе идентификатора
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            IEmployeeService service = new EmployeeService();

            var employee = service.GetEmployee(1001);
            PrintEmployeeDetails(employee);

            employee = service.GetEmployee(1004);
            PrintEmployeeDetails(employee);

            employee = service.GetEmployee(1020);
            PrintEmployeeDetails(employee);

            employee = service.GetEmployee(1002);
            PrintEmployeeDetails(employee);

            Console.Read();
        }

        static void PrintEmployeeDetails(Employee employee)
        {
            if (employee != null)
                Console.WriteLine(employee);
            else
                Console.WriteLine("Сотрудник не найден");
        }
    }
}