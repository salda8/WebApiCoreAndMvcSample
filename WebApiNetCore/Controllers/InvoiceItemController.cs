using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiNetCore.Dtos;
using WebApiNetCore.Entities;
using WebApiNetCore.Repositories;

namespace WebApiNetCore.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/InvoiceItem")]
    public class InvoiceItemController : Controller
    {
        private readonly IInvoiceItemRepository repository;

        public InvoiceItemController(IInvoiceItemRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/InvoiceIItems
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAll());
        }

        // GET: api/InvoiceIItems/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return Ok(repository.GetSingle(id));
        }
        
        // POST: api/InvoiceIItems
        [ValidateModelState]
        [HttpPost]
        public IActionResult Post([FromBody, Required]InvoiceItemCreateDto value)
        {
            repository.Add(Mapper.Map<InvoiceItem>(value));
            return Ok();
        }
       
       
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return NoContent();
        }
    }
}
