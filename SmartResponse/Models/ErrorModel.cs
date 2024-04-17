namespace SmartResponse.Models
{
    public class ErrorModel
    {
        public string? FieldName { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }

        public string FieldLang { get; set; }// = nameof(JsonLanguageModel.Default);
    }
}
