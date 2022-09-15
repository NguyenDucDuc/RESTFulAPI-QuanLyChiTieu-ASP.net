using QLCT.Common.DAL;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLCT.DAL
{
    public class UserRep : GenericRep<QuanLyChiTieuContext, User>
    {
        public override User Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }


        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m);
            return m.Id;
        }
        public bool CreateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Users.Add(user);
                        context.SaveChanges();
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        return false;
                    }
                }
            }
            return false;
        }
        public List<User> GetAll()
        {
            var context = new QuanLyChiTieuContext();
            return context.Users.Select(p => p).ToList();
        }
        public User GetUserByUsername(string username)
        {
            var context = new QuanLyChiTieuContext();
            User user = context.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }

        public User GetById(int id)
        {
            var context = new QuanLyChiTieuContext();
            User user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public User Update(int userId, User newUser)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var oldUser = context.Users.FirstOrDefault(ou => ou.Id == userId);
                        if (oldUser != null)
                        {
                            oldUser.Username = newUser.Username;
                            oldUser.Password = newUser.Password;
                            context.SaveChanges();
                            tran.Commit();
                            return oldUser;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
