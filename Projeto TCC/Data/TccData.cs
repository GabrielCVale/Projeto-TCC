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
                                Imagem = reader["Imagem"].ToString(),
                                valor = Convert.ToDecimal(reader["Valor"]),
                                contato = reader["Contato"].ToString()
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

        public bool Cadastro(string Nome, string DataNascimento, string Email, string Senha, string CPF, string RG, string Sexo, string Telefone, string Endereco, string Bairro, string Cidade, string EstadoCivil)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Use parâmetros para evitar SQL injection
                    string query = "INSERT INTO Cadastro (NomeCompleto, dataNascimento, email, senha, cpf, rg, Sexo, Telefone, endereco, Bairro, cidade, estadoCivil) VALUES" +
                                   "(@Nome, @DataNascimento, @Email, @Senha, @CPF, @RG, @Sexo, @Telefone, @Endereco, @Bairro, @Cidade, @EstadoCivil)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Adicione parâmetros para a consulta
                        command.Parameters.AddWithValue("@Nome", Nome);
                        command.Parameters.AddWithValue("@DataNascimento", DataNascimento);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Senha", Senha);
                        command.Parameters.AddWithValue("@CPF", CPF);
                        command.Parameters.AddWithValue("@RG", RG);
                        command.Parameters.AddWithValue("@Sexo", Sexo);
                        command.Parameters.AddWithValue("@Telefone", Telefone);
                        command.Parameters.AddWithValue("@Endereco", Endereco);
                        command.Parameters.AddWithValue("@Bairro", Bairro);
                        command.Parameters.AddWithValue("@Cidade", Cidade);
                        command.Parameters.AddWithValue("@EstadoCivil", EstadoCivil);

                        // Execute a inserção
                        command.ExecuteNonQuery();
                    }

                    return true; // Retorna true se o cadastro foi bem-sucedido
                }
            }
            catch (Exception ex)
            {
                // Logar a exceção ou fazer qualquer tratamento necessário
                return false; // Retorna false se ocorrer um erro durante o cadastro
            }
        }

        public bool ValidaContato(string Nome, string Email, string Mensagem)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Use parâmetros para evitar SQL injection
                    string query = "INSERT INTO ValidaContato (Nome, Email, Mensagem) VALUES" +
                                   "(@Nome, @Email, @Mensagem)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Adicione parâmetros para a consulta
                        command.Parameters.AddWithValue("@Nome", Nome);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Mensagem", Mensagem);


                        // Execute a inserção
                        command.ExecuteNonQuery();
                    }

                    return true; // Retorna true se o cadastro foi bem-sucedido
                }
            }
            catch (Exception ex)
            {
                // Logar a exceção ou fazer qualquer tratamento necessário
                return false; // Retorna false se ocorrer um erro durante o cadastro
            }
        }

        public bool Login(string Email, string Senha)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Use parâmetros para evitar SQL injection
                    string query = "SELECT COUNT(*) FROM CADASTRO WHERE Email = @Email and Senha = @Senha";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Adicione parâmetros para a consulta
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Senha", Senha);

                        // Execute a consulta e obtenha o número de registros correspondentes
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // Se o número de registros for maior que 0, significa que há uma correspondência
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Lide com exceções apropriadamente (registre, lance, etc.)
                return false;
            }
        }
    }
}
