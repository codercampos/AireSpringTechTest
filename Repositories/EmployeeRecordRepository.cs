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
            string connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Connection string is not configured");

            await using SqlConnection connection = new SqlConnection(connectionString);
            const string sql = "SELECT * FROM EmployeeRecord";
                
            await connection.OpenAsync();

            await using SqlCommand command = new SqlCommand(sql, connection);
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
}