using System.Collections.Generic;

namespace AllinOneStock.Models
{
    public class BloggerModel
    {
        public List<Messages> MssagesList { get; set; } = new List<Messages>();
    }
    public class Messages
    {
        public string SenderClient { get; set; }
        public string ReciverClient { get; set; }
        public string Message { get; set; }
    }
}
