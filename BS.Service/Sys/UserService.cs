using BS.Data;
using BS.Entity.Sys;
using BS.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Service.Sys
{
    public class UserService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        public bool ValidateLogin(string name, string password, out int errorCode, out string errorMsg, out User loginUser, out string token)
        {
            errorCode = 0;
            errorMsg = "";
            loginUser = null;
            token = "";

            var _dbContext = new ModelEntity();

            var user = _dbContext.User.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                errorCode = 31;
                errorMsg = "用户不存在";
                return false;
            }

            if (!user.Password.Equals(Secure.MixEncrypt(password, user.Salt)))
            {
                errorCode = 32;
                errorMsg = "密码错误";
                return false;
            }

            loginUser = user;
            token = Secure.EncryptStringByDES(user.OID + ":" + user.Password);

            return true;
        }

        /// <summary>
        /// 验证token的有效性
        /// </summary>
        public bool ValidateToken(string token, out int errorCode, out string errorMsg, out User currentUser)
        {
            currentUser = null;
            errorCode = 0;
            errorMsg = "";

            if (string.IsNullOrWhiteSpace(token))
            {
                errorCode = 40;
                errorMsg = "token不可为空";
                return false;
            }

            string desc = string.Empty;
            try
            {
                desc = Secure.DecryptStringByDES(token);
            }
            catch
            {
                errorCode = 41;
                errorMsg = "作废的token";
                return false;
            }

            string[] sr = desc.Split(':');
            if (sr.Length != 2)
            {
                errorCode = 44;
                errorMsg = "过时的token";
                return false;
            }
            int oid = Convert.ToInt32(sr[0]);
            string pwd = sr[1];

            var _dbContext = new ModelEntity();

            var user = _dbContext.User.Find(oid);
            //var user = _dbContext.GetEntity<User>(oid);
            if (user == null)
            {
                errorCode = 42;
                errorMsg = "无效的token";
                return false;
            }

            if (!user.Password.Equals(pwd))
            {
                errorCode = 43;
                errorMsg = "错误的token";
                return false;
            }

            currentUser = user;

            return true;
        }
    }

    public static class UserServiceExtension
    {
        public static List<int> GetMenuPermission(this User user)
        {
            var _dbContext = new ModelEntity();
            if (!user.IsAdmin)
            {
                var roleoids = _dbContext.UserRole.Where(x => x.User.OID == user.OID).Select(x => x.Role.OID).ToList();
                var menuoids = _dbContext.Role.Where(x => roleoids.Contains(x.OID)).SelectMany(x => x.RoleMenus.Where(z => !z.GcRecord).Select(z => z.Menu.OID)).ToList();
                return _dbContext.Menu.Where(x => menuoids.Contains(x.OID)).Select(x => x.OID).ToList();
            }
            else
            {
                return _dbContext.Menu.Select(x => x.OID).ToList();
            }
        }
        public static List<string> GetPermission(this User user)
        {
            var _dbContext = new ModelEntity();
            if (!user.IsAdmin)
            {
                var roleoids = _dbContext.UserRole.Where(x => x.User.OID == user.OID).Select(x => x.Role.OID).ToList();
                var permissionoids = _dbContext.Role.Where(x => roleoids.Contains(x.OID)).SelectMany(x => x.RolePermissions.Where(z => !z.GcRecord).Select(z => z.Permission.OID)).ToList();
                return _dbContext.Permission.Where(x => permissionoids.Contains(x.OID)).Select(x => "/api/" + x.Controller.ToLower() + "/" + x.Action.ToLower()).ToList();
            }
            else
            {
                return _dbContext.Permission.Select(x => "/api/" + x.Controller.ToLower() + "/" + x.Action.ToLower()).ToList();
            }
        }
    }
}
