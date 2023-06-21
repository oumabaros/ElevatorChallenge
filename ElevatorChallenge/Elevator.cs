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
        private bool[] FloorReady { get; set; }
        public int CurrentFloor = 1;
        public int TopFloor { get; set; }
        public ElevatorState Status = ElevatorState.Idle;
        public int ElevatorId { get; set; }
        public int ElevatorCapacity = 10;
        public int PeopleInElevator { get; set; }
        public enum ElevatorState
        {
            Idle = 0,
            Up = 1,
            Down = -1
        }
        public Elevator(int Id,int NumberOfFloors = 10)
        {
            FloorReady = new bool[NumberOfFloors + 1];
            TopFloor = NumberOfFloors;
            ElevatorId = Id;
            Status = ElevatorState.Idle;
            CurrentFloor = 1;
        }
               
        public bool Call(int floor,int currFloor,int elevatorId)
        {
            if (floor > TopFloor||floor<1)
            {
                Console.WriteLine("We only have {0} floors. The Floor given is invalid.", TopFloor);
                return false;
            }
            else
            {
                Status = ElevatorState.Idle;
                CurrentFloor = currFloor;
                FloorReady[floor] = false;
                Console.WriteLine("Elevator: {0} being called to {1}",elevatorId, floor);
                return true;
            }
            
        }

        private void Halt(int floor,int elevatorId)
        {
            Status = ElevatorState.Idle;
            CurrentFloor = floor;
            FloorReady[floor] = false;
            Console.WriteLine("Elevator: {0} stopped at floor {1}",elevatorId, floor);
        }

        private void Down(int floor, int elevatorId)
        {
            for (int i = CurrentFloor; i >= 1; i--)
            {
                if (FloorReady[i])
                {
                    Console.WriteLine("Elevator: {0} descending to floor number: {1}",elevatorId, floor);
                    Status = ElevatorState.Down;
                    Halt(floor,elevatorId);
                }
                else
                {
                    continue;
                }
                    
            }

            Status = ElevatorState.Idle;
            Console.WriteLine("Waiting..");
        }

        private void Up(int floor, int elevatorId)
        {
            for (int i = CurrentFloor; i <= TopFloor; i++)
            {
                if (FloorReady[i])
                {
                    Status = ElevatorState.Up;
                    Console.WriteLine("Elevator: {0} ascending to floor number {1}: ",elevatorId, floor);
                    Halt(floor, elevatorId);
                }
                else
                {
                    continue;
                }
                   
            }

            Status = ElevatorState.Idle;
            Console.WriteLine("Waiting..");
        }

        void MoveNot()
        {
            Console.WriteLine("That's our current floor");
        }

        public void InitiateMove(int floor,int elevatorId)
        {
            if (floor > TopFloor)
            {
                Console.WriteLine("We only have {0} floors", TopFloor);
                return;
            }

            FloorReady[floor] = true;

            switch (Status)
            {

                case ElevatorState.Down:
                    Down(floor,elevatorId);
                    break;

                case ElevatorState.Idle:
                    if (CurrentFloor < floor)
                        Up(floor,elevatorId);
                    else if (CurrentFloor == floor)
                        MoveNot();
                    else
                        Down(floor,elevatorId);
                    break;

                case ElevatorState.Up:
                    Up(floor,elevatorId);
                    break;

                default:
                    break;
            }


        }
                
    }

}
