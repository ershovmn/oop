using System;

class RaionalFraction : IComparable<RaionalFraction> {
    private int n, m;

    private static int NOD (int n, int m) {
        while(n != 0 && m != 0) {
            if(n > m) {
                n -= m;
            }
            else {
                m -= n;
            }
        }

        return Math.Max(n, m); 
    }
    
    public RaionalFraction() {
        this.n = 1;
        this.m = 1;
    }
    public RaionalFraction(int n1, int m1) {
        if(m1 == 0) {
            throw new ArithmeticException();
        }
        n = n1;
        m = m1;
    }

    public RaionalFraction(int n1) {
        n = n1;
        m = 1;
    }

    public static RaionalFraction operator +(RaionalFraction RT1, RaionalFraction RT2) {
        int n = RT1.m*RT2.n + RT2.m*RT1.n;
        int m = RT1.m*RT2.m;
        int nod = NOD(n, m);
        n = n / nod;
        m = m / nod;
        return new RaionalFraction(n, m);
    }

    public static Boolean operator < (RaionalFraction RT1, RaionalFraction RT2) {
        int n1 = RT1.n * RT2.m;
        int n2 = RT2.n * RT1.m;
        return n1 < n2;
    }

    public static Boolean operator > (RaionalFraction RT1, RaionalFraction RT2) {
        int n1 = RT1.n * RT2.m;
        int n2 = RT2.n * RT1.m;
        return n1 > n2;
    }

    public void Print() {
        Console.WriteLine($"{n}/{m}");
    }

    public int CompareTo(RaionalFraction other)
    {
        if(this < other) {
            return -1;
        }
        else if(this > other) {
            return 1;
        }
        return 0;
    }
}