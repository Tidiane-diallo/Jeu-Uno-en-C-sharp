using System;
using System.Collections.Generic;

namespace ProjetUno
{
    internal class Joueur
    {
        private string nom;
        public List<Carte> list_carte_joueur;

        public Joueur(string nom)
        {
            this.nom = nom;
            list_carte_joueur = new List<Carte>();
        }

        public string Nom
        {
            get { return nom; }
        }

        public void AfficherCarteJoueur()
        {
            Console.Write($"\n\n{nom} : ");
            foreach (Carte carte in list_carte_joueur)
            {
                carte.Afficher();
                Console.Write(" | ");
            }
        }

        public bool AnnoncerUno()
        {
            return list_carte_joueur.Count == 1;
        }

        public bool AGagné()
        {
            return list_carte_joueur.Count == 0;
        }

        public bool JouerCarte(Carte carte, Carte carteEnCours)
        {
            if (carte.EstJouable(carteEnCours))
            {
                list_carte_joueur.Remove(carte);
                carte.Afficher();
                return true;
            }
            return false;
        }

        public void Piocher(List<Carte> carte_list, int nombreDeCartes)
        {
            Random random = new Random();

            for (int i = 0; i < nombreDeCartes; i++)
            {
                int index = random.Next(0, carte_list.Count);
                Carte cartePiochee = carte_list[index];
                list_carte_joueur.Add(cartePiochee);
                carte_list.RemoveAt(index);
            }

        }
    }
}
