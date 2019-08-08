using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCross
{
    public class PuzzleBuild
    {
        
        public static List<Puzzel> getPuzzles()
        {
            List<Puzzel> puzzles = new List<Puzzel>();
            var facade = new PiCrossFacade();
            var readout = facade.LoadGameData("../../../../python/picross.zip");

            IList<IPuzzleLibraryEntry> entries = readout.PuzzleLibrary.Entries.ToList();

            foreach (var p in entries)
            {
                puzzles.Add(new Puzzel(p.Author, p.Puzzle));
            }
            return puzzles;
        }
    }
}
