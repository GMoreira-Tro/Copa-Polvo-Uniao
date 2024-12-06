using CRUDAPI.Models;
using CRUDAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CRUDAPI
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
            string? connectionString = Configuration.GetConnectionString("ConexaoBD");
            services.AddDbContext<Contexto>(opcoes => opcoes.UseSqlServer(connectionString));

            services.AddHttpClient();
            services.AddScoped<AtletaService>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<ConfrontoInscricaoService>();
            services.AddScoped<ConfrontoService>();
            services.AddScoped<ContaCorrenteService>();
            services.AddScoped<InscricaoService>();
            services.AddScoped<ModalidadeService>();
            services.AddScoped<NotificacaoService>();
            services.AddScoped<PagamentoContaCorrenteService>();
            services.AddScoped<PagamentoService>();
            services.AddScoped<PremioService>();
            services.AddScoped<TimeService>();
            services.AddScoped<UsuarioNotificacaoService>();
            services.AddScoped<UsuarioService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Contexto>();

                // Inicializa o banco com dados de exemplo
                DbInitializer.InicializarBancoDados(context);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });
        }
    }
}