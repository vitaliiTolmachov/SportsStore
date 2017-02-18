using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsstore@example.com";
        public string UserName = "mySmtpUserName";
        public string Password = "myPwd";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool UseSSL = true;
        //Set to true if U don't have connection and want to save email as file
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\Vitalii Tolmachov\Documents\Visual Studio 2013\Projects\MVC Practice\SportsStore\SportsStore\Emails";

        public string Subject = "New order submited";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings) {
            this.emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingCartDetails shippingCartDetails) {
            using (var smtpClient = new SmtpClient()) {
                smtpClient.EnableSsl = emailSettings.UseSSL;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                if (emailSettings.WriteAsFile) {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                var body = new StringBuilder()
                    .AppendLine("A new order has been submited")
                    .AppendLine("---")
                    .AppendLine("Items:");
                foreach (CartLine line in cart.Lines) {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat(CultureInfo.CurrentCulture,
                        "{0} x {1} (subtotal {2:C})",
                        line.Product.Name, line.Quantity, subtotal);
                }
                body.AppendFormat(CultureInfo.CurrentCulture,
                    "Total order value: {0:C}", cart.GetTotal())
                    .AppendLine("Ship to: ")
                    .AppendLine(shippingCartDetails.Name)
                    .AppendLine(shippingCartDetails.Line1)
                    .AppendLine(shippingCartDetails.Line2 ?? "")
                    .AppendLine(shippingCartDetails.Line3 ?? "")
                    .AppendLine(shippingCartDetails.City)
                    .AppendLine(shippingCartDetails.State ?? "")
                    .AppendLine(shippingCartDetails.Country)
                    .AppendLine(shippingCartDetails.Zip)
                    .AppendLine("-------------")
                    .AppendFormat(CultureInfo.CurrentCulture,
                        "Gift wrap: {0}", shippingCartDetails.GiftWrap ? "Yes" : "No");
                MailMessage message = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    emailSettings.Subject,
                    body.ToString());
                if (emailSettings.WriteAsFile) {
                    message.BodyEncoding = Encoding.ASCII;
                }
                //TODO: Make async
                smtpClient.Send(message);
            }
        }
    }
}
