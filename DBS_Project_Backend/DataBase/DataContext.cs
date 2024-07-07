namespace DBS_Project_Backend.DataBase;

public class DataContext
{
    private readonly IConfiguration configuration;
    public DataContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string createConnection()
    {
        return configuration.GetConnectionString("Default");
    }
}
