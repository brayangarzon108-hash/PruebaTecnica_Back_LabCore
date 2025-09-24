namespace StoreSample.Domain.Model.General
{
    public class Enumerations
    {
        public enum enumTypeMessageResponse
        {
            Success = 200,
            Error = 500,
            BadRequest = 400,
            NotFound = 404
        }

        public enum enumTypeCatalog
        {
            Area = 11,
            Company = 7,
            ComplianceControl = 2,
            TasksCritics = 31,
            Management=12,
            ExpiredEpps = 50,
        }
    }
}
