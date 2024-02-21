
using System.ComponentModel.DataAnnotations;

namespace MielMasse.Models.Domain
{
    public class Utilisateur
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Gsm { get; set; }
        public string? Adresse { get; set; }
        public Utilisateur? Parrain { get; set; }
        public Guid? ParrainId { get; set; }
        public int NbFilleuls { get; set; }
        public int NbFilleulsUsed { get; set; }
        public string? Status { get; set; }
        public string? Preferences { get; set; }


    }
}
