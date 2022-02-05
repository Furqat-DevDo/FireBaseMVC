using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FireBaseMVC.Models;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FireBaseMVC.Controllers;

public class HomeController : Controller
{
    IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "YFzrUL9OO2iORvZgyynkYsrh4QE7SqrqoOv6yQpe",
        BasePath = "https://capable-boulder-323806-default-rtdb.europe-west1.firebasedatabase.app/"
    };
    IFirebaseClient client;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        client = new FirebaseClient(config);
        var response = client.Get("UserModel");
        dynamic JsonResult = JsonConvert.DeserializeObject(response.Body);
        var list = new List<UserModel>();
        foreach (var item in JsonResult)
        {
            list.Add(JsonConvert.DeserializeObject<UserModel>(((JProperty)item).Value.ToString()));
        }
        return View(list);
    }
    [HttpPost]
    public IActionResult Create([FromForm] UserModel model)
    {
        var client=new FirebaseClient(config);
        client.Push("UserModel/", model);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
