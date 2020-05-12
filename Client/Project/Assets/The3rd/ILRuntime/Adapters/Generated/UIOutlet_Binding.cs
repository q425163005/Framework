using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class UIOutlet_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::UIOutlet);

            field = type.GetField("OutletInfos", flag);
            app.RegisterCLRFieldGetter(field, get_OutletInfos_0);
            app.RegisterCLRFieldSetter(field, set_OutletInfos_0);


        }



        static object get_OutletInfos_0(ref object o)
        {
            return ((global::UIOutlet)o).OutletInfos;
        }
        static void set_OutletInfos_0(ref object o, object v)
        {
            ((global::UIOutlet)o).OutletInfos = (System.Collections.Generic.List<global::UIOutlet.OutletInfo>)v;
        }


    }
}
