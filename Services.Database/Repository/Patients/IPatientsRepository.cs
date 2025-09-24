using StoreSample.Domain.Model.Dto;

namespace sst_database.sst_database.DbCore
{
    public interface IPatientsRepository
    {
        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        Task<List<Patients>> GetAll(string document);

        Task<bool> GetSpecific(int typeDocument, string document);

        /// <summary>
        /// Create or update 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFormDto></returns>
        public int UpsertDynamic(PatientsRequest input);

    }
}
