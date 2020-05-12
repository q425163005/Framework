using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MapEditor
{
    public class OperateUI : MonoBehaviour
    {
        public Button btnReLoad;
        public Button btnSave;
        void Start()
        {
            btnReLoad.AddClick(btnReLoad_Click);
            btnSave.AddClick(btnSave_Click);
        }

        void btnReLoad_Click()
        {
            //重新加载配置
            MapEditor.I.Config.ReadMapsConfig();

            MapConfig config = null;
            if (MapEditor.I.Config.dicMapsList.TryGetValue((int)MapEditor.I.MapType, out var list))
            {
                list.TryGetValue(MapEditor.I.MapId, out config);
            }
            MapEditor.I.SetMapConfig(MapEditor.I.MapId, config);
            UIRoot.I.MapSelect.Refresh();
        }
        void btnSave_Click()
        {
            MapEditor.I.Config.SaveMapConfig();
            TipsUI.Show("保存完成!!");
        }
    }
}
