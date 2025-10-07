using DinkToPdf;
using DinkToPdf.Contracts;
using Library.Data;
using Library.Services.Authentication;
using Library.Services.Collection;
using Library.Services.Report;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        //builder.Configuration.GetConnectionString("TestConnection"),
        b => b.MigrationsAssembly("Library")
    )

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

app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Outros middlewares...
app.UseRouting();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();