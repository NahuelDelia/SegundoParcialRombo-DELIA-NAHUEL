using SegundoParcialRombo.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcialRombo.Datos
{
    public class RepositorioRombos
    {
        private List<Rombo>? lista_Repo;
        private string? name = "romb.txt";
        private string? rutaProy = Environment.CurrentDirectory;
        private string? rutaCompleta;
        public RepositorioRombos()
        {
            lista_Repo = new List<Rombo>();
        }
        public int cantidad()
        {
            return lista_Repo!.Count;
        }
        public void agregar(Rombo rombo)
        {
            lista_Repo!.Add(rombo);
        }
        public bool existeRombo(Rombo rombo)
        {
            return lista_Repo!.Any(r => r.diagonalMenor == rombo.diagonalMenor && r.diagonalMayor == rombo.diagonalMayor && r.contorno == rombo.contorno);
        }

        public void borrarRombo(Rombo rombo)
        {
           lista_Repo!.Remove(rombo);
        }
        public void guardar()
        {
            rutaCompleta = Path.Combine(rutaProy, name);
            using (var writer = new StreamWriter(rutaCompleta!))
            {
                foreach (var rombo in lista_Repo!)
                {
                    string linea = crearLineatxt(rombo);
                    writer.WriteLine(linea);
                }
            }
        }

        private string crearLineatxt(Rombo rombo)
        {
            return $"{rombo.diagonalMenor}|{rombo.diagonalMenor}|{rombo.contorno.GetHashCode()}";

        }

        public bool exist(int diagmayor, int diagmenor)
        {
            return lista_Repo!.Any(e => e.diagonalMayor == diagmayor &&
            e.diagonalMenor == diagmenor);
        }
    }
}
