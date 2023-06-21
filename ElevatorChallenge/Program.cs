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
            string PeopleWaitingInput;
            int PeopleWaiting;
            Elevator Elvtr;
            Floor Fl = new Floor();
            int NumberOfElevators;
            string ElevatorInput;
            string ElevatorIdInput;
            int ElevatorId;
            int SelectedElevator=1;
            int MaxNumberOfFloors=1;
            int CurrentFloor = 1;
            List<Elevator> Elvtrs=new List<Elevator>();

            Console.WriteLine("How many elevators does the building have?");
            ElevatorInput = Console.ReadLine();

            if (Int32.TryParse(ElevatorInput, out NumberOfElevators))
            {
            Start:
                Console.WriteLine("How many floors does the building have?");
                FloorInput = Console.ReadLine();
                if (Int32.TryParse(FloorInput, out Flrs))
                {
                    MaxNumberOfFloors = Flrs;
                    for (int i = 0; i < Flrs; i++)
                    {
                        Fl.FloorId = i + 1;
                        Console.WriteLine("Enter the number of people waiting in floor {0}", i + 1);
                        PeopleWaitingInput = Console.ReadLine();
                        if (Int32.TryParse(PeopleWaitingInput, out PeopleWaiting))
                        {
                            Fl.NumberOfPeopleWaiting = PeopleWaiting;
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

                for(int i = 0; i < NumberOfElevators; i++)
                {
                    Elvtr = new Elevator(i+1,Flrs);
                    Elvtrs.Add(Elvtr);
                }
            }

            string input;

            StartFloor:
                Console.WriteLine("Which floor are you in?");
                input = Console.ReadLine();
                if (Int32.TryParse(input, out Flr))
                {
                    if(Flr>Flrs || Flr < 1)
                    {
                        Console.WriteLine("Invalid Floor Selection. Select a value between 1 and {0}",Flrs);
                        goto StartFloor;
                    }
                    else
                    {
                        CurrentFloor = Flr;
                        foreach (Elevator el in Elvtrs)
                        {
                            Console.Write("Elevator Number: {0}", el.ElevatorId);
                            Console.Write(" ");
                            Console.Write("Floor Number: {0}",el.CurrentFloor);
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
                                    Console.WriteLine("Maximum Floors: {0}", MaxNumberOfFloors);
                                }
                            }
                        }
                }
                
                else if (input == QUIT)
                {
                    Console.WriteLine("Exiting...");
                }
                
            

            while (input != QUIT)
            {
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
                            Console.WriteLine("Elevator ID: {0}",SelectedElevator);
                            Console.WriteLine("Number Of Elevators: {0}", Elvtrs.Count);
                            Elevator Elv = new Elevator(SelectedElevator, MaxNumberOfFloors);
                            /*Elevator Elv= (from elv in Elvtrs
                                          where elv.ElevatorId ==SelectedElevator
                                          select elv);*/
                            Elv.Call(Destination,CurrentFloor);
                            Elv.InitiateMove(Destination);
                            Console.WriteLine("Current Floor: {0}", Elv.CurrentFloor);

                            foreach (Elevator el in Elvtrs)
                            {
                                Console.Write("Elevator Number: {0}", el.ElevatorId);
                                Console.Write(" ");
                                Console.Write("Floor Number: {0}", el.CurrentFloor);
                                Console.WriteLine("");
                            }
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
    }
}