using BookStore.Configuration;
using BookStore.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>((options) =>
            {
                options.UseInMemoryDatabase("BookOData");
                options.EnableSensitiveDataLogging();
            });
            services.AddControllers().AddNewtonsoftJson();
            services.AddOData();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
                c.OperationFilter<ODataOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.MapODataRoute("api", "api", ODataConfiguration.GetEdmModel());
                endpoints.Select().Filter().Count().OrderBy().MaxTop(100);
                endpoints.MapControllers();
            });

            SeedData(app);
        }

        private void SeedData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    ISBN = "978-0-321-87758-1",
                    Title = "Essential C#5.0",
                    Author = "Rodrigo Bento",
                    Price = 59.99m,
                    Press = new Press
                    {
                        Id = 1,
                        Name = "Addison-Wesley",
                        Category = Category.Book
                    }
                },
                new Book
                {
                    Id = 2,
                    ISBN = "063-6-920-02371-5",
                    Title = "Enterprise Games",
                    Author = "Francisco Bento",
                    Price = 49.99m,
                    Press = new Press
                    {
                        Id = 2,
                        Name = "Addison-Wesley PDF",
                        Category = Category.EBook
                    }
                }
            };
            dbContext.Books.AddRange(books);
            dbContext.SaveChanges();
        }

    }
}
