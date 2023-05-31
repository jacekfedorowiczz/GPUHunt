namespace GPUHunt.MVC.Models
{
    public class Notification
    {
        public Notification(string type, string message)
        {
            Type = type ??
                throw new ArgumentNullException(nameof(type));
            Message = message ??
                throw new ArgumentNullException(nameof(message));

        }

        public string Type { get; set;  }
        public string Message { get; set;  }
    }
}
