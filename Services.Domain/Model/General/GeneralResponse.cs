namespace StoreSample.Domain.Model.General
{
    public class GeneralResponse
    {

        public GeneralResponse()
        {
                
        }
        public int Status { get; set; } = (int)Enumerations.enumTypeMessageResponse.Success;
        public dynamic? Data { get; set; }
        public string Message { get; set; } = "Proceso Realizado exitosamente";
    };
}
