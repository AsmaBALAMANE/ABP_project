using Microsoft.AspNetCore.Builder;
using MainApp;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("MainApp.Web.csproj"); 
await builder.RunAbpModuleAsync<MainAppWebTestModule>(applicationName: "MainApp.Web");

public partial class Program
{
}
