using MielMasse.Models.Domain;

namespace MielMasse.Models
{
    public class AddUtilisateurViewModel
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Gsm { get; set; }
        public string Adresse { get; set; }
        public string ParrainNom { get; set; }
        public int NbFilleuls { get; set; }
        public int NbFilleulsUsed { get; set; }
        public string Status { get; set; }
        public string Preferences { get; set; }
    }
}
