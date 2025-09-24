using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace Tenkus_core.Core
{
    public interface IProviderCustomFieldCore
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
        Task<GeneralResponse> Upsert(ProviderCustomFieldRequest input);

        #endregion
    }
}
