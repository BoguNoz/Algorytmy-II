namespace Algorithms;

class KnightsTour {

    int[] a = {2, 1, -1, -2, -2, -1, 1, 2};
    int[] b = {1, 2, 2, 1, -1, -2, -2, -1};

    int[,] board;

    public KnightsTour(int n) {
        board = new int[n, n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                board[i, j] = -1;
            }
        }
    }

    private void printSolution() {
        Console.WriteLine("Rozwiązanie problemu skoczka:\n");
        
        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                // + 1 by skorygować indeksy od 0 zamiast od 1 jak na wykadzie
                Console.Write((board[i, j] + 1) + " ");
            }
            Console.WriteLine();
        }
    }

    private bool isSafe(int x, int y) {
        // Sprawdzenie czy współrzędne są w granicach szachownicy i czy pole nie jest już zajęte
        return (x >= 0 && x < board.GetLength(0) && y >= 0 && y < board.GetLength(1) && board[x, y] == -1);
    }

    private void solveKT(int i, int x, int y, ref bool q) {
        for (int k = 0; k < a.Length && !q; k++){
            // Obliczenie nowych współrzędnych po ruchu skoczka
            int u = x + a[k];
            int v = y + b[k];
            if (isSafe(u, v)) {
                board[u, v] = i;
                // Sparwdzennie czy postawiliśmy skoczka na ostatnim polu szachownicy
                if (i == board.GetLength(0) * board.GetLength(1) - 1) {
                    q = true;
                    return;
                } else {
                    solveKT(i + 1, u, v, ref q);
                    if (!q) {
                        board[u, v] = -1;
                    } 
                    else {
                        q = true;
                    }
                }
            }
        }
    }

    public void Run() {
        // Ustawienie początkowej pozycji skoczka na szachownicy
        board[0, 0] = 0;
        bool q = false;
        solveKT(1, 0, 0, ref q);
        if (!q) {
            Console.WriteLine("Rozwiązanie problemu skoczka nie istnieje.");
        } else {
            printSolution();
        }
    }
}