namespace Domain.BusinessBase
{
    public class BusinessMessage
    {
        public string MessageCode { get; set; }
        public string MessageText { get; set; }
        public bool IsEndUserMessage { get; set; }
        public bool IsValidationMessage { get; set; }
    }
}
