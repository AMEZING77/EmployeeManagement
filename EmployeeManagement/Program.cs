using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//ʹ�þ�̬��Դ�ļ�
app.UseStaticFiles();

//app.MapGet("/", () => "Hello World!");


//�����м��
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello One");
    await next();
});
app.Use(async (context, next) =>
{
    app.Logger.LogInformation("Finish One");
    await context.Response.WriteAsync("Hello Two");
    app.Logger.LogInformation("Finish Two");
    await next();
});




app.Run();
