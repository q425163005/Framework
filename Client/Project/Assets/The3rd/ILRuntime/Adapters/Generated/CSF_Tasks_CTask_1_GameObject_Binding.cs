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
    unsafe class CSF_Tasks_CTask_1_GameObject_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(CSF.Tasks.CTask<UnityEngine.GameObject>);
            args = new Type[]{};
            method = type.GetMethod("GetAwaiter", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetAwaiter_0);
            args = new Type[]{};
            method = type.GetMethod("GetResult", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, GetResult_1);
            args = new Type[]{};
            method = type.GetMethod("Create", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, Create_2);
            Dictionary<string, List<MethodInfo>> genericMethods = new Dictionary<string, List<MethodInfo>>();
            List<MethodInfo> lst = null;                    
            foreach(var m in type.GetMethods())
            {
                if(m.IsGenericMethodDefinition)
                {
                    if (!genericMethods.TryGetValue(m.Name, out lst))
                    {
                        lst = new List<MethodInfo>();
                        genericMethods[m.Name] = lst;
                    }
                    lst.Add(m);
                }
            }
            args = new Type[]{typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor)};
            if (genericMethods.TryGetValue("Start", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor).MakeByRefType()))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, Start_3);

                        break;
                    }
                }
            }
            args = new Type[]{};
            method = type.GetMethod("get_Task", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Task_4);
            args = new Type[]{typeof(CSF.Tasks.CTask<UnityEngine.GameObject>), typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor)};
            if (genericMethods.TryGetValue("AwaitOnCompleted", out lst))
            {
                foreach(var m in lst)
                {
                    if(m.MatchGenericParameters(args, typeof(void), typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).MakeByRefType(), typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor).MakeByRefType()))
                    {
                        method = m.MakeGenericMethod(args);
                        app.RegisterCLRMethodRedirection(method, AwaitOnCompleted_5);

                        break;
                    }
                }
            }
            args = new Type[]{typeof(System.Exception)};
            method = type.GetMethod("SetException", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetException_6);
            args = new Type[]{typeof(UnityEngine.GameObject)};
            method = type.GetMethod("SetResult", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, SetResult_7);


        }


        static StackObject* GetAwaiter_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetAwaiter();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* GetResult_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.GetResult();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Create_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = CSF.Tasks.CTask<UnityEngine.GameObject>.Create();

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* Start_3(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor @stateMachine = (CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor)typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor).CheckCLRTypes(__intp.RetriveObject(ptr_of_this_method, __mStack));

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));

            instance_of_this_method.Start<CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor>(ref @stateMachine);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.StackObjectReference:
                    {
                        var ___dst = ILIntepreter.ResolveReference(ptr_of_this_method);
                        object ___obj = @stateMachine;
                        if (___dst->ObjectType >= ObjectTypes.Object)
                        {
                            if (___obj is CrossBindingAdaptorType)
                                ___obj = ((CrossBindingAdaptorType)___obj).ILInstance;
                            __mStack[___dst->Value] = ___obj;
                        }
                        else
                        {
                            ILIntepreter.UnboxObject(___dst, ___obj, __mStack, __domain);
                        }
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = @stateMachine;
                        }
                        else
                        {
                            var ___type = __domain.GetType(___obj.GetType()) as CLRType;
                            ___type.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, @stateMachine);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var ___type = __domain.GetType(ptr_of_this_method->Value);
                        if(___type is ILType)
                        {
                            ((ILType)___type).StaticInstance[ptr_of_this_method->ValueLow] = @stateMachine;
                        }
                        else
                        {
                            ((CLRType)___type).SetStaticFieldValue(ptr_of_this_method->ValueLow, @stateMachine);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = @stateMachine;
                    }
                    break;
            }

            __intp.Free(ptr_of_this_method);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            __intp.Free(ptr_of_this_method);
            return __ret;
        }

        static StackObject* get_Task_4(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            var result_of_this_method = instance_of_this_method.Task;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static StackObject* AwaitOnCompleted_5(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 3);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor @stateMachine = (CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor)typeof(CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor).CheckCLRTypes(__intp.RetriveObject(ptr_of_this_method, __mStack));

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            CSF.Tasks.CTask<UnityEngine.GameObject> @awaiter = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(__intp.RetriveObject(ptr_of_this_method, __mStack));

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));

            instance_of_this_method.AwaitOnCompleted<CSF.Tasks.CTask<UnityEngine.GameObject>, CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor>(ref @awaiter, ref @stateMachine);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.StackObjectReference:
                    {
                        var ___dst = ILIntepreter.ResolveReference(ptr_of_this_method);
                        object ___obj = @stateMachine;
                        if (___dst->ObjectType >= ObjectTypes.Object)
                        {
                            if (___obj is CrossBindingAdaptorType)
                                ___obj = ((CrossBindingAdaptorType)___obj).ILInstance;
                            __mStack[___dst->Value] = ___obj;
                        }
                        else
                        {
                            ILIntepreter.UnboxObject(___dst, ___obj, __mStack, __domain);
                        }
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = @stateMachine;
                        }
                        else
                        {
                            var ___type = __domain.GetType(___obj.GetType()) as CLRType;
                            ___type.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, @stateMachine);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var ___type = __domain.GetType(ptr_of_this_method->Value);
                        if(___type is ILType)
                        {
                            ((ILType)___type).StaticInstance[ptr_of_this_method->ValueLow] = @stateMachine;
                        }
                        else
                        {
                            ((CLRType)___type).SetStaticFieldValue(ptr_of_this_method->ValueLow, @stateMachine);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as CSF.IAsyncStateMachineClassInheritanceAdaptor.IAsyncStateMachineAdaptor[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = @stateMachine;
                    }
                    break;
            }

            __intp.Free(ptr_of_this_method);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.StackObjectReference:
                    {
                        var ___dst = ILIntepreter.ResolveReference(ptr_of_this_method);
                        object ___obj = @awaiter;
                        if (___dst->ObjectType >= ObjectTypes.Object)
                        {
                            if (___obj is CrossBindingAdaptorType)
                                ___obj = ((CrossBindingAdaptorType)___obj).ILInstance;
                            __mStack[___dst->Value] = ___obj;
                        }
                        else
                        {
                            ILIntepreter.UnboxObject(___dst, ___obj, __mStack, __domain);
                        }
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = @awaiter;
                        }
                        else
                        {
                            var ___type = __domain.GetType(___obj.GetType()) as CLRType;
                            ___type.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, @awaiter);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var ___type = __domain.GetType(ptr_of_this_method->Value);
                        if(___type is ILType)
                        {
                            ((ILType)___type).StaticInstance[ptr_of_this_method->ValueLow] = @awaiter;
                        }
                        else
                        {
                            ((CLRType)___type).SetStaticFieldValue(ptr_of_this_method->ValueLow, @awaiter);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as CSF.Tasks.CTask<UnityEngine.GameObject>[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = @awaiter;
                    }
                    break;
            }

            __intp.Free(ptr_of_this_method);
            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            __intp.Free(ptr_of_this_method);
            return __ret;
        }

        static StackObject* SetException_6(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Exception @exception = (System.Exception)typeof(System.Exception).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetException(@exception);

            return __ret;
        }

        static StackObject* SetResult_7(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 2);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            UnityEngine.GameObject @result = (UnityEngine.GameObject)typeof(UnityEngine.GameObject).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            CSF.Tasks.CTask<UnityEngine.GameObject> instance_of_this_method = (CSF.Tasks.CTask<UnityEngine.GameObject>)typeof(CSF.Tasks.CTask<UnityEngine.GameObject>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            instance_of_this_method.SetResult(@result);

            return __ret;
        }



    }
}
