using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using web_ui.Models;
using System.Collections;

namespace web_ui.Repositories
{
  public class KubernetesRepository : IKubernetesRepository
  {
    private readonly Kubernetes _client;

    public KubernetesRepository()
    {
      // _client = new Kubernetes(
      //   KubernetesClientConfiguration.InClusterConfig()
      // );

      _client = new Kubernetes(KubernetesClientConfiguration.BuildDefaultConfig());
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

    public async Task<IEnumerable> GetNamespacesAsync()
    {
      var ns = await _client.ListNamespaceAsync();
      return ns
        .Items
        .Select(n => new NamespaceModel
          {
            Name = n.Metadata.Name
        });
    }

    public async Task<string> GetLogsByPodId(string podId)
    {
      
      if (podId == string.Empty)
      {
          return "No pod selected!";
      }

      var list = _client.ListNamespacedPod("default");

      if (list.Items.Count == 0)
      {
          return "No pod selected!";
      }

      var pod = list.Items[0];


      var response = await _client.ReadNamespacedPodLogWithHttpMessagesAsync(
          pod.Metadata.Name,
          pod.Metadata.NamespaceProperty, follow: true).ConfigureAwait(false);
      var stream = response.Body;
      //stream.CopyTo(Console.OpenStandardOutput());

      return stream.ToString();

    }

  }
}
