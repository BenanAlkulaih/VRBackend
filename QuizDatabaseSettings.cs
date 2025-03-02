namespace VRBackend
{
    public class QuizDatabaseSettings : IQuizDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string QuestionsCollectionName { get; set; } = null!;
    }

}
