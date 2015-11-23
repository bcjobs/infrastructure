using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    class LoggerFactory
    {
        public LoggerFactory(Type serviceType)
        {
            Contract.Requires<ArgumentNullException>(serviceType != null);
            Contract.Requires<ArgumentNullException>(serviceType.IsInterface);
            ServiceType = serviceType;
        }

        public Type Emit()
        {
            Contract.Ensures(Contract.Result<Type>() != null);

            var tb = TypeBuilder();
            var fb = FieldBuilder(tb);
            DefineConstructor(tb, fb);
            //foreach (var fb in fbs)
            //    DelegateTo(tb, fb);

            return tb.CreateType();
        }

        Type ServiceType { get; }

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

            foreach (var mi in fi.FieldType.GetMethods())
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
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, fi);

                // Call with same parameters
                for (int i = 0; i < mi.GetParameters().Length; i++)
                    il.Emit(OpCodes.Ldarg, i + 1);

                il.Emit(OpCodes.Callvirt, mi);
                il.Emit(OpCodes.Ret);
            }
        }

        ConstructorBuilder ConstructorBuilder(TypeBuilder tb, FieldBuilder fb) =>
            tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new[] { fb.FieldType });

        FieldBuilder FieldBuilder(TypeBuilder tb) => tb
            .DefineField("_i", ServiceType, FieldAttributes.Private);

        TypeBuilder TypeBuilder() => ModuleBuilder()
            .DefineType(
                Guid.NewGuid().ToString(),
                TypeAttributes.Class | TypeAttributes.Public,
                typeof(object),
                new[] { ServiceType });
        
        ModuleBuilder ModuleBuilder() => AssemblyBuilder()
            .DefineDynamicModule(Guid.NewGuid().ToString());

        AssemblyBuilder AssemblyBuilder() => AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(
                    Guid.NewGuid().ToString()),
                    AssemblyBuilderAccess.RunAndSave);
    }
}
