namespace Modules.Shared
{
    public class R<T>
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = "";
        public T? Payload { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}