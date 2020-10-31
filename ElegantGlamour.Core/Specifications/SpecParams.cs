namespace ElegantGlamour.Core.Specifications
{
    public abstract class SpecParams
    {
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