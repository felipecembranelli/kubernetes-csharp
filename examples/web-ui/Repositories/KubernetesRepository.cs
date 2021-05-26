using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k8s;
using web_ui.Models;
using System.Collections;
using k8s.Models;

namespace web_ui.Repositories
{
  public class KubernetesRepository : IKubernetesRepository
  {
    private readonly Kubernetes _client;
    private readonly KubernetesClientConfiguration _config;

    public KubernetesRepository()
    {
      // _client = new Kubernetes(
      //   KubernetesClientConfiguration.InClusterConfig()
      // );

      _config = KubernetesClientConfiguration.BuildDefaultConfig();

      _client = new Kubernetes(_config);
    }

    public ClusterModel GetClusterInfo()
    {
        return new ClusterModel
      {
        BaseUri = _client.BaseUri.AbsoluteUri.ToString(),
        Host = _config.Host,
        CurrentContext = _config.CurrentContext,
      };
    }

  public List<NodeModel> GetNodes()
    {
      List<NodeModel> nodes = new List<NodeModel>();

      k8s.Models.V1NodeList nodeList;

      //    client.ListNamespacedPod("default");
      //                 client.ListNode();
      //                 client.ListNamespacedDeployment("default");

      nodeList = _client.ListNode();

      foreach (var item in nodeList.Items)
      {
        NodeModel n = new NodeModel 
        {
          Uid = item.Uid(),
          HostName = item.Name(),
          PodIP = item.Spec.PodCIDR,
          Labels = item.Labels()
        };
        nodes.Add(n);
      }

      return nodes;
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
