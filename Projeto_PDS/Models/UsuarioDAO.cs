﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_PDS.Models;
using Projeto_PDS.Helpers;
using MySql.Data.MySqlClient;
using Projeto_PDS.DataBase;

namespace Projeto_PDS.Models
{
    public class UsuarioDAO
    {
        private static Conexao _conn = new Conexao();

        public Usuario GetByUsuario(string usuario)
        {
            return new Usuario()
            {
                Id = 1,
                Nome = "João Teixeira",
                Senha = HashHelper.Compute("joao1234")
            };
        }

        public Usuario GetByUsuario(string usuarioNome, string senha)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM usuario " +
                    "WHERE usuario_usu = @usuario;";

                query.Parameters.AddWithValue("@usuario", usuarioNome);

                MySqlDataReader reader = query.ExecuteReader();

                Usuario usuario = null;

                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = reader.GetInt32("id_usu");
                    usuario.Nome = reader.GetString("nome_usu");
                    usuario.Permissao = reader.GetString("_usu");
                    usuario.Funcionario = new Funcionario() { Id = reader.GetInt32("id_fun"), Nome = reader.GetString("nome_fun") };
                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _conn.Close();
            }
        }
        public void Insert(Usuario usuario)
        {
            try
            {
                var comando = _conn.Query();

                comando.CommandText = "CALL InserirUsuario" +
                    "(@nome, @senha, @nivelPermissao, @idFuncionario)";

                comando.Parameters.AddWithValue("@nome", usuario.Nome);
                comando.Parameters.AddWithValue("@senha", usuario.Senha);
                comando.Parameters.AddWithValue("@nivelPermissao", usuario.Permissao);
                comando.Parameters.AddWithValue("@idFuncionario", null);

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
        public void Insert2(Usuario usuario)
        {
            try
            {

            }catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<Usuario> List()
        {
            try
            {
                List<Usuario> list = new List<Usuario>();

                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Usuario";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    var user = new Usuario();
                    user.Id = reader.GetInt32("id_usu");
                    user.Nome = Helpers.DAOHelper.GetString(reader, "nome_usu");
                    user.Permissao = Helpers.DAOHelper.GetString(reader, "nivel_permissao_usu");
                    user.Senha = Helpers.DAOHelper.GetString(reader, "senha_usu");
                   

                    list.Add(user);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
       
                
        public List<Usuario> List2()
        {
            try
            {
                Funcionario func = new Funcionario();
                List<Usuario> list = new List<Usuario>();

                var query = _conn.Query();
                query.CommandText = "SELECT usuario.id_usu, Funcionario.Nome_fun, usuario.nome_usu, usuario.nivel_permissao_usu, usuario.senha_usu from Usuario, funcionario" +
                    "where(usuario.id_fun_fk = funcionario.id_fun)";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    var user = new Usuario();
                   
                    user.Id = reader.GetInt32("usuario.id_usu");
                    user.Funcionario.Nome = Helpers.DAOHelper.GetString(reader, "funcionario.nome_fun");
                    user.Nome = Helpers.DAOHelper.GetString(reader, "usuario.nome_usu");
                    user.Permissao = Helpers.DAOHelper.GetString(reader, "usuario.nivel_permissao_usu");
                    user.Senha = Helpers.DAOHelper.GetString(reader, "usuario.senha_usu");
                    

                    list.Add(user);
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(Usuario usuario)
        {
            try
            {
                var comando = _conn.Query();
                comando.CommandText = "CALL DeletarUsuario(@id)";
                comando.Parameters.AddWithValue("@id", usuario.Id);
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
        public void Update(Usuario usuario)
        {
            try
            {
                var comando = _conn.Query();

                /*
                comando.CommandText = "UPDATE UsuarioSET " +
                    "nome_usu = @nome, permissao_usu = @perm, senha_usu = @senha, id_fun_fk = @funcionario"+
                    "WHERE id_usu = @id";
                */

                comando.CommandText = "CALL AtualizarUsuario" +
                    "(@nome, @senha, @nivelPermissao, @idFuncionario)";

                comando.Parameters.AddWithValue("@nome", usuario.Nome);
                comando.Parameters.AddWithValue("@senha", usuario.Senha);
                comando.Parameters.AddWithValue("@nivelPermissao", usuario.Permissao);
                comando.Parameters.AddWithValue("@idFuncionario", null);


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
