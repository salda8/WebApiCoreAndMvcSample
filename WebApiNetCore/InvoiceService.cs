using WebApiNetCore.Repositories;

namespace WebApiNetCore
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }
    }
}