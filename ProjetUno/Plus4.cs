using System;

namespace ProjetUno
{
    internal class Plus4 : CarteSpecial
    {
        public Plus4(string couleur, string symbole) : base(couleur, symbole)
        {
        }

        public override void Afficher()
        {
            Console.ForegroundColor = ConsoleColor.White; 
            Console.Write($"{couleur}+4");
            Console.ResetColor();
        }

        public override void Action(ref List<Joueur> list_joueurs, ref int joueurEncours)
        {
            Jeu.nmbreCartePioche += 4;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nL'adversaire doit piocher {Jeu.nmbreCartePioche} cartes !");
            Console.ResetColor();
            string nouvelleCouleur;
            do
            {
                Console.Write("\nChoisissez une nouvelle couleur (V, B, J, R) : ");
                nouvelleCouleur = Console.ReadLine()?.ToUpper();
            } while (nouvelleCouleur != "V" && nouvelleCouleur != "B" && nouvelleCouleur != "J" && nouvelleCouleur != "R");

            couleur = nouvelleCouleur;
        }

        public override bool EstJouable(Carte carte)
        {
            if (carte is CarteNumero || carte is CarteSpecial)
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
