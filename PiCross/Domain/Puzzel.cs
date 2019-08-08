using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCross
{
    public class Puzzel
    {
        public string Author { get; }
        public Puzzle Puzzle { get; }

        public Puzzel(string Author, Puzzle Puzzle)
        {
            this.Author = Author;
            this.Puzzle = Puzzle;
        }
    }
}
