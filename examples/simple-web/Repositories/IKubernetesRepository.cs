using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using KubeInspector.Models;
using System.Collections;

namespace KubeInspector.Repositories
{
  public interface IKubernetesRepository
  {
    Task<IEnumerable> GetPodsAsync(string ns = "default");
  }

  public class KubernetesRepository : IKubernetesRepository
  {
    private readonly Kubernetes _client;

    public KubernetesRepository()
    {
      _client = new Kubernetes(
        KubernetesClientConfiguration.InClusterConfig()
      );
    }

    public async Task<IEnumerable> GetPodsAsync(string ns = "default")
    {
      var pods = await _client.ListNamespacedPodAsync(ns);
      return pods
        .Items
        .Select(p => new PodListModel
          {
            Name = p.Metadata.Name,
            Id = p.Metadata.Uid,
            NodeName = p.Spec.NodeName
        });
    }
  }
}
