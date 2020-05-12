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
    unsafe class CSF_Mgr_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(CSF.Mgr);

            field = type.GetField("Assetbundle", flag);
            app.RegisterCLRFieldGetter(field, get_Assetbundle_0);
            app.RegisterCLRFieldSetter(field, set_Assetbundle_0);
            field = type.GetField("Task", flag);
            app.RegisterCLRFieldGetter(field, get_Task_1);
            app.RegisterCLRFieldSetter(field, set_Task_1);
            field = type.GetField("VersionCheck", flag);
            app.RegisterCLRFieldGetter(field, get_VersionCheck_2);
            app.RegisterCLRFieldSetter(field, set_VersionCheck_2);
            field = type.GetField("UI", flag);
            app.RegisterCLRFieldGetter(field, get_UI_3);
            app.RegisterCLRFieldSetter(field, set_UI_3);


        }



        static object get_Assetbundle_0(ref object o)
        {
            return CSF.Mgr.Assetbundle;
        }
        static void set_Assetbundle_0(ref object o, object v)
        {
            CSF.Mgr.Assetbundle = (CSF.AssetbundleMgr)v;
        }
        static object get_Task_1(ref object o)
        {
            return CSF.Mgr.Task;
        }
        static void set_Task_1(ref object o, object v)
        {
            CSF.Mgr.Task = (CSF.CTaskMgr)v;
        }
        static object get_VersionCheck_2(ref object o)
        {
            return CSF.Mgr.VersionCheck;
        }
        static void set_VersionCheck_2(ref object o, object v)
        {
            CSF.Mgr.VersionCheck = (CSF.VersionCheckMgr)v;
        }
        static object get_UI_3(ref object o)
        {
            return CSF.Mgr.UI;
        }
        static void set_UI_3(ref object o, object v)
        {
            CSF.Mgr.UI = (CSF.UIMgr)v;
        }


    }
}
