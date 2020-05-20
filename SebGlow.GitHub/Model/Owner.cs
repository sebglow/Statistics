namespace SebGlow.GitHub.Model
{
    public class Owner
    {
        public int id { get; set; }
        public string login { get; set; }
        public string url { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as Owner;

            if (toCompare == null)
                return false;

            return id == toCompare.id &&
                login == toCompare.login &&
                url == toCompare.url;
        }
    }
}
