using System.ComponentModel.DataAnnotations;

namespace BDwAI_BugTrackSys.Models
{
    public class Projekt
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Nazwa jest wymagane")]
        public string Nazwa { get; set; }

        public string Opis { get; set; }

        public virtual ICollection<Zgloszenie> Zgloszenia { get; set; }
    }
}