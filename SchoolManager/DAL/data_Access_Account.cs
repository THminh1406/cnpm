using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using SchoolManager.DTO;

namespace SchoolManager.DAL
{
    public class data_Access_Account : data_Access_Base
    {
        public data_Access_Account() { }

        public Accounts Login(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_LoginTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return null;
                        string stored = GetSafeString(reader, "password_hash");
                        int id = GetSafeInt(reader, "id_teacher");
                        string name = GetSafeString(reader, "full_name");
                        string user = GetSafeString(reader, "username");
                        string role = GetSafeString(reader, "user_role");
                        bool passwordMatches = false;
                        if (IsHashedFormat(stored))
                        {
                            passwordMatches = VerifyHashedPassword(stored, password);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(stored) && stored == password)
                            {
                                string newHash = HashPassword(password);
                                UpdatePasswordHash(id, newHash);
                                passwordMatches = true;
                            }
                        }

                        if (!passwordMatches) return null;
                        bool isActive = GetSafeBool(reader, "is_active");
                        bool isManual = GetSafeBool(reader, "IsManualDeactivated");

                        if (!isActive || isManual) return null;

                        return new Accounts(id, name, user, role);
                    }
                }
            }
            catch (Exception)
            {            }

            return null;
        }
        public int GetActivationState(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetActivationState", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return -1;

                        bool isActive = GetSafeBool(reader, "is_active");
                        bool isManual = GetSafeBool(reader, "IsManualDeactivated");

                        return (isActive && !isManual) ? 1 : 0;
                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool VerifyPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || password == null) return false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetPasswordHashByUsername", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return false;

                        string stored = GetSafeString(reader, "password_hash");

                        if (IsHashedFormat(stored))
                        {
                            return VerifyHashedPassword(stored, password);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(stored) && stored == password)
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void UpdatePasswordHash(int teacherId, string newHash)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            using (SqlCommand upd = new SqlCommand("sp_UpdatePasswordHashById", conn))
            {
                upd.CommandType = CommandType.StoredProcedure;
                upd.Parameters.AddWithValue("@id", teacherId);
                upd.Parameters.AddWithValue("@hash", newHash);
                conn.Open();
                upd.ExecuteNonQuery();
            }
        }

        public bool UsernameExists(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_UsernameExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch { return false; }
        }

        public bool EmailExists(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_EmailExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch { return false; }
        }

        public string GetConnectionStringForDiagnostics()
        {
            return connection_String;
        }

        public int GetUsernameCount(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetUsernameCount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return -1;
            }
        }

        public int GetEmailCount(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetEmailCount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool RegisterTeacher(string name, string email, string username, string passwordHash, string phoneNumber, string role = "teacher")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_RegisterTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@hash", passwordHash);
                    cmd.Parameters.AddWithValue("@email", (object)email ?? DBNull.Value);
                    var phoneParam = string.IsNullOrWhiteSpace(phoneNumber) ? string.Empty : phoneNumber;
                    cmd.Parameters.AddWithValue("@phone", phoneParam);
                    cmd.Parameters.AddWithValue("@role", role);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0; 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RegisterTeacher failed: " + ex.Message, ex);
            }
        }

        public bool UpdatePasswordByEmail(string email, string newHash)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_UpdatePasswordByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@hash", newHash);
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public string CreatePasswordHash(string password)
        {
            return HashPassword(password);
        }

        private bool IsHashedFormat(string stored)
        {
            if (string.IsNullOrEmpty(stored)) return false;
            return stored.Split(':').Length == 3;
        }

        private string HashPassword(string password)
        {
            const int iterations = 10000;
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return string.Format("{0}:{1}:{2}", iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
            }
        }

        private bool VerifyHashedPassword(string stored, string password)
        {
            try
            {
                var parts = stored.Split(':');
                if (parts.Length != 3) return false;
                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] storedHash = Convert.FromBase64String(parts[2]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    byte[] computed = pbkdf2.GetBytes(storedHash.Length);
                    return AreHashesEqual(storedHash, computed);
                }
            }
            catch
            {
                return false;
            }
        }

        private bool AreHashesEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
        private bool ColumnExists(SqlDataReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        private string GetSafeString(SqlDataReader reader, string columnName)
        {
            try
            {
                if (!ColumnExists(reader, columnName)) return string.Empty;
                int idx = reader.GetOrdinal(columnName);
                return reader.IsDBNull(idx) ? string.Empty : reader.GetString(idx);
            }
            catch
            {
                return string.Empty;
            }
        }

        private int GetSafeInt(SqlDataReader reader, string columnName)
        {
            try
            {
                if (!ColumnExists(reader, columnName)) return 0;
                int idx = reader.GetOrdinal(columnName);
                return reader.IsDBNull(idx) ? 0 : Convert.ToInt32(reader.GetValue(idx));
            }
            catch
            {
                return 0;
            }
        }

        private bool GetSafeBool(SqlDataReader reader, string columnName)
        {
            try
            {
                if (!ColumnExists(reader, columnName)) return false;
                int idx = reader.GetOrdinal(columnName);
                return reader.IsDBNull(idx) ? false : Convert.ToBoolean(reader.GetValue(idx));
            }
            catch
            {
                return false;
            }
        }

        public List<Accounts> GetPendingRegistrations()
        {
            var list = new List<Accounts>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetPendingRegistrations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = GetSafeInt(reader, "id_teacher");
                            string name = GetSafeString(reader, "full_name");
                            string user = GetSafeString(reader, "username");
                            string email = GetSafeString(reader, "email");
                            string phone = GetSafeString(reader, "phone_number");
                            string role = GetSafeString(reader, "user_role");

                            Accounts acc = new Accounts(id, name, user, role);
                            acc.Email = email;
                            acc.Phone = phone;

                            try { acc.CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("created_at")); } catch { acc.CreatedAt = null; }
                            list.Add(acc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPendingRegistrations failed: " + ex.Message, ex);
            }

            return list;
        }

        public bool ApproveTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_ApproveTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RejectTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_RejectTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Accounts> GetAllTeachers()
        {
            var list = new List<Accounts>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetAllTeachers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = GetSafeInt(reader, "id_teacher");
                            string name = GetSafeString(reader, "full_name");
                            string user = GetSafeString(reader, "username");
                            string email = GetSafeString(reader, "email");
                            string phone = GetSafeString(reader, "phone_number");
                            string role = GetSafeString(reader, "user_role");

                            Accounts acc = new Accounts(id, name, user, role);
                            acc.Email = email;
                            acc.Phone = phone;
                            try { acc.CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("created_at")); } catch { acc.CreatedAt = null; }
                            list.Add(acc);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public bool LockTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_LockTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UnlockTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_UnlockTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTeacher(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_DeleteTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public int GetAssignedClassId(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetAssignedClassId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    var res = cmd.ExecuteScalar();
                    if (res == null || res == DBNull.Value) return -1;
                    return Convert.ToInt32(res);
                }
            }
            catch
            {
                return -1;
            }
        }

        public Accounts GetTeacherById(int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetTeacherById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return null;

                        int id = GetSafeInt(reader, "id_teacher");
                        string name = GetSafeString(reader, "full_name");
                        string user = GetSafeString(reader, "username");
                        string email = GetSafeString(reader, "email");
                        string phone = GetSafeString(reader, "phone_number");
                        string role = GetSafeString(reader, "user_role");

                        Accounts acc = new Accounts(id, name, user, role);
                        acc.Email = email;
                        acc.Phone = phone;
                        try { acc.CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("created_at")); } catch { acc.CreatedAt = null; }
                        return acc;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateTeacherInfo(int teacherId, string fullName, string email, string phoneNumber)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_UpdateTeacherInfo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", (object)fullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object)phoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SetPasswordHashById(int teacherId, string passwordHash)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_SetPasswordHashById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@hash", (object)passwordHash ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
