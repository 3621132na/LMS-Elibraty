using LMS_Elibraty.Data;
using LMS_Elibraty.Services.Answers;
using LMS_Elibraty.Services.Asks;
using LMS_Elibraty.Services.Classes;
using LMS_Elibraty.Services.Document;
using LMS_Elibraty.Services.Emails;
using LMS_Elibraty.Services.ExamDetails;
using LMS_Elibraty.Services.Exams;
using LMS_Elibraty.Services.Facultys;
using LMS_Elibraty.Services.Feedbacks;
using LMS_Elibraty.Services.File;
using LMS_Elibraty.Services.Lessons;
using LMS_Elibraty.Services.Roles;
using LMS_Elibraty.Services.Subjects;
using LMS_Elibraty.Services.System;
using LMS_Elibraty.Services.Topics;
using LMS_Elibraty.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Teacher", policy => policy.RequireRole("Teacher"));
    options.AddPolicy("Student", policy => policy.RequireRole("Student"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LMSElibraryContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IAskService, AskService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExamDetailService, ExamDetailService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISystemService, SystemService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();