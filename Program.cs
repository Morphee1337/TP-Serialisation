using System;

namespace tpSerialisation
{
    internal class Program
    {
        private static void Main()
        {
            var gestion = new GestionJeux();
            gestion.AjouterJeu(new JeuVideo("Minecraft", "Mojang", 29.99));
            gestion.AjouterJeu(new JeuVideo("Mario Kart 8 Deluxe", "Nintendo", 59.99));
            gestion.AjouterJeu(new JeuVideo("The Witcher 3", "CD Projekt", 39.99));

            Console.WriteLine("Liste initiale");
            gestion.AfficherListe();

            const string cheminCsv = "jeux.csv";
            const string cheminXml = "jeux.xml";
            const string cheminJson = "jeux.json";

            gestion.SauvegarderCsv(cheminCsv);
            gestion.SauvegarderXml(cheminXml);
            gestion.SauvegarderJson(cheminJson);

            Console.WriteLine();

            var gestionCsv = new GestionJeux();
            gestionCsv.ChargerCsv(cheminCsv);
            Console.WriteLine("Liste chargée depuis CSV");
            gestionCsv.AfficherListe();

            Console.WriteLine();
            var gestionXml = new GestionJeux();
            gestionXml.ChargerXml(cheminXml);
            Console.WriteLine("Liste chargée depuis XML");
            gestionXml.AfficherListe();

            Console.WriteLine();
            var gestionJson = new GestionJeux();
            gestionJson.ChargerJson(cheminJson);
            Console.WriteLine("Liste chargée depuis JSON");
            gestionJson.AfficherListe();

            Console.WriteLine();
            Console.WriteLine($"Prix moyen des jeux : {gestion.CalculerPrixMoyen():0.00} €");
            gestion.AfficherJeuLePlusCher();

            var recherche = gestion.RechercherParTitre("Minecraft");
            Console.WriteLine(recherche is not null
                ? $"Jeu trouvé : {recherche}"
                : "Jeu non trouvé");
        }
    }
}
