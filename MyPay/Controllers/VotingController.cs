using ClosedXML.Excel;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class VotingController : BaseAdminSessionController
    {
        // GET: VotingCompetition
        [HttpGet]
        [Authorize]
        public ActionResult Index(string Id)
        {
            AddVotingCompetition model = new AddVotingCompetition();
            if (!String.IsNullOrEmpty(Id))
            {
                AddVotingCompetition outobject = new AddVotingCompetition();
                GetVotingCompetition inobject = new GetVotingCompetition();
                inobject.Id = Convert.ToInt64(Id);
                inobject.CheckDelete = 0;
                model = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
            }
            return View(model);
        }

        // Post: VotingCompetition
        [HttpPost]
        [Authorize]
        public ActionResult Index(AddVotingCompetition model, HttpPostedFileBase Image)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Message = "Please enter title";
            }
            else if (string.IsNullOrEmpty(model.Description))
            {
                ViewBag.Message = "Please enter description";
            }
            //else if (model.PricePerVote==0)
            //{
            //    ViewBag.Message = "Please enter price per vote";
            //}

            AddVotingCompetition outobject = new AddVotingCompetition();
            GetVotingCompetition inobject = new GetVotingCompetition();
            if (model.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddVotingCompetition res = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (model.EndTime < DateTime.UtcNow && model.EndTime <= model.PublishTime)
                        {
                            ViewBag.Message = "Please enter valid end time";
                            return View(model);
                        }
                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            res.Title = model.Title;
                            res.Description = model.Description;
                            res.PricePerVote = model.PricePerVote;
                            res.PublishTime = model.PublishTime;
                            res.EndTime = model.EndTime;
                            res.IsActive = model.IsActive;
                            res.Order = model.Order;
                            if (Image == null && (model.Image == "" || model.Image == null))
                            {
                                ViewBag.Message = "Please upload image";
                                return View(model);
                            }
                            if (string.IsNullOrEmpty(ViewBag.Message))
                            {
                                if (Image != null)
                                {
                                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                                    string filePath = Path.Combine(Server.MapPath("~/Images/VotingCompetition/") + fileName);
                                    Image.SaveAs(filePath);
                                    res.Image = fileName;
                                }

                                if (Session["AdminMemberId"] != null)
                                {
                                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    res.UpdatedByName = Session["AdminUserName"].ToString();
                                    bool status = RepCRUD<AddVotingCompetition, GetVotingCompetition>.Update(res, "votingcompetition");
                                    if (status)
                                    {
                                        ViewBag.SuccessMessage = "Successfully Updated Voting Competition Detail.";
                                        Common.AddLogs("Updated Voting Competition Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Not Updated.";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                if (model.EndTime < DateTime.UtcNow && model.EndTime <= model.PublishTime)
                {
                    ViewBag.Message = "Please enter valid end time";
                    return View(model);
                }
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    AddVotingCompetition res = new AddVotingCompetition();

                    res.Title = model.Title;
                    res.Description = model.Description;
                    res.PricePerVote = model.PricePerVote;
                    res.PublishTime = model.PublishTime;
                    res.EndTime = model.EndTime;
                    res.IsActive = model.IsActive;
                    res.Order = model.Order;

                    if (Image == null && (model.Image == "" || model.Image == null))
                    {
                        ViewBag.Message = "Please upload image";
                        return View(model);
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        if (Image != null)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                            string filePath = Path.Combine(Server.MapPath("~/Images/VotingCompetition/") + fileName);
                            Image.SaveAs(filePath);
                            res.Image = fileName;
                        }
                        if (Session["AdminMemberId"] != null)
                        {
                            res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.CreatedByName = Session["AdminUserName"].ToString();
                            Int64 Id = RepCRUD<AddVotingCompetition, GetVotingCompetition>.Insert(res, "votingcompetition");
                            if (Id > 0)
                            {
                                ViewBag.SuccessMessage = "Successfully Added Voting Competition.";
                                Common.AddLogs("Addded Voting Competition Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                            }
                            else
                            {
                                ViewBag.Message = "Not Added ! Try Again later.";
                            }
                        }
                    }
                }
            }
            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);

            return View(model);
        }

        // GET: VotingCompetitionList
        [HttpGet]
        [Authorize]
        public ActionResult VotingCompetitionList()
        {
            AddVotingCompetition model = new AddVotingCompetition();
            return View(model);
        }

        // Post: VotingCompetitionList
        [HttpPost]
        [Authorize]
        public ActionResult VotingCompetitionList(AddVotingCompetition model)
        {
            return View(model);
        }

        //VotingCompetitionDataTable
        [Authorize]
        public JsonResult GetVotingCompetitionList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Updated Date");
            columns.Add("Title");
            columns.Add("Publish Time");
            columns.Add("End Time");
            columns.Add("Price Per Vote");
            columns.Add("Status");
            columns.Add("Created By");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            string EventStatus = context.Request.Form["EventStatus"];
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddVotingCompetition> trans = new List<AddVotingCompetition>();
            GetVotingCompetition w = new GetVotingCompetition();
            w.CheckDelete = 0;
            if (EventStatus != "0")
            {
                if (EventStatus == "1")
                {
                    w.Running = "Running";
                }
                else if (EventStatus == "2")
                {
                    w.Scheduled = "Scheduled";
                }
                else if (EventStatus == "3")
                {
                    w.Closed = "Closed";
                }
            }
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddVotingCompetition
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         Title = row["Title"].ToString(),
                         PublishTimeDt = row["PublishTimeDt"].ToString(),
                         EndTimeDt = row["EndTimeDt"].ToString(),
                         PricePerVote = Convert.ToDecimal(row["PricePerVote"]),
                         TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                         TotalFreeVotes = Convert.ToInt32(row["TotalFreeVotes"]),
                         TotalVotes = Convert.ToInt32(row["TotalVotes"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         UpdatedDateDt = row["UpdatedDateDt"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         EventStatus = row["EventStatus"].ToString(),
                         Image = row["Image"].ToString(),
                         Order = Convert.ToInt32(row["Order"].ToString())
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddVotingCompetition>> objDataTableResponse = new DataTableResponse<List<AddVotingCompetition>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        //VotingCompetitionEnableDisable
        [HttpPost]
        [Authorize]
        public JsonResult VotingCompetitionBlockUnblock(AddVotingCompetition model)
        {
            AddVotingCompetition outobject = new AddVotingCompetition();
            GetVotingCompetition inobject = new GetVotingCompetition();
            inobject.Id = model.Id;
            AddVotingCompetition res = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddVotingCompetition, GetVotingCompetition>.Update(res, "votingcompetition");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update voting competition";
                    Common.AddLogs("Updated voting competition by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                }
                else
                {
                    ViewBag.Message = "Not Updated voting competition";
                    Common.AddLogs("Not Updated voting competition", true, (int)AddLog.LogType.Voting);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //VotingCompetitionDelete
        [HttpPost]
        [Authorize]
        public JsonResult DeleteVotingCompetition(string Id)
        {
            AddVotingCompetition outobject = new AddVotingCompetition();
            GetVotingCompetition inobject = new GetVotingCompetition();
            inobject.Id = Convert.ToInt64(Id);
            AddVotingCompetition res = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                res.IsDeleted = true;
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddVotingCompetition, GetVotingCompetition>.Update(res, "votingcompetition");
                if (IsUpdated)
                {
                    //Delete VotingCandidate of this competition
                    AddVotingCandidate outobject_candidate = new AddVotingCandidate();
                    GetVotingCandidate inobject_candidate = new GetVotingCandidate();
                    inobject_candidate.VotingCompetitionID = Convert.ToInt64(Id);
                    List<AddVotingCandidate> res_candidate = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecordList(Common.StoreProcedures.sp_VotingCandidate_Get, inobject_candidate, outobject_candidate);
                    if (res_candidate != null && res_candidate.Count != 0)
                    {
                        for (int i = 0; i < res_candidate.Count; i++)
                        {
                            res_candidate[i].IsDeleted = true;
                            res_candidate[i].UpdatedDate = DateTime.UtcNow;
                            bool IsUpdated_candidate = RepCRUD<AddVotingCandidate, GetVotingCandidate>.Update(res_candidate[i], "votingcandidate");
                            if (IsUpdated_candidate)
                            {
                                //Delete VotingList of this candidate andcompetition
                                AddVotingList outobject_list = new AddVotingList();
                                GetVotingList inobject_list = new GetVotingList();
                                inobject_list.VotingCompetitionID = Convert.ToInt64(Id);
                                inobject_list.VotingCandidateUniqueId = res_candidate[i].UniqueId;
                                List<AddVotingList> res_list = RepCRUD<GetVotingList, AddVotingList>.GetRecordList(Common.StoreProcedures.sp_VotingList_Get, inobject_list, outobject_list);
                                if (res_list != null && res_list.Count != 0)
                                {
                                    for (int k = 0; k < res_list.Count; k++)
                                    {
                                        res_list[k].IsDeleted = true;
                                        res_list[k].UpdatedDate = DateTime.UtcNow;
                                        bool IsUpdated_list = RepCRUD<AddVotingList, GetVotingList>.Update(res_list[k], "votinglist");
                                        if (IsUpdated_list)
                                        {
                                            //ViewBag.SuccessMessage = "Successfully Delete voting candidate";
                                            Common.AddLogs("Delete voting list of Candidate (UniqueId:" + res_candidate[i].UniqueId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                                        }
                                        else
                                        {
                                            //ViewBag.Message = "Not Delete voting candidate";
                                            Common.AddLogs("Not Delete voting list", true, (int)AddLog.LogType.Voting);

                                        }
                                    }
                                }
                                //ViewBag.SuccessMessage = "Successfully Delete voting candidate";
                                Common.AddLogs("Delete voting candidate from Competition (Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                            }
                            else
                            {
                                //ViewBag.Message = "Not Delete voting candidate";
                                Common.AddLogs("Not Delete voting candidate", true, (int)AddLog.LogType.Voting);

                            }
                        }
                    }

                    //Delete VotingPackage of this competition
                    AddVotingPackages outobject_packages = new AddVotingPackages();
                    GetVotingPackages inobject_packages = new GetVotingPackages();
                    inobject_packages.VotingCompetitionID = Convert.ToInt64(Id);
                    List<AddVotingPackages> res_packages = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecordList(Common.StoreProcedures.sp_VotingPackages_Get, inobject_packages, outobject_packages);
                    if (res_packages != null && res_packages.Count != 0)
                    {
                        for (int i = 0; i < res_packages.Count; i++)
                        {
                            res_packages[i].IsDeleted = true;
                            res_packages[i].UpdatedDate = DateTime.UtcNow;
                            bool IsUpdated_candidate = RepCRUD<AddVotingPackages, GetVotingPackages>.Update(res_packages[i], "votingpackages");
                            if (IsUpdated_candidate)
                            {
                                //ViewBag.SuccessMessage = "Successfully Delete voting candidate";
                                Common.AddLogs("Delete voting package from Competition (Id:" + res.Id + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                            }
                            else
                            {
                                //ViewBag.Message = "Not Delete voting candidate";
                                Common.AddLogs("Not Delete voting package", true, (int)AddLog.LogType.Voting);

                            }
                        }
                    }


                    ViewBag.SuccessMessage = "Successfully Delete voting competition";
                    Common.AddLogs("Delete voting competition by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                }
                else
                {
                    ViewBag.Message = "Not Delete voting competition";
                    Common.AddLogs("Not Delete voting competition", true, (int)AddLog.LogType.Voting);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        //VotingCompetitionDelete
        [HttpPost]
        [Authorize]
        public JsonResult SumUpdateVotingCompetition(string Id)
        {
            AddVotingCompetition outobject = new AddVotingCompetition();
            GetVotingCompetition inobject = new GetVotingCompetition();
            inobject.Id = Convert.ToInt64(Id);
            AddVotingCompetition res = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecord(Common.StoreProcedures.sp_VotingCompetition_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                VotingLists objRes = new VotingLists();
                objRes.VotingCompetitionId = res.Id;
                if (objRes.VotingSumUpdate())
                {
                    Models.Common.Common.AddLogs("Voting Competition Sum Updated successfully(VotingCompetitionId:" + res.Id + ")", false, Convert.ToInt32(AddLog.LogType.Voting));
                }
                else
                {
                    Models.Common.Common.AddLogs("Voting Competition Sum Updated Failed(VotingCompetitionId:" + res.Id + ")", false, Convert.ToInt32(AddLog.LogType.Voting));
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        // GET: AddVotingCandidates
        [HttpGet]
        [Authorize]
        public ActionResult AddVotingCandidates(string Id)
        {
            AddVotingCandidate model = new AddVotingCandidate();
            if (!String.IsNullOrEmpty(Id))
            {
                AddVotingCandidate outobject = new AddVotingCandidate();
                GetVotingCandidate inobject = new GetVotingCandidate();
                inobject.Id = Convert.ToInt64(Id);
                inobject.CheckDelete = 0;
                model = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);
            }

            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition(model.VotingCompetitionID.ToString());
            if (model.VotingCompetitionID != 0)
            {
                competitionlist.Find(c => c.Value == model.VotingCompetitionID.ToString()).Selected = true;
            }

            List<SelectListItem> genderlist = GetSelectList_Gender(model);

            ViewBag.Gender = genderlist;

            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View(model);
        }

        public static List<SelectListItem> GetSelectList_Gender(AddVotingCandidate model)
        {
            var genderlist = new List<SelectListItem>();
            //genderlist.Add(new SelectListItem
            //{
            //    Text = "Select Gender",
            //    Value = "0",
            //    Selected = true
            //});

            foreach (int value in Enum.GetValues(typeof(AddVotingCandidate.GenderTypes)))
            {
                string stringValue = Enum.GetName(typeof(AddVotingCandidate.GenderTypes), value).Replace("_", " ");
                if (value == model.Gender)
                {
                    genderlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString(), Selected = true });
                }
                else
                {
                    genderlist.Add(new SelectListItem { Text = stringValue, Value = value.ToString() });
                }
            }

            return genderlist;
        }

        // Post: AddVotingCandidates
        [HttpPost]
        [Authorize]
        public ActionResult AddVotingCandidates(AddVotingCandidate model, HttpPostedFileBase Image)
        {
            if (model.VotingCompetitionID == 0)
            {
                ViewBag.Message = "Please select  Voting Competition";

            }
            else if (model.ContentestNo == 0)
            {
                ViewBag.Message = "Please enter contestant number";

            }
            else if (string.IsNullOrEmpty(model.Name))
            {
                ViewBag.Message = "Please enter name";

            }
            else if (string.IsNullOrEmpty(model.Description))
            {
                ViewBag.Message = "Please enter description";

            }
            else if (string.IsNullOrEmpty(model.State))
            {
                ViewBag.Message = "Please enter state";

            }
            else if (string.IsNullOrEmpty(model.City))
            {
                ViewBag.Message = "Please enter city";

            }
            else if (model.ZipCode == 0)
            {
                ViewBag.Message = "Please enter zipcode";

            }
            else if (string.IsNullOrEmpty(model.EmailID))
            {
                ViewBag.Message = "Please enter email id";
            }
            else if (!Regex.Match(model.EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ViewBag.Message = "Please enter valid email";
            }
            else if (string.IsNullOrEmpty(model.ContactNo))
            {
                ViewBag.Message = "Please enter contact number";

            }

            AddVotingCandidate outobject = new AddVotingCandidate();
            GetVotingCandidate inobject = new GetVotingCandidate();
            if (string.IsNullOrEmpty(ViewBag.Message))
            {
                if (model.Id != 0)
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddVotingCandidate res = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (model.ContentestNo != res.ContentestNo)
                        {
                            AddVotingCandidate outobject_no = new AddVotingCandidate();
                            GetVotingCandidate inobject_no = new GetVotingCandidate();
                            inobject_no.ContentestNo = model.ContentestNo;
                            inobject_no.CheckDelete = 0;
                            inobject_no.VotingCompetitionID = model.VotingCompetitionID;
                            AddVotingCandidate res_no = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject_no, outobject_no);
                            if (res_no != null && res_no.Id != 0)
                            {
                                ViewBag.Message = "Contestant number already exist";
                            }
                        }
                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            res.Name = model.Name;
                            res.Description = model.Description;
                            res.Address = model.Address;
                            res.City = model.City;
                            res.ContactNo = model.ContactNo;
                            res.ContentestNo = model.ContentestNo;
                            res.CountryId = 216;
                            res.CountryName = "Nepal";
                            res.EmailID = model.EmailID;
                            res.State = model.State;
                            res.VotingCompetitionID = model.VotingCompetitionID;
                            res.IsActive = model.IsActive;
                            res.ZipCode = model.ZipCode;
                            res.Gender = model.Gender;
                            res.Age = model.Age;

                            if (Image == null && (model.Image == "" || model.Image == null))
                            {
                                ViewBag.Message = "Please upload image";

                            }
                            if (string.IsNullOrEmpty(ViewBag.Message))
                            {
                                if (Image != null)
                                {
                                    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                                    string filePath = Path.Combine(Server.MapPath("~/Images/VotingCandidate/") + fileName);
                                    Image.SaveAs(filePath);
                                    res.Image = fileName;
                                }

                                if (Session["AdminMemberId"] != null)
                                {
                                    res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                    res.UpdatedByName = Session["AdminUserName"].ToString();
                                    bool status = RepCRUD<AddVotingCandidate, GetVotingCandidate>.Update(res, "votingcandidate");
                                    if (status)
                                    {
                                        ViewBag.SuccessMessage = "Successfully Updated Voting Candidate Detail.";
                                        Common.AddLogs("Updated Voting Candidate Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Not Updated.";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    if (model.ContentestNo != 0)
                    {
                        AddVotingCandidate outobject_no = new AddVotingCandidate();
                        GetVotingCandidate inobject_no = new GetVotingCandidate();
                        inobject_no.ContentestNo = model.ContentestNo;
                        inobject_no.CheckDelete = 0;
                        inobject_no.VotingCompetitionID = model.VotingCompetitionID;
                        AddVotingCandidate res_no = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject_no, outobject_no);
                        if (res_no != null && res_no.Id != 0)
                        {
                            ViewBag.Message = "Contestant number already exist";

                        }
                    }
                    if (string.IsNullOrEmpty(ViewBag.Message))
                    {
                        AddVotingCandidate res = new AddVotingCandidate();
                        res.UniqueId = new CommonHelpers().GenerateUniqueId();
                        res.Name = model.Name;
                        res.Description = model.Description;
                        res.Address = model.Address;
                        res.City = model.City;
                        res.ContactNo = model.ContactNo;
                        res.ContentestNo = model.ContentestNo;
                        res.CountryId = 216;
                        res.CountryName = "Nepal";
                        res.EmailID = model.EmailID;
                        res.State = model.State;
                        res.VotingCompetitionID = model.VotingCompetitionID;
                        res.IsActive = model.IsActive;
                        res.ZipCode = model.ZipCode;
                        res.Gender = model.Gender;
                        res.Age = model.Age;

                        if (Image == null && (model.Image == "" || model.Image == null))
                        {
                            ViewBag.Message = "Please upload image";

                        }
                        if (string.IsNullOrEmpty(ViewBag.Message))
                        {
                            if (Image != null)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + Path.GetExtension(Image.FileName);
                                string filePath = Path.Combine(Server.MapPath("~/Images/VotingCandidate/") + fileName);
                                Image.SaveAs(filePath);
                                res.Image = fileName;
                            }
                            if (Session["AdminMemberId"] != null)
                            {
                                res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                                res.CreatedByName = Session["AdminUserName"].ToString();
                                Int64 Id = RepCRUD<AddVotingCandidate, GetVotingCandidate>.Insert(res, "votingcandidate");
                                if (Id > 0)
                                {
                                    ViewBag.SuccessMessage = "Successfully Added Voting Candidate.";
                                    Common.AddLogs("Addded Voting Candidate Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                                }
                                else
                                {
                                    ViewBag.Message = "Not Added ! Try Again later.";
                                }
                            }
                        }
                    }
                }
            }

            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition(model.VotingCompetitionID.ToString());
            if (model.VotingCompetitionID != 0)
            {
                competitionlist.Find(c => c.Value == model.VotingCompetitionID.ToString()).Selected = true;
            }
            List<SelectListItem> genderlist = GetSelectList_Gender(model);

            ViewBag.Gender = genderlist;
            ViewBag.VOTINGCOMPETITIONID = competitionlist;

            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);


            return View(model);
        }

        // GET: VotingCandidateList
        [HttpGet]
        [Authorize]
        public ActionResult VotingCandidateList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }

        //VotingCandidateDataTable
        [Authorize]
        public JsonResult GetVotingCandidateList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Competition Id");
            columns.Add("Name");
            columns.Add("Contestant No");
            columns.Add("State");
            columns.Add("City");
            columns.Add("Email");
            columns.Add("Contact No");
            columns.Add("Created By");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            string VotingCompetitionId = context.Request.Form["VotingCompetitionId"];
            string Name = context.Request.Form["Name"];
            string CheckRunning = context.Request.Form["CheckRunning"];
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddVotingCandidate> trans = new List<AddVotingCandidate>();
            GetVotingCandidate w = new GetVotingCandidate();
            w.VotingCompetitionID = VotingCompetitionId == "" ? 0 : Convert.ToInt64(VotingCompetitionId);
            w.Name = Name;
            w.CheckDelete = 0;
            w.CheckRunning = CheckRunning == null ? 0 : 1;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddVotingCandidate
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         Name = row["Name"].ToString(),
                         VotingCompetitionID = Convert.ToInt64(row["VotingCompetitionID"]),
                         ContentestNo = Convert.ToInt32(row["ContentestNo"].ToString()),
                         Rank = Convert.ToInt32(row["Rank"].ToString()),
                         TotalVotes = Convert.ToInt32(row["TotalVotes"].ToString()),
                         FreeVotes = Convert.ToInt32(row["FreeVotes"].ToString()),
                         PaidVotes = Convert.ToInt32(row["TotalVotes"].ToString()) - Convert.ToInt32(row["FreeVotes"].ToString()),
                         TotalAmount = Convert.ToDecimal(row["TotalAmount"].ToString()),
                         ContactNo = row["ContactNo"].ToString(),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         State = row["State"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         UpdatedByName = row["UpdatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         City = row["City"].ToString(),
                         EmailID = row["EmailID"].ToString(),
                         CompetitionName = row["CompetitionName"].ToString(),
                         Age = row["Age"].ToString(),
                         Gender = Convert.ToInt32(row["Gender"].ToString()),
                         GenderName = @Enum.GetName(typeof(AddVotingCandidate.GenderTypes), Convert.ToInt64(row["Gender"]))
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddVotingCandidate>> objDataTableResponse = new DataTableResponse<List<AddVotingCandidate>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };

            return Json(objDataTableResponse);

        }

        //VotingCandidateEnableDisable
        [HttpPost]
        [Authorize]
        public JsonResult VotingCandidateBlockUnblock(AddVotingCandidate model)
        {
            AddVotingCandidate outobject = new AddVotingCandidate();
            GetVotingCandidate inobject = new GetVotingCandidate();
            inobject.Id = model.Id;
            AddVotingCandidate res = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddVotingCandidate, GetVotingCandidate>.Update(res, "votingcandidate");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update voting candidate";
                    Common.AddLogs("Updated voting candidate by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                }
                else
                {
                    ViewBag.Message = "Not Updated voting candidate";
                    Common.AddLogs("Not Updated voting candidate", true, (int)AddLog.LogType.Voting);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //VotingCandidateDelete
        [HttpPost]
        [Authorize]
        public JsonResult DeleteVotingCandidate(string Id)
        {
            AddVotingCandidate outobject = new AddVotingCandidate();
            GetVotingCandidate inobject = new GetVotingCandidate();
            inobject.Id = Convert.ToInt64(Id);
            AddVotingCandidate res = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecord(Common.StoreProcedures.sp_VotingCandidate_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                res.IsDeleted = true;
                res.UpdatedDate = DateTime.UtcNow;
                bool IsUpdated = RepCRUD<AddVotingCandidate, GetVotingCandidate>.Update(res, "votingcandidate");
                if (IsUpdated)
                {
                    //Delete VotingList of this candidate andcompetition
                    AddVotingList outobject_list = new AddVotingList();
                    GetVotingList inobject_list = new GetVotingList();
                    inobject_list.VotingCompetitionID = res.VotingCompetitionID;
                    inobject_list.VotingCandidateUniqueId = res.UniqueId;
                    List<AddVotingList> res_list = RepCRUD<GetVotingList, AddVotingList>.GetRecordList(Common.StoreProcedures.sp_VotingList_Get, inobject_list, outobject_list);
                    if (res_list != null && res_list.Count != 0)
                    {
                        for (int i = 0; i < res_list.Count; i++)
                        {
                            res_list[i].IsDeleted = true;
                            res_list[i].UpdatedDate = DateTime.UtcNow;
                            bool IsUpdated_list = RepCRUD<AddVotingList, GetVotingList>.Update(res_list[i], "votinglist");
                            if (IsUpdated_list)
                            {
                                //ViewBag.SuccessMessage = "Successfully Delete voting candidate";
                                Common.AddLogs("Delete voting list of Candidate (UniqueId:" + res.UniqueId + ") by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                            }
                            else
                            {
                                //ViewBag.Message = "Not Delete voting candidate";
                                Common.AddLogs("Not Delete voting list", true, (int)AddLog.LogType.Voting);

                            }
                        }
                    }
                    ViewBag.SuccessMessage = "Successfully Delete voting candidate";
                    Common.AddLogs("Delete voting candidate by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);

                }
                else
                {
                    ViewBag.Message = "Not Delete voting candidate";
                    Common.AddLogs("Not Delete voting candidate", true, (int)AddLog.LogType.Voting);

                }
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }


        // GET: AddVotingPackage
        [HttpGet]
        [Authorize]
        public ActionResult AddVotingPackage(string Id)
        {
            AddVotingPackages model = new AddVotingPackages();
            if (!String.IsNullOrEmpty(Id))
            {
                AddVotingPackages outobject = new AddVotingPackages();
                GetVotingPackages inobject = new GetVotingPackages();
                inobject.Id = Convert.ToInt64(Id);
                inobject.CheckDelete = 0;
                model = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecord(Common.StoreProcedures.sp_VotingPackages_Get, inobject, outobject);
            }

            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition(model.VotingCompetitionID.ToString());
            if (model.VotingCompetitionID != 0)
            {
                competitionlist.Find(c => c.Value == model.VotingCompetitionID.ToString()).Selected = true;
            }

            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View(model);
        }

        // Post: AddVotingPackage
        [HttpPost]
        [Authorize]
        public ActionResult AddVotingPackage(AddVotingPackages model, HttpPostedFileBase Image)
        {
            if (model.VotingCompetitionID == 0)
            {
                ViewBag.Message = "Please select  Voting Competition";
            }
            else if (model.NoOfVotes == 0)
            {
                ViewBag.Message = "Please enter No of votes";
            }

            AddVotingPackages outobject = new AddVotingPackages();
            GetVotingPackages inobject = new GetVotingPackages();
            if (model.Id != 0)
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    inobject.Id = Convert.ToInt64(model.Id);
                    AddVotingPackages res = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecord(Common.StoreProcedures.sp_VotingPackages_Get, inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        res.NoOfVotes = model.NoOfVotes;
                        res.Amount = model.Amount;
                        res.VotingCompetitionID = model.VotingCompetitionID;
                        res.IsActive = model.IsActive;
                        res.Type = model.Type;
                        if (Session["AdminMemberId"] != null)
                        {
                            res.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                            res.UpdatedByName = Session["AdminUserName"].ToString();
                            bool status = RepCRUD<AddVotingPackages, GetVotingPackages>.Update(res, "votingpackages");
                            if (status)
                            {
                                ViewBag.SuccessMessage = "Successfully Updated Voting Packages Detail.";
                                Common.AddLogs("Updated Voting Packages Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                            }
                            else
                            {
                                ViewBag.Message = "Not Updated.";
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ViewBag.Message))
                {
                    AddVotingPackages res = new AddVotingPackages();
                    res.NoOfVotes = model.NoOfVotes;
                    res.Amount = model.Amount;
                    res.VotingCompetitionID = model.VotingCompetitionID;
                    res.IsActive = model.IsActive;

                    if (Session["AdminMemberId"] != null)
                    {
                        res.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        res.CreatedByName = Session["AdminUserName"].ToString();
                        Int64 Id = RepCRUD<AddVotingPackages, GetVotingPackages>.Insert(res, "votingpackages");
                        if (Id > 0)
                        {
                            ViewBag.SuccessMessage = "Successfully Added Voting Packages.";
                            Common.AddLogs("Addded Voting Packages Detail by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                        }
                        else
                        {
                            ViewBag.Message = "Not Added ! Try Again later.";
                        }
                    }
                }
            }

            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition(model.VotingCompetitionID.ToString());
            if (model.VotingCompetitionID != 0)
            {
                competitionlist.Find(c => c.Value == model.VotingCompetitionID.ToString()).Selected = true;
            }

            ViewBag.VOTINGCOMPETITIONID = competitionlist;

            inobject.Id = Convert.ToInt64(model.Id);
            model = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecord(Common.StoreProcedures.sp_VotingPackages_Get, inobject, outobject);


            return View(model);
        }

        // GET: VotingPackagesList
        [HttpGet]
        [Authorize]
        public ActionResult VotingPackagesList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }

        //VotingPackagesDataTable
        [Authorize]
        public JsonResult GetVotingPackagesList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Competition Id");
            columns.Add("No of Votes");
            columns.Add("Amount");
            columns.Add("Created By");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            string VotingCompetitionId = context.Request.Form["VotingCompetitionId"];
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];
            string CheckRunning = context.Request.Form["CheckRunning"];

            List<AddVotingPackages> trans = new List<AddVotingPackages>();
            GetVotingPackages w = new GetVotingPackages();
            w.VotingCompetitionID = VotingCompetitionId == "" ? 0 : Convert.ToInt64(VotingCompetitionId);
            w.CheckDelete = 0;
            w.CheckRunning = CheckRunning == null ? 0 : 1;

            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows

                     select new AddVotingPackages
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         NoOfVotes = Convert.ToInt32(row["NoOfVotes"]),
                         VotingCompetitionID = Convert.ToInt64(row["VotingCompetitionID"]),
                         Amount = Convert.ToDecimal(row["Amount"]),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         CompetitionName = row["CompetitionName"].ToString()
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddVotingPackages>> objDataTableResponse = new DataTableResponse<List<AddVotingPackages>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };
            return Json(objDataTableResponse);
        }

        //VotingPackagesBlockUnblock
        [HttpPost]
        [Authorize]
        public JsonResult VotingPackagesBlockUnblock(AddVotingPackages model)
        {
            AddVotingPackages outobject = new AddVotingPackages();
            GetVotingPackages inobject = new GetVotingPackages();
            inobject.Id = model.Id;
            AddVotingPackages res = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecord(Common.StoreProcedures.sp_VotingPackages_Get, inobject, outobject);
            if (res != null && res.Id != 0)
            {
                if (res.IsActive)
                {
                    res.IsActive = false;
                }
                else
                {
                    res.IsActive = true;
                }
                bool IsUpdated = RepCRUD<AddVotingPackages, GetVotingPackages>.Update(res, "votingpackages");
                if (IsUpdated)
                {
                    ViewBag.SuccessMessage = "Successfully Update voting packages";
                    Common.AddLogs("Updated voting packages by(AdminId:" + Session["AdminMemberId"] + ")", true, (int)AddLog.LogType.Voting);
                }
                else
                {
                    ViewBag.Message = "Not Updated voting packages";
                    Common.AddLogs("Not Updated voting packages", true, (int)AddLog.LogType.Voting);
                }
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        // GET: VotingList
        [HttpGet]
        [Authorize]
        public ActionResult VotingList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }

        //GetVotingList
        [Authorize]
        public JsonResult GetVotingList()
        {
            var context = HttpContext;
            List<string> columns = new List<string>();
            columns.Add("SNo");
            columns.Add("Created Date");
            columns.Add("Competition Id");
            columns.Add("Candidate Name");
            columns.Add("Package Id");
            columns.Add("No Of Votes");
            columns.Add("Member Id");
            columns.Add("Member Name");
            columns.Add("Member Contact");
            columns.Add("Created By");
            //This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables  
            Int32 ajaxDraw = Convert.ToInt32(context.Request.Form["draw"]);
            //OffsetValue  
            Int32 OffsetValue = Convert.ToInt32(context.Request.Form["start"]);
            //No of Records shown per page  
            Int32 PagingSize = Convert.ToInt32(context.Request.Form["length"]);
            //Getting value from the seatch TextBox  
            string searchby = context.Request.Form["search[value]"];
            //Index of the Column on which Sorting needs to perform  
            string sortColumn = context.Request.Form["order[0][column]"];
            //Finding the column name from the list based upon the column Index  
            string MemberContactNumber = context.Request.Form["MemberContactNumber"];
            string MemberName = context.Request.Form["MemberName"];
            string MemberID = context.Request.Form["MemberID"];
            string VotingCandidateName = context.Request.Form["VotingCandidateName"];
            string VotingCompetitionId = context.Request.Form["VotingCompetitionId"];
            string StartDate = context.Request.Form["StartDate"];
            string EndDate = context.Request.Form["EndDate"];
            string CheckRunning = context.Request.Form["CheckRunning"];
            sortColumn = columns[Convert.ToInt32(sortColumn)];
            //Sorting Direction  
            string sortDirection = context.Request.Form["order[0][dir]"];

            List<AddVotingList> trans = new List<AddVotingList>();

            GetVotingList w = new GetVotingList();
            w.MemberContactNumber = MemberContactNumber;
            w.MemberName = MemberName;
            w.VotingCandidateName = VotingCandidateName;
            if (MemberID != "" && MemberID != null)
            {
                w.MemberID = Convert.ToInt64(MemberID);
            }
            w.VotingCompetitionID = VotingCompetitionId == "" ? 0 : Convert.ToInt64(VotingCompetitionId);
            w.StartDate = StartDate;
            w.EndDate = EndDate;
            w.CheckDelete = 0;
            w.CheckRunning = CheckRunning == null ? 0 : 1;
            DataTable dt = w.GetData(sortColumn, sortDirection, OffsetValue, PagingSize, searchby);

            trans = (from DataRow row in dt.Rows
                     select new AddVotingList
                     {
                         Id = Convert.ToInt64(row["Id"]),
                         Sno = Convert.ToString(row["Sno"]),
                         NoofVotes = Convert.ToInt32(row["NoofVotes"]),
                         VotingCompetitionId = Convert.ToInt64(row["VotingCompetitionId"]),
                         VotingCandidateName = row["VotingCandidateName"].ToString(),
                         VotingPackageID = Convert.ToInt64(row["VotingPackageID"].ToString()),
                         MemberID = Convert.ToInt64(row["MemberID"]),
                         MemberContactNumber = row["MemberContactNumber"].ToString(),
                         MemberName = row["MemberName"].ToString(),
                         CreatedByName = row["CreatedByName"].ToString(),
                         IsActive = Convert.ToBoolean(row["IsActive"]),
                         CreatedDateDt = row["CreatedDateDt"].ToString(),
                         CompetitionName = row["CompetitionName"].ToString(),
                         FreeVotes = Convert.ToInt32(row["FreeVotes"]),
                         PaidVotes = Convert.ToInt32(row["PaidVotes"])
                     }).ToList();

            Int32 recordFiltered = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["FilterTotalCount"]) : 0;

            DataTableResponse<List<AddVotingList>> objDataTableResponse = new DataTableResponse<List<AddVotingList>>
            {
                draw = ajaxDraw,
                recordsFiltered = recordFiltered,
                recordsTotal = recordFiltered,
                data = trans
            };
            return Json(objDataTableResponse);
        }


        //ExportVotingList
        [HttpPost]
        [Authorize]
        public ActionResult VotingListExportExcel(Int64 MemberId, string MemberContactNumber, string MemberName, string VotingCandidateName, string FromDate, string ToDate, Int64 VotingCompetitionID)
        {
            var fileName = "Voting-" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".xlsx";

            //Save the file to server temp folder
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);

            GetVotingList w = new GetVotingList();

            w.MemberName = MemberName;
            w.MemberID = MemberId;
            w.MemberContactNumber = MemberContactNumber;
            w.VotingCandidateName = VotingCandidateName;
            w.VotingCompetitionID = VotingCompetitionID;
            w.StartDate = FromDate;
            w.EndDate = ToDate;
            w.CheckDelete = 0;
            DataTable dt = w.GetList();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = dt.DefaultView.ToTable(false, "Sno", "CreatedDateDt", "CompetitionName", "VotingCandidateName", "NoofVotes", "MemberID", "MemberName", "MemberContactNumber", "FreeVotes", "PaidVotes");
                dt.Columns["Sno"].ColumnName = "Sr No";
                dt.Columns["CreatedDateDt"].ColumnName = "Created Date";
                dt.Columns["CompetitionName"].ColumnName = "Competition Name";
                dt.Columns["VotingCandidateName"].ColumnName = "Candidate Name";
                dt.Columns["NoofVotes"].ColumnName = "No Of Votes";
                dt.Columns["MemberID"].ColumnName = "Member Id";
                dt.Columns["MemberName"].ColumnName = "Member Name";
                dt.Columns["MemberContactNumber"].ColumnName = "Member Contact";
                dt.Columns["FreeVotes"].ColumnName = "Free Votes";
                dt.Columns["PaidVotes"].ColumnName = "Paid Votes";
                dt.AcceptChanges();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "VotingReport(" + DateTime.Now.ToString("MMM") + ")");
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    ws.Columns().AdjustToContents();  // Adjust column width
                    ws.Rows().AdjustToContents();
                    wb.SaveAs(fullPath);
                }
            }
            var errorMessage = "you can return the errors here!";
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Download(string fileName)
        {
            //Get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/UserDocuments"), fileName);
            byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            System.IO.File.Delete(fullPath);
            return File(fileByteArray, "application/vnd.ms-excel", fileName);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExportExcelVotingListDump(string MemberId, string FromDate, string ToDate, string MemberContactNumber, string MemberName, string VotingCandidateName, string VotingCompetitionID)
        {
            var fileName = "";
            var errorMessage = "";
            if (string.IsNullOrEmpty(VotingCompetitionID))
            {
                errorMessage = "Please select Competition";
            }
            if (errorMessage == "")
            {
                fileName = "VotingListReport.xlsx";
                //Save the file to server temp folder
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Voting"), fileName);

                //string fromdate = DateTime.UtcNow.AddDays(-7).ToString("dd-MMM-yyyy");
                //string todate = DateTime.UtcNow.ToString("dd-MMM-yyyy");

                GetVotingList w = new GetVotingList();

                w.MemberName = MemberName;
                w.MemberID = Convert.ToInt64(MemberId);
                w.MemberContactNumber = MemberContactNumber;
                w.VotingCandidateName = VotingCandidateName;
                w.VotingCompetitionID = Convert.ToInt64(VotingCompetitionID);
                w.StartDate = FromDate;
                w.EndDate = ToDate;
                w.CheckDelete = 0;
                DataTable dt = w.GetDataDump();
                //DataTable dt = w.GetDataDump();
                if (dt != null && dt.Rows.Count > 0)
                {
                    AddExportData outobject_file = new AddExportData();
                    GetExportData inobject_file = new GetExportData();
                    inobject_file.Type = (int)AddExportData.ExportType.Voting;
                    AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);

                    if (res_file != null && res_file.Id != 0)
                    {
                        string oldfilePath = Path.Combine(Server.MapPath("~/ExportData/Voting"), res_file.FilePath);
                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        res_file.FilePath = fullPath;
                        res_file.UpdatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        bool status = RepCRUD<AddExportData, GetExportData>.Update(res_file, "exportdata");
                        if (status)
                        {

                            Common.AddLogs("Voting List Excel Updated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Voting), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Voting_Export);
                        }
                    }
                    else
                    {
                        AddExportData export = new AddExportData();
                        export.CreatedBy = Convert.ToInt64(Session["AdminMemberId"]);
                        export.CreatedByName = Session["AdminUserName"].ToString();
                        export.FilePath = fullPath;
                        export.Type = (int)AddExportData.ExportType.Voting;
                        export.IsActive = true;
                        export.IsApprovedByAdmin = true;
                        Int64 Id = RepCRUD<AddExportData, GetExportData>.Insert(export, "exportdata");
                        if (Id > 0)
                        {
                            Common.AddLogs("Voting List Excel Generated Successfully. File Path:" + fileName + ")", true, Convert.ToInt32(AddLog.LogType.Voting), Convert.ToInt64(Session["AdminMemberId"]), "", false, "web", "", (int)AddLog.LogActivityEnum.Voting_Export);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(dt, "VotingList");
                        ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                        ws.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                        ws.Columns().AdjustToContents();  // Adjust column width
                        ws.Rows().AdjustToContents();
                        wb.SaveAs(fullPath);
                    }

                }
                else
                {
                    errorMessage = "No data found";
                    fileName = "";
                }
            }
            //Return the Excel file name
            return Json(new { fileName = fileName, errorMessage = errorMessage });
        }
        [HttpPost]
        [Authorize]
        public ActionResult DownloadVotingListFileName()
        {
            var errorMessage = "";
            AddExportData outobject_file = new AddExportData();
            GetExportData inobject_file = new GetExportData();
            inobject_file.Type = (int)AddExportData.ExportType.Voting;

            AddExportData res_file = RepCRUD<GetExportData, AddExportData>.GetRecord(Common.StoreProcedures.sp_ExportData_Get, inobject_file, outobject_file);
            string fullPath = Path.Combine(Server.MapPath("~/ExportData/Voting"), res_file.FilePath);
            if (System.IO.File.Exists(fullPath))
            {
                return Json(new { fileName = res_file.FilePath, errorMessage = errorMessage });
            }
            else
            {
                errorMessage = "No file found";
                return Json(new { fileName = "", errorMessage = errorMessage });
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadVotingListFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Path.Combine(Server.MapPath("~/ExportData/Voting"), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                    //System.IO.File.Delete(fullPath);
                    return File(fileByteArray, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return RedirectToAction("/Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("/Index");
            }
        }

        // GET: VotingCandidateList
        [HttpGet]
        [Authorize]
        public ActionResult RunningVotingCandidateList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult RunningVotingList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }
        // GET: VotingPackagesList
        [HttpGet]
        [Authorize]
        public ActionResult RunningVotingPackagesList()
        {
            //Get VotingCompetitionList in DropDown
            List<SelectListItem> competitionlist = CommonHelpers.GetSelectList_VotingCompetition("");


            ViewBag.VOTINGCOMPETITIONID = competitionlist;
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult RunningVotingCompetitionList()
        {
            AddVotingCompetition model = new AddVotingCompetition();
            return View(model);
        }
    }
}