namespace PuzzlXolver
{
    public class Cell
    {
        public CellState State { get; private set; }

        Puzzle puzzle;
        int column, row;

        public Cell(Puzzle puzzle, int column, int row) {
            this.puzzle = puzzle;
            this.column = column;
            this.row = row;
			Value = '\u25A1';
			State = CellState.NotUsed;
		}

        public char Value {
            get { return puzzle.GetValue(column, row); }
            set 
            { 
                puzzle.SetValue(column, row, value);
                State = CellState.Filled;
            }
        }

        public void SetUsed()
        {
            State = CellState.InRange;
        }

        public override string ToString()
        {
            return (State == CellState.NotUsed ? ' ' : Value).ToString();
        }
    }
}
