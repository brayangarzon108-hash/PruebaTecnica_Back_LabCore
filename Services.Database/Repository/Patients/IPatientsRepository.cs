using StoreSample.Domain.Model.Dto;

namespace sst_database.sst_database.DbCore
{
    public interface IPatientsRepository
    {
        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        Task<List<Patients>> GetAll(int providerId);

        /// <summary>
        /// Create or update 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFormDto></returns>
        public int UpsertDynamic(ServiceRequest input);

    }
}
