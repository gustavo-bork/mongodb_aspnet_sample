namespace MongoDB_Test;

public class SchoolDatabaseSettings : ISchoolDatabaseSettings
{
    public string StudentsCollectionName { get; set; } = null!;
    public string CoursesCollectionName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

public interface ISchoolDatabaseSettings
{
    string StudentsCollectionName { get; set; }
    string CoursesCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
