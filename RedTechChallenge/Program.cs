
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllMethods",
        builder => builder.WithOrigins("http://localhost:3000", "http://45.33.84.231").AllowAnyMethod().AllowAnyHeader());
});

// Add services to the container.
//add profiles and create mapping object
var mappingConfig = new MapperConfiguration(m => {
    m.AddProfile(new OrderProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySql(connectString, ServerVersion.AutoDetect(connectString));
}
);

builder.Services.AddScoped<OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAllMethods");
app.UseSwagger();
app.UseSwaggerUI();


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthorization();

app.MapControllers();

app.Run();
