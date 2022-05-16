using static Repositories.DependencyInjection;
using static Communication.DependencyInjection;
using static UseCases.DependencyInjection;
using static MandradeFrameworks.Logs.Configuration.LogsConfiguration;
using MandradeFrameworks.Mensagens.Configuration;
using MandradeFrameworks.Autenticacao.Configuration;
using MandradeFrameworks.Logs.Models;
using Microsoft.OpenApi.Models;

const string NOME_API = "Scaffold API";
const string VERSAO_API = "v1";
const string TABELA_LOGS = "Logs_Template";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cnf =>
    {
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
    }
);

string connectionStringLogs = builder.Configuration.GetConnectionString("Logs");
var configuracoesLogs = new SQLLogsConfiguration(connectionStringLogs, TABELA_LOGS);

builder.Services.AdicionarMensageria();
builder.Services.AdicionarAutenticacao();
// AdicionarLogsSQL(configuracoesLogs);

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
