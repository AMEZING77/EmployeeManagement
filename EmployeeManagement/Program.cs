using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//������� mvc �� Service
builder.Services.Configure<MvcOptions>(options => options.EnableEndpointRouting = false);
builder.Services.AddMvc();
//AddMvc�����ڲ� calls AddMvcCore() 
//builder.Services.AddMvcCore();


var app = builder.Build();
#region 2024/12/05 20:40 ��������쳣����
//���뾡�����
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
fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("abc.html");
app.UseFileServer(fileServerOptions);


//����ʱ��������Index,��Ĭ��ת��������м��
app.UseMvcWithDefaultRoute();


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
