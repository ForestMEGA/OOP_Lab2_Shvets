using System;

namespace Lab2
{
    public class Drone
    {
        // інкапсуляція через приватні поля
        private string model;
        private double batteryCapacity;
        private double currentBatteryLevel;
        private double maxSpeed;

        // автовластивості та за замовчуванням
        public string Status { get; set; } = "Готовий до вильоту";

        public int Id { get; private set; }

        // конструктор
        public Drone(int id, string model, double batteryCapacity, double maxSpeed)
        {
            Id = id;
            Model = model; 
            BatteryCapacity = batteryCapacity;
            CurrentBatteryLevel = 100.0; 
            MaxSpeed = maxSpeed;
        }

        // валідація
        public string Model
        {
            get { return model; }
            set
            {
                if (value == "") throw new ArgumentException("Назва моделі не може бути порожньою!");
                model = value;
            }
        }

        public double BatteryCapacity
        {
            get { return batteryCapacity; }
            set
            {
                if (value <= 0) throw new ArgumentException("Ємність батареї повинна бути більшою за 0!");
                batteryCapacity = value;
            }
        }

        public double CurrentBatteryLevel
        {
            get { return currentBatteryLevel; }
            set
            {
                if (value < 0 || value > 100) throw new ArgumentException("Заряд батареї має бути в межах від 0 до 100%!");
                currentBatteryLevel = value;
            }
        }

        public double MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                if (value <= 0) throw new ArgumentException("Максимальна швидкість повинна бути більшою за 0!");
                maxSpeed = value;
            }
        }

        // обчислювальна властивість
        public double RemainingEnergy
        {
            get { return (batteryCapacity * currentBatteryLevel) / 100.0; }
        }

        public string StartFlight(double distance)
        {
            CheckSystem(); 
            double time = CalculateTime(distance);

            // зменьшення заряду на кожен кілометр
            CurrentBatteryLevel = currentBatteryLevel - (distance * 2.5);

            if (currentBatteryLevel < 20)
            {
                Status = "Потрібна зарядка";
            }

            return $"Політ успішний! Приблизний час у дорозі: {time} хв.";
        }

        // інкапсуляція
        private void CheckSystem()
        {
            if (currentBatteryLevel < 15)
                throw new InvalidOperationException("Неможливо злетіти: критичний рівень заряду (менше 15%)!");
        }

        private double CalculateTime(double distance)
        {
            return (distance / maxSpeed) * 60.0;
        }

        public string GetDetails()
        {
            return $"[ID: {Id}] {model} | Швидкість: {maxSpeed} км/год | Заряд: {currentBatteryLevel}% ({RemainingEnergy} мАг) | Статус: {Status}";
        }
    }
}
