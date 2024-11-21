using Api.Service;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez votre DbContext avant de construire l'application
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ConnectionDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConnectionDB"))
));


builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddScoped<ICommentService, CommentService>();

/* Add authentication directly by Azure AD on cloud.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
    .EnableTokenAcquisitionToCallDownstreamApi()

builder.Services.AddAuthorization();
*/
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();



// Adding Authentication
var validAudience = builder.Configuration["JWT:ValidAudience"];
var validIssuer = builder.Configuration["JWT:ValidIssuer"];
var secret = builder.Configuration["JWT:Secret"];


var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


//app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
