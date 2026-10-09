namespace Algorithms;

class EightQueensProblem {

    // wiersze
    private readonly bool[] _a = [true, true, true, true, true, true, true, true];
    
    // przekątne
    private readonly bool[] _b = [true, true, true, true, true, true, true, true, true, true, true, true, true, true, true];
    private readonly bool[] _c = [true, true, true, true, true, true, true, true, true, true, true, true, true, true, true];
    
    // Kolumna, w której znajduje się hetman w danym wierszu
    private readonly int[] _x = [-1, -1, -1, -1, -1, -1, -1, -1];
    
    private int _solutionsCount = 0;
    
    private bool _isSafe(int i, int j) {
        // + 7 w przkonten _c by uniknąć ujemnych indeksów
        return _a[j] && _b[i + j] && _c[i - j + 7];
    }

    private void _printSolution() {
        Console.WriteLine("Rozwiązanie problemu 8 hetmanów:\n");

        Console.Write("   ");
        for (int j = 0; j < 8; j++) {
            Console.Write($" {j + 1}  ");
        }
        Console.WriteLine();

        for (int i = 0; i < 8; i++) {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < 8; j++) {
                if (_x[i] == j) {
                    Console.Write(" Q  ");
                } else {
                    Console.Write(" .  ");
                }
            }
            Console.WriteLine();
        }
    }

    private void _solve8QP(int i, ref bool q) {
        int j;
        for (j = 0; j < 8 && !q; j++) {
            if (_isSafe(i, j)) {
                _x[i] = j;
                _a[j] = false;
                _b[i + j] = false;
                _c[i - j + 7] = false;
                if (i == 7) {
                    q = true;
                } else {
                    _solve8QP(i + 1, ref q);
                    if (!q) {
                        _a[j] = true;
                        _b[i + j] = true;
                        _c[i - j + 7] = true;
                    }
                }
            }
        }
    }

    private void _solve8QPALL(int i)
    {
        for (int j = 0; j < 8; j++)
        {
            if (_isSafe(i, j))
            {
                // Postaw hetmana
                _x[i] = j;
                _a[j] = false;
                _b[i + j] = false;
                _c[i - j + 7] = false;

                if (i == 7)
                {
                    // Znaleziono kompletne rozwiązanie
                    _solutionsCount++;
                    _printSolution();
                }
                else
                {
                    // Idź do następnego wiersza
                    _solve8QPALL(i + 1);
                }

                // Zawsze wykonaj backtracking
                _a[j] = true;
                _b[i + j] = true;
                _c[i - j + 7] = true;
            }
        }
    }

    public void Run(bool runAll = false) {
        if (runAll)
        {
            _solve8QPALL(0);
            Console.WriteLine($"\nLiczba wszystkich znalezionych rozwiązań: {_solutionsCount}");
            return;
        }
        
        bool q = false;
        _solve8QP(0, ref q);
        if (!q) {
            Console.WriteLine("Rozwiązanie problemu 8 hetmanów nie istnieje.");
        } else {
            _printSolution();
        }
    }
}