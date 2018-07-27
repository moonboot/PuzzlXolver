using System.Linq.Expressions;
using System.Collections.Generic;
using System;

namespace PuzzlXolver
{
    public static class KrydsordMix
    {
        public static Puzzle CreatePage9()
        {
            var words = new[] {
                "GES", "HAN", "INA", "LYN", "NON",
                "BÅSE", "ELVE", "ETIK", "GREJ", "HØNE", "IGEN",
                "IOTA", "LUDO", "NAIV", "TABU", "UVIS", "ÆDLE",
                "ARKEN", "HANOI", "LABRE", "NAGET", "NINJA", "SÅEDE",
                "FORFRA", "GRAPPA", "INDTIL", "JAGTEN", "NEDISE", "SIVSKO",
                "BJÆFFET", "ERHOLDT", "GRISERI", "RÅDSNAR",
                "SJOVERE", "UTÆTHED", "VANDLÅS", "VEJBUMP",
            };

            List<CellRange> cellRanges = new List<CellRange>();

			// 3's
			cellRanges.Add(new CellRange(5, 5, 3, Direction.Horizontal));
			cellRanges.Add(new CellRange(5, 7, 3, Direction.Horizontal));
			cellRanges.Add(new CellRange(5, 9, 3, Direction.Horizontal));
			cellRanges.Add(new CellRange(0, 6, 3, Direction.Vertical));
			cellRanges.Add(new CellRange(12, 6, 3, Direction.Vertical));

			// 4's
			foreach (var row in new[] {0, 2, 4, 10, 12, 14})
            {
                cellRanges.Add(new CellRange(0, row, 4, Direction.Horizontal));
				cellRanges.Add(new CellRange(9, row, 4, Direction.Horizontal));
			}

			// 5's
			cellRanges.Add(new CellRange(0, 0, 5, Direction.Vertical));
			cellRanges.Add(new CellRange(0, 10, 5, Direction.Vertical));
			cellRanges.Add(new CellRange(12, 0, 5, Direction.Vertical));
			cellRanges.Add(new CellRange(12, 10, 5, Direction.Vertical));
			cellRanges.Add(new CellRange(5, 5, 5, Direction.Vertical));
			cellRanges.Add(new CellRange(7, 5, 5, Direction.Vertical));

			// 6's
			cellRanges.Add(new CellRange(0, 6, 6, Direction.Horizontal));
			cellRanges.Add(new CellRange(7, 6, 6, Direction.Horizontal));
			cellRanges.Add(new CellRange(0, 8, 6, Direction.Horizontal));
			cellRanges.Add(new CellRange(7, 8, 6, Direction.Horizontal));
			cellRanges.Add(new CellRange(6, 0, 6, Direction.Vertical));
			cellRanges.Add(new CellRange(6, 9, 6, Direction.Vertical));

			// Horizontal 7's
			foreach (var row in new[] { 1, 3, 11, 13 })
			{
				cellRanges.Add(new CellRange(3, row, 7, Direction.Horizontal));
			}

			// Vertical 7's
			cellRanges.Add(new CellRange(3, 0, 7, Direction.Vertical));
			cellRanges.Add(new CellRange(9, 0, 7, Direction.Vertical));
			cellRanges.Add(new CellRange(3, 8, 7, Direction.Vertical));
			cellRanges.Add(new CellRange(9, 8, 7, Direction.Vertical));

			var puzzle = new Puzzle(13, 15, cellRanges, words);

			string anchorWord = "HANOI";
			CellRange anchorRange = puzzle.FindCellRange(5, 5, Direction.Vertical);
			puzzle = puzzle.SetWord(anchorRange, anchorWord);

            puzzle.SetSolution(
                "NAIV  F  UVIS",
                "A  ERHOLDT  Å",
                "GREJ  R  ÆDLE",
                "E  BJÆFFET  D",
                "TABU  R  HØNE",
                "   M HAN E   ",
                "GRAPPA INDTIL",
                "E    NON    Y",
                "SIVSKO JAGTEN",
                "   J INA R   ",
                "LUDO  E  IOTA",
                "A  VANDLÅS  R",
                "BÅSE  I  ETIK",
                "R  RÅDSNAR  E",
                "ELVE  E  IGEN");

			puzzle.Verify();

			return puzzle;
		} 

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

			var puzzle = new Puzzle(23, 23, Create357Ranges(), words);

			string anchorWord = "ROYCE";
			CellRange anchorRange = puzzle.FindCellRange(22, 18, Direction.Vertical);
			puzzle = puzzle.SetWord(anchorRange, anchorWord);

			puzzle.Verify();

			return puzzle;
		}

		public static Puzzle CreatePage59()
		{
			var words = new[] {
				"DOG", "DUN", "KEL", "NID", "REB", "RET", "RUN", "VAT",
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

			var puzzle = new Puzzle(23, 23, Create357Ranges(), words);

			string anchorWord = "RADER";
			CellRange anchorRange = puzzle.FindCellRange(22, 18, Direction.Vertical);
			puzzle = puzzle.SetWord(anchorRange, anchorWord);

            puzzle.SetSolution(
                "GALLA OPTOG CHIPS AFKOM",
                "N   G R   A O   N N   O",
                "A SPINKEL RUN BRUDGOM B",
                "G L L E Y D G R E A O I",
                "SKØRE NASSE ASIAT VARYL",
                "  J     E     E     S  ",
                "BEFRI RØRIG UTOPI ØBOEN",
                "U E S A Ø I D S N R M Æ",
                "T RIMSMED NID TIDLIGT G",
                "I   E P   E Ø   S G   T",
                "KODER ERROR DOVNE ENDTE",
                "  U     E     A     O  ",
                "PANEL TOBAK NUTID BAGEL",
                "L   Y E   A I   A A   E",
                "U POSTHUS RET FILOSOF D",
                "M R E U E M A O R A E E",
                "PLANT SUMMA LIRKE RÅDET",
                "  K     I     L     T  ",
                "DØSIG EMNER KRYBE OPHØR",
                "R I L P A Å O D V M A A",
                "U SKITSER KEL TRICEPS D",
                "E   D O   I D   G G   E",
                "RASTE MÅNED ENØRE ANKER");
			
            puzzle.Verify();

			return puzzle;
		}


        public static Puzzle CreateWeekendDuoPage29()
        {
            var words = new[] {
                "CNN", "EGO", "ILT", "LAL", "NØK", "PIN", "RHO",
                "TEN", "TUR", "TYK", "URO", "UZI", "VAG", "VAL",
                "EGNE", "GUMP", "ISNE", "LÅST", "MOMS", "NABO",
                "RIET", "ROSA", "SNIP", "SPID", "SØGE", "TEAM",
                "AEROB", "ALGER", "DADEL", "ENDNU", "ESROM", "ETUDE",
                "HENNA", "SUTTE", "TURBO", "TYREN", "TØMTE", "YNKET",
                "DJÆVLE", "GÆLISK", "HVÆSER", "ILDRØD", "MIAVER", "SKODDE", "UDKAST", "UHØRTE",
                "AFGIVET", "ALBERTA", "BROWNIE", "DENGSER", "GRILLEN", "INDLØSE",
                "NOTATER", "OPPEFRA", "PRIVATE", "STODDER", "STUDIUM", "UVÆRDIG",
                "AFHENTET", "AGERJORD", "BUMLETOG", "DECEMBER", "ENDEFULD", "KAKAOTRÆ", "TREKLANG", "VERSERER",
                "AYATOLLAH", "EJENDOMME", "ETYMOLOGI", "INDSKIBER", "LYKKETRÆF", "MELODIØSE", "RUINEREDE", "WAGONERNE",
            };

            List<CellRange> cellRanges = new List<CellRange>();

            Action<int, int, int, Direction> addRangeWithMirror = (int originX, int originY, int length, Direction direction) =>
            {
                cellRanges.Add(new CellRange(originX, originY, length, direction));
                cellRanges.Add(new CellRange(23 - originX - (direction == Direction.Horizontal ? length : 1), originY, length, direction));
            };

            Action<int, int, int, Direction> addRangeWithDoubleMirror = (int originX, int originY, int length, Direction direction) =>
            {
                addRangeWithMirror(originX, originY, length, direction);
                addRangeWithMirror(originX, 21 - originY - (direction == Direction.Vertical ? length : 1), length, direction);
            };

            // Ranges mirrored both horizontally and vertically, horizontal ranges
            addRangeWithDoubleMirror(0, 0, 9, Direction.Horizontal);
            addRangeWithDoubleMirror(8, 1, 3, Direction.Horizontal);
            addRangeWithDoubleMirror(1, 2, 8, Direction.Horizontal);
            addRangeWithDoubleMirror(0, 4, 7, Direction.Horizontal);
            addRangeWithDoubleMirror(6, 5, 5, Direction.Horizontal);
            addRangeWithDoubleMirror(0, 6, 7, Direction.Horizontal);
            addRangeWithDoubleMirror(7, 7, 4, Direction.Horizontal);
            addRangeWithDoubleMirror(0, 9, 4, Direction.Horizontal);
            addRangeWithDoubleMirror(5, 9, 6, Direction.Horizontal);

            // Ranges mirrored both horizontally and vertically, vertical ranges
            addRangeWithDoubleMirror(1, 0, 5, Direction.Vertical);
            addRangeWithDoubleMirror(1, 6, 4, Direction.Vertical);
            addRangeWithDoubleMirror(2, 4, 3, Direction.Vertical);
            addRangeWithDoubleMirror(3, 0, 3, Direction.Vertical);
            addRangeWithDoubleMirror(6, 2, 5, Direction.Vertical);
            addRangeWithDoubleMirror(8, 0, 6, Direction.Vertical);
            addRangeWithDoubleMirror(10, 0, 8, Direction.Vertical);

            // Ranges mirrored only horizontally (crosses horizontal center)
            addRangeWithMirror(3, 10, 3, Direction.Horizontal);
            addRangeWithMirror(3, 6, 9, Direction.Vertical);
            addRangeWithMirror(5, 6, 9, Direction.Vertical);
            addRangeWithMirror(7, 7, 7, Direction.Vertical);
            addRangeWithMirror(9, 7, 7, Direction.Vertical);

            var puzzle = new Puzzle(23, 21, cellRanges, words);

            string anchorWord = "TØMTE";
            CellRange anchorRange = puzzle.FindCellRange(6, 2, Direction.Vertical);
            puzzle = puzzle.SetWord(anchorRange, anchorWord);

            puzzle.SetSolution(
                "AYATOLLAH A B MELODIØSE",
                " N Y    VAG UZI    L U ",
                " KAKAOTRÆ E M AFHENTET ",
                " E    Ø S R L V E    T ",
                "STUDIUM E J E E NOTATER",
                "  R   TURBO TYREN   U  ",
                "BROWNIE   R O   ALBERTA",
                " O A N SPID GUMP Y T E ",
                " S G D T N   V R K Y A ",
                "NABO SKODDE GÆLISK MOMS",
                "   NØK D L   R V EGO   ",
                "ISNE ILDRØD UDKAST LÅST",
                " N R B E S   I T R O Ø ",
                " I N E RIET EGNE Æ G G ",
                "OPPEFRA   R N   AFGIVET",
                "  I   ETUDE DADEL   A  ",
                "DENGSER H K E J GRILLEN",
                " N    O Ø L F Æ E    S ",
                " DECEMBER A U VERSERER ",
                " N N    TEN LAL    H O ",
                "RUINEREDE G D EJENDOMME");

            puzzle.Verify();

            return puzzle;
        }

        static List<CellRange> Create357Ranges() 
        {
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

            return cellRanges;
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
