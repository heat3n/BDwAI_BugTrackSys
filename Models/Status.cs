using System.ComponentModel.DataAnnotations;

namespace BDwAI_BugTrackSys.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        public virtual ICollection<Zgloszenie>? Zgloszenia { get; set; }
    }
}