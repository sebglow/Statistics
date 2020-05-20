namespace SebGlow.GitHub.Model
{
    public class Repository
    {
        public Owner owner { get; set; }
        public string name { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public int forks_count { get; set; }
        public int size { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Repository;

            if (toCompare == null)
                return false;

            return stargazers_count == toCompare.stargazers_count &&
                forks_count == toCompare.forks_count &&
                watchers_count == toCompare.watchers_count &&
                size == toCompare.size &&
                name == toCompare.name &&
                owner.Equals(toCompare.owner);
        }
    }
}
