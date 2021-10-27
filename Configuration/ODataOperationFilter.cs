using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookStore.Configuration
{
    public class ODataOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var descriptor = context.ApiDescription;

            // Include params something in routes that start with 'api/book'
            if (descriptor != null && descriptor.RelativePath.StartsWith("api/Book"))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "$filter",
                    Description = "Filter the results using OData syntax",
                    In = ParameterLocation.Query,
                    Required = false,
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "$select",
                    Description = "Select the attributes to return using OData syntax",
                    In = ParameterLocation.Query,
                    Required = false,
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "$count",
                    Description = "Filter the number of the count results using OData syntax",
                    In = ParameterLocation.Query,
                    Required = false,
                });
            }
        }
    }
}
