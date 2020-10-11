using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenricRepository<T> : IGenricRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        public GenricRepository(StoreContext context)
        {
            _context=context;
        }
        public async Task<T> GetByIdAsync(int id)
        {
           return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}