using BeerApiBackend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BeerApiBackend
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
            services.AddMvc();
            services.AddDbContext<BeerAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BeerAppDbContext")));
            services.AddTransient<Beer_contextDAL, Beer_contextDAL>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Beer Recipe API",
                    Version = "v1",
                    Description = "Backend API for Beer Recipe App",
                    Contact = new Contact
                    {
                        Name = "Jonathan Grimm",
                        Email = "jonathanscottgrimm@gmail.com"
                    },
                    License = new License
                    {
                        Name = "Jonathan Grimm. All rights reserved."
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("CorsPolicy");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beer Recipe API V1");
                // Sets swagger as the default startup page.
                c.RoutePrefix = string.Empty;
            });
        }
    }

}
