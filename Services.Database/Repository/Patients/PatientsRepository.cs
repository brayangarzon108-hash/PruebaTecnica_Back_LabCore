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
        public async Task<List<Patients>> GetAll(string document)
        {
            try
            {
                var data = _DbContext.Patients.Where(x => x.Enabled.HasValue && x.Enabled.Value).AsEnumerable();

                if (!string.IsNullOrEmpty(document))
                {
                    data = data.Where(x => x.Document.Contains(document));
                }

                return data.OrderByDescending(x => x.Id).ToList();
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
        public async Task<bool> GetSpecific(int typeDocument, string document)
        {
            try
            {
                return await _DbContext.Patients.Where(x => x.TypeDocument == typeDocument && x.Document == document).AnyAsync();
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
        public int UpsertDynamic(PatientsRequest input)
        {
            try
            {

                if (input.Id == 0)
                {
                    Patients insertDb = new Patients()
                    {
                        Id = input.Id,
                        TypeDocument = input.TypeDocument,
                        Document = input.Document,
                        CityId = input.CityId,
                        LastName = input.LastName,
                        Name = input.Name,
                        BirthDate = input.BirthDate,
                        Phone = input.Phone,
                        Email = input.Email,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Enabled = input.Enabled,
                        CreatedBy = input.UserId,
                        UpdatedBy = input.UserId
                    };
                    _DbContext.Patients.Add(insertDb);
                    _DbContext.SaveChanges();
                }
                else
                {
                    var recordDb = _DbContext.Patients.Where(x => x.Id == input.Id).FirstOrDefault();
                    recordDb.Enabled = input.Enabled;
                    recordDb.Name = input.Name;
                    recordDb.LastName = input.LastName;
                    recordDb.BirthDate = input.BirthDate;
                    recordDb.CityId = input.CityId;
                    recordDb.Phone = input.Phone;
                    recordDb.Email = input.Email;
                    recordDb.UpdatedDate = DateTime.Now;

                    _DbContext.SaveChanges();
                }

                return _DbContext.Patients.Where(x => x.TypeDocument == input.TypeDocument && x.Document == input.Document).Max(x => x.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
