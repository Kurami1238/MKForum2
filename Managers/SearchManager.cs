using MKForum.Helpers;
using MKForum.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MKForum.Managers
{
    public class SearchManager
    {

        public static List<Post> getAllSrchList(string srchText, int cboardid, List<Post> srchPostList)
        {


            string connStr = "Server=localhost;Database=MKForum;Integrated Security=True;";            //連線字串
            string commandText = @"
                        SELECT *
                        FROM [MKForum].[dbo].[Posts]
            WHERE PostCotent LIKE '%@srchWord%' OR Title LIKE '%@srchWord%'
                        ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@srchWord", srchText);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        //把取得的資料放進陣列(不知道為什麼讀不到)
                        while (reader.Read())
                        {
                            Post MatchData = new Post()
                            {
                                PostID = (Guid)reader["PostID"],
                                Title = reader["Title"] as string,
                            };
                            srchPostList.Add(MatchData);

                        }
                        return srchPostList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("HeaderArea.getSrchPostList", ex);
                throw;
            }
        }

        //取得當前子版搜尋的List
        public static List<Post> getCboardSrchList(string srchText, int cboardid, List<Post> srchPostList)
        {
            // 從Session取得當前子板塊ID
            //int cboardid = this.Session["CboradID"] as int;

            string connStr = "Server=localhost;Database=MKForum;Integrated Security=True;";            //連線字串
            string commandText = @"
                        SELECT *
                        FROM [MKForum].[dbo].[Posts]
            WHERE CboardID='%@cboardid' PostCotent LIKE '%@srchWord%' OR Title LIKE '%@srchWord%'
                        ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@srchWord", srchText);
                        command.Parameters.AddWithValue("@cboardid", cboardid);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();


                        //把取得的資料放進陣列
                        while (reader.Read())
                        {
                            Post MatchData = new Post()
                            { PostID = (Guid)reader["PostID"] };
                            srchPostList.Add(MatchData);

                        }
                        return srchPostList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("HeaderArea.getSrchPostList", ex);
                throw;
            }
        }
        //取得作者搜尋的List
        public static List<Post> getWriterSrchList(string srchText, int cboardid, List<Post> srchPostList)
        {


            string connStr = "Server=localhost;Database=MKForum;Integrated Security=True;";            //連線字串
            string commandText = @"
                SELECT *
                FROM [MKForum].[dbo].[Posts]
				WHERE PostCotent LIKE '%@srchWord%' OR Title LIKE '%@srchWord%'
                ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand command = new SqlCommand(commandText, conn))
                    {
                        command.Parameters.AddWithValue("@srchWord", srchText);

                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();


                        //把取得的資料放進陣列
                        while (reader.Read())
                        {
                            Post MatchData = new Post()
                            { PostID = (Guid)reader["PostID"] };
                            srchPostList.Add(MatchData);

                        }
                        return srchPostList;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("HeaderArea.getSrchPostList", ex);
                throw;
            }
        }

    }
}