using Autofac;
using Autofac.Builder;
using cqrs_review_windsor.Command;
using cqrs_review_windsor.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace cqrs_review_windsor.IoC
{
    public static class AutofacRegisterCqrs
    {
        public static void AddCqrsAutofac(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGeneric(typeof(QueryFor<>)).As(typeof(IQueryFor<>));
            containerBuilder.RegisterTypedFactory<IQueryBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<IQueryFactory>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CommandBuilder>().As<ICommandBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<ICommandFactory>().InstancePerLifetimeScope();
        }
    }

    // !!!! TADOS https://tados.ru/ Extesion


    public abstract class TypedFactoryBase
    {
        protected readonly IComponentContext ComponentContext;

        protected TypedFactoryBase(IComponentContext componentContext)
        {
            ComponentContext = componentContext;
        }

        protected object Resolve(Type type)
        {
            return ComponentContext.Resolve(type);
        }
    }

    public static class TypedFactoryExtensions
    {
        private static readonly AssemblyBuilder AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("TypedFactoryAssembly"), AssemblyBuilderAccess.Run);
        private static readonly ModuleBuilder ModuleBuilder = AssemblyBuilder.DefineDynamicModule("Factories");
        private static readonly Type TypedFactoryBaseType = typeof(TypedFactoryBase);

        private static readonly ConstructorInfo TypedFactoryBaseConstructorInfo = TypedFactoryBaseType.GetMatchingConstructor(new[] { typeof(IComponentContext) });
        private static readonly FieldInfo ComponentContextFieldInfo = TypedFactoryBaseType
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .Single(x => x.Name == "ComponentContext");

        private static readonly MethodInfo ResolveMethodInfo = typeof(ResolutionExtensions)
            .GetMethods()
            .Single(x =>
                x.Name == "Resolve" &&
                x.IsGenericMethod &&
                x.GetParameters().Length == 1);

        // TODO : implement resolve with parameters
        // private static readonly MethodInfo ParameterizedResolveMethodInfo;



        public static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterTypedFactory<TFactoryService>(this ContainerBuilder builder)
            where TFactoryService : class
        {
            return builder.RegisterType(CreateTypedFactory<TFactoryService>()).As<TFactoryService>();
        }

        private static Type CreateTypedFactory<TFactoryService>()
            where TFactoryService : class
        {
            Type factoryServiceType = typeof(TFactoryService);

            if (!factoryServiceType.GetTypeInfo().IsInterface)
                throw new InvalidOperationException("Only interfaces are allowed to create typed factories");

            TypeBuilder typeBuilder = ModuleBuilder.DefineType($"{factoryServiceType.Name}_Implementation");
            typeBuilder.AddInterfaceImplementation(factoryServiceType);
            typeBuilder.SetParent(TypedFactoryBaseType);

            ConstructorBuilder constructorBuilder =
                typeBuilder.DefineConstructor(
                    MethodAttributes.Public |
                    MethodAttributes.HideBySig |
                    MethodAttributes.SpecialName |
                    MethodAttributes.RTSpecialName,
                    CallingConventions.Standard,
                    new[] { typeof(IComponentContext) });

            ILGenerator constructorGenerator = constructorBuilder.GetILGenerator();

            constructorGenerator.Emit(OpCodes.Ldarg_0);
            constructorGenerator.Emit(OpCodes.Ldarg_1);
            constructorGenerator.Emit(OpCodes.Call, TypedFactoryBaseConstructorInfo);
            constructorGenerator.Emit(OpCodes.Ret);

            foreach (MethodInfo methodInfo in factoryServiceType.GetMethods())
            {
                ParameterInfo[] parametersInfo = methodInfo.GetParameters();

                if (parametersInfo.Any())
                    throw new NotImplementedException("Can't pass parameters now");

                if (parametersInfo.Any(x => x.IsOut))
                    throw new InvalidOperationException("No out parameters are allowed");

                Type returnType = methodInfo.ReturnType;

                MethodBuilder newMethodInfo = typeBuilder.DefineMethod(methodInfo.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual, returnType, new Type[0]);

                if (returnType.GenericTypeArguments.Length > 0)
                {
                    string[] typeParameterNames = returnType.GenericTypeArguments.Select(x => x.Name).ToArray();
                    GenericTypeParameterBuilder[] genericArguments = newMethodInfo.DefineGenericParameters(typeParameterNames);

                    for (int index = 0; index < returnType.GenericTypeArguments.Length; index++)
                    {
                        Type sourceGenericType = returnType.GenericTypeArguments[index];

                        List<Type> interfaceConstraints = new List<Type>();

                        foreach (Type constraint in sourceGenericType.GetTypeInfo().GetGenericParameterConstraints())
                        {
                            if (constraint.GetTypeInfo().IsInterface)
                            {
                                interfaceConstraints.Add(constraint);
                            }
                            else
                            {
                                genericArguments[index].SetBaseTypeConstraint(constraint);
                            }
                        }

                        genericArguments[index].SetInterfaceConstraints(interfaceConstraints.ToArray());

                        GenericParameterAttributes attributes = GenericParameterAttributes.None;

                        foreach (
                            GenericParameterAttributes attribute in
                            Enum.GetValues(typeof(GenericParameterAttributes)).Cast<GenericParameterAttributes>())
                        {
                            if ((sourceGenericType.GetTypeInfo().GenericParameterAttributes & attribute) !=
                                GenericParameterAttributes.None)
                                attributes |= attribute;
                        }

                        genericArguments[index].SetGenericParameterAttributes(attributes);
                    }
                }

                ILGenerator newMethodGenerator = newMethodInfo.GetILGenerator();

                newMethodGenerator.Emit(OpCodes.Ldarg_0);
                newMethodGenerator.Emit(OpCodes.Ldfld, ComponentContextFieldInfo);
                newMethodGenerator.Emit(OpCodes.Call, ResolveMethodInfo.MakeGenericMethod(returnType));
                newMethodGenerator.Emit(OpCodes.Ret);

            }

            return typeBuilder.CreateTypeInfo().AsType();
        }
    }
}
