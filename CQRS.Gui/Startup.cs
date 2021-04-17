using CQRS.Simple.Aggregates;
using CQRS.Simple.Commands;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Publishers;
using CQRS.Simple.Repositories;
using CQRS.Simple.Routers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            var messageRouter = new MessageRouter();
            services.AddSingleton<IRouter>(x => messageRouter);
            services.AddSingleton<ICommandDispatcher>(x => messageRouter);
            services.AddSingleton<IEventPublisher>(x => messageRouter);
            services.AddSingleton<IEventStore, EventStore>();
            services.AddSingleton<IEventSourcingHandler<InventoryItemAggregate>, EventSourcingHandler<InventoryItemAggregate>>();
            services.AddSingleton<ICommandHandler, InventoryCommandHandler>();
            services.AddSingleton<IEventHandler, EventHandler>();

            var provider = services.BuildServiceProvider();
            var commands = provider.GetRequiredService<ICommandHandler>();
            var router = provider.GetRequiredService<IRouter>();
            router.RegisterHandler<CheckInItemsCommand>(commands.Handle);
            router.RegisterHandler<CreateItemCommand>(commands.Handle);
            router.RegisterHandler<DeactivateItemCommand>(commands.Handle);
            router.RegisterHandler<RemoveItemsCommand>(commands.Handle);
            router.RegisterHandler<RenameItemCommand>(commands.Handle);

            var eventHandler = provider.GetRequiredService<IEventHandler>();
            router.RegisterHandler<ItemCreatedEvent>(eventHandler.Handle);
            router.RegisterHandler<ItemDeactivatedEvent>(eventHandler.Handle);
            router.RegisterHandler<ItemRenamedEvent>(eventHandler.Handle);
            router.RegisterHandler<ItemsCheckedInEvent>(eventHandler.Handle);
            router.RegisterHandler<ItemsRemovedEvent>(eventHandler.Handle);

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
