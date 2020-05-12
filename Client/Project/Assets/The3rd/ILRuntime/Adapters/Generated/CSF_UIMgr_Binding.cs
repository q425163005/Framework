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
    unsafe class CSF_UIMgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CSF.UIMgr);
            args = new Type[]{};
            method = type.GetMethod("RefreshLang", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, RefreshLang_0);
            args = new Type[]{typeof(UnityEngine.RectTransform)};
            method = type.GetMethod("SetUIBgPadding", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetUIBgPadding_1);

            field = type.GetField("canvasAdaptive", flag);
            app.RegisterCLRFieldGetter(field, get_canvasAdaptive_0);
            app.RegisterCLRFieldSetter(field, set_canvasAdaptive_0);
            field = type.GetField("canvas", flag);
            app.RegisterCLRFieldGetter(field, get_canvas_1);
            app.RegisterCLRFieldSetter(field, set_canvas_1);
            field = type.GetField("UIRoot", flag);
            app.RegisterCLRFieldGetter(field, get_UIRoot_2);
            app.RegisterCLRFieldSetter(field, set_UIRoot_2);


        }


        static StackObject* RefreshLang_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.UIMgr instance_of_this_method = (CSF.UIMgr)typeof(CSF.UIMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.RefreshLang();

            return __ret;
        }

        static StackObject* SetUIBgPadding_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.RectTransform @rect = (UnityEngine.RectTransform)typeof(UnityEngine.RectTransform).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            CSF.UIMgr instance_of_this_method = (CSF.UIMgr)typeof(CSF.UIMgr).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetUIBgPadding(@rect);

            return __ret;
        }


        static object get_canvasAdaptive_0(ref object o)
        {
            return ((CSF.UIMgr)o).canvasAdaptive;
        }
        static void set_canvasAdaptive_0(ref object o, object v)
        {
            ((CSF.UIMgr)o).canvasAdaptive = (global::CanvasAdaptive)v;
        }
        static object get_canvas_1(ref object o)
        {
            return ((CSF.UIMgr)o).canvas;
        }
        static void set_canvas_1(ref object o, object v)
        {
            ((CSF.UIMgr)o).canvas = (UnityEngine.Canvas)v;
        }
        static object get_UIRoot_2(ref object o)
        {
            return ((CSF.UIMgr)o).UIRoot;
        }
        static void set_UIRoot_2(ref object o, object v)
        {
            ((CSF.UIMgr)o).UIRoot = (UnityEngine.RectTransform)v;
        }


    }
}
