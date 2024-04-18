namespace ASPCoreWebAPI.Models
{
    public class clsEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
