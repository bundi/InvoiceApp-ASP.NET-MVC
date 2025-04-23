using System.Net;
using System.Numerics;
using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceApp.Pages.Invoices
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public InvoiceDto InvoiceDto { get; set; } = new InvoiceDto();

        public Invoice Invoice { get; set; }

        private readonly ApplicationDbContext context;

        public EditModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult OnGet(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            Invoice = invoice;

            InvoiceDto.Number = Invoice.Number;
            InvoiceDto.Status = Invoice.Status;
            InvoiceDto.IssueDate = Invoice.IssueDate;
            InvoiceDto.DueDate = Invoice.DueDate;

            InvoiceDto.Service = Invoice.Service;
            InvoiceDto.UnitPrice = Invoice.UnitPrice;
            InvoiceDto.Quantity = Invoice.Quantity;

            InvoiceDto.ClientName = Invoice.ClientName;
            InvoiceDto.Email = Invoice.Email;
            InvoiceDto.Phone = Invoice.Phone;
            InvoiceDto.Address = Invoice.Address;

            return Page();

        }

        public string successMessage = "";

        public IActionResult OnPost(int id)
        {
            var invoice = context.Invoices.Find(id);
            if (invoice == null)
            {
                return RedirectToAction("/Invoices/Index");
            }

            Invoice = invoice;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Invoice.Number = InvoiceDto.Number;
            Invoice.Status = InvoiceDto.Status;
            Invoice.IssueDate = InvoiceDto.IssueDate;
            Invoice.DueDate = InvoiceDto.DueDate;

            Invoice.Service = InvoiceDto.Service;
            Invoice.UnitPrice = InvoiceDto.UnitPrice;
            Invoice.Quantity = InvoiceDto.Quantity;

            Invoice.ClientName = InvoiceDto.ClientName;
            Invoice.Email = InvoiceDto.Email;
            Invoice.Phone = InvoiceDto.Phone;
            Invoice.Address = InvoiceDto.Address;

            context.SaveChanges();

            successMessage = "Invoice Updated Successfully";

            return Page();
        }
    }
}
