using SmartphoneRegister.Data.Interface;
using SmatphoneRegister.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SmartphoneRegister.Data
{
    public class SmartphoneSqlServerRepository : ISmartPhoneRepository
    {
        private readonly string stringDeConexao = "Data Source=DESKTOP-HHRHVVK;Initial Catalog=SmartphoneRegister;Integrated Security=True";

        private void ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            SqlConnection con = ObterConexao();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                foreach(SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameters);
                }
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CadastrarSmartphones(Smartphones smartphones)
        {
            SqlConnection con = ObterConexao();
            try
            {
                ExecuteNonQuery(

                    $"INSERT INTO Smartphones (marca, modelo, valor) VALUES (@Marca, @Modelo, @Valor)",
                    new SqlParameter("@Marca", smartphones.Marca),
                    new SqlParameter("@Modelo", smartphones.Modelo),
                    new SqlParameter("@Valor", smartphones.Valor)
                    
                    );
                
                
                    
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void AtualizarSmarthpones(Smartphones smartphones)
        {
            try
            {
                ExecuteNonQuery
                    (
                        $"UPDATE Smartphones SET id(marca, modelo, valor) values (@Marca,@Modelo,@Valor)", 
                        new SqlParameter("@Marca", smartphones.Marca), 
                        new SqlParameter("@Modelo", smartphones.Modelo), 
                        new SqlParameter("@Valor", smartphones.Valor)
                    ); 
                ExecuteNonQuery
                    (
                        $"INSERT INTO Smartphones(marca, modelo, valor) values (@Marca,@Modelo,@Valor)", 
                        new SqlParameter("@Marca", smartphones.Marca), 
                        new SqlParameter("@Modelo", smartphones.Modelo), 
                        new SqlParameter("@Valor", smartphones.Valor)
                    );
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Smartphones> ListarSmartphones()
        {
            List<Smartphones> smartphones = null;
            SqlConnection con = ObterConexao();
            SqlDataReader dr = null;
            try
            {
                dr = ExecuteReader(con, $"SELECT * FROM Smartphones");
                if (dr.HasRows)
                {
                    smartphones = new List<Smartphones>();
                    while (dr.Read())
                    {
                        smartphones.Add(new Smartphones(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetDecimal(3)));                    
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                FecharConexaoEDataReader(con, dr);
            }
            return smartphones;
        }

        public void RemoverSmarthpones(int id)
        {
            try
            {
                ExecuteNonQuery
                    (
                        $"DELETE FROM Smartphones WHERE id = @Id", 
                        new SqlParameter("@Id", id)
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SqlDataReader ExecuteReader(SqlConnection con, string sql, params SqlParameter[] parameters)
        {
            SqlDataReader dr;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            dr = cmd.ExecuteReader();
            return dr;
        }

        private SqlConnection ObterConexao()
        {
            return new SqlConnection(stringDeConexao);
        }

        private static void FecharConexaoEDataReader(SqlConnection con, SqlDataReader dr)
        {
            con.Close();
            if(dr != null)
            {
                dr.Close();
            }
        }

        
    }
}
