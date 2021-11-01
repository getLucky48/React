using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Vipros.Kernel.Layer.Xdpp;

namespace React.Controllers
{
    [Route("api/data/schedulers")]
    [Produces("application/json")]
    [ApiController]
    public class DataController : ControllerBase
    {

        [HttpGet]
        public JsonResult Get()
        {

            string sort = this.Request.Query["sort"];
            string[] range = this.Request.Query["range"].ToString()
                                    .Replace("\"", string.Empty)
                                    .Replace("[", string.Empty)
                                    .Replace("]", string.Empty)
                                    .Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);


            Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(this.Request.Query["filter"].ToString());

            int rangeStart = int.Parse(range[0]);
            int rangeEnd = int.Parse(range[1]);
            int count = rangeEnd - rangeStart;

            List<Hdpp_scheduler_form> source = Hdpp_scheduler_form.GetList();
            List<Hdpp_scheduler_form> schedulerList = source;

            #region filters
            
            if (filter.ContainsKey("subj_jur_id")) { schedulerList = schedulerList.FindAll(t => filter["subj_jur_id"].Contains(t.subj_jur_id.ToString())); }

            int countOfFiltered = schedulerList.Count;

            if(rangeEnd > countOfFiltered) { count = countOfFiltered - rangeStart; }

            schedulerList = schedulerList.GetRange(rangeStart, count);           

            #endregion

            this.Response.Headers.Add("Content-Range", $"{rangeStart}-{rangeEnd}/{countOfFiltered}");

            string data = JsonConvert.SerializeObject(schedulerList);

            JsonResult response = new JsonResult(data);

            return response;

        }

    }

}
