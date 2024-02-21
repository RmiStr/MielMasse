using MielMasse.Models.Domain;

namespace MielMasse.Models
{
    public class UpdateUtilisateurViewModel
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Gsm { get; set; }
        public string? Adresse { get; set; }
        public Utilisateur? Parrain { get; set; }
        public Guid? ParrainId { get; set; }
        public int NbFilleuls { get; set; }
        public int NbFilleulsUsed { get; set; }
        public string Status { get; set; }
        public string? Preferences { get; set; }
    }
}
