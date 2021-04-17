using CQRS.Simple.Aggregates;
using CQRS.Simple.Commands;
using CQRS.Simple.Events;
using CQRS.Simple.Handlers;
using CQRS.Simple.Projections;
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
            var storage = new EventStore(messageRouter);
            var rep = new Repository<InventoryItemAggregate>(storage);

            var commands = new InventoryCommandHandler(rep);
            messageRouter.RegisterHandler<CheckInItemsCommand>(commands.Handle);
            messageRouter.RegisterHandler<CreateItemCommand>(commands.Handle);
            messageRouter.RegisterHandler<DeactivateItemCommand>(commands.Handle);
            messageRouter.RegisterHandler<RemoveItemsCommand>(commands.Handle);
            messageRouter.RegisterHandler<RenameItemCommand>(commands.Handle);

            var detail = new InventoryItemDetailView();
            messageRouter.RegisterHandler<ItemCreatedEvent>(detail.Handle);
            messageRouter.RegisterHandler<ItemDeactivatedEvent>(detail.Handle);
            messageRouter.RegisterHandler<ItemRenamedEvent>(detail.Handle);
            messageRouter.RegisterHandler<ItemsCheckedInEvent>(detail.Handle);
            messageRouter.RegisterHandler<ItemsRemovedEvent>(detail.Handle);

            var list = new InventoryListView();
            messageRouter.RegisterHandler<ItemCreatedEvent>(list.Handle);
            messageRouter.RegisterHandler<ItemRenamedEvent>(list.Handle);
            messageRouter.RegisterHandler<ItemDeactivatedEvent>(list.Handle);
            ServiceLocator.MessageRouter = messageRouter;

            services.AddSingleton<ICommandDispatcher>(x => messageRouter);
            services.AddSingleton<IEventPublisher>(x => messageRouter);
            services.AddSingleton<IEventStore, EventStore>();
            services.AddSingleton<IRepository<InventoryItemAggregate>, Repository<InventoryItemAggregate>>();

            services.AddSingleton<IInventoryCommandHandler, InventoryCommandHandler>();
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
