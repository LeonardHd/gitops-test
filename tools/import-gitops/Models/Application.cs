namespace import_gitops.Models
{
    internal class Application : Object
    {
        public string? Name { get; set; }
        public string? Version { get; set; }

        public List<object>? Objects { get; set; }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
