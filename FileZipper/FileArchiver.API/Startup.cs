using FileArchiver.Domain.Context;
using FileArchiver.Domain.Repositories.Implementation;
using FileArchiver.Domain.Repositories.Interfaces;
using FileArchiver.Services.Implementation;
using FileArchiver.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileArchiver.API
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
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
                options.AutomaticAuthentication = true;
            });

            services.AddDbContext<FilesArchiveDBContext>
                (options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                        ServiceLifetime.Transient
                );
            services.AddScoped<IUsersService, UserService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IDocumentTypesService, DocumentTypesService>();
            services.AddScoped<IDocumentTypesRepository, DocumentTypesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IFilesRepository, FilesRepositroy>();
            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(co => co.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
