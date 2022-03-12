using MKForum.Helpers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MKForum.Managers
{
    public class AccountManager
    {
        public Member GetAccount(string account)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Members
                    WHERE Account = @account ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@account", account);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Member member = new Member()
                            {
                                MemberID = (Guid)reader["MemberID"],
                                Account = reader["Account"] as string,
                                Password = reader["PWD"] as string,
                                Email = reader["Email"] as string
                            };

                            return member;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountMember.GetAccount(string account)", ex);
                throw;
            }
        }
        public Member GetAccount(Guid id)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Members
                    WHERE MemberID = @id ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Member member = new Member()
                            {
                                MemberID = (Guid)reader["MemberID"],
                                Account = reader["Account"] as string,
                                Password = reader["PWD"] as string
                            };

                            return member;
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountMember.GetAccount(Guid id)", ex);
                throw;
            }
        }
        public void CreateAccount(Member member, MemberRegister memberRegister)
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) != null)
                throw new Exception("已存在相同的帳號");

            // 2. 新增資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  
                INSERT INTO Members(MemberID,Account,PWD,Email)VALUES(@MemberID,@Account,@PWD,@Email)

                INSERT INTO MemberRecords (MemberID)VALUES(@MemberID)

                 INSERT INTO MemberRegisters(MemberID,Captcha)
                  VALUES(@MemberID,@Captcha)
                  ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        member.MemberID = Guid.NewGuid();
                        command.Parameters.AddWithValue("@MemberID", member.MemberID);
                        command.Parameters.AddWithValue("@Account", member.Account);
                        command.Parameters.AddWithValue("@PWD", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@Captcha", memberRegister.Captcha);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountMember.CreateAccount(Member member)", ex);
                throw;
            }
        }
        public void UpdateAccount(Member member)
        {
            // 1. 判斷資料庫是否有相同的 Account
            if (this.GetAccount(member.Account) == null)
                throw new Exception("帳號不存在：" + member.Account);

            // 2. 編輯資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  UPDATE Members
                    SET 
                        PWD = @pwd
                    WHERE
                        ID = @id ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@id", member.MemberID);
                        command.Parameters.AddWithValue("@pwd", member.Password);

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountMember.UpdateAccount(Member member)", ex);
                throw;
            }
        }

        public void DeleteAccounts(List<Guid> ids)
        {
            // 1. 判斷是否有傳入 id
            if (ids == null || ids.Count == 0)
                throw new Exception("需指定 id");

            List<string> param = new List<string>();
            for (var i = 0; i < ids.Count; i++)
            {
                param.Add("@id" + i);
            }
            string inSql = string.Join(", ", param);    // @id1, @id2, @id3, etc...

            // 2. 刪除資料
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                $@" DELETE Members
                    WHERE MemberID IN ({inSql}) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            command.Parameters.AddWithValue("@id" + i, ids[i]);
                        }

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("MapContentManager.GetMapList", ex);
                throw;
            }
        }

        public List<Member> GetAccountList(string keyword)
        {
            string connStr = ConfigHelper.GetConnectionString();
            string commandText =
                @"  SELECT *
                    FROM Members ";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                commandText += " WHERE Account LIKE '%'+@keyword+'%'";
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            command.Parameters.AddWithValue("@keyword", keyword);
                        }

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        List<Member> list = new List<Member>();

                        while (reader.Read())
                        {
                            Member member = new Member()
                            {
                                MemberID = (Guid)reader["MemberID"],
                                Account = reader["Account"] as string
                            };

                            list.Add(member);
                        }

                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("AccountMember.List<Member> GetAccountList(string keyword)", ex);
                throw;
            }
        }

        #region "帳號註冊"
        public bool TryRegister(string account, string password, string email, string capt)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;
            bool isEmailRight = false;
            bool isCaptRight = false;

            Member memberAcc = this.GetAccount(account);//抓SQL帳號

            //帳號
            if (memberAcc == null)//找不到就能註冊
                isAccountRight = true;
            if (string.Empty == account)
                isAccountRight = false;
            //密碼強度-使用regex进行格式设置 至少有数字、大小写字母，最少3个字符、最长8个字符
            Regex regex = new Regex(@"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{3,8}");
            if (regex.IsMatch(password))
            {
                isPasswordRight = true;
            }
            else
            {
                isPasswordRight = false;
            }
            //Email格式
            if (email == null)
            {
                isEmailRight = false;
            }
            if (new EmailAddressAttribute().IsValid(email))
            {
                isEmailRight = true;
            }
            else
            {

                isEmailRight = false;
            }
            //驗證碼
            if (capt == null)
            {
                isCaptRight = false;
            }
            else
            {
                isCaptRight = true;
            }

            //檢查帳號、密碼、Email格式、驗證碼是否正確
            bool result = (isAccountRight && isPasswordRight && isEmailRight && isCaptRight);
            ;

            //帳密正確，把值寫入Session
            if (result)
            {
                HttpContext.Current.Session["Member"] = new Member()
                {
                    Account = account,
                    Password = password,
                    Email = email,

                };
                HttpContext.Current.Session["MemberRegister"] = new MemberRegister()
                {

                    Captcha = capt
                };
            }


            return result;

        }
        public void CreateRegister(Member member, MemberRegister memberRegister)
        {
            string connStr = ConfigHelper.GetConnectionString();

            string commandText = @"
              
             
            INSERT INTO Members(MemberID,Account,PWD,Email)VALUES(@MemberID,@Account,@PWD,@Email)

            INSERT INTO MemberRecords (MemberID) 
            VALUES(@MemberID)

             INSERT INTO MemberRegisters(MemberID,Captcha)
              VALUES(@MemberID,@Captcha)

                   ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        conn.Open();
                        var md = Guid.NewGuid();
                        command.Parameters.AddWithValue("@Account", member.Account);
                        command.Parameters.AddWithValue("@PWD", member.Password);
                        command.Parameters.AddWithValue("@Email", member.Email);
                        command.Parameters.AddWithValue("@MemberID", md);
                        //command.Parameters.AddWithValue("@RegistrationDate", memberRecord.RegistrationDate);
                        command.Parameters.AddWithValue("@Captcha", memberRegister.Captcha);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("RegisterManager.CreateRegister", ex);
                throw;
            }
        }
        public Member GetCurrentUser()
        {
            Member account = HttpContext.Current.Session["Member"] as Member;

            return account;

        }
        public bool IsRegister()
        {
            Member account = GetCurrentUser();

            return (account != null);
        }
        #endregion

        #region  "驗證碼"
        //1.獲取驗證碼
        public string CreateValidateCode(int length)
        {
            string validateCode = "";
            //int[] randMembers = new int[length];
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                validateCode += r.Next(0, 9).ToString();
            }
            return validateCode;
        }

        //2.製作圖片
        public void CreateValidateImg(string code, HttpContext context)
        {
            //定義圖片
            Bitmap image = new Bitmap((int)Math.Ceiling(code.Length * 12.0), 22);
            Graphics graphics = Graphics.FromImage(image);
            //生成隨機字體顏色
            try
            {
                Random r = new Random();
                graphics.Clear(Color.White);
                //繪製線條
                for (int i = 0; i < 10; i++)
                {
                    int x1 = r.Next(image.Width);
                    int x2 = r.Next(image.Width);
                    int y1 = r.Next(image.Height);
                    int y2 = r.Next(image.Height);
                    graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                //繪製文本
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                for (int i = 0; i < code.Length; i++)
                {
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)), Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)), 0, true);
                    graphics.DrawString(code[i].ToString(), font, brush, 3 + i * 10, 2);
                }

                //繪製前景
                for (int i = 0; i < 120; i++)
                {
                    image.SetPixel(r.Next(image.Width), r.Next(image.Height), Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)));
                }

                //將繪製的驗證碼圖片保存爲jpg，並寫入到頁面
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                context.Response.Clear();
                context.Response.ContentType = "Image/jpeg";
                context.Response.BinaryWrite(stream.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                image.Dispose();
                graphics.Dispose();
            }
        }


        #endregion "i"

        #region"帳號登入"
        public bool TryLogin(string account, string password)
        {
            bool isAccountRight = false;
            bool isPasswordRight = false;

            Member memberAcc = this.GetAccount(account);

            if (memberAcc == null)//找不到就登入失敗
                return false;

            if (string.Compare(memberAcc.Account, account, true) == 0)
                isAccountRight = true;
            if (memberAcc.Password == password)
                isPasswordRight = true;
            //檢查帳號密碼是否正確
            bool result = (isAccountRight && isPasswordRight);

            //帳密正確，把值寫入Session
            if (result)
            {
                memberAcc.Password = null;
                HttpContext.Current.Session["Member"] = memberAcc;


            }


            return result;
        }
        #endregion
    }
}