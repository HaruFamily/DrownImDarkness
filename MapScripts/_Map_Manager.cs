using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.Rendering.DebugUI;

public class _Map_Manager : MonoBehaviour
{
    #region FollowerBox
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public _Map_MoveManager _Map_MoveManager;

    public _Map_FieldCreator _Map_FieldCreator;
    public _Map_BattleCreator _Map_BattleCreator;

    public _Map_BattleRound _Map_BattleRound;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public TextAsset _Data_RegionInput_TextAsset;
    public TextAsset _Data_MapInput_TextAsset;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Dictionary<string, RegionDataClass> _Data_Region_Dictionary = new Dictionary<string, RegionDataClass>();
    public Dictionary<string, MapDataClass> _Data_Map_Dictionary = new Dictionary<string, MapDataClass>();
    public Dictionary<string, LanguageClass> _Language_Map_Dictionary = new Dictionary<string, LanguageClass>();
    public Dictionary<string, LanguageClass> _Language_Weather_Dictionary = new Dictionary<string, LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public GameObject _Map_SelectUnit_GameObject;
    //原野玩家位置
    public Transform _Map_SelectStore_Transform;
    public Transform _Map_FieldMap_Transform;
    public Transform _Map_BattleMap_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Queue<_Map_SelectUnit> _Map_SelectPool_ScriptsQueue = new Queue<_Map_SelectUnit>();
    //範圍內物件表
    public List<_Map_SelectUnit> _Card_GroundUnitInRange_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInRangePath_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInRangeExtend_ScriptsList = new List<_Map_SelectUnit>();

    public List<_Map_SelectUnit> _Card_GroundUnitInPath_ScriptsList = new List<_Map_SelectUnit>();
    public List<_Map_SelectUnit> _Card_GroundUnitInSelect_ScriptsList = new List<_Map_SelectUnit>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //Class——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------
    //地區資料
    public class MapDataClass///地圖(一個地圖可能包含複數Region)
    {
        public Vector2Int Size;
        //全域變異
        public List<string> Syndrome = new List<string>();
        //地圖
        public string[,] Map;
        //該符號代表何種區域
        public Dictionary<string, List<string>> RegionCode = new Dictionary<string, List<string>>();
        //固定發生事件
        public Dictionary<Vector2Int, string> TargetEvent = new Dictionary<Vector2Int, string>();
        //全域時間事件(通常天氣相關)
        public Dictionary<string, List<string>> TimeEvent = new Dictionary<string, List<string>>();
    }
    //----------------------------------------------------------------------------------------------------

    //設定資料類別----------------------------------------------------------------------------------------------------
    //地區資料
    public class RegionDataClass//對應Region
    {
        public List<string> Tags = new List<string>();
        //變異
        public List<string> Syndrome = new List<string>();
        //時間事件(只在該區域發生)
        public List<KeyValuePair<string, int>> Branch = new List<KeyValuePair<string, int>>();
        //時間事件(只在該區域發生)
        public Dictionary<string, List<string>> TimeEvent = new Dictionary<string, List<string>>();
        //數量事件/限量列表<列表內隨機(共用數量)>
        public List<Dictionary<string, int>> QuantityEvent = new List<Dictionary<string, int>>();
        //隨機事件
        public List<Dictionary<string, int>> RandomEvent = new List<Dictionary<string, int>>();
    }
    //----------------------------------------------------------------------------------------------------

    //板塊單位資訊----------------------------------------------------------------------------------------------------
    public class GroundDataClass//地塊資訊
    {
        public string Region;
        public List<string> Tags = new List<string>();//標籤(影響穿越/視野等)
        public string Event;//事件
        public List<_Map_FieldObjectUnit> Objects = new List<_Map_FieldObjectUnit>();//持有的物件
    }
    //----------------------------------------------------------------------------------------------------

    //板塊單位資訊----------------------------------------------------------------------------------------------------
    public class FieldDataClass//地塊資訊
    {
        public Vector2Int Size;
        public Dictionary<Vector2Int, GroundDataClass> Data;//地塊資料
        //變異(All(全體),Region(區域))
        public Dictionary<string, List<string>> Syndrome = new Dictionary<string, List<string>>();
        //時間事件(All(全體),Region(區域))
        public Dictionary<string, Dictionary<string, List<string>>> TimeEvent = 
            new Dictionary<string, Dictionary<string, List<string>>>();
        //State
        public Dictionary<Vector2Int, bool> CanPass;//能否經過
        public Dictionary<Vector2Int, bool> CanView;//能否透視
    }
    //----------------------------------------------------------------------------------------------------

    //——————————————————————————————————————————————————————————————————————
    #endregion FollowerBox

    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        DataSet();
        LanguageSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild

    #region DataBaseSet
    //——————————————————————————————————————————————————————————————————————
    private void DataSet()
    {
        #region - Map -
        //地圖----------------------------------------------------------------------------------------------------
        string[] QuickSave_MapSourceSplit = _Data_MapInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_MapSourceSplit.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            MapDataClass QuickSave_Map_Class = new MapDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_MapSourceSplit[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_DataSourceSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            string[] QuickSave_ElementSplit_StringArray = QuickSave_DataSourceSplit[2].Substring(6).Split(","[0]);
            QuickSave_Map_Class.Size =
                new Vector2Int(int.Parse(QuickSave_ElementSplit_StringArray[0]), int.Parse(QuickSave_ElementSplit_StringArray[1]));
            string QuickSave_Syndrome_String = QuickSave_DataSourceSplit[3].Substring(10);
            QuickSave_Map_Class.Syndrome = new List<string>();
            if (QuickSave_Syndrome_String != "")
            {
                QuickSave_Map_Class.Syndrome.AddRange(
                    new List<string>(QuickSave_Syndrome_String.Split(","[0])));
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_ElementDataSourceSplit = QuickSave_Split_StringArray[1].Split("\r"[0]);
                QuickSave_Map_Class.Map = new string[QuickSave_Map_Class.Size.x, QuickSave_Map_Class.Size.y];
                for (int y = 1; y < QuickSave_ElementDataSourceSplit.Length - 1; y++)
                {
                    string[] QuickSave_ExtraSplit = QuickSave_ElementDataSourceSplit[y].Split(","[0]);
                    for (int x = 0; x < QuickSave_ExtraSplit.Length; x++)
                    {
                        QuickSave_Map_Class.Map[x, y - 1] = QuickSave_ExtraSplit[x];
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[2].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    List<string> QuickSave_Region_StringList = new List<string>();
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        int QuickSave_Count_Int = int.Parse(QuickSave_InnerSplit_StringArray[1]);
                        for (int a = 0; a < QuickSave_Count_Int; a++)
                        {
                            QuickSave_Region_StringList.Add(QuickSave_InnerSplit_StringArray[0]);
                        }
                    }
                    QuickSave_Map_Class.RegionCode.Add(QuickSave_TextSplit[0], QuickSave_Region_StringList);
                }
            }
            //----------------------------------------------------------------------------------------------------   
            
            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_ElementDataSourceSplit = QuickSave_Split_StringArray[3].Split("\r"[0]);
                for (int s = 1; s < QuickSave_ElementDataSourceSplit.Length - 1; s++)
                {
                    string[] QuickSave_TextSplit = QuickSave_ElementDataSourceSplit[s].Substring(1).Split(":"[0]);
                    string[] QuickSave_VectorSplit = QuickSave_TextSplit[0].Split(","[0]);
                    Vector2Int QuickSave_Vector_Vector =
                        new Vector2Int(int.Parse(QuickSave_VectorSplit[0]), int.Parse(QuickSave_VectorSplit[1]));
                    //置入索引
                    if (QuickSave_TextSplit[1] == "")
                    {
                        continue;
                    }
                    QuickSave_Map_Class.TargetEvent.Add(QuickSave_Vector_Vector, QuickSave_TextSplit[1]);
                }
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[4].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Map_Class.TimeEvent.Add(
                        QuickSave_TextSplit[0], new List<string>(QuickSave_TextSplit[1].Split(","[0])));
                }
            }
            //----------------------------------------------------------------------------------------------------


            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Map_Dictionary.Add(QuickSave_DataSourceSplit[1].Substring(5), QuickSave_Map_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_MapInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Region -
        //生物----------------------------------------------------------------------------------------------------
        //AI
        string[] QuickSave_RegionSourceSplit = _Data_RegionInput_TextAsset.text.Split("—"[0]);
        for (int t = 0; t < QuickSave_RegionSourceSplit.Length; t++)
        {
            //建立資料----------------------------------------------------------------------------------------------------
            RegionDataClass QuickSave_Region_Class = new RegionDataClass();
            //分割其他與數字
            string[] QuickSave_Split_StringArray = QuickSave_RegionSourceSplit[t].Split("–"[0]);
            //----------------------------------------------------------------------------------------------------

            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_Split_StringArray[0].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------
            string[] QuickSave_DataSourceSplit = QuickSave_Split_StringArray[0].Split("\r"[0]);
            QuickSave_Region_Class.Tags =
                new List<string>(QuickSave_DataSourceSplit[2].Substring(6).Split(","[0]));
            QuickSave_Region_Class.Syndrome =
                new List<string>(QuickSave_DataSourceSplit[3].Substring(10).Split(","[0]));
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[1].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Region_Class.Branch.Add(
                        new KeyValuePair<string, int>(QuickSave_TextSplit[0], int.Parse(QuickSave_TextSplit[1])));
                }
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[2].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(":"[0]);
                    QuickSave_Region_Class.TimeEvent.Add(
                        QuickSave_TextSplit[0], new List<string>(QuickSave_TextSplit[1].Split(","[0])));
                }
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[3].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    Dictionary<string, int> QuickSave_Events_Dictionary = new Dictionary<string, int>();
                    int QuickSave_Count_Int = int.Parse(QuickSave_TextSplit[0]);
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        QuickSave_Events_Dictionary.Add(
                            QuickSave_InnerSplit_StringArray[0], int.Parse(QuickSave_InnerSplit_StringArray[1]));
                    }
                    for (int c = 0; c < QuickSave_Count_Int; c++)
                    {
                        QuickSave_Region_Class.QuantityEvent.Add(QuickSave_Events_Dictionary);
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //其他項目----------------------------------------------------------------------------------------------------
            {
                string[] QuickSave_EventDataSourceSplit = QuickSave_Split_StringArray[4].Split("\r"[0]);
                for (int s = 1; s < QuickSave_EventDataSourceSplit.Length - 1; s++)
                {
                    if (QuickSave_EventDataSourceSplit[s] == "" || QuickSave_EventDataSourceSplit[s] == "\n")
                    {
                        continue;
                    }
                    string[] QuickSave_TextSplit = QuickSave_EventDataSourceSplit[s].Substring(1).Split(","[0]);
                    Dictionary<string, int> QuickSave_Events_Dictionary = new Dictionary<string, int>();
                    int QuickSave_Count_Int = int.Parse(QuickSave_TextSplit[0]);
                    for (int r = 1; r < QuickSave_TextSplit.Length; r++)
                    {
                        string[] QuickSave_InnerSplit_StringArray = QuickSave_TextSplit[r].Split(":"[0]);
                        QuickSave_Events_Dictionary.Add(
                            QuickSave_InnerSplit_StringArray[0], int.Parse(QuickSave_InnerSplit_StringArray[1]));
                    }
                    for (int c = 0; c < QuickSave_Count_Int; c++)
                    {
                        QuickSave_Region_Class.RandomEvent.Add(QuickSave_Events_Dictionary);
                    }
                }
            }
            //----------------------------------------------------------------------------------------------------

            //設定----------------------------------------------------------------------------------------------------
            //置入索引
            _Data_Region_Dictionary.Add(QuickSave_DataSourceSplit[1].Substring(5), QuickSave_Region_Class);
            //----------------------------------------------------------------------------------------------------
        }
        //釋放記憶體
        _Data_RegionInput_TextAsset = null;
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————


    //語言設置——————————————————————————————————————————————————————————————————————
    public void LanguageSet()
    {
        #region - Map -
        //取得起始文本----------------------------------------------------------------------------------------------------
        //地板型事件
        string QuickSave_MapTextSource_String = "";
        string QuickSave_MapTextAssetCheck_String = "";
        QuickSave_MapTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_" + _World_Manager._Config_Language_String + "_Map.txt";
        if (File.Exists(QuickSave_MapTextAssetCheck_String))
        {
            QuickSave_MapTextSource_String = File.ReadAllText(QuickSave_MapTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Map/_" + _World_Manager._Config_Language_String + "_Map.txt");
            QuickSave_MapTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_TraditionalChinese_Map.txt";
            QuickSave_MapTextSource_String = File.ReadAllText(QuickSave_MapTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_MapSourceSplit = QuickSave_MapTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_MapSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_MapSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_MapSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Map_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Weather -
        //取得起始文本----------------------------------------------------------------------------------------------------
        //地板型事件
        string QuickSave_WeatherTextSource_String = "";
        string QuickSave_WeatherTextAssetCheck_String = "";
        QuickSave_WeatherTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_" + _World_Manager._Config_Language_String + "_Weather.txt";
        if (File.Exists(QuickSave_WeatherTextAssetCheck_String))
        {
            QuickSave_WeatherTextSource_String = File.ReadAllText(QuickSave_WeatherTextAssetCheck_String);
        }
        else
        {
            print("NoTextAsset：/Map/_" + _World_Manager._Config_Language_String + "_Weather.txt");
            QuickSave_WeatherTextAssetCheck_String = Application.streamingAssetsPath + "/Map/_TraditionalChinese_Weather.txt";
            QuickSave_WeatherTextSource_String = File.ReadAllText(QuickSave_WeatherTextAssetCheck_String);
        }
        //----------------------------------------------------------------------------------------------------

        //文本分割與置入索引----------------------------------------------------------------------------------------------------
        //地板型事件
        //分割為單項
        string[] QuickSave_WeatherSourceSplit = QuickSave_WeatherTextSource_String.Split("—"[0]);
        for (int t = 0; t < QuickSave_WeatherSourceSplit.Length; t++)
        {
            //註解跳出----------------------------------------------------------------------------------------------------
            if (QuickSave_WeatherSourceSplit[t].Substring(0, 2) == "//")
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //分割為單行﹝最開頭與最結尾為空白﹞
            string[] QuickSave_TextSplit = QuickSave_WeatherSourceSplit[t].Split("\r"[0]);
            LanguageClass QuickSave_Language_Class = new LanguageClass();
            //建立資料_Substring(X)代表由X開始
            QuickSave_Language_Class.Name = QuickSave_TextSplit[2].Substring(6);
            QuickSave_Language_Class.Summary = QuickSave_TextSplit[3].Substring(9);
            QuickSave_Language_Class.Description = QuickSave_TextSplit[4].Substring(13);

            //置入索引
            _Language_Weather_Dictionary.Add(QuickSave_TextSplit[1].Substring(5), QuickSave_Language_Class);
        }
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion DataBaseSet

    #region Start
    public bool _Map_BattleComplete_Bool = false;
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void ManagerStart(int TimePass = 0)
    {
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    string QuickSave_Map_String = 
                        _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
                    _Map_FieldMap_Transform.gameObject.SetActive(true);
                    _Map_BattleMap_Transform.gameObject.SetActive(false);
                    if (!_Map_FieldCreator._Map_Data_Dictionary.ContainsKey(QuickSave_Map_String))
                    {
                        //建立權限
                        FieldStateSet("Locking", "初始設定");
                        //地圖製作
                        _Map_FieldCreator.SystemStart(QuickSave_Map_String);
                    }
                    else//從其他地方回到(Field已生成)
                    {
                        _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
                        MapGroundUnitComplete();
                        /*
                        if (_UI_EventManager._Event_NowEventKey_String == "")
                        {
                            _World_Manager._Object_Manager._Object_Player_Script.BuildFront();
                        }
                        else
                        {
                            _UI_EventManager.EventFinding(_UI_EventManager._Event_NowVector_Class, TimePass, false);
                        }*/
                    }
                }
                break;
            case "Battle":
                {
                    _Map_BattleMap_Transform.gameObject.SetActive(true);
                    _Map_FieldMap_Transform.gameObject.SetActive(false);
                    if (!_Map_BattleComplete_Bool)
                    {
                        //建立權限
                        BattleStateSet("Locking", "初始設定");
                        //地圖製作
                        _Map_BattleCreator.SystemStart();
                        _Map_BattleComplete_Bool = true;
                    }
                }
                break;
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region MapComplete
    //地圖完成——————————————————————————————————————————————————————————————————————
    public void MapGroundUnitComplete()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
        _Object_CreatureUnit QuickSave_Creature_Script = _Object_Manager._Object_Player_Script;
        //----------------------------------------------------------------------------------------------------

        //物件開始----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    //玩家列開始
                    Vector QuickSave_StartCoor_Class =
                        QuickSave_Creature_Script._Creature_FieldObjectt_Script._Map_Coordinate_Class;
                    QuickSave_Creature_Script._Player_Script.FieldStart(QuickSave_StartCoor_Class);
                }
                break;
            case "Battle":
                {
                    QuickSave_Creature_Script._Player_Script.BattleStart();
                    //生物製造
                    _Object_Manager.NPCSet(_UI_EventManager._Battle_NPCCreateKey_String);
                    _Map_BattleRound.CreatureDelaySet();
                    //起始呼叫
                    BattleStateSet("RoundTimes", "地圖完成");
                    //回合進行
                    StartCoroutine(_Map_BattleRound.RoundCall());
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void ClearMap()
    {
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Battle":
                for (int x = 0; x < _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0); x++)
                {
                    for (int y = 0; y < _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1); y++)
                    {
                        if ((x + y) % 2 == 0)
                        {
                            Destroy(_Map_BattleCreator._Map_GroundBoard_ScriptsArray[x, y].gameObject);
                        }
                    }
                }
                _Map_BattleCreator._Map_PreCreate_BoolArray = null;
                _Map_BattleCreator._Map_GroundBoard_ScriptsArray = null;
                _Map_BattleMap_Transform.gameObject.SetActive(false);
                break;
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion MapComplete

    #region FieldState
    //設定狀態種類——————————————————————————————————————————————————————————————————————
    //當前狀態值
    static public string _State_FieldState_String;
    //狀態表----------------------------------------------------------------------------------------------------
    //Locking-鎖定
    //BuildFront-前置

    //SelectExplore-選擇探索
    //SelectRange-選擇範圍

    //AnimeFront-動畫前置
    //AnimeMiddle-動畫中置
    //AnimeBack-動畫後置

    //BuildBack-生物後置

    //EventFront-事件前置
    //EventMiddle-事件中置
    //EventSelect-事件選擇
    //EventFrame-事件說明
    //EventBack-事件後置

    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————


    //狀態調整——————————————————————————————————————————————————————————————————————
    public void FieldStateSet(string Set, string Hint = null)
    {
        //測試機----------------------------------------------------------------------------------------------------
        if (Hint != null && _World_Manager._Test_Hint_Bool)
        {
            print(Set + "：" + Hint);
        }
        //----------------------------------------------------------------------------------------------------

        //權限變更----------------------------------------------------------------------------------------------------
        switch (Set)
        {
            case "Locking":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "BuildFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "SelectExplore":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "SelectRange":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "AnimeFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "EventFront":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventMiddle":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventSelect":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventFrame":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "EventBack":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;

            default:
                print("StateName_String：﹝" + Set + "﹞is Wrong String");
                return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //設定當前狀態
        _State_FieldState_String = Set;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FieldState


    #region BattleState
    //設定狀態種類——————————————————————————————————————————————————————————————————————
    //當前狀態值
    static public string _State_BattleState_String;
    //觸發發動中
    public bool _State_Reacting_Bool;
    public List<string> _State_ReactTag_StringList;

    //狀態表----------------------------------------------------------------------------------------------------
    //Locking-鎖定
    //RoundTimes-時間軸排序
    //RoundCall-行為呼叫

    //BuildFront-生物前置

    //PlayerBehavior-選擇行為
    //PlayerEnchance-選擇附魔與範圍
    //EnemySelect-敵人行為

    //BuildMiddle-選擇中置

    //AnimeFront-動畫前置
    //AnimeMiddle-動畫中置
    //AnimeBack-動畫後置

    //BuildBack-生物後置
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————


    //狀態調整——————————————————————————————————————————————————————————————————————
    public void BattleStateSet(string Set, string Hint = null)
    {
        //測試機----------------------------------------------------------------------------------------------------
        if (Hint != null && _World_Manager._Test_Hint_Bool)
        {
            print("Times：" + _Map_BattleRound._Round_Time_Int + "_" + Set + "：" + Hint);
        }
        //----------------------------------------------------------------------------------------------------

        //權限變更----------------------------------------------------------------------------------------------------
        switch (Set)
        {
            case "Locking":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = false;
                break;
            case "RoundTimes":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "RoundCall":
                _World_Manager._Authority_UICover_Bool = false;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "PlayerBehavior":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "PlayerEnchance":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = true;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "EnemySelect":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "AnimeFront":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeMiddle":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;
            case "AnimeBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            case "BuildBack":
                _World_Manager._Authority_UICover_Bool = true;
                _World_Manager._Authority_CardClick_Bool = false;
                _World_Manager._Authority_CameraSet_Bool = true;
                break;

            default:
                print("StateName_String：﹝" + Set + "﹞is Wrong String");
                return;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //設定當前狀態
        _State_BattleState_String = Set;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion BattleState


    #region ViewSet
    #region - SelectQueueSet -
    //——————————————————————————————————————————————————————————————————————
    public _Map_SelectUnit TakeSelectDeQueue(Vector Coordinate)//拿出
    {
        //----------------------------------------------------------------------------------------------------
        _Map_SelectUnit Answer_Return_Script = null;
        Dictionary<Vector2Int, _Map_SelectUnit> QuickSave_Board_Dictionary = 
            new Dictionary<Vector2Int, _Map_SelectUnit>();
        Vector3 QuickSave_Pos_Vector = new Vector3();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                QuickSave_Board_Dictionary = _Map_FieldCreator._Map_SelectBoard_Dictionary;
                QuickSave_Pos_Vector = _Map_FieldCreator._Math_CooridnateTransform_Vector2(Coordinate);
                break;
            case "Battle":
                QuickSave_Board_Dictionary = _Map_BattleCreator._Map_SelectBoard_Dictionary;
                QuickSave_Pos_Vector = _Map_BattleCreator._Math_CooridnateTransform_Vector2(Coordinate);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Board_Dictionary.TryGetValue(Coordinate.Vector2Int, out _Map_SelectUnit Value))
        {
            Answer_Return_Script = Value;
        }
        else
        {
            if (_Map_SelectPool_ScriptsQueue.Count > 0)
            {
                Answer_Return_Script = _Map_SelectPool_ScriptsQueue.Dequeue();
            }
            else
            {
                Answer_Return_Script =
                    Instantiate(_Map_SelectUnit_GameObject, _Map_SelectStore_Transform).GetComponent<_Map_SelectUnit>();
            }
            QuickSave_Board_Dictionary.Add(Coordinate.Vector2Int, Answer_Return_Script);
        }
        _UI_Card_Unit QuickSave_Card_Script =
            _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script;
        Answer_Return_Script.SystemStart(Coordinate, QuickSave_Pos_Vector, QuickSave_Card_Script);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void TakeSelectEnQueue(_Map_SelectUnit SelectUnit)//收回
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<Vector2Int, _Map_SelectUnit> QuickSave_Board_Dictionary = new Dictionary<Vector2Int, _Map_SelectUnit>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                QuickSave_Board_Dictionary = _Map_FieldCreator._Map_SelectBoard_Dictionary;
                break;
            case "Battle":
                QuickSave_Board_Dictionary = _Map_BattleCreator._Map_SelectBoard_Dictionary;
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        SelectUnit.transform.localPosition = Vector3.zero;
        SelectUnit.transform.localScale = Vector3.zero;
        _Map_SelectPool_ScriptsQueue.Enqueue(SelectUnit);
        QuickSave_Board_Dictionary.Remove(SelectUnit._Basic_Coordinate_Class.Vector2Int);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion ViewSet

    #region RangeSet
    #region - ViewOn -
    //顯示地面範圍——————————————————————————————————————————————————————————————————————
    public void ViewOn(string Type, Vector Center/*Select時選擇點*/,
        List<Vector> Target,
        List<Vector> Range,
        List<Vector> Path,
        List<Vector> Select)
    {
        //變數----------------------------------------------------------------------------------------------------
        //細節分類
        string[] QuickSave_Type_StringArray = Type.Split("_"[0]);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (QuickSave_Type_StringArray[0])
        {
            #region - Range -
            case "Range":
                {
                    //----------------------------------------------------------------------------------------------------
                    if (Target != null)
                    {
                        if (Range == null || Range.Count == 0)
                        {
                            Range = Target;
                        }
                    }
                    foreach (Vector Vector in Range)
                    {
                        //變數----------------------------------------------------------------------------------------------------
                        //地塊
                        _Map_SelectUnit _Map_RangeSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //設定----------------------------------------------------------------------------------------------------
                        if (!_Map_RangeSelectUnit._State_InRange_Bool)
                        {
                            //設定狀態
                            _Map_RangeSelectUnit._State_InRange_Bool = true;
                            //設定視覺
                            _Map_RangeSelectUnit.ColorSet("Range");
                        }
                        //----------------------------------------------------------------------------------------------------

                        //加入索引----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInRange_ScriptsList.Contains(_Map_RangeSelectUnit))
                        {
                            _Map_RangeSelectUnit._Map_MouseSencer_Collider.enabled = true;
                            _Card_GroundUnitInRange_ScriptsList.Add(_Map_RangeSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }

                    //額外範圍----------------------------------------------------------------------------------------------------
                    //Path
                    foreach (Vector Vector in Path)
                    {
                        //取得地塊
                        _Map_SelectUnit _Map_PathSelectUnit = TakeSelectDeQueue(Vector);
                        //已經為Range
                        if (_Map_PathSelectUnit._State_InRange_Bool)
                        {
                            continue;
                        }

                        if (!_Map_PathSelectUnit._State_InRangePath_Bool)
                        {
                            //設定狀態
                            _Map_PathSelectUnit._State_InRangePath_Bool = true;
                            //設定視覺
                            _Map_PathSelectUnit.ColorSet("RangePath");
                        }

                        if (!_Card_GroundUnitInRangePath_ScriptsList.Contains(_Map_PathSelectUnit))
                        {
                            _Map_PathSelectUnit._Map_MouseSencer_Collider.enabled = true;
                            _Card_GroundUnitInRangePath_ScriptsList.Add(_Map_PathSelectUnit);
                        }
                    }


                    //Select
                    foreach (Vector Vector in Select)
                    {
                        //變數----------------------------------------------------------------------------------------------------
                        //取得地塊
                        _Map_SelectUnit _Map_SelectSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //判定----------------------------------------------------------------------------------------------------
                        //已經為Range/Path
                        if (_Map_SelectSelectUnit._State_InRange_Bool)
                        {
                            continue;
                        }
                        if (_Map_SelectSelectUnit._State_InRangePath_Bool)
                        {
                            continue;
                        }
                        //----------------------------------------------------------------------------------------------------

                        //設定----------------------------------------------------------------------------------------------------
                        if (!_Map_SelectSelectUnit._State_InRangeExtend_Bool)
                        {
                            //設定狀態
                            _Map_SelectSelectUnit._State_InRangeExtend_Bool = true;
                            //設定視覺
                            _Map_SelectSelectUnit.ColorSet("RangeExtend");
                        }
                        //----------------------------------------------------------------------------------------------------

                        //加入索引----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInRangeExtend_ScriptsList.Contains(_Map_SelectSelectUnit))
                        {
                            _Card_GroundUnitInRangeExtend_ScriptsList.Add(_Map_SelectSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            #endregion

            #region - Select -
            case "Select":
                {
                    //額外範圍----------------------------------------------------------------------------------------------------
                    //Path
                    foreach (Vector Vector in Path)
                    {
                        //變數----------------------------------------------------------------------------------------------------
                        //地塊
                        _Map_SelectUnit _Map_PathSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //設定----------------------------------------------------------------------------------------------------
                        //可做顯示選擇範圍內目標提示'
                        //設定視覺
                        _Map_PathSelectUnit.ColorSet("Path");
                        //----------------------------------------------------------------------------------------------------

                        //加入索引----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInPath_ScriptsList.Contains(_Map_PathSelectUnit))
                        {
                            _Card_GroundUnitInPath_ScriptsList.Add(_Map_PathSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }


                    //Select
                    foreach (Vector Vector in Select)
                    {
                        //變數----------------------------------------------------------------------------------------------------
                        //地塊
                        _Map_SelectUnit _Map_RangeSelectUnit = TakeSelectDeQueue(Vector);
                        //----------------------------------------------------------------------------------------------------

                        //設定----------------------------------------------------------------------------------------------------
                        //可做顯示選擇範圍內目標提示'
                        //設定視覺
                        _Map_RangeSelectUnit.ColorSet("Select");
                        //----------------------------------------------------------------------------------------------------

                        //加入索引----------------------------------------------------------------------------------------------------
                        if (!_Card_GroundUnitInSelect_ScriptsList.Contains(_Map_RangeSelectUnit))
                        {
                            _Card_GroundUnitInSelect_ScriptsList.Add(_Map_RangeSelectUnit);
                        }
                        //----------------------------------------------------------------------------------------------------
                    }

                    _Map_SelectUnit _Map_CenterSelectUnit = TakeSelectDeQueue(Center);
                    //使用點無效時
                    if (!_Card_GroundUnitInSelect_ScriptsList.Contains(_Map_CenterSelectUnit))
                    {
                        _Map_CenterSelectUnit.ColorSet("SelectNone");
                        _Card_GroundUnitInSelect_ScriptsList.Add(_Map_CenterSelectUnit);
                    }
                    //----------------------------------------------------------------------------------------------------

                }
                break;
            #endregion
            default:
                break;
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - ViewOff -
    //顯示地面範圍——————————————————————————————————————————————————————————————————————
    public void ViewOff(string Type)
    {
        //設置----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Range -
            case "Range":
                //關閉顯示----------------------------------------------------------------------------------------------------
                //範圍
                for (int a = 0; a < _Card_GroundUnitInRange_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRange_ScriptsList[a];
                    QuickSave_SelectUnit_Script._Map_MouseSencer_Collider.enabled = false;
                    QuickSave_SelectUnit_Script._State_InRange_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRange_ScriptsList.Clear();
                //路徑
                for (int a = 0; a < _Card_GroundUnitInRangePath_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRangePath_ScriptsList[a];
                    QuickSave_SelectUnit_Script._Map_MouseSencer_Collider.enabled = false;
                    QuickSave_SelectUnit_Script._State_InRangePath_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRangePath_ScriptsList.Clear();
                //範圍延伸
                for (int a = 0; a < _Card_GroundUnitInRangeExtend_ScriptsList.Count; a++)
                {
                    _Map_SelectUnit QuickSave_SelectUnit_Script =
                        _Card_GroundUnitInRangeExtend_ScriptsList[a];
                    QuickSave_SelectUnit_Script._State_InRangeExtend_Bool = false;
                    TakeSelectEnQueue(QuickSave_SelectUnit_Script);
                    QuickSave_SelectUnit_Script.ColorSet("Clear");
                }
                _Card_GroundUnitInRangeExtend_ScriptsList.Clear();
                //----------------------------------------------------------------------------------------------------
                break;
            #endregion

            #region - Select -
            case "Select":
                //設定----------------------------------------------------------------------------------------------------
                //路徑
                for (int a = 0; a < _Card_GroundUnitInPath_ScriptsList.Count; a++)
                {
                    _Card_GroundUnitInPath_ScriptsList[a].ColorSet("Clear");
                }
                _Card_GroundUnitInPath_ScriptsList.Clear();
                //選擇
                for (int a = 0; a < _Card_GroundUnitInSelect_ScriptsList.Count; a++)
                {
                    _Card_GroundUnitInSelect_ScriptsList[a].ColorSet("Clear");
                }
                _Card_GroundUnitInSelect_ScriptsList.Clear();
                //----------------------------------------------------------------------------------------------------
                break;
            #endregion
            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - PathSelect -
    //依照Vector(限定)與Direction整理(只有Key不相同)——————————————————————————————————————————————————————————————————————
    public List<PathSelectPairClass> PathSelectPair(PathCollectClass Path, SelectCollectClass Select, Vector Coordinate)
    {
        //----------------------------------------------------------------------------------------------------
        List<PathSelectPairClass> Answer_Return_ClassList = new List<PathSelectPairClass>();
        Dictionary<Vector, Dictionary<int, PathSelectPairClass>> QuickSave_Return_Dictionary =
            new Dictionary<Vector, Dictionary<int, PathSelectPairClass>>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Path.Data.Count == 0)
        {
            //無Path
            foreach (SelectUnitClass SelectUnit in Select.Data)
            {
                if (Coordinate != SelectUnit.Vector)
                {
                    continue;
                }
                if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                {
                    if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                    {
                        DirectionDic.Select.Add(SelectUnit);
                    }
                    else
                    {
                        PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                        {
                            Path = new List<PathUnitClass>(),
                            Select = new List<SelectUnitClass> { SelectUnit }
                        };
                        QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    }
                }
                else
                {
                    Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                        new Dictionary<int, PathSelectPairClass>();
                    PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                    {
                        Path = new List<PathUnitClass> (),
                        Select = new List<SelectUnitClass> { SelectUnit }
                    };
                    QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                }
            }
        }
        else
        {
            foreach (PathUnitClass PathUnit in Path.Data)
            {
                foreach (SelectUnitClass SelectUnit in Select.Data)
                {
                    if (Coordinate != PathUnit.Vector ||
                        Coordinate != SelectUnit.Vector)
                    {
                        continue;
                    }
                    if (PathUnit.Direction == SelectUnit.Direction)
                    {
                        if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                        {
                            if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                            {
                                DirectionDic.Path.Add(PathUnit);
                                DirectionDic.Select.Add(SelectUnit);
                            }
                            else
                            {
                                PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                                {
                                    Path = new List<PathUnitClass> { PathUnit },
                                    Select = new List<SelectUnitClass> { SelectUnit }
                                };
                                QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            }
                        }
                        else
                        {
                            Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                                new Dictionary<int, PathSelectPairClass>();
                            PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                            {
                                Path = new List<PathUnitClass> { PathUnit },
                                Select = new List<SelectUnitClass> { SelectUnit }
                            };
                            QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                        }
                    }
                }
            }
        }

        foreach (Dictionary<int, PathSelectPairClass> FDic in QuickSave_Return_Dictionary.Values)
        {
            foreach (PathSelectPairClass SDic in FDic.Values)
            {
                Answer_Return_ClassList.Add(SDic);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //依照Vector與Direction整理(只有Key不相同)——————————————————————————————————————————————————————————————————————
    public List<PathSelectPairClass> PathSelectPair(PathCollectClass Path, SelectCollectClass Select)
    {
        //----------------------------------------------------------------------------------------------------
        List<PathSelectPairClass> Answer_Return_ClassList = new List<PathSelectPairClass>();
        Dictionary<Vector, Dictionary<int, PathSelectPairClass>> QuickSave_Return_Dictionary = 
            new Dictionary<Vector, Dictionary<int, PathSelectPairClass>>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Path.Data.Count == 0)
        {
            foreach (SelectUnitClass SelectUnit in Select.Data)
            {
                if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                {
                    if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                    {
                        DirectionDic.Select.Add(SelectUnit);
                    }
                    else
                    {
                        PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                        {
                            Path = new List<PathUnitClass>(),
                            Select = new List<SelectUnitClass> { SelectUnit }
                        };
                        QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    }
                }
                else
                {
                    Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                        new Dictionary<int, PathSelectPairClass>();
                    PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                    {
                        Path = new List<PathUnitClass>(),
                        Select = new List<SelectUnitClass> { SelectUnit }
                    };
                    QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                    QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                }
            }
        }
        else
        {
            foreach (PathUnitClass PathUnit in Path.Data)
            {
                foreach (SelectUnitClass SelectUnit in Select.Data)
                {
                    if (PathUnit.Vector == SelectUnit.Vector &&
                        PathUnit.Direction == SelectUnit.Direction)
                    {
                        if (QuickSave_Return_Dictionary.TryGetValue(SelectUnit.Vector, out Dictionary<int, PathSelectPairClass> VectorDic))
                        {
                            if (VectorDic.TryGetValue(SelectUnit.Direction, out PathSelectPairClass DirectionDic))
                            {
                                DirectionDic.Path.Add(PathUnit);
                                DirectionDic.Select.Add(SelectUnit);
                            }
                            else
                            {
                                PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                                {
                                    Path = new List<PathUnitClass> { PathUnit },
                                    Select = new List<SelectUnitClass> { SelectUnit }
                                };
                                QuickSave_Return_Dictionary[SelectUnit.Vector].Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            }
                        }
                        else
                        {
                            Dictionary<int, PathSelectPairClass> QuickSave_PathSelect_Dictionary =
                                new Dictionary<int, PathSelectPairClass>();
                            PathSelectPairClass QuickSave_PathSelect_Class = new PathSelectPairClass
                            {
                                Path = new List<PathUnitClass> { PathUnit },
                                Select = new List<SelectUnitClass> { SelectUnit }
                            };
                            QuickSave_PathSelect_Dictionary.Add(SelectUnit.Direction, QuickSave_PathSelect_Class);
                            QuickSave_Return_Dictionary.Add(SelectUnit.Vector, QuickSave_PathSelect_Dictionary);
                        }
                    }
                }
            }
        }

        foreach (Dictionary<int, PathSelectPairClass> FDic in QuickSave_Return_Dictionary.Values)
        {
            foreach (PathSelectPairClass SDic in FDic.Values)
            {
                Answer_Return_ClassList.Add(SDic);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - ClassToVector -
    //DirectionRangeClass轉譯為VectorList——————————————————————————————————————————————————————————————————————
    public List<Vector> Range_ClassToVector(Vector Center, DirectionRangeClass RangeClass)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<Vector> Answer_Return_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //建立清單----------------------------------------------------------------------------------------------------
        int QuickSave_DirectionX_Int = RangeClass.DirectionX;
        int QuickSave_DirectionY_Int = RangeClass.DirectionY;
        BoolRangeClass QuickSave_Range_Class = RangeClass.Range;
        for (int y = 0; y < QuickSave_Range_Class.Coordinate.GetLength(1); y++)
        {
            for (int x = 0; x < QuickSave_Range_Class.Coordinate.GetLength(0); x++)
            {
                //設定變數----------------------------------------------------------------------------------------------------
                int QuickSave_XCoordinate_Int = Center.X;
                int QuickSave_YCoordinate_Int = Center.Y;
                if (QuickSave_DirectionX_Int * QuickSave_DirectionY_Int == 1)
                {
                    QuickSave_XCoordinate_Int = Center.X -
                        QuickSave_DirectionX_Int * ((x - QuickSave_Range_Class.Center.x) + (y - QuickSave_Range_Class.Center.y));
                    QuickSave_YCoordinate_Int = Center.Y +
                        QuickSave_DirectionY_Int * ((x - QuickSave_Range_Class.Center.x) - (y - QuickSave_Range_Class.Center.y));
                }
                else
                {
                    QuickSave_XCoordinate_Int = Center.X -
                        -QuickSave_DirectionX_Int * ((x - QuickSave_Range_Class.Center.x) - (y - QuickSave_Range_Class.Center.y));
                    QuickSave_YCoordinate_Int = Center.Y +
                        -QuickSave_DirectionY_Int * ((x - QuickSave_Range_Class.Center.x) + (y - QuickSave_Range_Class.Center.y));
                }
                Vector QuickSave_Coordinate_Class = new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
                //----------------------------------------------------------------------------------------------------

                //例外判定----------------------------------------------------------------------------------------------------
                //範圍存在判定
                if (!RangeClass.Range.Coordinate[x, y])
                {
                    continue;
                }
                //地圖遞塊綜合判定
                if ((QuickSave_XCoordinate_Int + QuickSave_YCoordinate_Int)%2 != 0)
                {
                    continue;
                }
                //----------------------------------------------------------------------------------------------------

                //執行----------------------------------------------------------------------------------------------------
                //新增至回傳值
                Answer_Return_ClassList.Add(QuickSave_Coordinate_Class);
                //----------------------------------------------------------------------------------------------------
            }
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - ScriptToClass
    //GroundUnit轉譯為BoolRangeClass——————————————————————————————————————————————————————————————————————
    public BoolRangeClass Range_ScriptToClass(Vector2Int Center, List<Vector2Int> Grounds)//new bool[1,1] = {X}
    {
        #region - GetArraySize -
        //變數----------------------------------------------------------------------------------------------------
        //轉譯後地板座標
        List<Vector2Int> QuickSave_GroundsInArray_Vector2List = new List<Vector2Int>();
        //最大尺寸
        int QuickSave_ArrayXMax_Int = 0;
        int QuickSave_ArrayYMax_Int = 0;
        //最小尺寸
        int QuickSave_ArrayXMin_Int = 0;
        int QuickSave_ArrayYMin_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //取得陣列大小----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Grounds.Count; a++)
        {
            //為中心點則脫離
            if (Grounds[a] == Center)
            {
                QuickSave_GroundsInArray_Vector2List.Add(new Vector2Int(0, 0));
                continue;
            }

            //確認當前檢索板塊座標
            Vector2Int QuickSave_Grounds_Vector3 = Grounds[a];
            //X
            int QuickSave_X_Int =
                ((QuickSave_Grounds_Vector3.x - Center.x) - (QuickSave_Grounds_Vector3.y - Center.y)) / 2;
            //Y
            int QuickSave_Y_Int =
                (-((QuickSave_Grounds_Vector3.x - Center.x) + (QuickSave_Grounds_Vector3.y - Center.y))) / 2;
            //加入轉譯後座標
            QuickSave_GroundsInArray_Vector2List.Add(new Vector2Int(QuickSave_X_Int, QuickSave_Y_Int));

            //取得最大值X
            if (QuickSave_X_Int > QuickSave_ArrayXMax_Int)
            {
                QuickSave_ArrayXMax_Int = QuickSave_X_Int;
            }
            //取得最小值X
            else if (QuickSave_X_Int < QuickSave_ArrayXMin_Int)
            {
                QuickSave_ArrayXMin_Int = QuickSave_X_Int; ;
            }
            //取得最大值Y
            if (QuickSave_Y_Int > QuickSave_ArrayYMax_Int)
            {
                QuickSave_ArrayYMax_Int = QuickSave_Y_Int;
            }
            //取得最小值Y
            else if (QuickSave_Y_Int < QuickSave_ArrayYMin_Int)
            {
                QuickSave_ArrayYMin_Int = QuickSave_Y_Int; ;
            }
        }
        //----------------------------------------------------------------------------------------------------
        #endregion

        //變數----------------------------------------------------------------------------------------------------
        //回傳陣列
        BoolRangeClass Answer_Range_Class =
            new BoolRangeClass { Coordinate = new bool[(QuickSave_ArrayXMax_Int - QuickSave_ArrayXMin_Int) + 1, (QuickSave_ArrayYMax_Int - QuickSave_ArrayYMin_Int) + 1] };
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //中心點設置
        Answer_Range_Class.Center = new Vector2Int(-QuickSave_ArrayXMin_Int, -QuickSave_ArrayYMin_Int);
        //範圍設置
        for (int a = 0; a < QuickSave_GroundsInArray_Vector2List.Count; a++)
        {
            Answer_Range_Class.Coordinate
                [QuickSave_GroundsInArray_Vector2List[a].x - QuickSave_ArrayXMin_Int,
                QuickSave_GroundsInArray_Vector2List[a].y - QuickSave_ArrayYMin_Int] = true;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Range_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - RangeCheck -
    //判斷是否位於範圍內——————————————————————————————————————————————————————————————————————
    public bool _Map_MapCheck_Bool(Vector Coordinate, int Xsize,int Ysize)
    {
        if ((Coordinate.X + Coordinate.Y) % 2 != 0)
        {
            return false;
        }
        if (Xsize <= Coordinate.x || Coordinate.x < 0 ||
            Ysize <= Coordinate.y || Coordinate.y < 0)
        {
            return false;
        }

        return true;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion RangeSet
}
