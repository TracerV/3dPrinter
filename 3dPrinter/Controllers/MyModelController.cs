using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3dPrinter.Domain.ViewModels.MyModel;
using _3dPrinter.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _3dPrinter.Controllers;

public class MyModelController : Controller
{
    private readonly IMyModelService _modelService;
    private readonly IFilamentService _filamentService;
    private readonly ICustomerService _customerService;

    public MyModelController(IMyModelService modelService, IFilamentService filamentService,ICustomerService customerService)
    {
        _modelService = modelService;
        _filamentService = filamentService;
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult GetMyModels()
    {
        var response = _modelService.GetModels();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(MyModelViewModel model)
    {
        var response = await _modelService.GetModel(model.Id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return PartialView("DeletePartial", response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View("Error", $"{response.Description}");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _modelService.DeleteModel(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetMyModels");
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> AddEdit(int id)
    {
        if (id == 0)
        {
            var resp = new MyModelViewModel();
            ViewBag.FilamentList = FilamentList();
            ViewBag.CustomerList = CustomerList();
            return PartialView("AddEditPartial", resp);
        }

        var response = await _modelService.GetModel(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            ViewBag.FilamentList = FilamentList();
            ViewBag.CustomerList = CustomerList();
            return PartialView("AddEditPartial", response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View("Error", $"{response.Description}");
    }

    private List<SelectListItem> FilamentList()
    {
        var lstItems =
            // List<Filament> items = _filamentService.GetFilaments().Data("Name", "", 1, 1000);
            _filamentService.GetFilaments().Data.Select(ut => new SelectListItem()
        {
            Value = ut.Id.ToString(),
            Text = ut.Name
        }).ToList();

        var defItem = new SelectListItem()
        {
            Value = "",
            Text = "----Select Filament----"
        };

        lstItems.Insert(0, defItem);

        return lstItems;
    }
    
    private List<SelectListItem> CustomerList()
    {
        var lstItems =
            // List<Filament> items = _filamentService.GetFilaments().Data("Name", "", 1, 1000);
            _customerService.GetCustomers().Data.Select(ut => new SelectListItem()
        {
            Value = ut.Id.ToString(),
            Text = ut.Name
        }).ToList();

        var defItem = new SelectListItem()
        {
            Value = "",
            Text = "----Select Customer----"
        };

        lstItems.Insert(0, defItem);

        return lstItems;
    }

    [HttpPost]
    public async Task<IActionResult> AddEdit(MyModelViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
                await _modelService.CreateModel(model);
            }
            else
            {
                await _modelService.Edit(model.Id, model);
            }

            return RedirectToAction("GetMyModels");
        }

        return View("Error");
    }
}