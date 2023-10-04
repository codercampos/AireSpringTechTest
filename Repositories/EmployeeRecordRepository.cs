using System.Data;
using AireSpringTechTest.Data;
using AireSpringTechTest.Repositories.Contracts;
using Microsoft.Data.SqlClient;

namespace AireSpringTechTest.Repositories;

public class EmployeeRecordRepository : IEmployeeRecordRepository
{
    private readonly IConfiguration _configuration;
    public EmployeeRecordRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IList<EmployeeRecord>> GetEmployeeRecords()
    {
        IList<EmployeeRecord> employeeRecords = new List<EmployeeRecord>();
        try
        {
            SqlConnection connection = GetConnection();
            const string sql = "SELECT * FROM EmployeeRecord";
                
            await connection.OpenAsync();

            await using SqlCommand command = new (sql, connection);
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            
            while (reader.Read())
            {
                // Get the model
                EmployeeRecord employeeRecord = new()
                {
                    EmployeeId = (int)reader.GetValue("EmployeeID"),
                    EmployeeLastName = (string)reader.GetValue("EmployeeLastName"),
                    EmployeeFirstName = (string)reader.GetValue("EmployeeFirstName"),
                    EmployeePhone = (string)reader.GetValue("EmployeePhone"),
                    EmployeeZip = (string)reader.GetValue("EmployeeZip"),
                    HireDate = (DateTime)reader.GetValue("HireDate")
                };
                employeeRecords.Add(employeeRecord);
            }

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return employeeRecords;
    }
    
    public async Task<EmployeeRecord?> GetEmployeeRecord(int id)
    {
        EmployeeRecord? employeeRecord = null;
        try
        {
            SqlConnection connection = GetConnection();
            const string sql = "SELECT * FROM EmployeeRecord WHERE EmployeeID = @inEmployeeId";
                
            await connection.OpenAsync();

            await using SqlCommand command = new (sql, connection);
            command.Parameters.Add("@inEmployeeId", SqlDbType.Int).Value = id;
            await using SqlDataReader reader = await command.ExecuteReaderAsync();
            
            if (reader.Read())
            {
                // Get the model
                employeeRecord = new EmployeeRecord 
                {
                    EmployeeId = (int)reader.GetValue("EmployeeID"),
                    EmployeeLastName = (string)reader.GetValue("EmployeeLastName"),
                    EmployeeFirstName = (string)reader.GetValue("EmployeeFirstName"),
                    EmployeePhone = (string)reader.GetValue("EmployeePhone"),
                    EmployeeZip = (string)reader.GetValue("EmployeeZip"),
                    HireDate = (DateTime)reader.GetValue("HireDate")
                };
                
            }

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return employeeRecord;
    }
    
    
    public async Task InsertEmployeeRecord(EmployeeRecord employeeRecord)
    {
        try
        {
            SqlConnection connection = GetConnection();
            const string sql = "INSERT INTO EmployeeRecord VALUES (@inLastName, @inFirstName, @inPhoneNumber, @inZip, @inHireDate)";
            await connection.OpenAsync();

            await using SqlCommand command = new (sql, connection);
            command.Parameters.Add("@inLastName", SqlDbType.NVarChar).Value = employeeRecord.EmployeeLastName;
            command.Parameters.Add("@inFirstName", SqlDbType.NVarChar).Value = employeeRecord.EmployeeFirstName;
            command.Parameters.Add("@inPhoneNumber", SqlDbType.NVarChar).Value = employeeRecord.EmployeePhone;
            command.Parameters.Add("@inZip", SqlDbType.NVarChar).Value = employeeRecord.EmployeeZip;
            command.Parameters.Add("@inHireDate", SqlDbType.Date).Value = employeeRecord.HireDate;
            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task UpdateEmployeeRecord(EmployeeRecord employeeRecord)
    {
        try
        {
            SqlConnection connection = GetConnection();
            const string sql = "UPDATE EmployeeRecord SET EmployeeLastName = @inLastName, " +
                               "EmployeeFirstName = @inFirstName, " +
                               "EmployeePhone = @inPhoneNumber, " +
                               "EmployeeZip = @inZip, " +
                               "HireDate = @inHireDate " +
                               "WHERE EmployeeID = @inEmployeeId";
            await connection.OpenAsync();

            await using SqlCommand command = new (sql, connection);
            command.Parameters.Add("@inLastName", SqlDbType.NVarChar).Value = employeeRecord.EmployeeLastName;
            command.Parameters.Add("@inFirstName", SqlDbType.NVarChar).Value = employeeRecord.EmployeeFirstName;
            command.Parameters.Add("@inPhoneNumber", SqlDbType.NVarChar).Value = employeeRecord.EmployeePhone;
            command.Parameters.Add("@inZip", SqlDbType.NVarChar).Value = employeeRecord.EmployeeZip;
            command.Parameters.Add("@inHireDate", SqlDbType.Date).Value = employeeRecord.HireDate;
            command.Parameters.Add("@inEmployeeId", SqlDbType.Int).Value = employeeRecord.EmployeeId;
            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task DeleteEmployeeRecord(int employeeId)
    {
        try
        {
            SqlConnection connection = GetConnection();
            const string sql = "DELETE FROM EmployeeRecord WHERE EmployeeID = @inEmployeeId";
            await connection.OpenAsync();

            await using SqlCommand command = new (sql, connection);
            command.Parameters.Add("@inEmployeeId", SqlDbType.Int).Value = employeeId;
            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SqlConnection GetConnection()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("Connection string is not configured");

        return new SqlConnection(connectionString);
    }
}