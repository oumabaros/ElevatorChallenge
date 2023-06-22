using System;
using System.Threading;


namespace ElevatorChallenge
{
    public class Program
    {
        private const string QUIT = "q";
        
        public static void Main(string[] args)
        {
            int Flr;
            int Destination;
            int Flrs=1;
            string FloorInput;
            Elevator Elvtr;
            Floor Fl = new Floor();
            int NumberOfElevators=1;
            string ElevatorInput;
            string ElevatorIdInput;
            int ElevatorId;
            int SelectedElevator=1;
            int MaxNumberOfFloors=1;
            int CurrentFloor = 1;
            string input = "";
            List<Elevator> Elvtrs=new List<Elevator>();
            
            //Prompt for number of elavators.
            Console.WriteLine("How many elevators does the building have?");
            ElevatorInput = Console.ReadLine();

            if (Int32.TryParse(ElevatorInput, out NumberOfElevators))
            {
            Start:
                //Prompt for number of floors.
                Console.WriteLine("How many floors does the building have?");
                FloorInput = Console.ReadLine();
                if (Int32.TryParse(FloorInput, out Flrs))
                {
                    MaxNumberOfFloors = Flrs;
                    Random rn=new Random();
                    //Set Floor IDs and Randomly set number of people waiting on each floor to a maximum of 20.
                    for (int i = 0; i < Flrs; i++)
                    {
                        int people = rn.Next(20);
                        Fl.FloorId = i + 1;
                        Fl.NumberOfPeopleWaiting=people;
                        Console.WriteLine("Floor Number: {0} ,Number Of People Waiting: {1}",Fl.FloorId,Fl.NumberOfPeopleWaiting);
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
                //Randomly set number of people in each elevator to a maximum of ElevatorCapacity(10)
                Random rnd = new Random();
                for (int i = 1; i < NumberOfElevators+1; i++){
                    Elvtr = new Elevator(i,Flrs);
                    int people = rnd.Next(Elvtr.ElevatorCapacity);
                    Elvtr.PeopleInElevator = people;
                    Elvtrs.Add(Elvtr);
                }
                RandomizeSelection(Elvtrs, Flrs);
            }
            //Prompt for current floor, elevator number and destination floor. Press "q" to quit.           
            while (input != QUIT)
            {
                StartFloor:
                Console.WriteLine("Which floor are you in?");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out Flr))
                {
                    if (Flr > Flrs || Flr < 1)
                    {
                        Console.WriteLine("Invalid Floor Selection. Select a value between 1 and {0}", Flrs);
                        goto StartFloor;
                    }
                    else
                    {
                        CurrentFloor = Flr;
                        foreach (Elevator el in Elvtrs)
                        {
                            Console.Write("Elevator Number: {0}", el.ElevatorId);
                            Console.Write(" ");
                            Console.Write("Floor Number: {0}", el.CurrentFloor);
                            Console.WriteLine("");
                        }
                    ElevatorSelect:
                        Console.WriteLine("Select An Elevator:");
                        ElevatorIdInput = Console.ReadLine();
                        if (Int32.TryParse(ElevatorIdInput, out ElevatorId))
                        {

                            if (ElevatorId > Elvtrs.Count || ElevatorId < 1)
                            {
                                Console.WriteLine("Invalid Elevator Selection. Select values from 1 to {0}", Elvtrs.Count);
                                goto ElevatorSelect;
                            }
                            else
                            {
                                SelectedElevator = ElevatorId;
                                Console.WriteLine("Selected Elevator Number: {0}", SelectedElevator);
                            }
                        }
                    }
                }

                else if (input == QUIT)
                {
                    Console.WriteLine("Exiting...");
                }

                DestinationFloor:
                    Console.WriteLine("Which floor are you going to?");
                    input = Console.ReadLine();
                    if (Int32.TryParse(input, out Destination))
                    {
                        if(Destination> Flrs || Destination < 1 ||Destination==Flr)
                        {
                            Console.WriteLine("Invalid Destination Floor Selection.Destination Floor must not be your current floor and must not be greater than {0}",Flrs);
                            goto DestinationFloor;
                        }
                        else
                        {
                            //Perform elevator actions (move up,down or halt)
                            Elevator Elv= Elvtrs.Where(a=>a.ElevatorId==SelectedElevator).FirstOrDefault();
                            Elv.PeopleInElevator++;
                            Elv.Call(Destination,CurrentFloor,SelectedElevator);
                            Elv.InitiateMove(Destination,SelectedElevator);
                            CurrentFloor=Destination;
                            Flr = Destination;
                            //Print results to console.
                            foreach (Elevator el in Elvtrs)
                            {
                                Console.Write("Elevator Number: {0}", el.ElevatorId);
                                Console.Write(" ");
                                Console.Write("Floor Number: {0}", el.CurrentFloor);
                                Console.Write(" ");
                                Console.Write("People In Elevator: {0}", el.PeopleInElevator);
                                Console.Write(" ");
                                Console.Write("Weight Limit: {0}", el.ElevatorCapacity);
                                Console.Write(" ");
                                Console.Write("Status: {0}", el.Status);
                                Console.WriteLine("");
                            }
                            goto StartFloor;
                        }
                        
                    }
                    else if (input == QUIT)
                    {
                        Console.WriteLine("Exiting...");
                    }
                    else
                    {
                        Console.WriteLine("The floor you provided is invalid...please try again!");
                    }
            }
                        
        }

        //Initialize elevators by randomly spreading them among the floors.
        public static void RandomizeSelection(List<Elevator> Elvtrs,int Flrs)
        {
            Random rnd = new Random();
            for (int i = 1; i < Elvtrs.Count+1; i++)
            {
                Elevator Elv = new Elevator(i,Flrs);
                Elv = Elvtrs.Where(a => a.ElevatorId == i).FirstOrDefault();
                int dest = rnd.Next(1, Flrs + 1);
                int currFloor = rnd.Next(1, Flrs + 1);
                int selElevator = rnd.Next(1, Elvtrs.Count + 1);
                Elv.Call(dest, currFloor, selElevator);
                Elv.InitiateMove(dest, selElevator);
            }

        }
        
    }
}