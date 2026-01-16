using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BDwAI_BugTrackSys.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        [JsonIgnore]
        public virtual ICollection<Zgloszenie>? Zgloszenia { get; set; }
    }
}