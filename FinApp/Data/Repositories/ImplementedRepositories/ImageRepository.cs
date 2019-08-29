using DAL.Context;
using DAL.Entities;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.ImplementedRepositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public FinAppContext Context { get => _context as FinAppContext; }

        public ImageRepository(DbContext context) : base(context)
        {
        }

        public override async Task AddAsync(Image entity)
        {
            await Context.Images.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
    }
}
