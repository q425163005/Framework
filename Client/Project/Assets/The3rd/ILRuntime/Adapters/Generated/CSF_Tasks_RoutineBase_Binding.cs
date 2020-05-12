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
    unsafe class CSF_Tasks_RoutineBase_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(CSF.Tasks.RoutineBase);
            args = new Type[]{};
            method = type.GetMethod("get_IsCompleted", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_IsCompleted_0);
            args = new Type[]{typeof(System.Single)};
            method = type.GetMethod("WaitForSeconds", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WaitForSeconds_1);
            args = new Type[]{typeof(System.Func<System.Boolean>)};
            method = type.GetMethod("WaitUntil", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WaitUntil_2);
            args = new Type[]{};
            method = type.GetMethod("WaitForNextFrame", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, WaitForNextFrame_3);


        }


        static StackObject* get_IsCompleted_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.Tasks.RoutineBase instance_of_this_method = (CSF.Tasks.RoutineBase)typeof(CSF.Tasks.RoutineBase).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.IsCompleted;

            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method ? 1 : 0;
            return __ret + 1;
        }

        static StackObject* WaitForSeconds_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Single @seconds = *(float*)&ptr_of_this_method->Value;


            var result_of_this_method = CSF.Tasks.RoutineBase.WaitForSeconds(@seconds);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* WaitUntil_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Func<System.Boolean> @condition = (System.Func<System.Boolean>)typeof(System.Func<System.Boolean>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);


            var result_of_this_method = CSF.Tasks.RoutineBase.WaitUntil(@condition);

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* WaitForNextFrame_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = CSF.Tasks.RoutineBase.WaitForNextFrame();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
