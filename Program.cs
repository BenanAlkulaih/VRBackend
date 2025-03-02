using Microsoft.Extensions.Options;
using VRBackend;
using VRBackend.Hubs;
using VRBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<QuizDatabaseSettings>(
    builder.Configuration.GetSection(nameof(QuizDatabaseSettings)));
builder.Services.AddSingleton<IQuizDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<QuizDatabaseSettings>>().Value);
builder.Services.AddSingleton<QuestionService>();

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<QuizHub>("/quizHub");
});

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
