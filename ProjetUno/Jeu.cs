using ProjetUno;
using System;

internal class Jeu
{
    private List<Joueur> joueurs_list;
    private List<Carte> carte_list;
    Carte carteEnCours;
    Joueur joueurEncours;
    public static int nmbreCartePioche = 0;

    public Jeu()
    {
        joueurs_list = new List<Joueur>();
        carte_list = new List<Carte>();
    }

    public Joueur JoueurEnCours
    {
        get{ return joueurEncours; }set {joueurEncours = value;}
    }
    public List<Joueur> Joueurs_list
    {
        get { return joueurs_list; }
    }

    public List<Carte> Carte_list
    {
        get { return carte_list; }
    }
    public Carte CarteEnCours
    {
        get { return carteEnCours; }
        set { carteEnCours = value; }
    }

    public void CreerCarte()
    {
        string[] couleurs = { "V", "B", "J", "R" };

        foreach (string couleur in couleurs)
        {
            carte_list.Add(new CarteNumero(couleur, 0));

            for (int i = 1; i <= 9; i++)
            {
                carte_list.Add(new CarteNumero(couleur, i));
                carte_list.Add(new CarteNumero(couleur, i));
            }

            carte_list.Add(new Plus2(couleur, "+2"));
            carte_list.Add(new Plus2(couleur, "+2"));
            carte_list.Add(new SauterTour(couleur, "@"));
            carte_list.Add(new SauterTour(couleur, "@"));
            carte_list.Add(new ChangerSens(couleur, "><"));
            carte_list.Add(new ChangerSens(couleur, "><"));
        }

        for (int i = 0; i < 4; i++)
        {
            carte_list.Add(new Plus4("n", "+4"));
            carte_list.Add(new ModifierCouleur("n", "#"));
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✅ Les cartes ont été créées.");
        Console.ResetColor();
    }

    public void DistribuerCarte()
    {
        Random random = new Random();

        foreach (Joueur joueur in joueurs_list)
        {
            for (int i = 0; i < 5; i++)
            {
                int index = random.Next(0, carte_list.Count);
                Carte carteRandom = carte_list[index];
                joueur.list_carte_joueur.Add(carteRandom);
                carte_list.RemoveAt(index);
            }
        }
        int index2;
        do
        {
             index2 = random.Next(0, carte_list.Count);
             carteEnCours = carte_list[index2];
        } while (carteEnCours is CarteSpecial);
        carte_list.RemoveAt(index2);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✅ Les cartes ont été distribuées.");
        Console.ResetColor();
    }
    public void AfficherCarteList()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n✅ Voici les cartes :\n");
        Console.ResetColor();
        foreach (Carte carte in carte_list)
        {
            carte.Afficher();
            Console.Write("|");
        }
        Console.WriteLine("\n");
    }
    public void AfficherCarteEnCours()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nCarte en cours : ");
        Console.ResetColor(); 
        carteEnCours.Afficher();
    }

    public void AjouterJoueurs(int nmbrJoueur)
    {
        for (int i = 0; i < nmbrJoueur; i++)
        {
            string nomJoueur;
            bool joueurExistant;

            do
            {
                Console.Write($"\nEntrez le nom du joueur {i + 1} : ");
                nomJoueur = Console.ReadLine();

                if (nomJoueur=="")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Le nom du joueur ne peut pas être vide. Veuillez réessayer");
                    Console.ResetColor();
                    joueurExistant = true;
                    continue;
                }
                joueurExistant = false;
                foreach (var joueur in joueurs_list)
                {
                    if (joueur.Nom.Equals(nomJoueur))
                    {
                        joueurExistant = true;
                        break; 
                    }
                }

                if (joueurExistant)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ce joueur a déjà été ajouté. Veuillez entrer un nom différent");
                    Console.ResetColor();
                }
            }
            while (nomJoueur=="" || joueurExistant);

            Joueur newJoueur = new Joueur(nomJoueur);
            joueurs_list.Add(newJoueur);
        }
    }


    public void JouerTour(ref bool partieEnCours)
    {
        for (int iJoueurEnCours = 0; iJoueurEnCours < joueurs_list.Count; iJoueurEnCours++)
        {
            bool carteJouee = false;
            if (!partieEnCours) break;

            AfficherEntete();
            AfficherCartesDeTousLesJoueurs();
            AfficherCarteEnCours();

            while (!carteJouee)
            {
 
                Joueur joueur = joueurs_list[iJoueurEnCours];

                Console.Write($"\n\n{joueur.Nom} : c'est votre tour. Entrez le nom d'une carte pour jouer ou entrez 'P' pour piocher : ");
                string choix = Console.ReadLine().ToUpper();

                if (choix == "P")
                {
                    
                    joueur.Piocher(carte_list, Math.Max(nmbreCartePioche, 1));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n{joueur.Nom} a pioché {Math.Max(nmbreCartePioche, 1)} carte(s).");
                    Console.ResetColor();
                    nmbreCartePioche = 0;
                    carteJouee = true;
                }
                else
                {
                    Carte carteJoueeOption = joueur.list_carte_joueur.FirstOrDefault(c => c.ToString().ToUpper() == choix && c.EstJouable(carteEnCours));

                    if (carteJoueeOption != null)
                    {
                        carteEnCours = carteJoueeOption;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"\n{joueurs_list[iJoueurEnCours].Nom} a joué : ");
                        Console.ResetColor();
                        carteEnCours.Afficher();
                        carteJouee = true;

                        joueurs_list[iJoueurEnCours].list_carte_joueur.Remove(carteJoueeOption);

                        if (joueurs_list[iJoueurEnCours].AnnoncerUno())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\n{joueurs_list[iJoueurEnCours].Nom} a annoncé Uno.");
                            Console.ResetColor();
                        }

                        carteEnCours.Action(ref joueurs_list, ref iJoueurEnCours);

                        if (VerifierVictoire())
                        {
                            partieEnCours = false;
                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLa carte choisie n'est pas jouable. Vous devez choisir une autre carte ou piocher.");
                        Console.ResetColor();
                    }
                }
                
            }
        }
    }


    public void AfficherEntete()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\nLEGENDE");
        Console.ResetColor();
        
        Console.WriteLine("\nCartes : 'Sauter-tour' tapez @; '>< Changer Sens' tapez ><; 'Changez-couleur' tapez #");
        Console.Write("Couleurs : ");

        Console.Write(" 'Jaune' tapez ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("J ;");
        Console.ResetColor();


        Console.Write(" 'Bleu' tapez ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("B ;");
        Console.ResetColor();


        Console.Write(" 'Rouge' tapez ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("R ;");
        Console.ResetColor();

        Console.Write(" 'Vert' tapez ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("V ;");
        Console.ResetColor();


        Console.Write(" 'Noir' tapez ");
        Console.ForegroundColor = ConsoleColor.Gray; 
        Console.Write("N ;");
        Console.ResetColor();
 

        Console.WriteLine("\n"); 
    }


    public void AfficherCartesDeTousLesJoueurs()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nVoici les cartes des joueurs:\n\n");
        Console.ResetColor();
        foreach (Joueur joueur in joueurs_list)
        {
            Console.Write($"{joueur.Nom} : ");
            foreach (Carte carte in joueur.list_carte_joueur)
            {
                carte.Afficher();
                Console.Write("|");
            }
            Console.WriteLine("");
        }
    }

    public void JouerPartie()
    {
        bool partieEnCours = true;

        while (partieEnCours)
        {
            JouerTour(ref partieEnCours);
        }

        Console.WriteLine("\nLa partie est terminée !");
    }

    public bool VerifierVictoire()
    {
        foreach (Joueur joueur in joueurs_list)
        {
            if (joueur.AGagné())
            {
                AfficherFelicitation(joueur.Nom);
                return true;
            }
        }
        return false;
    }

    public void AfficherFelicitation(string nomGagnant)
    {
        Random random = new Random();
        int largeur = Console.WindowWidth; 
        int hauteur = Console.WindowHeight; 
        int nombreElements = 50; 

        Console.Clear();

        for (int iteration = 0; iteration < 30; iteration++) 
        {
            for (int i = 0; i < nombreElements; i++)
            {
                int x = random.Next(0, largeur);
                int y = random.Next(0, hauteur);

                int oldX = Console.CursorLeft;
                int oldY = Console.CursorTop;

                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = (ConsoleColor)random.Next(1, 16); 
                Console.Write(random.Next(0, 2) == 0 ? "*" : "@" ,"🎉");

                Console.SetCursorPosition(oldX, oldY); 
            }

            Thread.Sleep(100); 
            Console.Clear(); 
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n\n\n🎉 Félicitations, {nomGagnant} ! Vous avez gagné la partie ! 🎉");
        Console.ResetColor();
    }
}

