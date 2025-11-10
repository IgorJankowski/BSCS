using BSCS.Clients;
using BSCS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register API Client with HttpClient
builder.Services.AddHttpClient<ProductApiClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

// Register Services
builder.Services.AddScoped<IProductService, ProductService>();
// for the sake of demo it'll be a singleton instance
builder.Services.AddSingleton<ICartService, CartService>();

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

await app.RunAsync();
