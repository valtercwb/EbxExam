using AspnetCore_EFCoreInMemory.Models;
using AutoMapper;
using EbanxExam.Application;
using EbanxExam.Application.Interface;
using EbanxExam.Domain.Interface;
using EbanxExam.Domain.Models;
using EbanxExam.Domain.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EbanxExam
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
      services.AddScoped<IAccountService, AccountService>();
      services.AddScoped<IAccountRepository, AccountRepository>();
      services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "EbanxExam"));
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "EbanxExam", Version = "v1" });
      });

      var config = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.CreateMap<Account, Application.Transients.Destination>();
        cfg.CreateMap<Account, Application.Transients.Origin>();
      });
      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EbanxExam v1"));
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
