using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiNetCore.Dtos;
using WebApiNetCore.Entities;
using WebApiNetCore.Models;
using WebApiNetCore.Repositories;

namespace WebApiNetCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [AuthenticationFilter]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IUrlHelper urlHelper;
        

        public InvoiceController(IUrlHelper urlHelper, IInvoiceRepository InvoiceRepository)
        {
            this.invoiceRepository = InvoiceRepository;
            this.urlHelper = urlHelper;
           
        }
        
        [ProducesResponseType(typeof(IEnumerable<InvoiceDto>), 201)]
        [HttpGet(Name = nameof(GetAllInvoices))]
        public IActionResult GetAllInvoices([FromQuery, Required] QueryParameters queryParameters)
        {
            
            return Ok(invoiceRepository.GetAll(queryParameters).ToList());
        }

        [HttpGet]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(InvoiceDto), 201)]
        [Route("{id:int}", Name = nameof(GetSingleInvoice))]
        public IActionResult GetSingleInvoice(int id)
        {
            Invoice Invoice = invoiceRepository.GetSingle(id);

            if (Invoice == null)
            {
                return NotFound();
            }

            return Ok(Invoice);
        }

        [ValidateModelState]
        [HttpPost(Name = nameof(AddInvoice))]
        public IActionResult AddInvoice([FromBody,Required] InvoiceCreateDto InvoiceCreateDto)
        {
            invoiceRepository.Add(Mapper.Map<Invoice>(InvoiceCreateDto));
            return Ok();

            //if (!_InvoiceRepository.Save())
            //{
            //    throw new Exception("Creating a Invoiceitem failed on save.");
            //}

            //Invoice newInvoiceItem = _InvoiceRepository.GetSingle(toAdd.Id);

            //return CreatedAtRoute(nameof(GetSingleInvoice), new { id = newInvoiceItem.Id },
            //    Mapper.Map<InvoiceDto>(newInvoiceItem));
        }

        [ProducesResponseType(typeof(InvoiceDto), 201)]
        [HttpPatch("{id:int}", Name = nameof(PayInvoice))]
        public IActionResult PayInvoice(int id)
        {
            Invoice updated = invoiceRepository.ChangeStatus(id, Status.Paid);

            return Ok(Mapper.Map<InvoiceDto>(updated));
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(RemoveInvoice))]
        public IActionResult RemoveInvoice(int id)
        {
            invoiceRepository.Delete(id);

            return NoContent();
        }

        [HttpPut]
        [ValidateModelState]
        [Route("{id:int}", Name = nameof(UpdateInvoice))]
        public IActionResult UpdateInvoice(int id, [FromBody]InvoiceUpdateDto InvoiceUpdateDto)
        {
            return Ok(Mapper.Map<InvoiceDto>(invoiceRepository.Update(id, Mapper.Map<Invoice>(InvoiceUpdateDto))));
        }
    }
}