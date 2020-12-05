using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SimpleShort.Controllers;
using SimpleShort.Data;
using SimpleShort.Data.LogService;
using SimpleShort.Entities;

namespace SimpleShortTests.cs
{
    public class Tests
    {
        private Mock<IShortenUrlRepository> _shortenUrlRepository;
        private Mock<ILogService> _logService;
        private ShortenUrlController _controller;


        [SetUp]
        public void Setup()
        {
            _shortenUrlRepository = new Mock<IShortenUrlRepository>();
            _logService = new Mock<ILogService>();
            _controller = new ShortenUrlController(_shortenUrlRepository.Object, _logService.Object);
        }

        [Test]
        [TestCase("", "", "")]
        [TestCase(null, null, null)]
        [TestCase("null", "null", "null")]
        [TestCase("http://google.com", "google", "01/01/9999 11:59 PM")]
        [TestCase("https://google.com", "secure google", "01/01/0001 11:59 PM")]
        public async Task GetLongUrlTest(string originalUrl, string path, string expiration)
        {
            _shortenUrlRepository.Setup(r => r.GetLongUrl(path)).ReturnsAsync(originalUrl);
            var result = await _controller.GetLongUrl(path);
            Assert.AreEqual(result, originalUrl);
        }

        // [Test]
        // [TestCase("", "", "", "")]
        // public async Task GetShortenedUrlTest(string ipAddress, string originalUrl, string path, string expiration)
        // {
        //     var shortUrl = new SimpleShortUrl(originalUrl, path, expiration);
        //     _shortenUrlRepository.Setup(r => r.CreateShortedUrl()).ReturnsAsync(shortUrl);
        //     var result = await _controller.Get(path);
        //     Assert.AreEqual(result, originalUrl);
        // }
    }
}