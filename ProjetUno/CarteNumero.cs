using System;

namespace ProjetUno
{
    internal class CarteNumero : Carte
    {
        private int numero;

        public CarteNumero(string couleur, int numero) : base(couleur, numero.ToString())
        {
            this.numero = numero;
        }

        public int Numero
        {
            get { return numero; }
        }

        public override void Afficher()
        {
            ConsoleColor couleurConsole;
            switch (couleur)
            {
                case "V": couleurConsole = ConsoleColor.Green; break;
                case "B": couleurConsole = ConsoleColor.Blue; break;
                case "J": couleurConsole = ConsoleColor.Yellow; break;
                case "R": couleurConsole = ConsoleColor.Red; break;
                default:  couleurConsole = ConsoleColor.White;break;
            }
            Console.ForegroundColor = couleurConsole;
            Console.Write($"{couleur}{numero}");
            Console.ResetColor();
        }

        public override bool EstJouable(Carte carteEnCours)
        {
            if (carteEnCours is CarteNumero carteNumero)
            {
                return this.couleur == carteNumero.couleur || this.numero == carteNumero.numero;
            }
            else if (carteEnCours is Plus2 plus2 && Jeu.nmbreCartePioche <= 0)
            {
                return this.couleur == plus2.Couleur;
            }
            else if (carteEnCours is Plus4 plus4 && Jeu.nmbreCartePioche <= 0)
            {
                return this.couleur == plus4.Couleur;
            }
            else if ( carteEnCours is ModifierCouleur modifierCouleur)
            {
                return this.couleur==modifierCouleur.Couleur;
            }
            else if( carteEnCours is SauterTour sauter)
            {
                return this.couleur==sauter.Couleur;
            }
            else if (carteEnCours is ChangerSens changer)
            {
                return this.couleur == changer.Couleur;
            }
            return false; 
        }

        public override string ToString()
        {
            return $"{couleur}{numero}";
        }

        public override void Action(ref List<Joueur> list_joueurs, ref int joueurEncours)
        {
           
        }
    }
}
