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
    unsafe class CanvasAdaptive_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::CanvasAdaptive);

            field = type.GetField("HightScale", flag);
            app.RegisterCLRFieldGetter(field, get_HightScale_0);
            app.RegisterCLRFieldSetter(field, set_HightScale_0);
            field = type.GetField("CutoutsHeight", flag);
            app.RegisterCLRFieldGetter(field, get_CutoutsHeight_1);
            app.RegisterCLRFieldSetter(field, set_CutoutsHeight_1);
            field = type.GetField("CutoutsBottonHeight", flag);
            app.RegisterCLRFieldGetter(field, get_CutoutsBottonHeight_2);
            app.RegisterCLRFieldSetter(field, set_CutoutsBottonHeight_2);


        }



        static object get_HightScale_0(ref object o)
        {
            return ((global::CanvasAdaptive)o).HightScale;
        }
        static void set_HightScale_0(ref object o, object v)
        {
            ((global::CanvasAdaptive)o).HightScale = (System.Single)v;
        }
        static object get_CutoutsHeight_1(ref object o)
        {
            return ((global::CanvasAdaptive)o).CutoutsHeight;
        }
        static void set_CutoutsHeight_1(ref object o, object v)
        {
            ((global::CanvasAdaptive)o).CutoutsHeight = (System.Int32)v;
        }
        static object get_CutoutsBottonHeight_2(ref object o)
        {
            return ((global::CanvasAdaptive)o).CutoutsBottonHeight;
        }
        static void set_CutoutsBottonHeight_2(ref object o, object v)
        {
            ((global::CanvasAdaptive)o).CutoutsBottonHeight = (System.Int32)v;
        }


    }
}
