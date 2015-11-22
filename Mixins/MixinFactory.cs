using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    class MixinFactory
    {
        public MixinFactory(Type mixinInterface)
        {
            Contract.Requires<ArgumentNullException>(mixinInterface != null);
            Contract.Requires<ArgumentException>(mixinInterface.IsInterface);

            MixinInterface = mixinInterface;
        }

        public Type Emit()
        {
            Contract.Ensures(Contract.Result<Type>() != null);

            var tb = TypeBuilder();
            var fbs = FieldBuilders(tb);
            DefineConstructor(tb, fbs);
            foreach (var fb in fbs)
                DelegateTo(tb, fb);

            return tb.CreateType();
        }

        Type MixinInterface { get; }

        IEnumerable<Type> MixedInterfaces => MixinInterface.GetInterfaces();

        void DefineConstructor(TypeBuilder tb, IEnumerable<FieldBuilder> fbs)
        {
            Contract.Requires<ArgumentNullException>(tb != null);
            Contract.Requires<ArgumentNullException>(fbs != null);

            ConstructorBuilder ctor = ConstructorBuilder(tb, fbs);
            ILGenerator il = ctor.GetILGenerator();

            // Call base constructor
            ConstructorInfo ci = tb.BaseType.GetConstructor(new Type[] { });
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[0]));

            // Store type parameters in private fields
            for (ushort i = 0; i < fbs.Count(); i++)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg, i + 1);
                il.Emit(OpCodes.Stfld, fbs.ElementAt(i));
            }
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

        ConstructorBuilder ConstructorBuilder(TypeBuilder tb, IEnumerable<FieldBuilder> fbs) =>
            tb.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                fbs.Select(fb => fb.FieldType).ToArray());

        IEnumerable<FieldBuilder> FieldBuilders(TypeBuilder tb) => MixedInterfaces
            .Select((mi, i) => tb.DefineField(
                "_i" + i,
                mi,
                FieldAttributes.Private))
            .ToArray();

        TypeBuilder TypeBuilder() => ModuleBuilder()
            .DefineType(
                Guid.NewGuid().ToString(),
                TypeAttributes.Class | TypeAttributes.Public,
                typeof(object),
                new[] { MixinInterface });
        
        ModuleBuilder ModuleBuilder() => AssemblyBuilder()
            .DefineDynamicModule(Guid.NewGuid().ToString());

        AssemblyBuilder AssemblyBuilder() => AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(
                    Guid.NewGuid().ToString()),
                    AssemblyBuilderAccess.RunAndSave);
    }

}
