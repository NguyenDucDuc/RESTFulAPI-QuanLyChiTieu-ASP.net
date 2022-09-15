using QLCT.Common.DAL;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLCT.DAL
{
    public class BelongToRep : GenericRep<QuanLyChiTieuContext, BelongTo>
    {
        public bool Create(BelongTo belongTo)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.BelongTos.Add(belongTo);
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

        public bool Update(int belongToId, BelongTo newBelongTo)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        BelongTo belongTo = context.BelongTos.FirstOrDefault(g => g.Id == belongToId);
                        belongTo.UserId = newBelongTo.UserId;
                        belongTo.GroupId = newBelongTo.GroupId;
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

        public BelongTo GetByUserIdAndGroupId(int userId, int groupId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                BelongTo belongto = context.BelongTos.FirstOrDefault(g => g.UserId == userId && g.GroupId == groupId);
                if (belongto != null)
                    return belongto;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
