using import_gitops;
using import_gitops.Models;
using k8s.Models;
using Environment = import_gitops.Models.Environment;

var di = new DirectoryInfo("/git/gitops-test");

var Applications =  await GetApplicationsAsync(di.FullName + "/apps");
var Hosts = await GetHostsAsync(di.FullName + "/hosts");

Console.WriteLine("\nAPPLICATIONS");
foreach (var obj in Applications)
{
    Console.WriteLine(obj);
}

Console.WriteLine("\nHOSTS");
foreach (var obj in Hosts)
{
    Console.WriteLine(obj);
}


// ****** METHODS *******
async Task<List<Application>> GetApplicationsAsync(string applicationRootDirectory)
{
    var appsDirs = Directory.GetDirectories(applicationRootDirectory);

    var applications = new List<Application>();

    foreach (var app in appsDirs)
    {
        // If no versions (sub-directory) exist in directory
        if (Directory.GetDirectories(app).Count() == 0)
        {
            applications.Add(new Application
            {
                Name = new DirectoryInfo(app).Name,
                Objects = await Kustomize.BuildAsync(app)
            });
        }
        else
        {
            foreach (var version in Directory.GetDirectories(app))
            {
                applications.Add(new Application
                {
                    Name = new DirectoryInfo(app).Name,
                    Version = new DirectoryInfo(version).Name,
                    Objects = await Kustomize.BuildAsync(version)
                });
            }
        }
    }

    return applications;
}

async Task<List<Host>> GetHostsAsync(string hostRootDirectory)
{
    var hostsDirs = Directory.GetDirectories(hostRootDirectory);

    var hosts = new List<Host>();

    // Host
    foreach (var host in hostsDirs)
    {
        var _host = new Host { Name = new DirectoryInfo(host).Name };

        // Environment
        foreach (var environment in Directory.GetDirectories(host))
        {
            _host.Environments.Add(new Environment
            {
                Name = new DirectoryInfo(environment).Name,
                Applications = await GetApplicationsAsync(environment + "/apps")
            });
        }

        hosts.Add(_host);
    }

    return hosts;
}
