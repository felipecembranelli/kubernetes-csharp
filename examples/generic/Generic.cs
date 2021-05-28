using System;
using System.Threading.Tasks;
using k8s;
using k8s.Models;

namespace exec
{
    internal class Generic
    {
        private static async Task Main(string[] args)
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            //var generic = new GenericClient(config, "", "v1", "nodes");
            // var node = await generic.ReadAsync<V1Node>("kube0").ConfigureAwait(false);
            // Console.WriteLine(node.Metadata.Name);

            var genericPods = new GenericClient(config, "", "v1", "pods");
            var pods = await genericPods.ListNamespacedAsync<V1PodList>("default").ConfigureAwait(false);
            foreach (var pod in pods.Items)
            {
                Console.WriteLine(pod.Metadata.Name);
            }

            var genericServices = new GenericClient(config, "", "v1", "services");
            var services = await genericServices.ListNamespacedAsync<V1ServiceList>("default").ConfigureAwait(false);
            foreach (var svc in services.Items)
            {
                Console.WriteLine(svc.Metadata.Name);
            }

            var genericDeployments = new GenericClient(config, "apps", "v1", "deployments");
            var deployments = await genericDeployments.ListNamespacedAsync<V1DeploymentList>("default").ConfigureAwait(false);
            foreach (var dep in deployments.Items)
            {
                Console.WriteLine(dep.Metadata.Name);
            }

            var genericDaemonSets = new GenericClient(config, "apps", "v1", "daemonsets");
            var daemonsets = await genericDaemonSets.ListNamespacedAsync<V1DaemonSetList>("kube-system").ConfigureAwait(false);
            foreach (var d in daemonsets.Items)
            {
                Console.WriteLine(d.Metadata.Name);
            }

            // var genericIngress = new GenericClient(config, "apps", "v1", "ingress");
            // var ingresses = await genericIngress.ListNamespacedAsync<V1IngressList>("default").ConfigureAwait(false);
            // foreach (var i in ingresses.Items)
            // {
            //     Console.WriteLine(i.Metadata.Name);
            // }

            var genericReplicaset = new GenericClient(config, "apps", "v1", "replicasets");
            var replicasets = await genericReplicaset.ListNamespacedAsync<V1ReplicaSetList>("default").ConfigureAwait(false);
            foreach (var r in replicasets.Items)
            {
                Console.WriteLine(r.Metadata.Name);
            }

            // var genericJob = new GenericClient(config, "", "v1", "jobs");
            // var jobs = await genericJob.ListNamespacedAsync<V1JobList>("default").ConfigureAwait(false);
            // foreach (var r in jobs.Items)
            // {
            //     Console.WriteLine(r.Metadata.Name);
            // }
        }
    }
}
