using Microsoft.Extensions.Options;
using VRBackend;
using VRBackend.Hubs;
using VRBackend.Services;

System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
System.Net.ServicePointManager.CheckCertificateRevocationList = false;

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

// Configure CORS to allow requests from your React app.
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy before routing.
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<QuizHub>("/quizHub");
    endpoints.MapHub<ChatHub>("/chathub");
});

app.Run();
