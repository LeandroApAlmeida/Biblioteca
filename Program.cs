using DinkToPdf;
using DinkToPdf.Contracts;
using Library.Data;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DiscardService;
using Library.Services.DonationService;
using Library.Services.LoanService;
using Library.Services.LoginService;
using Library.Services.LogService;
using Library.Services.PasswordService;
using Library.Services.PersonService;
using Library.Services.ReportService;
using Library.Services.SessionService;
using Library.Services.SettingsService;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);


new CustomAssemblyLoadContext().LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


builder.Services.AddTransient<Argon2Params>(provider => {
    return new Argon2Reader(Path.Combine(Directory.GetCurrentDirectory(), "argon2.xml")).Read(16, 16, 4);
});


builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<ICollectionService, CollectionService>();

builder.Services.AddScoped<IDiscardService, DiscardService>();

builder.Services.AddScoped<IDonationService, DonationService>();

builder.Services.AddScoped<ILoanService, LoanService>();

builder.Services.AddScoped<IPersonService, PersonService>();

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordService, Argon2Service>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<IPdfReportService, PdfReportService>();

builder.Services.AddScoped<IHtmlReportService, HtmlReportService>();

builder.Services.AddScoped<ISettingsService, SettingsService>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}"
);

app.Run();