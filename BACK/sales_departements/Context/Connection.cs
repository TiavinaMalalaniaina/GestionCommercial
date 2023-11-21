using Npgsql;

namespace sales_departements.Context;

public class Connection
{
    private readonly string Host =  "localhost";
    private readonly string User =  "postgres";
    private readonly string Password =  "mdpprom15";
    private readonly string Database =  "sales_departement";

    public NpgsqlConnection Connect() {
        string connectionString = "Host="+Host+";Username="+User+";Password="+Password+";Database="+Database;
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }

}
