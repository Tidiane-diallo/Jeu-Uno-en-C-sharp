using System;

namespace ProjetUno
{
    internal class ModifierCouleur : CarteSpecial
    {
        public ModifierCouleur(string couleur, string symbole) : base(couleur, symbole) { }

        public override void Afficher()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{couleur}{symbole}");
            Console.ResetColor();
        }

        public override void Action(ref List<Joueur> list_joueurs, ref int joueurEncours)
        {
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
            if(Jeu.nmbreCartePioche == 0)
            {
                return true;
            }
            else
                return false;
        }
        public override string ToString()
        {
            return $"{couleur}{symbole}";
        }
    }
}
