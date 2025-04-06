using System.Data.SqlClient;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoImpacta.Models;

namespace ProjetoImpacta.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public Cliente cliente = new Cliente();
        public string errorMessage = string.Empty;
        public string sucessMessage = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost() 
        {
            cliente.Nome = Request.Form["nome"];
            cliente.Email = Request.Form["email"];
            cliente.Telefone = Request.Form["telefone"];
            cliente.Endereco = Request.Form["endereco"];

            if(cliente.Nome.Length == 0 || cliente.Email.Length == 0 || cliente.Telefone.Length == 0 || cliente.Endereco.Length == 0)
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

                    string sql = "INSERT INTO CLIENTE" +
                                 "(nome, email, telefone, endereco) VALUES " +
                                 "(@nome, @email, @telefone, @endereco);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", cliente.Nome);
                        command.Parameters.AddWithValue("@email", cliente.Email);
                        command.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        command.Parameters.AddWithValue("@endereco", cliente.Endereco);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            cliente.Nome = "";
            cliente.Email = "";
            cliente.Telefone = "";
            cliente.Endereco = "";

            sucessMessage = "Cliente cadastrado com sucesso!";

            Response.Redirect("/Clientes/Index");
        }
    }
}
