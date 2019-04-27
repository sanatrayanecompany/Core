using DI03.Services;
using StructureMap;

namespace DI03
{
    class Program
    {
        static void Main(string[] args)
        {
            // تنظيمات اواليه برنامه كه فقط يكبار بايد در طول عمر برنامه انجام شود
            ObjectFactory.Initialize(x =>
            {
                //x.For<IEmailsService>().Use<EmailsService>();
                //x.For<IUsersService>().Singleton().Use<UsersService>();                                
                x.Scan(scan =>
                {
                    scan.AssemblyContainingType<IEmailsService>();
                    scan.WithDefaultConventions();
                });                
            });            

            //نمونه‌اي از نحوه استفاده از تزريق وابستگي‌هاي خودكار
            var emailsService1 = ObjectFactory.GetInstance<IEmailsService>();
            emailsService1.SendEmailToUser(userId: 1, subject: "Test1", body: "Hello!");

            var emailsService2 = ObjectFactory.GetInstance<IEmailsService>();
            emailsService2.SendEmailToUser(userId: 1, subject: "Test2", body: "Hello!");
        }
    }
}