using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.Filament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dPrinter.Service.Interfaces
{
    public interface IFilamentService
    {
        IBaseResponse<List<Filament>> GetFilaments();
        Task<IBaseResponse<Filament>> CreateFilament(FilamentViewModel filament);
        Task<IBaseResponse<bool>> DeleteFilament(int id);
        Task<IBaseResponse<FilamentViewModel>> GetFilament(int id);
        Task<IBaseResponse<Filament>> Edit(int id,FilamentViewModel model);
    }
}
