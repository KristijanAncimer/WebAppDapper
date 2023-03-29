namespace WebAppDapper.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string naziv { get; set; }
        public List<Kvar>? Kvarovi { get; set; }
        public long trajanje { get; set; }
    }
}
