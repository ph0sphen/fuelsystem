using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel
{ // Базовий клас сенсора
    public abstract class Sensor
    {
        public string Name { get; set; }
        public double Value { get; protected set; }

        public Sensor(string name)
        {
            Name = name;
        }

        public abstract void UpdateValue(double newValue);
    }

    // Класи сенсорів
    public class PressureSensor : Sensor
    {
        public PressureSensor() : base("Pressure Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Pressure updated to {Value} bar");
        }
    }

    public class FuelLevelSensor : Sensor
    {
        public FuelLevelSensor() : base("Fuel Level Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Fuel level is {(Value == 1 ? "full" : "empty")}");
        }
    }

    public class TemperatureSensor : Sensor
    {
        public TemperatureSensor() : base("Temperature Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Temperature updated to {Value} °C");
        }
    }

    public class AirMassSensor : Sensor
    {
        public AirMassSensor() : base("Air Mass Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Air mass updated to {Value} g/s");
        }
    }

    public class OxygenSensor : Sensor
    {
        public OxygenSensor() : base("Oxygen Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Oxygen level updated to {Value}%");
        }
    }

    public class EmissionsSensor : Sensor
    {
        public EmissionsSensor() : base("Emissions Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Emissions level updated to {Value}%");
        }
    }

    public class FilterContaminationSensor : Sensor
    {
        public FilterContaminationSensor() : base("Filter Contamination Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Filter is {(Value == 1 ? "contaminated" : "clean")}");
        }
    }

    public class CrankshaftSensor : Sensor
    {
        public CrankshaftSensor() : base("Crankshaft Speed Sensor") { }

        public override void UpdateValue(double newValue)
        {
            Value = newValue;
            Console.WriteLine($"Crankshaft speed updated to {Value} rpm");
        }
    }

    // Базовий клас системи управління
    public abstract class ControlSystem
    {
        public string Name { get; set; }

        protected ControlSystem(string name)
        {
            Name = name;
        }

        public abstract void Control();
    }

    // Класи для систем керування
    public class FuelPump : ControlSystem
    {
        public bool IsOn { get; private set; }
        private double fuelLevel;

        public FuelPump() : base("Fuel Pump") { }

        public void SetFuelLevel(double fuelLevel)
        {
            this.fuelLevel = fuelLevel;
            Control(); // Виклик Control після оновлення значення рівня палива
        }

        public override void Control()
        {
            if (fuelLevel == 0)
            {
                IsOn = true;
                Console.WriteLine("Fuel Pump is ON due to low fuel level.");
            }
            else
            {
                IsOn = false;
                Console.WriteLine("Fuel Pump is OFF.");
            }
        }
    }
    public class SolenoidValve : ControlSystem
    {
        public bool IsOpen { get; private set; }
        private double pressure;
        public SolenoidValve() : base("Solenoid Valve") { }

        public override void Control()
        {
            if (pressure > 5)
            {
                IsOpen = false;
                Console.WriteLine("Solenoid Valve is CLOSED due to high pressure.");
            }
            else
            {
                IsOpen = true;
                Console.WriteLine("Solenoid Valve is OPEN.");
            }
        }
    }

    public class InjectorControl : ControlSystem
    {
        public bool IsInjecting { get; private set; }
        private double oxygenLevel;
        public InjectorControl() : base("Injector Control") { }

        public override void Control()
        {
            IsInjecting = oxygenLevel < 15;
            Console.WriteLine($"Injector Control is {(IsInjecting ? "active" : "inactive")}");
        }
    }

    public class SafetyShutdownSystem : ControlSystem
    {
        public bool IsEmergency { get; private set; }
        private double emissions;
        private double temperature;
        public SafetyShutdownSystem() : base("Safety Shutdown System") { }

        public override void Control()
        {
            IsEmergency = emissions > 80 || temperature > 90;
            Console.WriteLine(IsEmergency ? "Emergency shutdown initiated!" : "System is operating normally.");
        }
    }

    // Основний клас для тестування
    class Program
    {
        static void Main(string[] args)
        {
            // Ініціалізація сенсорів
            var pressureSensor = new PressureSensor();
            var fuelLevelSensor = new FuelLevelSensor();
            var temperatureSensor = new TemperatureSensor();
            var oxygenSensor = new OxygenSensor();
            var emissionsSensor = new EmissionsSensor();

            // Ініціалізація систем керування
            var fuelPump = new FuelPump();
            var solenoidValve = new SolenoidValve();
            var injectorControl = new InjectorControl();
            var safetySystem = new SafetyShutdownSystem();

            // Оновлення значень сенсорів
            pressureSensor.UpdateValue(3.5);
            fuelLevelSensor.UpdateValue(1);
            temperatureSensor.UpdateValue(75);
            oxygenSensor.UpdateValue(14);
            emissionsSensor.UpdateValue(85);

            // Виклик методів контролю для кожної системи
            fuelPump.SetFuelLevel(fuelLevelSensor.Value);
            solenoidValve.Control();
            injectorControl.Control();
            safetySystem.Control();
        }
    }
}
