using QLCT.Common.BLL;
using QLCT.Common.Req;
using QLCT.Common.Rsp;
using QLCT.DAL;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.BLL
{
    public class IncomeOrSpendingSvc : GenericSvc<IncomeOrSpendingRep, IncomeOrSpending>
    {
        private IncomeOrSpendingRep incomeOrSpendingRep;
        private UserRep userRep;
        public IncomeOrSpendingSvc()
        {
            incomeOrSpendingRep = new IncomeOrSpendingRep();
            userRep = new UserRep();
        }
        public IncomeOrSpendingRsp Create(IncomeOrSpendingCreateReq incomeOrSpendingCreateReq, int userId)
        {

            //Tao moi chi tieu => gan chi tieu req vao chi tieu de call REP
            IncomeOrSpending incomeOrSpending = new IncomeOrSpending();
            incomeOrSpending.Id = incomeOrSpendingCreateReq.Id;
            incomeOrSpending.Money = incomeOrSpendingCreateReq.Money;
            incomeOrSpending.Purpose = incomeOrSpendingCreateReq.Purpose;
            incomeOrSpending.Time = incomeOrSpendingCreateReq.Time;
            incomeOrSpending.Type = incomeOrSpendingCreateReq.Type;
            incomeOrSpending.UserId = userId;
            incomeOrSpendingRep.Create(incomeOrSpending);

            //Tao moi chi tieu RSP de tra ve
            IncomeOrSpendingRsp incomeOrSpendingRsp = new IncomeOrSpendingRsp();
            incomeOrSpendingRsp.Id = incomeOrSpendingCreateReq.Id;
            incomeOrSpendingRsp.Money = incomeOrSpendingCreateReq.Money;
            incomeOrSpendingRsp.Purpose = incomeOrSpendingCreateReq.Purpose;
            incomeOrSpendingRsp.Time = incomeOrSpendingCreateReq.Time;
            incomeOrSpendingRsp.Type = incomeOrSpendingCreateReq.Type;
            incomeOrSpendingRsp.UserId = userId;

            return incomeOrSpendingRsp;
        }

        public IncomeOrSpendingRsp Update(IncomeOrSpendingUpdateReq incomeOrSpendingUpdateReq, int incomeOrSpendingId, int userId)
        {
            // tao moi thu chi de truyen vao tang REPO
            IncomeOrSpending incomeOrSpending = new IncomeOrSpending();
            incomeOrSpending.Id = incomeOrSpendingId;
            incomeOrSpending.Money = incomeOrSpendingUpdateReq.Money;
            incomeOrSpending.Purpose = incomeOrSpendingUpdateReq.Purpose;
            incomeOrSpending.Time = incomeOrSpendingUpdateReq.Time;
            incomeOrSpending.Type = incomeOrSpendingUpdateReq.Type;
            incomeOrSpending.UserId = userId;
            incomeOrSpendingRep.Update(incomeOrSpending);

            //tao moi thu chi response de tra ve
            IncomeOrSpendingRsp incomeOrSpendingRsp = new IncomeOrSpendingRsp();
            incomeOrSpendingRsp.Id = incomeOrSpendingId;
            incomeOrSpendingRsp.Money = incomeOrSpendingUpdateReq.Money;
            incomeOrSpendingRsp.Purpose = incomeOrSpendingUpdateReq.Purpose;
            incomeOrSpendingRsp.Time = incomeOrSpendingUpdateReq.Time;
            incomeOrSpendingRsp.Type = incomeOrSpendingUpdateReq.Type;
            incomeOrSpendingRsp.UserId = userId;
            return incomeOrSpendingRsp;
        }
        public IncomeOrSpendingDetailRsp Detail(int incomeOrSpendingId)
        {
            try
            {
                IncomeOrSpending incomeOrSpending = incomeOrSpendingRep.GetById(incomeOrSpendingId);
                IncomeOrSpendingDetailRsp incomeOrSpendingDetailRsp = new IncomeOrSpendingDetailRsp();
                User user = userRep.GetById((int)incomeOrSpending.UserId);
                UserReq userReq = new UserReq();


                if (incomeOrSpending != null && user != null)
                {
                    incomeOrSpendingDetailRsp.Id = incomeOrSpending.Id;
                    incomeOrSpendingDetailRsp.Money = incomeOrSpending.Money;
                    incomeOrSpendingDetailRsp.Type = incomeOrSpending.Type;
                    incomeOrSpendingDetailRsp.Time = incomeOrSpending.Time;
                    incomeOrSpendingDetailRsp.Purpose = incomeOrSpending.Purpose;
                    incomeOrSpendingDetailRsp.UserId = incomeOrSpending.UserId;

                    userReq.Id = user.Id;
                    userReq.Username = user.Username;
                    userReq.Password = user.Password;
                    userReq.Active = user.Active;
                    userReq.Role = user.Role;

                    incomeOrSpendingDetailRsp.User = userReq;


                    return incomeOrSpendingDetailRsp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public List<IncomeOrSpendingRsp> GetAllIncome(int userId)
        {
            try
            {
                List<IncomeOrSpending> incomes = incomeOrSpendingRep.GetAllIncome(userId);
                if (incomes != null)
                {
                    List<IncomeOrSpendingRsp> incomeRsps = new List<IncomeOrSpendingRsp>();
                    incomes.ForEach(i =>
                    {
                        IncomeOrSpendingRsp incomeRsp = new IncomeOrSpendingRsp();
                        incomeRsp.Id = i.Id;
                        incomeRsp.Money = i.Money;
                        incomeRsp.Type = i.Type;
                        incomeRsp.Purpose = i.Purpose;
                        incomeRsp.Time = i.Time;
                        incomeRsp.UserId = i.UserId;
                        incomeRsps.Add(incomeRsp);
                    });

                    return incomeRsps;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public List<IncomeOrSpendingRsp> GetAllSpending(int userId)
        {
            try
            {
                List<IncomeOrSpending> spendings = incomeOrSpendingRep.GetAllSpending(userId);
                if (spendings != null)
                {
                    List<IncomeOrSpendingRsp> spendingRsps = new List<IncomeOrSpendingRsp>();
                    spendings.ForEach(i =>
                    {
                        IncomeOrSpendingRsp spendingRsp = new IncomeOrSpendingRsp();
                        spendingRsp.Id = i.Id;
                        spendingRsp.Money = i.Money;
                        spendingRsp.Type = i.Type;
                        spendingRsp.Purpose = i.Purpose;
                        spendingRsp.Time = i.Time;
                        spendingRsp.UserId = i.UserId;
                        spendingRsps.Add(spendingRsp);
                    });

                    return spendingRsps;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public DeleteRsp Delete(int incomeOrSpendingId)
        {
            DeleteRsp deleteRsp = new DeleteRsp();
            try
            {
                if (incomeOrSpendingRep.Delete(incomeOrSpendingId))
                {
                    deleteRsp.StatusCode = 200;
                    deleteRsp.Message = "Success";
                    return deleteRsp;
                }
            }
            catch (Exception ex)
            {
                deleteRsp.StatusCode = 404;
                deleteRsp.Message = "Error";
                return deleteRsp;
            }
            deleteRsp.StatusCode = 404;
            deleteRsp.Message = "Error";
            return deleteRsp;
        }

        public IncomeOrSpending GetById(int incomeOrSpendingId)
        {
            return incomeOrSpendingRep.GetById(incomeOrSpendingId);
        }

        public TotalStatsRsp TotalStats(int userId)
        {
            return incomeOrSpendingRep.TotalStats(userId);
        }


    }
}
