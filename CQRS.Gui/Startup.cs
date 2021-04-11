using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Simple.Aggregates;
using CQRS.Simple.Bus;
using CQRS.Simple.Commands;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Projections;
using CQRS.Simple.Publishers;
using CQRS.Simple.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CQRS.Gui
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
            var bus = new FakeBus();
            var storage = new EventStore(bus);
            var rep = new Repository<InventoryItemAggregate>(storage);

            var commands = new InventoryCommandHandlers(rep);
            bus.RegisterHandler<CheckInItemsCommand>(commands.Handle);
            bus.RegisterHandler<CreateItemCommand>(commands.Handle);
            bus.RegisterHandler<DeactivateItemCommand>(commands.Handle);
            bus.RegisterHandler<RemoveItemsCommand>(commands.Handle);
            bus.RegisterHandler<RenameItemCommand>(commands.Handle);

            var detail = new InventoryItemDetailView();
            bus.RegisterHandler<ItemCreatedEvent>(detail.Handle);
            bus.RegisterHandler<ItemDeactivatedEvent>(detail.Handle);
            bus.RegisterHandler<ItemRenamedEvent>(detail.Handle);
            bus.RegisterHandler<ItemsCheckedInEvent>(detail.Handle);
            bus.RegisterHandler<ItemsRemovedEvent>(detail.Handle);

            var list = new InventoryListView();
            bus.RegisterHandler<ItemCreatedEvent>(list.Handle);
            bus.RegisterHandler<ItemRenamedEvent>(list.Handle);
            bus.RegisterHandler<ItemDeactivatedEvent>(list.Handle);
            ServiceLocator.Bus = bus;

            services.AddSingleton<IReadModelFacade, ReadModelFacade>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
