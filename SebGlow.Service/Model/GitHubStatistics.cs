
namespace SebGlow.Service.Model
{
    public class GitHubStatistics
    {
        public RepositoriesOwner owner { get; set; }
        public Letters letters { get; set; }
        public decimal avgStargazers { get; set; }
        public decimal avgWatchers { get; set; }
        public decimal avgForks { get; set; }
        public decimal avgSize { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as GitHubStatistics;

            if (toCompare == null)
                return false;
            
            return avgStargazers == toCompare.avgStargazers &&
                avgWatchers == toCompare.avgWatchers &&
                avgForks == toCompare.avgForks &&
                avgSize == toCompare.avgSize &&
                letters.Equals(toCompare.letters) &&
                owner.Equals(toCompare.owner);
        }
    }
}
