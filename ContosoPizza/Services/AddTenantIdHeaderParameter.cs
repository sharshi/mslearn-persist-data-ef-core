using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AddTenantIdHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "tenant_id",
            In = ParameterLocation.Header,
            Required = true, // set to false if this is not required
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}
