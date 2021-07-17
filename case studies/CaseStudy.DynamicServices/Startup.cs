using CaseStudy.DynamicService.SDK;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CaseStudy.DynamicServices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.LoadModularServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CaseStudy.DynamicServices", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaseStudy.DynamicServices v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ModularServiceExtensions
    {

        public static IServiceCollection LoadModularServices(this IServiceCollection services)
        {
            var localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dlls = Directory.GetFiles(localPath, "*.dll");
            var loaded = AssemblyLoader.GetLoadedAssemblies();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => Path.GetDirectoryName(a.Location) == localPath)
                .ToDictionary(a => a.Location);

            foreach (var a in assemblies.Values)
            {
                foreach (var t in a.GetExportedTypes())
                {
                    var attr = t.GetCustomAttribute<ServiceAttribute>();
                    if (attr != null)
                    {
                        if (attr is SingletonServiceAttribute) services.AddSingleton(attr.ServiceType, t);
                        if (attr is TransientServiceAttribute) services.AddTransient(attr.ServiceType, t);
                        if (attr is ScopedServiceAttribute) services.AddScoped(attr.ServiceType, t);
                    }
                }
            }

            return services;
        }
    }

    internal class AssemblyLoader
    {
        private static List<Assembly> _loadedAssemblies = new();

        public static List<Assembly> GetLoadedAssemblies(params string[] scanAssembliesStartsWith)
        {
            if (_loadedAssemblies.Any())
            {
                return _loadedAssemblies;
            }

            LoadAssemblies(scanAssembliesStartsWith);
            return _loadedAssemblies;
        }

        private static void LoadAssemblies(params string[] scanAssembliesStartsWith)
        {
            HashSet<Assembly> loadedAssemblies = new();
            List<string> assembliesToBeLoaded = new();
            string appDllsDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (scanAssembliesStartsWith?.Any() == true)
            {
                if (scanAssembliesStartsWith.Length == 1)
                {
                    var searchPattern = $"{scanAssembliesStartsWith.First()}*.dll";
                    var assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                    assembliesToBeLoaded.AddRange(assemblyPaths);
                }

                if (scanAssembliesStartsWith.Length > 1)
                {
                    foreach (string starsWith in scanAssembliesStartsWith)
                    {
                        var searchPattern = $"{starsWith}*.dll";
                        var assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                        assembliesToBeLoaded.AddRange(assemblyPaths);
                    }
                }
            }
            else
            {
                var assemblyPaths = Directory.GetFiles(appDllsDirectory, "*.dll");
                assembliesToBeLoaded.AddRange(assemblyPaths);
            }

            foreach (var path in assembliesToBeLoaded)
            {
                try
                {
                    loadedAssemblies.Add(Assembly.LoadFrom(path));
                }
                catch (Exception)
                {
                    continue;
                }
            }

            _loadedAssemblies = loadedAssemblies.ToList();
        }
    }
}