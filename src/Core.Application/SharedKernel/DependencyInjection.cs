using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.SharedKernel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // TODO: Use SimpleInjector as  IoC container.
            //services.AddTransient(typeof(IValidator<>), typeof(AbstractValidator<>));
            // services.AddTransient(
            //     typeof(IValidator<GetCustomerQuery>), 
            //     typeof(GetCustomerQueryValidator));
            // services.AddTransient(
            //     typeof(
            //         IValidator<CreateCustomerCommand>), 
            //     typeof(CreateCustomerCommandValidator));
            // services.AddTransient(
            //     typeof(
            //         IValidator<EditCustomerDetailsCommand>), 
            //     typeof(
            //         EditCustomerDetailsCommandValidator));
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(
                    ValidationBehavior<,>));
            // services.AddTransient(
            //     typeof(IValidator<CreateVirtualMachineCommand>), 
            //     typeof(CreateVirtualMachineCommandValidator));
            // services.AddTransient(typeof(IValidator<DeleteVirtualMachineCommand>),
            //     typeof(DeleteVirtualMachineCommandValidator));
            // services.AddTransient(typeof(IValidator<CreateRentalCommand>),
            //     typeof(CreateRentalCommandValidator));
            // services.AddTransient(typeof(IValidator<ListRentalsQuery>),
            //     typeof(ListRentalsQueryValidator));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        // public static void RegisterApplication(Container container)
        // {
        //     var assemblies = GetAssemblies().ToArray();
        //     
        //     container.Collection.Register(typeof(IValidator<>), assemblies);
        //     container.RegisterSingleton<IMediator, Mediator>();
        //     container.Register(typeof(IRequestHandler<,>), Assembly.GetExecutingAssembly);
        //     RegisterHandlers(container, typeof(INotificationHandler<>), assemblies);
        //     RegisterHandlers(container, typeof(IRequestExceptionAction<,>), assemblies);
        //     RegisterHandlers(container, typeof(IRequestExceptionHandler<,,>), assemblies);
        //
        //     //Pipeline
        //     container.Collection.Register(typeof(IPipelineBehavior<,>), new []
        //     {
        //         typeof(RequestExceptionProcessorBehavior<,>),
        //         typeof(RequestExceptionActionProcessorBehavior<,>),
        //         typeof(RequestPreProcessorBehavior<,>),
        //         typeof(RequestPostProcessorBehavior<,>)
        //     });
        //     
        //     container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
        // }
        // private static void RegisterHandlers(Container container, Type collectionType, Assembly[] assemblies)
        // {
        //     // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
        //     var handlerTypes = container.GetTypesToRegister(collectionType, assemblies, new TypesToRegisterOptions
        //     {
        //         IncludeGenericTypeDefinitions = true,
        //         IncludeComposites = false,
        //     });
        //
        //     container.Collection.Register(collectionType, handlerTypes);
        // }
        //
        // private static IEnumerable<Assembly> GetAssemblies()
        // {
        //     yield return typeof(IMediator).GetTypeInfo().Assembly;
        // }

    }
}