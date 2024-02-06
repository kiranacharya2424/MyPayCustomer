using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyPay.Repository
{
    public static class RepTicket
    {
        public static string AddTicketDetail(string authenticationToken, AddUserLoginWithPin resGetRecord, Int64 MemberId, string description, string title, int categoryid, string categoryname, string email, string name, string contactnumber, string filename, string transactionid, string platform, bool IsMobile, string devicecode, string IpAddress)
        {
            string result = "";
            try
            {
                if (description == "")
                {
                    result = "Please Enter Description";
                    return result;
                }
                else if (title == "")
                {
                    result = "Please Enter Subject";
                    return result;
                }
                else if (contactnumber == "")
                {
                    result = "Please Enter ContactNo.";
                    return result;
                }
                else if (categoryid == 0)
                {
                    result = "Please Select Department";
                    return result;
                }
                else if (MemberId == 0)
                {
                    result = "Please Enter MemberId";
                    return result;
                }
                AddTicket outobject = new AddTicket();
                outobject.UpdatedMessage = description;
                outobject.MainMessage = description;
                outobject.TransactionId = transactionid;
                outobject.TicketTitle = title;
                outobject.Status = (int)AddTicket.TicketStatus.Open;
                outobject.Priority = (int)AddTicket.Priorities.Low;
                outobject.IsActive = true;
                outobject.CreatedBy = MemberId;
                outobject.CreatedByName = name;
                outobject.CategoryId = categoryid;
                outobject.CategoryName = categoryname;
                outobject.Email = resGetRecord.Email;
                outobject.Name = resGetRecord.FirstName + " " + resGetRecord.LastName;
                outobject.ContactNumber = resGetRecord.ContactNumber;
                outobject.Platform = platform;
                outobject.IpAddress = IpAddress;
                outobject.TicketId = Models.Common.Common.RandomNumber(111111, 999999).ToString();
                AddTicketsReply outobject_reply = new AddTicketsReply();
                GetTicketsReply inobject_reply = new GetTicketsReply();
                if (!string.IsNullOrEmpty(filename))
                {
                    outobject.IsAttached = true;
                    outobject_reply.AttachFile = filename;
                }
                Int64 id = RepCRUD<AddTicket, GetTicket>.Insert(outobject, "tickets");
                if (id > 0)
                {
                    AddTicket outobject_t = new AddTicket();
                    GetTicket inobject_t = new GetTicket();
                    inobject_t.Id = id;
                    AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject_t, outobject_t);
                    if (res != null || res.Id > 0)
                    {
                        outobject_reply.IsActive = true;
                        outobject_reply.IsMainMessage = true;
                        outobject_reply.Name = name;
                        outobject_reply.TicketId = res.TicketId;
                        outobject_reply.Type = (int)AddTicketsReply.Types.Sender;
                        outobject_reply.Title = title;
                        outobject_reply.Message = description;
                        outobject_reply.IsAdmin = false;
                        //outobject_reply.CreatedBy = Common.GetCreatedById(authenticationToken);
                        //outobject_reply.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        outobject.CreatedBy = MemberId;
                        outobject.CreatedByName = name;
                        Int64 id_reply = RepCRUD<AddTicketsReply, GetTicketsReply>.Insert(outobject_reply, "ticketsreply");
                        if (id_reply > 0)
                        {
                            if (!string.IsNullOrEmpty(filename))
                            {
                                AddTicketImages(authenticationToken, filename, res.Id, id_reply, false, MemberId, name);
                            }
                            if (IsMobile)
                            {
                                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Ticket;
                                Common.SendNotification(authenticationToken, VendorAPIType, MemberId, "Ticket Raised", "New Ticket Raised Id:" + res.TicketId);
                                //SentNotifications.SendSingleNotification("Ticket Raised", "New Ticket Raised Id:" + tg.TicketId, devicecode, platform, "", MemberId.ToString());
                            }
                            Common.AddLogs("Raised new ticket, ID : " + res.TicketId, false, Convert.ToInt32(AddLog.LogType.Ticket), MemberId, contactnumber, IsMobile, platform, devicecode);
                            result = "success";
                        }
                        else
                        {
                            result = "Not Added";
                        }
                    }
                    else
                    {
                        result = Common.CommonMessage.Data_Not_Found;
                    }
                }
                else
                {
                    result = "Not Added";
                }
            }
            catch (Exception ex)
            {
                Models.Common.Common.AddLogs("Add Ticket detail Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.Ticket), 0, "", IsMobile);
                result = ex.Message;
            }
            return result;
        }

        public static bool AddTicketImages(string authenticationToken, string filename, Int64 Id, Int64 ReplyId, bool IsAdmin, Int64 MemberId, string UserName)
        {
            try
            {
                bool flag = false;
                AddTicket outobject_t = new AddTicket();
                GetTicket inobject_t = new GetTicket();
                inobject_t.Id = Id;
                AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject_t, outobject_t);
                if (res != null || res.Id > 0)
                {
                    string[] image;
                    image = filename.Split(',');
                    foreach (var item in image)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            //Ticket_Images ti = new Ticket_Images();
                            AddTicketImages outobject = new AddTicketImages();
                            outobject.TicketId = res.TicketId.ToString();
                            outobject.ReplyId = ReplyId;
                            outobject.IsAdmin = IsAdmin;
                            outobject.IsActive = true;
                            outobject.Image = item;
                            outobject.IsApprovedByAdmin = IsAdmin;
                            outobject.CreatedBy = MemberId;
                            outobject.CreatedByName = UserName;
                            //if (IsAdmin)
                            //{
                            //    outobject.CreatedBy = AdminMemberId;
                            //    outobject.CreatedByName = AdminUserName;
                            //}
                            //else
                            //{
                            //    //outobject.CreatedBy = Common.GetCreatedById(authenticationToken);
                            //    //outobject.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            //}

                            Int64 id_image = RepCRUD<AddTicketImages, GetTicketImages>.Insert(outobject, "ticketimages");
                            if (id_image > 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }
                return flag;
            }
            catch (Exception ex)
            {
                Common.AddLogs("Add Ticket Image Error:" + ex.Message, false, Convert.ToInt32(AddLog.LogType.Ticket), 0, "", true);
                //return  ex.Message;
            }
            return false;
        }

        public static string TicketReplyDetail(string authenticationToken, Int32 Take, Int32 Skip, string description, AddUserLoginWithPin resUser, Int64 MemberId, string TicketId, string name, string filename, string platform, bool IsMobile, string devicecode)
        {
            try
            {
                if (string.IsNullOrEmpty(description) && string.IsNullOrEmpty(filename))
                {
                    return "Please Enter Description Or Attach File.";
                }
                else if (string.IsNullOrEmpty(TicketId))
                {

                    return "Please Enter TicketId";
                }
                else if (MemberId == 0)
                {
                    return "Please Enter MemberId";
                }

                AddTicketsReply outobject = new AddTicketsReply();
                AddTicket outobject_t = new AddTicket();
                GetTicket inobject_t = new GetTicket();
                inobject_t.TicketId = TicketId;
                inobject_t.Take = Take;
                inobject_t.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
                AddTicket res = RepCRUD<GetTicket, AddTicket>.GetRecord(Common.StoreProcedures.sp_Tickets_Get, inobject_t, outobject_t);
                if (res != null || res.Id > 0)
                {
                    if (res.Status == (int)AddTicket.TicketStatus.Close)
                    {
                        return "Ticket is already closed.";
                    }
                    res.UpdatedMessage = description;
                    res.UpdatedDate = DateTime.UtcNow;
                    res.Status = (int)AddTicket.TicketStatus.Open;
                    res.IsSeen = false;
                    if (!string.IsNullOrEmpty(filename))
                    {
                        res.IsAttached = true;
                        outobject.AttachFile = filename;

                    }
                    bool status = RepCRUD<AddTicket, GetTicket>.Update(res, "tickets");
                    if (status)
                    {
                        outobject.IsActive = true;
                        outobject.Name = resUser.FirstName + " " + resUser.LastName;
                        outobject.TicketId = res.TicketId;
                        outobject.Type = (int)AddTicketsReply.Types.Sender;
                        outobject.Title = res.TicketTitle;
                        outobject.Message = description;
                        outobject.IsAdmin = false;
                        outobject.CreatedBy = resUser.MemberId;
                        outobject.CreatedByName = resUser.FirstName + " " + resUser.LastName;
                        Int64 id_reply = RepCRUD<AddTicketsReply, GetTicketsReply>.Insert(outobject, "ticketsreply");
                        if (id_reply > 0)
                        {
                            if (!string.IsNullOrEmpty(filename))
                            {
                                AddTicketImages(authenticationToken, filename, res.Id, id_reply, false, MemberId, name);
                            }
                            if (IsMobile)
                            {
                                //int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Ticket;
                                //Common.SendNotification(authenticationToken, VendorAPIType, MemberId, "Ticket Status", "Added new comment on ticket Id:" + res.TicketId);
                            }
                            Common.AddLogs("Added new comment on ticket, where ID : " + res.TicketId, false, Convert.ToInt32(AddLog.LogType.Ticket), MemberId, name, IsMobile, platform, devicecode);
                            return "success";
                        }
                        else
                        {
                            return "Not Added";
                        }
                    }
                    else
                    {
                        return "Not Updated";
                    }
                }
                else
                {
                    return Common.CommonMessage.Data_Not_Found;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}