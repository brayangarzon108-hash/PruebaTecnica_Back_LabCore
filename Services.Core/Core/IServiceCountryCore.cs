using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public interface IServiceCountryCore
    {
        #region Get

        /// <summary>
        /// Get  list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        Task<GeneralResponse> GetAll(int serviceId);

        /// <summary>
        /// Get list Summarry
        /// </summary>
        /// <returns>List<UserDto> </returns>
        Task<GeneralResponse> GetAllSummary();

        /// <summary>
        /// Create or Update
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        Task<GeneralResponse> Upsert(ServiceCountryRequest input);

        #endregion
    }
}
