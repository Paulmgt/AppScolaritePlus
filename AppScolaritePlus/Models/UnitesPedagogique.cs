namespace AppScolaritePlus.Models
{
    public class UnitesPedagogique
    {
        public int Id { get; set; }

        public string? Designation { get; set; }

        public ICollection<Modules>? Modules { get; set; } = new List<Modules>();


    }
}
