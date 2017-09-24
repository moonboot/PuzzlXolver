using System;

namespace PuzzlXolver
{
    public static class KrydsordMix
    {
        public static Puzzle CreatePage59()
        {
            var words = new[] {
                "BOG", "DUN", "KEL", "NID", "REB", "RET", "RUN", "VAT",
                "AFKOM", "AGILE", "ANGAV", "ANKER", "ASIAT", "BAGEL", "BASAR", "BEFRI",
                "BUTIK", "CHIPS", "CONGA", "DALRE", "DOVNE", "DRUER", "DØSIG", "EMNER",
                "ENDTE", "ENØRE", "EPSOM", "ERROR", "EVIGE", "GALLA", "GARDE", "GINER",
                "GLIDE", "GNAGS", "INDSE", "ISMER", "KARMA", "KODER", "KOLDE", "KRYBE",
                "LEDET", "LIRKE", "LYSET", "MOBIL", "MÅNED", "NASSE", "NITAL", "NUTID",
                "NÆGTE", "OMEGA", "OPHØR", "OPTOG", "ORKEN", "PANEL", "PLANT", "PLUMP",
                "RADER", "RAMPE", "RASTE", "RØRIG", "RÅDET", "RÅKID", "SKØRE", "SNUET",
                "SUMMA", "TEHUS", "TOBAK", "UDDØD", "UTOPI", "VARYL", "ØBOEN", "ØRIGE",
                "BRIEOST", "BRUDGOM", "FEDTHAS", "FILOSOF", "FORLYDT", "LYSERØD", "MORSOMT", "POSTHUS",
                "PRAKSIS", "RIMSMED", "SEMINAR", "SKITSER", "SLØJFER", "SPINKEL", "TIDLIGT", "TRICEPS",
            };

            var puzzle = new Puzzle(23, 23, words);

			// 5x5 squares
            for (int column = 0; column < 24; column += 6)
			{
				for (int row = 0; row < 24; row += 6)
				{
                    AddSquare(puzzle, column, row, 5);
				}
			}

			// 7x7 squares
			for (int column = 2; column < 26; column += 12)
			{
				for (int row = 2; row < 26; row += 12)
				{
					AddSquare(puzzle, column, row, 7);
				}
			}

            // 3s
			for (int i = 2; i < 24; i += 6)
			{
				puzzle.AddCellRange(10, i, 3, Direction.Horizontal);
				puzzle.AddCellRange(i, 10, 3, Direction.Vertical);
			}

            string anchorWord = "RADER";
            CellRange anchorRange = puzzle.FindCellRange(22, 18, Direction.Vertical);
            anchorRange.SetWord(anchorWord);
            puzzle.State.MarkWordAsUsed(anchorWord);

            puzzle.Verify();

			return puzzle;
		}

        static void AddSquare(Puzzle puzzle, int column, int row, int size)
        {
			puzzle.AddCellRange(column, row, size, Direction.Horizontal);
			puzzle.AddCellRange(column, row, size, Direction.Vertical);
			puzzle.AddCellRange(column + size - 1, row, size, Direction.Vertical);
			puzzle.AddCellRange(column, row + size -1, size, Direction.Horizontal);
		}
	}
}
