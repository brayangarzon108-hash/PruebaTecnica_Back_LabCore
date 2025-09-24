using DbContexto;
using Microsoft.EntityFrameworkCore;
using StoreSample.Domain.Model.Dto;

namespace sst_database.sst_database.DbCore
{
    public class CityRepository : ICityRepository
    {
        private readonly DatabaseContext _DbContext;

        public CityRepository(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        #region Get
        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        public async Task<List<City>> GetAll()
        {
            try
            {
                return await _DbContext.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        public async Task<bool> GetAllAny(string isoCode)
        {
            try
            {
                return await _DbContext.Countries.AnyAsync(x => x.IsoCode == isoCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Create or update 
        /// </summary>
        /// <param name="input"></param>
        /// <returns>List<DynamicFormDto></returns>
        public void UpsertDynamic(CountryRequest input)
        {
            try
            {

                if (input.CountryId == 0)
                {
                    City insertDb = new City()
                    {
                        IsoCode = input.IsoCode,
                        CountryId = input.CountryId,
                        Name = input.Name,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Enabled = input.Enabled,
                        CreatedBy = input.UserId,
                        UpdatedBy = input.UserId
                    };
                    _DbContext.Countries.Add(insertDb);
                    _DbContext.SaveChanges();
                }
                else
                {
                    var recordDb = _DbContext.Countries.Where(x => x.CountryId == input.CountryId).FirstOrDefault();
                    recordDb.Enabled = input.Enabled;
                    recordDb.UpdatedDate = DateTime.Now;

                    _DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
