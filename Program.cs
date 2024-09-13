using ClientsContactsProj.Data;
using ClientsContactsProj.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter=true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))); 
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ContactService>();
    
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


//not to return 0
using (var scope = app.Services.CreateScope()){
    var db=scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
}

//minimal APIs
app.MapGet("/clients", async (AppDbContext _context) =>{
    return await _context.Clients.ToArrayAsync();
});

app.MapGet("/clients/{id}", async (int id, AppDbContext _context) => {
      var client=await _context.Clients.FindAsync(id);
            if(client==null){
                return Results.NotFound();
            }
            return Results.Ok(client);
});

app.Run();
