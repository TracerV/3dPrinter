using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dPrinter.DAL.Repositories
{
    public class FilamentRepository : IBaseRepository<Filament>
    {
        private readonly ApplicationDbContext _db;

        public FilamentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Filament entity)
        {
            await _db.Filament.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Filament entity)
        {
            _db.Filament.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Filament> GetAll()
        {
            return _db.Filament; 
        }

        public async Task<Filament> Update(Filament entity)
        {
            _db.Filament.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
