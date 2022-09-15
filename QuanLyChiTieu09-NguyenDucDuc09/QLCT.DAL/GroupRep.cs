using QLCT.Common.DAL;
using QLCT.Common.Req;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLCT.DAL
{
    public class GroupRep : GenericRep<QuanLyChiTieuContext, Group>
    {
        public bool Create(Group group)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Groups.Add(group);
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

        public bool Update(String groupName, int groupId, int userId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = context.Groups.FirstOrDefault(g => g.Id == groupId);
                        group.Groupname = groupName;
                        group.UserId = userId;
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

        public Group GetById(int groupId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                Group group = context.Groups.FirstOrDefault(g => g.Id == groupId);
                if (group != null)
                    return group;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public List<Group> GetAll(int userId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                List<Group> groups = context.Groups.Where(g => g.UserId == userId).ToList();
                return groups;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public bool Delete(int groupId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = context.Groups.FirstOrDefault(g => g.Id == groupId);
                        context.Groups.Remove(group);
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



    }
}
