using AdmissionsCommissionOnline.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdmissionsCommissionOnline
{
    public class BDcontext
    {
        const string connectionString = "Host=localhost;Database=AdmissionsCommissionOnline;Username=postgres;Password=0211";

        public NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        public List<Education> GetEducation()
        {
            List< Education > educations = new List< Education >();

            string sqlProduct = "SELECT * FROM \"education\"";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlProduct, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Education education = new Education();
                        education.Id = Convert.ToInt16(reader.GetValue(1));
                        education.Title = reader.GetValue(1).ToString();
                        education.EducationType = reader.GetValue(2).ToString();
                        educations.Add(education);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return educations;
        }

        public List<Education> GetEducationForTupe(string EducationType)
        {
            List<Education> educations = new List<Education>();

            string sqlProduct = $"SELECT * FROM \"education\" WHERE LOWER(\"educationtype\") = LOWER('{EducationType}')";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlProduct, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Education education = new Education();
                        education.Id = Convert.ToInt16(reader.GetValue(0));
                        education.Title = reader.GetValue(1).ToString();
                        education.EducationType = reader.GetValue(2).ToString();
                        educations.Add(education);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return educations;
        }

        public List<int> GetEducationForTitle(string EducationTitle)
        {
            List<int> educations = new List<int>();

            string sqlProduct = $"SELECT * FROM \"education\" WHERE LOWER(\"title\") = LOWER('{EducationTitle}')";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlProduct, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int education = new int();
                        education = Convert.ToInt16(reader.GetValue(0));                      
                        educations.Add(education);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return educations;
        }

        public List<USExam> GetUSExam()
        {
            List<USExam> exams = new List<USExam>();

            string sqlProduct = "SELECT * FROM \"usexam\"";
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlProduct, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        USExam exam = new USExam();

                        exam.Id = Convert.ToInt32(reader.GetValue(0));
                        exam.Title = reader.GetValue(1).ToString();
                        exams.Add(exam);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return exams;
        }

        public void AddEnrollee(Enrollee enrollee)
        {
            string sql = "INSERT INTO enrollee (fio, fio_parent, email, password, phone, snils, seria, number, educational_institution, document, point) VALUES (@FIO, @FIOParent, @Email, @Password, @Phone, @SNILS, @Seria, @Number, @EducationalInstitution, @Document, @Point)";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@FIO", enrollee.FIO);
                    command.Parameters.AddWithValue("@FIOParent", enrollee.FIOParent);
                    command.Parameters.AddWithValue("@Email", enrollee.Email);
                    command.Parameters.AddWithValue("@Password", enrollee.Password);
                    command.Parameters.AddWithValue("@Phone", enrollee.Phone);
                    command.Parameters.AddWithValue("@SNILS", enrollee.SNILS);
                    command.Parameters.AddWithValue("@Seria", enrollee.Seria);
                    command.Parameters.AddWithValue("@Number", enrollee.Number);
                    command.Parameters.AddWithValue("@EducationalInstitution", enrollee.EducationalInstitution);
                    command.Parameters.AddWithValue("@Document", enrollee.Document);
                    command.Parameters.AddWithValue("@Point", enrollee.Point);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddUser(string Email, string Password)
        {
            string sql = "INSERT INTO \"user\" ( email, password,roles) VALUES ( @Email, @Password, @Roles)";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Roles", "us");
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            bool isAuthenticated = false;
            string sql = "SELECT COUNT(*) FROM \"user\" WHERE email = @Email AND password = @Password";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    // Проверяем, есть ли пользователь с указанным логином и паролем
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        isAuthenticated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isAuthenticated;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            string sql = "SELECT id, email, password, roles FROM \"user\" WHERE email = @Email";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Email", email);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Email = Convert.ToString(reader["email"]),
                                Rols = Convert.ToString(reader["roles"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return user;
        }

    }
}
