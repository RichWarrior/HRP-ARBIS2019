namespace HRP.Arbis.Models
{
    public class MenuViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Nested { get; set; }
        public int Rank { get; set; }
    }
}