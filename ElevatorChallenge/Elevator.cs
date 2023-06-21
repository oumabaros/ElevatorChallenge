using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElevatorChallenge.Elevator;

namespace ElevatorChallenge
{
    public class Elevator
    {
        private bool[] floorReady;
        public int CurrentFloor = 1;
        public int topfloor;
        public ElevatorState Status = ElevatorState.Idle;


        public int ElevatorCapacity = 10;

        //public int CurrentFloor { get; set; }

        public List<int> PeopleInElevator { get; set; }
             

        public enum ElevatorState
        {
            Idle = 0,
            Up = 1,
            Down = -1
        }

        public ElevatorState CurrentElevatorState { get; set; }

        public Elevator(int NumberOfFloors = 10)
        {
            floorReady = new bool[NumberOfFloors + 1];
            topfloor = NumberOfFloors;
        }
        /*public Elevator_(int[] ElevatorFloorsServedByElevator, int E_Capacity)
        {
            FloorsServedByElevator = ElevatorFloorsServedByElevator;
            ElevatorCapacity = E_Capacity;
            CurrentFloor = FloorsServedByElevator[0];
            PeopleInElevator = new List<int>();
            CurrentElevatorState = ElevatorState.Idle;
            StopFloorRequest = new PriorityQueue<Person, Person>(CompareFloorRequests);
            CompareFloorRequests.FloorsServedByElevator = FloorsServedByElevator.Count();
        }*/

        
        /**
		  * Display current people in elevator
		*/
        public string DisplayPeopleInElevator()
        {
            StringBuilder CurrentPeopleInElevator = new StringBuilder("(");
            foreach (int Person in PeopleInElevator)
            {
                CurrentPeopleInElevator.Append(Person + " ");
            }
            if (CurrentPeopleInElevator.Length > 1)
                CurrentPeopleInElevator.Remove(CurrentPeopleInElevator.Length - 1, 1);
            CurrentPeopleInElevator.Append(")");
            return CurrentPeopleInElevator.ToString();
        }

        public bool Call(int floor)
        {
            if (floor > topfloor||floor<1)
            {
                Console.WriteLine("We only have {0} floors. The Floor given is invalid.", topfloor);
                return false;
            }
            else
            {
                Status = ElevatorState.Idle;
                CurrentFloor = floor;
                floorReady[floor] = false;
                Console.WriteLine("Elevator being called to {0}", floor);
                return true;
            }
            
        }

        private void Stop(int floor)
        {
            Status = ElevatorState.Idle;
            CurrentFloor = floor;
            floorReady[floor] = false;
            Console.WriteLine("Stopped at floor {0}", floor);
        }

        private void Descend(int floor)
        {
            for (int i = CurrentFloor; i >= 1; i--)
            {
                if (floorReady[i])
                    Stop(floor);
                else
                    continue;
            }

            Status = ElevatorState.Idle;
            Console.WriteLine("Waiting..");
        }

        private void Ascend(int floor)
        {
            for (int i = CurrentFloor; i <= topfloor; i++)
            {
                if (floorReady[i])
                    Stop(floor);
                else
                    continue;
            }

            Status = ElevatorState.Idle;
            Console.WriteLine("Waiting..");
        }

        void StayPut()
        {
            Console.WriteLine("That's our current floor");
        }

        public void FloorPress(int floor)
        {
            if (floor > topfloor)
            {
                Console.WriteLine("We only have {0} floors", topfloor);
                return;
            }

            floorReady[floor] = true;

            switch (Status)
            {

                case ElevatorState.Down:
                    Descend(floor);
                    break;

                case ElevatorState.Idle:
                    if (CurrentFloor < floor)
                        Ascend(floor);
                    else if (CurrentFloor == floor)
                        StayPut();
                    else
                        Descend(floor);
                    break;

                case ElevatorState.Up:
                    Ascend(floor);
                    break;

                default:
                    break;
            }


        }

        public enum ElevatorStatus
        {
            UP,
            STOPPED,
            DOWN
        }
    }

}
