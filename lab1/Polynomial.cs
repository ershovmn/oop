using System;
using System.Collections.Generic;

class Polynomial {
    public int degree;
    public SetFractions polynomial;

    public Polynomial(RaionalFraction[] pol) {
        polynomial = new SetFractions(pol);
        degree = pol.Length;
    }

    public Polynomial(SetFractions pol) {
        polynomial = pol;
        degree = pol.Length();
    }

    public static Polynomial operator + (Polynomial polynomial1, Polynomial polynomial2) {
        SetFractions polynomial3;
        SetFractions polynomial4;
        if(polynomial1.degree > polynomial2.degree) {
            polynomial3 = polynomial1.polynomial;
            polynomial4 = polynomial2.polynomial;
        }
        else {
            polynomial3 = polynomial2.polynomial;
            polynomial4 = polynomial1.polynomial;
        }
        
        for(int i = 0; i < polynomial4.Length(); i++) {
            polynomial3[i] = polynomial3[i] + polynomial4[i];
        }
        return new Polynomial(polynomial3);
    }
}