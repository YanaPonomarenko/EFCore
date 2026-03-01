using Academy.Context;
using Academy.Group.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Academy;

internal class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<AcademyDbContext>(options =>
        {
            DbContextConfigurator.Configure(options);
        });

        

        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AcademyDbContext>();

        if (context.Database.CanConnect())
            Console.WriteLine("Пiдключення до БД встановлено");
        else
            Console.WriteLine("Не вдалось підключитись до БД");
    }
}




    //{
    //    //
    //    var configuration = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json",optional: false)
    //        .Build();
    //    var optionsBuilder = new DbContextOptionsBuilder<AcademyDbContext>();
    //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
    //

    //using (var context = new AcademyDbContext(optionsBuilder.Options))
    //{
    //Group group = new Group();
    //group.Name = "PV522";
    //context.Groups.Add(group);
    //context.SaveChanges();
    //var gr1= context.Groups.FirstOrDefault(group => group.Id == 1);
    //Console.WriteLine(gr1.Name);

    //Student st1 = new Student();
    //st1.Name = "John";
    //st1.Surname = "Surname3";
    //st1.GroupId = 2;
    //context.Add(st1);
    //context.SaveChanges();

    //практика 25.02
    //    Group group = new Group();
    //    group.Name = "PV522";
    //    context.Groups.Add(group);
    //    context.SaveChanges();

    //    Student st1 = new Student();
    //    st1.Name = "Steve";
    //    st1.Surname = "Surname3";
    //    st1.GroupId = group.Id; 
    //    context.Add(st1);
    //    context.SaveChanges();

    //    var students = context.Students
    //        .Where(st => st.GroupId==group.Id)
    //        .Include(st => st.Group)
    //        .ToList();

    //    students.ForEach(st =>
    //    {
    //        Console.WriteLine(st.Name);
    //        Console.WriteLine(st.Group.Name);
    //    });
    //}

    //using(var context = new AcademyDbContext())
    //{
    //context.Database.EnsureCreated();
    //context.Database.EnsureDeleted();
    //var student = new Student();
    //Console.WriteLine("Enter name");
    //student.Name = Console.ReadLine();
    //Console.WriteLine("Enter surname");
    //student.Surname = Console.ReadLine();
    //context.Students.Add(student);
    //context.SaveChanges();
    //var student = context.Students.FirstOrDefault(st => st.Id == 1);
    //if(student!=null)
    //{
    //Console.WriteLine($"Name: {student.Name} {student.Surname}");
    //}
    //}
    //}
}
