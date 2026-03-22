using SE_Project.Pages;
using SE_Project.Forms;
using SE_Project.PagesParts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace SE_Project.Helpers
{
    class DBHelper
    {
        // Connection string for localdb
        static string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WorkSyncDB;Integrated Security=True;";

        SqlConnection conn;

        public DBHelper()
        {
            conn = new SqlConnection(conString);
        }

        // --- CONNECTION HELPERS ---
        public bool connect()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error: " + ex.Message);
                return false;
            }
        }

        public void disconnect()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        // --- ROW OPERATIONS ---
        public string InsertRow(string query)
        {
            try
            {
                if (connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int result = cmd.ExecuteNonQuery();
                        return result > 0 ? "Row inserted successfully." : "No rows were inserted.";
                    }
                }
                return "Failed to connect to database.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                disconnect();
            }
        }

        public string DeleteRow(string query)
        {
            try
            {
                if (connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int result = cmd.ExecuteNonQuery();
                        return result > 0 ? "Row deleted successfully." : "No rows were deleted.";
                    }
                }
                return "Failed to connect to database.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
            finally
            {
                disconnect();
            }
        }

        public int GetTotalRowCount(string tableName)
        {
            string query = $"SELECT COUNT(*) FROM {tableName};";
            try
            {
                if (connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                disconnect();
            }
        }

        // --- PROJECT SPECIFIC LOGIC ---
        public string getButtons(string query, FlowLayoutPanel panel)
        {
            string ret = "";
            try
            {
                if (connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            panel.Controls.Clear();

                            while (reader.Read())
                            {
                                // Yahan UserControl1 use kar rahe hain kyunki aapka file name UserControl1 hai
                                UserControl1 card = new UserControl1();

                                // Column names match hone chahiye (name, description)
                                card.ProjectTitle = reader["name"].ToString();
                                card.ProjectDesc = reader["description"].ToString();

                                if (!string.IsNullOrEmpty(card.ProjectTitle))
                                {
                                    panel.Controls.Add(card);
                                }
                            }
                            ret = "Data Fetched Successfully.. :)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = "Error: " + ex.Message;
            }
            finally
            {
                disconnect();
            }
            return ret;
        }

        // Ye method project ko ek table se dusri mein move karne ke liye (Sahi Logic)
        public bool MoveProject(string projectName, string projectDesc, string sourceTable, string targetTable)
        {
            try
            {
                if (connect())
                {
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Target table mein insert karo
                            string insertQuery = $"INSERT INTO {targetTable} (name, description) VALUES (@name, @desc)";
                            using (SqlCommand insCmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                insCmd.Parameters.AddWithValue("@name", projectName);
                                insCmd.Parameters.AddWithValue("@desc", projectDesc);
                                insCmd.ExecuteNonQuery();
                            }

                            // 2. Source table se delete karo
                            string deleteQuery = $"DELETE FROM {sourceTable} WHERE name = @name";
                            using (SqlCommand delCmd = new SqlCommand(deleteQuery, conn, transaction))
                            {
                                delCmd.Parameters.AddWithValue("@name", projectName);
                                delCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            finally
            {
                disconnect();
            }
        }

        // --- AUTHENTICATION ---
        public bool userAuth(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM userlist WHERE username = @username AND password = @password;";
            try
            {
                if (connect())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result > 0;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Auth Error: " + ex.Message);
                return false;
            }
            finally
            {
                disconnect();
            }
        }
    }
}