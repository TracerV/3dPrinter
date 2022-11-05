using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Enum;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.Filament;
using _3dPrinter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3dPrinter.Service.Implementations
{
    public class FilamentService : IFilamentService
    {
        private readonly IBaseRepository<Filament> _filamentRepository;

        public FilamentService(IBaseRepository<Filament> filamentRepository)
        {
            _filamentRepository = filamentRepository;
        }

        public async Task<IBaseResponse<FilamentViewModel>> GetFilament(int id)
        {
        
            try
            {
                var filament = await _filamentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (filament == null)
                {
                    return new BaseResponse<FilamentViewModel>()
                    {
                        Description = "Filament not found",
                        StatusCode = StatusCode.FilamentNotFound
                    };
                }
                var data = new FilamentViewModel()
                {
                    Id = filament.Id,
                    Name = filament.Name,
                    Manufacturer = filament.Manufacturer,
                    TempOfPrint = filament.TempOfPrint,
                    Price = filament.Price
                };
                return new BaseResponse<FilamentViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilamentViewModel>()
                {
                    Description = $"[GetFilament] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public IBaseResponse<List<Filament>> GetFilaments()
        {
            try
            {
                var filaments = _filamentRepository.GetAll().ToList();
                if (!filaments.Any())
                {
                    return new BaseResponse<List<Filament>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Filament>>()
                {
                    Data = filaments,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Filament>>()
                {
                    Description = $"[GetFilaments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteFilament(int id)
        {
            try
            {
                var filament = await _filamentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (filament == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Filament not found",
                        StatusCode = StatusCode.FilamentNotFound,
                        Data = false
                    };
                }
                await _filamentRepository.Delete(filament);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilament] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Filament>> CreateFilament(FilamentViewModel model)
        {
            try
            {
                var filament = new Filament()
                {
                    Name = model.Name,
                    Manufacturer = model.Manufacturer,
                    TempOfPrint = model.TempOfPrint,
                    Price = model.Price
                };
                await _filamentRepository.Create(filament);
                return new BaseResponse<Filament>()
                {
                    StatusCode = StatusCode.OK,
                    Data = filament
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Filament>()
                {
                    Description = $"[CreateFilament] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Filament>> Edit(int id, FilamentViewModel model)
        {
            try
            {
                var filament = await _filamentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (filament == null)
                {
                    return new BaseResponse<Filament>()
                    {
                        Description = "Filament not found",
                        StatusCode = StatusCode.FilamentNotFound
                    };
                }
                filament.Name = model.Name;
                filament.Manufacturer = model.Manufacturer;
                filament.TempOfPrint = model.TempOfPrint;
                filament.Price = model.Price;

                await _filamentRepository.Update(filament);
                return new BaseResponse<Filament>()
                {
                    Data = filament,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Filament>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
