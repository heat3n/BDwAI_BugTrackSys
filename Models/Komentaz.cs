namespace BDwAI_BugTrackSys.Models
{
    public class Komentarz
    {
        public int Id { get; set; }
        public string Tresc { get; set; }
        public DateTime DataDodania { get; set; }
        public string Autor { get; set; }
        public int ZgloszenieId { get; set; }
        public virtual Zgloszenie? Zgloszenie { get; set; }
    }
}