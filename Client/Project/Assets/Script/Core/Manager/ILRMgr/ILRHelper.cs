using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CSF
{
    public static class ILRHelper
    {
        public static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            // 注册重定向函数

            // 注册委托
            appdomain.DelegateManager.RegisterMethodDelegate<List<object>>();
            appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();           
            appdomain.DelegateManager.RegisterFunctionDelegate<Google.Protobuf.Adapt_IMessage.Adaptor>();
            appdomain.DelegateManager.RegisterMethodDelegate<Google.Protobuf.Adapt_IMessage.Adaptor>();
            appdomain.DelegateManager.RegisterFunctionDelegate<Google.Protobuf.Adapt_IMessage.Adaptor, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Boolean>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Int32>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Boolean>();
            appdomain.DelegateManager.RegisterMethodDelegate<Vector2>();
            appdomain.DelegateManager.RegisterMethodDelegate<Vector2Int>();
            appdomain.DelegateManager.RegisterFunctionDelegate<UnityEngine.Vector2, System.Single>();

            appdomain.DelegateManager.RegisterMethodDelegate<System.Single>();
            appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Single>();
            appdomain.DelegateManager.RegisterFunctionDelegate<KeyValuePair<System.Int32, System.Int32>, KeyValuePair<System.Int32, System.Int32>, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.Int32, ILRuntime.Runtime.Intepreter.ILTypeInstance>, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.Int32, ILRuntime.Runtime.Intepreter.ILTypeInstance>, ILRuntime.Runtime.Intepreter.ILTypeInstance>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Single>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Int32>();

            appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
            appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.String>();

            appdomain.DelegateManager.RegisterMethodDelegate<Spine.TrackEntry>();


            //注册UGUI事件委托
            appdomain.DelegateManager.RegisterMethodDelegate<BaseEventData>();
            appdomain.DelegateManager.RegisterMethodDelegate<PointerEventData>();
            appdomain.DelegateManager.RegisterMethodDelegate<AxisEventData>();

            appdomain.DelegateManager.RegisterMethodDelegate<System.String>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.String, System.String>();
           

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction>((action) =>
            {
                return new UnityAction(() => { ((System.Action)action)(); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<System.Int32>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Int32>((arg0) =>
                {
                    ((Action<System.Int32>)act)(arg0);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<System.Boolean>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Boolean>((arg0) =>
                {
                    ((Action<System.Boolean>)act)(arg0);
                });
            });            

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityAction<BaseEventData>>((action) =>
            {
                return new UnityAction<BaseEventData>((a) => { ((System.Action<BaseEventData>)action)(a); });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.PointerDataDelegate>((act) =>
            {
                return new EventListener.PointerDataDelegate((eventData) =>{((Action<UnityEngine.EventSystems.PointerEventData>)act)(eventData);});
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<EventListener.AxisDataDelegate>((act) =>
            {
                return new EventListener.AxisDataDelegate((eventData) =>{((Action<UnityEngine.EventSystems.AxisEventData>)act)(eventData);});
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
            {
                return new DG.Tweening.TweenCallback(() =>
                {
                    ((Action)act)();
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
            {
                return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
                {
                    return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>)act)(obj);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.Vector2>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<UnityEngine.Vector2>((arg0) =>
                {
                    ((Action<UnityEngine.Vector2>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Single>>((act) =>
            {
                return new DG.Tweening.Core.DOSetter<System.Single>((pNewValue) =>
                {
                    ((Action<System.Single>)act)(pNewValue);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Collections.Generic.KeyValuePair<System.Int32, System.Int32>>>((act) =>
            {
                return new System.Comparison<System.Collections.Generic.KeyValuePair<System.Int32, System.Int32>>((x, y) =>
                {
                    return ((Func<System.Collections.Generic.KeyValuePair<System.Int32, System.Int32>, System.Collections.Generic.KeyValuePair<System.Int32, System.Int32>, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Single>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Single>((arg0) =>
                {
                    ((Action<System.Single>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback<System.Int32>>((act) =>
            {
                return new DG.Tweening.TweenCallback<System.Int32>((value) =>
                {
                    ((Action<System.Int32>)act)(value);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Single>>((act) =>
            {
                return new DG.Tweening.Core.DOGetter<System.Single>(() =>
                {
                    return ((Func<System.Single>)act)();
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOGetter<System.Int32>>((act) =>
            {
                return new DG.Tweening.Core.DOGetter<System.Int32>(() =>
                {
                    return ((Func<System.Int32>)act)();
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.Core.DOSetter<System.Int32>>((act) =>
            {
                return new DG.Tweening.Core.DOSetter<System.Int32>((pNewValue) =>
                {
                    ((Action<System.Int32>)act)(pNewValue);
                });
            });
            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.String>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.String>((arg0) =>
                {
                    ((Action<System.String>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<Spine.AnimationState.TrackEntryDelegate>((act) =>
            {
                return new Spine.AnimationState.TrackEntryDelegate((trackEntry) =>
                {
                    ((Action<Spine.TrackEntry>)act)(trackEntry);
                });
            });


            #region 滑动
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector3>();
            //缩放
            appdomain.DelegateManager.RegisterDelegateConvertor<MapScrollRect.ZoomDelegate>((act) =>
            {
                return new MapScrollRect.ZoomDelegate((arg0) =>
                {
                    ((Action<UnityEngine.Vector3>)act)(arg0);
                });
            });

            #endregion

            #region 支付

            //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Purchasing.IStoreController, UnityEngine.Purchasing.IExtensionProvider>();
            //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Purchasing.InitializationFailureReason>();
            //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Purchasing.Product, UnityEngine.Purchasing.PurchaseFailureReason>();
            //appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Purchasing.PurchaseEventArgs>();

            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.Purchaser.InitSuccessedDelegate>((act) =>
            //{
            //    return new CSF.Purchaser.InitSuccessedDelegate(() =>
            //    {
            //        ((Action)act)();
            //    });
            //});
            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.Purchaser.InitFailedDelegate>((act) =>
            //{
            //    return new CSF.Purchaser.InitFailedDelegate(() =>
            //    {
            //        ((Action)act)();
            //    });
            //});

            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.Purchaser.PurchaseSuccessedDelegate>((act) =>
            //{
            //    return new CSF.Purchaser.PurchaseSuccessedDelegate((productid, orderid) =>
            //    {
            //        ((Action<System.String, System.String>)act)(productid, orderid);
            //    });
            //});
            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.Purchaser.PurchaseFailedDelegate>((act) =>
            //{
            //    return new CSF.Purchaser.PurchaseFailedDelegate(() =>
            //    {
            //        ((Action)act)();
            //    });
            //});

            #endregion

            #region 谷歌登录

            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.GoogleSDKLogin.GoogleLoginSuccessDelegate>((act) =>
            //{
            //    return new CSF.GoogleSDKLogin.GoogleLoginSuccessDelegate((token) =>
            //    {
            //        ((Action<System.String>)act)(token);
            //    });
            //});
            //appdomain.DelegateManager.RegisterDelegateConvertor<CSF.GoogleSDKLogin.GoogleLoginFailedDelegate>((act) =>
            //{
            //    return new CSF.GoogleSDKLogin.GoogleLoginFailedDelegate(() =>
            //    {
            //        ((Action)act)();
            //    });
            //});


            #endregion

            #region FaceBook登陆
            //appdomain.DelegateManager.RegisterDelegateConvertor<Facebook.Unity.InitDelegate>((act) =>
            //{
            //    return new Facebook.Unity.InitDelegate(() =>
            //    {
            //        ((Action)act)();
            //    });
            //});
            //appdomain.DelegateManager.RegisterDelegateConvertor<Facebook.Unity.HideUnityDelegate>((act) =>
            //{
            //    return new Facebook.Unity.HideUnityDelegate((isUnityShown) =>
            //    {
            //        ((Action<System.Boolean>)act)(isUnityShown);
            //    });
            //});
            //appdomain.DelegateManager.RegisterMethodDelegate<Facebook.Unity.ILoginResult>();
            //appdomain.DelegateManager.RegisterDelegateConvertor<Facebook.Unity.FacebookDelegate<Facebook.Unity.ILoginResult>>((act) =>
            //{
            //    return new Facebook.Unity.FacebookDelegate<Facebook.Unity.ILoginResult>((result) =>
            //    {
            //        ((Action<Facebook.Unity.ILoginResult>)act)(result);
            //    });
            //});
            #endregion
            

            CLog.RegisterILRuntimeCLRRedirection(appdomain);
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(appdomain);
            // 注册适配器
            Assembly assembly = typeof(Main).Assembly;
            foreach (Type type in assembly.GetTypes())
            {
                object[] attrs = type.GetCustomAttributes(typeof(ILAdapterAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }
                object obj = Activator.CreateInstance(type);
                CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
                if (adaptor == null)
                {
                    continue;
                }
                appdomain.RegisterCrossBindingAdaptor(adaptor);
            }

            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
        }
        
        //ILIntepreter4077
        public static void MethodException(ILIntepreter intepreter,Exception ex,ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            if (!(ex is ILRuntimeException))
            {
                if (appdomain != null && appdomain.DebugService != null)
                {
                    CLog.Error("[ILR Error]:" + ex.Message + "\t\t" + appdomain.DebugService.GetStackTrace(intepreter) + "\n");
                }
            }
        }
    }
}
