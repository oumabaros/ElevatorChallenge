﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElevatorChallenge.Elevator;

namespace ElevatorChallenge
{
    public class Elevator
    {
        private bool[] ReadyState { get; set; }
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
            ReadyState = new bool[NumberOfFloors + 1];
            TopFloor = NumberOfFloors;
            ElevatorId = Id;
            Status = ElevatorState.Idle;
            CurrentFloor = 1;
        }
        //Call elevator       
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
                ReadyState[floor] = false;
                Console.WriteLine("Elevator: {0} being called to floor {1}",elevatorId, currFloor);
                return true;
            }
            
        }
        //Method to halt elavator
        private void Halt(int floor,int elevatorId)
        {
            Status = ElevatorState.Idle;
            CurrentFloor = floor;
            ReadyState[floor] = false;
            Console.WriteLine("Elevator: {0} stopped at floor {1}",elevatorId, floor);
        }
        //Method for elevator going down.
        private void Down(int floor, int elevatorId)
        {
            for (int i = CurrentFloor; i >= 1; i--)
            {
                if (ReadyState[i])
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
        //Method for elevator going up
        private void Up(int floor, int elevatorId)
        {
            for (int i = CurrentFloor; i <= TopFloor; i++)
            {
                if (ReadyState[i])
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
        //Method when destination floor=current floor.
        void MoveNot()
        {
            Console.WriteLine("That's our current floor");
        }
        //Main method for elevator actions(calls MoveNot,Up,Down or Halt).
        public void InitiateMove(int floor,int elevatorId)
        {
            if (floor > TopFloor)
            {
                Console.WriteLine("We only have {0} floors", TopFloor);
                return;
            }

            ReadyState[floor] = true;

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
