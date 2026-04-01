using System;

namespace tpSerialisation
{
    public class JeuVideo
    {
        private string titre;
        private string studio;
        private double prix;

        public JeuVideo()
        {
            titre = string.Empty;
            studio = string.Empty;
            prix = 0.0;
        }

        public JeuVideo(string titre, string studio, double prix)
        {
            this.titre = titre;
            this.studio = studio;
            this.prix = prix;
        }

        public string Titre
        {
            get => titre;
            set => titre = value;
        }

        public string Studio
        {
            get => studio;
            set => studio = value;
        }

        public double Prix
        {
            get => prix;
            set => prix = value;
        }

        public override string ToString()
        {
            return $"{Titre} - {Studio} - {Prix:0.00} €";
        }
    }
}
