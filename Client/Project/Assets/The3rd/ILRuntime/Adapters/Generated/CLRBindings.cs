using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {


        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            CSF_Tasks_CTask_1_Texture_Binding.Register(app);
            CSF_Tasks_CTask_Binding.Register(app);
            CSF_Tasks_CTask_1_AudioClip_Binding.Register(app);
            CSF_Tasks_CTask_1_SkeletonDataAsset_Binding.Register(app);
            System_String_Binding.Register(app);
            CSF_Mgr_Binding.Register(app);
            CSF_AssetbundleMgr_Binding.Register(app);
            CSF_Tasks_RoutineBase_Binding.Register(app);
            System_Byte_Binding.Register(app);
            System_Security_Cryptography_RNGCryptoServiceProvider_Binding.Register(app);
            System_Security_Cryptography_RandomNumberGenerator_Binding.Register(app);
            System_BitConverter_Binding.Register(app);
            System_Random_Binding.Register(app);
            System_Object_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            System_TimeZone_Binding.Register(app);
            System_DateTime_Binding.Register(app);
            System_TimeSpan_Binding.Register(app);
            System_Int32_Binding.Register(app);
            System_Action_Binding.Register(app);
            CSF_CLog_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            System_Text_StringBuilder_Binding.Register(app);
            System_Collections_IList_Binding.Register(app);
            System_Collections_ICollection_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Array_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Reflection_PropertyInfo_Binding.Register(app);
            System_Reflection_MethodBase_Binding.Register(app);
            System_Text_ASCIIEncoding_Binding.Register(app);
            System_Text_Encoding_Binding.Register(app);
            System_Convert_Binding.Register(app);
            System_Text_RegularExpressions_Regex_Binding.Register(app);
            System_Text_RegularExpressions_Group_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Int64_Binding.Register(app);
            System_Text_UTF8Encoding_Binding.Register(app);
            UnityEngine_Color_Binding.Register(app);
            UnityEngine_ColorUtility_Binding.Register(app);
            CSF_CTaskMgr_Binding.Register(app);
            CSF_Tasks_RoutineManager_Binding.Register(app);
            UnityEngine_Application_Binding.Register(app);
            UnityEngine_QualitySettings_Binding.Register(app);
            UnityEngine_Screen_Binding.Register(app);
            UnityEngine_Input_Binding.Register(app);
            CSF_AppSetting_Binding.Register(app);
            CSF_VersionCheckMgr_Binding.Register(app);
            System_NotImplementedException_Binding.Register(app);
            CSF_Tasks_CTask_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Object_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_List_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Int32_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            System_Char_Binding.Register(app);
            CSF_Tasks_CTask_1_Object_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Int32_ILTypeInstance_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            CSF_UIMgr_Binding.Register(app);
            UnityEngine_PlayerPrefs_Binding.Register(app);
            System_Collections_Generic_List_1_IDisposable_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            ExtensionMethods_Binding.Register(app);
            UnityEngine_Canvas_Binding.Register(app);
            UnityEngine_SortingLayer_Binding.Register(app);
            LayerManager_Binding.Register(app);
            CSF_Tasks_CTask_1_GameObject_Binding.Register(app);
            EventListener_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            UnityEngine_UI_Dropdown_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_Int32_Binding.Register(app);
            UnityEngine_UI_Toggle_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_Boolean_Binding.Register(app);
            UnityEngine_UI_ScrollRect_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_Vector2_Binding.Register(app);
            UnityEngine_UI_Slider_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_Single_Binding.Register(app);
            UnityEngine_UI_InputField_Binding.Register(app);
            UnityEngine_Events_UnityEvent_1_String_Binding.Register(app);
            DragEventListener_Binding.Register(app);
            UnityEngine_UI_Image_Binding.Register(app);
            UnityEngine_Resources_Binding.Register(app);
            UnityEngine_UI_Graphic_Binding.Register(app);
            UnityEngine_UI_Selectable_Binding.Register(app);
            UnityEngine_Material_Binding.Register(app);
            System_Collections_Generic_List_1_Transform_Binding.Register(app);
            System_Collections_IEnumerator_Binding.Register(app);
            UnityEngine_Events_UnityAction_2_Boolean_Toggle_Binding.Register(app);
            CSF_Tasks_CTask_1_SpriteAtlas_Binding.Register(app);
            UnityEngine_U2D_SpriteAtlas_Binding.Register(app);
            UnityEngine_RectTransform_Binding.Register(app);
            UnityEngine_UI_RawImage_Binding.Register(app);
            UIOutlet_Binding.Register(app);
            System_Collections_Generic_List_1_UIOutlet_Binding_OutletInfo_Binding.Register(app);
            UIOutlet_Binding_OutletInfo_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_GameObject_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            CanvasAdaptive_Binding.Register(app);
            DG_Tweening_DOTweenModuleUI_Binding.Register(app);
            DG_Tweening_TweenSettingsExtensions_Binding.Register(app);
            DG_Tweening_ShortcutExtensions_Binding.Register(app);
            UnityEngine_Vector2Int_Binding.Register(app);
            Spine_Unity_SkeletonGraphic_Binding.Register(app);
            Spine_SkeletonData_Binding.Register(app);
            System_Collections_Generic_List_1_Single_Array_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            Spine_AnimationState_Binding.Register(app);
            Spine_TrackEntry_Binding.Register(app);
            Spine_Unity_SkeletonDataAsset_Binding.Register(app);
            Spine_ExposedList_1_Animation_Binding.Register(app);
            Spine_Animation_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Exception_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_KeyCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_KeyCollection_Binding_Enumerator_Binding.Register(app);
            System_Linq_Enumerable_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_ValueCollection_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_ILTypeInstance_Binding_ValueCollection_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_Transform_Binding.Register(app);
            UnityEngine_SceneManagement_SceneManager_Binding.Register(app);
            UnityEngine_SceneManagement_Scene_Binding.Register(app);
            UnityEngine_UI_ColorBlock_Binding.Register(app);
            CSF_Tasks_CTask_1_Material_Binding.Register(app);
            UnityEngine_AudioSource_Binding.Register(app);
            UnityEngine_Animator_Binding.Register(app);
            System_Action_1_ILTypeInstance_Binding.Register(app);
            UnityEngine_UI_Text_Binding.Register(app);
            System_Collections_Generic_List_1_GameObject_Binding.Register(app);
            UnityEngine_Behaviour_Binding.Register(app);
            DG_Tweening_DOTween_Binding.Register(app);
            System_Collections_Generic_Queue_1_String_Binding.Register(app);
            CSF_Tasks_CTaskHandle_Binding.Register(app);
            System_Collections_Generic_Queue_1_ILTypeInstance_Binding.Register(app);
            System_Collections_Generic_Queue_1_Action_Binding.Register(app);
            CSF_Tasks_CTask_1_Boolean_Binding.Register(app);
            UnityEngine_Networking_UnityWebRequest_Binding.Register(app);
            IEnumeratorAwaitExtensions_Binding.Register(app);
            IEnumeratorAwaitExtensions_Binding_SimpleCoroutineAwaiter_1_AsyncOperation_Binding.Register(app);
            UnityEngine_Networking_DownloadHandler_Binding.Register(app);
            UnityEngine_Events_UnityEventBase_Binding.Register(app);
            UnityEngine_Mathf_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_GameObject_Int32_Binding.Register(app);
            System_Action_2_GameObject_Int32_Binding.Register(app);
            System_Collections_Generic_Queue_1_GameObject_Binding.Register(app);
            UnityEngine_RectOffset_Binding.Register(app);
            UnityEngine_Rect_Binding.Register(app);
            UnityEngine_UI_GridLayoutGroup_Binding.Register(app);
            UnityEngine_UI_LayoutGroup_Binding.Register(app);
            UnityEngine_UI_HorizontalOrVerticalLayoutGroup_Binding.Register(app);
            System_Action_1_Int32_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
        }
    }
}
