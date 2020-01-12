using BS.Data;
using BS.Data.Infrastructure;
using BS.Entity.Sys;
using BS.Infrastructure.Common;
using System;
using System.Web.Mvc;

namespace BSTemplate.Infrastructure.Core
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 数据链接
        /// </summary>
        protected ModelEntity _dbContext;
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _dbContext = new ModelEntity();
            base.OnActionExecuting(filterContext);
        }

        protected ResponseResult CreateResponseResult(Action<ResponseResult> function)
        {
            ResponseResult response = new ResponseResult();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.ErrorCode = 40;
                    //response.ErrorMsg = ModelState.TranlateError();
                    return response;
                }

                function.Invoke(response);
            }
            catch (Exception e)
            {
                LogError(e);

                response.ErrorCode = -1;
                response.ErrorMsg = "系统太忙了";
                if (Request.Url.ToString().Contains("localhost"))
                {
                    string msg; string stack;
                    GetErrorMsg(e, out msg, out stack);
                    response.ErrorMsg = msg;
                }
            }

            return response;
        }

        private void GetErrorMsg(Exception e, out string msg, out string stack)
        {
            msg = e.Message;
            stack = e.StackTrace;

            var ex = e.InnerException;
            while (ex != null)
            {
                msg += ex.Message;
                stack += ex.StackTrace;
                ex = ex.InnerException;
            }
        }

        protected void LogError(Exception e)
        {
            try
            {
                string msg; string stack;
                GetErrorMsg(e, out msg, out stack);

                var db = DbFactory.GetNewDbContext();
                Error error = new Error();
                error.Action = Request.Path.ToLower();
                error.Message = msg;
                error.StackTrace = stack;
                //if (e.InnerException != null)
                //{
                //    error.InnerMessage = e.InnerException.Message;
                //    error.InnerStackTrace = e.InnerException.StackTrace;
                //}
                db.Entry(error).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                // nothing
            }
        }
    }
}