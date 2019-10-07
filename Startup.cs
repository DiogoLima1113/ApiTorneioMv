using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Classe;
using api_torneio_mv.Dominio.Repositorio.Interface;
using api_torneio_mv.Servico;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api_torneio_mv
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            ConnectionProvider connectionProvider = new ConnectionProvider(Configuration);

            Func<IServiceProvider, ConnectionProvider> spConnectionProvider = _ => connectionProvider;

            services.AddScoped<IConnectionProvider, ConnectionProvider>(spConnectionProvider);
            services.AddScoped<IDbRepositorio<Jogo>, DbRepositorioJogo>();
            services.AddScoped<IDbRepositorio<Jogador>, DbRepositorioJogador>();
            services.AddScoped<IDbRepositorio<RelacionamentoTimeJogador>, DbRepositorioRelacionamentoTimeJogador>();
            services.AddScoped<IDbRepositorio<Time>, DbRepositorioTime>();
            services.AddScoped<IMeusServicos, MeusServicos>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
