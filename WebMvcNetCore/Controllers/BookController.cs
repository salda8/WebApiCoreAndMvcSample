using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using WebMvcNetCore.Models;
using Newtonsoft.Json;


namespace EF.Web.Controllers
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
            var restRequest = new RestRequest("api/invoice/", Method.GET);
            var response = client.Execute(restRequest);
            var deserializedRespone = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Invoice>>(response.Content);
            return View(deserializedRespone);
        }

        public ActionResult CreateEditInvoice(int? id)
        {
            Invoice model = new Invoice();
            if (id.HasValue)
            {
                model = InvoiceRepository.GetById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditInvoice(Invoice model)
        {
            if (model.ID == 0)
            {
                model.ModifiedDate = System.DateTime.Now;
                model.AddedDate = System.DateTime.Now;
                model.IP = Request.UserHostAddress;
                InvoiceRepository.Insert(model);
            }
            else
            {
                var editModel = InvoiceRepository.GetById(model.ID);
                editModel.Title = model.Title;
                editModel.Author = model.Author;
                editModel.ISBN = model.ISBN;
                editModel.Published = model.Published;
                editModel.ModifiedDate = System.DateTime.Now;
                editModel.IP = Request.UserHostAddress;
                InvoiceRepository.Update(editModel);
            }

            if (model.ID > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DeleteInvoice(int id)
        {
            Invoice model = InvoiceRepository.GetById(id);
            return View(model);
        }

        [HttpPost,ActionName("DeleteInvoice")]
        public ActionResult ConfirmDeleteInvoice(int id)
        {
            Invoice model = InvoiceRepository.GetById(id);
            InvoiceRepository.Delete(model);
            return RedirectToAction("Index");
        }

        public ActionResult DetailInvoice(int id)
        {
            Invoice model = InvoiceRepository.GetById(id);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
