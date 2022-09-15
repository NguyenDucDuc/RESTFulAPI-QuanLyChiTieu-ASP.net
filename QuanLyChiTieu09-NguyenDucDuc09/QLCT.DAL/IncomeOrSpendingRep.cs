using QLCT.Common.DAL;
using QLCT.Common.Rsp;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLCT.DAL
{
    public class IncomeOrSpendingRep : GenericRep<QuanLyChiTieuContext, IncomeOrSpending>
    {

        public IncomeOrSpending Create(IncomeOrSpending incomeOrSpending)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.IncomeOrSpendings.Add(incomeOrSpending);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return incomeOrSpending;
        }

        public bool Update(IncomeOrSpending incomeOrSpending)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        IncomeOrSpending incomeOrSpending2 = context.IncomeOrSpendings.FirstOrDefault(i => i.Id == incomeOrSpending.Id);
                        incomeOrSpending2.Money = incomeOrSpending.Money;
                        incomeOrSpending2.Purpose = incomeOrSpending.Purpose;
                        incomeOrSpending2.Time = incomeOrSpending.Time;
                        incomeOrSpending2.Type = incomeOrSpending.Type;
                        incomeOrSpending2.UserId = incomeOrSpending.UserId;
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

        public IncomeOrSpending GetById(int id)
        {
            var context = new QuanLyChiTieuContext();
            IncomeOrSpending incomeOrSpending = context.IncomeOrSpendings.FirstOrDefault(i => i.Id == id);
            if (incomeOrSpending != null)
            {
                return incomeOrSpending;
            }
            return null;
        }

        public List<IncomeOrSpending> GetAllIncome(int userId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                List<IncomeOrSpending> incomes = context.IncomeOrSpendings.Where(i => i.UserId == userId && i.Type == "INCOME").ToList();
                return incomes;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public List<IncomeOrSpending> GetAllSpending(int userId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                List<IncomeOrSpending> spendings = context.IncomeOrSpendings.Where(i => i.UserId == userId && i.Type == "SPENDING").ToList();
                return spendings;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public bool Delete(int incomeOrSpendingId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyChiTieuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        IncomeOrSpending incomeOrSpending = context.IncomeOrSpendings.FirstOrDefault(i => i.Id == incomeOrSpendingId);
                        context.IncomeOrSpendings.Remove(incomeOrSpending);
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

        public TotalStatsRsp TotalStats(int userId)
        {
            try
            {
                var context = new QuanLyChiTieuContext();
                decimal totalIncome = (decimal)context.IncomeOrSpendings.Where(i => i.UserId == userId && i.Type == "INCOME").Select(i => i.Money).Sum();
                decimal totalSpending = (decimal)context.IncomeOrSpendings.Where(i => i.UserId == userId && i.Type == "SPENDING").Select(i => i.Money).Sum();
                decimal totalSpendingGroup = (decimal)context.UserToGroups.Where(u => u.UserId == userId && u.Type == "SPENDING").Select(u => u.Money).Sum();
                TotalStatsRsp totalStatsRsp = new TotalStatsRsp();
                totalStatsRsp.Income = totalIncome;
                totalStatsRsp.Spending = totalSpending + totalSpendingGroup;
                return totalStatsRsp;

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
