﻿/*
 * HomeController:
 * Responsible for user interface.
 * Depending on the URL, a specific page will be shown. We can do calculations or data grabbing right in here.
 */

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleShort.Data;
using SimpleShort.Data.LogService;
using SimpleShort.Entities;
using SimpleShort.Models;

namespace SimpleShort.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortenUrlRepository _repository;
        private readonly ILogService _logger;

        public HomeController(IShortenUrlRepository repository, ILogService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateShortenedUrlModel model)
        {
            model.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            try
            {
                var newShortUrl = await _repository.CreateShortedUrl(model);
                var modelJson = JsonConvert.SerializeObject(model);
                var newShirtUrlJson = JsonConvert.SerializeObject(newShortUrl);
                await _logger.Log("CreateShortedUrl", $"HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], NewShirtUrl: [{newShirtUrlJson}]");
                ViewBag.CreateMessage = "Newly created url: http://simpleshort/" + newShortUrl.Path;
            }
            catch (Exception ex)
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                await _logger.Log("GetAllShortenedUrls", "HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{jsonModel}], Error: {ex}");
                ViewBag.CreateMessage =
                    "An error occurred, please try again. If this continues please contact a site admin.";
            }

            return View("Index", model);
        }


        [Route("{path}")]
        public async Task<IActionResult> Catch(string path)
        {
            if (path.Contains("swagger"))
                return Redirect("swagger/index.html");

            try
            {
                var longUrl = await _repository.GetLongUrl(path);
                await _logger.Log("GetLongUrl", $"HttpGet:{path}", $"OriginalUrl:{longUrl}");

                if (longUrl != null && !longUrl.Contains("http"))
                    longUrl = $"http://{longUrl}";

                return Redirect(longUrl);

            }
            catch (Exception ex)
            {
                await _logger.Log("GetLongUrl", $"HttpGet:{path}", $"Error: {ex}");
            }

            return View("Index");
        }
    }
}
