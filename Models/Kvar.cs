using System.ComponentModel;

namespace WebAppDapper.Models
{
    public class Kvar
    {
        public int Id { get; set; }
        [DisplayName("Naziv Kvara")]
        public string naziv_kvara { get; set; }
        [DisplayName("Naziv Stroja")]
        public string naziv_stroja { get; set; }
        [DisplayName("Prioritet")]
        public string prioritet { get; set; }
        [DisplayName("Vrijeme pocetka")]
        public DateTime vrijeme_pocetka { get; set; }
        [DisplayName("Vrijeme zavrsetka")]
        public DateTime? vrijeme_zavrsetka { get; set; }
        [DisplayName("Detaljni opis")]
        public string detaljni_opis { get; set; }
        [DisplayName("Status Kvara")]
        public bool status_kvara { get; set; }
    }
}
