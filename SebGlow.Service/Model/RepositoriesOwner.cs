
namespace SebGlow.Service.Model
{
    public class RepositoriesOwner
    {
        public int id { get; set; }
        public string login { get; set; }
        public string url { get; set; }

        public override bool Equals(object obj)
        {
            var toCompare = obj as RepositoriesOwner;

            if (toCompare == null)
                return false;

            return id == toCompare.id &&
                login == toCompare.login &&
                url == toCompare.url;
        }
    }
}
