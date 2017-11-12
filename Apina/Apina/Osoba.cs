using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apina
{
    public class Osoba
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RC1 { get; set; }
        public string RC2 { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return "Příjmení:  " + Surname + "  Jméno:  " + Name + "  RČ:  " + RC1 + " / " + RC2 + "  Datum narození:  " + BirthDate;
        }
    }
}
