using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToogleApi.Models;

namespace ToogleApi.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddFlagRecord(FeatureFlag flag);
        void UpdateFlagRecord(FeatureFlag flag);
        void DeleteFlagRecord(string id);
        FeatureFlag GetFlagByIdSingleRecord(string id);
        List<FeatureFlag> GetFlagsRecords();

        FeatureFlag GetFlagByKeySingleRecord(string key);
    }
}
