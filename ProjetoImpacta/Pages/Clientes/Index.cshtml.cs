using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoImpacta.Models;

namespace ProjetoImpacta.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<Cliente> listaClientes = new List<Cliente>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=JEFF-MOBILE;Initial Catalog=ProjetoImpacta;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();

                    string sql = "SELECT * FROM CLIENTE";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cliente cliente = new Cliente();
                                cliente.id = "" + reader.GetInt32(0);
                                cliente.Nome = reader.GetString(1);
                                cliente.Email = reader.GetString(2);
                                cliente.Telefone = reader.GetString(3);
                                cliente.Endereco = reader.GetString(4);

                                listaClientes.Add(cliente);
                            }

                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
            }

            
        }
    }
}
