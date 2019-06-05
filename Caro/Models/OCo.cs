using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro.Models
{
    public class OCo
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public CellValues OfPlayer { get; set; }

        public OCo()
        {
            Row = 0;
            Col = 0;
            OfPlayer = CellValues.None;
        }

        public enum CellValues { None = 0, Player1 = 1, Player2 = 2 }
    }
}
