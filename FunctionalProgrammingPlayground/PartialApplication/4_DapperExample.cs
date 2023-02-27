using Dapper;
using LaYumba.Functional;
using System.Data;
using System.Data.SqlClient;

namespace FunctionalProgrammingPlayground.PartialApplication;
internal class DapperExample
{
    public void Run()
    {
        ConnectionString conn = "retrieve ConnectionString from Configuration";
        SqlTemplate defaultSql = "SELECT * FROM EMPLOYEES";
        SqlTemplate sqlById = $"{defaultSql} WHERE ID = @Id";
        SqlTemplate sqlByName = $"{defaultSql} WHER LASTNAME = @LastName";

        // The following two methods particularize the very general Retrieve function
        // ((ConnectionString, SqlTemplate) -> object -> IEnumerable<T>)
        // by supplying the particularizing parameters ConnectionString and SqlTemplate.
        // They, in turn, return a function that requires an object (i.e. SQL parameter) to return an IEnumerable<T>

        // queryById: object -> IEnumerable<Employee>
        var queryById = conn.Retrieve<Employee>(sqlById);
        // queryByLastName: object -> IEnumerable<Employee>
        var queryByLastName = conn.Retrieve<Employee>(sqlByName);

        var randomEmployee = LookupEmployee(Guid.NewGuid());
        var employeesNamedCurry = FindEmployeesByLastName("Curry");

        // lookupEmployee: Guid -> Option<Employee>
        Option<Employee> LookupEmployee(Guid id) => queryById(new { Id = id }).SingleOrDefault();
        // findEmployeesByLastName: string -> IEnumerable<Employee>
        IEnumerable<Employee> FindEmployeesByLastName(string lastName) =>
            queryByLastName(new { LastName = lastName });
    }
}

public record Employee(Guid Id, string LastName);

public record ConnectionString(string Value)
{
    public static implicit operator string(ConnectionString c) => c.Value;
    public static implicit operator ConnectionString(string s) => new(s);
}

public record SqlTemplate(string Value)
{
    public static implicit operator string(SqlTemplate c) => c.Value;
    public static implicit operator SqlTemplate(string s) => new(s);
}

public static class ConnectionStringExt
{
    // Retrieve<T>: (ConnectionString, SqlTemplate) -> object -> IEnumerable<T>
    public static Func<object, IEnumerable<T>> Retrieve<T>(this ConnectionString connStr, SqlTemplate sql) =>
        param => ConnectionHelper.Connect(connStr, conn => conn.Query<T>(sql, param));
    // connStr and sql are available on application startup
    // param change with each query
}

public static class ConnectionHelper
{
    public static R Connect<R>(ConnectionString connString, Func<SqlConnection, R> f)
    {
        using var conn = new SqlConnection(connString);
        conn.Open();
        return f(conn);
    }
}
