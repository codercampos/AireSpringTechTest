using AireSpringTechTest.Data;

namespace AireSpringTechTest.Repositories.Contracts;

public interface IEmployeeRecordRepository
{
    /// <summary>
    /// Gets a list of employee records from the database.
    /// </summary>
    Task<IList<EmployeeRecord>> GetEmployeeRecords();

    /// <summary>
    /// Creates a new employee record
    /// </summary>
    /// <param name="employeeRecord">The employee record's model</param>
    Task InsertEmployeeRecord(EmployeeRecord employeeRecord);

    /// <summary>
    /// Gets a single employee record that belongs to the specified ID
    /// </summary>
    /// <param name="id">The employee ID that will be retrieved</param>
    Task<EmployeeRecord?> GetEmployeeRecord(int id);

    /// <summary>
    /// Updates a single employee record that belongs to the specified ID
    /// </summary>
    /// <param name="employeeRecord"></param>
    Task UpdateEmployeeRecord(EmployeeRecord employeeRecord);

    /// <summary>
    /// Deletes a single employee record that belongs to the specified ID
    /// </summary>
    /// <param name="employeeId"></param>
    Task DeleteEmployeeRecord(int employeeId);
}