using System;

namespace ProjetUno
{
    internal class SauterTour : CarteSpecial
    {
        
        public SauterTour(string couleur, string symbole) : base(couleur, symbole)
        {
            
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
                default: couleurConsole = ConsoleColor.White; break;
            }

            Console.ForegroundColor = couleurConsole;
            Console.Write($"{couleur}{symbole}"); 
            Console.ResetColor();
        }

        public override void Action(ref List<Joueur> list_joueurs, ref int joueurEncours)
        {
            int indexJoueurEnCours = joueurEncours;

            
            joueurEncours = (indexJoueurEnCours + 1) % list_joueurs.Count;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nLe tour du joueur {list_joueurs[(indexJoueurEnCours + 1) % list_joueurs.Count].Nom} a été sauté.");
            Console.ResetColor();
        }

        public override bool EstJouable(Carte carteEnCours)
        {
            if (this.couleur == carteEnCours.Couleur && Jeu.nmbreCartePioche == 0 || this.symbole == carteEnCours.Symbole)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{couleur}{symbole}";
        }
    }
}
