using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace MainApp.Web.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Prioritize paths starting with "/todo"
            var sortedPaths = swaggerDoc.Paths
                .Where(x => !x.Key.ToLowerInvariant().StartsWith("/api/abp") //remove paths those start with /api/abp prefix
                         && !x.Key.ToLowerInvariant().Contains("tenant") //remove paths those contain tenant
                         && !x.Key.ToLowerInvariant().Contains("feature-management") //remove paths those contain feature-management
                         && !x.Key.ToLowerInvariant().Contains("setting-management")) //remove paths those contain settings-management
                .OrderBy(pair => !pair.Key.Contains("/todo")) // Prioritize "todo" paths
                .ThenBy(pair => pair.Key) // Sort alphabetically after priority
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            // Replace paths with sorted ones
            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var path in sortedPaths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }

        }
    }
}
