using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Configuration;
using System.Threading;
using System.Xml;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.DAL
{
    public class UserDAL
    {
       
        private static readonly BaseDAL _baseDAL = new BaseDAL();
        public static string sGetAllUsers = "[sa].[Proc_GetAllUsers]";
        private static Random random = new Random();

        public async Task<List<User>> fn_GetAllUsers(string ActionType)
        {
            List<User> _lst = new List<User>();
            try
            {
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                    new CustomDataPair() { Key = "@ActionType", Obj = ActionType}
                };
                System.Data.DataTable dt = _baseDAL.GetData(sGetAllUsers, CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, BaseDAL.GenerateDataParameters(stringDataPairs));

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        User obj = new User();
                        obj.UserID = Convert.ToInt32(dr["UserID"].ToString());
                        obj.UserName = dr["UserName"].ToString();
                        obj.FName = dr["FName"].ToString();
                        obj.LName = dr["LName"].ToString();
                        //obj.Password = dr["Password"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.UserType = dr["UserType"].ToString();
                        obj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                        obj.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        obj.ResponseMsg = dr["ResponseMessage"].ToString();
                        _lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return _lst;
        }
        public async Task<User> fn_GetAllUsersByID(string ActionType, int? UserId)
        {
            User usr = new User();
            try
            {
                List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                {
                    new CustomDataPair() { Key = "@UserId", Obj = UserId},
                    new CustomDataPair() { Key = "@ActionType", Obj = ActionType}
                };
                System.Data.DataTable dt = _baseDAL.GetData(sGetAllUsers, CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, BaseDAL.GenerateDataParameters(stringDataPairs));

                if (dt != null && dt.Rows.Count > 0)
                {
                     usr.UserID = Convert.ToInt32(dt.Rows[0]["UserID"].ToString());
                     usr.UserName = dt.Rows[0]["UserName"].ToString();
                     usr.FName = dt.Rows[0]["FName"].ToString();
                     usr.LName = dt.Rows[0]["LName"].ToString();
                     //usr.Password = dt.Rows[0]["Password"].ToString();
                     usr.Email = dt.Rows[0]["Email"].ToString();
                     usr.UserType = dt.Rows[0]["UserType"].ToString();
                     usr.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"].ToString());
                     usr.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                     usr.ResponseMsg = dt.Rows[0]["ResponseMessage"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return usr;
        }
        public async Task<User> fn_InsertUser(string ActionType, User user)
        {
            User _user = new User();
            try
            {
                if (user != null)
                {
                        //string sixteenDigitGuid = RandomString(16);
                        List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                         {
                           new CustomDataPair() { Key = "@FName", Obj = user.FName==null?"":user.FName },
                           new CustomDataPair() { Key = "@LName", Obj = user.LName==null?"":user.LName },
                           new CustomDataPair() { Key = "@Email", Obj = user.Email==null?"":user.Email },
                           new CustomDataPair() { Key = "@UserType", Obj = user.UserType==null?"":user.UserType },
                           new CustomDataPair() { Key = "@UserName", Obj = user.UserName==null?"":user.UserName },
                           new CustomDataPair() { Key = "@Password", Obj = user.Password==null?"":user.Password },
                           new CustomDataPair() { Key = "@ActionType", Obj = ActionType}
                         };
                        System.Data.DataTable dtnew = _baseDAL.GetData(sGetAllUsers, CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, BaseDAL.GenerateDataParameters(stringDataPairs));
                    if (dtnew != null)
                    {
                        if (dtnew.Rows.Count > 0)
                        {
                            _user.ResponseMsg = dtnew.Rows[0]["ResponseMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _user;
        }
        public async Task<User> fn_DeleteUser(string ActionType, int? UserId)
        {
            User _user = new User();
            try
            {
                if (UserId > 0)
                {
                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                         {
                            new CustomDataPair() { Key = "@UserId", Obj = UserId},
                            new CustomDataPair() { Key = "@ActionType", Obj = ActionType} 
                         };
                    System.Data.DataTable dtnew = _baseDAL.GetData(sGetAllUsers, CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, BaseDAL.GenerateDataParameters(stringDataPairs));
                    if (dtnew != null)
                    {
                        if (dtnew.Rows.Count > 0)
                        {
                            _user.ResponseMsg = dtnew.Rows[0]["ResponseMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _user;
        }
        public async Task<User> fn_UpdateUser(string ActionType, User user)
        {
            User _user = new User();
            try
            {
                if (user != null)
                {               
                    List<CustomDataPair> stringDataPairs = new List<CustomDataPair>
                         {
                           new CustomDataPair() { Key = "@FName", Obj = user.FName==null?"":user.FName },
                           new CustomDataPair() { Key = "@LName", Obj = user.LName==null?"":user.LName },
                           new CustomDataPair() { Key = "@Email", Obj = user.Email==null?"":user.Email },
                           new CustomDataPair() { Key = "@UserType", Obj = user.UserType==null?"":user.UserType },
                           new CustomDataPair() { Key = "@UserName", Obj = user.UserName==null?"":user.UserName },
                           new CustomDataPair() { Key = "@ActionType", Obj = ActionType},
                           new CustomDataPair() { Key = "@UserId", Obj = user.UserID},
                         };
                    System.Data.DataTable dtnew = _baseDAL.GetData(sGetAllUsers, CommonVariables.SqlCommandTimeout, CommandType.StoredProcedure, BaseDAL.GenerateDataParameters(stringDataPairs));
                    if (dtnew != null)
                    {
                        if (dtnew.Rows.Count > 0)
                        {
                            _user.ResponseMsg = dtnew.Rows[0]["ResponseMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _user;
        }
    }
}
