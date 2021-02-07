using BackEnd.BAL.Models;
using BackEnd.Service.IService;
using EmailService;
using System;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
  public class emailService : IemailService
  {
    private readonly IEmailSender _emailSender;

    public emailService(IEmailSender emailSender)
    {
      _emailSender = emailSender;
    }
    public async Task<bool> sendVerfication(int verficationCode,string Email)
    {
        //string Url = "http://localhost:4200/verfication/" + verficationCode.ToString();
      string code = "<p style='color:#0678F4'>" + "Verfication Code : "+verficationCode.ToString() + "</p>";
      var message = new Message(new string[] { Email }, "Gssor", code);
      await _emailSender.SendEmailAsync(message);

      return true;
    }

    public async Task<bool> sendResetPasswordCode(int forgetpasswordCode, string Email)
    {
        //string Url = "http://localhost:4200/verfication/" + verficationCode.ToString();
        string code ="<p style='color:#0678F4'>" + "Forget Password Code : " + forgetpasswordCode.ToString() + "</p>";
        var message = new Message(new string[] { Email }, "Gssor", code);
        await _emailSender.SendEmailAsync(message);

        return true;
    }
        public IResponseDTO SendEmail(string to,string Subject, string body)
        {
          
            try
            {
                System.Net.Mail.MailMessage MailMessageObj = new System.Net.Mail.MailMessage();
                MailMessageObj.From = new System.Net.Mail.MailAddress("");
                MailMessageObj.To.Add(to);
                MailMessageObj.Subject = Subject;
                MailMessageObj.Body = body;
                MailMessageObj.BodyEncoding = System.Text.Encoding.UTF8;
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;//587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "Atia@261187");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(MailMessageObj);
               
                return new ResponseDTO
                {
                    Data = null,
                    Code = 200,
                    Message = "OK"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Code = 400,
                    Message = ex.Message
                };
            }
          
        }
    }
    public class SendMail
    {
        public string ToEmail{ get; set; }
        public string Subject { get; set; }
        public string body { get; set; }

    }
}
