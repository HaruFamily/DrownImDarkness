using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using System.Linq;

public class _Object_Manager : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //資料源----------------------------------------------------------------------------------------------------
    //生物資料庫
    public TextAsset _Data_CreatureInput_TextAsset;
    //生物策略AI資料庫
    public TextAsset _Data_CreatureAIInput_TextAsset;
    //NPC生成編號分類
    public TextAsset _Data_NPCCreateInput_TextAsset;
    //物件
    public TextAsset _Data_ObjectInput_TextAsset;
    //----------------------------------------------------------------------------------------------------

    //索引----------------------------------------------------------------------------------------------------
    //生物索引
    public Dictionary<string, CreatureDataClass> _Data_Creature_Dictionary = new Dictionary<string, CreatureDataClass>();
    public Dictionary<string, LanguageClass> _Language_Creature_Dictionary = new Dictionary<string, LanguageClass>();
    //生物策略AI索引
    public Dictionary<string, Dictionary<string, float>> _Data_CreatureAI_Dictionary = new Dictionary<string, Dictionary<string, float>>();
    //NPC生成分類
    public Dictionary<string, NPCCreateDataClass> _Data_NPCCreate_Dictionary = new Dictionary<string, NPCCreateDataClass>();
    //物件
    public Dictionary<string, ObjectDataClass> _Data_Object_Dictionary = new Dictionary<string, ObjectDataClass>();
    public Dictionary<string, LanguageClass> _Language_Object_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //玩家----------------------------------------------------------------------------------------------------
    //戰鬥玩家目錄
    public _Object_CreatureUnit _Object_Player_Script;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public List<_Object_CreatureUnit> _Object_NPCs_ScriptsList = new List<_Object_CreatureUnit>();

    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    //----------------------------------------------------------------------------------------------------


    //置入區----------------------------------------------------------------------------------------------------
    //生物生成物件
    public GameObject _Object_CreatureUnit_GameObject;
    //引導生成物件(純顯示)
    public GameObject _Object_GuideUnit_GameObject;
    //存放區
    public Transform _Object_PlayerObjectStore_Transform;
    public Transform _Object_NPCStore_Transform;
    public Transform _Object_ObjectStore_Transform;
    public Transform _Object_TimePosStore_Transform;
    public Transform _Object_ProjectStore_Transform;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region DictionarySet
    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------

    //生物資料
    public class CreatureDataClass
    {
        public string Sect;

        public string ConceptFaction;
        public List<CustomRecipeMakeClass> Concept = new List<CustomRecipeMakeClass>();
        public List<string> WeaponFaction = new List<string>();
        public List<CustomRecipeMakeClass> Weapon = new List<CustomRecipeMakeClass>();
        public List<CustomRecipeMakeClass> Item = new List<CustomRecipeMakeClass>();
    }

    public class CustomRecipeMakeClass
    {
        public string Target;

        public List<string> MaterialKey = new List<string>();
        public List<string> MaterialStatus = new List<string>();
        public List<string[]> MaterialSpecialAffix = new List<string[]>();

        public List<KeyValuePair<string,int>> Process = new List<KeyValuePair<string, int>>();
    }

    //NPC生成資料
    public class NPCCreateDataClass
    {
        public List<string> CreatureKey = new List<string>();
    }

    //物件
    public class ObjectDataClass : DataClass
    {
        public List<string> Faction = new List<string>();
        public List<string> Tag = new List<string>();
        public List<int> StatusData = new List<int>();
        public List<int> MaterialData = new List<int>();
        public string[] SpecialAffix = new string[4];
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion DictionarySet


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Object_Manager = this;
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


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void ManagerStart()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Player_Script._Player_Script.SystemStart();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region DataBaseSet
    //——————————————————————————————————————————————————————————————————————
    private void DataSet()
    {
        #region - CreatureData -
        //生物----------------------------------------------------------------------------------------------------
        string[] QuickSave_CreatureSourceSplit = _Data_CreatureInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_CreatureSourceSplit.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            CreatureDataClass QuickSave_Data_Class = new CreatureDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_CreatureSourceSplit[t].Split("–"[0]);
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
            QuickSave_Data_Class.Sect = QuickSave_TextSplit_StringArray[2].Substring(6);
            //----------------------------------------------------------------------------------------------------

            //道具----------------------------------------------------------------------------------------------------
            string[] QuickSave_ConceptTextSplit = QuickSave_Split_StringArray[1].Split("-"[0]);
            if (QuickSave_ConceptTextSplit.Length >= 1)
            {
                for (int r = 1; r < QuickSave_ConceptTextSplit.Length; r++)
                {
                    CustomRecipeMakeClass QuickSave_CustomMake_Class = new CustomRecipeMakeClass();
                    string[] QuickSave_MaterialTextSplit = QuickSave_ConceptTextSplit[r].Split("\r"[0]);
                    QuickSave_Data_Class.ConceptFaction = QuickSave_MaterialTextSplit[1].Substring(9);
                    QuickSave_CustomMake_Class.Target = QuickSave_MaterialTextSplit[2].Substring(8);
                    for (int M = 3; M < QuickSave_MaterialTextSplit.Length; M++)
                    {
                        string[] QuickSave_RecipeUnit = QuickSave_MaterialTextSplit[M].Replace("\n", "").Split(":"[0]);
                        switch (QuickSave_RecipeUnit[0])
                        {
                            case "Material":
                                QuickSave_CustomMake_Class.MaterialKey.Add(QuickSave_RecipeUnit[1]);
                                QuickSave_CustomMake_Class.MaterialStatus.Add(QuickSave_RecipeUnit[2]);
                                QuickSave_CustomMake_Class.MaterialSpecialAffix.Add(QuickSave_RecipeUnit[3].Split(","[0]));
                                break;
                            case "Process":
                                KeyValuePair<string, int> QuickSave_Process_Pair = new KeyValuePair<string, int>(QuickSave_RecipeUnit[1], int.Parse(QuickSave_RecipeUnit[2]));
                                QuickSave_CustomMake_Class.Process.Add(QuickSave_Process_Pair);
                                break;
                        }
                    }
                    QuickSave_Data_Class.Concept.Add(QuickSave_CustomMake_Class);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //武器與流派----------------------------------------------------------------------------------------------------
            string[] QuickSave_WeaponTextSplit = QuickSave_Split_StringArray[2].Split("-"[0]);
            if (QuickSave_WeaponTextSplit.Length>=1)
            {
                for (int r = 1; r < QuickSave_WeaponTextSplit.Length; r++)
                {
                    CustomRecipeMakeClass QuickSave_CustomMake_Class = new CustomRecipeMakeClass();
                    string[] QuickSave_MaterialTextSplit = QuickSave_WeaponTextSplit[r].Split("\r"[0]);
                    QuickSave_Data_Class.WeaponFaction.Add(QuickSave_MaterialTextSplit[1].Substring(9));
                    QuickSave_CustomMake_Class.Target = QuickSave_MaterialTextSplit[2].Substring(8);
                    for (int M = 3; M < QuickSave_MaterialTextSplit.Length; M++)
                    {
                        string[] QuickSave_RecipeUnit = QuickSave_MaterialTextSplit[M].Replace("\n","").Split(":"[0]);
                        switch (QuickSave_RecipeUnit[0])
                        {
                            case "Material":
                                QuickSave_CustomMake_Class.MaterialKey.Add(QuickSave_RecipeUnit[1]);
                                QuickSave_CustomMake_Class.MaterialStatus.Add(QuickSave_RecipeUnit[2]);
                                QuickSave_CustomMake_Class.MaterialSpecialAffix.Add(QuickSave_RecipeUnit[3].Split(","[0]));
                                break;
                            case "Process":
                                KeyValuePair<string, int> QuickSave_Process_Pair = new KeyValuePair<string, int> (QuickSave_RecipeUnit[1], int.Parse(QuickSave_RecipeUnit[2]));
                                QuickSave_CustomMake_Class.Process.Add(QuickSave_Process_Pair);
                                break;
                        }
                    }
                    QuickSave_Data_Class.Weapon.Add(QuickSave_CustomMake_Class);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //道具----------------------------------------------------------------------------------------------------
            string[] QuickSave_ItemTextSplit = QuickSave_Split_StringArray[3].Split("-"[0]);
            if (QuickSave_ItemTextSplit.Length >= 1)
            {
                for (int r = 1; r < QuickSave_ItemTextSplit.Length; r++)
                {
                    CustomRecipeMakeClass QuickSave_CustomMake_Class = new CustomRecipeMakeClass();
                    string[] QuickSave_MaterialTextSplit = QuickSave_ItemTextSplit[r].Split("\r"[0]);
                    QuickSave_CustomMake_Class.Target = QuickSave_MaterialTextSplit[1].Substring(8);
                    for (int M = 2; M < QuickSave_MaterialTextSplit.Length; M++)
                    {
                        string[] QuickSave_RecipeUnit = QuickSave_MaterialTextSplit[M].Replace("\n", "").Split(":"[0]);
                        switch (QuickSave_RecipeUnit[0])
                        {
                            case "Material":
                                QuickSave_CustomMake_Class.MaterialKey.Add(QuickSave_RecipeUnit[1]);
                                QuickSave_CustomMake_Class.MaterialStatus.Add(QuickSave_RecipeUnit[2]);
                                QuickSave_CustomMake_Class.MaterialSpecialAffix.Add(QuickSave_RecipeUnit[3].Split(","[0]));
                                break;
                            case "Process":
                                KeyValuePair<string, int> QuickSave_Process_Pair = 
                                    new KeyValuePair<string, int>(QuickSave_RecipeUnit[1], int.Parse(QuickSave_RecipeUnit[2]));
                                QuickSave_CustomMake_Class.Process.Add(QuickSave_Process_Pair);
                                break;
                        }
                    }
                    QuickSave_Data_Class.Item.Add(QuickSave_CustomMake_Class);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Creature_Dictionary.Add(QuickSave_TextSplit_StringArray[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_CreatureInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - CreatureAIData -
        //生物----------------------------------------------------------------------------------------------------
        //AI
        string[] QuickSave_CreatureAISourceSplit = _Data_CreatureAIInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_CreatureAISourceSplit.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空資料
            Dictionary<string, float> QuickSave_AIData_Dictionary = new Dictionary<string, float>();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_CreatureAISourceSplit[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單項
            string[] QuickSave_AIDataSourceSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            for (int s = 2; s < QuickSave_AIDataSourceSplit.Length; s++)
            {
                if (QuickSave_AIDataSourceSplit[s] == "" || QuickSave_AIDataSourceSplit[s] == "\n")
                {
                    continue;
                }
                string[] QuickSave_TextSplit = QuickSave_AIDataSourceSplit[s].Substring(1).Split(":"[0]);
                //置入索引
                QuickSave_AIData_Dictionary.Add(QuickSave_TextSplit[0], float.Parse(QuickSave_TextSplit[1]));
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_CreatureAI_Dictionary.Add(QuickSave_AIDataSourceSplit[1].Substring(5), QuickSave_AIData_Dictionary);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_CreatureAIInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - NPCCreate -
        //NPC生成----------------------------------------------------------------------------------------------------
        string[] QuickSave_NPCCreateSourceSplit = _Data_NPCCreateInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_NPCCreateSourceSplit.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空資料
            NPCCreateDataClass QuickSave_Data_StringList = new NPCCreateDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_NPCCreateSourceSplit[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            //分割為單項
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_CreatureKey_StringArray = QuickSave_TextSplit[2].Substring(13).Split(","[0]);
            if (QuickSave_CreatureKey_StringArray[0].Length != 0)
            {
                for (int b = 0; b < QuickSave_CreatureKey_StringArray.Length; b++)
                {
                    string[] QuickSave_Creature_StringArray = QuickSave_CreatureKey_StringArray[b].Split(":"[0]);
                    int QuickSave_Count_Int = int.Parse(QuickSave_Creature_StringArray[1]);
                    for (int c = 0; c < QuickSave_Count_Int; c++)
                    {
                        QuickSave_Data_StringList.CreatureKey.Add(QuickSave_Creature_StringArray[0]);
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_NPCCreate_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Data_StringList);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_NPCCreateInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Object -
        //武器----------------------------------------------------------------------------------------------------
        List<string> QuickSave_ObjectSourceSplit = new List<string>();
        QuickSave_ObjectSourceSplit.AddRange(_Data_ObjectInput_TextAsset.text.Split("—"[0]));
        for (int t = 0; t < QuickSave_ObjectSourceSplit.Count; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            //空類別
            ObjectDataClass QuickSave_Data_Class = new ObjectDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_ObjectSourceSplit[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            string[] QuickSave_TextSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_SubSplite_StringArray = null;

            QuickSave_Data_Class.Faction = new List<string>(QuickSave_TextSplit[2].Substring(9).Split(","[0]));
            QuickSave_Data_Class.Tag = new List<string>(QuickSave_TextSplit[3].Substring(5).Split(","[0]));
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[4].Substring(12).Split(","[0]);
            QuickSave_Data_Class.StatusData = new List<int>();
            foreach (string Value in QuickSave_SubSplite_StringArray)
            {
                QuickSave_Data_Class.StatusData.Add(int.Parse(Value));
            }
            QuickSave_SubSplite_StringArray = QuickSave_TextSplit[5].Substring(14).Split(","[0]);
            QuickSave_Data_Class.MaterialData = new List<int>();
            foreach (string Value in QuickSave_SubSplite_StringArray)
            {
                QuickSave_Data_Class.MaterialData.Add(int.Parse(Value));
            }
            QuickSave_Data_Class.SpecialAffix = QuickSave_TextSplit[6].Substring(14).Split(","[0]);
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
            _Data_Object_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Data_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_ObjectInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————

    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        #region - Creature -
        //取得起始文本----------------------------------------------------------------------------------------------------
        //生物
        string QuickSave_CreatureTextSource_String = "";
        string QuickSave_CreatureTextAssetCheck_String = "";
        QuickSave_CreatureTextAssetCheck_String = Application.streamingAssetsPath + "/Object/_" + _World_Manager._Config_Language_String + "_Creature.txt";
        if (File.Exists(QuickSave_CreatureTextAssetCheck_String))
        {
            QuickSave_CreatureTextSource_String = File.ReadAllText(QuickSave_CreatureTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Object/_" + _World_Manager._Config_Language_String + "_Creature.txt");
            QuickSave_CreatureTextAssetCheck_String = Application.streamingAssetsPath + "/Object/_TraditionalChinese_Creature.txt";
            QuickSave_CreatureTextSource_String = File.ReadAllText(QuickSave_CreatureTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //讀取文檔----------------------------------------------------------------------------------------------------
        string[] QuickSave_CreatureSourceSplit = QuickSave_CreatureTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_CreatureSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_CreatureSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_CreatureSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Creature_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Object -
        string QuickSave_ObjectTextSource_String = "";
        string QuickSave_ObjectTextAssetCheck_String = "";
        QuickSave_ObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Object/_" + _World_Manager._Config_Language_String + "_Object.txt";
        if (File.Exists(QuickSave_ObjectTextAssetCheck_String))
        {
            QuickSave_ObjectTextSource_String = File.ReadAllText(QuickSave_ObjectTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Object/_" + _World_Manager._Config_Language_String + "_Object.txt");
            QuickSave_ObjectTextAssetCheck_String = Application.streamingAssetsPath + "/Object/_TraditionalChinese_Object.txt";
            QuickSave_ObjectTextSource_String = File.ReadAllText(QuickSave_ObjectTextAssetCheck_String);
        }
        //分割為單項
        string[] QuickSave_ObjectSourceSplit = QuickSave_ObjectTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_ObjectSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_ObjectSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_ObjectSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Object_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
            //----------------------------------------------------------------------------------------------------
        }
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet

    #region Object
    //基礎設置——————————————————————————————————————————————————————————————————————
    public void CreatureStartSet(_Object_CreatureUnit Owner, string Key)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        CreatureDataClass QuickSave_CreatureData_Class = _Data_Creature_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //編號、派系----------------------------------------------------------------------------------------------------
        Owner._Basic_Key_String = Key;
        Owner._Data_Sect_String = QuickSave_CreatureData_Class.Sect;
        Owner._Basic_Language_Class = _Language_Creature_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //起始概念
        CustomRecipeMakeClass QuickSave_ConceptUnit_Class = QuickSave_CreatureData_Class.Concept[0];
        _Item_ConceptUnit QuickSave_Concept_Script =
            _Item_Manager.ConceptStartSet(Owner, QuickSave_ConceptUnit_Class.Target, 1, false, CustomMake: QuickSave_ConceptUnit_Class)[0];
        Owner._Object_Inventory_Script.InventorySet("Equip", QuickSave_Concept_Script);
        QuickSave_Concept_Script._Basic_Object_Script.FactionSet(QuickSave_CreatureData_Class.ConceptFaction);
        //起始裝備
        for (int a = 0; a < QuickSave_CreatureData_Class.Weapon.Count; a++)
        {
            CustomRecipeMakeClass QuickSave_WeaponUnit_Class = QuickSave_CreatureData_Class.Weapon[a];
            _Item_WeaponUnit QuickSave_Weapon_Script =
                _Item_Manager.WeaponStartSet(Owner, QuickSave_WeaponUnit_Class.Target, 1, false, CustomMake: QuickSave_WeaponUnit_Class)[0];
            Owner._Object_Inventory_Script.InventorySet("Equip", QuickSave_Weapon_Script);
            QuickSave_Weapon_Script._Basic_Object_Script.FactionSet(QuickSave_CreatureData_Class.WeaponFaction[a]);
        }
        //起始道具
        for (int a = 0; a < QuickSave_CreatureData_Class.Item.Count; a++)
        {
            CustomRecipeMakeClass QuickSave_ItemUnit_Class = QuickSave_CreatureData_Class.Item[a];
            _Item_ItemUnit QuickSave_Item_Script =
                _Item_Manager.ItemStartSet(Owner, QuickSave_ItemUnit_Class.Target, 1, false, CustomMake: QuickSave_ItemUnit_Class)[0];
            Owner._Object_Inventory_Script.InventorySet("Equip", QuickSave_Item_Script);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //進階設定——————————————————————————————————————————————————————————————————————
    public void CreatureStatusAdvanceSet(SourceClass UserSource, List<string> ChangeStatus)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit QuickSave_Object_Script = UserSource.Source_BattleObject;
        _Object_CreatureUnit QuickSave_Creature_Script = UserSource.Source_Creature;
        Dictionary<string, NumbericalValueClass> QuickSave_Point_Dicitonary = QuickSave_Object_Script._Basic_Point_Dictionary;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        foreach (string Key in ChangeStatus)
        {
            switch (Key)
            {
                case "Medium":
                    {
                        int QuickSave_Scale_Int = 5;
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(QuickSave_Object_Script.
                            Key_Status("Medium", UserSource, null, null));
                        float QuickSave_Substract_Float = 
                            QuickSave_Status_Int * QuickSave_Scale_Int - 
                            QuickSave_Point_Dicitonary["MediumPoint"].Default;
                        QuickSave_Point_Dicitonary["MediumPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        QuickSave_Point_Dicitonary["MediumPoint"].Point += 
                            QuickSave_Substract_Float;
                        QuickSave_Object_Script.MediumPointView();

                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_EquipStatus_ClassArray[0].Default = 
                            50 + QuickSave_Status_Int * 5f;
                    }
                    break;
                case "Catalyst"://最大生命值視觸媒而定
                    {
                        int QuickSave_Scale_Int = 5;
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(QuickSave_Object_Script.
                            Key_Status("Catalyst", UserSource, null, null));
                        float QuickSave_Substract_Float = 
                            QuickSave_Status_Int * QuickSave_Scale_Int - 
                            QuickSave_Point_Dicitonary["CatalystPoint"].Default;
                        QuickSave_Point_Dicitonary["CatalystPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        QuickSave_Point_Dicitonary["CatalystPoint"].Point += 
                            QuickSave_Substract_Float;
                        QuickSave_Object_Script.MediumPointView();
                        QuickSave_Object_Script.CatalystPointView();

                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_EquipStatus_ClassArray[3].Default = 
                            50 + QuickSave_Status_Int * 5f;
                    }
                    break;
                case "Consciousness":
                    {
                        int QuickSave_Scale_Int = 1;
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(QuickSave_Object_Script.
                            Key_Status("Consciousness", UserSource, null, null));
                        float QuickSave_Substract_Float = 
                            QuickSave_Status_Int * QuickSave_Scale_Int - 
                            QuickSave_Point_Dicitonary["ConsciousnessPoint"].Default;
                        QuickSave_Point_Dicitonary["ConsciousnessPoint"].Default = 
                            QuickSave_Status_Int * QuickSave_Scale_Int;
                        QuickSave_Point_Dicitonary["ConsciousnessPoint"].Point += 
                            QuickSave_Substract_Float;
                        QuickSave_Object_Script.ConsciousnessPointView();
                    }
                    break;
                case "Strength":
                    {
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(QuickSave_Object_Script.
                            Key_Status("Strength", UserSource, null, null));
                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_EquipStatus_ClassArray[2].Default = 
                            50 + QuickSave_Status_Int * 5f;
                    }
                    break;
                case "Precision":
                    {
                        int QuickSave_Status_Int = 
                            Mathf.RoundToInt(QuickSave_Object_Script.
                            Key_Status("Precision", UserSource, null, null));
                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_EquipStatus_ClassArray[1].Default = 
                            50 + QuickSave_Status_Int * 5f;
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //通常物件生成——————————————————————————————————————————————————————————————————————
    public _Map_BattleObjectUnit ObjectSet(string Type,string Key, Vector SpawnPoint, SourceClass SupportSource,
        int Time,int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit Answer_Object_Script = null;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Creature -
            case "Creature":
                {
                    //----------------------------------------------------------------------------------------------------
                    _Object_CreatureUnit QuickSave_Creature_Script =
                        Instantiate(_Object_CreatureUnit_GameObject, _Object_NPCStore_Transform).GetComponent<_Object_CreatureUnit>();
                    QuickSave_Creature_Script._NPC_Script.SystemStart(Key);
                    QuickSave_Creature_Script._NPC_Script.FieldBattleStartSet();

                    Vector QuickSave_StartCooridnate_Class = new Vector(
                        Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.x),
                        Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.y));
                    _Map_BattleGroundUnit QuickSave_StartGround_Script =
                        _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                        [QuickSave_StartCooridnate_Class.X, QuickSave_StartCooridnate_Class.Y];

                    while (!_Map_Manager._Map_MapCheck_Bool(QuickSave_StartCooridnate_Class,
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0),
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1)) ||
                            !QuickSave_StartGround_Script.
                            StayCheck("Stay", QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null,
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int))
                    {
                        QuickSave_StartCooridnate_Class = new Vector(
                            Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.x),
                            Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.y));
                        QuickSave_StartGround_Script =
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                            [QuickSave_StartCooridnate_Class.X, QuickSave_StartCooridnate_Class.Y];
                    }

                    QuickSave_Creature_Script._NPC_Script.FieldBattleEndSet(QuickSave_StartCooridnate_Class);
                    Answer_Object_Script = QuickSave_Creature_Script._Basic_Object_Script;
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion

            #region - Object -
            case "Object":
                {
                    //----------------------------------------------------------------------------------------------------
                    ObjectDataClass QuickSave_Data_Class = 
                        _Data_Object_Dictionary[Key];
                    //----------------------------------------------------------------------------------------------------

                    //----------------------------------------------------------------------------------------------------
                    if (!_Map_Manager._Map_MapCheck_Bool(SpawnPoint,
                        _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0),
                        _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1)))
                    {
                        break;
                    }

                    Answer_Object_Script =
                        Instantiate(_Object_GuideUnit_GameObject, _Object_ObjectStore_Transform).GetComponent<_Map_BattleObjectUnit>();
                    SourceClass QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "Object"
                    };
                    if (SupportSource != null)
                    {
                        QuickSave_Source_Class.Source_Creature = SupportSource.Source_Creature;
                        QuickSave_Source_Class.Source_Concept = SupportSource.Source_Concept;
                        QuickSave_Source_Class.Source_Weapon = SupportSource.Source_Weapon;
                        QuickSave_Source_Class.Source_Item = SupportSource.Source_Item;
                        QuickSave_Source_Class.Source_Card = SupportSource.Source_Card;
                        QuickSave_Source_Class.Source_BattleObject = Answer_Object_Script;
                    }
                    Answer_Object_Script.SystemStart(Key, QuickSave_Source_Class,
                        QuickSave_Data_Class, _Language_Object_Dictionary[Key]);

                    List<string> QuickSave_Avdance_StringList =
                        new List<string> { "Medium", "Catalyst", "Consciousness" };
                    _World_Manager._Item_Manager.ObjectStatusAdvanceSet(Answer_Object_Script, QuickSave_Avdance_StringList);

                    if (!_Map_BattleCreator._Map_GroundBoard_ScriptsArray[SpawnPoint.X, SpawnPoint.Y].
                        StayCheck("Stay", Answer_Object_Script._Basic_Source_Class, null, Time, Order))
                    {
                        //刪除
                        _Basic_SaveData_Class.
                            ObjectListDataRemove(Type, Answer_Object_Script);
                        Destroy(Answer_Object_Script.gameObject);
                        Answer_Object_Script = null;
                        break;
                    }
                    else
                    {
                        //生成
                        _World_Manager._Map_Manager._Map_MoveManager.
                            Spawn(SpawnPoint, QuickSave_Source_Class,
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion

            #region - TimePos -
            case "TimePos":
                {
                    //----------------------------------------------------------------------------------------------------
                    if (SupportSource == null)
                    {
                        print("Need SupportSource");
                        break;
                    }
                    //----------------------------------------------------------------------------------------------------

                    //----------------------------------------------------------------------------------------------------
                    _Map_BattleObjectUnit QuickSave_Object_Script = SupportSource.Source_BattleObject;
                    SourceClass QuickSave_Source_Class = new SourceClass
                    {
                        SourceType = "TimePos",
                        Source_Creature = SupportSource.Source_Creature,
                        Source_Card = SupportSource.Source_Card,
                        Source_BattleObject = QuickSave_Object_Script
                    };
                    List<string> QuickSave_Tag_StringList =
                        QuickSave_Object_Script.Key_Tag(QuickSave_Object_Script._Basic_Source_Class, QuickSave_Source_Class);
                    ObjectDataClass QuickSave_Data_Class = new ObjectDataClass
                    {
                        Tag = QuickSave_Tag_StringList,
                        StatusData = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },
                        MaterialData = new List<int> { 0, 0, 0, 0},
                        SpecialAffix = new string[4] { "","","",""}
                    };

                    Answer_Object_Script =
                        Instantiate(_Object_GuideUnit_GameObject, _Object_TimePosStore_Transform).GetComponent<_Map_BattleObjectUnit>();

                    Answer_Object_Script.SystemStart("TimePos",QuickSave_Source_Class, 
                        QuickSave_Data_Class, QuickSave_Object_Script._Basic_Language_Class);
                    _World_Manager._Map_Manager._Map_MoveManager.
                        Spawn(SpawnPoint, QuickSave_Source_Class,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    SupportSource.Source_Card._Basic_SaveData_Class.ObjectDataSet("TimePos",Answer_Object_Script);
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Object_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - NPC -
    //NPC創造——————————————————————————————————————————————————————————————————————
    public void NPCSet(string Key)
    {
        //建立隨機編號庫----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Creature_StringData = _Data_NPCCreate_Dictionary[Key].CreatureKey;
        //----------------------------------------------------------------------------------------------------

        //製造----------------------------------------------------------------------------------------------------
        //生成
        for (int b = 0; b < QuickSave_Creature_StringData.Count; b++)
        {
            //----------------------------------------------------------------------------------------------------
            _Map_BattleObjectUnit QuickSave_Object_Script = ObjectSet("Creature", QuickSave_Creature_StringData[b], null, null,
                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            _Basic_SaveData_Class.
                ObjectListDataAdd("NPC", QuickSave_Object_Script);
            _Object_NPCs_ScriptsList.Add(QuickSave_Object_Script._Basic_Source_Class.Source_Creature);
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - State -
    //——————————————————————————————————————————————————————————————————————
    public bool ObjectRoundSet()//生死/驅動與否，與判定是否需要停止(玩家死亡)
    {
        //死亡與否----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                        _Object_Player_Script._Object_Inventory_Script._Item_CarryObject_ScriptsList;
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        if (Object._State_Death_Bool && !Object._State_DeathCheck_Bool)
                        {
                            if (!Object.Death())
                            {
                                return false;//終止
                            }
                        }
                    }
                }
                break;
            case "Battle":
                {
                    List<string> QuickSave_Keys_StringList = 
                        new List<string> { "Concept", "Weapon", "Item", "Object" };
                    foreach (string Key in QuickSave_Keys_StringList)
                    {
                        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                            _Basic_SaveData_Class.ObjectListDataGet(Key);
                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                        {
                            if (Object._State_Death_Bool && !Object._State_DeathCheck_Bool)
                            {
                                if (!Object.Death())
                                {
                                    return false;//終止
                                }
                            }
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //使用狀態----------------------------------------------------------------------------------------------------
        //Driving
        {
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet("PreDriving");
            List<SourceClass> QuickSave_Sources_ClassList =
                _Basic_SaveData_Class.SourceListDataGet("PreDriving");
            for (int a = 0; a < QuickSave_Objects_ScriptsList.Count; a++)
            {
                QuickSave_Objects_ScriptsList[a].StateSet("Driving", QuickSave_Sources_ClassList[a],
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            }
            _Basic_SaveData_Class.ObjectListDataSet("PreDriving", null);
            _Basic_SaveData_Class.SourceListDataSet("PreDriving", null);
        }

        List<_Map_BattleObjectUnit> QuickSave_SaveObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
        //Break
        {
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet("PreBreak");
            List<SourceClass> QuickSave_Sources_ClassList =
                _Basic_SaveData_Class.SourceListDataGet("PreBreak");
            for (int a = 0; a < QuickSave_Objects_ScriptsList.Count; a++)
            {
                QuickSave_SaveObjects_ScriptsList.Add(QuickSave_Objects_ScriptsList[a]);
                QuickSave_Objects_ScriptsList[a].StateSet("Break", QuickSave_Sources_ClassList[a],
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            }
            _Basic_SaveData_Class.ObjectListDataSet("PreBreak", null);
            _Basic_SaveData_Class.SourceListDataSet("PreBreak", null);
        }
        //Abandoning
        {
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet("PreAbandoning");
            List<SourceClass> QuickSave_Sources_ClassList =
                _Basic_SaveData_Class.SourceListDataGet("PreAbandoning");
            for (int a = 0; a < QuickSave_Objects_ScriptsList.Count; a++)
            {
                if (QuickSave_SaveObjects_ScriptsList.Contains(QuickSave_Objects_ScriptsList[a]))
                {
                    continue;
                }
                QuickSave_SaveObjects_ScriptsList.Add(QuickSave_Objects_ScriptsList[a]);
                QuickSave_Objects_ScriptsList[a].StateSet("Abandoning", QuickSave_Sources_ClassList[a],
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            }
            _Basic_SaveData_Class.ObjectListDataSet("PreAbandoning", null);
            _Basic_SaveData_Class.SourceListDataSet("PreAbandoning", null);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;//繼續
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region ScenesSet
    public void HoldToField(_Object_CreatureUnit Creature)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _Item_Object_Inventory QuickSave_Inventory_Script = Creature._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //生物招式----------------------------------------------------------------------------------------------------
        Creature.HoldToField();
        QuickSave_Inventory_Script._Item_EquipConcepts_Script.HoldToField();
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[a].HoldToField();
        }
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipItems_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[a].HoldToField();
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void FieldToHold(_Object_CreatureUnit Creature)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Item_Object_Inventory QuickSave_Inventory_Script = Creature._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //生物招式----------------------------------------------------------------------------------------------------
        Creature.FieldToHold();
        QuickSave_Inventory_Script._Item_EquipConcepts_Script.FieldToHold();
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[a].FieldToHold();
        }
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipItems_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[a].FieldToHold();
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void FieldToBattle(_Object_CreatureUnit Creature)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Item_Object_Inventory QuickSave_Inventory_Script = Creature._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //生物招式----------------------------------------------------------------------------------------------------
        Creature.FieldToBattle();
        QuickSave_Inventory_Script._Item_EquipConcepts_Script.FieldToBattle();
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[a].FieldToBattle();
        }
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipItems_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[a].FieldToBattle();
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void BattleToField(_Object_CreatureUnit Creature)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Item_Object_Inventory QuickSave_Inventory_Script = Creature._Object_Inventory_Script;
        //----------------------------------------------------------------------------------------------------

        //生物招式----------------------------------------------------------------------------------------------------
        Creature.BattleToField();
        QuickSave_Inventory_Script._Item_EquipConcepts_Script.BattleToField();
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList[a].BattleToField();
        }
        for (int a = 0; a < QuickSave_Inventory_Script._Item_EquipItems_ScriptsList.Count; a++)
        {
            QuickSave_Inventory_Script._Item_EquipItems_ScriptsList[a].BattleToField();
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion SkillWeaponSet

    #region ViewSet
    #region - ColliderTurn -
    //開啟碰撞器——————————————————————————————————————————————————————————————————————
    public void ColliderTurnOn()
    {
        //----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Keys_StringList =
            new List<string> { "Concept", "Weapon", "Item", "Object" };
        foreach (string Key in QuickSave_Keys_StringList)
        {
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet(Key);
            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
            {
                foreach (_World_IsometicSorting IsometicSorting in Object._IsometicSorting_Caller_ScriptList)
                {
                    if (IsometicSorting._Map_MouseSencer_Collider == null)
                    {
                        continue;
                    }
                    IsometicSorting._Map_MouseSencer_Collider.enabled = true;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //關閉碰撞器——————————————————————————————————————————————————————————————————————
    public void ColliderTurnOff()
    {
        //----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Keys_StringList =
            new List<string> { "Concept", "Weapon", "Item", "Object" };
        foreach (string Key in QuickSave_Keys_StringList)
        {
            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet(Key);
            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
            {
                foreach (_World_IsometicSorting IsometicSorting in Object._IsometicSorting_Caller_ScriptList)
                {
                    if (IsometicSorting._Map_MouseSencer_Collider == null)
                    {
                        continue;
                    }
                    IsometicSorting._Map_MouseSencer_Collider.enabled = false;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion ViewSet

    #region AI
    #region - ActionScore -
    //動作評分——————————————————————————————————————————————————————————————————————
    public float ActionScore(string Type, Dictionary<string, float> AIData,List<string> ScoreList,int Time,int Order, _Map_BattleObjectUnit HateTarget)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        //----------------------------------------------------------------------------------------------------

        //計分----------------------------------------------------------------------------------------------------
        if (ScoreList.Count >0)
        {
            //逐行計分
            foreach (string ScoreKey in ScoreList)
            {
                //當前行為
                string QuickSave_NowAction_String = ScoreKey;
                if (QuickSave_NowAction_String == "")
                {
                    continue;
                }
                //數值
                float QuickSave_Value_Float = 1;
                //仇恨比重
                float QuickSave_HateTargetWeight_Float = 1;
                //行為比重(來源於AIData)
                float QuickSave_ActionWeight_Float = 0;

                //註解判定
                if (QuickSave_NowAction_String.Contains("Hater"))
                {
                    QuickSave_HateTargetWeight_Float *= AIData["HateWeight"];
                    QuickSave_NowAction_String = QuickSave_NowAction_String.Replace("Hater", "Enemy");
                }
                //"Hater_MediumPoint｜10;
                string[] QuickSave_ActionSplit_StringArray = QuickSave_NowAction_String.Split("｜"[0]);

                //評分項目
                string QuickSave_AICode_String = QuickSave_ActionSplit_StringArray[0];

                //評分
                switch (QuickSave_AICode_String)
                {
                    case "Self_GetEffectObject":
                    case "Friend_GetEffectObject":
                    case "Enemy_GetEffectObject":
                    case "Object_GetEffectObject":
                    case "Self_LostEffectObject":
                    case "Friend_LostEffectObject":
                    case "Enemy_LostEffectObject":
                    case "Object_LostEffectObject":
                    case "Self_GetEffectCard":
                    case "Friend_GetEffectCard":
                    case "Enemy_GetEffectCard":
                    case "Object_GetEffectCard":
                        {
                            List<string> QuickSave_Key_StringList =
                                new List<string>(QuickSave_ActionSplit_StringArray[1].Split(":"[0]));

                            _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
                                _Effect_Manager._Data_EffectObject_Dictionary[QuickSave_Key_StringList[0]];
                            float QuickSave_EffectValue_Float = QuickSave_EffectData_Class.Value;

                            QuickSave_Value_Float = float.Parse(QuickSave_Key_StringList[1]);
                            QuickSave_ActionWeight_Float = 
                                AIData[QuickSave_AICode_String] * QuickSave_EffectValue_Float;
                        }
                        break;

                    case "Self_MediumPoint":
                    case "Friend_MediumPoint":
                    case "Enemy_MediumPoint":
                    case "Object_MediumPoint":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String] * 1.15f;
                        }
                        break;
                    case "Self_CatalystPoint":
                    case "Friend_CatalystPoint":
                    case "Enemy_CatalystPoint":
                    case "Object_CatalystPoint":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String];
                        }
                        break;
                    case "Self_Dead":
                    case "Friend_Dead":
                    case "Enemy_Dead":
                    case "Object_Dead":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String] * 2;
                        }
                        break;

                    case "Self_DealCard":
                    case "Friend_DealCard":
                    case "Enemy_DealCard":
                    case "Object_DealCard":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String];
                        }
                        break;
                    case "Self_ThrowCard":
                    case "Friend_ThrowCard":
                    case "Enemy_ThrowCard":
                    case "Object_ThrowCard":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String];
                        }
                        break;

                    case "DelayBefore":
                    case "DelayAfter":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String] * 0.1f;
                        }
                        break;

                    case "Reverse":
                        {
                            QuickSave_Value_Float = float.Parse(QuickSave_ActionSplit_StringArray[1]);
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String] * 2f;
                        }
                        break;

                    case "React":
                        {
                            QuickSave_Value_Float = 1;
                            QuickSave_ActionWeight_Float = AIData[QuickSave_AICode_String] * 5;
                        }
                        break;

                    case "Move_Destination":
                        {
                            if (Type != "Self")
                            {
                                continue;
                            }
                            string[] QuickSave_CoordinateSplit_StringArray = QuickSave_ActionSplit_StringArray[1].Split(","[0]);
                            float QuickSave_X_Float = int.Parse(QuickSave_CoordinateSplit_StringArray[0].Replace("(", ""));
                            float QuickSave_Y_Float = int.Parse(QuickSave_CoordinateSplit_StringArray[1]);
                            float QuickSave_Z_Float = int.Parse(QuickSave_CoordinateSplit_StringArray[2].Replace(")", ""));
                            Vector QuickSave_Destination_Class = 
                                new Vector(QuickSave_X_Float, QuickSave_Y_Float, QuickSave_Z_Float);
                            //得分 = (最高得分 / -(警界距離'2)) * (與舒適距離的距離'2) + 最高得分
                            float QuickSave_HightestScore = 20;
                            float QuickSave_CoordinateDistance =
                                QuickSave_Destination_Class.Distance(HateTarget.TimePosition(Time, Order)) -
                                AIData["ComfortableDistance"];
                            float QuickSave_NoticeDistanceScore_Float = 
                                (AIData["NoticeDistance"] + 1 - QuickSave_CoordinateDistance) / (AIData["NoticeDistance"] + 1);
                            QuickSave_ActionWeight_Float =
                                AIData["ComfortableDistanceWeight"] *
                                (Mathf.Pow(QuickSave_NoticeDistanceScore_Float, 0.5f)) * 
                                QuickSave_HightestScore;
                        }
                        break;
                }
                Answer_Return_Float +=
                    (QuickSave_Value_Float * QuickSave_ActionWeight_Float * QuickSave_HateTargetWeight_Float);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        float QuickSave_TypeScale_Float = 1;
        switch (Type)
        {
            case "Self":
            case "Friend":
            case "Enemy":
            case "Object":
                {
                    QuickSave_TypeScale_Float *= AIData[Type + "Weight"];
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_Float * QuickSave_TypeScale_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion AI

    #region Variable
    //——————————————————————————————————————————————————————————————————————
    public List<_Map_BattleObjectUnit> TimeObjects(string Type, 
        SourceClass UserSource, 
        int Time,int Order, Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> Answer_Return_SctiptsList = new List<_Map_BattleObjectUnit>();
        List<string> QuickSave_Keys_StringList = 
            new List<string> { "Concept", "Weapon", "Item", "Object" };
        //----------------------------------------------------------------------------------------------------

        //基本進入----------------------------------------------------------------------------------------------------
        foreach (string Key in QuickSave_Keys_StringList)
        {
            List<_Map_BattleObjectUnit> QuickSave_DataObjects_ScriptsList =
                _Basic_SaveData_Class.ObjectListDataGet(Key);
            foreach (_Map_BattleObjectUnit Object in QuickSave_DataObjects_ScriptsList)
            {
                if (Object._State_Death_Bool)
                {
                    continue;
                }
                if (Object.TimePosition(Time, Order) != Coordinate)
                {
                    continue;
                }
                Answer_Return_SctiptsList.Add(Object);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //Type----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = 
            new List<_Map_BattleObjectUnit>(Answer_Return_SctiptsList);
        switch (Type)
        {
            case "Normal"://驅動迴避(同位置包含驅動者時不命中驅動物)
                {
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        SourceClass QuickSave_TargetSource_Class =
                            Object._Basic_Source_Class;
                        //被驅動/持有且非使用者
                        if (UserSource == null)
                        {
                            continue;
                        }
                        if (QuickSave_TargetSource_Class.SourceType != "Concept" &&
                            QuickSave_TargetSource_Class.Source_Creature != null &&
                            QuickSave_TargetSource_Class.Source_Creature != UserSource.Source_Creature)
                        {
                            _Map_BattleObjectUnit QuickSave_OtherObject_Script =
                                Object._Basic_Source_Class.Source_Creature._Basic_Object_Script;
                            if (QuickSave_Objects_ScriptsList.Contains(QuickSave_OtherObject_Script))
                            {
                                Answer_Return_SctiptsList.Remove(Object);
                                continue;
                            }
                        }
                    }
                }
                break;
            case "UserDriving"://使用者驅動(被使用者驅動者-不包含概念)
                {
                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        SourceClass QuickSave_TargetSource_Class =
                            Object._Basic_Source_Class;
                        //被自身驅動且非Concept
                        if (Object._Basic_DrivingOwner_Script != UserSource.Source_Creature._Basic_Object_Script)
                        {
                            Answer_Return_SctiptsList.Remove(Object);
                            continue;
                        }
                        if (QuickSave_TargetSource_Class.SourceType == "Concept")
                        {
                            Answer_Return_SctiptsList.Remove(Object);
                            continue;
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_SctiptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
}
