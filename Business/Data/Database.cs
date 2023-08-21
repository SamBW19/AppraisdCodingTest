using Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    public interface IDatabase
    {
        Appraisal AddNewAppraisal(Appraisal newAppraisal);
        List<Appraisal> GetAllAppraisals();
        Appraisal UpdateAppraisal(Appraisal appraisalToUpdate);
    }

    public class Database : IDatabase
    {
        
        private static string APPRAISALS_PATH =  "..\\..\\Database\\Appraisals.json";
        public List<Appraisal> GetAllAppraisals()
        {
            var data = File.ReadAllText(APPRAISALS_PATH);
            if (string.IsNullOrEmpty(data)) return new List<Appraisal>();
            return JsonConvert.DeserializeObject<List<Appraisal>>(data);
        }

        public Appraisal AddNewAppraisal(Appraisal newAppraisal)
        {
            var appraisals = GetAllAppraisals();
            var currentMaxID = appraisals.Any() ? appraisals.Max(x => x.AppraisalID) : 0;
            newAppraisal.AppraisalID = currentMaxID + 1;
            appraisals.Add(newAppraisal);
            File.WriteAllText(APPRAISALS_PATH, JsonConvert.SerializeObject(appraisals,Formatting.Indented));
            return newAppraisal;
        }

        public Appraisal UpdateAppraisal(Appraisal appraisalToUpdate)
        {
            var appraisals = GetAllAppraisals();
            var appraisalsWithoutUpdate = appraisals.Where(x => x.AppraisalID != appraisalToUpdate.AppraisalID).ToList();
            appraisalsWithoutUpdate.Add(appraisalToUpdate);
            File.WriteAllText(APPRAISALS_PATH, JsonConvert.SerializeObject(appraisalsWithoutUpdate, Formatting.Indented));
            return appraisalToUpdate;
        }
    }
}
