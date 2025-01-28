using System;

namespace ProjetUno
{
    internal abstract class Carte
    {
        protected string couleur;
        protected string symbole;

        public Carte(string couleur, string symbole)
        {
            this.couleur = couleur;
            this.symbole = symbole;
        }

        public string Couleur
        {
            get { return couleur; }
            set { this.couleur = value; }
        }

        public string Symbole
        {
            get { return symbole; }
        }


        public abstract string ToString();
        public abstract void Afficher();
        public abstract void Action(ref List<Joueur> joueurs_list, ref int index);
        public abstract bool EstJouable(Carte carte);
    }
}
