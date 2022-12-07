using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3dPrinter.Service.Interfaces
{
    public interface ICustomerService
    {
        IBaseResponse<List<Customer>> GetCustomers();
        Task<IBaseResponse<Customer>> CreateCustomer(CustomerViewModel customer);
        Task<IBaseResponse<bool>> DeleteCustomer(int id);
        Task<IBaseResponse<CustomerViewModel>> GetCustomer(int id);
        Task<IBaseResponse<Customer>> Edit(int id,CustomerViewModel model);
    }
}