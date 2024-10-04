var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Configure HttpClient with SSL validation bypass for development.
builder.Services.AddHttpClient("FieverApiClient", client =>
    {
        // Assuming the API is running locally with this URL. Update accordingly.
        client.BaseAddress = new Uri("http://localhost:5166");
    })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        // Bypass SSL validation ONLY for development.
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();