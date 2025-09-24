using DbContexto;
using Microsoft.EntityFrameworkCore;
using StoreSample.Domain.Model.Dto;

namespace sst_database.sst_database.DbCore
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly DatabaseContext _DbContext;

        public PatientsRepository(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        #region Get
        /// <summary>
        /// Get list
        /// </summary>
        /// <returns>List<CatalogDto> </returns>
        public async Task<List<Patients>> GetAll(int providerId)
        {
            try
            {
                return await _DbContext.Services.Where(x => x.ProviderId == providerId).ToListAsync();
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
        public int UpsertDynamic(ServiceRequest input)
        {
            try
            {

                if (input.ServiceId == 0)
                {
                    Patients insertDb = new Patients()
                    {
                        ServiceId = input.ServiceId,
                        ProviderId = input.ProviderId,
                        Name = input.Name,
                        HourlyRate = input.HourlyRate,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Enabled = input.Enabled,
                        CreatedBy = input.UserId,
                        UpdatedBy = input.UserId
                    };
                    _DbContext.Services.Add(insertDb);
                    _DbContext.SaveChanges();
                }
                else
                {
                    var recordDb = _DbContext.Services.Where(x => x.ServiceId == input.ServiceId).FirstOrDefault();
                    recordDb.Enabled = input.Enabled;
                    recordDb.Name = input.Name;
                    recordDb.HourlyRate = input.HourlyRate;
                    recordDb.UpdatedDate = DateTime.Now;

                    _DbContext.SaveChanges();
                }

                return _DbContext.Services.Where(x => x.ProviderId == input.ProviderId).Max(x => x.ServiceId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
