using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CSF
{
    public class ModelExtendMenu
    {
        //[MenuItem("GameObject/★模型扩展★/增加赛马绑点脚本", false,10)]
        //static void CreateHorseObject(MenuCommand menuCommadn)
        //{
        //    GameObject target = menuCommadn.context as GameObject;
        //    if (target.GetComponent<ModelOutlet>() == null)
        //    {
        //        ModelOutlet outlet = target.AddComponent<ModelOutlet>();
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "Animator" });  //动画
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "MatSub" });  //子材质
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "MatMain" });  //主材质
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "HMask_Dummy" }); //马罩                
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "Saddle_Dummy" });//马鞍
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "RiderPos" });  //骑师               
        //    }
        //}

        //[MenuItem("GameObject/★模型扩展★/增加骑师绑点脚本", false, 10)]
        //static void CreateJocketObject(MenuCommand menuCommadn)
        //{
        //    GameObject target = menuCommadn.context as GameObject;
        //    if (target.GetComponent<ModelOutlet>() == null)
        //    {
        //        ModelOutlet outlet = target.AddComponent<ModelOutlet>();
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "Animator" });  //动画
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "MatMain" });  //主材质
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "Whip_Dummy" });//马鞭
        //        outlet.OutletInfos.Add(new ModelOutlet.ModelOutletInfo() { NodeName = "Helmet_Dummy" }); //头盔                
        //    }
        //}
    }
}