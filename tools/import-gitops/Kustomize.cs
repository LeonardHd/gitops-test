using k8s;
using k8s.Models;
using System.Text;

namespace import_gitops
{
    public class Kustomize
    {
       public static async Task<List<IKubernetesObject>> BuildAsync(string directory)
        {
            var s = Shell.Run("kubectl", $@"kustomize {directory}");

            var typeMap = new Dictionary<String, Type>();
            typeMap.Add("v1/Pod", typeof(V1Pod));
            typeMap.Add("v1/Service", typeof(V1Service));
            typeMap.Add("apps/v1/Deployment", typeof(V1Deployment));
            typeMap.Add("v1/ConfigMap", typeof(V1ConfigMap));

            using(var ms = new MemoryStream(Encoding.ASCII.GetBytes(s)))
            {
                var obj = await KubernetesYaml.LoadAllFromStreamAsync(ms, typeMap);

                return obj.ConvertAll(new Converter<object, IKubernetesObject>(ToIKubernetesObject));

            }
        }

        public static IKubernetesObject ToIKubernetesObject(object obj)
        {
            return (IKubernetesObject)obj;
        }
    }
}
