using System.ComponentModel.DataAnnotations;

namespace Knab.Identity.Api.Models;

public class ErrorViewModel
{
    public ErrorViewModel()
    {
    }

    public ErrorViewModel(string? requestId, string error, string errorDescription)
    {
        RequestId = requestId;
        Error = error;
        ErrorDescription = errorDescription;
    }

    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


    [Display(Name = "Error")] public string? Error { get; set; }

    [Display(Name = "Description")] public string? ErrorDescription { get; set; }
}