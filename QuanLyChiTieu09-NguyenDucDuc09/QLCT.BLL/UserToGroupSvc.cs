using QLCT.Common.Req;
using QLCT.Common.Rsp;
using QLCT.DAL;
using QLCT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLCT.BLL
{
    public class UserToGroupSvc
    {
        private UserToGroupRep userToGroupRep;
        public UserToGroupSvc()
        {
            userToGroupRep = new UserToGroupRep();
        }

        public UserToGroupCreateRsp Create(UserToGroupCreateReq userToGroupCreateReq, int userId)
        {
            try
            {
                //Tao 1 chi tieu cho nhom tu chi tieu REQ
                if (userToGroupCreateReq != null)
                {
                    UserToGroup userToGroup = new UserToGroup();
                    userToGroup.Id = userToGroupCreateReq.Id;
                    userToGroup.UserId = userId;
                    userToGroup.GroupId = userToGroupCreateReq.GroupId;
                    userToGroup.Money = userToGroupCreateReq.Money;
                    userToGroup.Purpose = userToGroupCreateReq.Purpose;
                    userToGroup.Time = userToGroupCreateReq.Time;
                    userToGroup.Type = userToGroupCreateReq.Type;
                    userToGroupRep.Create(userToGroup);

                    // tao 1 chi tieu cho nhom Rsp de tra ve du lieu

                    UserToGroupCreateRsp userToGroupCreateRsp = new UserToGroupCreateRsp();
                    userToGroupCreateRsp.Id = userToGroupCreateReq.Id;
                    userToGroupCreateRsp.UserId = userId;
                    userToGroupCreateRsp.GroupId = userToGroupCreateReq.GroupId;
                    userToGroupCreateRsp.Money = userToGroupCreateReq.Money;
                    userToGroupCreateRsp.Purpose = userToGroupCreateReq.Purpose;
                    userToGroupCreateRsp.Time = userToGroupCreateReq.Time;
                    userToGroupCreateRsp.Type = userToGroupCreateReq.Type;

                    return userToGroupCreateRsp;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public List<UserToGroupCreateRsp> GetAll(int userId)
        {
            try
            {
                List<UserToGroup> userToGroups = userToGroupRep.GetAll(userId);
                List<UserToGroupCreateRsp> userToGroupCreateRsps = new List<UserToGroupCreateRsp>();
                userToGroups.ForEach(u =>
                {
                    // voi moi user chuyen thanh model user rsp de tra ve 
                    UserToGroupCreateRsp userToGroupCreateRsp = new UserToGroupCreateRsp();
                    userToGroupCreateRsp.Id = u.Id;
                    userToGroupCreateRsp.UserId = userId;
                    userToGroupCreateRsp.GroupId = u.GroupId;
                    userToGroupCreateRsp.Money = u.Money;
                    userToGroupCreateRsp.Purpose = u.Purpose;
                    userToGroupCreateRsp.Time = u.Time;
                    userToGroupCreateRsp.Type = u.Type;

                    userToGroupCreateRsps.Add(userToGroupCreateRsp);
                });
                return userToGroupCreateRsps;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public UserToGroupCreateRsp Update(int userToGroupId, decimal money)
        {
            try
            {

                if (userToGroupRep.Update(userToGroupId, money))
                {
                    // tao moi thu chi group de tra ve
                    UserToGroupCreateRsp userToGroupCreateRsp = new UserToGroupCreateRsp();
                    UserToGroup userToGroup = userToGroupRep.GetById(userToGroupId);
                    userToGroupCreateRsp.Id = userToGroup.Id;
                    userToGroupCreateRsp.UserId = userToGroup.UserId;
                    userToGroupCreateRsp.GroupId = userToGroup.GroupId;
                    userToGroupCreateRsp.Money = userToGroup.Money;
                    userToGroupCreateRsp.Purpose = userToGroup.Purpose;
                    userToGroupCreateRsp.Time = userToGroup.Time;
                    userToGroupCreateRsp.Type = userToGroup.Type;

                    return userToGroupCreateRsp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public UserToGroup GetById(int userToGroupId)
        {
            return userToGroupRep.GetById(userToGroupId);
        }

        public DeleteRsp Delete(int userToGroupId)
        {
            DeleteRsp deleteRsp;
            try
            {
                if (userToGroupRep.Delete(userToGroupId))
                {

                    deleteRsp = new DeleteRsp();
                    deleteRsp.StatusCode = 200;
                    deleteRsp.Message = "Delete success";
                    return deleteRsp;
                }
            }
            catch (Exception ex)
            {
                deleteRsp = new DeleteRsp();
                deleteRsp.StatusCode = 500;
                deleteRsp.Message = "Error";
                return deleteRsp;
            }
            deleteRsp = new DeleteRsp();
            deleteRsp.StatusCode = 500;
            deleteRsp.Message = "Error";
            return deleteRsp;
        }

        public decimal HaveToPay(int groupId)
        {
            return userToGroupRep.HaveToPay(groupId);
        }
    }
}
