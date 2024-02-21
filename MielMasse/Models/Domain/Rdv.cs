
using System.ComponentModel.DataAnnotations;

namespace MielMasse.Models.Domain
{
    public class Rdv
    {
        [Key]
        public Guid Id { get; set; }
        public string UtilisateurId { get; set; }
        public DateTime DateHeure { get; set; }
        public string Adresse { get; set; }
        public int NbReduc { get; set; }
        public int Tarif { get; set; }


    }
}
