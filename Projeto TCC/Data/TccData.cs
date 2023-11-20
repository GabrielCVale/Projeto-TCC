using System;
using System.Collections.Generic;
using MySqlConnector;
using Projeto_TCC.Models;

namespace Tcc.Data
{
    public class MySqlConnectionHelper
    {
        private readonly string _connectionString;

        public MySqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<CadastroModel> ObterViagens()
        {
            List<CadastroModel> viagens = new List<CadastroModel>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Viagens", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CadastroModel viagem = new CadastroModel
                            {
                                // Ajuste conforme suas colunas no banco de dados
                                Id = Convert.ToInt32(reader["Id"]),
                                nomeViagem = reader["NomeViagem"].ToString(),
                                Hora = reader["Hora"].ToString(),
                                data = reader["Data"].ToString(),
                                Imagem= reader["Imagem"].ToString(),
                                valor = Convert.ToDecimal(reader["Valor"])
                            };

                            viagens.Add(viagem);
                        }
                    }
                }
            }

            return viagens;
        }

        public IEnumerable<CadastroModel> ObterDetalhesViagem(int id)
        {
            List<CadastroModel> viagens = new List<CadastroModel>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Use parâmetros para evitar SQL injection
                string query = "SELECT * FROM Viagens WHERE id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Adicione um parâmetro para a consulta
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CadastroModel viagem = new CadastroModel
                            {
                                nomeViagem = reader["NomeViagem"].ToString(),
                                Hora = reader["Hora"].ToString(),
                                data = reader["Data"].ToString(),
                                Imagem = reader["Imagem"].ToString(),
                                valor = reader["Valor"] != DBNull.Value ? Convert.ToDecimal(reader["Valor"]) : 0,
                                contato = reader["Contato"].ToString()
                            };

                            viagens.Add(viagem);
                        }
                    }
                }
            }

            return viagens;
        }

    }
}
