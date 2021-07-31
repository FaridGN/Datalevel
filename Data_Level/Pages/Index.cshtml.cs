using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Data_Level.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public RegisterForm RegisterForm { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPostAsync(string mycourse)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RegisterForm.Course = mycourse;

            var mailbody = $@"Hello website owner,
            This is a new contact request from your website:

             FullName: {RegisterForm.FullName}
             Email: {RegisterForm.Email}
             Phone: {RegisterForm.Phone}
             Courses: {RegisterForm.Course}

            Cheers,
            The websites contact form";

            SendMail(mailbody);

            return RedirectToPage("SuccessMessage");
        }

        private void SendMail(string mailbody)
        {
            using (var message = new MailMessage(RegisterForm.Email, "gnfarid@gmail.com"))
            {
                message.To.Add(new MailAddress("gnfarid@gmail.com"));
                message.From = new MailAddress(RegisterForm.Email);
                message.Subject = "Qeydiyyat";
                message.Body = mailbody;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("gnfarid@gmail.com", "@Gf369852147");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
        }
    }
}
