using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleShort.Data;
using SimpleShort.Data.LogService;
using SimpleShort.Models;

namespace SimpleShort.Controllers
{
    [ApiController]
    [Route("/shorten")]
    [Produces("application/json")]
    public class ShortenUrlController : ControllerBase
    {
        private readonly IShortenUrlRepository _repository;
        private readonly ILogService _logger;

        public ShortenUrlController(IShortenUrlRepository repository, ILogService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetLongUrl(string shortUrl)
        {
            try
            {
                var longUrl = await _repository.GetLongUrl(shortUrl);
                await _logger.Log("GetLongUrl", $"HttpGet:{shortUrl}", $"OriginalUrl:{longUrl}");
                return Ok(longUrl);
            }
            catch (Exception ex)
            {
                await _logger.Log("GetLongUrl", $"HttpGet:{shortUrl}", $"Error: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllShortenedUrls(string ipAddress)
        {
            try
            {
                var shortUrls = await _repository.GetAllShortenedUrls(ipAddress);
                var shortUrlsJson = JsonConvert.SerializeObject(shortUrls);
                await _logger.Log("GetAllShortenedUrls", $"HttpGet:{ipAddress}", $"SimpleShortUrls:{shortUrlsJson}");
                return Ok(shortUrls);
            }
            catch (Exception ex)
            {
                await _logger.Log("GetAllShortenedUrls", $"HttpGet:{ipAddress}", $"Error: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortedUrl(CreateShortenedUrlModel model)
        {
            try
            {
                var newShortUrl = await _repository.CreateShortedUrl(model);
                var modelJson = JsonConvert.SerializeObject(model);
                var newShirtUrlJson = JsonConvert.SerializeObject(newShortUrl);
                await _logger.Log("CreateShortedUrl", $"HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], NewShirtUrl: [{newShirtUrlJson}]");
                return Ok(newShortUrl);
            }
            catch (Exception ex)
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                await _logger.Log("GetAllShortenedUrls", "HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{jsonModel}], Error: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShortedUrl(UpdateShortenedUrlModel model)
        {
            try
            {
                var updatedShortUrl = await _repository.UpdateShortedUrl(model);
                var modelJson = JsonConvert.SerializeObject(model);
                var updatedShortUrlJson = JsonConvert.SerializeObject(updatedShortUrl);
                await _logger.Log("UpdateShortedUrl", $"HttpPut:UpdateShortenedUrlModel", $"UpdateShortenedUrlModel: [{modelJson}], UpdatedShortUrl: [{updatedShortUrlJson}]");
                return Ok(updatedShortUrl);
            }
            catch (Exception ex)
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                await _logger.Log("UpdateShortedUrl", "HttpPut:UpdateShortenedUrlModel", $"UpdateShortenedUrlModel: [{jsonModel}], Error: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShortedUrl(DeleteShortenedUrlModel model)
        {
            try
            {
                var deleteResponse = await _repository.DeleteShortedUrl(model);
                var modelJson = JsonConvert.SerializeObject(model);
                var deleteResponseJson = JsonConvert.SerializeObject(deleteResponse);
                await _logger.Log("DeleteShortedUrl", $"HttpDelete:DeleteShortenedUrlModel", $"DeleteShortenedUrlModel: [{modelJson}], DeleteResponse: [{deleteResponseJson}]");
                return Ok(deleteResponse);
            }
            catch (Exception ex)
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                await _logger.Log("DeleteShortedUrl", "HttpDelete:DeleteShortenedUrlModel", $"DeleteShortenedUrlModel: [{jsonModel}], Error: {ex}");
                return BadRequest(ex);
            }
        }
    }
}
