using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public interface IServiceCore
    {
        #region Get

        /// <summary>
        /// Get  list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        Task<GeneralResponse> GetAll(int providerId);

        /// <summary>
        /// Create or Update
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        Task<GeneralResponse> Upsert(ServiceRequest input);

        #endregion
    }
}
