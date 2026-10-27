using System;
using System.Collections.Generic;

class Vehicle
{
    protected string vehicleId;
    protected string brand;
    protected double dailyRate;

    public Vehicle(string id, string brand, double rate)
    {
        vehicleId = id;
        this.brand = brand;
        dailyRate = rate;
    }

    public virtual double CalculateRentalCost(int days)
    {
        return dailyRate * days;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Vehicle [ID={vehicleId}, Brand={brand}]");
    }
}

class Car : Vehicle
{
    private int numSeats;

    public Car(string id, string brand, double rate, int seats)
        : base(id, brand, rate)
    {
        numSeats = seats;
    }

    public override double CalculateRentalCost(int days)
    {
        return dailyRate * days * 1.10;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Car [ID={vehicleId}, Brand={brand}, Seats={numSeats}]");
    }
}

class Motorcycle : Vehicle
{
    private int engineCapacity;

    public Motorcycle(string id, string brand, double rate, int cc)
        : base(id, brand, rate)
    {
        engineCapacity = cc;
    }

    public override double CalculateRentalCost(int days)
    {
        return dailyRate * days * 0.95;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Motorcycle [ID={vehicleId}, Brand={brand}, CC={engineCapacity}]");
    }
}

class Customer
{
    private string name;
    private string licenseNumber;
    private Vehicle rentedVehicle;

    public Customer(string name, string licenseNumber)
    {
        this.name = name;
        this.licenseNumber = licenseNumber;
        rentedVehicle = null!;
    }

    public void RentVehicle(Vehicle vehicle)
    {
        rentedVehicle = vehicle;
    }

    public void ReturnVehicle()
    {
        rentedVehicle = null!;
    }

    public void PrintRentalSummary(int days)
    {
        Console.WriteLine("\n=== Customer Rental Summary ===");
        Console.WriteLine($"Customer: {name} (License: {licenseNumber})");

        if (rentedVehicle != null)
        {
            rentedVehicle.DisplayInfo();
            Console.WriteLine($"Rented: {rentedVehicle.GetType().Name}");
            Console.WriteLine($"Total Cost for {days} days: ${rentedVehicle.CalculateRentalCost(days):F2}");
        }
    }
}

class Program
{
    static void Main()
    {
        Car car = new Car("101", "Toyota", 100, 5);
        Motorcycle motorcycle = new Motorcycle("102", "Yamaha", 50, 150);

        List<Vehicle> vehicles = new List<Vehicle>();
        vehicles.Add(car);
        vehicles.Add(motorcycle);

        Console.WriteLine("=== Vehicle Rental Costs ===");

        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.DisplayInfo();
            Console.WriteLine($"Cost for 3 days: ${vehicle.CalculateRentalCost(3):F2}");
        }

        Customer customer = new Customer("Richie", "AB1234");
        customer.RentVehicle(car);
        customer.PrintRentalSummary(5);
    }
}