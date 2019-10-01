using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class SetFractions {
    private List<RaionalFraction> fractions = new List<RaionalFraction>();
    private Dictionary<RaionalFraction, int> countFractionsMore = new Dictionary<RaionalFraction, int>();
    private Dictionary<RaionalFraction, int> countFractionsLess = new Dictionary<RaionalFraction, int>();

    private RaionalFraction max;
    private RaionalFraction min;

    public SetFractions(RaionalFraction[] array) {
        foreach(RaionalFraction RF in array) {
            this.Add(RF);
        }
    }

    public int Length() {
        return fractions.Count;
    }

    public RaionalFraction Max() {
        max = fractions.Max();
        return max;
    }

    public RaionalFraction Min() {
        min = fractions.Min();
        return min;
    }

    public void Add(RaionalFraction RF) {
        fractions.Add(RF);
        if(max == null) {
            max = RF;
            min = RF;
            return;
        }
        if(RF > max) {
            max = RF;
        }
        if(RF < min) {
            min = RF;
        }
    }

    public int CountMoreThanFraction(RaionalFraction RF) {
        try {
            return countFractionsMore[RF];
        }
        catch {}
        int count = 0;
        foreach(RaionalFraction rf in fractions) {
            if(rf > RF) {
                count++;
            }
        }
        countFractionsMore.Add(RF, count);
        return count;
    }

    public int CountLessThanFraction(RaionalFraction RF) {
        try {
            return countFractionsLess[RF];
        }
        catch {}
        int count = 0;
        foreach(RaionalFraction rf in fractions) {
            if(rf < RF) {
                count++;
            }
        }
        countFractionsLess.Add(RF, count);
        return count;
    }

    public RaionalFraction At(int n) {
        return fractions[n];
    }

    public RaionalFraction this[int n] {
        set {
            fractions[n] = value;
        }
        get {
            return fractions[n];
        }
    }

    public void LoadFromFile (string path) {
        if(File.Exists(path)) {
            using (StreamReader sr = new StreamReader(path)) {
                string s = "";
                while(s != null) {
                    s = sr.ReadLine();
                    string[] array = s.Split('/');
                    try {
                        fractions.Add(new RaionalFraction(Convert.ToInt32(array[0]), Convert.ToInt32(array[1])));
                    }
                    catch {
                        Console.WriteLine("error");
                    }
                }
            }
        }
    }
}