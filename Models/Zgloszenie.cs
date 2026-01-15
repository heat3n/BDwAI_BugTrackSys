using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BDwAI_BugTrackSys.Models
{
    public class Zgloszenie
    {
        public int Id { get; set; }

        [Required]
        public string Temat { get; set; }

        [Required]
        public string Opis { get; set; }

        public DateTime DataUtworzenia { get; set; }

        public int ProjektId { get; set; }
        public virtual Projekt? Projekt { get; set; }

        public int PriorytetId { get; set; }
        public virtual Priorytet? Priorytet { get; set; }

        public int StatusId { get; set; }
        public virtual Status? Status { get; set; }
        public string? UzytkownikId { get; set; }
        public virtual IdentityUser? Uzytkownik { get; set; }
        public virtual ICollection<Komentarz>? Komentarze { get; set; }
    }
}