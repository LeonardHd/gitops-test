using System.Text;

namespace import_gitops.Models
{
    internal class Host : Object
    {
        public string? Name { get; set; }
        public List<Environment> Environments { get; set; } = new List<Environment>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Name);

            foreach (var environment in Environments)
            {
                sb.AppendLine($"\t{environment.Name}");

                foreach (var application in environment.Applications)
                {
                    sb.AppendLine($"\t\t{application.Name}");
                }
            }

            return sb.ToString();
        }
    }
}
