using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    /*
     * 一个典型的 Entity Framework Core DbContext 实现，用于与数据库进行交互。
     * AppDbContext 类继承自 DbContext，并定义了一个 DbSet<Employee> 属性，用于表示数据库中的 Employee 表。
     * 
     * 逐步解析这段代码：
     * 
     * public class AppDbContext : DbContext
     * 定义了一个名为 AppDbContext 的类，它继承自 DbContext。
     * DbContext 是 Entity Framework Core 的核心类，用于表示数据库上下文，它管理数据库连接、事务和缓存等。
     * 
     * 
     * public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     * AppDbContext 类的构造函数。
     * 它接受一个 DbContextOptions<AppDbContext> 类型的参数 options，并将其传递给基类 DbContext 的构造函数。
     * DbContextOptions<AppDbContext> 包含了配置 AppDbContext 所需的所有选项，如数据库连接字符串、数据库提供程序等。
     * 
     * 
     * public DbSet<Employee> Employees { get; set; }
     * 定义了一个 DbSet<Employee> 类型的属性 Employees。
     * DbSet<TEntity> 是 Entity Framework Core 中表示数据库表的一个集合。
     * Employees 属性对应于数据库中的 Employee 表。通过 Employees 属性，你可以执行查询、添加、删除和更新操作。
     * 
     * 
     * 为了使用这个 AppDbContext 类，你需要在 ASP.NET Core 应用的启动配置中注册它，并配置数据库连接
    */
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

    }
}
