using System.Threading.Tasks;
using _3dPrinter.Domain.ViewModels.MyModel;
using _3dPrinter.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _3dPrinter.Controllers;

public class MyModelController : Controller
{
    private readonly IMyModelService _modelService;

    public MyModelController(IMyModelService modelService)
    {
        _modelService = modelService;
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
            return PartialView("AddEditPartial", resp);
        }
        var response = await _modelService.GetModel(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return PartialView("AddEditPartial", response.Data);
        }

        ModelState.AddModelError("", response.Description);
        return View("Error", $"{response.Description}");
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