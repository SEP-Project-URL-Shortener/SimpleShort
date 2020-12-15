/*
 * HomeController:
 * Responsible for user interface.
 * Depending on the URL, a specific page will be shown. We can do calculations or data grabbing right in here.
 */

// Included Libraries
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleShort.Models;
using System;
using System.Threading.Tasks;
using SimpleShort.Interfaces;

namespace SimpleShort.Controllers
{
    public class HomeController : Controller
    {
        // Interface of Url data Repository
        private readonly IShortenUrlRepository _repository;

        // Interface of the log service
        private readonly ILogService _logger;

        // Default constructor gets repository and logger via dependency injection
        public HomeController(IShortenUrlRepository repository, ILogService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Get the Index.cshtml page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Preform a post request on the index page
        [HttpPost]
        public async Task<IActionResult> Index(CreateShortenedUrlModel model)
        {
            // Get the users IP address
            model.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // CreateShortenedUrlModel case as json string
            var modelJson = JsonConvert.SerializeObject(model);

            try
            {
                // Create a new url from model
                var newShortUrl = await _repository.CreateShortedUrl(model);

                

                // SimpleShortUrl case as json string
                var newShirtUrlJson = JsonConvert.SerializeObject(newShortUrl);

                // Log information
                await _logger.Log("CreateShortedUrl", $"HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], NewShirtUrl: [{newShirtUrlJson}]");

                ViewBag.CreateMessage = newShortUrl == null
                    ? "Short URL already taken, please try another short url."
                    : "Newly created url: https://simpleshort.com/" + newShortUrl.Path;
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("GetAllShortenedUrls", "HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], Error: {ex}");
                
                ViewBag.CreateMessage = "Short URL already taken, please try another short url.";
            }

            // Return index.cshtml with the model
            return View("Index", model);
        }

        // Catch any request other than the index page
        [Route("{path}")]
        public async Task<IActionResult> Catch(string path)
        {

            // If the url is a swagger url, redirect to swagger
            if (path.Contains("swagger"))
                return Redirect("swagger/index.html");

            try
            {
                // Get the long url for the given path
                var longUrl = await _repository.GetLongUrl(path);
                
                // Log information
                await _logger.Log("GetLongUrl", $"HttpGet:{path}", $"OriginalUrl:{longUrl}");

                // if the long url does not start with http, add it then redirect.
                return longUrl != null && !longUrl.Contains("http")
                    ? Redirect($"http://{longUrl}")
                    : Redirect(longUrl);
            }
            catch (Exception ex)
            {

                // Log information
                await _logger.Log("GetLongUrl", $"HttpGet:{path}", $"Error: {ex}");
            }

            // return to index.cshtml
            return View("Index");
        }
    }
}