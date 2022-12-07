using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Enum;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.MyModel;
using _3dPrinter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace _3dPrinter.Service.Implementations;

public class MyModelService : IMyModelService
{
    private readonly IBaseRepository<MyModel> _modelRepository;
    private readonly IBaseRepository<Filament> _filamentRepository;
    private readonly IBaseRepository<Customer> _customerRepository;
    public MyModelService(IBaseRepository<MyModel> modelRepository, IBaseRepository<Filament> filamentRepository, IBaseRepository<Customer> customerRepository)
    {
        _modelRepository = modelRepository;
        _filamentRepository = filamentRepository;
        _customerRepository = customerRepository;
    }

    public IBaseResponse<List<MyModel>> GetModels()
    {
        try
        {
            var models = _modelRepository.GetAll()
                .Include(fl=>fl.Filament)
                .Include(ct=>ct.Customer)
                .ToList();
            if (!models.Any())
            {
                return new BaseResponse<List<MyModel>>()
                {
                    Description = "Found 0 elements",
                    StatusCode = StatusCode.OK
                };
            }

            return new BaseResponse<List<MyModel>>()
            {
                Data = models,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<MyModel>>()
            {
                Description = $"[GetModels] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<MyModel>> CreateModel(MyModelViewModel viewModel)
    {
        try
        {
            var model = new MyModel()
            {
                Name = viewModel.Name,
                Cost = viewModel.Cost,
                FilamentId = viewModel.FilamentId,
                CustomerId = viewModel.CustomerId,
                TimeOfPrint = viewModel.TimeOfPrint
            };
            await _modelRepository.Create(model);
            return new BaseResponse<MyModel>()
            {
                StatusCode = StatusCode.OK,
                Data = model
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<MyModel>()
            {
                Description = $"[CreateModel] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteModel(int id)
    {
        try
        {
            var model = await _modelRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Model not found",
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };
            }

            await _modelRepository.Delete(model);
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
                Description = $"[DeleteModel] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<MyModelViewModel>> GetModel(int id)
    {
        try
        {
            var model = await _modelRepository.GetAll()
                .Include(fl=>fl.Filament)
                .Include(ct=>ct.Customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return new BaseResponse<MyModelViewModel>()
                {
                    Description = "Model not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            var data = new MyModelViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Cost = model.Cost,
                Filament = model.Filament,
                Customer = model.Customer,
                TimeOfPrint = model.TimeOfPrint,
                FilamentId= model.FilamentId,
                CustomerId = model.CustomerId,
            };
            return new BaseResponse<MyModelViewModel>()
            {
                StatusCode = StatusCode.OK,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<MyModelViewModel>()
            {
                Description = $"[GetModel] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<MyModel>> Edit(int id, MyModelViewModel viewModel)
    {
        try
        {
            var model = await _modelRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return new BaseResponse<MyModel>()
                {
                    Description = "Model not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            model.Name = viewModel.Name;
            model.Cost = viewModel.Cost;
            model.FilamentId = viewModel.FilamentId;
            model.CustomerId = viewModel.CustomerId;
            model.TimeOfPrint = viewModel.TimeOfPrint;

            await _modelRepository.Update(model);
            return new BaseResponse<MyModel>()
            {
                Data = model,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<MyModel>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}