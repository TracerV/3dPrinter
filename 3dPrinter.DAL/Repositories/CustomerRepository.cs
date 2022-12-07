using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace _3dPrinter.DAL.Repositories
{
    public class CustomerRepository : IBaseRepository<Customer>
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Customer entity)
        {
            await _db.Customer.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Customer entity)
        {
            _db.Customer.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Customer> GetAll()
        {
            return _db.Customer; 
        }

        public async Task<Customer> Update(Customer entity)
        {
            _db.Customer.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}