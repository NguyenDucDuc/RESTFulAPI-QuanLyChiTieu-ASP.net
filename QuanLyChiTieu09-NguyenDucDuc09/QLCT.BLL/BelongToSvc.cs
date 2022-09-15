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
    public class BelongToSvc : GenericSvc<BelongToRep, BelongTo>
    {
        private BelongToRep belongToRep;
        private UserRep userRep;
        private GroupRep groupRep;
        public BelongToSvc()
        {
            belongToRep = new BelongToRep();
            userRep = new UserRep();
            groupRep = new GroupRep();
        }

        public BelongTo Create(BelongToReq belongToReq)
        {
            try
            {
                if (belongToReq != null)
                {
                    BelongTo belongTo = new BelongTo();
                    belongTo.Id = belongToReq.Id;
                    belongTo.UserId = belongToReq.UserId;
                    belongTo.GroupId = belongToReq.GroupId;
                    belongToRep.Create(belongTo);
                    return belongTo;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public BelongToRsp Update(int belongToId, BelongToUpdateReq belongToUpdateReq)
        {
            try
            {
                if (belongToUpdateReq != null)
                {
                    BelongTo belongTo = new BelongTo();

                    belongTo.UserId = belongToUpdateReq.UserId;
                    belongTo.GroupId = belongToUpdateReq.GroupId;
                    if (belongToRep.Update(belongToId, belongTo))
                    {
                        // neu update ok thi tao moi mot belongto tra ve
                        BelongToRsp belongToRsp = new BelongToRsp();
                        belongToRsp.Id = belongToId;
                        belongToRsp.UserId = belongToUpdateReq.UserId;
                        belongToRsp.GroupId = belongToUpdateReq.GroupId;

                        return belongToRsp;
                    }


                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public BelongTo GetByUserIdAndGroupId(int userId, int groupId)
        {
            return belongToRep.GetByUserIdAndGroupId(userId, groupId);
        }
    }
}
