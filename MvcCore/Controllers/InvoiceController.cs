using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using WebApiNetCore.Dtos;

namespace WebMvcNetCore.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IRestClient client;

        public InvoiceController(IRestClient client)
        {
            this.client = client;
        }

        public ActionResult Index()
        {
            var restRequest = new RestRequest("api/invoice", Method.GET)
                .AddQueryParameter("api-version", "1")
                .AddHeader("x-api-key", "Secret007");

            var response = client.Execute(restRequest);
            var deserializedRespone = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<InvoiceDto>>(response.Content);
            return View(deserializedRespone);
        }

        [HttpPost]
        public ActionResult CreateInvoiceItem(InvoiceItemDto invoiceItem)
        {
            var postRestRequest = new RestRequest($"api/invoiceItem", Method.POST)
             .AddQueryParameter("api-version", "1")
             .AddHeader("x-api-key", "Secret007")
            .AddJsonBody(invoiceItem);

            var response = client.Execute(postRestRequest);
            return RedirectToAction("Index");
        }

        public ActionResult CreateInvoiceItem(int id)
        {

            return View(new InvoiceItemCreateDto() { InvoiceId = id });
        }
        [HttpGet]
        public ActionResult CreateEditInvoice(int? id)
        {
            InvoiceDto invoice = new InvoiceDto();
            if (id != null)
            {
                var restRequest = new RestRequest($"api/invoice/{id}", Method.GET)
               .AddQueryParameter("api-version", "1")
               .AddHeader("x-api-key", "Secret007");

                var response = client.Execute(restRequest);
                invoice = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceDto>(response.Content);
            }

            return View(invoice);
        }

        [HttpPost]
        public ActionResult CreateEditInvoice(InvoiceDto invoice)
        {
            if (invoice.Id == 0)
            {
                var postRestRequest = new RestRequest($"api/invoice", Method.POST)
              .AddQueryParameter("api-version", "1")
              .AddHeader("x-api-key", "Secret007")
               .AddJsonBody(invoice);

                var response = client.Execute(postRestRequest);
            }
            else
            {
                var restRequest = new RestRequest($"api/invoice/{invoice.Id}", Method.PUT).AddJsonBody(invoice)
              .AddQueryParameter("api-version", "1")

              .AddHeader("x-api-key", "Secret007");

                var response = client.Execute(restRequest);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteInvoice(int id)
        {
            var restRequest = new RestRequest($"api/invoice/{id}", Method.DELETE)
              .AddQueryParameter("api-version", "1")
              .AddHeader("x-api-key", "Secret007");

            var response = client.Execute(restRequest);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteInvoice")]
        public ActionResult ConfirmDeleteInvoice(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult DetailInvoice(int id)
        {
            var restRequest = new RestRequest($"api/invoice/{id}", Method.GET)
               .AddQueryParameter("api-version", "1")
               .AddHeader("x-api-key", "Secret007");

            var response = client.Execute(restRequest);
            var deserializedRespone = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceDto>(response.Content);
            return View(deserializedRespone);
        }

        public ActionResult PayInvoice(int id)
        {
            var restRequest = new RestRequest($"api/invoice/{id}", Method.PATCH)
            .AddQueryParameter("api-version", "1")

            .AddHeader("x-api-key", "Secret007");

            var response = client.Execute(restRequest);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteInvoiceItem")]
        public ActionResult ConfirmDeleteInvoiceItem(int id)
        {
            var restRequest = new RestRequest($"api/invoiceItem/{id}", Method.DELETE)
              .AddQueryParameter("api-version", "1")
              .AddHeader("x-api-key", "Secret007");

            var response = client.Execute(restRequest);
            //Invoice model = InvoiceRepository.GetById(id);
            //InvoiceRepository.Delete(model);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}