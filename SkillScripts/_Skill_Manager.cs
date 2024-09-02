using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static System.Collections.Specialized.BitVector32;

public class _Skill_Manager : MonoBehaviour
{
    #region ElementBox
    #region - DataElement -
    //子物件集——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public GameObject _Skill_FactionUnit_GameObject;
    public GameObject _Skill_PassiveUnit_GameObject;
    public GameObject _Skill_SkillLeafUnit_GameObject;

    public Transform _View_ProjectStore_Transform;
    //----------------------------------------------------------------------------------------------------


    //資料源----------------------------------------------------------------------------------------------------
    //範圍資料庫
    public TextAsset _Data_RangeInput_TextAsset;
    public TextAsset _Data_PathInput_TextAsset;

    //流派資料庫
    public TextAsset _Data_FactionInput_TextAsset;
    //被動資料庫
    public TextAsset _Data_PassiveInput_TextAsset;
    //配朝資料庫
    public TextAsset _Data_SkillLeavesInput_TextAsset;
    //探索資料庫
    public TextAsset _Data_ExploreInput_TextAsset;
    //行為資料庫
    public TextAsset _Data_BehaviorInput_TextAsset;
    //附魔資料庫
    public TextAsset _Data_EnchanceInput_TextAsset;
    //----------------------------------------------------------------------------------------------------

    //索引----------------------------------------------------------------------------------------------------
    //選取索引
    public Dictionary<string, BoolRangeClass> _Data_Range_Dictionary = new Dictionary<string, BoolRangeClass>();
    public Dictionary<string, List<Vector>> _Data_Path_Dictionary = new Dictionary<string, List<Vector>>();
    //流派//各類起始招式
    public Dictionary<string, FactionDataClass> _Data_Faction_Dictionary = new Dictionary<string, FactionDataClass>();
    public Dictionary<string, LanguageClass> _Language_Faction_Dictionary = new Dictionary<string, LanguageClass>();
    //被動
    public Dictionary<string, PassiveDataClass> _Data_Passive_Dictionary = new Dictionary<string, PassiveDataClass>();
    public Dictionary<string, LanguageClass> _Language_Passive_Dictionary = new Dictionary<string, LanguageClass>();
    //配招
    public Dictionary<string, SkillLeavesDataClass> _Data_SkillLeaves_Dictionary = new Dictionary<string, SkillLeavesDataClass>();
    public Dictionary<string, LanguageClass> _Language_SkillLeaves_Dictionary = new Dictionary<string, LanguageClass>();
    //探索索引
    public Dictionary<string, ExploreDataClass> _Data_Explore_Dictionary = new Dictionary<string, ExploreDataClass>();
    public Dictionary<string, LanguageClass> _Language_Explore_Dictionary = new Dictionary<string, LanguageClass>();
    //行為索引
    public Dictionary<string, BehaviorDataClass> _Data_Behavior_Dictionary = new Dictionary<string, BehaviorDataClass>();
    public Dictionary<string, LanguageClass> _Language_Behavior_Dictionary = new Dictionary<string, LanguageClass>();
    //附魔索引
    public Dictionary<string, EnchanceDataClass> _Data_Enchance_Dictionary = new Dictionary<string, EnchanceDataClass>();
    public Dictionary<string, LanguageClass> _Language_Enchance_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - ClassElement -
    //——————————————————————————————————————————————————————————————————————
    //學習與持有----------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion ElementBox


    #region DictionarySet
    //各類匯入區——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------
    //流派資料
    [System.Serializable]
    [HideInInspector]
    public class FactionDataClass
    {
        public List<string> Syndrome;//變異
        public List<string> Passive;//被動
        public List<KeyValuePair<string,int>> SkillLeaves = new List<KeyValuePair<string, int>>();
        public List<string> SkillExtend;//額外技能(可學習)
    }

    [System.Serializable]
    [HideInInspector]
    public class SkillLeavesDataClass
    {
        public List<string> SpecialType;//特殊性(如：不會被丟棄至墓地)
        public List<string> Explore;
        public List<string> Behavior;
        public List<string> Enchance;
        public Dictionary<string,string> Special = new Dictionary<string, string>();//特殊(EX:Loading,XXX_XXX_XXX)
    }

    //被動資料
    [System.Serializable]
    [HideInInspector]
    public class PassiveDataClass : DataClass
    {
    }

    //選擇資料
    [System.Serializable]
    [HideInInspector]
    public class RangeDataClass : DataClass
    {
        public List<BoolRangeClass> Range = new List<BoolRangeClass>();
        public List<List<Vector>> Path = new List<List<Vector>>();
        public List<BoolRangeClass> Select = new List<BoolRangeClass>();
    }

    //探索資料
    [System.Serializable]
    [HideInInspector]
    public class ExploreDataClass : RangeDataClass
    {
        //作用類型
        public List<string> UseTag;
        public List<string> OwnTag;
        public int TimePass;
    }

    //行為資料
    [System.Serializable]
    [HideInInspector]
    public class BehaviorDataClass : RangeDataClass
    {
        public List<string> UseTag;
        public List<string> OwnTag;
        public List<string> ReactTag;
        public int Enchant;
        public int DelayBefore;
        public int DelayAfter;
    }

    //附魔資料
    [System.Serializable]
    [HideInInspector]
    public class EnchanceDataClass : RangeDataClass
    {
        public List<string> SupplyTag;
        public List<string> RequiredTag;
        public List<string> AddenTag;
        public List<string> RemoveTag;
        public int Enchant;
        public int DelayBefore;
        public int DelayAfter;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion DictionarySet


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Skill_Manager = this;
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
        #region - Range -
        //範圍----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_RangeSourceSplit = _Data_RangeInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_RangeSourceSplit.Length - 1; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            BoolRangeClass QuickSave_Data_Class = new BoolRangeClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_RangeSourceSplit[t].Split("–"[0]);
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
            //----------------------------------------------------------------------------------------------------

            //數字----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_RangeSplit_StringArray = QuickSave_Split_StringArray[1].Split("\r"[0]);

            //建立處存區
            int QuickSave_ArrayXSize_Int = 0;
            for (int a = 1; a < QuickSave_RangeSplit_StringArray.Length; a++)
            {
                if (QuickSave_RangeSplit_StringArray[a].Length > QuickSave_ArrayXSize_Int)
                {
                    QuickSave_ArrayXSize_Int = QuickSave_RangeSplit_StringArray[a].Length;
                }
            }
            bool[,] QuickSave_CoordinateSave_BoolArray = 
                new bool[QuickSave_ArrayXSize_Int - 1, QuickSave_RangeSplit_StringArray.Length - 2];

            //建立資料_Substring(X)代表由X開始
            for (int y = 1; y < QuickSave_RangeSplit_StringArray.Length - 1; y++)
            {
                if (QuickSave_RangeSplit_StringArray[y] == "")
                {
                    continue;
                }
                for (int x = 1; x < QuickSave_RangeSplit_StringArray[y].Length; x++)
                {
                    switch (QuickSave_RangeSplit_StringArray[y][x].ToString())
                    {
                        case "▽":
                            QuickSave_Data_Class.Center = new Vector2Int(x - 1, y - 1);
                            QuickSave_CoordinateSave_BoolArray[x - 1, y - 1] = false;
                            break;
                        case "◆":
                            QuickSave_CoordinateSave_BoolArray[x - 1, y - 1] = true;
                            break;
                        default:
                            QuickSave_CoordinateSave_BoolArray[x - 1, y - 1] = false;
                            break;
                    }
                }
            }
            QuickSave_Data_Class.Coordinate = QuickSave_CoordinateSave_BoolArray;
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            string QuickSave_Key_String = QuickSave_TextSplit_StringArray[1].Substring(5);
            _Data_Range_Dictionary.Add(QuickSave_Key_String + "_0", QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------

            //額外新增(選取項)----------------------------------------------------------------------------------------------------
            {
                BoolRangeClass QuickSave_NextData_Class = new BoolRangeClass();
                int QuickSave_LengthX_Int = QuickSave_Data_Class.Coordinate.GetLength(0);
                int QuickSave_LengthY_Int = QuickSave_Data_Class.Coordinate.GetLength(1);
                QuickSave_NextData_Class.Center = QuickSave_Data_Class.Center;
                QuickSave_NextData_Class.Coordinate = new bool[QuickSave_LengthX_Int, QuickSave_LengthY_Int];
                for (int x = 0; x < QuickSave_LengthX_Int; x++)
                {
                    for (int y = 0; y < QuickSave_LengthY_Int; y++)
                    {
                        QuickSave_NextData_Class.Coordinate[x, y] = QuickSave_Data_Class.Coordinate[x, y];
                    }
                }
                QuickSave_NextData_Class.Coordinate[QuickSave_NextData_Class.Center.x, QuickSave_NextData_Class.Center.y] = true;
                //置入索引
                _Data_Range_Dictionary.Add(QuickSave_Key_String + "_1", QuickSave_NextData_Class);
            }
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_RangeInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Path -
        //流派----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_PathSourceSplit_StringArray = _Data_PathInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_PathSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            List<Vector> QuickSave_Data_Vector = new List<Vector>();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_PathSourceSplit_StringArray[t].Split("–"[0]);
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
            string[] QuickSave_Path_StringArray = QuickSave_TextSplit_StringArray[2].Substring(6).Split("→"[0]);
            if (QuickSave_Path_StringArray[0].Length != 0)
            {
                for (int a = 1; a < QuickSave_Path_StringArray.Length; a++)
                {
                    string[] QuickSave_PathSplite_StringArray = QuickSave_Path_StringArray[a].Split(","[0]);
                    Vector QuickSave_Vector =
                        new Vector { x = int.Parse(QuickSave_PathSplite_StringArray[0]), y = int.Parse(QuickSave_PathSplite_StringArray[1]) };
                    QuickSave_Data_Vector.Add(QuickSave_Vector);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Path_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Vector);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_PathInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Faction -
        //流派----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_FactionSourceSplit_StringArray = _Data_FactionInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_FactionSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            FactionDataClass QuickSave_Data_Class = new FactionDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_FactionSourceSplit_StringArray[t].Split("–"[0]);
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
            QuickSave_Data_Class.Syndrome = new List<string>();
            if (QuickSave_TextSplit_StringArray[2].Substring(10) != "")
            {
                QuickSave_Data_Class.Syndrome = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(10).Split(","[0]));
            }
            QuickSave_Data_Class.Passive = new List<string>();
            if (QuickSave_TextSplit_StringArray[3].Substring(9) != "")
            {
                QuickSave_Data_Class.Passive = new List<string>(QuickSave_TextSplit_StringArray[3].Substring(9).Split(","[0]));
            }
            string[] QuickSave_SkillLeaves_StringArray = QuickSave_TextSplit_StringArray[4].Substring(13).Split(","[0]);
            if (QuickSave_SkillLeaves_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_SkillLeaves_StringArray.Length; a++)
                {
                    string[] QuickSave_LeavesPair_StringArray = QuickSave_SkillLeaves_StringArray[a].Split(":"[0]);
                    KeyValuePair<string, int> QuickSave_LeavesPair_Pair =
                        new KeyValuePair<string, int>(QuickSave_LeavesPair_StringArray[0], int.Parse(QuickSave_LeavesPair_StringArray[1]));
                    QuickSave_Data_Class.SkillLeaves.Add(QuickSave_LeavesPair_Pair);
                }
            }
            QuickSave_Data_Class.SkillExtend = 
                new List<string>(QuickSave_TextSplit_StringArray[5].Substring(13).Split(","[0]));
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Faction_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_FactionInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Passive -
        //卡片----------------------------------------------------------------------------------------------------
        //探索
        //分割為單項
        string[] QuickSave_PassiveSourceSplit_StringArray = _Data_PassiveInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_PassiveSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            PassiveDataClass QuickSave_Data_Class = new PassiveDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_PassiveSourceSplit_StringArray[t].Split("–"[0]);
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
            //QuickSave_Data_Class.Type = QuickSave_TextSplit_StringArray[2].Substring(6);
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

            //編號----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Passive_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_PassiveInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - SkillLeaves -
        //流派----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_SkillLeavesSourceSplit_StringArray = _Data_SkillLeavesInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_SkillLeavesSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            SkillLeavesDataClass QuickSave_Data_Class = new SkillLeavesDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_SkillLeavesSourceSplit_StringArray[t].Split("–"[0]);
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
            QuickSave_Data_Class.SpecialType = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(13).Split(","[0]));
            QuickSave_Data_Class.Explore = new List<string>(QuickSave_TextSplit_StringArray[3].Substring(9).Split(","[0]));
            QuickSave_Data_Class.Behavior = new List<string>(QuickSave_TextSplit_StringArray[4].Substring(10).Split(","[0]));
            QuickSave_Data_Class.Enchance = new List<string>(QuickSave_TextSplit_StringArray[5].Substring(10).Split(","[0]));
            List<string> QuickSave_Split_StringList = 
                new List<string>(QuickSave_TextSplit_StringArray[6].Substring(9).Split(","[0]));
            foreach (string Key in QuickSave_Split_StringList)
            {
                string[] QuickSave_SplitSplit_StringArray = Key.Split(":"[0]);
                QuickSave_Data_Class.Special.Add(QuickSave_SplitSplit_StringArray[0], QuickSave_SplitSplit_StringArray[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_SkillLeaves_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_SkillLeavesInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Explore -
        //卡片----------------------------------------------------------------------------------------------------
        //探索
        //分割為單項
        string[] QuickSave_ExploreSourceSplit_StringArray = _Data_ExploreInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_ExploreSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            ExploreDataClass QuickSave_Data_Class = new ExploreDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_ExploreSourceSplit_StringArray[t].Split("–"[0]);
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
            QuickSave_Data_Class.UseTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(8).Split(","[0]));
            QuickSave_Data_Class.OwnTag = new List<string>(QuickSave_TextSplit_StringArray[3].Substring(8).Split(","[0]));
            QuickSave_Data_Class.TimePass = int.Parse(QuickSave_TextSplit_StringArray[4].Substring(10));
            string[] QuickSave_Range_StringArray = QuickSave_TextSplit_StringArray[5].Substring(7).Split(","[0]);
            if (QuickSave_Range_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Range_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Range.Add(_Data_Range_Dictionary[QuickSave_Range_StringArray[a]]);
                }
            }
            string[] QuickSave_Path_StringArray = QuickSave_TextSplit_StringArray[6].Substring(6).Split(","[0]);
            if (QuickSave_Path_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Path_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Path.Add(_Data_Path_Dictionary[QuickSave_Path_StringArray[a]]);
                }
            }
            string[] QuickSave_Select_StringArray = QuickSave_TextSplit_StringArray[7].Substring(8).Split(","[0]);
            if (QuickSave_Select_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Select_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Select.Add(_Data_Range_Dictionary[QuickSave_Select_StringArray[a]]);
                }
            }
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

            //編號----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Explore_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_ExploreInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Behavior -
        //行為----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_BehaviorSourceSplit_StringArray = _Data_BehaviorInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_BehaviorSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            BehaviorDataClass QuickSave_Data_Class = new BehaviorDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_BehaviorSourceSplit_StringArray[t].Split("–"[0]);
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
            QuickSave_Data_Class.UseTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(8).Split(","[0]));
            QuickSave_Data_Class.OwnTag = new List<string>(QuickSave_TextSplit_StringArray[3].Substring(8).Split(","[0]));
            QuickSave_Data_Class.ReactTag = new List<string>(QuickSave_TextSplit_StringArray[4].Substring(10).Split(","[0]));
            QuickSave_Data_Class.Enchant = int.Parse(QuickSave_TextSplit_StringArray[5].Substring(9));
            QuickSave_Data_Class.DelayBefore = int.Parse(QuickSave_TextSplit_StringArray[6].Substring(13));
            QuickSave_Data_Class.DelayAfter = int.Parse(QuickSave_TextSplit_StringArray[7].Substring(12));
            string[] QuickSave_Range_StringArray = QuickSave_TextSplit_StringArray[8].Substring(7).Split(","[0]);
            if (QuickSave_Range_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Range_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Range.Add(_Data_Range_Dictionary[QuickSave_Range_StringArray[a]]);
                }
            }
            string[] QuickSave_Path_StringArray = QuickSave_TextSplit_StringArray[9].Substring(6).Split(","[0]);
            if (QuickSave_Path_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Path_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Path.Add(_Data_Path_Dictionary[QuickSave_Path_StringArray[a]]);
                }
            }
            string[] QuickSave_Select_StringArray = QuickSave_TextSplit_StringArray[10].Substring(8).Split(","[0]);
            if (QuickSave_Select_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Select_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Select.Add(_Data_Range_Dictionary[QuickSave_Select_StringArray[a]]);
                }
            }
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

            //編號----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Behavior_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_BehaviorInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Enchance -
        //附魔----------------------------------------------------------------------------------------------------
        //分割為單項
        string[] QuickSave_EnchanceSourceSplit_StringArray = _Data_EnchanceInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_EnchanceSourceSplit_StringArray.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            EnchanceDataClass QuickSave_Data_Class = new EnchanceDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_EnchanceSourceSplit_StringArray[t].Split("–"[0]);
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
            QuickSave_Data_Class.SupplyTag = new List<string>(QuickSave_TextSplit_StringArray[2].Substring(11).Split(","[0]));
            QuickSave_Data_Class.RequiredTag = new List<string>(QuickSave_TextSplit_StringArray[3].Substring(13).Split(","[0]));
            QuickSave_Data_Class.AddenTag = new List<string>(QuickSave_TextSplit_StringArray[4].Substring(10).Split(","[0]));
            QuickSave_Data_Class.RemoveTag = new List<string>(QuickSave_TextSplit_StringArray[5].Substring(11).Split(","[0]));
            QuickSave_Data_Class.Enchant = int.Parse(QuickSave_TextSplit_StringArray[6].Substring(9));
            QuickSave_Data_Class.DelayBefore = int.Parse(QuickSave_TextSplit_StringArray[7].Substring(13));
            QuickSave_Data_Class.DelayAfter = int.Parse(QuickSave_TextSplit_StringArray[8].Substring(12));
            string[] QuickSave_Range_StringArray = QuickSave_TextSplit_StringArray[9].Substring(7).Split(","[0]);
            if (QuickSave_Range_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Range_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Range.Add(_Data_Range_Dictionary[QuickSave_Range_StringArray[a]]);
                }
            }
            string[] QuickSave_Path_StringArray = QuickSave_TextSplit_StringArray[10].Substring(6).Split(","[0]);
            if (QuickSave_Path_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Path_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Path.Add(_Data_Path_Dictionary[QuickSave_Path_StringArray[a]]);
                }
            }
            string[] QuickSave_Select_StringArray = QuickSave_TextSplit_StringArray[11].Substring(8).Split(","[0]);
            if (QuickSave_Select_StringArray[0].Length != 0)
            {
                for (int a = 0; a < QuickSave_Select_StringArray.Length; a++)
                {
                    QuickSave_Data_Class.Select.Add(_Data_Range_Dictionary[QuickSave_Select_StringArray[a]]);
                }
            }
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

            //編號----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_KeySplit_StringArray = QuickSave_Split_StringArray[2].Split("\r"[0]);
            //建立資料
            for (int a = 1; a < QuickSave_KeySplit_StringArray.Length - 1; a++)
            {
                string[] QuickSave_KeyValue = QuickSave_KeySplit_StringArray[a].Split(":"[0]);
                QuickSave_Data_Class.Keys.Add(QuickSave_KeyValue[0].Substring(1), QuickSave_KeyValue[1]);
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Enchance_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_EnchanceInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————

    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        #region - Faction -
        //被動——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_FactionTextSource_String = "";
        string QuickSave_FactionTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_FactionTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_Faction.txt";
        if (File.Exists(QuickSave_FactionTextAssetCheck_String))
        {
            QuickSave_FactionTextSource_String = File.ReadAllText(QuickSave_FactionTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Faction.txt");
            QuickSave_FactionTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Faction.txt";
            QuickSave_FactionTextSource_String = File.ReadAllText(QuickSave_FactionTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_FactionSourceSplit = QuickSave_FactionTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_FactionSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_FactionSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_FactionSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Faction_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - Passive -
        //被動——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_PassiveTextSource_String = "";
        string QuickSave_PassiveTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_PassiveTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_Passive.txt";
        if (File.Exists(QuickSave_PassiveTextAssetCheck_String))
        {
            QuickSave_PassiveTextSource_String = File.ReadAllText(QuickSave_PassiveTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Passive.txt");
            QuickSave_PassiveTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Passive.txt";
            QuickSave_PassiveTextSource_String = File.ReadAllText(QuickSave_PassiveTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_PassiveSourceSplit = QuickSave_PassiveTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_PassiveSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_PassiveSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_PassiveSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Passive_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - SkillLeaves -
        //被動——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_SkillLeavesTextSource_String = "";
        string QuickSave_SkillLeavesTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_SkillLeavesTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_SkillLeaves.txt";
        if (File.Exists(QuickSave_SkillLeavesTextAssetCheck_String))
        {
            QuickSave_SkillLeavesTextSource_String = File.ReadAllText(QuickSave_SkillLeavesTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_SkillLeaves.txt");
            QuickSave_SkillLeavesTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_SkillLeaves.txt";
            QuickSave_SkillLeavesTextSource_String = File.ReadAllText(QuickSave_SkillLeavesTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_SkillLeavesSourceSplit = QuickSave_SkillLeavesTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_SkillLeavesSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_SkillLeavesSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_SkillLeavesSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_SkillLeaves_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - Explore -
        //探索——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_ExploreTextSource_String = "";
        string QuickSave_ExploreTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_ExploreTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_Explore.txt";
        if (File.Exists(QuickSave_ExploreTextAssetCheck_String))
        {
            QuickSave_ExploreTextSource_String = File.ReadAllText(QuickSave_ExploreTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Explore.txt");
            QuickSave_ExploreTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Explore.txt";
            QuickSave_ExploreTextSource_String = File.ReadAllText(QuickSave_ExploreTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_ExploreSourceSplit = QuickSave_ExploreTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_ExploreSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_ExploreSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //執行----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_ExploreSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Explore_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - Behavior -
        //行為——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_BehaviorTextSource_String = "";
        string QuickSave_BehaviorTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_BehaviorTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_Behavior.txt";
        if (File.Exists(QuickSave_BehaviorTextAssetCheck_String))
        {
            QuickSave_BehaviorTextSource_String = File.ReadAllText(QuickSave_BehaviorTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Behavior.txt");
            QuickSave_BehaviorTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Behavior.txt";
            QuickSave_BehaviorTextSource_String = File.ReadAllText(QuickSave_BehaviorTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_BehaviorSourceSplit = QuickSave_BehaviorTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_BehaviorSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_BehaviorSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //執行----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_BehaviorSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Behavior_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion

        #region - Enchance -
        //附魔——————————————————————————————————————————————————————————————————————
        //變數----------------------------------------------------------------------------------------------------
        string QuickSave_EnchanceTextSource_String = "";
        string QuickSave_EnchanceTextAssetCheck_String = "";
        //----------------------------------------------------------------------------------------------------

        //取得文檔----------------------------------------------------------------------------------------------------
        QuickSave_EnchanceTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_" + _World_Manager._Config_Language_String + "_Enchance.txt";
        if (File.Exists(QuickSave_EnchanceTextAssetCheck_String))
        {
            QuickSave_EnchanceTextSource_String = File.ReadAllText(QuickSave_EnchanceTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Skill/_" + _World_Manager._Config_Language_String + "_Enchance.txt");
            QuickSave_EnchanceTextAssetCheck_String = Application.streamingAssetsPath + "/Skill/_TraditionalChinese_Enchance.txt";
            QuickSave_EnchanceTextSource_String = File.ReadAllText(QuickSave_EnchanceTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_EnchanceSourceSplit = QuickSave_EnchanceTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_EnchanceSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_EnchanceSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //執行----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_EnchanceSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Enchance_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
        //——————————————————————————————————————————————————————————————————————
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet

    #region Anime
    #region - AttackAnim -
    public IEnumerator AttackAnim(SourceClass Source, Dictionary<string, PathPreviewClass> PathPreview, bool CallNext)
    {
        //----------------------------------------------------------------------------------------------------
        //動畫
        yield return new WaitForSeconds(1f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
        if (Source.Source_Card != null && PathPreview != null && CallNext)
        {
            Source.Source_Card.UseCardEffectEnd(PathPreview);
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion 
    #endregion

    #region StatusSkillSet
    #region - Faction -
    //流派設定(需刪除舊的)——————————————————————————————————————————————————————————————————————
    public _Skill_FactionUnit FactionStartSet(Transform Store,string Key,SourceClass Source)
    {
        //----------------------------------------------------------------------------------------------------
        _Skill_FactionUnit Answer_Return_Script = Instantiate(_Skill_FactionUnit_GameObject, Store).GetComponent<_Skill_FactionUnit>();
        Answer_Return_Script._View_Hint_Script.HintSet("New","Faction");
        if (!_Data_Faction_Dictionary.ContainsKey(Key))
        {
            print(Key);
        }
        FactionDataClass QuickSave_FactionData_Class = _Data_Faction_Dictionary[Key];
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_Script._Basic_Key_String = Key;
        Answer_Return_Script._Basic_Data_Class = QuickSave_FactionData_Class;
        Answer_Return_Script._Basic_Source_Class = Source;

        if (QuickSave_FactionData_Class.Passive != null)
        {
            foreach (string Passive in QuickSave_FactionData_Class.Passive)
            {
                _World_Manager._Effect_Manager.GetEffectObject(
                    Passive, 1,
                    Source, Source,
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            }
        }

        for (int a = 0; a < QuickSave_FactionData_Class.SkillLeaves.Count; a++)
        {
            string QuickSave_Key_String = QuickSave_FactionData_Class.SkillLeaves[a].Key;
            int QuickSave_Count_Int = QuickSave_FactionData_Class.SkillLeaves[a].Value;

            for (int b = 0; b < QuickSave_Count_Int; b++)
            {
                _UI_Card_Unit QuickSave_Card_Script = 
                    SkillLeafStartSet(Answer_Return_Script, QuickSave_Key_String);
                Answer_Return_Script.AddSkillLeaf(QuickSave_Card_Script, "Cards");
            }
        }

        //被動-額外招式設置
        foreach (_Effect_EffectObjectUnit Passive in Source.Source_BattleObject._Effect_Passive_Dictionary.Values)
        {
            Passive.Key_SkillExtend(Answer_Return_Script);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_Script._Basic_Language_Class = _Language_Faction_Dictionary[Key];
        Answer_Return_Script.ViewSet();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - PEBE StartSet -
    #region - SkillLeaf -
    //——————————————————————————————————————————————————————————————————————
    public _UI_Card_Unit SkillLeafStartSet(_Skill_FactionUnit Faction,string Key)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Card_Unit Answer_Return_Script = 
            Instantiate(_Skill_SkillLeafUnit_GameObject, Faction._Faction_SkillStore_Transform).GetComponentInChildren<_UI_Card_Unit>();
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        SourceClass QuickSave_FactionSource_Class = Faction._Basic_Source_Class;
        SkillLeavesDataClass QuickSave_Data_Class = _Data_SkillLeaves_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //取得資料
        Answer_Return_Script.name = "Card_Unit";
        Answer_Return_Script._Basic_Key_String = Key;
        SourceClass QuickSave_Source_Class  = new SourceClass
        {
            SourceType = "Card",
            Source_Concept = QuickSave_FactionSource_Class.Source_Concept,
            Source_Weapon = QuickSave_FactionSource_Class.Source_Weapon,
            Source_Item = QuickSave_FactionSource_Class.Source_Item,
            Source_MaterialData = QuickSave_FactionSource_Class.Source_MaterialData,
            Source_Creature = QuickSave_FactionSource_Class.Source_Creature,
            Source_FieldObject = QuickSave_FactionSource_Class.Source_FieldObject,
            Source_BattleObject = QuickSave_FactionSource_Class.Source_BattleObject,
            Source_Card = Answer_Return_Script
        };
        Answer_Return_Script._Basic_Source_Class = QuickSave_Source_Class;
        Answer_Return_Script._Card_OwnerFaction_Script = Faction;

        #region - SpecialType -
        foreach (string SpecialType in QuickSave_Data_Class.SpecialType)
        {
            if (SpecialType == "")
            {
                break;
            }
            _World_Manager._Effect_Manager.GetEffectCard(
                SpecialType, 1,
                QuickSave_Source_Class, QuickSave_Source_Class,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        }
        #endregion

        #region - Explore -
        {
            foreach (string DicKey in QuickSave_Data_Class.Explore)
            {
                _Skill_ExploreUnit QuickSave_Explore_Script = Answer_Return_Script.gameObject.AddComponent<_Skill_ExploreUnit>();
                string QuickSave_Key_String = DicKey;
                QuickSave_Explore_Script._Basic_Key_String = QuickSave_Key_String;
                if (!_Data_Explore_Dictionary.TryGetValue(QuickSave_Key_String, out ExploreDataClass DicValue))
                {
                    print(QuickSave_Key_String);
                }
                QuickSave_Explore_Script._Basic_Data_Class = _Data_Explore_Dictionary[QuickSave_Key_String];
                QuickSave_Explore_Script._Basic_Language_Class = _Language_Explore_Dictionary[QuickSave_Key_String];
                QuickSave_Explore_Script._Owner_Faction_Script = Faction;
                Answer_Return_Script._Card_ReplaceExplore_Dicitonary.Add(
                    QuickSave_Key_String, QuickSave_Explore_Script);
                QuickSave_Explore_Script._Owner_Card_Script = Answer_Return_Script;
                SourceClass QuickSave_DataSource_Class = new SourceClass
                {
                    SourceType = "Card",
                    Source_Concept = QuickSave_FactionSource_Class.Source_Concept,
                    Source_Weapon = QuickSave_FactionSource_Class.Source_Weapon,
                    Source_Item = QuickSave_FactionSource_Class.Source_Item,
                    Source_MaterialData = QuickSave_FactionSource_Class.Source_MaterialData,
                    Source_Creature = QuickSave_FactionSource_Class.Source_Creature,
                    Source_FieldObject = QuickSave_FactionSource_Class.Source_FieldObject,
                    Source_BattleObject = QuickSave_FactionSource_Class.Source_BattleObject,
                    Source_Card = Answer_Return_Script,
                    Source_NumbersData = QuickSave_Explore_Script._Basic_Data_Class.Numbers,
                    Source_KeysData = QuickSave_Explore_Script._Basic_Data_Class.Keys
                };
                QuickSave_Explore_Script._Basic_Source_Class = QuickSave_DataSource_Class;

                _Map_BattleRound.
                    _Round_TimesLimits_ClassList.Add(QuickSave_Explore_Script._Basic_TimesLimit_Class);
            }
            Answer_Return_Script._Card_ExploreUnit_Script = Answer_Return_Script._Card_ReplaceExplore_Dicitonary[QuickSave_Data_Class.Explore[0]];
        }
        #endregion

        #region - Behavior -
        {
            foreach (string DicKey in QuickSave_Data_Class.Behavior)
            {
                _Skill_BehaviorUnit QuickSave_Behavior_Script = Answer_Return_Script.gameObject.AddComponent<_Skill_BehaviorUnit>();
                string QuickSave_Key_String = DicKey;
                QuickSave_Behavior_Script._Basic_Key_String = QuickSave_Key_String;
                if (!_Data_Behavior_Dictionary.TryGetValue(QuickSave_Key_String, out BehaviorDataClass DicValue))
                {
                    print(QuickSave_Key_String);
                }
                QuickSave_Behavior_Script._Basic_Data_Class = _Data_Behavior_Dictionary[QuickSave_Key_String];
                QuickSave_Behavior_Script._Basic_Language_Class = _Language_Behavior_Dictionary[QuickSave_Key_String];
                QuickSave_Behavior_Script._Owner_Faction_Script = Faction;
                Answer_Return_Script._Card_ReplaceBehavior_Dicitonary.Add(
                    QuickSave_Key_String, QuickSave_Behavior_Script);
                QuickSave_Behavior_Script._Owner_Card_Script = Answer_Return_Script;
                SourceClass QuickSave_DataSource_Class = new SourceClass
                {
                    SourceType = "Card",
                    Source_Concept = QuickSave_FactionSource_Class.Source_Concept,
                    Source_Weapon = QuickSave_FactionSource_Class.Source_Weapon,
                    Source_Item = QuickSave_FactionSource_Class.Source_Item,
                    Source_MaterialData = QuickSave_FactionSource_Class.Source_MaterialData,
                    Source_Creature = QuickSave_FactionSource_Class.Source_Creature,
                    Source_FieldObject = QuickSave_FactionSource_Class.Source_FieldObject,
                    Source_BattleObject = QuickSave_FactionSource_Class.Source_BattleObject,
                    Source_Card = Answer_Return_Script,
                    Source_NumbersData = QuickSave_Behavior_Script._Basic_Data_Class.Numbers,
                    Source_KeysData = QuickSave_Behavior_Script._Basic_Data_Class.Keys
                };
                QuickSave_Behavior_Script._Basic_Source_Class = QuickSave_DataSource_Class;
                _Map_BattleRound.
                    _Round_TimesLimits_ClassList.Add(QuickSave_Behavior_Script._Basic_TimesLimit_Class);
            }
            Answer_Return_Script._Card_BehaviorUnit_Script = 
                Answer_Return_Script._Card_ReplaceBehavior_Dicitonary[QuickSave_Data_Class.Behavior[0]];
        }
        #endregion

        #region - Enchance -
        {
            _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
            foreach (string DicKey in QuickSave_Data_Class.Enchance)
            {
                _Skill_EnchanceUnit QuickSave_Enchance_Script = 
                    Answer_Return_Script.gameObject.AddComponent<_Skill_EnchanceUnit>();
                string QuickSave_Key_String = DicKey;
                QuickSave_Enchance_Script._Basic_Key_String = QuickSave_Key_String;
                QuickSave_Enchance_Script._Basic_Type_String = "Enchance";
                if (!_Data_Enchance_Dictionary.TryGetValue(QuickSave_Key_String, out EnchanceDataClass DicValue))
                {
                    print(QuickSave_Key_String);
                }
                QuickSave_Enchance_Script._Basic_Data_Class = _Data_Enchance_Dictionary[QuickSave_Key_String];
                QuickSave_Enchance_Script._Basic_Language_Class = _Language_Enchance_Dictionary[QuickSave_Key_String];
                QuickSave_Enchance_Script._Owner_Faction_Script = Faction;
                Answer_Return_Script._Card_ReplaceEnchance_Dicitonary.Add(
                    QuickSave_Key_String, QuickSave_Enchance_Script);
                QuickSave_Enchance_Script._Owner_Card_Script = Answer_Return_Script;
                SourceClass QuickSave_DataSource_Class = new SourceClass
                {
                    SourceType = "Card",
                    Source_Concept = QuickSave_FactionSource_Class.Source_Concept,
                    Source_Weapon = QuickSave_FactionSource_Class.Source_Weapon,
                    Source_Item = QuickSave_FactionSource_Class.Source_Item,
                    Source_MaterialData = QuickSave_FactionSource_Class.Source_MaterialData,
                    Source_Creature = QuickSave_FactionSource_Class.Source_Creature,
                    Source_FieldObject = QuickSave_FactionSource_Class.Source_FieldObject,
                    Source_BattleObject = QuickSave_FactionSource_Class.Source_BattleObject,
                    Source_Card = Answer_Return_Script,
                    Source_NumbersData = QuickSave_Enchance_Script._Basic_Data_Class.Numbers,
                    Source_KeysData = QuickSave_Enchance_Script._Basic_Data_Class.Keys
                };
                QuickSave_Enchance_Script._Basic_Source_Class = QuickSave_DataSource_Class;
                _Map_BattleRound.
                    _Round_TimesLimits_ClassList.Add(QuickSave_Enchance_Script._Basic_TimesLimit_Class);

                _Effect_Manager.GetEffectCard(QuickSave_Key_String, 1,
                    QuickSave_Source_Class, QuickSave_Source_Class,
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, 
                    QuickSave_Enchance_Script/*紀錄EffectCard*/);
            }
            Answer_Return_Script._Card_EnchanceUnit_Script = 
                Answer_Return_Script._Card_ReplaceEnchance_Dicitonary[QuickSave_Data_Class.Enchance[0]];
        }
        #endregion

        #region - Special -
        {
            _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
            foreach (string DicKey in QuickSave_Data_Class.Special.Keys)
            {
                _Skill_EnchanceUnit QuickSave_Special_Script =
                    Answer_Return_Script.gameObject.AddComponent<_Skill_EnchanceUnit>();
                string QuickSave_Key_String = QuickSave_Data_Class.Special[DicKey];
                QuickSave_Special_Script._Basic_Key_String = QuickSave_Key_String;
                QuickSave_Special_Script._Basic_Type_String = DicKey;
                QuickSave_Special_Script._Basic_Data_Class = _Data_Enchance_Dictionary[QuickSave_Key_String];
                QuickSave_Special_Script._Basic_Language_Class = _Language_Enchance_Dictionary[QuickSave_Key_String];
                QuickSave_Special_Script._Owner_Faction_Script = Faction;
                Answer_Return_Script._Card_SpecialUnit_Dictionary.Add(DicKey, QuickSave_Special_Script);
                QuickSave_Special_Script._Owner_Card_Script = Answer_Return_Script;
                SourceClass QuickSave_DataSource_Class = new SourceClass
                {
                    SourceType = "Card",
                    Source_Concept = QuickSave_FactionSource_Class.Source_Concept,
                    Source_Weapon = QuickSave_FactionSource_Class.Source_Weapon,
                    Source_Item = QuickSave_FactionSource_Class.Source_Item,
                    Source_MaterialData = QuickSave_FactionSource_Class.Source_MaterialData,
                    Source_Creature = QuickSave_FactionSource_Class.Source_Creature,
                    Source_FieldObject = QuickSave_FactionSource_Class.Source_FieldObject,
                    Source_BattleObject = QuickSave_FactionSource_Class.Source_BattleObject,
                    Source_Card = Answer_Return_Script,
                    Source_NumbersData = QuickSave_Special_Script._Basic_Data_Class.Numbers,
                    Source_KeysData = QuickSave_Special_Script._Basic_Data_Class.Keys
                };
                QuickSave_Special_Script._Basic_Source_Class = QuickSave_DataSource_Class;
                _Map_BattleRound.
                    _Round_TimesLimits_ClassList.Add(QuickSave_Special_Script._Basic_TimesLimit_Class);

                _Effect_Manager.GetEffectCard(QuickSave_Key_String, 1,
                    QuickSave_Source_Class, QuickSave_Source_Class,
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int,
                    QuickSave_Special_Script/*紀錄EffectCard*/);
            }
        }
        #endregion

        //隨機數字
        Answer_Return_Script._Round_Unit_Class = new RoundElementClass
        {
            DelayType = "Card",
            Source = Answer_Return_Script._Basic_Source_Class
        };
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion
    #endregion StatusSkillSet


    #region Duplicate
    public _Skill_FactionUnit DuplicateFaction(Transform Store, _Skill_FactionUnit Target)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Skill_FactionUnit Answer_Return_Script =
            Instantiate(_Skill_FactionUnit_GameObject, Store).GetComponent<_Skill_FactionUnit>();
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        Answer_Return_Script._Basic_Key_String = Target._Basic_Key_String;
        Answer_Return_Script._Basic_Source_Class = Target._Basic_Source_Class.DeepClone();
        Answer_Return_Script._Basic_Data_Class = Target._Basic_Data_Class.DeepClone();

        //及高機率Bug
        Answer_Return_Script._Faction_SkillLeaves_Dictionary = Target._Faction_SkillLeaves_Dictionary.DeepClone();
        Answer_Return_Script._Faction_AddenLeaves_ScriptsList = Target._Faction_AddenLeaves_ScriptsList.DeepClone();
        Answer_Return_Script._Faction_RemoveLeaves_ScriptsList = Target._Faction_RemoveLeaves_ScriptsList.DeepClone();
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion Duplicate

    #region Math
    #region - Set -
    //——————————————————————————————————————————————————————————————————————
    public List<string> TagsSet(List<string> Original, List<string> AddKeys = null, List<string> RemoveKeys = null)
    {
        HashSet<string> AnswerString = new HashSet<string>(Original);
        if (AddKeys != null && AddKeys.Count > 0)
        {
            foreach (string Key in AddKeys)
            {
                #region TagCheck
                switch (Key)
                {
                    case "Forward":
                    case "Backward":
                        {
                            AnswerString.Remove("Forward");
                            AnswerString.Remove("Backward");
                        }
                        break;
                    case "Upward":
                    case "Downward":
                        {
                            AnswerString.Remove("Upward");
                            AnswerString.Remove("Downward");
                        }
                        break;
                    case "Leftward":
                    case "Centerward":
                    case "Rightward":
                        {
                            AnswerString.Remove("Leftward");
                            AnswerString.Remove("Centerward");
                            AnswerString.Remove("Rightward");
                        }
                        break;
                    case "Inward":
                    case "Outward":
                        {
                            AnswerString.Remove("Inward");
                            AnswerString.Remove("Outward");
                        }
                        break;
                    case "Straightward":
                    case "Curvedward":
                        {
                            AnswerString.Remove("Straightward");
                            AnswerString.Remove("Curvedward");
                        }
                        break;

                    case "LightAttack":
                    case "HeavyAttack":
                    case "FinishAttack":
                        {
                            AnswerString.Remove("LightAttack");
                            AnswerString.Remove("HeavyAttack");
                            AnswerString.Remove("FinishAttack");
                        }
                        break;

                    case "ProgressiveMove":
                    case "InstantMove":
                    case "UniqueMove":
                        {
                            AnswerString.Remove("ProgressiveMove");
                            AnswerString.Remove("InstantMove");
                            AnswerString.Remove("UniqueMove");
                        }
                        break;

                    case "PushDisplace":
                    case "PullDisplace":
                        {
                            AnswerString.Remove("PushDisplace");
                            AnswerString.Remove("PullDisplace");
                        }
                        break;

                    case "DirectionRange":
                    case "OverallRange":
                        {
                            AnswerString.Remove("DirectionRange");
                            AnswerString.Remove("OverallRange");
                        }
                        break;

                    case "SlashDamage":
                    case "PunctureDamage":
                    case "ImpactDamage":
                    case "EnergyDamage":
                    case "ChaosDamage":
                    case "AbstractDamage":
                    case "StarkDamage":
                        {
                            AnswerString.Remove("SlashDamage");
                            AnswerString.Remove("PunctureDamage");
                            AnswerString.Remove("ImpactDamage");
                            AnswerString.Remove("EnergyDamage");
                            AnswerString.Remove("ChaosDamage");
                            AnswerString.Remove("AbstractDamage");
                            AnswerString.Remove("StarkDamage");
                        }
                        break;

                    case "Preparatory":
                    case "Burst":
                    case "Instantaneous":
                        {
                            AnswerString.Remove("Preparatory");
                            AnswerString.Remove("Burst");
                            AnswerString.Remove("Instantaneous");
                        }
                        break;

                    case "Soar":
                    case "Drop":
                        {
                            AnswerString.Remove("Soar");
                            AnswerString.Remove("Drop");
                        }
                        break;
                }
                #endregion
                AnswerString.Add(Key);
            }
        }
        if (RemoveKeys != null && RemoveKeys.Count > 0)
        {
            foreach (string Key in RemoveKeys)
            {
                AnswerString.Remove(Key);
            }
        }
        return new List<string>(AnswerString);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Check -
    //——————————————————————————————————————————————————————————————————————
    public bool TagContains(List<string> Checked, List<string> CheckKeys, bool NeedAllMatch)
    {
        bool OneMatch = false;
        bool AllMatch = true;
        foreach (string CheckKey in CheckKeys)
        {
            switch (CheckKey)
            {
                case "All":
                    return true;
                case "X":
                    return false;
                default:
                    {
                        if (CheckKey.Contains("("))
                        {
                            List<string> TagsSplit = new List<string>(CheckKey.Replace(")", "").Split("("[0]));
                            List<string> Tags = new List<string>(TagsSplit[1].Split("|"[0]));
                            switch (TagsSplit[0])
                            {
                                case "Select":
                                    {
                                        bool TagHave = false;
                                        for (int t = 0; t < Tags.Count; t++)
                                        {
                                            if (Checked.Contains(Tags[t]))
                                            {
                                                TagHave = true;
                                                break;
                                            }
                                        }
                                        if (TagHave)
                                        {
                                            //其中包含一個
                                            OneMatch = true;
                                        }
                                        else
                                        {
                                            //都不包含
                                            AllMatch = false;
                                        }
                                    }
                                    break;
                                case "Un":
                                    {
                                        bool TagHave = false;
                                        for (int t = 0; t < Tags.Count; t++)
                                        {
                                            if (Checked.Contains(Tags[t]))
                                            {
                                                TagHave = true;
                                                break;
                                            }
                                        }
                                        if (TagHave)
                                        {
                                            //其中包含一個
                                            AllMatch = false;
                                        }
                                        else
                                        {
                                            //都不包含
                                            OneMatch = true;
                                        }
                                    }
                                    break;
                                default:
                                    print("Fault");
                                    break;
                            }
                        }
                        else
                        {
                            if (!Checked.Contains(CheckKey))
                            {
                                AllMatch = false;
                            }
                            else
                            {
                                OneMatch = true;
                            }
                        }
                    }
                    break;
            }
        }
        if (NeedAllMatch)
        {
            return AllMatch;
        }
        else
        {
            return OneMatch;
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Math
}
