using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjetUno
{
    internal class ChangerSens : CarteSpecial
    {
        public ChangerSens(string couleur, string symbole) : base(couleur, symbole) { }

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

        public override bool EstJouable(Carte carteEnCours)
        {
            if (this.couleur == carteEnCours.Couleur && Jeu.nmbreCartePioche==0 || this.symbole == carteEnCours.Symbole)
            {
                return true;
            }
            return false;
        }

        public override void Action(ref List<Joueur> list_joueurs, ref int joueurEncours)
        {
            List<Joueur> list_gameurs = new List<Joueur>();

            if (joueurEncours > 0)
            {
                for (int i = joueurEncours - 1; i >= 0; i--)
                {
                    list_gameurs.Add(list_joueurs[i]);
                }

                for (int j = list_joueurs.Count - 1; j >= joueurEncours; j--)
                {
                    list_gameurs.Add(list_joueurs[j]);
                }
            }
            else
            {
                for (int j = list_joueurs.Count - 1; j >= 0; j--)
                {
                    list_gameurs.Add(list_joueurs[j]);
                }
            }

            list_joueurs.Clear();
            list_joueurs.AddRange(list_gameurs);

           joueurEncours = list_joueurs.Count;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nLe sens du jeu a été inversé.");
            Console.ResetColor();
        }


        public override string ToString()
        {
            return $"{couleur}{symbole}";
        }
    }
}
