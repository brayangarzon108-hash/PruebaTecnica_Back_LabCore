using StoreSample.Domain.Model.Dto;

namespace sst_database.sst_database.DbCore
{
    public interface ICityRepository
    {
        /// <summary>
        /// Get  list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        Task<List<City>> GetAll();

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        Task<bool> GetAllAny(string isoCode);

        /// <summary>
        /// Create or update 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFormDto></returns>
        void UpsertDynamic(CountryRequest input);

    }
}
