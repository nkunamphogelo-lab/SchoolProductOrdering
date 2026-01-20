using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolProductOrdering.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace SchoolProductOrdering.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "First name is required")]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;

        [BindProperty]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Cellphone number is required")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Delivery location is required")]
        public string DeliveryLocation { get; set; } = string.Empty;

        // Capture the payment method from the radio buttons
        [BindProperty]
        public string PaymentMethod { get; set; } = "CreditCard";

        public decimal TotalAmount { get; set; }

        public void OnGet()
        {
            LoadCartTotal();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadCartTotal();
                return Page();
            }

            // 1. Calculate dates for the email and success page
            string orderDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            string deliveryDate = DateTime.Now.AddDays(3).ToString("dddd, dd MMMM yyyy");

            // 2. SEND THE REAL EMAIL
            try
            {
                await SendRealEmail(Email, Name, orderDate, deliveryDate);
                TempData["EmailStatus"] = "Sent";
            }
            catch (Exception ex)
            {
                // If email fails (e.g., no internet), we still want the order to complete
                Console.WriteLine("Email failed: " + ex.Message);
                TempData["EmailStatus"] = "Failed";
            }

            // 3. Prepare data for OrderSuccess page
            TempData["OrderDate"] = orderDate;
            TempData["DeliveryDate"] = deliveryDate;
            TempData["UserEmail"] = Email;

            // 4. Clear the Cart
            HttpContext.Session.Remove("CartItems");
            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToPage("./OrderSuccess");
        }

        private async Task SendRealEmail(string toEmail, string userName, string orderDate, string deliveryDate)
        {
            var fromAddress = new MailAddress("your-email@gmail.com", "LearnMate Stationery");
            var toAddress = new MailAddress(toEmail, userName);
            const string fromPassword = "your-app-password"; // Not your regular password!

            string subject = "Order Confirmation - LearnMate";
            string body = $@"
        <h1>Hello {userName},</h1>
        <p>Thank you for your order with LearnMate!</p>
        <p><strong>Order Date:</strong> {orderDate}</p>
        <p><strong>Estimated Delivery:</strong> {deliveryDate}</p>
        <p>We are currently preparing your stationery for school!</p>
        <br>
        <p>Regards,<br>The LearnMate Team</p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

        private void LoadCartTotal()
        {
            var cartJson = HttpContext.Session.GetString("CartItems");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
                TotalAmount = cart.Sum(item => item.Price * item.Quantity);
            }
        }

        private void SimulateEmailSending(string email)
        {
            // Logs to the Output window in Visual Studio
            Console.WriteLine($"[EMAIL SENT]: Order confirmation and receipt sent to {email}");
        }
    }
}