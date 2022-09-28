using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.ViewModels.Filament;
using _3dPrinter.Models;
using _3dPrinter.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetFilament(int id)
        {
            var response = await _filamentService.GetFilament(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _filamentService.DeleteFilament(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetFilaments");
            }
            return RedirectToAction("Error");

        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var response = await _filamentService.GetFilament(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(FilamentViewModel model)
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
            }
            return View();
        }
    }
}
