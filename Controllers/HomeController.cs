using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using URLShortner.Domain;
using URLShortner.DTOs;

namespace URLShortner.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IURLShortnerService _urlShortnetService;
        public HomeController(IURLShortnerService urlShortnetService)
        {
            _urlShortnetService = urlShortnetService;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl(URLToShorten dto)
        {
            //Validate the URL
            bool isValid = Uri.TryCreate(dto.URL, UriKind.Absolute, out Uri uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            string url = dto.URL.Trim('/');
            if (isValid)
            {
                var urlChunk = await _urlShortnetService.URLExists(url);
                if (string.IsNullOrWhiteSpace(urlChunk))
                {
                    urlChunk = await _urlShortnetService.ShortenURL(url);
                }
                var responseUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{urlChunk}";
                return Ok(responseUri);
            }
            else
            {
                return BadRequest($"Invalid URL");
            }
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<IActionResult> GetURL(string ID)
        {
            var URL = await _urlShortnetService.GetURL(ID);
            return !string.IsNullOrWhiteSpace(URL) ? Redirect(URL) : (IActionResult)BadRequest("URL NOT FOUND");
        }
    }
}