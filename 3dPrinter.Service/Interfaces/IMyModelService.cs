using System.Collections.Generic;
using System.Threading.Tasks;
using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.MyModel;

namespace _3dPrinter.Service.Interfaces;

public interface IMyModelService
{
    IBaseResponse<List<MyModel>> GetModels();
    Task<IBaseResponse<MyModel>> CreateModel(MyModelViewModel model);
    Task<IBaseResponse<bool>> DeleteModel(int id);
    Task<IBaseResponse<MyModelViewModel>> GetModel(int id);
    Task<IBaseResponse<MyModel>> Edit(int id, MyModelViewModel model);

}