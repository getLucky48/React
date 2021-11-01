using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Vipros.Kernel.Layer.Xdpp;

namespace React.Controllers
{
    [Route("api/data/subjjur")]
    [Produces("application/json")]
    [ApiController]
    public class SubjjurController : ControllerBase
    {

        [HttpGet]
        public JsonResult Get() { return Proccess(); }

        [HttpPost]
        public JsonResult Post() { return Proccess(); }

        private JsonResult Proccess()
        {

            List<Subj_jur> sjList = Subj_jur.GetList();

            string data = JsonConvert.SerializeObject(sjList);

            JsonResult response = new JsonResult(data);

            return response;

        }

    }

}
