using AireSpringTechTest.Data;

namespace AireSpringTechTest.Repositories.Contracts;

public interface IEmployeeRecordRepository
{
    /// <summary>
    /// Gets a list of employee records from the database.
    /// </summary>
    Task<IList<EmployeeRecord>> GetEmployeeRecords();
}