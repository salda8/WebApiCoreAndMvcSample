using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
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
    [SwaggerResponse(400, typeof(BadRequestResult), "Error when model state validation failes")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IUrlHelper urlHelper;

        public InvoiceController(IUrlHelper urlHelper, IInvoiceRepository InvoiceRepository)
        {
            this.invoiceRepository = InvoiceRepository;
            this.urlHelper = urlHelper;
        }

        [SwaggerResponse(201, typeof(IEnumerable<InvoiceDto>))]
        [HttpGet(Name = nameof(GetAllInvoices))]
        public IActionResult GetAllInvoices([FromQuery, Required] QueryParameters queryParameters)
        {
            return Ok(invoiceRepository.GetAll(queryParameters).ToList());
        }

        [HttpGet]
        [SwaggerResponse(201, typeof(InvoiceDto))]
        [Route("{id:int}", Name = nameof(GetSingleInvoice))]
        public IActionResult GetSingleInvoice(int id)
        {
            InvoiceDto Invoice = invoiceRepository.GetSingle(id);

            if (Invoice == null)
            {
                return NotFound();
            }

            return Ok(Invoice);
        }

        [ValidateModelState]
        [HttpPost(Name = nameof(AddInvoice))]
        public IActionResult AddInvoice([FromBody, Required] InvoiceCreateDto InvoiceCreateDto)
        {
            invoiceRepository.Add(InvoiceCreateDto);
            return Ok();

            //if (!_InvoiceRepository.Save())
            //{
            //    throw new Exception("Creating a Invoiceitem failed on save.");
            //}

            //Invoice newInvoiceItem = _InvoiceRepository.GetSingle(toAdd.Id);

            //return CreatedAtRoute(nameof(GetSingleInvoice), new { id = newInvoiceItem.Id },
            //    Mapper.Map<InvoiceDto>(newInvoiceItem));
        }

        [SwaggerResponse(201, typeof(InvoiceDto))]
        [HttpPatch("{id:int}", Name = nameof(PayInvoice))]
        public IActionResult PayInvoice(int id)
        {
            var updated = invoiceRepository.ChangeStatus(id, Status.Paid);

            return Ok(updated);
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
        [SwaggerResponse(201, typeof(InvoiceDto))]
        [Route("{id:int}", Name = nameof(UpdateInvoice))]
        public IActionResult UpdateInvoice(int id, [FromBody]InvoiceUpdateDto InvoiceUpdateDto)
        {
            return Ok(invoiceRepository.Update(id, InvoiceUpdateDto));
        }
    }
}