using BS.Entity.Sys;
using BS.Infrastructure.Common;
using BS.Service.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSTemplate.Infrastructure.Core
{
    public class SessionController : BaseController
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected User CurrentUser { get; private set; }
        // GET: Session
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string token = Request.Headers.Get("Token");
            ResponseResult response = new ResponseResult();
            UserService userService = new UserService();

            int errorCode; string errorMsg; User user;
            var b = userService.ValidateToken(token, out errorCode, out errorMsg, out user);
            if (!b)
            {
                response.ErrorCode = errorCode;
                response.ErrorMsg = "登录状态超时，请尝试重新登录";
                filterContext.Result = Content(response.ToString());
                return;
            }

            CurrentUser = user;

            if (!CurrentUser.IsAdmin)
            {
                object[] filter = filterContext.ActionDescriptor.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (filter.Any()) //有标记判断权限
                {
                    var s = filter.FirstOrDefault();
                    List<string> permissions = CurrentUser.GetPermission();
                    var ispath = Request.Path.ToLower();
#if DEBUG
                    ispath = "/api" + ispath;
#endif
                    if (!permissions.Contains(ispath))
                    {
                        response.ErrorCode = 501;
                        response.ErrorMsg = "您没有该操作的权限";
                        filterContext.Result = Content(response.ToString());
                        return;
                    }
                }
            }
        }
    }
}