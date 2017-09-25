using System.Collections.Generic;

namespace PuzzlXolver
{
    public static class KrydsordMix
    {
		public static Puzzle CreateRoyce()
		{
			var words = new[] {
				"ASE", "BEN", "KÅL", "LUN", "NEJ", "NÅL", "OVN", "SNU",
				"ADIEU", "ADIOS", "ADLØD", "AKSER", "ALOHA", "ARIEN", "ARRET", "ARRIG",
				"BAGOM", "BATIK", "BETLE", "BOLIG", "BUMLE", "CHECK", "CHIPS", "DANDY",
				"DATID", "DEKAN", "ELBAS", "ENERE", "ENSOM", "ETAPE", "GALLA", "GEBYR",
				"GJORD", "GRAVE", "HALLO", "HALVØ", "HEADE", "HEKSE", "HENGÅ", "HVERV",
				"HØJEN", "HØLÆS", "ILDER", "ISLAM", "KLODS", "KLOGT", "LANGS", "MAPPE",
				"MÆLER", "MÅNED", "NUVEL", "OPSTÅ", "RAIDS", "RALLY", "RANCH", "REVLE",
				"ROYCE", "SADEL", "SAHIB", "SNEET", "SPAER", "STADE", "SYNGE", "TAPIR",
				"TERTS", "TOUCH", "TVIVL", "TÅBER", "UDDØD", "VRANG", "ØSTPÅ", "ÅRLIG",
				"ELENDIG", "FLINTEN", "GALANTE", "KATALOG", "KUTYMEN", "KÆDELÅS", "LIGBLEG", "REALIST",
				"REBELSK", "ROSTBØF", "SKAMLØS", "SKARVER", "SNALRET", "SVIREDE", "UDDRIVE", "ULVEHYL",
			};

			List<CellRange> cellRanges = new List<CellRange>();

			// 5x5 squares
			for (int column = 0; column < 24; column += 6)
			{
				for (int row = 0; row < 24; row += 6)
				{
					cellRanges.AddRange(AddSquare(column, row, 5));
				}
			}

			// 7x7 squares
			for (int column = 2; column < 26; column += 12)
			{
				for (int row = 2; row < 26; row += 12)
				{
					cellRanges.AddRange(AddSquare(column, row, 7));
				}
			}

			// 3s
			for (int i = 2; i < 24; i += 6)
			{
				cellRanges.Add(new CellRange(10, i, 3, Direction.Horizontal));
				cellRanges.Add(new CellRange(i, 10, 3, Direction.Vertical));
			}

			var puzzle = new Puzzle(23, 23, cellRanges, words);

			string anchorWord = "ROYCE";
			CellRange anchorRange = puzzle.FindCellRange(22, 18, Direction.Vertical);
			puzzle = puzzle.SetWord(anchorRange, anchorWord);

			puzzle.Verify();

			return puzzle;
		}

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

			List<CellRange> cellRanges = new List<CellRange>();

			// 5x5 squares
			for (int column = 0; column < 24; column += 6)
			{
				for (int row = 0; row < 24; row += 6)
				{
					cellRanges.AddRange(AddSquare(column, row, 5));
				}
			}

			// 7x7 squares
			for (int column = 2; column < 26; column += 12)
			{
				for (int row = 2; row < 26; row += 12)
				{
					cellRanges.AddRange(AddSquare(column, row, 7));
				}
			}

			// 3s
			for (int i = 2; i < 24; i += 6)
			{
				cellRanges.Add(new CellRange(10, i, 3, Direction.Horizontal));
				cellRanges.Add(new CellRange(i, 10, 3, Direction.Vertical));
			}

			var puzzle = new Puzzle(23, 23, cellRanges, words);

			string anchorWord = "RADER";
			CellRange anchorRange = puzzle.FindCellRange(22, 18, Direction.Vertical);
			puzzle = puzzle.SetWord(anchorRange, anchorWord);

			puzzle.Verify();

			return puzzle;
		}

		static IEnumerable<CellRange> AddSquare(int column, int row, int size)
        {
			yield return new CellRange(column, row, size, Direction.Horizontal);
			yield return new CellRange(column, row, size, Direction.Vertical);
			yield return new CellRange(column + size - 1, row, size, Direction.Vertical);
			yield return new CellRange(column, row + size -1, size, Direction.Horizontal);
		}
	}
}
