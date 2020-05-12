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
    unsafe class EventListener_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::EventListener);
            args = new Type[]{typeof(UnityEngine.GameObject)};
            method = type.GetMethod("Get", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Get_0);
            args = new Type[]{typeof(UnityEngine.Component)};
            method = type.GetMethod("Get", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Get_1);

            field = type.GetField("onClick", flag);
            app.RegisterCLRFieldGetter(field, get_onClick_0);
            app.RegisterCLRFieldSetter(field, set_onClick_0);
            field = type.GetField("onEnter", flag);
            app.RegisterCLRFieldGetter(field, get_onEnter_1);
            app.RegisterCLRFieldSetter(field, set_onEnter_1);
            field = type.GetField("onExit", flag);
            app.RegisterCLRFieldGetter(field, get_onExit_2);
            app.RegisterCLRFieldSetter(field, set_onExit_2);
            field = type.GetField("onDown", flag);
            app.RegisterCLRFieldGetter(field, get_onDown_3);
            app.RegisterCLRFieldSetter(field, set_onDown_3);
            field = type.GetField("onUp", flag);
            app.RegisterCLRFieldGetter(field, get_onUp_4);
            app.RegisterCLRFieldSetter(field, set_onUp_4);


        }


        static StackObject* Get_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.GameObject @go = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = global::EventListener.Get(@go);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Get_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.Component @go = (UnityEngine.Component)typeof(UnityEngine.Component).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = global::EventListener.Get(@go);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }


        static object get_onClick_0(ref object o)
        {
            return ((global::EventListener)o).onClick;
        }
        static void set_onClick_0(ref object o, object v)
        {
            ((global::EventListener)o).onClick = (global::EventListener.PointerDataDelegate)v;
        }
        static object get_onEnter_1(ref object o)
        {
            return ((global::EventListener)o).onEnter;
        }
        static void set_onEnter_1(ref object o, object v)
        {
            ((global::EventListener)o).onEnter = (global::EventListener.PointerDataDelegate)v;
        }
        static object get_onExit_2(ref object o)
        {
            return ((global::EventListener)o).onExit;
        }
        static void set_onExit_2(ref object o, object v)
        {
            ((global::EventListener)o).onExit = (global::EventListener.PointerDataDelegate)v;
        }
        static object get_onDown_3(ref object o)
        {
            return ((global::EventListener)o).onDown;
        }
        static void set_onDown_3(ref object o, object v)
        {
            ((global::EventListener)o).onDown = (global::EventListener.PointerDataDelegate)v;
        }
        static object get_onUp_4(ref object o)
        {
            return ((global::EventListener)o).onUp;
        }
        static void set_onUp_4(ref object o, object v)
        {
            ((global::EventListener)o).onUp = (global::EventListener.PointerDataDelegate)v;
        }


    }
}
