using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CSF;
using LitJson;
using MapEditor;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;
using System.Linq;
using CSF.Tasks;

namespace HeroModelEditor
{
    public class WarHeroEditScript : MonoBehaviour
    {

        public Dropdown ddlModel;
        public Button btnSave;
        public Slider[] sliderList;

        //private AssetbundleMgr Assetbundle;
        private SkeletonGraphic[] modelList;
        Dictionary<string, HeroModelSetting> dirModelSetting = new Dictionary<string, HeroModelSetting>();

        HeroModelSetting mConfig;
        void Awake()
        {
            Mgr.Initialize();
            StartTask().Run();           
        }
        async CTask StartTask()
        {
            await Mgr.Assetbundle.Initialize();
            modelList = transform.GetComponentsInChildren<SkeletonGraphic>();           
            LoadConfig();
        }
            // Start is called before the first frame update
        void Start()
        {
            ddlModel.AddChange(ddlModel_Change);
            ddlModel.ClearOptions();
            foreach (var item in dirModelSetting.Keys)
            {
                ddlModel.options.Add(new OptionData(item));
            }
            SetDropDownDefaultValue(ddlModel);

            for (int i = 0; i < sliderList.Length; i += 3)
            {
                sliderList[i].AddChange(slider_ChangeX,i/3);
                sliderList[i+1].AddChange(slider_ChangeY, i / 3);
                sliderList[i + 2].AddChange(slider_ChangeS, i / 3);

                sliderList[i].GetComponentInChildren<InputField>().AddChange(sliderText_ChangeX, i / 3);
                sliderList[i + 1].GetComponentInChildren<InputField>().AddChange(sliderText_ChangeY, i / 3);
                sliderList[i + 2].GetComponentInChildren<InputField>().AddChange(sliderText_ChangeS, i / 3);
            }
            btnSave.AddClick(btnSave_Click);
        }

        void slider_ChangeX(float value,Slider slider,int row)
        {
            mConfig.Pos[row][0] = (int)value;
            slider.GetComponentInChildren<InputField>().text = ((int)value).ToString();
            setModelPos();
        }
        void sliderText_ChangeX(string value, InputField field, int row)
        {
            float val = 0;
            float.TryParse(value, out val);
            slider_ChangeX(val, field.GetComponentInParent<Slider>(), row);
        }
        ///=====================================

        void slider_ChangeY(float value, Slider slider, int row)
        {
            mConfig.Pos[row][1] = (int)value;
            slider.GetComponentInChildren<InputField>().text = ((int)value).ToString();
            setModelPos();
        }
        void sliderText_ChangeY(string value, InputField field, int row)
        {
            float val = 0;
            float.TryParse(value, out val);
            slider_ChangeY(val, field.GetComponentInParent<Slider>(), row);
        }
        ///=====================================
        void slider_ChangeS(float value, Slider slider, int row)
        {
            mConfig.Pos[row][2] = value;
            slider.GetComponentInChildren<InputField>().text = value.ToString();
            setModelPos();
        }
        void sliderText_ChangeS(string value, InputField field, int row)
        {
            float val = 0;
            float.TryParse(value, out val);
            slider_ChangeS(val, field.GetComponentInParent<Slider>(), row);
        }


        void setModelPos()
        {
            RectTransform rectTran;
            float[] pos;
            for (int i = 0; i < modelList.Length; i++)
            {
                rectTran = modelList[i].rectTransform;
                pos = mConfig.Pos[i];
                rectTran.anchoredPosition = new Vector2((int)pos[0], (int)pos[1]);
                rectTran.localScale = Vector3.one * pos[2];
            }
        }
        void ddlModel_Change(int index)
        {

            ddlModelTask(ddlModel.options[index].text).Run();
        }
        async CTask ddlModelTask(string model)
        {
            mConfig = dirModelSetting[model];
            if (mConfig == null)
            {
                mConfig = new HeroModelSetting();
                mConfig.Model = model;
                mConfig.Pos = new List<float[]>();             
                dirModelSetting[model] = mConfig;
            }
            for (int i = 0; i < sliderList.Length; i += 3)
            {
                int pIndex = i / 3;
                if (mConfig.Pos.Count <= pIndex)
                    mConfig.Pos.Add(new float[] { 0, 0, 1 });
                else if (mConfig.Pos[pIndex].Length < 3)  //补上新加的Scale
                {
                    float[] newP = new float[] { 0, 0, 1 };
                    for (int x = 0; x < mConfig.Pos[pIndex].Length; x++)
                        newP[x] = mConfig.Pos[pIndex][x];
                    mConfig.Pos[pIndex] = newP;
                }
            }

            for (int i = 0; i < sliderList.Length; i += 3)
            {
                int pIndex = i / 3;
                sliderList[i].value = (int)mConfig.Pos[pIndex][0];
                sliderList[i + 1].value = (int)mConfig.Pos[pIndex][1];
                sliderList[i + 2].value = mConfig.Pos[pIndex][2];

                sliderList[i].GetComponentInChildren<InputField>().text = sliderList[i].value.ToString();
                sliderList[i + 1].GetComponentInChildren<InputField>().text = sliderList[i+1].value.ToString();
                sliderList[i + 2].GetComponentInChildren<InputField>().text = sliderList[i+2].value.ToString();

            }
            SkeletonDataAsset asset = await LoadMonsterModel("Hero/" + model);
            foreach (var sg in modelList)
            {
                sg.skeletonDataAsset = asset;
                sg.Initialize(true);
                //sg.AnimationState.SetAnimation(0, "Idle01", true);
            }           
            setModelPos();
        }
        void btnSave_Click()
        {
            string path = Path.GetFullPath(AppSetting.BundleResDir+ "Data/HeroModelSetting.txt");
            FileInfo info = new FileInfo(path);
            List<HeroModelSetting> list = new List<HeroModelSetting>();
            foreach (var sett in dirModelSetting.Values)
            {
                if (sett != null)
                    list.Add(sett);
            }
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8")))
                {
                    sWriter.WriteLine(JsonMapper.ToJson(list));
                }
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.DisplayDialog("提示信息", "全部保存成功", "确定");
#endif


        }


        public async CTask<SkeletonDataAsset> LoadMonsterModel(string assetBundleName)
        {
            string assetName = assetBundleName.Substring(assetBundleName.LastIndexOf("/") + 1) + AppSetting.SkeletonDataName;
            assetBundleName = AppSetting.SkeletonDataAssetDir + assetBundleName + AppSetting.SkeletonDataName + AppSetting.SkeletonDataExtName;
            return await Mgr.Assetbundle.LoadAsset<SkeletonDataAsset>(assetBundleName, assetName);
        }
      
        private void LoadConfig()
        {
            //TextAsset asset = await Assetbundle.LoadAsset<TextAsset>("data/HeroModelSetting.txt" + AppSetting.SkeletonDataExtName, "HeroModelSetting");

            string baseBunldDir = AppSetting.BundleResDir + AppSetting.SkeletonDataAssetDir + "Hero/";
            int subLength = AppSetting.SkeletonDataName.Length + AppSetting.SkeletonDataExtName.Length;
            foreach (string filepath in Directory.GetFiles(baseBunldDir, "*.json", SearchOption.AllDirectories))
                dirModelSetting.Add(Path.GetFileNameWithoutExtension(filepath), null);

            string path = Path.GetFullPath(AppSetting.BundleResDir + "Data/HeroModelSetting.txt");
            FileInfo info = new FileInfo(path);
            if (!info.Exists) return;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.GetEncoding("UTF-8")))
                {
                    string content = reader.ReadToEnd();
                    List<HeroModelSetting> list = JsonMapper.ToObject<List<HeroModelSetting>>(content);
                    if (list == null) return;
                    foreach (var config in list)
                    {
                        if (dirModelSetting.ContainsKey(config.Model))
                            dirModelSetting[config.Model] = config;
                    }
                }
            }
        }

        private void SetDropDownDefaultValue(Dropdown ddl)
        {
            if (ddl.value == 0) //不会触发事件，强制触发一下
            {
                ddl.captionText.text = ddl.options[0].text;
                ddl.onValueChanged.Invoke(0);
            }
            else
                ddl.value = 0;
        }
    }
}
