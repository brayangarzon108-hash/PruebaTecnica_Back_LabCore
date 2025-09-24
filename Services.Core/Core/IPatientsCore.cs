using StoreSample.Domain.Model.Dto;
using StoreSample.Domain.Model.General;

namespace sst_core.Core
{
    public interface IPatientsCore
    {
        #region Get

        /// <summary>
        /// Get  list
        /// </summary>
        /// <returns>List<UserDto> </returns>
        Task<GeneralResponse> GetAll(string document);

        /// <summary>
        /// Create or Update
        /// </summary>
        /// <param name="input">UserDto</param>
        /// <returns> List<UserDto></returns>
        Task<GeneralResponse> Upsert(PatientsRequest input);

        #endregion
    }
}
