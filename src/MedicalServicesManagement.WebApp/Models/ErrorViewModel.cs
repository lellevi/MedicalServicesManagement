namespace MedicalServicesManagement.WebApp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorMessage { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel(string errorMessage, string controllerName = "Home", string actionName = "Index")
        {
            ErrorMessage = errorMessage;
            ControllerName = controllerName;
            ActionName = actionName;
        }
    }
}
