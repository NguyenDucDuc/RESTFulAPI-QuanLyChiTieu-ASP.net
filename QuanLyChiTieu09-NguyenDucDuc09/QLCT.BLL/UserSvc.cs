using QLCT.Common.BLL;
using QLCT.Common.Req;
using QLCT.Common.Rsp;
using QLCT.DAL;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace QLCT.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        private UserRep userRep;
        public UserSvc()
        {
            userRep = new UserRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }
        public UserReq CreateUser(UserReq userReq)
        {
            //var res = new SingleRsp();

            User user = new User();

            user.Id = userReq.Id;
            user.Username = userReq.Username;
            user.Password = BC.HashPassword(userReq.Password);
            user.Active = userReq.Active;
            user.Role = userReq.Role;
            if (userRep.CreateUser(user))
            {
                userReq.Password = user.Password;
                return userReq;
            }
            return null;
        }
        public List<UserRsp> GetAll()
        {
            List<UserRsp> userRsps = new List<UserRsp>();
            userRep.GetAll().ForEach(u =>
            {
                UserRsp userRsp = new UserRsp();
                userRsp.Id = u.Id;
                userRsp.Username = u.Username;
                userRsp.Password = u.Password;
                userRsp.Active = u.Active;
                userRsp.Role = u.Role;
                userRsps.Add(userRsp);
            });

            return userRsps; ;
        }
        public UserRsp Login(UserLoginReq userLoginReq)
        {
            try
            {

                User user = userRep.GetUserByUsername(userLoginReq.Username);
                Random rd = new Random();

                if (user != null && BC.Verify(userLoginReq.Password, user.Password))
                {
                    UserRsp userRsp = new UserRsp();
                    userRsp.Id = user.Id;
                    userRsp.Password = user.Password;
                    userRsp.Username = user.Username;
                    userRsp.Role = user.Role;
                    userRsp.Active = user.Active;

                    userRsp.AccessToken = BC.HashPassword(rd.Next(1000, 10000).ToString()) + "`" + user.Id;

                    return userRsp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public UserRsp Update(int userId, UserUpdateReq userUpdateReq)
        {
            try
            {
                User newUser = new User();
                newUser.Username = userUpdateReq.Username;
                newUser.Password = BC.HashPassword(userUpdateReq.Password);
                User userUpdated = userRep.Update(userId, newUser);
                if (userUpdated != null)
                {
                    UserRsp userRsp = new UserRsp();
                    userRsp.Id = userId;
                    userRsp.Username = userUpdated.Username;
                    userRsp.Password = userUpdated.Password;
                    userRsp.Active = userUpdated.Active;
                    userRsp.Role = userUpdated.Role;

                    return userRsp;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public User GetById(int userId)
        {
            try
            {
                return userRep.GetById(userId);
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
