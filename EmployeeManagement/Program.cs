using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
{
    //ʹ�� AddDbContextPool ȷ������ pool �����е� AppDbContext
    builder.Services.AddDbContextPool<AppDbContext>(optionsAction: options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection"));
    });
    //������� mvc �� Service
    builder.Services.Configure<MvcOptions>(options => options.EnableEndpointRouting = false);
    builder.Services.AddMvc()/*.AddXmlDataContractSerializerFormatters()*/;
    //AddMvc�����ڲ� calls AddMvcCore() 
    //builder.Services.AddMvcCore();

    //AddSingleton  
    //builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
    builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
    builder.Services.AddScoped<ILogger, Logger<string>>();

    ////AddTransient
    //builder.Services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
    ////AddScoped
    //builder.Services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
}

var app = builder.Build();
#region 2024/12/05 20:40 ��������쳣����
//���뾡�����
//if (builder.Environment.IsEnvironment("ss"))
if (builder.Environment.IsDevelopment())
{
    DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
    {
        SourceCodeLineCount = 3,
    };
    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
}
else
{
    app.UseExceptionHandler("/Error");
    //app.UseStatusCodePages();
    //������ʹ�ã�ӦΪû����ʾ�������Ĵ��󷵻�ֵ
    //app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
#endregion

#region 2024/12/05 21:00 ��ε��뾲̬��Դ
//ʹ�þ�̬��Դ�ļ�
//������ʹ��Ĭ��
/*
 * ������ wwwroot ������Դ�ļ���
 * 1���½��ļ��� wwwroot ��
 * 2����Ŀ .csproj ���������  
 *  <ItemGroup>
 *      <Folder Include="wwwroot\css\" />
 *      <Folder Include="wwwroot\js\" />
 *  </ItemGroup>
 *  
 *  3����Ӿ�̬��Դ�ļ�
 */
/*Ĭ������ҳ�� default.html
app.UseDefaultFiles() �����ã�
    ���������һ��Ŀ¼ʱ��UseDefaultFiles �м�������Ŀ¼���Ƿ����Ĭ���ļ����� index.html����
    ����ҵ�Ĭ���ļ���UseDefaultFiles �Ὣ�����ض��򵽸�Ĭ���ļ���ע�⣬����ֻ�ǽ������ض��򣬲�û��ʵ�ʷ����ļ����ݡ�
app.UseStaticFiles() �����ã�
    UseStaticFiles �м�������ṩ��̬�ļ������ݸ��ͻ��ˡ�
    ���������һ��������ļ�·��ʱ��UseStaticFiles ����Ҳ����ظ��ļ������ݡ�
Ϊʲô˳����Ҫ��
    ������������� app.UseStaticFiles()�������� app.UseDefaultFiles()��
    ��ô������һ��Ŀ¼ʱ��UseStaticFiles �����ȳ��Դ��������
    ����Ŀ¼��ͨ����������Ϊ��Ŀ¼�����ļ������ UseStaticFiles �����ҵ��κ���������Ӧ���󣬵��·��� 404 ����

    �෴��������������� app.UseDefaultFiles()��
    ������Ŀ¼���Ƿ����Ĭ���ļ������������ض��򵽸�Ĭ���ļ���
    ��󣬵������ض���Ĭ���ļ�ʱ��UseStaticFiles �ܹ���ȷ���ҵ������ظ��ļ������ݡ�
*/
//app.UseDefaultFiles();
//app.UseStaticFiles();
#endregion

#region 2024/12/05 21:10 �������Ĭ������ҳ UseDefaultFiles
//DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
//defaultFilesOptions.DefaultFileNames.Clear();
//defaultFilesOptions.DefaultFileNames.Add("foo.html");
//app.UseDefaultFiles(defaultFilesOptions);
//app.UseStaticFiles();
#endregion

#region 2024/12/05 21:15 �������Ĭ������ҳ UseFileServer
FileServerOptions fileServerOptions = new FileServerOptions();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("123.html");
//app.UseFileServer(fileServerOptions);

//����ʱ��������Index,��Ĭ��ת��������м��
//app.UseMvcWithDefaultRoute();
//app.UseStaticFiles();
//ʹ���Զ���·��
app.UseMvc(routes =>
{
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
});

//ʹ�� Attribute Route
//app.UseMvc();



//���ڱ��ش��� foo.html ,���� pipeline ���˴���ֹ�������м����������
//��Ϊ UseFileServer �м���ڳɹ����������
//ͨ���Ὣ HttpContext.Response.HasStarted ����Ϊ true��
//��ʾ��Ӧ�Ѿ���ʼ���ͣ��Ӷ���ֹ�����м���Ĵ���
//���� HTTP �������һ����������һ����Ӧ��ʼ���ͣ��Ͳ������޸���Ӧͷ��״̬�룬��˺����м��Ҳû�б�Ҫ������

#endregion
//app.MapGet("/", () => "Hello World!");

#region 2024/12/05 20:50 ��δ����м��
//1�� �׳��쳣ǰ���������ȷ��� await context.Response.WriteAsync("Hello One"); �ᵼ�½����쳣��ʾʧ�ܣ�
//2�� �м��2���쳣��1����󣬲����ٱ����沶��
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Hello One");
//    await next();
//});
//app.Use(async (context, next) =>
//{
//    app.Logger.LogInformation("Middleware 1 started");
//    app.Logger.LogInformation("Middleware 1 finished writing response");
//    await next();
//});
//app.Use(async (context, next) =>
//{
//    throw new Exception("An error occurred in Middleware 2");
//    await next();
//});
#endregion

app.Run();
