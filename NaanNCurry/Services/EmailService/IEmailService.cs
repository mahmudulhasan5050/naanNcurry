using NaanNCurry.Model;

namespace NaanNCurry.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDataTransferObject request);
    }
}
