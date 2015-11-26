using Infra.Events.Dispatching;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Events
{    
    public class WrapperFactory
    {
        public static T Create<T>(T service)
        {
            var factory = new WrapperFactory(typeof(T));
            var type = factory.Emit();
            return (T)Activator.CreateInstance(type, service);
        }

        public static Type Emit(Type interfaceType)
        {
            var factory = new WrapperFactory(interfaceType);
            return factory.Emit();
        }

        public WrapperFactory(Type interfaceType)
        {
            Contract.Requires<ArgumentNullException>(interfaceType != null);
            Contract.Requires<ArgumentNullException>(interfaceType.IsInterface);
            ServiceInterface = interfaceType;
        }

        Type Emit()
        {
            Contract.Ensures(Contract.Result<Type>() != null);
            var tb = TypeBuilder();
            var fb = FieldBuilder(tb);

            DefineConstructor(tb, fb);
            DelegateTo(tb, fb);
            return tb.CreateType();
        }
                
        Type ServiceInterface { get; }

        void DefineConstructor(TypeBuilder tb, FieldBuilder fb)
        {
            Contract.Requires<ArgumentNullException>(tb != null);
            Contract.Requires<ArgumentNullException>(fb != null);

            ConstructorBuilder ctor = ConstructorBuilder(tb, fb);
            ILGenerator il = ctor.GetILGenerator();

            // Call base constructor
            ConstructorInfo ci = tb.BaseType.GetConstructor(new Type[] { });
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));

            // Store type parameters in private fields
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg, 1);
            il.Emit(OpCodes.Stfld, fb);

            il.Emit(OpCodes.Ret);
        }

        void DelegateTo(TypeBuilder tb, FieldInfo fi)
        {
            Contract.Requires<ArgumentNullException>(tb != null);
            Contract.Requires<ArgumentNullException>(fi != null);

            foreach (var mi in
                fi.FieldType.GetMethods().Concat(
                    fi.FieldType.GetInterfaces().SelectMany(i => i.GetMethods())))
            {
                var mb = tb.DefineMethod(
                    mi.Name,
                    mi.Attributes & (~MethodAttributes.Abstract), // Could not call absract method, so remove flag
                    mi.ReturnType,
                    mi.GetParameters().Select(p => p.ParameterType).ToArray());

                if (mi.IsGenericMethod)
                {
                    var gas = mi
                        .GetGenericArguments();

                    var gtpbs = mb.DefineGenericParameters(gas
                        .Select(t => t.Name)
                        .ToArray());

                    for (int i = 0; i < gas.Length; i++)
                    {
                        var ga = gas[i];
                        var gtpb = gtpbs[i];
                        gtpb.SetGenericParameterAttributes(ga.GenericParameterAttributes);                        
                        gtpb.SetInterfaceConstraints(ga.GetGenericParameterConstraints());                        
                    }
                }       
                    
                mb.SetReturnType(mi.ReturnType);                
                
                // Emit method body
                ILGenerator il = mb.GetILGenerator();
                                
                 // .locals
                var scopeType = typeof(EventScope);
                var exceptionType = typeof(Exception);
                var scope = il.DeclareLocal(scopeType);
                var exception = il.DeclareLocal(exceptionType);
                il.Emit(OpCodes.Newobj, scopeType.GetConstructor(new Type[0]));
                il.Emit(OpCodes.Stloc, scope);

                // try {
                il.BeginExceptionBlock();

                // Call with same parameters
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fi);
                for (int i = 0; i < mi.GetParameters().Length; i++)
                    il.Emit(OpCodes.Ldarg, i + 1);
                il.Emit(OpCodes.Callvirt, mi);

                // Call s.Complete();
                var complete = scopeType.GetMethod("Complete");
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldloc, scope);
                il.Emit(OpCodes.Callvirt, complete);
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Nop);

                // Return
                il.Emit(OpCodes.Ret);

                // } catch {
                il.BeginCatchBlock(typeof(Exception));

                // Call s.Fail(ex);
                var fail = scopeType.GetMethod("Fail");
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldloc, scope);
                il.Emit(OpCodes.Ldloc_1);
                il.Emit(OpCodes.Callvirt, fail);
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Rethrow);

                // } // catch
                il.EndExceptionBlock();

                // return
                il.Emit(OpCodes.Ret);
            }
        }

        ConstructorBuilder ConstructorBuilder(TypeBuilder tb, FieldBuilder fb) =>
            tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new[] { fb.FieldType });

        FieldBuilder FieldBuilder(TypeBuilder tb) => tb
            .DefineField("_s", ServiceInterface, FieldAttributes.Private);

        TypeBuilder TypeBuilder() => ModuleBuilder()
            .DefineType(
                Guid.NewGuid().ToString(),
                TypeAttributes.Class | TypeAttributes.Public,
                typeof(object),
                new[] { ServiceInterface });
        
        ModuleBuilder ModuleBuilder() => AssemblyBuilder()
            .DefineDynamicModule(Guid.NewGuid().ToString());

        AssemblyBuilder AssemblyBuilder() => AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(
                    Guid.NewGuid().ToString()),
                    AssemblyBuilderAccess.RunAndSave);
    }
}
