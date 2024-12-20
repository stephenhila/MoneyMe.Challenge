using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.Mappings;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Data;

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

        app.MapControllerRoute(
            name: "PostLoanApplication",
            pattern: "{controller=Loans}/{action=PostLoanApplication}");

        app.MapControllerRoute(
            name: "LoanApplicationDetails",
            pattern: "{controller=Loans}/{action=LoanApplicationDetails}/{id?}");

        app.Run();
    }
}
