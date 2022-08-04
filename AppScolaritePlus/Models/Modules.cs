namespace AppScolaritePlus.Models
{
    public class Modules
    {

        public int Id { get; set; }

        public string Nom { get; set; }

        public string? Descriptif { get; set; }

        public string? Logo { get; set; }

        public string? Resume { get; set; }

        public string? Infos { get; set; }

        public int? ParcoursId { get; set; }

        public int? UnitesPedagogiqueId { get; set; }

        public Parcours? Parcours { get; set; }

        public UnitesPedagogique? UnitePedagogique { get; set; }


    }
}
