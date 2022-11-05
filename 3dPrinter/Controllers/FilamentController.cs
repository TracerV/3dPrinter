using _3dPrinter.Domain.ViewModels.Filament;
using _3dPrinter.Service.Interfaces;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// using _3dPrinter.Domain.Entity;

namespace _3dPrinter.Controllers
{
    public class FilamentController : Controller
    {
        private readonly IFilamentService _filamentService;

        public FilamentController(IFilamentService filamentService)
        {
            _filamentService = filamentService;
        }

        [HttpGet]
        public IActionResult GetFilaments()
        {
            var response = _filamentService.GetFilaments();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error",$"{response.Description}");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(FilamentViewModel model)
        {
            var response = await _filamentService.GetFilament(model.Id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("Delete",response.Data);
            }
            ModelState.AddModelError("",response.Description);
            return PartialView();
        }
        
       [HttpPost]
        public async Task<IActionResult> Delete(int id)
         {
             var response = await _filamentService.DeleteFilament(id);
             if (response.StatusCode == Domain.Enum.StatusCode.OK)
             {
                 return RedirectToAction("GetFilaments");
                 // return PartialView("Delete");
             }
             return View("Error", $"{response.Description}");
         }

       [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if (id == 0)
            {
                var resp = new FilamentViewModel();
                return PartialView("AddEdit",resp);
            }
            var response = await _filamentService.GetFilament(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("AddEdit",response.Data);
            }
            ModelState.AddModelError("",response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(FilamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _filamentService.CreateFilament(model);
                }
                else
                {
                    await _filamentService.Edit(model.Id, model);
                }
                return RedirectToAction("GetFilaments");
                // return PartialView("AddEdit",model);
            }
            return View();
        }
    }
}
