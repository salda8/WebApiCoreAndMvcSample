using System;
using System.Linq;
using AutoMapper;
using SampleWebApiAspNetCore.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleWebApiAspNetCore.Repositories;
using System.Collections.Generic;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;
using SampleWebApiAspNetCore.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SampleWebApiAspNetCore.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Route("api/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _InvoiceRepository;
        private readonly IUrlHelper _urlHelper;

        public InvoicesController(IUrlHelper urlHelper, IInvoiceRepository InvoiceRepository)
        {
            _InvoiceRepository = InvoiceRepository;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetAllInvoices))]
        public IActionResult GetAllInvoices([FromQuery] QueryParameters queryParameters)
        {
            List<InvoiceItem> InvoiceItems = _InvoiceRepository.GetAll(queryParameters).ToList();

            var allItemCount = _InvoiceRepository.Count();

            var paginationMetadata = new
            {
                totalCount = allItemCount,
                pageSize = queryParameters.PageCount,
                currentPage = queryParameters.Page,
                totalPages = queryParameters.GetTotalPages(allItemCount)
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var links = CreateLinksForCollection(queryParameters, allItemCount);

            var toReturn = InvoiceItems.Select(x => ExpandSingleInvoiceItem(x));

            return Ok(new
            {
                value = toReturn,
                links = links
            });
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetSingleInvoice))]
        public IActionResult GetSingleInvoice(int id)
        {
            InvoiceItem InvoiceItem = _InvoiceRepository.GetSingle(id);

            if (InvoiceItem == null)
            {
                return NotFound();
            }

            return Ok(ExpandSingleInvoiceItem(InvoiceItem));
        }

        [HttpPost(Name = nameof(AddInvoice))]
        public IActionResult AddInvoice([FromBody] InvoiceCreateDto InvoiceCreateDto)
        {
            if (InvoiceCreateDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            InvoiceItem toAdd = Mapper.Map<InvoiceItem>(InvoiceCreateDto);

            _InvoiceRepository.Add(toAdd);

            if (!_InvoiceRepository.Save())
            {
                throw new Exception("Creating a Invoiceitem failed on save.");
            }

            InvoiceItem newInvoiceItem = _InvoiceRepository.GetSingle(toAdd.Id);
            
            return CreatedAtRoute(nameof(GetSingleInvoice), new { id = newInvoiceItem.Id },
                Mapper.Map<InvoiceItemDto>(newInvoiceItem));
        }

        [HttpPatch("{id:int}", Name = nameof(PartiallyUpdateInvoice))]
        public IActionResult PartiallyUpdateInvoice(int id, [FromBody] JsonPatchDocument<InvoiceUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            InvoiceItem existingEntity = _InvoiceRepository.GetSingle(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            InvoiceUpdateDto InvoiceUpdateDto = Mapper.Map<InvoiceUpdateDto>(existingEntity);
            patchDoc.ApplyTo(InvoiceUpdateDto, ModelState);

            TryValidateModel(InvoiceUpdateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(InvoiceUpdateDto, existingEntity);
            InvoiceItem updated = _InvoiceRepository.Update(id, existingEntity);

            if (!_InvoiceRepository.Save())
            {
                throw new Exception("Updating a Invoiceitem failed on save.");
            }

            return Ok(Mapper.Map<InvoiceItemDto>(updated));
        }

        [HttpDelete]
        [Route("{id:int}", Name = nameof(RemoveInvoice))]
        public IActionResult RemoveInvoice(int id)
        {
            InvoiceItem InvoiceItem = _InvoiceRepository.GetSingle(id);

            if (InvoiceItem == null)
            {
                return NotFound();
            }

            _InvoiceRepository.Delete(id);

            if (!_InvoiceRepository.Save())
            {
                throw new Exception("Deleting a Invoiceitem failed on save.");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}", Name = nameof(UpdateInvoice))]
        public IActionResult UpdateInvoice(int id, [FromBody]InvoiceUpdateDto InvoiceUpdateDto)
        {
            if (InvoiceUpdateDto == null)
            {
                return BadRequest();
            }

            var existingInvoiceItem = _InvoiceRepository.GetSingle(id);

            if (existingInvoiceItem == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(InvoiceUpdateDto, existingInvoiceItem);

            _InvoiceRepository.Update(id, existingInvoiceItem);

            if (!_InvoiceRepository.Save())
            {
                throw new Exception("Updating a Invoiceitem failed on save.");
            }

            return Ok(Mapper.Map<InvoiceItemDto>(existingInvoiceItem));
        }

        [HttpGet("GetRandomMeal", Name = nameof(GetRandomMeal))]
        public IActionResult GetRandomMeal()
        {
            ICollection<InvoiceItem> InvoiceItems = _InvoiceRepository.GetRandomMeal();

            IEnumerable<InvoiceItemDto> dtos = InvoiceItems
                .Select(x => Mapper.Map<InvoiceItemDto>(x));

            var links = new List<LinkDto>();

            // self 
            links.Add(new LinkDto(_urlHelper.Link(nameof(GetRandomMeal), null), "self", "GET"));

            return Ok(new
            {
                value = dtos,
                links = links
            });
        }

        private List<LinkDto> CreateLinksForCollection(QueryParameters queryParameters, int totalCount)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
             new LinkDto(_urlHelper.Link(nameof(GetAllInvoices), new
             {
                 pagecount = queryParameters.PageCount,
                 page = queryParameters.Page,
                 orderby = queryParameters.OrderBy
             }), "self", "GET"));

            links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllInvoices), new
            {
                pagecount = queryParameters.PageCount,
                page = 1,
                orderby = queryParameters.OrderBy
            }), "first", "GET"));

            links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllInvoices), new
            {
                pagecount = queryParameters.PageCount,
                page = queryParameters.GetTotalPages(totalCount),
                orderby = queryParameters.OrderBy
            }), "last", "GET"));

            if (queryParameters.HasNext(totalCount))
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllInvoices), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page + 1,
                    orderby = queryParameters.OrderBy
                }), "next", "GET"));
            }

            if (queryParameters.HasPrevious())
            {
                links.Add(new LinkDto(_urlHelper.Link(nameof(GetAllInvoices), new
                {
                    pagecount = queryParameters.PageCount,
                    page = queryParameters.Page - 1,
                    orderby = queryParameters.OrderBy
                }), "previous", "GET"));
            }

            return links;
        }

        private dynamic ExpandSingleInvoiceItem(InvoiceItem InvoiceItem)
        {
            var links = GetLinks(InvoiceItem.Id);
            InvoiceItemDto item = Mapper.Map<InvoiceItemDto>(InvoiceItem);

            var resourceToReturn = item.ToDynamic() as IDictionary<string, object>;
            resourceToReturn.Add("links", links);

            return resourceToReturn;
        }

        private IEnumerable<LinkDto> GetLinks(int id)
        {
            var links = new List<LinkDto>();

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(GetSingleInvoice), new { id = id }),
              "self",
              "GET"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(RemoveInvoice), new { id = id }),
              "delete_Invoice",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link(nameof(AddInvoice), null),
              "create_Invoice",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link(nameof(UpdateInvoice), new { id = id }),
               "update_Invoice",
               "PUT"));

            return links;
        }
    }

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Invoices")]
    public class Invoices2Controller : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("2.0");
        }
    }
}
