using IRIS_API.configrationClass;
using IRIS_IDataMain.configrationClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IRIS_IDataMain.Controllers
{
    public class IRISIDController : ApiController
    {


        [System.Web.Http.HttpPost]

        public HttpResponseMessage PassImageIRIS_IDATA(List<string> eyes)
        {


            List<ResponseImage> ResImage = new List<ResponseImage>();

            try
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
                ResponseImage OBJ_IMG = new ResponseImage();




                if (eyes[0] != "" && eyes[1] != "")
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


                if (qulityLift > Convert.ToInt32(eyes[2]) && qulityRight > Convert.ToInt32(eyes[2]))
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


            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                Logger.WriteLog("ErrorMessage" + Environment.NewLine + ex.Message + Environment.NewLine + stackTrace + "Line" + line);

                return Request.CreateResponse(HttpStatusCode.OK, "Error occurred", Configuration.Formatters.JsonFormatter);

            }


            return Request.CreateResponse(HttpStatusCode.OK, ResImage.ToList(), Configuration.Formatters.JsonFormatter);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage VerificationIRISIDATA(List<string> IistEyesPostion)
        {
             

             string LiftEyeFromSytem = "";
             string RightEyeFromSytem = "";
             string LiftEyeFromDevice = "";
             string RightEyeFromDevice = "";

            var MatchsCorrect = false; 
            IrisMethods irisMethods = new IrisMethods();


            try
            {
                 
                if (IistEyesPostion == null || IistEyesPostion.Count == 0 || IistEyesPostion.Count == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "null eyes", Configuration.Formatters.JsonFormatter);
                }
                if (IistEyesPostion[0] != "" && IistEyesPostion[1] != "")
                {
                    LiftEyeFromSytem = IistEyesPostion[0];
                    RightEyeFromSytem = IistEyesPostion[1];

                    LiftEyeFromDevice = IistEyesPostion[2];
                    RightEyeFromDevice = IistEyesPostion[3];


                    MatchsCorrect = irisMethods.VerifyMatchesImageSIRIS(RightEyeFromSytem, RightEyeFromDevice);

                }
                else if (IistEyesPostion[0] == "" && IistEyesPostion[1] != "") //Left Is Missing
                {
                    RightEyeFromSytem = IistEyesPostion[1];
                    RightEyeFromDevice = IistEyesPostion[3];
                    MatchsCorrect = irisMethods.VerifyMatchesImageSIRIS(RightEyeFromSytem, RightEyeFromDevice);
                    //   EyesLift = The Eyes Is Missing
                }
                else if (IistEyesPostion[0] != "" && IistEyesPostion[1] == "") //Right Is Missing
                {
                    LiftEyeFromSytem = IistEyesPostion[0];
                    LiftEyeFromDevice = IistEyesPostion[2];
                    MatchsCorrect = irisMethods.VerifyMatchesImageSIRIS(LiftEyeFromSytem,  LiftEyeFromDevice);
                    //EyesRight = The Eyes Right Is Missing
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, MatchsCorrect, Configuration.Formatters.JsonFormatter);
                }



            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                Logger.WriteLog("ErrorMessage" + Environment.NewLine + ex.Message + Environment.NewLine + stackTrace + "Line" + line);
                return Request.CreateResponse(HttpStatusCode.OK, "Error occurred", Configuration.Formatters.JsonFormatter);
            }

            return Request.CreateResponse(HttpStatusCode.OK, MatchsCorrect, Configuration.Formatters.JsonFormatter);
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage PassOmran(int id)
        {
            try
            {
                var ddd = id;
            }
            catch(Exception ex)
            {

                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                Logger.WriteLog("ErrorMessage" + Environment.NewLine + ex.Message + Environment.NewLine + stackTrace + "Line" + line);
                return Request.CreateResponse(HttpStatusCode.OK, "Error occurred", Configuration.Formatters.JsonFormatter);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Omran", Configuration.Formatters.JsonFormatter);

        }
    }
}
