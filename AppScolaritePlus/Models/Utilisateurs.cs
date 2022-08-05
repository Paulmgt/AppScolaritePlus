using System.ComponentModel.DataAnnotations;

namespace AppScolaritePlus.Models
{
    public class Utilisateurs
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string? Adresse { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateNaissance { get; set; }

        public int? SessionsFormationId { get; set; }

        public SessionsFormation? SessionsFormation { get; set; }


    }
}
