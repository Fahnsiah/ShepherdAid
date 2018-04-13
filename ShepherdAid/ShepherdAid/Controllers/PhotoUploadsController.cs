using ShepherdAid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ShepherdAid.Controllers
{
    public class PhotoUploadsController : Controller
    {
        private ShepherdAidDBEntities db = new ShepherdAidDBEntities();
        // GET: PhotoUploads
        public ActionResult file(int id)
        {
            Session["member_id"] = id;
            Member member = db.Members.Find(id);
            if (member != null)
            {
                string fullname = string.Format("{0} {1} {2}", member.FirstName, member.MiddleName, member.LastName);
                ViewBag.Name = fullname;
            }
            return View();
        }
        [HttpPost]
        public ActionResult file(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string fileExtension = Path.GetExtension(file.FileName);

                    int memberID = Convert.ToInt32(Session["member_id"]);
                    string memberFolder = string.Format("~/Content/{0}/{1}", StaticVariables.MemberDefaultFolder, memberID);
                    string directory = Server.MapPath(memberFolder);

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    string filename = string.Format("{0}{1}{2}{3}", DateTime.Now.DayOfYear,
                                                                    DateTime.Now.Hour,
                                                                    DateTime.Now.Second,
                                                                    fileExtension);

                    string filePath = Path.Combine(directory, filename);

                    file.SaveAs(filePath);
                    string imgUrl = Path.Combine(memberFolder, filename);
                    imgUrl = imgUrl.Replace(@"\", "/").Replace("~/Content/", "");
                    if (this.SaveFilePathToDatabase(imgUrl))
                    {
                        return RedirectToAction("Details", "Members", new { id = memberID });
                    }
                    else
                    {
                        ViewBag.Message = "Error: Failed to add file path to databasse.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

        public bool SaveFilePathToDatabase(string filePath)
        {
            try
            {
                //save to database
                int memberID = Convert.ToInt32(Session["member_id"]);
                var checkPhoto = db.MemberPhotos.Where(x=>x.MemberID == memberID);
                if (checkPhoto.Count() < 1)
                {
                    MemberPhoto memberPhoto = new MemberPhoto();
                    memberPhoto.MemberID = memberID;
                    memberPhoto.DocumentPath = filePath;
                    memberPhoto.RecordedBy = User.Identity.GetUserName();
                    memberPhoto.DateRecorded = DateTime.Now;
                    db.MemberPhotos.Add(memberPhoto);
                    db.SaveChanges();
                }
                else
                {
                    MemberPhoto memberPhoto = checkPhoto.FirstOrDefault();
                    memberPhoto.DocumentPath = filePath;
                    db.Entry(memberPhoto).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}