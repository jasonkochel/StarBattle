using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarBattle.Core.Models
{
    public class Puzzle
    {
        public List<Shape> Shapes { get; set; }
    }

    public class Shape
    {
        private readonly List<Cell> _cells = new List<Cell>();

        public void ToggleCell(int row, int col)
        {
            if (_cells.Any(c => c.Row == row && c.Col == col))
            {
                _cells.RemoveAll(c => c.Row == row && c.Col == col);
            }
            else
            {
                _cells.Add(new Cell { Row = row, Col = col });
            }
        }

        public IEnumerable<CellView> Cells => _cells.Select(c => new CellView
        {
            Row = c.Row,
            Col = c.Col,
            BorderTop = !_cells.Any(cc => cc.Col == c.Col && cc.Row == c.Row - 1),
            BorderBottom = !_cells.Any(cc => cc.Col == c.Col && cc.Row == c.Row + 1),
            BorderLeft = !_cells.Any(cc => cc.Row == c.Row && cc.Col == c.Col - 1),
            BorderRight = !_cells.Any(cc => cc.Row == c.Row && cc.Col == c.Col + 1),
        });
    }

    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }

    public class CellView : Cell
    {
        public bool BorderTop { get; set; }
        public bool BorderBottom { get; set; }
        public bool BorderLeft { get; set; }
        public bool BorderRight { get; set; }
    }
}
