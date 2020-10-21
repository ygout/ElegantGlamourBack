namespace ElegantGlamour.Core.Specifications
{
    public class PrestationSpecParams
    {
        public int? CategoryId { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public string Group { get; set; }
    }
}