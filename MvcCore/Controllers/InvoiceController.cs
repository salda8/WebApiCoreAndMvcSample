using DataStructures;
using DataStructures.Dtos;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Api;

namespace MvcCore.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IApiClient client;

        public InvoiceController(IApiClient client)
        {
            this.client = client;
        }

        [ActionName(nameof(DeleteInvoiceItem))]
        public ActionResult DeleteInvoiceItem(int id)
        {
            var response = client.DeleteDtoSync(id, ApiResources.InvoiceItem);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult CreateEditInvoice(int? id)
        {
            InvoiceDto invoice = new InvoiceDto();
            if (id != null)
            {
                invoice = client.GetByIdDtoSync<InvoiceDto>(ApiResources.Invoice, id.Value);
            }

            return View(invoice);
        }

        /// <summary>
        /// Creates the edit invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult CreateEditInvoice(InvoiceDto invoice)
        {
            if (invoice.Id == 0)
            {
                var response = client.PostDtoSync(invoice, ApiResources.Invoice);
            }
            else
            {
                var response = client.PutDtoSync(invoice, invoice.Id, ApiResources.Invoice);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult CreateInvoiceItem(InvoiceItemDto invoiceItem)
        {
            client.PostDtoSync(invoiceItem, ApiResources.InvoiceItem);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult CreateInvoiceItem(int id)
        {
            return View(new InvoiceItemCreateDto() { InvoiceId = id });
        }

        [ActionName(nameof(DeleteInvoice))]
        public ActionResult DeleteInvoice(int id)
        {
            var response = client.DeleteDtoSync(id, ApiResources.Invoice);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult DetailInvoice(int id)
        {
            var response = client.GetByIdDtoSync<InvoiceDto>(ApiResources.Invoice, id);
            return View(response);
        }

        public ActionResult Index()
        {
            return View(client.GetAllDtoSync<InvoiceDto>(ApiResources.Invoice));
        }

        public ActionResult PayInvoice(int id)
        {
            var response = client.PatchDtoSync(id, ApiResources.Invoice);
            return RedirectToAction(nameof(Index));
        }
    }
}