namespace Polokwane_surf.Server.Service
{
    public class EmailService
    {
        public void ValidateContactForm(string name, string email, string phoneNumber, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.");

            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject is required.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Valid email is required.");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is required.");
        }
    }
}
