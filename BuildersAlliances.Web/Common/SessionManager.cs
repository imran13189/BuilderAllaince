    
using BuildersAlliances.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildersAlliances.Common
{
    public class SessionManager
    {
        public static LoggedInUserDetails LoggedInUser
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["LoggedInUser"] == null)
                {
                    HttpContext.Current.Session["LoggedInUser"] = new LoggedInUserDetails();
                }
                return (LoggedInUserDetails)HttpContext.Current.Session["LoggedInUser"];
            }
            set { HttpContext.Current.Session["LoggedInUser"] = value; }
        }
        public static LoggedInPhysicianStaffDetails PhysicianStaffDetails
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["LoggedInPhysicianStaffUser"] == null)
                {
                    HttpContext.Current.Session["LoggedInPhysicianStaffUser"] = new LoggedInPhysicianStaffDetails();
                }
                return (LoggedInPhysicianStaffDetails)HttpContext.Current.Session["LoggedInPhysicianStaffUser"];
            }
            set { HttpContext.Current.Session["LoggedInUser"] = value; }
        }

        public static LoggedInPharmacyStaffDetails PharmacyStaffDetails
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["LoggedInPharmacyStaffUser"] == null)
                {
                    HttpContext.Current.Session["LoggedInPharmacyStaffUser"] = new LoggedInPharmacyStaffDetails();
                }
                return (LoggedInPharmacyStaffDetails)HttpContext.Current.Session["LoggedInPharmacyStaffUser"];
            }
            set { HttpContext.Current.Session["LoggedInUser"] = value; }
        }

        public static PlanDetails PlanDetails
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                if (HttpContext.Current.Session["PlanDetails"] == null)
                {
                    HttpContext.Current.Session["PlanDetails"] = new PlanDetails();
                }
                return (PlanDetails)HttpContext.Current.Session["PlanDetails"];
            }
            set { HttpContext.Current.Session["PlanDetails"] = value; }
        }


        public static void FillPhysicianStaffSession(Guid PhysicianId,Guid? PhysicianAdminId,int? UserType,int UserId)
        {
            SessionManager.PhysicianStaffDetails.PhysicianId = PhysicianId;
            SessionManager.PhysicianStaffDetails.PhysicianAdminId = PhysicianAdminId;
            SessionManager.PhysicianStaffDetails.UserType = UserType;
            SessionManager.PhysicianStaffDetails.UserId = UserId;
      
        }

        public static void FillPharmacyStaffStaffSession(Guid PharmacyId, Guid? PharmacyAdminId, int? UserType, int UserId)
        {
            SessionManager.PharmacyStaffDetails.PharmacyStaffId = PharmacyId;
            SessionManager.PharmacyStaffDetails.PharmacyAdminId = PharmacyAdminId;
            SessionManager.PharmacyStaffDetails.UserType = UserType;
            SessionManager.PharmacyStaffDetails.UserId = UserId;
        }


        public static void FillSession(int userid, string email,string name,int?[] RoleId)
        {
            SessionManager.LoggedInUser.UserID = userid;
            SessionManager.LoggedInUser.Name = name;
            
            SessionManager.LoggedInUser.Email = email;
            
            
            SessionManager.LoggedInUser.RoleId = RoleId;


        }

        public static void FillPlanSession(bool IsPlanExpired,bool IsPlanSelected,int PlanId)
        {
            SessionManager.PlanDetails.IsPlanExpired = IsPlanExpired;
            SessionManager.PlanDetails.IsPlanSelected = IsPlanSelected;
            SessionManager.PlanDetails.PlanId = PlanId;
        }

    }



}