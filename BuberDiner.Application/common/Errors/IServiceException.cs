using System.Net;

namespace BuberDiner.Application.Common.Errors;

public interface IServiceException {
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; }
}