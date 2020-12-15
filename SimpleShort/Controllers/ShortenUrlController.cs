/*
 * ShortenUrlController:
 * Route = /shorten
 * Responsible for shortening URL API.
 * Depending on the URL, a specific API will be used. Basic CRUD operations.
 *
 * HttpGet - Get information
 * HttpPost - Create information
 * HttpPut - Update information
 * HttpDelete - Delete information
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
    [ApiController]
    [Route("/shorten")]
    [Produces("application/json")]
    public class ShortenUrlController : ControllerBase
    {
        // Interface of Url data Repository
        private readonly IShortenUrlRepository _repository;

        // Interface of the log service
        private readonly ILogService _logger;

        // Default constructor gets repository and logger via dependency injection
        public ShortenUrlController(IShortenUrlRepository repository, ILogService logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Get the long url from the short url path
        [HttpGet]
        public async Task<IActionResult> GetLongUrl(string shortUrl)
        {
            try
            {
                // Get the long url from the repository
                var longUrl = await _repository.GetLongUrl(shortUrl);

                // Log information
                await _logger.Log("GetLongUrl", $"HttpGet:{shortUrl}", $"OriginalUrl:{longUrl}");

                // Return the long url
                return Ok(longUrl);
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("GetLongUrl", $"HttpGet:{shortUrl}", $"Error: {ex}");

                // Return the error
                return BadRequest(ex);
            }
        }

        // Get all shortened urls created by an ip address
        [HttpGet("all")]
        public async Task<IActionResult> GetAllShortenedUrls(string ipAddress)
        {
            try
            {
                // Get all urls created by given IP address
                var shortUrls = await _repository.GetAllShortenedUrls(ipAddress);

                // SimpleShortUrl[] case as json string
                var shortUrlsJson = JsonConvert.SerializeObject(shortUrls);

                // Log information
                await _logger.Log("GetAllShortenedUrls", $"HttpGet:{ipAddress}", $"SimpleShortUrls:{shortUrlsJson}");

                // Return the short urls
                return Ok(shortUrls);
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("GetAllShortenedUrls", $"HttpGet:{ipAddress}", $"Error: {ex}");

                // Return the error
                return BadRequest(ex);
            }
        }

        // Create a new short url from the provided model
        [HttpPost]
        public async Task<IActionResult> CreateShortedUrl(CreateShortenedUrlModel model)
        {
            // CreateShortenedUrlModel case as json string
            var modelJson = JsonConvert.SerializeObject(model);

            try
            {
                // Create the new url and get the information back
                var newShortUrl = await _repository.CreateShortedUrl(model);

                // SimpleShortUrl case as json string
                var newShirtUrlJson = JsonConvert.SerializeObject(newShortUrl);

                // Log information
                await _logger.Log("CreateShortedUrl", $"HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], NewShirtUrl: [{newShirtUrlJson}]");
                
                // Return the short url information
                return Ok(newShortUrl);
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("GetAllShortenedUrls", "HttpPost:CreateShortenedUrlModel", $"CreateShortenedUrlModel: [{modelJson}], Error: {ex}");
                
                // Return the error
                return BadRequest(ex);
            }
        }

        // Update an existing url
        [HttpPut]
        public async Task<IActionResult> UpdateShortedUrl(UpdateShortenedUrlModel model)
        {
            // UpdateShortenedUrlModel case as json string
            var modelJson = JsonConvert.SerializeObject(model);

            try
            {
                // Get the updated url from the repository after updating
                var updatedShortUrl = await _repository.UpdateShortedUrl(model);

                // SimpleShortUrl case as json string
                var updatedShortUrlJson = JsonConvert.SerializeObject(updatedShortUrl);
                
                // Log information
                await _logger.Log("UpdateShortedUrl", $"HttpPut:UpdateShortenedUrlModel", $"UpdateShortenedUrlModel: [{modelJson}], UpdatedShortUrl: [{updatedShortUrlJson}]");
                
                // Return the short url information
                return Ok(updatedShortUrl);
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("UpdateShortedUrl", "HttpPut:UpdateShortenedUrlModel", $"UpdateShortenedUrlModel: [{modelJson}], Error: {ex}");
                
                // Return the error
                return BadRequest(ex);
            }
        }

        // Delete an existing url
        [HttpDelete]
        public async Task<IActionResult> DeleteShortedUrl(DeleteShortenedUrlModel model)
        {
            // DeleteShortenedUrlModel case as json string
            var modelJson = JsonConvert.SerializeObject(model);
            try
            {
                // Delete the url from the database and get the response
                var deleteResponse = await _repository.DeleteShortedUrl(model);

                // DeleteResponse case as json string
                var deleteResponseJson = JsonConvert.SerializeObject(deleteResponse);
                
                // Log information
                await _logger.Log("DeleteShortedUrl", $"HttpDelete:DeleteShortenedUrlModel", $"DeleteShortenedUrlModel: [{modelJson}], DeleteResponse: [{deleteResponseJson}]");
                
                // Return the delete response
                return Ok(deleteResponse);
            }
            catch (Exception ex)
            {
                // Log information
                await _logger.Log("DeleteShortedUrl", "HttpDelete:DeleteShortenedUrlModel", $"DeleteShortenedUrlModel: [{modelJson}], Error: {ex}");
                
                // Return the error
                return BadRequest(ex);
            }
        }
    }
}