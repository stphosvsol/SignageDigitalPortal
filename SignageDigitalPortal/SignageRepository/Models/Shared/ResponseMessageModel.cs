namespace SignageRepository.Models.Shared
{
    public class ResponseMessageModel
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string UrlToGo { get; set; }
        public bool ExceptionMessage { get; set; }
        public string Title { get; set; }
        public bool HasInnExc { get; set; }
        public string Id { get; set; }
    }
}

