namespace Crowdfund.Core.Model {
    public class Multimedia 
    {
        public int Id { get; set; }
        public Project project { get; set; }

        public string url { get; set; }

        public MultimediaCategory multimediaCategory { get; set; }
    }
}