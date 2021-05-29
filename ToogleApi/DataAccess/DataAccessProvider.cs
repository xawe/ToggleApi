using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToogleApi.Models;

namespace ToogleApi.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {            
            _context = context;
        }
        void IDataAccessProvider.AddFlagRecord(FeatureFlag flag)
        {
            _context.FeatureFlag.Add(flag);
            _context.SaveChanges();
        }

        void IDataAccessProvider.DeleteFlagRecord(string id)
        {
            var entity = _context.FeatureFlag.FirstOrDefault(t => t.Id == id);
            _context.FeatureFlag.Remove(entity);
            _context.SaveChanges();
        }

        FeatureFlag IDataAccessProvider.GetFlagByIdSingleRecord(string id)
        {
            return _context.FeatureFlag.FirstOrDefault(t => t.Id == id);
        }

        List<FeatureFlag> IDataAccessProvider.GetFlagsRecords()
        {
            return _context.FeatureFlag.ToList();
        }

        void IDataAccessProvider.UpdateFlagRecord(FeatureFlag flag)
        {
            _context.FeatureFlag.Update(flag);
            _context.SaveChanges();
        }
    }
}
