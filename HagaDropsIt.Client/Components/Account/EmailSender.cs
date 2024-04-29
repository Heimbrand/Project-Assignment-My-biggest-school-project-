using Azure;
using Azure.Communication.Email;
using HagaDropsIt.Client.Data;
using HagaDropsIt.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace HagaDropsIt.Client.Components.Account
{
    internal sealed class EmailSender : IEmailSender<ApplicationUser>
    {
        private readonly EmailClient emailClient;
        private string? sendersEmail = "DoNotReply@hagadrops.shop";
        private string connectionString;

        public EmailSender(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureCommunicationServices") ?? throw new ArgumentNullException("AzureCommunicationServices connection string is missing");
            emailClient = new EmailClient(connectionString) ?? throw new ArgumentNullException("EmailClient is null");
            this.sendersEmail = configuration["SendersEmail"] ?? throw new ArgumentNullException("SendersEmail is null");

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Started,
                senderAddress: sendersEmail,
                recipientAddress: email,
                subject: subject,
                htmlContent: htmlMessage,
                plainTextContent: "");

            return Task.CompletedTask;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
    }
}
