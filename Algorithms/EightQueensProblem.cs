namespace Algorithms;

class EightQueensProblem {

    // wiersze
    bool[] a = {true, true, true, true, true, true, true, true};
    // przekątne
    bool[] b = {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true};
    bool[] c = {true, true, true, true, true, true, true, true, true, true, true, true, true, true, true};
    // Kolumna w ktrej znajduje się hetman w danym wierszu
    int[] x = {-1, -1, -1, -1, -1, -1, -1, -1};
    
    private bool isSafe(int i, int j) {
        // + 7 w przkonten c by uniknąć ujemnych indeksów
        return a[j] && b[i + j] && c[i - j + 7];
    }

    private void printSolution() {
        Console.WriteLine("Rozwiązanie problemu 8 hetmanów:\n");

        Console.Write("   ");
        for (int j = 0; j < 8; j++) {
            Console.Write($" {j + 1}  ");
        }
        Console.WriteLine();

        for (int i = 0; i < 8; i++) {
            Console.Write($"{i + 1,2} ");
            for (int j = 0; j < 8; j++) {
                if (x[i] == j) {
                    Console.Write(" Q  ");
                } else {
                    Console.Write(" .  ");
                }
            }
            Console.WriteLine();
        }
    }

    private void solve8QP(int i, ref bool q) {
        int j;
        for (j = 0; j < 8 && !q; j++) {
            if (isSafe(i, j)) {
                x[i] = j;
                a[j] = false;
                b[i + j] = false;
                c[i - j + 7] = false;
                if (i == 7) {
                    q = true;
                } else {
                    solve8QP(i + 1, ref q);
                    if (!q) {
                        a[j] = true;
                        b[i + j] = true;
                        c[i - j + 7] = true;
                    }
                }
            }
        }
    }

    public void Run() {
        bool q = false;
        solve8QP(0, ref q);
        if (!q) {
            Console.WriteLine("Rozwiązanie problemu 8 hetmanów nie istnieje.");
        } else {
            printSolution();
        }
    }
}