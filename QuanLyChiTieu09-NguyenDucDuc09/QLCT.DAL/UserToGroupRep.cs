using QLCT.Common.DAL;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLCT.DAL
{
    public class UserToGroupRep : GenericRep<QuanLyChiTieuContext, UserToGroup>
    {
        public bool Create(UserToGroup userToGroup)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.UserToGroups.Add(userToGroup);
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

        public List<UserToGroup> GetAll(int userId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                List<UserToGroup> userToGroups = context.UserToGroups.Where(u => u.UserId == userId).ToList();
                if (userToGroups != null)
                    return userToGroups;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public UserToGroup GetById(int userToGroupId)
        {
            var context = new QuanLyChiTieuContext();
            UserToGroup userToGroup = context.UserToGroups.FirstOrDefault(u => u.Id == userToGroupId);
            if (userToGroup != null)
            {
                return userToGroup;
            }

            return null;
        }

        public bool Update(int userToGroupId, decimal money)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        UserToGroup userToGroup = context.UserToGroups.FirstOrDefault(u => u.Id == userToGroupId);
                        userToGroup.Money = money;
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

        public bool Delete(int userToGroupId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        UserToGroup userToGroup = context.UserToGroups.FirstOrDefault(u => u.Id == userToGroupId);
                        context.Remove(userToGroup);
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

        public decimal HaveToPay(int groupId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                decimal totalGroupMoney = (decimal)context.UserToGroups.Where(u => u.GroupId == groupId).Select(u => u.Money).Sum();
                int countUser = context.UserToGroups.Where(u => u.GroupId == groupId).Select(u => u.UserId).Distinct().Count();

                return totalGroupMoney/ (countUser+1);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }
    }
}
