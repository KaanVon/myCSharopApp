using BS.Entity.Sys;
using BS.Infrastructure.Common;
using BS.Infrastructure.Utils;
using BSTemplate.Infrastructure.Core;
using BSTemplate.Infrastructure.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSTemplate.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ResponseResult Login(LoginModel model)
        {
            return CreateResponseResult(res =>
            {
                User user = _dbContext.User.FirstOrDefault(x => x.Name == model.Name);
                if(user == null)
                {
                    res.ErrorCode = 31;
                    res.ErrorMsg = "用户不存在或密码错误";
                    return;
                }
                //string salt = Secure.CreateSalt();
                string pwd = Secure.MixEncrypt(model.Password, user.Salt);
                if (!user.Password.Equals(pwd))
                {
                    res.ErrorCode = 32;
                    res.ErrorMsg = "密码错误";
                    return ;
                }
                string token  = Secure.EncryptStringByDES(user.OID + ":" + user.Password);
                res.Data = new
                {
                    oid = user.OID,
                    name = user.Name,
                    avatar = user.Avatar,
                    token
                };
            });
        }
    }
}