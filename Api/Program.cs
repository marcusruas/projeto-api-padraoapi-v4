using static Repositories.DependencyInjection;
using static Communication.DependencyInjection;
using static UseCases.DependencyInjection;
using static MandradeFrameworks.Retornos.Configuration.RetornosConfiguration;
using MandradeFrameworks.Mensagens.Configuration;
using MandradeFrameworks.Autenticacao.Configuration;
using Microsoft.OpenApi.Models;

const string NOME_API = "Scaffold API";
const string VERSAO_API = "v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config => config.AdicionarConfiguracoes());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cnf => {
    cnf.SwaggerDoc(VERSAO_API, new OpenApiInfo { Version = VERSAO_API, Title = NOME_API });
    cnf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Header autenticacao via Json Web Tokens (JWT). insira abaixo o seu token da seguinte forma: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    cnf.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

builder.Services.AdicionarMensageria();
builder.Services.AdicionarAutenticacao();

builder.Services.AddUseCases();
builder.Services.AddCommunication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
