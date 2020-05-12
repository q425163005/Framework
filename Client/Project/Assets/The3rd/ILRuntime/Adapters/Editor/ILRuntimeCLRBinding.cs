#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using CSF;

[System.Reflection.Obfuscation(Exclude = true)]
public class ILRuntimeCLRBinding
{
    [MenuItem("ILRuntime/Generate CLR Binding Code")]
    static void GenerateCLRBinding()
    {
        HashSet<MethodBase> excludeMethods = new HashSet<MethodBase>();
        excludeMethods.Add(typeof(float).GetMethod("IsFinite"));

        List<Type> types = new List<Type>();
        types.Add(typeof(float));
        types.Add(typeof(int));
        types.Add(typeof(float));
        types.Add(typeof(long));
        types.Add(typeof(object));
        types.Add(typeof(string));
        types.Add(typeof(Array));
        types.Add(typeof(Vector2));
        types.Add(typeof(Vector3));
        types.Add(typeof(Quaternion));
        types.Add(typeof(GameObject));
        types.Add(typeof(UnityEngine.Object));
        types.Add(typeof(Transform));
        types.Add(typeof(RectTransform));       
        types.Add(typeof(Time));
        types.Add(typeof(Debug));
               
        ////所有DLL内的类型的真实C#类型都是ILTypeInstance
        types.Add(typeof(List<ILRuntime.Runtime.Intepreter.ILTypeInstance>));
        ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, "Assets/The3rd/ILRuntime/Adapters/Generated", excludeMethods);
        AssetDatabase.Refresh();
    }


    [MenuItem("ILRuntime/Generate CLR Binding Code by Analysis")]
    static void GenerateCLRBindingByAnalysis()
    {
        //用新的分析热更dll调用引用来生成绑定代码
        ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        System.IO.FileStream fs = new System.IO.FileStream(AppSetting.ILRCodeDir + AppSetting.HotFixName + ".dll", System.IO.FileMode.Open, System.IO.FileAccess.Read);
        domain.LoadAssembly(fs);
        //Crossbind Adapter is needed to generate the correct binding code
        //InitILRuntime(domain);
        CSF.ILRHelper.InitILRuntime(domain);
        ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/The3rd/ILRuntime/Adapters/Generated");
        AssetDatabase.Refresh();
    }    
}
#endif
