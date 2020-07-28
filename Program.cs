using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Lab5._3UsedCarLot
{
    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Decimal Price { get; set; }




        public Car()
        {
            Make = "Empty";
            Model = "Empty";
            Year = 0;
            Price = 0.00m;

           CarLot.Add(this);
        }
        public Car(string theMake, string theModel, int theYear, Decimal thePrice)
        {
            Make = theMake;
            Model = theModel;
            Year = theYear;
            Price = thePrice;

            CarLot.Add(this);
        }
        public override string ToString()
        {
            string word = "|   New  |";
            return $"{Make,-20} {Model,-10} {Year,-5} ${Price,0} {word,23} ";
        }
    }
    class UsedCar : Car
    {
        public double Mileage { get; set; }

        public UsedCar():base()
        {
            
            Mileage = 0;

        }

        public UsedCar(string theMake, string theModel, int theYear, Decimal thePrice, double theMileage)
        :base(theMake,theModel,theYear,thePrice)
        {
            
            Mileage = theMileage;

        }

        public override string ToString()
        {
            string word = "|  Used  |";
            return $"{Make,-20} {Model,-10} {Year,-5} ${Price,0} {Mileage,6} Miles  {word,10} ";
            
        }
    }
    class CarLot
    {
       private static List<Car> myList = new List<Car>();
        
       public static int GetNumber()
        {
            return myList.Count;
        }

       public static void ViewCars()
        {
            
            for(int i = 0; i<myList.Count; i++)
            {
                Console.WriteLine($"{i+1}. {myList[i]}");
            }

        }

        public static void ShowcaseCar(int index)
        {
            Console.WriteLine($"{myList[index-1]}");
        }
        
        public static void Add(Car thecar)
        {
            myList.Add(thecar);
        }

        public static void Remove(int index)
        {
            myList.RemoveAt(index-1);
        }
    }



    class Program
    {

        public static void BuildCar()
        {
            string make = "";
            string model = "";
            int year = 0;
            Decimal price = 0.00m;
            int mileage = 0;

            Console.Clear();
            Console.WriteLine("What is the make?");
            make = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("What is the model?");
            model = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("What is the year?");
            bool isValid = int.TryParse(Console.ReadLine(), out year);
            while (!isValid)
            {
                Console.Clear();
                Console.WriteLine("Please enter a valid answer for the year.");
                isValid = int.TryParse(Console.ReadLine(), out year);
            }

            Console.Clear();
            Console.WriteLine("What is the price?");
            isValid = Decimal.TryParse(Console.ReadLine(), out price);
            while (!isValid)
            {
                Console.Clear();
                Console.WriteLine("Please enter a valid answer for the price.");
                isValid = Decimal.TryParse(Console.ReadLine(), out price);
            }

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Is this a new vehicle? (Y/N)");
                string answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    break;
                }
                if (answer == "n")
                {
                    Console.WriteLine("How many miles?");
                    isValid = int.TryParse(Console.ReadLine(), out mileage);
                    while (!isValid)
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a valid answer for the year.");
                        isValid = int.TryParse(Console.ReadLine(), out mileage);
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }


            if (mileage > 0)
            {
                Car car = new UsedCar(make, model, year, price, mileage);
            }
            else if(mileage == 0)
            {
                Car car = new Car(make, model, year, price);
            }

        }

        static void Main(string[] args)
        {
            

            Car car1 = new Car("Tesla", "Model S", 2020, 80000.00m); 
            Car car2 = new Car("Chevrolet", "Camaro", 2020, 50000.00m); 
            Car car3 = new Car("Jeep","Wrangler", 2020 ,60000.00m);
            Car car4 = new UsedCar("Pontiac","Firebird", 1995, 5000.00m, 89000);
            Car car5 = new UsedCar("Ford","F150",1997, 3000.00m, 100000);
            Car car6 = new UsedCar("Chrysler","300",2010, 8000.00m, 65000);

            

            bool isRunning = true;
            while (isRunning)
            {
                int num1 = CarLot.GetNumber()+1;
                int num2 = CarLot.GetNumber()+2;
                int num3 = CarLot.GetNumber()+3;
                int num4 = CarLot.GetNumber()+4;

                Console.WriteLine("Welcome to the car lot, what would you like to do?");
                CarLot.ViewCars();
                Console.WriteLine($"{num1}. Add a Car.");
                Console.WriteLine($"{num2}. Buy a Car.");
                Console.WriteLine($"{num3}. Quit.");

                bool isValid = int.TryParse(Console.ReadLine(), out int number);
                while (!isValid || number >= num4)
                {
                    Console.WriteLine("Please enter a valid answer. ");
                    isValid = int.TryParse(Console.ReadLine(), out number);
                }

                if(number == num1)
                {
                    BuildCar();
                }
                if(number == num2)
                {
                    Console.Clear();
                    CarLot.ViewCars();

                    Console.WriteLine("Which car do you want to buy?");
                    isValid = int.TryParse(Console.ReadLine(), out int choice);
                    while (!isValid || choice > CarLot.GetNumber())
                    {
                        Console.WriteLine("Sorry, that's not on our list.");
                        isValid = int.TryParse(Console.ReadLine(), out choice);
                    }

                    Console.Clear();
                    CarLot.ShowcaseCar(choice);
                    Console.WriteLine("Are you sure you want to buy this vehicle? (Y/N)");

                    bool isStillRunning = true;
                    while (isStillRunning) {
                        string signDocs = Console.ReadLine().ToLower();
                        if (signDocs == "y")
                        {
                            CarLot.Remove(choice);
                            Console.WriteLine("Thanks for shopping with us!");
                            Console.WriteLine("Press enter to return to the menu.");
                            Console.ReadLine();
                            break;

                        }
                        else if (signDocs == "n")
                        {
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    //Console.ReadLine();
                    

                }
                if(number == num3)
                {
                    Environment.Exit(1);
                }
                else
                {
                    continue;
                }

            }



        }
    }
}
