using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace tpSerialisation
{
    public class GestionJeux
    {
        private List<JeuVideo> jeux;

        public GestionJeux()
        {
            jeux = new List<JeuVideo>();
        }

        [XmlArray("Jeux")]
        [XmlArrayItem("Jeu")]
        public List<JeuVideo> Jeux
        {
            get => jeux;
            set => jeux = value ?? new List<JeuVideo>();
        }

        public void AjouterJeu(JeuVideo jeu)
        {
            if (jeu == null)
            {
                throw new ArgumentNullException(nameof(jeu));
            }

            jeux.Add(jeu);
        }

        public bool SupprimerJeu(string titre)
        {
            var jeu = jeux.FirstOrDefault(j => j.Titre.Equals(titre, StringComparison.OrdinalIgnoreCase));
            if (jeu == null)
            {
                return false;
            }

            jeux.Remove(jeu);
            return true;
        }

        public void AfficherListe()
        {
            if (!jeux.Any())
            {
                Console.WriteLine("Aucun jeu à afficher.");
                return;
            }

            for (int i = 0; i < jeux.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {jeux[i]}");
            }
        }

        public void SauvegarderCsv(string chemin)
        {
            using var writer = new StreamWriter(chemin);
            foreach (var jeu in jeux)
            {
                writer.WriteLine($"{jeu.Titre};{jeu.Studio};{jeu.Prix.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        public void ChargerCsv(string chemin)
        {
            if (!File.Exists(chemin))
            {
                Console.WriteLine($"Fichier CSV introuvable : {chemin}");
                return;
            }

            jeux.Clear();
            var lignes = File.ReadAllLines(chemin);

            foreach (var ligne in lignes)
            {
                if (string.IsNullOrWhiteSpace(ligne))
                {
                    continue;
                }

                var parties = ligne.Split(';');
                if (parties.Length != 3)
                {
                    continue;
                }

                if (double.TryParse(parties[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double prix))
                {
                    jeux.Add(new JeuVideo(parties[0], parties[1], prix));
                }
            }
        }

        public void SauvegarderXml(string chemin)
        {
            var serializer = new XmlSerializer(typeof(GestionJeux));
            using var stream = File.Create(chemin);
            serializer.Serialize(stream, this);
        }

        public void ChargerXml(string chemin)
        {
            if (!File.Exists(chemin))
            {
                Console.WriteLine($"Fichier XML introuvable : {chemin}");
                return;
            }

            var serializer = new XmlSerializer(typeof(GestionJeux));
            using var stream = File.OpenRead(chemin);
            if (serializer.Deserialize(stream) is GestionJeux resultat)
            {
                jeux = resultat.jeux ?? new List<JeuVideo>();
            }
        }

        public void SauvegarderJson(string chemin)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(jeux, options);
            File.WriteAllText(chemin, json);
        }

        public void ChargerJson(string chemin)
        {
            if (!File.Exists(chemin))
            {
                Console.WriteLine($"Fichier JSON introuvable : {chemin}");
                return;
            }

            var contenu = File.ReadAllText(chemin);
            var liste = JsonSerializer.Deserialize<List<JeuVideo>>(contenu);
            jeux = liste ?? new List<JeuVideo>();
        }

        public double CalculerPrixMoyen()
        {
            if (!jeux.Any())
            {
                return 0.0;
            }

            return jeux.Average(j => j.Prix);
        }

        public void AfficherJeuLePlusCher()
        {
            if (!jeux.Any())
            {
                Console.WriteLine("Aucun jeu pour calculer le prix le plus cher.");
                return;
            }

            var jeuPlusCher = jeux.OrderByDescending(j => j.Prix).First();
            Console.WriteLine($"Jeu le plus cher : {jeuPlusCher}");
        }

        public JeuVideo? RechercherParTitre(string titre)
        {
            return jeux.FirstOrDefault(j => j.Titre.Equals(titre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
