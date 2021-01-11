using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TestWeb
{
  public class FileUploadOperation : IOperationFilter
  {
    public void Apply(Operation operation, OperationFilterContext context)
    {
      foreach (IParameter parameter in operation.Parameters)
      {
        if (parameter is NonBodyParameter && (parameter as NonBodyParameter).Type == "object")
        {
          parameter.In = "formData";
          (parameter as NonBodyParameter).Type = "file";
        }
      }
    }
  }
}
