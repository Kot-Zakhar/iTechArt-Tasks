using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.Faker;

namespace ShareMe.WebApplication
{
    public class Program
    {
        public static void InitDb()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (!unitOfWork.UserRepository.GetAll().Any())
                {
                    DbFaker.Reset();
                    DbFaker.Categories = unitOfWork.CategoryRepository.GetAll().ToList();
                    DbFaker.Tags = unitOfWork.TagRepository.GetAll().ToList();
                    DbFaker.Generate();

                    unitOfWork.UserRepository.CreateRange(DbFaker.Users);
                    unitOfWork.CategoryRepository.CreateRange(DbFaker.Categories);
                    unitOfWork.PostRepository.CreateRange(DbFaker.Posts);
                    unitOfWork.CommentRepository.CreateRange(DbFaker.Comments);
                    unitOfWork.TagRepository.CreateRange(DbFaker.Tags);

                    unitOfWork.Commit();
                }
            }
        }
        public static void Main(string[] args)
        {
            InitDb();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
