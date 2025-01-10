using DinkToPdf;
using DinkToPdf.Contracts;
using Library.Data;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DiscardService;
using Library.Services.DonationService;
using Library.Services.LoanService;
using Library.Services.PersonService;
using Library.Services.ReportService;
using Library.Services.SessionService;
using Library.Services.UserService;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


new CustomAssemblyLoadContext().LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<ReportService>();


builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<ICollectionService, CollectionService>();

builder.Services.AddScoped<IDiscardService, DiscardService>();

builder.Services.AddScoped<IDonationService, DonationService>();

builder.Services.AddScoped<ILoanService, LoanService>();

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Details}/{id?}");

app.Run();