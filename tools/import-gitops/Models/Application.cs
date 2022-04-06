using k8s;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace import_gitops.Models
{
    internal class Application : Object
    {
        public string? Name { get; set; }
        public string? Version { get; set; }

        public List<IKubernetesObject>? Objects { get; set; }

        public override string ToString()
        {

            var sb = new StringBuilder();

            sb.AppendLine($"{Name} {Version}");

            foreach (var kubernetesObject in Objects)
            {
                sb.AppendLine($"\t{kubernetesObject.Kind}");    
            }

            return sb.ToString();
        }
    }
}
