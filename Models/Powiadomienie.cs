using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BDwAI_BugTrackSys.Models
{
    public class Powiadomienie
    {
        public int Id { get; set; }
        public string Tresc { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public bool CzyPrzeczytane { get; set; } = false;
        public string UzytkownikId { get; set; }
        public virtual IdentityUser Uzytkownik { get; set; }
        public int ZgloszenieId { get; set; }
        public virtual Zgloszenie Zgloszenie { get; set; }
    }
}