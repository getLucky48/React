using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Vipros.Kernel.Layer.Xdpp;

namespace React.Controllers
{
    [Route("api/data/passportform")]
    [Produces("application/json")]
    [ApiController]
    public class PassportFormController : ControllerBase
    {

        [HttpGet]
        public JsonResult Get() { return Proccess(); }

        [HttpPost]
        public JsonResult Post() { return Proccess(); }

        private JsonResult Proccess()
        {

            string depProp = this.Request.Query["depproperty"];
            string levelNum = this.Request.Query["levelnum"];
            string dependency = this.Request.Query["hdpp_passport_form_ver_id"];


            //level 0 guid
            List<Guid> pfGuidListLvl_0 = new List<Guid>()
            {
                Guid.Parse("2d9bc918-3fc7-4c81-b67c-39aa92d1c3ab"),
                Guid.Parse("448cf317-7068-45f8-bf8c-e7ac2d5ce5f1"),
                Guid.Parse("f6295ec8-f084-4509-97b8-b9dfc13f7c3e")
            };

            //level 1
            List<Hdpp_passport_form> pfListLvl_1 = Hdpp_passport_form.GetList(t => pfGuidListLvl_0.Contains(t.hdpp_passport_form_id));

            List<Guid> pfGuidListLvl_1 = new List<Guid>();
            foreach (var target in pfListLvl_1)
                pfGuidListLvl_1.Add(target.id);

            //level 2
            List<Hdpp_passport_form> pfListLvl_2 = Hdpp_passport_form.GetList(t => pfGuidListLvl_1.Contains(t.hdpp_passport_form_id));

            string data = JsonConvert.SerializeObject(pfListLvl_1);

            if (!string.IsNullOrEmpty(levelNum) && int.TryParse(levelNum, out int lvl))
            {

                if(lvl == 1) {  }
                if(lvl == 2) { data = JsonConvert.SerializeObject(pfListLvl_2); }

            }

            if(Guid.TryParse(dependency, out Guid depValue))
            {

                //todo: reflexion dependency field
                List<Hdpp_passport_form> depPfList = Hdpp_passport_form.GetList(t => t.hdpp_passport_form_id == depValue);

                data = JsonConvert.SerializeObject(depPfList);

            }


            JsonResult response = new JsonResult(data);

            return response;

        }

    }

}
