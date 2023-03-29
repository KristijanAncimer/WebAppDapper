using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDapper.Models
{
    public class Stroj
    {
        public int Id { get; set; }
        [DisplayName("Naziv stroja")]
        public string naziv_stroja { get; set; }
    }
}
