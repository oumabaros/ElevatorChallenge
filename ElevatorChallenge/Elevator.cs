using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
    public class Elevator
    {
        public int[] FloorsServedByElevator { get; set; }

        public int ElevatorCapacity { get; set; }

        public int CurrentFloor { get; set; }

        public List<int> PeopleInElevator { get; set; }

        // Priority queue to store floor drop off requests
        public ElevatorRequestCompare CompareFloorRequests = new ElevatorRequestCompare(0, 0);
        public PriorityQueue<Person, Person> StopFloorRequest;

        public enum ElevatorState
        {
            Idle = 0,
            Up = 1,
            Down = -1
        }

        public ElevatorState CurrentElevatorState { get; set; }


        public Elevator(int[] ElevatorFloorsServedByElevator, int E_Capacity)
        {
            FloorsServedByElevator = ElevatorFloorsServedByElevator;
            ElevatorCapacity = E_Capacity;
            CurrentFloor = FloorsServedByElevator[0];
            PeopleInElevator = new List<int>();
            CurrentElevatorState = ElevatorState.Idle;
            StopFloorRequest = new PriorityQueue<Person, Person>(CompareFloorRequests);
            CompareFloorRequests.FloorsServedByElevator = FloorsServedByElevator.Count();
        }

        /**
		  * Floors served by elevator are stored in an array
		*/
        public void MoveElevatorToNextFloor(ElevatorState state)
        {
            if (state == ElevatorState.Up && CurrentFloor < FloorsServedByElevator[FloorsServedByElevator.Count() - 1])
            {
                CurrentFloor = FloorsServedByElevator[Array.IndexOf(FloorsServedByElevator, CurrentFloor) + 1];
                CompareFloorRequests.CurrentFloor = CurrentFloor;
            }
            else if (state == ElevatorState.Down && CurrentFloor > FloorsServedByElevator[0])
            {
                CurrentFloor = FloorsServedByElevator[Array.IndexOf(FloorsServedByElevator, CurrentFloor) - 1];
                CompareFloorRequests.CurrentFloor = CurrentFloor;
            }
        }

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
    }

}
