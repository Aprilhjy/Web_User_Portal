using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Web_Portal.Models.Account;
using User_Web_Portal.Models.General;
using WebMatrix.WebData;

namespace User_Web_Portal.ViewModels.Account
{
    public class AccountViewModels
    {
        public static List<SelectListItem> GetAllRoles(int roleId)
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            { 
                using (SqlCommand cmd = new SqlCommand("usp_GetRoles", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["RoleName"].ToString();
                        item.Text = reader["RoleName"].ToString();

                        roles.Add(item);
                    }
                }
                
                return roles;
        }
    }
    public static UserProfile GetUserProfileData(int currentUserId)
        {
            UserProfile userProfile = new UserProfile();
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAdvisorProfileData", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@UserID", currentUserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    userProfile.FullName = reader["FullName"].ToString();
                    userProfile.Email = reader["Email"].ToString();
                    userProfile.Address = reader["Address"].ToString();

                }

            }
            return userProfile;
        }

        public static void UpdateUserProfile(UserProfile userProfile)
        {
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_AdvisorUpdateProfile", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@UserID", WebSecurity.CurrentUserId);
                    cmd.Parameters.AddWithValue("@FullName", userProfile.FullName);
                    cmd.Parameters.AddWithValue("@Email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@Address", userProfile.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}