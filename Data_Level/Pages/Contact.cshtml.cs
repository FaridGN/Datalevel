using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace Data_Level.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public ContactForm Contact { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mailbody = $@"Hello website owner,
            This is a new contact request from your website:

             LastName: {Contact.FullName}
             Email: {Contact.Email}
             Message: ""{Contact.Message}""

            Cheers,
            The websites contact form";

            SendMail(mailbody);

            return RedirectToPage("Index");
        }

        private void SendMail(string mailbody)
        {
            using (var message = new MailMessage(Contact.Email, "gnfarid@gmail.com"))
            {
                message.To.Add(new MailAddress("gnfarid@gmail.com"));
                message.From = new MailAddress(Contact.Email);
                message.Subject = "New E-Mail from my website";
                message.Body = mailbody;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("gnfarid@gmail.com", "369852147bca");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
        }
    }
}