using System;

namespace LiBook.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public Exception Exception { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}