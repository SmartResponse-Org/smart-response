namespace SmartResponse.Models
{
    public class ErrorModel
    {
        public bool IsInputError { get => !string.IsNullOrWhiteSpace(FieldName); }

        public string? FieldName { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
