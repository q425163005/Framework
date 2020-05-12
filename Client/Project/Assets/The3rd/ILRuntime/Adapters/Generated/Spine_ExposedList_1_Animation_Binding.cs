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
    unsafe class Spine_ExposedList_1_Animation_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(Spine.ExposedList<Spine.Animation>);

            field = type.GetField("Items", flag);
            app.RegisterCLRFieldGetter(field, get_Items_0);
            app.RegisterCLRFieldSetter(field, set_Items_0);


        }



        static object get_Items_0(ref object o)
        {
            return ((Spine.ExposedList<Spine.Animation>)o).Items;
        }
        static void set_Items_0(ref object o, object v)
        {
            ((Spine.ExposedList<Spine.Animation>)o).Items = (Spine.Animation[])v;
        }


    }
}
