using System.Net;

namespace Sprout.Exam.Common.ResultHandlingDto
{
    public class ActionResult
    {
        public ActionResult(string message, HttpStatusCode httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }
        public HttpStatusCode HttpStatusCode { get; set; }
        public int StatusCode => (int)HttpStatusCode;
        public string Message { get; set; }
        public bool IsSuccessful => StatusCode >= 200 && StatusCode <= 201;
    }
}
