namespace VRBackend
{
    public interface IQuizDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string QuestionsCollectionName { get; set; }
    }
}