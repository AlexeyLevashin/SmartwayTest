using DataAccess.Dapper.Interfaces;

namespace DataAccess.Dapper;

public class QueryObject : IQueryObject
{
    public QueryObject(string sql, object? parameters = null)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentException("sql is missing");
        }

        Sql = sql;
        Parameters = parameters;
    }
    
    public string Sql { get; set; }
    public object? Parameters { get; set; }
}