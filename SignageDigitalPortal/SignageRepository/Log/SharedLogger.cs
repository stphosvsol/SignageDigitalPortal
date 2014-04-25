using System;
using System.Web;
using System.Web.Script.Serialization;
using log4net;
using SignageRepository.Resources;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SignageRepository.Log
{
    public class SharedLogger
    {

        public static void LogError(Exception ex, params object[] arrVal)
        {
            try
            {
                var username = GetUser();
                var modelExcep = new ExceptionLog
                {
                    MsgException = ex.Message,
                    ExceptionLogUid = Guid.NewGuid(),
                    InnerException = GetInnerExceptions(ex),
                    ParamsValues = GetSerializedValues(arrVal),
                    StackTrace = ex.StackTrace,
                    Timestamp = DateTime.Now,
                    Username = username
                };

                SaveLogToDb(modelExcep);
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void SaveLogToDb(ExceptionLog modelExcep)
        {
            try
            {
                using (var dbConn = new SignageLogEntities())
                {
                    dbConn.ExceptionLog.Add(modelExcep);
                    dbConn.SaveChanges();
                }
            }
            catch (Exception)
            {
                SaveLogToFile(modelExcep);
            }
        }

        private static void SaveLogToFile(ExceptionLog exceptionLog)
        {
            try
            {
                LogManager.GetLogger("ERROR_LOGGER").Fatal(GetSerializedValue(exceptionLog));
            }
            catch (Exception)
            {
                return;
            }
        }

        private static string GetSerializedValue(ExceptionLog exceptionLog)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(exceptionLog);
            }
            catch (Exception)
            {
                return string.Format(ResSharedRep.ERROR_RECURSION, "GetSerializedValue");
            }
        }


        private static string GetSerializedValues(object[] arrVal)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(arrVal);
            }
            catch (Exception ex)
            {
                return string.Format(ResSharedRep.ERROR_RECURSION, "GetSerializedValues - " + ex.Message);
            }
        }

        private static string GetInnerExceptions(Exception exception)
        {
            try
            {
                if (exception.InnerException == null)
                {
                    return "|EOE|";
                }
                return string.Format("|{0}|{1}", exception.InnerException.Message, GetInnerExceptions(exception.InnerException));
            }
            catch (Exception ex)
            {
                return string.Format(ResSharedRep.ERROR_RECURSION, "GetInnerExceptions");
            }
        }

        private static string GetUser()
        {
            try
            {
                if (HttpContext.Current == null || HttpContext.Current.User == null)
                {
                    return string.Empty;
                }

                return HttpContext.Current.User.Identity.Name;

            }
            catch (Exception)
            {
                return ResSharedRep.ERROR_NOUSER_FROMCONTEXT;
            }
        }


        //Public Shared Sub LogDebug()

        //End Sub

    }
}

