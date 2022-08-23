﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Projeto_PDS.DataBase;

namespace Projeto_PDS.Models
{
    public class ClienteDAO
    {
        private static Conexao _conn = new Conexao();

        public void Insert(Cliente cliente)
        {
            try
            {
                var comando = _conn.Query();

                comando.CommandText = "INSERT Into Cliente Value " +

                    "(null, @nome, @email, @cpf, @telefone, @endereco, @rg, @data_nasc, @sexo, @renda_familiar, @foto)";

                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
                comando.Parameters.AddWithValue("@rg", cliente.Rg);
                comando.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@sexo", cliente.Sexo);
                comando.Parameters.AddWithValue("@renda_familiar", cliente.RendaFamiliar);
                comando.Parameters.AddWithValue("@foto", cliente.Foto);

                var resultado = comando.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Ocorreram erros ao salvar as informações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cliente> List()
        {
            try
            {
                List<Cliente> list = new List<Cliente>();

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM cliente";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    var cliente = new Cliente();
                    cliente.Id = reader.GetInt32("id_cli");
                    cliente.Nome = Helpers.DAOHelper.GetString(reader, "nome_cli");
                    cliente.Email = Helpers.DAOHelper.GetString(reader, "email_cli");
                    cliente.Cpf = Helpers.DAOHelper.GetString(reader, "cpf_cli");
                    cliente.Telefone = Helpers.DAOHelper.GetString(reader, "telefone_cli");
                    cliente.Endereco = Helpers.DAOHelper.GetString(reader, "endereco_cli");
                    cliente.Rg = Helpers.DAOHelper.GetString(reader, "rg_cli");
                    cliente.DataNasc = Convert.ToDateTime(Helpers.DAOHelper.GetString(reader, "data_nasc_cli"));
                    cliente.Sexo = Helpers.DAOHelper.GetString(reader, "sexo_cli");
                    cliente.RendaFamiliar = Helpers.DAOHelper.GetString(reader, "renda_familiar_cli");
                    cliente.Foto = Helpers.DAOHelper.GetString(reader, "foto_cli");

                    list.Add(cliente);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(Cliente cliente)
        {
            try
            {
                var comando = _conn.Query();
                comando.CommandText = "DELETE FROM cliente WHERE id_esc = @id";
                comando.Parameters.AddWithValue("@id", cliente.Id);
                var resultado = comando.ExecuteNonQuery();
                if (resultado == 0)
                {
                    throw new Exception("Ocorreram problemas ao salvar as informações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Cliente cliente)
        {
            try
            {
                var comando = _conn.Query();

                comando.CommandText = "UPDATE Cliente SET " +
                    "nome_cli = @nome, email_cli = @email, cpf_cli = @cpf, telefone_cli = @telefone, endereco_cli = @endereco, rg_cli = @rg, " +
                    "data_nasc_cli = @data_nasc, sexo_cli = @sexo, renda_familiar_cli = @renda_familiar, foto_cli = @foto" +
                    "WHERE id_cli = @id";

                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
                comando.Parameters.AddWithValue("@rg", cliente.Rg);
                comando.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@sexo", cliente.Sexo);
                comando.Parameters.AddWithValue("@renda_familiar", cliente.RendaFamiliar);
                comando.Parameters.AddWithValue("@foto", cliente.Foto);

                var resultado = comando.ExecuteNonQuery();

                if (resultado == 0)
                {
                    throw new Exception("Ocorreram erros ao salvar as informações");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
