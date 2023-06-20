using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace ElevatorChallenge
{
    public class Person
    {
        [Name("ID")]
        public int ID { get; set; }

        [Name("Floor")]
        public int Floor { get; set; }

        [Name("To Floor")]
        public int ToFloor { get; set; }

        [Name("Time")]
        public int Time { get; set; }
    }
}
