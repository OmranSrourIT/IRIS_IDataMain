using IRIS_API.configrationClass;
using IRIS_IDataMain.configrationClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IRIS_IDataMain.Controllers
{
    public class IRISIDController : ApiController
    {


        ApiResult oApi = new ApiResult();
        // GET api/values
        //https://localhost:44316/api/IRISID/PassImageIRIS_IDATA
        //https://192.168.88.232:44316/api/IRISID/PassImageIRIS_IDATA
        //http://localhost/IRIS_IDataMain/api/IRISID/PassImageIRIS_IDATA
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PassImageIRIS_IDATA(List<string> eyes)
        {
            if (eyes == null || eyes.Count == 0 || eyes.Count == 1)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "null eyes", Configuration.Formatters.JsonFormatter);
            }

            bool isGoodQulity;
            string liftEye = string.Empty;
            string rightEye = string.Empty;

            int qulityLift;
            int qulityRight;
            IrisMethods irisMethods = new IrisMethods();

            List<ResponseImage> ResImage = new List<ResponseImage>();
            ResponseImage OBJ_IMG = new ResponseImage();

           
           

            if (eyes[0] != "" && eyes[1] !="")
            {
                liftEye = eyes[0];
                rightEye = eyes[1];
                qulityLift = irisMethods.checkQulity(liftEye);
                qulityRight = irisMethods.checkQulity(rightEye);
            }
            else if (eyes[0] == "" && eyes[1] != "")
            {
                rightEye = eyes[1]; 
                qulityRight = irisMethods.checkQulity(rightEye);
                qulityLift = 110; //The Eyes Is Missing
            }
            else if (eyes[0] != "" && eyes[1] == "")
            {
                liftEye = eyes[0];
                qulityLift = irisMethods.checkQulity(liftEye);
                qulityRight = 110;  //The Eyes Is Missing
            }
            else
            {
                qulityLift = 0;
                qulityRight = 0;
            }

            OBJ_IMG.ImageLeft = eyes[0];
            OBJ_IMG.ImageRight = eyes[1];
            OBJ_IMG.ImageQuailtyLeft = qulityLift.ToString();
            OBJ_IMG.ImageQuailtyRight = qulityRight.ToString();
      
            ResImage.Add(OBJ_IMG);


            if (qulityLift > 75 && qulityRight > 75)
            { 
                isGoodQulity = true;
                OBJ_IMG.MessageQuailty = "Success,High quality";
                OBJ_IMG.ResultQuailty = true;
            }
            else
            { 
                isGoodQulity = false;
                OBJ_IMG.MessageQuailty = "Fialed,Low Quality";
                OBJ_IMG.ResultQuailty = false;
            }


            return Request.CreateResponse(HttpStatusCode.OK,  ResImage.ToList(), Configuration.Formatters.JsonFormatter);
        }

        ////https://192.168.88.232:44316/api/IRISID/PassImageIRIS_IDATA1
        [System.Web.Http.HttpGet]
        public HttpResponseMessage PassImageIRIS_IDATA1()
        {

            return Request.CreateResponse(HttpStatusCode.OK, "Omran", Configuration.Formatters.JsonFormatter);

        }
    }
}
