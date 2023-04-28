using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Bookshelf.Api;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
        _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = $"Bookshelf API {apiVersion}",
                    Description = "Курсовой демонстрационный проект Bookshelf.Backend",
                    Contact = new OpenApiContact
                    {
                        Name = " Зверев Даниил",
                        Email = string.Empty,
                        Url = new Uri("https://t.me/zverevDaniil")
                    }
                });

            options.AddSecurityDefinition("OAuth2",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:7111/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:7111/connect/token"),
                        }
                    },
                    Type = SecuritySchemeType.OAuth2
                });

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "OAuth2",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string> { }
                    }
                });

            options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                    ? methodInfo.Name
                    : null);
        }
    }
}
