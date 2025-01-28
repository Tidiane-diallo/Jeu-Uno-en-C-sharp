using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetUno
{
    internal abstract class CarteSpecial : Carte
    {
        
        public CarteSpecial(string couleur, string symbole) : base(couleur,symbole)
        {
            
        }

        //public override void Action();
    }

}
