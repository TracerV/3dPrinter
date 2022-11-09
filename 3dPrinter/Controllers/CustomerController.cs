using _3dPrinter.Domain.ViewModels.Customer;
using _3dPrinter.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _3dPrinter.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var response = _customerService.GetCustomers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error",$"{response.Description}");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(CustomerViewModel model)
        {
            var response = await _customerService.GetCustomer(model.Id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("DeletePartial",response.Data);
            }
            ModelState.AddModelError("",response.Description);
            return View("Error",$"{response.Description}");
        }
        
       [HttpPost]
        public async Task<IActionResult> Delete(int id)
         {
             var response = await _customerService.DeleteCustomer(id);
             if (response.StatusCode == Domain.Enum.StatusCode.OK)
             {
                 return RedirectToAction("GetCustomers");
             }
             return View("Error", $"{response.Description}");
         }

       [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            if (id == 0)
            {
                var resp = new CustomerViewModel();
                return PartialView("AddEditPartial",resp);
            }
            var response = await _customerService.GetCustomer(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView("AddEditPartial",response.Data);
            }
            ModelState.AddModelError("",response.Description);
            return View("Error",$"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _customerService.CreateCustomer(model);
                }
                else
                {
                    await _customerService.Edit(model.Id, model);
                }
                return RedirectToAction("GetCustomers");
            }
            return View("Error");
        }
    }
}
