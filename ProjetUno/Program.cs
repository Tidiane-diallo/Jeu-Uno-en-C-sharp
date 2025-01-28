using System;
using System.Collections.Generic;

namespace ProjetUno
{
    class Program
    { 
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Jeu Uno";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n\t\t\t\t\tBienvenue dans le Jeu Uno !\n");
            Console.ResetColor();
            Jeu jeu = new Jeu();
           // jeu.AfficherEntete();
            int n;
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\n\tEntrez le nombre de joueurs (entre 3 et 10) : ");
                    Console.ResetColor();
                    n = int.Parse(Console.ReadLine());

                    if (n < 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t❌ Il faut au moins 3 joueurs pour jouer.");
                        Console.ResetColor();
                    }
                    else if (n > 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t❌ Le nombre maximum de joueurs est 10.");
                        Console.ResetColor();
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t❌ Veuillez entrer un nombre valide.");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n\n✅ Initialisation du jeu pour {n} joueurs...");
            Console.ResetColor();

            
            jeu.CreerCarte();
            

            
            jeu.AfficherCarteList();
            

            jeu.AjouterJoueurs(n);
            jeu.DistribuerCarte();
           

       
            jeu.JouerPartie();
        }

        
    }
}
