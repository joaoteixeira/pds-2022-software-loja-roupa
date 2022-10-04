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

                comando.CommandText = "INSERT Into Cliente Values " +

                    "(null, @nome, @email, @cpf, @telefone, @rua, @numero, @bairro, @rg, @data_nasc, @renda_familiar, null, null)"; //ULTIMO NULL É DA FOREIGN KEY DE SEXO

                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@cpf", cliente.Cpf);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@rua", cliente.Rua);
                comando.Parameters.AddWithValue("@numero", cliente.Numero);
                comando.Parameters.AddWithValue("@bairro", cliente.Bairro);
                comando.Parameters.AddWithValue("@rg", cliente.Rg);
                comando.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@renda_familiar", cliente.RendaFamiliar);
                //comando.Parameters.AddWithValue("@foto", cliente.Foto);

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
                    cliente.Rua = Helpers.DAOHelper.GetString(reader, "rua_cli");
                    cliente.Numero = Helpers.DAOHelper.GetString(reader, "numero_cli");
                    cliente.Bairro = Helpers.DAOHelper.GetString(reader, "bairro_cli");
                    cliente.Rg = Helpers.DAOHelper.GetString(reader, "rg_cli");
                    cliente.DataNasc = Convert.ToDateTime(Helpers.DAOHelper.GetString(reader, "data_nasc_cli"));
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
                comando.CommandText = "DELETE FROM cliente WHERE id_cli = @id";
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
                    "nome_cli = @nome, email_cli = @email, cpf_cli = @cpf, telefone_cli = @telefone, rua_cli = @rua, numero_cli = @numero, bairro_cli = @bairro, rg_cli = @rg, " +
                    "data_nasc_cli = @data_nasc, renda_familiar_cli = @renda_familiar, foto_cli = null" +
                    "WHERE id_cli = @id";

                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@email", cliente.Email);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@rua", cliente.Rua);
                comando.Parameters.AddWithValue("@numero", cliente.Numero);
                comando.Parameters.AddWithValue("@bairro", cliente.Bairro);
                comando.Parameters.AddWithValue("@rg", cliente.Rg);
                comando.Parameters.AddWithValue("@data_nasc", cliente.DataNasc?.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@renda_familiar", cliente.RendaFamiliar);

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
