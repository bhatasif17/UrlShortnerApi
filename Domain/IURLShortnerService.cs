using System;
using System.Threading.Tasks;
using URLShortner.Persistence;

namespace URLShortner.Domain
{
    public interface IURLShortnerService
    {
        public Task<string> ShortenURL(string URL);

        public Task<string> GetURL(string ID);

        public Task<string> URLExists(string URL);
    }

    public class URLShortnerService : IURLShortnerService
    {
        private readonly IURLShortnerRepository _urlShortnerRepository;
        public URLShortnerService(IURLShortnerRepository urlShortnerRepository)
        {
            _urlShortnerRepository = urlShortnerRepository;
        }

        public async Task<string> GetURL(string ID)
        {
            return await _urlShortnerRepository.GetURL(ID);
        }

        public async Task<string> ShortenURL(string URL)
        {
            var ID = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            await _urlShortnerRepository.ShortenURL(URL, ID);
            return ID;
        }

        public async Task<string> URLExists(string URL)
        {
            var exists = await _urlShortnerRepository.URLExists(URL);
            return !string.IsNullOrWhiteSpace(exists) ? exists : "";
        }
    }
}
