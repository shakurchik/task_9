using Microsoft.EntityFrameworkCore;
using Task9.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PrescriptionContext>(options =>
    options.UseSqlite("Data Source=prescriptions.db"));
builder.Services.AddControllers();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PrescriptionContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();