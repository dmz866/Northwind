using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.UnitOfWork;

namespace Northwind.WebApi.Controllers
{
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetId(int id)
        {
            return Ok(_unitOfWork.Customer.GetById(id));
        }

        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            return Ok(_unitOfWork.Customer.GetList(page, rows));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if(ModelState.IsValid)
            {
                return Ok(_unitOfWork.Customer.Insert(customer));
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            if(ModelState.IsValid && _unitOfWork.Customer.Update(customer))
            {
                return Ok(new { Message = "Customer updated" });
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Customer customer)
        {
            if(customer.Id > 0)
            {
                return Ok(_unitOfWork.Customer.Delete(customer));
            }

            return BadRequest();
        }
    }
}