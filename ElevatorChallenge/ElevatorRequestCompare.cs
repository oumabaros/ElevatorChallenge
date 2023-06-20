using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge
{
 /**
 * Adds dropoff requests to the elevator's priority queue.
 */
    public class ElevatorRequestCompare : IComparer<Person>
    {
        public int CurrentFloor { get; set; }
        public int ElevatorState { get; set; }
        public int FloorsServedByElevator { get; set; }

        public int Compare(Person? p1, Person? p2)
        {
            if (p1 != null && p1.ToFloor == CurrentFloor && p2 != null && p2.ToFloor != CurrentFloor)
                return int.MinValue;
            else if (p2 != null && p2.ToFloor == CurrentFloor && p1 != null && p1.ToFloor != CurrentFloor)
                return int.MaxValue;
            else if (ElevatorState == 1 && p1 != null && p2 != null)
            {
                int score1 = (p1.ToFloor > CurrentFloor) ? (p1.ToFloor - CurrentFloor - FloorsServedByElevator) : (FloorsServedByElevator - (p1.ToFloor - CurrentFloor));
                int score2 = (p2.ToFloor > CurrentFloor) ? (p2.ToFloor - CurrentFloor - FloorsServedByElevator) : (FloorsServedByElevator - (p2.ToFloor - CurrentFloor));
                return score1 - score2;
            }
            else if (ElevatorState == -1 && p1 != null && p2 != null)
            {
                int score1 = (p1.ToFloor < CurrentFloor) ? (CurrentFloor - p1.ToFloor - FloorsServedByElevator) : (FloorsServedByElevator - (CurrentFloor - p1.ToFloor));
                int score2 = (p2.ToFloor < CurrentFloor) ? (CurrentFloor - p2.ToFloor - FloorsServedByElevator) : (FloorsServedByElevator - (CurrentFloor - p2.ToFloor));
                return score1 - score2;
            }
            else if (ElevatorState == 0 && p1 != null && p2 != null)
                return Math.Abs(p1.ToFloor - CurrentFloor) - Math.Abs(p2.ToFloor - CurrentFloor);

            return int.MaxValue;
        }

        public ElevatorRequestCompare(int floor, int state)
        {
            CurrentFloor = floor;
            ElevatorState = state;
        }
    }
}
