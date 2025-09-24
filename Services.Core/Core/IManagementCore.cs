using StoreSample.Domain.Model.General;

namespace Tenkuscore.Core
{
    public interface IManagementCore
    {

        #region Token

        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        GeneralResponse GenerateToken(string user);

        #endregion
    }
}
