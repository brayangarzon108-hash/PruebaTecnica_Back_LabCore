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
                return await _DbContext.Cities.ToListAsync();
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
                return await _DbContext.Cities.AnyAsync(x => x.IsoCode == isoCode);
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
        public void UpsertDynamic(CityRequest input)
        {
            try
            {

                if (input.CityId == 0)
                {
                    City insertDb = new City()
                    {
                        IsoCode = input.IsoCode,
                        CityId = input.CityId,
                        Name = input.Name,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Enabled = input.Enabled,
                        CreatedBy = input.UserId,
                        UpdatedBy = input.UserId
                    };
                    _DbContext.Cities.Add(insertDb);
                    _DbContext.SaveChanges();
                }
                else
                {
                    var recordDb = _DbContext.Cities.Where(x => x.CityId == input.CityId).FirstOrDefault();
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
