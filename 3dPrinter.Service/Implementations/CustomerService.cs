using _3dPrinter.DAL.Interfaces;
using _3dPrinter.Domain.Entity;
using _3dPrinter.Domain.Enum;
using _3dPrinter.Domain.Response;
using _3dPrinter.Domain.ViewModels.Customer;
using _3dPrinter.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3dPrinter.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepository<Customer> _customerRepository;

        public CustomerService(IBaseRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IBaseResponse<CustomerViewModel>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (customer == null)
                {
                    return new BaseResponse<CustomerViewModel>
                    {
                        Description = "Customer not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new CustomerViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email
                };
                return new BaseResponse<CustomerViewModel>
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerViewModel>
                {
                    Description = $"[GetCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Customer>> GetCustomers()
        {
            try
            {
                var customers = _customerRepository.GetAll().ToList();
                if (!customers.Any())
                {
                    return new BaseResponse<List<Customer>>
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Customer>>
                {
                    Data = customers,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Customer>>
                {
                    Description = $"[GetCustomers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (customer == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Customer not found",
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await _customerRepository.Delete(customer);
                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Customer>> CreateCustomer(CustomerViewModel model)
        {
            try
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email
                };
                await _customerRepository.Create(customer);
                return new BaseResponse<Customer>
                {
                    StatusCode = StatusCode.OK,
                    Data = customer
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>
                {
                    Description = $"[CreateCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Customer>> Edit(int id, CustomerViewModel model)
        {
            try
            {
                var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (customer == null)
                {
                    return new BaseResponse<Customer>
                    {
                        Description = "Customer not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                customer.Name = model.Name;
                customer.Phone = model.Phone;
                customer.Email = model.Email;

                await _customerRepository.Update(customer);
                return new BaseResponse<Customer>
                {
                    Data = customer,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}