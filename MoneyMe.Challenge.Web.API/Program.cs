using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.Mappings;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SaveLoanApplicationCommand).Assembly));
        builder.Services.AddDbContext<LoanContext>(options => options.UseInMemoryDatabase("LoanDatabase"));
        builder.Services.AddScoped<ILoanContext, LoanContext>();
        builder.Services.AddAutoMapper(typeof(LoanApplicationMappingProfile));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                policy => policy.WithOrigins(builder.Configuration["FrontEndBaseUrl"])
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
