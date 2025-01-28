using System;

namespace ProjetUno
{
    internal class Plus2 : CarteSpecial
    {
        public Plus2(string couleur, string symbole) : base(couleur, symbole)
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

        public override void Action(ref List<Joueur> list_joueurs, ref int joueur)
        {
            Jeu.nmbreCartePioche += 2;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nLe prochain joueur doit piocher {Jeu.nmbreCartePioche} cartes.");
            Console.ResetColor();
        }

        public override bool EstJouable(Carte carteEnCours)
        {
            if ((this.couleur == carteEnCours.Couleur) || (carteEnCours is Plus2) || (carteEnCours is Plus4))
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
