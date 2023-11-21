using Npgsql;
using sales_departements.Context;

namespace sales_departements.Models;

public class Session
{
   public string? Value {get; set;}
   public void SetSession(string value) {
      NpgsqlConnection connection = new Connection().Connect();
      NpgsqlCommand command = new NpgsqlCommand("UPDATE session SET value ='"+value+"' RETURNING id", connection);
      command.ExecuteReader();
      connection.Close();
   }

   public string? GetSession() {
      NpgsqlConnection connection = new Connection().Connect();
      NpgsqlCommand command = new NpgsqlCommand("SELECT *FROM session", connection);
      NpgsqlDataReader dr = command.ExecuteReader();
      if(dr.Read()) {
         return dr["value"].ToString();
      }
      connection.Close();
      return "";
   }



}