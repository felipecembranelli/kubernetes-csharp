using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_ui.Models;
using web_ui.Repositories;

namespace web_ui.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IKubernetesRepository _repository;

        public HomeController(ILogger<HomeController> logger, IKubernetesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("pods/{ns}")]
        public async Task<IActionResult> GetPodsByNamespaces([FromQuery (Name = "ns")] string ns = "default")
        {
            //return View();
            if (!ModelState.IsValid)
            {
                return BadRequest ();
            }

            try
            {
                var pods = await _repository.GetPodsAsync (ns);
                //return Ok (pods);
                return View("Pods", pods);
            }
            catch(Exception)
            {
                return StatusCode (500);
            }
        }

        [HttpGet]
        [Route("namespaces")]
        public async Task<IActionResult> GetNamespaces()
        {
            //return View();
            if (!ModelState.IsValid)
            {
                return BadRequest ();
            }

            try
            {
                var namespaces = await _repository.GetNamespacesAsync();
                return View("Namespaces", namespaces);
            }
            catch(Exception)
            {
                return StatusCode (500);
            }
        }

        [HttpGet]
        [Route("pods/logs/{podId}")]
        public async Task<IActionResult> GetLogsByPod([FromQuery (Name = "podId")] string podId)
        {
            //return View();
            if (!ModelState.IsValid)
            {
                return BadRequest ();
            }

            try
            {
                var text = await _repository.GetLogsByPodId ("pod1");
                //return Ok (pods);
                return View("PodLogs", text);
            }
            catch(Exception)
            {
                return StatusCode (500);
            }
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
