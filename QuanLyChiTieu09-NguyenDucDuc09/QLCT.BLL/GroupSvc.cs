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
    public class GroupSvc : GenericSvc<GroupRep, Group>
    {
        private GroupRep groupRep;
        private UserRep userRep;
        public GroupSvc()
        {
            groupRep = new GroupRep();
            userRep = new UserRep();
        }

        public GroupCreateRsp Create(GroupCreateReq groupCreateReq, int userId)
        {
            try
            {
                Group group = new Group();
                group.Groupname = groupCreateReq.Groupname;
                group.Id = groupCreateReq.Id;
                group.UserId = userId;
                if (groupRep.Create(group))
                {
                    //tao moi group RSP de tra ve
                    GroupCreateRsp groupCreateRsp = new GroupCreateRsp();
                    groupCreateRsp.Groupname = groupCreateReq.Groupname;
                    groupCreateRsp.Id = groupCreateReq.Id;
                    groupCreateRsp.UserId = userId;
                    return groupCreateRsp;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public GroupUpdateRsp Update(String groupName, int groupId, int userId)
        {
            try
            {
                if (groupRep.Update(groupName, groupId, userId))
                {
                    Group group = groupRep.GetById(groupId);
                    if (group != null)
                    {
                        GroupUpdateRsp groupUpdateRsp = new GroupUpdateRsp();
                        groupUpdateRsp.Groupname = group.Groupname;
                        groupUpdateRsp.UserId = group.UserId;
                        groupUpdateRsp.Id = group.Id;
                        return groupUpdateRsp;
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public List<GroupRsp> GetAll(int userId)
        {
            try
            {
                List<Group> groups = groupRep.GetAll(userId);
                if (groups != null)
                {
                    List<GroupRsp> groupRsps = new List<GroupRsp>();
                    groups.ForEach(g =>
                    {
                        GroupRsp groupRsp = new GroupRsp();
                        groupRsp.Groupname = g.Groupname;
                        groupRsp.UserId = g.UserId;
                        groupRsp.Id = g.Id;

                        User user = userRep.GetById((int)g.UserId);

                        //do userReq co nhung thuoc tinh se reponse ve nen se duoc su dung lai
                        UserReq userRsp = new UserReq();
                        userRsp.Id = user.Id;
                        userRsp.Username = user.Username;
                        userRsp.Password = user.Password;
                        userRsp.Active = user.Active;
                        userRsp.Role = user.Role;

                        //set user cho groupReponse la userReponse
                        groupRsp.User = userRsp;
                        groupRsps.Add(groupRsp);
                    });

                    return groupRsps;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public DeleteRsp Delete(int groupId)
        {
            DeleteRsp deleteRsp;
            try
            {
                if (groupRep.Delete(groupId))
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

        public Group GetById(int groupId)
        {
            return groupRep.GetById(groupId);
        }


    }
}
