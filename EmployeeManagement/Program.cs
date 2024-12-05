using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//如何配置 mvc 的 Service
builder.Services.Configure<MvcOptions>(options => options.EnableEndpointRouting = false);
builder.Services.AddMvc();
//AddMvc（）内部 calls AddMvcCore() 
//builder.Services.AddMvcCore();


var app = builder.Build();
#region 2024/12/05 20:40 如何配置异常界面
//必须尽早插入
if (builder.Environment.IsEnvironment("ss"))
    if (builder.Environment.IsDevelopment())
    {
        DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
        {
            SourceCodeLineCount = 3,
        };
        app.UseDeveloperExceptionPage(developerExceptionPageOptions);
    }
#endregion

#region 2024/12/05 21:00 如何导入静态资源
//使用静态资源文件
//必须先使用默认
/*
 * 如何添加 wwwroot 本地资源文件；
 * 1、新建文件夹 wwwroot ；
 * 2、项目 .csproj 中添加引用  
 *  <ItemGroup>
 *      <Folder Include="wwwroot\css\" />
 *      <Folder Include="wwwroot\js\" />
 *  </ItemGroup>
 *  
 *  3、添加静态资源文件
 */
/*默认启动页面 default.html
app.UseDefaultFiles() 的作用：
    当请求的是一个目录时，UseDefaultFiles 中间件会检查该目录中是否存在默认文件（如 index.html）。
    如果找到默认文件，UseDefaultFiles 会将请求重定向到该默认文件。注意，这里只是进行了重定向，并没有实际发送文件内容。
app.UseStaticFiles() 的作用：
    UseStaticFiles 中间件负责提供静态文件的内容给客户端。
    当请求的是一个具体的文件路径时，UseStaticFiles 会查找并返回该文件的内容。
为什么顺序重要：
    如果你先配置了 app.UseStaticFiles()，再配置 app.UseDefaultFiles()，
    那么当请求一个目录时，UseStaticFiles 会首先尝试处理该请求。
    由于目录中通常不包含名为该目录名的文件，因此 UseStaticFiles 不会找到任何内容来响应请求，导致返回 404 错误。

    相反，如果你先配置了 app.UseDefaultFiles()，
    它会检查目录中是否存在默认文件，并将请求重定向到该默认文件。
    随后，当请求被重定向到默认文件时，UseStaticFiles 能够正确地找到并返回该文件的内容。
*/
//app.UseDefaultFiles();
//app.UseStaticFiles();
#endregion

#region 2024/12/05 21:10 如何配置默认启动页 UseDefaultFiles
//DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
//defaultFilesOptions.DefaultFileNames.Clear();
//defaultFilesOptions.DefaultFileNames.Add("foo.html");
//app.UseDefaultFiles(defaultFilesOptions);
//app.UseStaticFiles();
#endregion

#region 2024/12/05 21:15 如何配置默认启动页 UseFileServer
FileServerOptions fileServerOptions = new FileServerOptions();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("abc.html");
app.UseFileServer(fileServerOptions);


//若此时输入其他Index,则默认转向下面的中间件
app.UseMvcWithDefaultRoute();


//由于本地存在 foo.html ,所以 pipeline 到此处终止，以下中间件不再运行
//因为 UseFileServer 中间件在成功处理请求后，
//通常会将 HttpContext.Response.HasStarted 设置为 true，
//表示响应已经开始发送，从而阻止后续中间件的处理。
//这是 HTTP 请求处理的一个基本规则：一旦响应开始发送，就不能再修改响应头或状态码，因此后续中间件也没有必要再运行

#endregion
//app.MapGet("/", () => "Hello World!");

#region 2024/12/05 20:50 如何创建中间件
//1、 抛出异常前，不能优先返回 await context.Response.WriteAsync("Hello One"); 会导致界面异常显示失败；
//2、 中间件2的异常被1捕获后，不能再被界面捕获；
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello One");
//    await next();
//});
app.Use(async (context, next) =>
{
    app.Logger.LogInformation("Middleware 1 started");
    app.Logger.LogInformation("Middleware 1 finished writing response");
    await next();
});
app.Use(async (context, next) =>
{
    throw new Exception("An error occurred in Middleware 2");
    await next();
});
#endregion

app.Run();
