using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Mappings;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Data;
using MoneyMe.Challenge.Web.UI.Services;
using Refit;

namespace MoneyMe.Challenge.Web.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetLoanApplicationQuery).Assembly));
        builder.Services.AddDbContext<LoanContext>(options => options.UseInMemoryDatabase("LoanDatabase"));
        builder.Services.AddScoped<ILoanContext, LoanContext>();
        builder.Services.AddAutoMapper(typeof(LoanApplicationMappingProfile));
        builder.Services.AddRefitClient<ILoanApplicationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]));

        builder.Services.AddSession();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.MapRazorPages();

        app.UseSession();

        app.Run();
    }
}
