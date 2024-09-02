using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class _Effect_Manager : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //資料源----------------------------------------------------------------------------------------------------
    //物體效果資料庫
    public TextAsset _Data_EffectObjectInput_TextAsset;
    public TextAsset _Data_EffectCardInput_TextAsset;
    //物體效果圖片資料庫
    public PictureDataClass[] _Sprite_EffectObjectInput_ClassArray;
    public PictureDataClass[] _Sprite_EffectCardInput_ClassArray;
    //----------------------------------------------------------------------------------------------------

    //索引----------------------------------------------------------------------------------------------------
    //物體效果索引
    public Dictionary<string, EffectDataClass> _Data_EffectObject_Dictionary = new Dictionary<string, EffectDataClass>();
    public Dictionary<string, EffectDataClass> _Data_EffectCard_Dictionary = new Dictionary<string, EffectDataClass>();
    public Dictionary<string, LanguageClass> _Language_EffectObject_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_EffectCard_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //物件區----------------------------------------------------------------------------------------------------
    //生物效果
    public GameObject _Effect_CreatureUnit_GameObject;
    //武器效果
    public GameObject _Effect_ObjectUnit_GameObject;
    public GameObject _Effect_CardUnit_GameObject;
    //----------------------------------------------------------------------------------------------------
    //子物件集——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region DictionarySet
    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------
    //物體效果資料
    public class EffectDataClass : DataClass
    {
        public List<string> EffectTag;//
        public string Decay;//持續型態
        public int StackLimit;
        public int DecayTimes;
        public float Value;//偏好價值
    }
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    [System.Serializable]
    //名稱與圖片
    public class PictureDataClass
    {
        public string Key;
        public Sprite Sprite;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion DictionarySet


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Effect_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //建立資料----------------------------------------------------------------------------------------------------
        //資料庫
        DataSet();
        //設置語言
        LanguageSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region DataBaseSet
    //——————————————————————————————————————————————————————————————————————
    private void DataSet()
    {
        #region - EffectObject -
        //物體效果----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_EffectObjectSourceSplit_StringArray = _Data_EffectObjectInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_EffectObjectSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            EffectDataClass QuickSave_Data_Class = new EffectDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_EffectObjectSourceSplit_StringArray[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit_StringArray = QuickSave_Split_StringArray[0].Split("\r"[0]);
            //建立資料_Substring(X)代表由X開始
            QuickSave_Data_Class.EffectTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(5).Split(","[0]));
            QuickSave_Data_Class.Decay = QuickSave_TextSplit_StringArray[3].Substring(7);
            QuickSave_Data_Class.StackLimit = int.Parse(QuickSave_TextSplit_StringArray[4].Substring(12));
            QuickSave_Data_Class.DecayTimes = int.Parse(QuickSave_TextSplit_StringArray[5].Substring(12));
            QuickSave_Data_Class.Value = float.Parse(QuickSave_TextSplit_StringArray[6].Substring(7));
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_NumberSplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Numbers.Add(QuickSave_KeyValue[0].Substring(1), float.Parse(QuickSave_KeyValue[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //置入索引
            _Data_EffectObject_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
        }
        //釋放記憶體
        _Data_EffectObjectInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - EffectCard -
        //物體效果----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_EffectCardSourceSplit_StringArray = _Data_EffectCardInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_EffectCardSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            EffectDataClass QuickSave_Data_Class = new EffectDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_EffectCardSourceSplit_StringArray[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit_StringArray = QuickSave_Split_StringArray[0].Split("\r"[0]);
            //建立資料_Substring(X)代表由X開始
            QuickSave_Data_Class.EffectTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(5).Split(","[0]));
            QuickSave_Data_Class.Decay = QuickSave_TextSplit_StringArray[3].Substring(7);
            QuickSave_Data_Class.StackLimit = int.Parse(QuickSave_TextSplit_StringArray[4].Substring(12));
            QuickSave_Data_Class.DecayTimes = int.Parse(QuickSave_TextSplit_StringArray[5].Substring(12));
            QuickSave_Data_Class.Value = float.Parse(QuickSave_TextSplit_StringArray[6].Substring(7));
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_NumberSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_NumberSplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_NumberSplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Numbers.Add(QuickSave_KeyValue[0].Substring(1), float.Parse(QuickSave_KeyValue[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //置入索引
            _Data_EffectCard_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
        }
        //釋放記憶體
        _Data_EffectCardInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————


    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        #region - EffectObject -
        //生物效果——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_EffectObjectTextSource_String = "";
        string QuickSave_EffectObjectTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_EffectObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_" + _World_Manager._Config_Language_String + "_EffectObject.txt";
        if (File.Exists(QuickSave_EffectObjectTextAssetCheck_String))
        {
            QuickSave_EffectObjectTextSource_String = File.ReadAllText(QuickSave_EffectObjectTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Effect/_" + _World_Manager._Config_Language_String + "_EffectObject.txt");
            QuickSave_EffectObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_TraditionalChinese_EffectObject.txt";
            QuickSave_EffectObjectTextSource_String = File.ReadAllText(QuickSave_EffectObjectTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_EffectObjectSourceSplit = QuickSave_EffectObjectTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_EffectObjectSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_EffectObjectSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_EffectObjectSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_EffectObject_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - EffectCard -
        //生物效果——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_EffectCardTextSource_String = "";
        string QuickSave_EffectCardTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_EffectCardTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_" + _World_Manager._Config_Language_String + "_EffectCard.txt";
        if (File.Exists(QuickSave_EffectCardTextAssetCheck_String))
        {
            QuickSave_EffectCardTextSource_String = File.ReadAllText(QuickSave_EffectCardTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Effect/_" + _World_Manager._Config_Language_String + "_EffectCard.txt");
            QuickSave_EffectCardTextAssetCheck_String = Application.streamingAssetsPath + "/Effect/_TraditionalChinese_EffectCard.txt";
            QuickSave_EffectCardTextSource_String = File.ReadAllText(QuickSave_EffectCardTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_EffectCardSourceSplit = QuickSave_EffectCardTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_EffectCardSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_EffectCardSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_EffectCardSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_EffectCard_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void ManagerStart()
    {
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region GetEffect
    #region EffectObject
    //取得武器效果——————————————————————————————————————————————————————————————————————
    public List<string> GetEffectObject(string Key, int Count,
        SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        //持有者
        _Effect_EffectObjectUnit QuickSave_Effect_Script = null;
        int QuickSave_Increase_Int = 0;
        int QuickSave_BeforeStack_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //免疫判定/數字變動----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
            //免疫跳出
            if (!(QuickSave_Value_Int > 0))
            {
                return new List<string> { "Null｜" };
            }
        }
        //----------------------------------------------------------------------------------------------------

        //判定----------------------------------------------------------------------------------------------------
        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Value))
        {
            QuickSave_BeforeStack_Int = Value.Key_Stack("Default", null, null, null, null);
            int QuickSave_EffectStackLimit_Int = Value.Key_StackLimit();
            if (QuickSave_BeforeStack_Int + QuickSave_Value_Int >= QuickSave_EffectStackLimit_Int)
            {
                QuickSave_Increase_Int = QuickSave_EffectStackLimit_Int - QuickSave_BeforeStack_Int;
            }
            else
            {
                QuickSave_Increase_Int = QuickSave_Value_Int;
            }
            if (Action)
            {
                QuickSave_Effect_Script = Value;
                QuickSave_Effect_Script.StackIncrease(QuickSave_Value_Int);
            }
        }
        else
        {
            if (_Data_EffectObject_Dictionary.TryGetValue(Key, out EffectDataClass Effect))
            {
                if (QuickSave_Value_Int >= Effect.StackLimit)
                {
                    QuickSave_Increase_Int = Effect.StackLimit;
                }
                else
                {
                    QuickSave_Increase_Int = QuickSave_Value_Int;
                }
            }
            else
            {
                QuickSave_Increase_Int = 1;
            }
            if (Action)
            {
                //生成
                QuickSave_Effect_Script = Instantiate(_Effect_ObjectUnit_GameObject, 
                    TargetSource.Source_BattleObject._View_EffectStore_Transform).GetComponent<_Effect_EffectObjectUnit>();
                QuickSave_Effect_Script.SystemStart(TargetSource.Source_BattleObject, Key);
                QuickSave_Effect_Script.StackIncrease(QuickSave_Value_Int);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            //陣營設置
            if (!Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_GetEffectObject｜" + Key + ":" + QuickSave_Increase_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_GetEffectObject｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_GetEffectObject｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_GetEffectObject｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_GetEffectObject｜" + Key + ":" + QuickSave_Increase_Int);
                }
            }
            //得到效果後 效果
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key, QuickSave_BeforeStack_Int.ToString(), QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseGetEffect", QuickSave_Data_StringList/*效果,過往層數,應提升層數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "GetEffect", QuickSave_Data_StringList/*效果,過往層數,應提升層數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //降低武器效果——————————————————————————————————————————————————————————————————————
    public List<string> LostEffectObject(string Key, int Count, SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        int QuickSave_Decrease_Int = 0;
        int QuickSave_BeforeStack_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //免疫判定/數字變動----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("LostEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("LostEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
            //免疫跳出
            if (!(QuickSave_Value_Int > 0))
            {
                return new List<string> { "Null｜" };
            }
        }
        //----------------------------------------------------------------------------------------------------

        //判定----------------------------------------------------------------------------------------------------
        //判定是否有狀態
        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Value))
        {
            QuickSave_BeforeStack_Int = Value.Key_Stack("Default", null, null, null, null);
            if (QuickSave_Value_Int > QuickSave_BeforeStack_Int)
            {
                QuickSave_Decrease_Int = QuickSave_BeforeStack_Int;
            }
            else
            {
                QuickSave_Decrease_Int = QuickSave_Value_Int;
            }

            if (Action)
            {
                //取得位置
                Value.StackDecrease("Lost", QuickSave_Value_Int);
            }

            //----------------------------------------------------------------------------------------------------
            //陣營設置
            if (!Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_LostEffectObject｜" + Key + ":" + -QuickSave_Decrease_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_LostEffectObject｜" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_LostEffectObject｜" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_LostEffectObject｜" + Key + ":" + -QuickSave_Decrease_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_LostEffectObject｜" + Key + ":" + -QuickSave_Decrease_Int);
                }
            }
            //----------------------------------------------------------------------------------------------------
        }
        else
        {
            return Answer_Return_StringList;
        }
        if (QuickSave_Type_String == "EffectObject")
        {
            //得到效果後 效果
            List<string> QuickSave_Data_StringList =
                new List<string> { "Object", Key, QuickSave_BeforeStack_Int.ToString(), QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseLostEffect", QuickSave_Data_StringList/*效果,過往層數,應減少數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "LostEffect", QuickSave_Data_StringList/*效果,過往層數,應減少數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion EffectObject

    #region EffectCard
    //取得武器效果——————————————————————————————————————————————————————————————————————
    public List<string> GetEffectCard(string Key, int Count, 
        SourceClass UserSource, SourceClass TargetSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order, _Skill_EnchanceUnit EnchancePlace = null)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Type_String = Key.Split("_"[0])[0];
        if (TargetSource.Source_Card == null)
        {
            return Answer_Return_StringList;
        }
        //持有者
        _Effect_EffectCardUnit QuickSave_Effect_Script = null;
        int QuickSave_Increase_Int = 0;
        int QuickSave_Value_Int = Count;
        //----------------------------------------------------------------------------------------------------

        //免疫判定/數字變動----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            List<string> QuickSave_Data_StringList =
                new List<string> { "Card", Key };
            QuickSave_Value_Int += int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueAdd"][0]);
            QuickSave_Value_Int *= int.Parse(
                UserSource.Source_BattleObject.SituationCaller("GetEffectValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                 HateTarget, Action, Time, Order)["ValueMultiply"][0]);
        }
        //----------------------------------------------------------------------------------------------------

        //判定----------------------------------------------------------------------------------------------------
        if (_Data_EffectCard_Dictionary.TryGetValue(Key, out EffectDataClass Effect))
        {
            if (QuickSave_Value_Int >= Effect.StackLimit)
            {
                QuickSave_Increase_Int = Effect.StackLimit;
            }
            else
            {
                QuickSave_Increase_Int = QuickSave_Value_Int;
            }
        }
        else
        {
            QuickSave_Increase_Int = 1;
        }
        if (Action)
        {
            //生成
            QuickSave_Effect_Script = Instantiate(_Effect_CardUnit_GameObject, 
                TargetSource.Source_Card._View_EffectStore_Transform).GetComponent<_Effect_EffectCardUnit>();
            QuickSave_Effect_Script.SystemStart(TargetSource.Source_Card, Key, EnchancePlace);
            QuickSave_Effect_Script.StackIncrease(QuickSave_Increase_Int);
            if (EnchancePlace != null)
            {
                EnchancePlace._Owner_EnchanceEffectCard_Script = QuickSave_Effect_Script;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Type_String == "EffectObject")
        {
            //陣營設置
            if (Action)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                }
            }
            //得到效果後 效果
            List<string> QuickSave_Data_StringList =
                new List<string> { "Card", Key, "0", QuickSave_Value_Int.ToString() };
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            UserSource.Source_BattleObject.SituationCaller(
                "CauseGetEffect", QuickSave_Data_StringList/*效果,過往層數,應提升層數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
            Answer_Return_StringList.AddRange(
                _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
            TargetSource.Source_BattleObject.SituationCaller(
                "GetEffect", QuickSave_Data_StringList/*效果,過往層數,應提升層數*/,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)));
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion EffectCard
    #endregion GetEffect


    #region EffectAnim
    public IEnumerator EffectAnim(SourceClass Source, Dictionary<string, PathPreviewClass> PathPreview, bool CallNext)
    {
        //動畫
        yield return new WaitForSeconds(1f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));

        if (Source.Source_Card != null && PathPreview != null && CallNext)
        {
            Source.Source_Card.UseCardEffectEnd(PathPreview);
        }
    }
    #endregion EffectAnim
}
