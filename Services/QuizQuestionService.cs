using MongoDB.Driver;
using VRBackend.Models;

namespace VRBackend.Services;

public class QuestionService
{
    private readonly IMongoCollection<Question> _questions;

    public QuestionService(IQuizDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _questions = database.GetCollection<Question>(settings.QuestionsCollectionName);
    }

    // Get ALL
    public List<Question> Get() =>
        _questions.Find(question => true).ToList();

    // Get question by ID
    public Question Get(string id) =>
        _questions.Find(question => question.Id == id).FirstOrDefault();

    // Create
    public Question Create(Question question)
    {
        _questions.InsertOne(question);
        return question;
    }

    // Update 
    public void Update(string id, Question questionIn) =>
        _questions.ReplaceOne(question => question.Id == id, questionIn);

    // Delete 
    public void Remove(string id) =>
        _questions.DeleteOne(question => question.Id == id);
}
