using System;
using System.Threading;


namespace ElevatorChallenge
{
    public class Program
    {
        private const string QUIT = "q";
        
        public static void Main(string[] args)
        {
        
        Start:
            
            Console.WriteLine("How many floors does the building have?");

            int floor;
            string floorInput;
            string peopleWaitingInput;
            int peopleWaiting;
            Elevator elevator;
            Floor fl = new Floor();

            floorInput = Console.ReadLine();

            if (Int32.TryParse(floorInput, out floor))
            {
                elevator = new Elevator(floor);
                for(int i = 0; i < floor; i++)
                {
                    fl.FloorId=i+1;
                    Console.WriteLine("Enter the number of people waiting in floor {0}",i+1);
                    peopleWaitingInput=Console.ReadLine();
                    if (Int32.TryParse(peopleWaitingInput, out peopleWaiting))
                    {
                        fl.NumberOfPeopleWaiting=peopleWaiting;
                    }
                    else
                    {
                        Console.WriteLine("The value entered is not valid!");
                        Console.Beep();
                        Thread.Sleep(2000);
                        Console.Clear();
                        goto Start;
                    }
                }
            }
            else
            {
                Console.WriteLine("The value entered is not valid!");
                Console.Beep();
                Thread.Sleep(2000);
                Console.Clear();
                goto Start;
            }

            string input;

            StartFloor:
                Console.WriteLine("Which floor are you in?");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out floor))
                {
                    if (!elevator.Call(floor))
                    {
                    goto StartFloor;
                    }
                }
                
                else if (input == QUIT)
                {
                    Console.WriteLine("Exiting...");
                }
                
            

            while (input != QUIT)
            {
                Console.WriteLine("Which floor are you going to?");

                input = Console.ReadLine();
                if (Int32.TryParse(input, out floor))
                    elevator.FloorPress(floor);
                else if (input == QUIT)
                    Console.WriteLine("Exiting...");
                else
                    Console.WriteLine("The floor you provided is invalid...please try again!");
            }
        }
    }


    
}