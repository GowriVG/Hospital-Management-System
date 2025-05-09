using Microsoft.EntityFrameworkCore;
using HospitalManagement.Data;
using HospitalManagement.Managers;
using HospitalManagement.Managers.Managers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDB")));

builder.Services.AddScoped<IPatientManager, PatientManager>();

builder.Services.AddScoped<IDoctorManager, DoctorManager>();

builder.Services.AddScoped<IAppointmentScheduler, AppointmentSchedulerManager>();

builder.Services.AddScoped<IMedicalRecordManager, MedicalRecordManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Use CORS middleware
app.UseCors("AllowAll");






app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
