using MoistureContent.Models;
using Newtonsoft.Json.Linq;

namespace MoistureContent.Data
{
    public class DataService
    {

        private readonly string filePath = @"Data.json";

        public DataService()
        {

        }

        ///<summary>
        /// INCOMPLETE: Intended to save incoming data from Web into JSON file
        /// </summary>
        public void SaveData(WaterContentModel model)
        {
            var moistureData = "{'method': " + model.Preparationmodels.Method_Type + "}";
            try
            {
                var json = File.ReadAllText(filePath);
                var jsonObject = JObject.Parse(json);
                var data = jsonObject.GetValue("data") as JArray;
                var newData = JObject.Parse(moistureData);
                data.Add(newData);

                jsonObject["data"] = data;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, newJsonResult);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}