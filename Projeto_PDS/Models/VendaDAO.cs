﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using Projeto_PDS.DataBase;

namespace Projeto_PDS.Models
{
    public class VendaDAO
    {
        private static Conexao _conn = new Conexao();

        public void Insert(Venda venda)
        {
            try
            {
                var comando = _conn.Query();

                comando.CommandText = "CALL InserirVenda(@valor, @dataVenda, @horaVenda, @forma_pagamento, @funcionario, @cliente)";

                comando.Parameters.AddWithValue("@valor", venda.Valor);
                comando.Parameters.AddWithValue("@dataVenda", venda.Data);
                comando.Parameters.AddWithValue("@horaVenda", venda.Hora);
                comando.Parameters.AddWithValue("@forma_pagamento", venda.FormaPagamento);
                comando.Parameters.AddWithValue("@funcionario", venda.Funcionario.Id);
                comando.Parameters.AddWithValue("@cliente", venda.Cliente.Id);

                var result = comando.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("A venda não foi realizada. Verifique e tente novamente.");

                comando.CommandText = "SELECT LAST_INSERT_ID();";
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();
                int Venda_last_id = reader.GetInt32("LAST_INSERT_ID()");

                reader.Close();

                InsertItens(Venda_last_id, venda.Itens);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertItens(int vendaId, List<VendaItem> itens)
        {

            foreach (VendaItem item in itens)
            {
                var comando = _conn.Query();
                comando.CommandText = "CALL InserirVendaProduto(@quantidade, @valor, @valor_total, @produto, @venda)";

                comando.Parameters.AddWithValue("@quantidade", item.Quantidade);
                comando.Parameters.AddWithValue("@valor", item.Valor);
                comando.Parameters.AddWithValue("@valor_total", item.ValorTotal);
                comando.Parameters.AddWithValue("@produto", item.Produto.Id);
                comando.Parameters.AddWithValue("@venda", vendaId);

                var result = comando.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Os itens da venda não foram adicionados. Verifique e tente novamente.");
            }
        }
        public void CancelarVenda(Venda venda)
        {
            try
            {
                var comando = _conn.Query();
                comando.CommandText = "CALL DeletarVenda(@id)";
                comando.Parameters.AddWithValue("@id", venda.Id);
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
    }
}
