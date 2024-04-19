namespace ASPCoreWebAPI.Models
{
    public class FileModel
    {
        public string FileName { get; set; } = string.Empty;
        public IFormFile file { get; set; }
    }
}
