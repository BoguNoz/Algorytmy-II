namespace Algorithms;

class KnightsTour {

    private readonly int[] _a = {2, 1, -1, -2, -2, -1, 1, 2};
    private readonly int[] _b = {1, 2, 2, 1, -1, -2, -2, -1};

    private readonly int[,] _board;
    private int _solutionsCount = 0;

    public KnightsTour(int n) {
        _board = new int[n, n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                _board[i, j] = -1;
            }
        }
    }

    private void _printSolution() {
        Console.WriteLine("Rozwiązanie problemu skoczka:\n");
        
        for (int i = 0; i < _board.GetLength(0); i++) {
            for (int j = 0; j < _board.GetLength(1); j++) {
                // + 1 by skorygować indeksy od 0 zamiast od 1 jak na wykadzie
                Console.Write((_board[i, j] + 1) + " ");
            }
            Console.WriteLine();
        }
    }

    private bool _isSafe(int x, int y) {
        // Sprawdzenie czy współrzędne są w granicach szachownicy i czy pole nie jest już zajęte
        return (x >= 0 && x < _board.GetLength(0) && y >= 0 && y < _board.GetLength(1) && _board[x, y] == -1);
    }

    
    // Eksploruje tylko jedno rozwiązanie
    private void _solveKT(int i, int x, int y, ref bool q) {
        for (int k = 0; k < _a.Length && !q; k++){
            // Obliczenie nowych współrzędnych po ruchu skoczka
            int u = x + _a[k];
            int v = y + _b[k];
            
            if (_isSafe(u, v)) {
                _board[u, v] = i;
                // Sprawdzenię czy postawiliśmy skoczka na ostatnim polu szachownicy
                if (i == _board.GetLength(0) * _board.GetLength(1) - 1) {
                    q = true; // Znaleziono rozwiązanie ustawiamy flagę
                    return;
                } 
                _solveKT(i + 1, u, v, ref q);
                
                if (!q) {
                    _board[u, v] = -1; // cofamy ruch tylko gdy NIE znaleziono rozwiązania
                } 
                else {
                    q = true;
                }
            }
        }
    }
    
    
    // Eksploruje wszystkie rozwiązania
    private void _solveKTAll(int i, int x, int y)
    {
        // Warunek znalezienia pełnego rozwiązania
        if (i == _board.GetLength(0) * _board.GetLength(1))
        {
            _solutionsCount++;
            _printSolution();
            return;
        }
        
        for (int k = 0; k < _a.Length; k++) {
            int u = x + _a[k];
            int v = y + _b[k];

            if (_isSafe(u, v)) {
                _board[u, v] = i;
                _solveKTAll(i + 1, u, v);
                _board[u, v] = -1; // backtracking
            }
        }
    }

    public void Run(bool runAll = false) {

        if (runAll)
        {
            _board[0, 0] = 0;
            _solveKTAll(1, 0, 0);

            Console.WriteLine($"\nLiczba wszystkich znalezionych rozwiązań: {_solutionsCount}");
            return;
        }
        
        // Ustawienie początkowej pozycji skoczka na szachownicy
        _board[0, 0] = 0;
        bool q = false;
        _solveKT(1, 0, 0, ref q);
        if (!q) {
            Console.WriteLine("Rozwiązanie problemu skoczka nie istnieje.");
        } else {
            _printSolution();
        }
    }
    
}