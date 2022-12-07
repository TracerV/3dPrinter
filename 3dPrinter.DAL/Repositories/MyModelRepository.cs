using System.Linq;
using System.Threading.Tasks;
using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;

namespace _3dPrinter.DAL.Repositories;

public class MyModelRepository : IBaseRepository<MyModel>
{
    private readonly ApplicationDbContext _db;

    public MyModelRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Create(MyModel entity)
    {
        await _db.MyModel.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(MyModel entity)
    {
        _db.MyModel.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public IQueryable<MyModel> GetAll()
    {
        return _db.MyModel;
    }

    public async Task<MyModel> Update(MyModel entity)
    {
        _db.MyModel.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

}