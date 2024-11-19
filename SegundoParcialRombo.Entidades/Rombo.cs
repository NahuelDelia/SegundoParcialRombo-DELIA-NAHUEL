using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcialRombo.Entidades
{
    public class Rombo
    {
        public int diagonalMayor { get; set; }
        public int diagonalMenor { get; set; }
        public Contorno contorno { get; set; }

        public double Lado()
        {
            return Math.Sqrt(Math.Pow(diagonalMayor / 2, 2) + Math.Pow(diagonalMenor / 2, 2));
        }
        public double getArea()
        {
            return (diagonalMayor * diagonalMenor) / 2;
        }
        public double getPerimetro()
        {
            return 4 * Lado();
        }
    }
}
