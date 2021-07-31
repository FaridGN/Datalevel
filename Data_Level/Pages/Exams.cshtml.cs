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
    public class ExamsModel : PageModel
    {
        [BindProperty]
        public ExamForm ExamForm { get; set; }

        public void OnGet()
        {
           
        }

        public IActionResult OnPostAsync(string myexam)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ExamForm.Exam = myexam;

            var mailbody = $@"Hello website owner,
            This is a new exam request from your website:

             FullName: {ExamForm.FullName}
             Email: {ExamForm.Email}
             Id Seriya: {ExamForm.Identity}
             Exam: {ExamForm.Exam}

            Cheers,
            The websites contact form";

            SendMail(mailbody);

            return RedirectToPage("ExamMessage");
        }

        private void SendMail(string mailbody)
        {
            using (var message = new MailMessage(ExamForm.Email, "gnfarid@gmail.com"))
            {
                message.To.Add(new MailAddress("gnfarid@gmail.com"));
                message.From = new MailAddress(ExamForm.Email);
                message.Subject = "Imtahana Qeydiyyat";
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