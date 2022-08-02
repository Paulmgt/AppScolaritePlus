using System.ComponentModel.DataAnnotations;

namespace AppScolaritePlus.Models
{
    public class SessionsFormation
    {

        public int Id { get; set; }

        public string Nom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

        public int ParcoursId { get; set; }

        public Parcours? Parcours { get; set; }

        public ICollection<Utilisateurs>? Utilisateurs { get; set; } = new List<Utilisateurs>();


    }
}
