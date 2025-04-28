using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoImpacta.Models;

namespace ProjetoImpacta.Pages.Clientes
{
    public class EditModel : PageModel
    {
        public Cliente cliente = new Cliente();
        public string errorMessage = string.Empty;
        public string sucessMessage = string.Empty;

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=JEFF-MOBILE;Initial Catalog=ProjetoImpacta;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM CLIENTE WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cliente.id = "" + reader.GetInt32(0);
                                cliente.Nome = reader.GetString(1);
                                cliente.Email = reader.GetString(2);
                                cliente.Telefone = reader.GetString(3);
                                cliente.Endereco = reader.GetString(4);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }

        public void OnPost() 
        {
            cliente.id = Request.Form["id"];
            cliente.Nome = Request.Form["nome"];
            cliente.Email = Request.Form["email"];
            cliente.Telefone = Request.Form["telefone"];
            cliente.Endereco = Request.Form["endereco"];

            if (cliente.id.Length == 0 || cliente.Nome.Length == 0 || cliente.Email.Length == 0 || cliente.Telefone.Length == 0 || cliente.Endereco.Length == 0)
            {
                errorMessage = "Todo os campos são obrigatórios";
                return;
            }

            try
            {
                string connectionString = "Data Source=JEFF-MOBILE;Initial Catalog=ProjetoImpacta;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE CLIENTE " +
                                 "SET nome=@nome, email=@email, telefone=@telefone, endereco=@endereco " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@email", cliente.Email);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        command.Parameters.AddWithValue("@endereco", cliente.Endereco);
                        command.Parameters.AddWithValue("@id", cliente.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}
