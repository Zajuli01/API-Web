using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Repositories;
using API_Web.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();


builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
    smtpServer: builder.Configuration["Enail:SmtpServer"],
    smtpPort: int.Parse(builder.Configuration["Email:SmtpPort"]),
    fromEmailAddress: builder.Configuration["Email:FromEmailAddress"]
    ));

builder.Services.AddScoped<ITokenService, TokenService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
