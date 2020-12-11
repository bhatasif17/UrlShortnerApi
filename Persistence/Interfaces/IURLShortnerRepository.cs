using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using URLShortner.Models;

namespace URLShortner.Persistence
{
    public interface IURLShortnerRepository
    {
        Task ShortenURL(string URL, string ID);
        Task<string> URLExists(string URL);

        Task<string> GetURL(string ID);
    }

    public class URLShortnerRepository : IURLShortnerRepository
    {
        private readonly URLShortnerContext _context;
        public URLShortnerRepository(URLShortnerContext context)
        {
            _context = context;
        }
        public async Task<string> GetURL(string ID)
        {
            var unit = await _context.Urlstores.FirstOrDefaultAsync(o => o.Id == ID);
            return unit is not null ? unit.Url : "";
        }

        public async Task ShortenURL(string URL, string ID)
        {
            Urlstore model = new();
            model.Url = URL;
            model.Id = ID;

            await _context.Urlstores.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<string> URLExists(string URL)
        {
            var unit = await _context.Urlstores.FirstOrDefaultAsync(o => o.Url == URL);
            return unit is not null ? unit.Id : "";
        }


    }
}
