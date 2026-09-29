using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        private static DroneManager manager = new DroneManager();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // початкові два обьєкти
            manager.AddNewDrone("DJI Mavic 3", 5000, 70);
            manager.AddNewDrone("FPV Bomber", 4500, 130);

            while (true)
            {
                // менюшка програми
                Console.WriteLine("\n--- МЕНЮ КЕРУВАННЯ ДРОНАМИ ---");
                Console.WriteLine("1 - Додати об'єкт");
                Console.WriteLine("2 - Переглянути всі об'єкти");
                Console.WriteLine("3 - Знайти об'єкт");
                Console.WriteLine("4 - Продемонструвати поведінку");
                Console.WriteLine("5 - Видалити об'єкт");
                Console.WriteLine("0 - Вийти з програми");
                Console.WriteLine("Виберіть дію:");

                string choice = Console.ReadLine();

                if (choice == "1") CreateDroneMenu();
                else if (choice == "2") ShowAllMenu();
                else if (choice == "3") FindMenu();
                else if (choice == "4") FlightMenu();
                else if (choice == "5") DeleteMenu();
                else if (choice == "0") { Console.WriteLine("Вихід з програми..."); break; }
                else Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
            }
        }

        static void CreateDroneMenu()
        {
            // властивості дрона
            try
            {
                Console.WriteLine("Введіть модель дрона:");
                string name = Console.ReadLine();

                Console.WriteLine("Введіть ємність батареї (мАг):");
                double cap = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Введіть максимальну швидкість (км/год):");
                double speed = Convert.ToDouble(Console.ReadLine());

                manager.AddNewDrone(name, cap, speed);
                Console.WriteLine("Дрон успішно додано!");
            }
            catch (Exception ex)
            {
                // ловити помилку
                Console.WriteLine($"Помилка додавання об'єкта: {ex.Message}");
            }
        }

        static void ShowAllMenu()
        {
            List<Drone> all = manager.GetList();
            if (all.Count == 0)
            {
                Console.WriteLine("Список об'єктів порожній.");
                return;
            }

            for (int i = 0; i < all.Count; i++)
            {
                Console.WriteLine(all[i].GetDetails());
            }
        }

        static void FindMenu()
        {
            Console.WriteLine("Введіть ID дрона для пошуку:");
            int id = Convert.ToInt32(Console.ReadLine());

            Drone found = manager.FindById(id);
            if (found != null) Console.WriteLine(found.GetDetails());
            else Console.WriteLine("Дрон із таким ID не знайдений.");
        }

        static void FlightMenu()
        {
            try
            {
                Console.WriteLine("Введіть ID дрона для демонстрації поведінки (тест польоту):");
                int id = Convert.ToInt32(Console.ReadLine());

                Drone activeDrone = manager.FindById(id);
                if (activeDrone == null)
                {
                    Console.WriteLine("Дрон не знайдений!");
                    return;
                }

                Console.WriteLine("Введіть дистанцію польоту (км):");
                double dist = Convert.ToDouble(Console.ReadLine());

                //  метод демонстрації поведінки, містить у собі приватні перевірки
                string flightResult = activeDrone.StartFlight(dist);
                Console.WriteLine(flightResult);
                Console.WriteLine($"Оновлений стан об'єкта: {activeDrone.GetDetails()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка під час демонстрації поведінки: {ex.Message}");
            }
        }

        static void DeleteMenu()
        {
            Console.WriteLine("Введіть ID дрона для видалення:");
            int id = Convert.ToInt32(Console.ReadLine());

            bool deleted = manager.RemoveDrone(id);
            if (deleted) Console.WriteLine("Об'єкт успішно видалено з системи.");
            else Console.WriteLine("Не вдалося знайти або видалити дрон із таким ID.");
        }
    }
}
