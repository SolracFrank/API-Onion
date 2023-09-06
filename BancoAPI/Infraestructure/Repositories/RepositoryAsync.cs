using Application.Interface;
using Ardalis.Specification.EntityFrameworkCore;
using Infraestructure.Data;


namespace Infraestructure.Repositories
{
    public class RepositoryAsync <T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext Context;

        public RepositoryAsync(ApplicationDbContext context) :base(context)
        {
            Context = context;
        }
    }
}
