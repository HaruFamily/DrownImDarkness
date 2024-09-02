using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class _Map_FieldCreator : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //地面預製物
    public GameObject _Map_GroundUnit_GameObject;
    public Transform _Map_Store_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //地圖地板圖尺寸
    public float _Map_GroundUnitSize_Float;
    //地圖地板間格
    public float _Map_GroundUnitSpacing_Float;
    //----------------------------------------------------------------------------------------------------

    //基本設定----------------------------------------------------------------------------------------------------
    //地圖稀有度
    public Vector2Int _Map_RarityRange_Vector2;
    //地圖高級稀有度
    public Vector2 _Map_RarityHighIncrease_Vector2;
    //----------------------------------------------------------------------------------------------------

    //資料----------------------------------------------------------------------------------------------------
    //地圖地板物件總座標庫﹝存放地形用，連結製地圖板塊程式﹞
    public Dictionary<Vector2Int, _Map_FieldGroundUnit> _Map_GroundBoard_Dictionary = new Dictionary<Vector2Int, _Map_FieldGroundUnit>();
    public Dictionary<Vector2Int, _Map_SelectUnit> _Map_SelectBoard_Dictionary = new Dictionary<Vector2Int, _Map_SelectUnit>();

    public Dictionary<string, _Map_Manager.FieldDataClass> _Map_Data_Dictionary = new Dictionary<string, _Map_Manager.FieldDataClass>();

    public Transform _Map_ScriptsStore_Transform;
    private Queue<_Map_FieldGroundUnit> _Map_GroundPool_ScriptsQueue = new Queue<_Map_FieldGroundUnit>();
    //----------------------------------------------------------------------------------------------------

    //板塊顯示----------------------------------------------------------------------------------------------------
    public List<Vector> _Card_GroundUnitViewOn_ClassList = new List<Vector>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region Start
    //起始呼叫/*總生成*/——————————————————————————————————————————————————————————————————————
    public void SystemStart(string Map)
    {
        //建立資料----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_Manager.FieldDataClass QuickSave_Data_Class = 
            new _Map_Manager.FieldDataClass();
        _Map_Manager.MapDataClass QuickSave_MapData_Class =
            _Map_Manager._Data_Map_Dictionary[Map];
        //地板資料
        Dictionary<Vector2Int, _Map_Manager.GroundDataClass> QuickSave_GroundData_Dictionary =
            new Dictionary<Vector2Int, _Map_Manager.GroundDataClass>();
        //指定事件位置
        Dictionary<string, List<Vector2Int>> QuickSave_QuantityEvent_Dictionary = 
            new Dictionary<string, List<Vector2Int>>();
        //區域資料
        Dictionary<string, _Map_Manager.RegionDataClass> QuickSave_RegionData_Dictionary = 
            new Dictionary<string, _Map_Manager.RegionDataClass>();
        string[,] QuickSave_Map_StringArray = QuickSave_MapData_Class.Map;
        string[,] QuickSave_NewMap_StringArray =
            new string[QuickSave_MapData_Class.Size.x, QuickSave_MapData_Class.Size.y];

        HashSet<string> QuickSave_Region_StringHashSet = new HashSet<string>();
        HashSet<string> QuickSave_EmptyRegion_StringHashSet = new HashSet<string>();//．的區域的Key
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //資料設定
        QuickSave_Data_Class.Size = QuickSave_MapData_Class.Size;
        QuickSave_Data_Class.Data = QuickSave_GroundData_Dictionary;
        QuickSave_Data_Class.Syndrome.Add("All", QuickSave_MapData_Class.Syndrome);
        QuickSave_Data_Class.TimeEvent.Add("All", QuickSave_MapData_Class.TimeEvent);
        //紀錄所有Region資料
        foreach (string Key in QuickSave_MapData_Class.RegionCode.Keys)
        {
            List<string> QuickSave_Value_StringList = QuickSave_MapData_Class.RegionCode[Key];
            if (Key == "．")
            {
                QuickSave_EmptyRegion_StringHashSet.UnionWith(QuickSave_Value_StringList);
            }
            QuickSave_Region_StringHashSet.UnionWith(QuickSave_Value_StringList);
        }
        foreach (string KeyValue in QuickSave_Region_StringHashSet)
        {
            _Map_Manager.RegionDataClass QuickSave_RegionData_Class = 
                _Map_Manager._Data_Region_Dictionary[KeyValue];
            QuickSave_RegionData_Dictionary.Add(KeyValue, QuickSave_RegionData_Class);
            QuickSave_QuantityEvent_Dictionary.Add(KeyValue, new List<Vector2Int>());
            QuickSave_Data_Class.Syndrome.Add(KeyValue, QuickSave_RegionData_Class.Syndrome);
            QuickSave_Data_Class.TimeEvent.Add(KeyValue, QuickSave_RegionData_Class.TimeEvent);
        }
        //設定地形區域
        for (int y = 0; y < QuickSave_Map_StringArray.GetLength(1); y++)
        {
            for (int x = 0; x < QuickSave_Map_StringArray.GetLength(0); x++)
            {
                if (QuickSave_MapData_Class.RegionCode.TryGetValue(QuickSave_Map_StringArray[x, y], out List<string> Region))
                {
                    string QuickSave_RandomRegion_String = Region[Random.Range(0, Region.Count)];
                    QuickSave_Map_StringArray[x, y] = QuickSave_RandomRegion_String;
                }
                else
                {
                    QuickSave_Map_StringArray[x, y] = "";
                }
            }
        }
        //地形延伸
        bool QuickSave_BranchNext_Bool = true;
        int QuickSave_BranchCount_Int = 0;
        while (QuickSave_BranchNext_Bool)
        {
            QuickSave_BranchNext_Bool = false;
            for (int y = 0; y < QuickSave_Map_StringArray.GetLength(1); y++)
            {
                for (int x = 0; x < QuickSave_Map_StringArray.GetLength(0); x++)
                {
                    string QuickSave_Region_String = QuickSave_Map_StringArray[x,y];
                    QuickSave_NewMap_StringArray[x, y] = QuickSave_Region_String;
                    if (QuickSave_Region_String == "")
                    {
                        continue;
                    }
                    List<KeyValuePair<string, int>> QuickSave_RegionPair_PairList =
                        QuickSave_RegionData_Dictionary[QuickSave_Region_String].Branch;
                    if (QuickSave_BranchCount_Int < QuickSave_RegionPair_PairList.Count)
                    {
                        QuickSave_BranchNext_Bool = true;
                        string QuickSave_Type_String =
                            QuickSave_RegionPair_PairList[QuickSave_BranchCount_Int].Key;
                        int QuickSave_Probability_Int =
                            QuickSave_RegionPair_PairList[QuickSave_BranchCount_Int].Value;
                        switch (QuickSave_Type_String)
                        {
                            case "Extend"://(周圍延伸)
                                {
                                    //跳出
                                    if ((x + 1 >= QuickSave_Map_StringArray.GetLength(0)) ||
                                        (x - 1 < 0) ||
                                        (y + 1 >= QuickSave_Map_StringArray.GetLength(1)) ||
                                        (y - 1 < 0))
                                    {
                                        continue;
                                    }
                                    //延伸
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x + 1, y + 1]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x + 1, y + 1] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x + 1, y - 1]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x + 1, y - 1] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x - 1, y + 1]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x - 1, y + 1] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x - 1, y - 1]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x - 1, y - 1] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                }
                                break;
                            case "Step"://跳步(隔一格)
                                {
                                    //跳出
                                    if ((x + 2 >= QuickSave_Map_StringArray.GetLength(0)) ||
                                        (x - 2 < 0) ||
                                        (y + 2 >= QuickSave_Map_StringArray.GetLength(1)) ||
                                        (y - 2 < 0))
                                    {
                                        continue;
                                    }
                                    //延伸
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x + 2, y + 2]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x + 2, y + 2] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x + 2, y - 2]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x + 2, y - 2] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x - 2, y + 2]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x - 2, y + 2] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                    if (QuickSave_EmptyRegion_StringHashSet.Contains(
                                        QuickSave_Map_StringArray[x - 2, y - 2]))
                                    {
                                        if (Random.Range(0, 100) < QuickSave_Probability_Int)
                                        {
                                            QuickSave_NewMap_StringArray[x - 2, y - 2] =
                                                QuickSave_Region_String;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            QuickSave_BranchCount_Int++;
            QuickSave_Map_StringArray = QuickSave_NewMap_StringArray;
        }
        //設置
        for (int y = 0; y< QuickSave_Map_StringArray.GetLength(1); y++)
        {
            for (int x = 0; x < QuickSave_Map_StringArray.GetLength(0); x++)
            {
                string QuickSave_Region_String = QuickSave_Map_StringArray[x, y];
                if (QuickSave_Region_String == "")
                {
                    continue;
                }
                Vector2Int QuickSave_NowCoor_Vector = new Vector2Int(x, y);
                _Map_Manager.GroundDataClass QuickSave_GroundUnitData_Class = new _Map_Manager.GroundDataClass();
                //Region
                QuickSave_GroundUnitData_Class.Region = QuickSave_Region_String;
                //Tags
                QuickSave_GroundUnitData_Class.Tags = 
                    new List<string>(QuickSave_RegionData_Dictionary[QuickSave_Region_String].Tags);
                //Event
                if (QuickSave_MapData_Class.TargetEvent.TryGetValue(QuickSave_NowCoor_Vector, out string Event))
                {
                    //指定事件
                    QuickSave_GroundUnitData_Class.Event = Event;
                }
                else
                {
                    //隨機事件
                    List<Dictionary<string, int>> QuickSave_RegionEventData_DictionaryList =
                        QuickSave_RegionData_Dictionary[QuickSave_Region_String].RandomEvent;//已經依照數量複製了
                    int QuickSave_Random_Int =
                        Random.Range(0, QuickSave_RegionEventData_DictionaryList.Count);
                    Dictionary<string, int> QuickSave_EventPair_Dictionary =
                        QuickSave_RegionEventData_DictionaryList[QuickSave_Random_Int];
                    List<string> QuickSave_RandomEvents_StringList = new List<string>();
                    foreach (string Key in QuickSave_EventPair_Dictionary.Keys)
                    {
                        for (int r = 0; r < QuickSave_EventPair_Dictionary[Key]; r++)
                        {
                            QuickSave_RandomEvents_StringList.Add(Key);
                        }
                    }
                    QuickSave_Random_Int = 
                        Random.Range(0, QuickSave_RandomEvents_StringList.Count);
                    QuickSave_GroundUnitData_Class.Event = 
                        QuickSave_RandomEvents_StringList[QuickSave_Random_Int];
                    QuickSave_QuantityEvent_Dictionary[QuickSave_Region_String].Add(QuickSave_NowCoor_Vector);
                }
                QuickSave_GroundData_Dictionary.Add(QuickSave_NowCoor_Vector, QuickSave_GroundUnitData_Class);
            }
        }

        //隨機限量事件
        foreach (string KeyValue in QuickSave_QuantityEvent_Dictionary.Keys)
        {
            List<Dictionary<string, int>> QuickSave_RegionEventData_DictionaryList =
                QuickSave_RegionData_Dictionary[KeyValue].QuantityEvent;//已經依照數量複製了
            List<Vector2Int> QuickSave_QuantityEvent_VectorList =
                QuickSave_QuantityEvent_Dictionary[KeyValue];
            foreach (Dictionary<string, int> QuantityEvent in QuickSave_RegionEventData_DictionaryList)
            {
                List<string> QuickSave_RandomEvents_StringList = new List<string>();
                foreach (string Key in QuantityEvent.Keys)
                {
                    for (int r = 0; r < QuantityEvent[Key]; r++)
                    {
                        QuickSave_RandomEvents_StringList.Add(Key);
                    }
                }
                int QuickSave_Random_Int =
                    Random.Range(0, QuickSave_RandomEvents_StringList.Count);
                string QuickSave_RandomEvent_String = 
                    QuickSave_RandomEvents_StringList[QuickSave_Random_Int];
                if(QuickSave_QuantityEvent_VectorList.Count > 0)
                {
                    QuickSave_Random_Int =
                        Random.Range(0, QuickSave_QuantityEvent_VectorList.Count);
                    Vector2Int QuickSave_Random_Vector =
                        QuickSave_QuantityEvent_VectorList[QuickSave_Random_Int];
                    QuickSave_QuantityEvent_VectorList.Remove(QuickSave_Random_Vector);
                    QuickSave_GroundData_Dictionary[QuickSave_Random_Vector].Event =
                        QuickSave_RandomEvent_String;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //起始位置
        _World_Manager._Object_Manager._Object_Player_Script.
            _Creature_FieldObjectt_Script._Map_Coordinate_Class =
            new Vector(4, 4);
        //移除資料(減少記憶體消耗)
        _Map_Manager._Data_Region_Dictionary.Remove(Map);
        _Map_Data_Dictionary.Add(Map, QuickSave_Data_Class);
        //繼續
        _World_Manager._Map_Manager.MapGroundUnitComplete();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region RangeFind
    //範圍(可選取)/路徑(行動路徑，受阻擋則停止)/範圍(抵達觸發目標後的範圍)
    //RangeType：
    //Directional：方向性同設定
    //Overall：全向性同設定
    //PathType：
    //Null：無路徑(瞬間出現)
    //AStar：路徑搜尋格數(起點開始搜尋)
    //Point：點對點(可能會斜線跳動)(起點至終點)
    //SelectType：
    //Block：同設定
    //All：等同於Range的範圍
    #region - FindDivert -
    //尋找分流——————————————————————————————————————————————————————————————————————
    public List<Vector> Find_Divert
        (string Key,
        List<PathUnitClass> PathDic, List<SelectUnitClass> SelectDic,
        List<int> DirectionRange, Tuple PathRange,
        string RangeType, string PathType, string SelectType,
        BoolRangeClass Range, List<Vector> Path, BoolRangeClass Select,
        Vector UserCoordinate = null, Vector TargetCoordinate = null)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<Vector> Answer_Range_Class = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //分流範圍顯示----------------------------------------------------------------------------------------------------
        switch (RangeType)
        {
            case "Directional":
                Answer_Range_Class =
                    Range_Directional
                    (Key, PathDic, SelectDic,
                    DirectionRange, PathRange,
                    PathType, SelectType,
                    Range, Path, Select,
                    UserCoordinate, TargetCoordinate);
                break;
            case "Overall":
                Answer_Range_Class =
                    Range_Overall
                    (Key, PathDic, SelectDic,
                    PathRange,
                    PathType, SelectType,
                    Range, Path, Select,
                    UserCoordinate, TargetCoordinate);
                break;

            default:
                print("Wrong Key in RangeType with﹝" + RangeType + "﹞");
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Range_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Find

    #region - Range -
    #region - Range_Directional -
    //方向性同設定——————————————————————————————————————————————————————————————————————
    private List<Vector> Range_Directional
        (string Key,
        List<PathUnitClass> PathDic, List<SelectUnitClass> SelectDic,
        List<int> DirectionRange, Tuple PathRange,
        string PathType, string SelectType,
        BoolRangeClass RangeData, List<Vector> PathData, BoolRangeClass SelectData,
        Vector UserCoordinate, Vector TargetCoordinate)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //四周範圍顯示----------------------------------------------------------------------------------------------------
        for (int a = 0; a < DirectionRange.Count; a++)
        {
            int XOffset = 0;
            int YOffset = 0;
            switch (DirectionRange[a])
            {
                case 1:
                    XOffset = 1;
                    YOffset = 1;
                    break;
                case 2:
                    XOffset = -1;
                    YOffset = 1;
                    break;
                case 3:
                    XOffset = -1;
                    YOffset = -1;
                    break;
                case 4:
                    XOffset = 1;
                    YOffset = -1;
                    break;
            }

            //建立清單----------------------------------------------------------------------------------------------------
            for (int y = 0; y < RangeData.Coordinate.GetLength(1); y++)
            {
                for (int x = 0; x < RangeData.Coordinate.GetLength(0); x++)
                {
                    //設定變數----------------------------------------------------------------------------------------------------
                    int QuickSave_XCoordinate_Int = UserCoordinate.X;
                    int QuickSave_YCoordinate_Int = UserCoordinate.Y;
                    if (XOffset * YOffset == 1)
                    {
                        QuickSave_XCoordinate_Int = UserCoordinate.X -
                            XOffset * ((x - RangeData.Center.x) + (y - RangeData.Center.y));
                        QuickSave_YCoordinate_Int = UserCoordinate.Y +
                            YOffset * ((x - RangeData.Center.x) - (y - RangeData.Center.y));
                    }
                    else
                    {
                        QuickSave_XCoordinate_Int = UserCoordinate.X -
                            -XOffset * ((x - RangeData.Center.x) - (y - RangeData.Center.y));
                        QuickSave_YCoordinate_Int = UserCoordinate.Y +
                            -YOffset * ((x - RangeData.Center.x) + (y - RangeData.Center.y));
                    }
                    Vector QuickSave_Coordinate_Class =
                        new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
                    //----------------------------------------------------------------------------------------------------

                    //例外判定----------------------------------------------------------------------------------------------------
                    //範圍存在判定                    
                    if (!RangeData.Coordinate[x, y] && 
                        (RangeData.Center.x != x && RangeData.Center.y != y))
                    {
                        continue;
                    }
                    //地圖遞塊綜合判定
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                        _Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                        _Map_Data_Dictionary[QuickSave_Map_String].Size.y))
                    {
                        continue;
                    }
                    //----------------------------------------------------------------------------------------------------

                    //複雜設置----------------------------------------------------------------------------------------------------
                    List<Vector> QuickSave_SelectExtend_ClassList = new List<Vector>();//延伸點
                    if (RangeData.Coordinate[x, y])
                    {
                        Answer_Range_ClassList.Add(QuickSave_Coordinate_Class);
                    }

                    //Path
                    if (PathData != null)
                    {
                        DirectionPathClass QuickSave_Path_Class =
                            Path(PathType, QuickSave_Coordinate_Class, TargetCoordinate, XOffset, YOffset, PathData);
                        if (QuickSave_Path_Class != null && QuickSave_Path_Class.Path.Count > 0)
                        {
                            List<sbyte> QuickSave_Direction_ClassList = QuickSave_Path_Class.Direction;
                            List<Vector> QuickSave_Path_ClassList = QuickSave_Path_Class.Path;

                            for (int p = (int)PathRange.Min; p < QuickSave_Path_ClassList.Count; p++)
                            {
                                if (p > PathRange.Max)
                                {
                                    break;
                                }
                                Vector2Int QuickSave_PathCoordinate_Vector = QuickSave_Path_ClassList[p].Vector2Int;
                                QuickSave_SelectExtend_ClassList.Add(QuickSave_Path_ClassList[p]);
                                DirectionPathClass QuickSave_NewPath_Class = null;
                                switch (PathType)
                                {
                                    case "Instant":
                                        {
                                            QuickSave_NewPath_Class = new DirectionPathClass
                                            {
                                                Direction =
                                                new List<sbyte> { QuickSave_Direction_ClassList[0], QuickSave_Direction_ClassList[p] },
                                                Path =
                                                new List<Vector> { QuickSave_Path_ClassList[0], QuickSave_Path_ClassList[p] }
                                            };
                                        }
                                        break;
                                    default:
                                        {
                                            QuickSave_NewPath_Class = new DirectionPathClass
                                            {
                                                Direction = QuickSave_Direction_ClassList.GetRange(0, p + 1),
                                                Path = QuickSave_Path_ClassList.GetRange(0, p + 1)
                                            };
                                        }
                                        break;
                                }

                                //新增
                                PathUnitClass QuickSave_PathUnit_Class = new PathUnitClass
                                {
                                    Key = Key,
                                    Vector = new Vector(QuickSave_PathCoordinate_Vector),
                                    Direction = DirectionRange[a],
                                    Path = QuickSave_NewPath_Class
                                };
                                PathDic.Add(QuickSave_PathUnit_Class);
                            }
                        }
                        else
                        {
                            if (RangeData.Coordinate[x, y])
                            {
                                QuickSave_SelectExtend_ClassList.Add(QuickSave_Coordinate_Class);
                            }
                        }
                    }
                    else
                    {
                        if (RangeData.Coordinate[x, y])
                        {
                            QuickSave_SelectExtend_ClassList.Add(QuickSave_Coordinate_Class);
                        }
                    }

                    //Select
                    for (int s = 0; s < QuickSave_SelectExtend_ClassList.Count; s++)
                    {
                        DirectionRangeClass QuickSave_Select_Class =
                            Select(SelectType,
                            QuickSave_SelectExtend_ClassList[s],
                            new Vector2Int(XOffset, YOffset),
                            new Vector2Int(x, y),
                            RangeData, SelectData);
                        if (QuickSave_Select_Class != null)
                        {
                            Vector2Int QuickSave_SelectCoordinate_Vector = QuickSave_SelectExtend_ClassList[s].Vector2Int;
                            //新增
                            SelectUnitClass QuickSave_SelectUnit_Class = new SelectUnitClass
                            {
                                Key = Key,
                                Vector = new Vector(QuickSave_SelectCoordinate_Vector),
                                Direction = DirectionRange[a],
                                Select = QuickSave_Select_Class
                            };
                            SelectDic.Add(QuickSave_SelectUnit_Class);
                        }
                    }
                    //----------------------------------------------------------------------------------------------------
                }
            }
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Range_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Range_Overall -
    //方向性線性——————————————————————————————————————————————————————————————————————
    private List<Vector> Range_Overall
        (string Key,
        List<PathUnitClass> PathDic, List<SelectUnitClass> SelectDic,
        Tuple PathRange,
        string PathType, string SelectType,
        BoolRangeClass RangeData, List<Vector> PathData, BoolRangeClass SelectData,
        Vector UserCoordinate, Vector TargetCoordinate)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        //----------------------------------------------------------------------------------------------------

        //範圍顯示----------------------------------------------------------------------------------------------------
        for (int y = 0; y < RangeData.Coordinate.GetLength(1); y++)
        {
            for (int x = 0; x < RangeData.Coordinate.GetLength(0); x++)
            {
                //設定變數----------------------------------------------------------------------------------------------------
                int QuickSave_XCoordinate_Int = UserCoordinate.X +
                        ((x - RangeData.Center.x) + (y - RangeData.Center.y));
                int QuickSave_YCoordinate_Int = UserCoordinate.Y -
                        ((x - RangeData.Center.x) - (y - RangeData.Center.y));
                Vector QuickSave_Coordinate_Class =
                    new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
                //----------------------------------------------------------------------------------------------------

                //例外判定----------------------------------------------------------------------------------------------------
                if (!RangeData.Coordinate[x, y] &&
                    (RangeData.Center.x != x && RangeData.Center.y != y))
                {
                    continue;
                }
                //範圍存在判定
                if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.y))
                {
                    continue;
                }
                //----------------------------------------------------------------------------------------------------

                //複雜設置----------------------------------------------------------------------------------------------------
                List<Vector> QuickSave_SelectExtend_ClassList = new List<Vector>();//延伸點
                if (RangeData.Coordinate[x, y])
                {
                    Answer_Range_ClassList.Add(QuickSave_Coordinate_Class);
                }
                //Path
                if (PathData != null)
                {
                    DirectionPathClass QuickSave_Path_Class =
                        Path(PathType, QuickSave_Coordinate_Class, TargetCoordinate, 1, 1, PathData);
                    if (QuickSave_Path_Class != null && QuickSave_Path_Class.Path.Count > 1)
                    {
                        List<sbyte> QuickSave_Direction_ClassList = QuickSave_Path_Class.Direction;
                        List<Vector> QuickSave_Path_ClassList = QuickSave_Path_Class.Path;

                        for (int p = (int)PathRange.Min; p < QuickSave_Path_ClassList.Count; p++)
                        {
                            if (p > PathRange.Max)
                            {
                                break;
                            }
                            Vector2Int QuickSave_PathCoordinate_Vector = QuickSave_Path_ClassList[p].Vector2Int;
                            DirectionPathClass QuickSave_NewPath_Class = null;
                            switch (PathType)
                            {
                                case "Instant":
                                    {
                                        QuickSave_NewPath_Class = new DirectionPathClass
                                        {
                                            Direction =
                                            new List<sbyte> { QuickSave_Direction_ClassList[0], QuickSave_Direction_ClassList[p] },
                                            Path =
                                            new List<Vector> { QuickSave_Path_ClassList[0], QuickSave_Path_ClassList[p] }
                                        };
                                    }
                                    break;
                                default:
                                    {
                                        QuickSave_NewPath_Class = new DirectionPathClass
                                        {
                                            Direction = QuickSave_Direction_ClassList.GetRange(0, p + 1),
                                            Path = QuickSave_Path_ClassList.GetRange(0, p + 1)
                                        };
                                    }
                                    break;
                            }

                            //新增
                            PathUnitClass QuickSave_PathUnit_Class = new PathUnitClass
                            {
                                Key = Key,
                                Vector = new Vector(QuickSave_PathCoordinate_Vector),
                                Direction = 0,
                                Path = QuickSave_NewPath_Class
                            };
                            PathDic.Add(QuickSave_PathUnit_Class);
                        }
                    }
                    else
                    {
                        if (RangeData.Coordinate[x, y])
                        {
                            QuickSave_SelectExtend_ClassList.Add(QuickSave_Coordinate_Class);
                        }
                    }
                }
                else
                {
                    if (RangeData.Coordinate[x, y])
                    {
                        QuickSave_SelectExtend_ClassList.Add(QuickSave_Coordinate_Class);
                    }
                }

                //Select
                for (int s = 0; s < QuickSave_SelectExtend_ClassList.Count; s++)
                {
                    DirectionRangeClass QuickSave_Select_Class =
                        Select(SelectType,
                        QuickSave_SelectExtend_ClassList[s],
                        new Vector2Int(-1, -1),
                        new Vector2Int(x, y),
                        RangeData, SelectData);
                    if (QuickSave_Select_Class != null)
                    {
                        Vector2Int QuickSave_SelectCoordinate_Vector = QuickSave_SelectExtend_ClassList[s].Vector2Int;
                        //新增
                        SelectUnitClass QuickSave_SelectUnit_Class = new SelectUnitClass
                        {
                            Key = Key,
                            Vector = new Vector(QuickSave_SelectCoordinate_Vector),
                            Direction = 0,
                            Select = QuickSave_Select_Class
                        };
                        SelectDic.Add(QuickSave_SelectUnit_Class);
                    }
                }
                //----------------------------------------------------------------------------------------------------
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Range_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region - Path -
    //——————————————————————————————————————————————————————————————————————
    public DirectionPathClass Path(
        string PathType,
        Vector StartCoordinate/*選擇的地塊非使用者*/, Vector TargetCoordinate,
        int XOffset = 0,int YOffset = 0, List<Vector> PathData = null)
    {
        //----------------------------------------------------------------------------------------------------
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (PathType)
        {
            case "Null":
                Answer_Return_Class = null;
                break;
            case "Normal":
            case "Instant":
                Answer_Return_Class =
                    Path_Data(XOffset, YOffset, StartCoordinate, PathData);
                break;
            case "PointToPoint":
                break;
            case "AStar":
                Answer_Return_Class =
                    Path_AStar(StartCoordinate, TargetCoordinate);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //變數——————————————————————————————————————————————————————————————————————
    //搜尋單位類別
    class PathFindClass
    {
        public Vector Coordinate;
        public int Distance;
        public bool End;
    }
    //——————————————————————————————————————————————————————————————————————


    #region - Data -
    //——————————————————————————————————————————————————————————————————————
    public DirectionPathClass Path_Data(
        int XDirection, int YDirection, Vector StartCoordinate, List<Vector> Path)
    {
        //變數----------------------------------------------------------------------------------------------------
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();
        List<sbyte> QuickSave_Direction_IntList = new List<sbyte>();
        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        //----------------------------------------------------------------------------------------------------

        //搜尋----------------------------------------------------------------------------------------------------
        //可能超出地圖範圍
        sbyte QuickSave_Direction_Int = _World_Manager.Direction(XDirection, YDirection);
        QuickSave_Direction_IntList.Add(QuickSave_Direction_Int);
        QuickSave_Path_ClassList.Add(new Vector(StartCoordinate));
        for (int a = 0; a < Path.Count; a++)
        {
            Vector QuickSave_PathUnit_Class = Path[a];
            int QuickSave_XCoordinate_Int = 0;
            int QuickSave_YCoordinate_Int = 0;
            if (XDirection * YDirection == 1)
            {
                QuickSave_XCoordinate_Int = StartCoordinate.X -
                    XDirection * (QuickSave_PathUnit_Class.X - QuickSave_PathUnit_Class.Y);
                QuickSave_YCoordinate_Int = StartCoordinate.Y +
                    YDirection * (QuickSave_PathUnit_Class.X + QuickSave_PathUnit_Class.Y);
            }
            else
            {
                QuickSave_XCoordinate_Int = StartCoordinate.X -
                    -XDirection * (QuickSave_PathUnit_Class.X + QuickSave_PathUnit_Class.Y);
                QuickSave_YCoordinate_Int = StartCoordinate.Y +
                    -YDirection * (QuickSave_PathUnit_Class.X - QuickSave_PathUnit_Class.Y);
            }
            Vector QuickSave_Coordinate_Class =
                new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
            //地圖遞塊綜合判定
            if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.y))
            {
                break;
            }
            QuickSave_Direction_IntList.Add(QuickSave_Direction_Int);
            QuickSave_Path_ClassList.Add(QuickSave_Coordinate_Class);
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        Answer_Return_Class.Direction = QuickSave_Direction_IntList;
        Answer_Return_Class.Path = QuickSave_Path_ClassList;
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Path_PointToPoint -
    //——————————————————————————————————————————————————————————————————————
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Path_AStar -
    //路徑搜尋格數——————————————————————————————————————————————————————————————————————
    public DirectionPathClass Path_AStar(
        Vector StartCoordinate, Vector TargetCoordinate)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();
        Vector2Int QuickSave_Size_Vector = _Map_Data_Dictionary[QuickSave_Map_String].Size;

        //與目標距離
        int[,] QuickSave_DistanceToTarget_IntArray =
            new int[QuickSave_Size_Vector.x + 1,
            QuickSave_Size_Vector.y + 1];
        //玩家移動距離
        int[,] QuickSave_DistancePlayerWalk_IntArray =
            new int[QuickSave_Size_Vector.x + 1,
            QuickSave_Size_Vector.y + 1];
        //來源方向﹝ 1 = 左上至右下： 2 = 右上至左下： 3 = 右下至左上： 4 = 左下至右上﹞
        sbyte[,] QuickSave_DirectionComeFrom_ByteArray =
            new sbyte[QuickSave_Size_Vector.x + 1,
            QuickSave_Size_Vector.y + 1];

        //搜尋目錄
        List<PathFindClass> QuickSave_FindingList_ClassList = new List<PathFindClass>();//不可Queueu(要Sort)
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        //與目標距離
        for (int y = 0; y <= QuickSave_Size_Vector.y; y++)
        {
            for (int x = 0; x <= QuickSave_Size_Vector.x; x++)
            {
                //距離設置
                if (Mathf.Abs(x - TargetCoordinate.X) >
                    Mathf.Abs(y - TargetCoordinate.Y))
                {
                    QuickSave_DistanceToTarget_IntArray[x, y] = Mathf.Abs(x - TargetCoordinate.X) * 10;
                }
                else
                {
                    QuickSave_DistanceToTarget_IntArray[x, y] = Mathf.Abs(y - TargetCoordinate.Y) * 10;
                }
            }
        }
        //最基礎搜尋點
        QuickSave_FindingList_ClassList.Add(new PathFindClass
        {
            Coordinate = StartCoordinate,
            Distance = 0
        });
        //----------------------------------------------------------------------------------------------------

        //搜尋----------------------------------------------------------------------------------------------------
        while (QuickSave_FindingList_ClassList.Count > 0)
        {
            //當前搜尋物
            PathFindClass QuickSave_Find_Class = QuickSave_FindingList_ClassList[0];
            QuickSave_FindingList_ClassList.RemoveAt(0);

            //跳出判定
            if (QuickSave_Find_Class.Coordinate.Vector2Int == TargetCoordinate.Vector2Int)
            {
                break;
            }

            //四向延伸搜尋
            //下個距離
            int QuickSave_NextDistance_Int = QuickSave_Find_Class.Distance + 1;
            Vector QuickSave_NextCoordinate_Class = new Vector();
            #region - QuadrantFinding -
            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    QuickSave_NextCoordinate_Class = new Vector(QuickSave_Find_Class.Coordinate.X + x, QuickSave_Find_Class.Coordinate.Y + y);

                    #region - Check -
                    //地塊類型判定
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_NextCoordinate_Class,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.y))
                    {
                        continue;
                    }

                    //是否已搜查
                    if (QuickSave_DistancePlayerWalk_IntArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] != 0)
                    {
                        continue;
                    }
                    #endregion

                    //設定世界記號
                    QuickSave_DistancePlayerWalk_IntArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = QuickSave_NextDistance_Int;
                    if (x == 1)
                    {
                        if (y == 1)
                        {
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 1;
                        }
                        else
                        {
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 4;
                        }
                    }
                    else if (x == -1)
                    {
                        if (y == -1)
                        {
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 3;
                        }
                        else
                        {
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 2;
                        }
                    }
                    //設定搜尋單位
                    QuickSave_FindingList_ClassList.Add(new PathFindClass
                    {
                        Coordinate = QuickSave_NextCoordinate_Class,
                        Distance = QuickSave_NextDistance_Int
                    });

                }
            }
            #endregion

            //整理環境
            //依照距離排序
            QuickSave_FindingList_ClassList.Sort((x, y) => x.Distance.CompareTo(y.Distance));
        }
        //----------------------------------------------------------------------------------------------------

        //取得路徑----------------------------------------------------------------------------------------------------
        //逆向搜尋體
        Vector QuickSave_BackFind_Class = new Vector(TargetCoordinate);
        List<Vector> QuickSave_PathCoordinate_ClassList = new List<Vector>(); ;
        List<sbyte> QuickSave_PathDirection_ByteList = new List<sbyte>();

        //開始搜尋
        while (QuickSave_BackFind_Class.Vector2Int != StartCoordinate.Vector2Int)
        {
            sbyte QuickSave_DirectionComeFrom = QuickSave_DirectionComeFrom_ByteArray[QuickSave_BackFind_Class.X, QuickSave_BackFind_Class.Y];
            //判斷何處過來
            QuickSave_PathDirection_ByteList.Add(QuickSave_DirectionComeFrom);
            QuickSave_PathCoordinate_ClassList.Add(
                new Vector(QuickSave_BackFind_Class));
            switch (QuickSave_DirectionComeFrom)
            {
                case 1:
                    //左上到右下
                    QuickSave_BackFind_Class.x -= 1;
                    QuickSave_BackFind_Class.y -= 1;

                    break;
                case 2:
                    //右上到左下
                    QuickSave_BackFind_Class.x -= 1;
                    QuickSave_BackFind_Class.y += 1;
                    break;
                case 3:
                    //右下到左上
                    QuickSave_BackFind_Class.x += 1;
                    QuickSave_BackFind_Class.y += 1;
                    break;
                case 4:
                    //左下到右上
                    QuickSave_BackFind_Class.x += 1;
                    QuickSave_BackFind_Class.y -= 1;
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        Answer_Return_Class.Direction = QuickSave_PathDirection_ByteList;
        Answer_Return_Class.Path = QuickSave_PathCoordinate_ClassList;
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region - Select -
    //同設定——————————————————————————————————————————————————————————————————————
    public DirectionRangeClass Select
        (string SelectType,
        Vector UserCoordinate,
        Vector2Int Direction,
        Vector2Int Center,
        BoolRangeClass RangeData, BoolRangeClass SelectData)
    {
        //變數----------------------------------------------------------------------------------------------------
        DirectionRangeClass QuickSave_Select_Class = new DirectionRangeClass();
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (SelectType)
        {
            case "Block":
                QuickSave_Select_Class.Direction = Direction;
                QuickSave_Select_Class.Range = SelectData;
                break;

            case "All":
                QuickSave_Select_Class.Direction = Direction;
                QuickSave_Select_Class.Range = new BoolRangeClass { Center = Center, Coordinate = RangeData.Coordinate };
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        //List<Vector> Answer_Return_ClassList = Range_ClassToDictionary(UserCoordinate, QuickSave_Select_Class);
        return QuickSave_Select_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion RangeFind

    #region ViewSet
    //——————————————————————————————————————————————————————————————————————
    public void ViewSet(Vector Center, int Range)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        List<_Map_FieldGroundUnit> QuickSave_Grounds_ScriptsList = new List<_Map_FieldGroundUnit>();
        List<Vector> QuickSave_Grounds_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<Vector> QuickSave_Finded_ClassList = new List<Vector>();
        QuickSave_Finded_ClassList.Add(Center);
        List<PathFindClass> QuickSave_Find_ClassList = new List<PathFindClass>();
        QuickSave_Find_ClassList.Add(new PathFindClass
        {
            Coordinate = Center,
            Distance = 0
        });
        while (QuickSave_Find_ClassList.Count > 0)
        {
            PathFindClass QuickSave_First_Class = QuickSave_Find_ClassList[0];
            QuickSave_Find_ClassList.RemoveAt(0);
            Vector QuickSave_Coor_Class = QuickSave_First_Class.Coordinate;
            int QuickSave_Dis_Class = QuickSave_First_Class.Distance;
            if (QuickSave_Dis_Class > Range)
            {
                continue;
            }
            QuickSave_Grounds_ClassList.Add(QuickSave_Coor_Class);
            //不再延伸
            if (QuickSave_First_Class.End)
            {
                continue;
            }

            #region - QuadrantFinding -
            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    Vector QuickSave_NextCoordinate_Class = 
                        new Vector(QuickSave_Coor_Class.X + x, QuickSave_Coor_Class.Y + y);

                    #region - Check -
                    //是否已搜查
                    if (QuickSave_Finded_ClassList.Contains(QuickSave_NextCoordinate_Class))
                    {
                        continue;
                    }

                    //地塊類型判定
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_NextCoordinate_Class,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                            _Map_Data_Dictionary[QuickSave_Map_String].Size.y))
                    {
                        continue;
                    }

                    //地塊視野判定
                    bool QuickSave_End_Bool = false;
                    if (!StayCheck("View", QuickSave_NextCoordinate_Class, QuickSave_Coor_Class, null))
                    {
                        QuickSave_End_Bool = true;
                    }
                    #endregion

                    //設定世界記號
                    QuickSave_Finded_ClassList.Add(QuickSave_NextCoordinate_Class);
                    //設定搜尋單位
                    QuickSave_Find_ClassList.Add(new PathFindClass
                    {
                        Coordinate = QuickSave_NextCoordinate_Class,
                        Distance = QuickSave_Dis_Class + 1,
                        End = QuickSave_End_Bool
                    });

                }
            }
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<Vector> QuickSave_AddVector_ClassList = new List<Vector>(QuickSave_Grounds_ClassList);
        List<Vector> QuickSave_RemoveVector_ClassList = new List<Vector>(_Card_GroundUnitViewOn_ClassList);

        QuickSave_AddVector_ClassList.RemoveAll
            (itemA => _Card_GroundUnitViewOn_ClassList.Exists(itemB => itemB.Vector2Int == itemA.Vector2Int));
        QuickSave_RemoveVector_ClassList.RemoveAll
            (itemA => QuickSave_Grounds_ClassList.Exists(itemB => itemB.Vector2Int == itemA.Vector2Int));

        foreach (Vector AddVector in QuickSave_AddVector_ClassList)
        {
            _Map_FieldGroundUnit QuickSave_Ground_Script = TakeGroundDeQueue(AddVector);
            if (QuickSave_Ground_Script != null)
            {
                _Card_GroundUnitViewOn_ClassList.Add(AddVector);
            }
        }
        foreach (Vector RemoveVector in QuickSave_RemoveVector_ClassList)
        {
            _Card_GroundUnitViewOn_ClassList.Remove(RemoveVector);
            _Map_GroundBoard_Dictionary[RemoveVector.Vector2Int].
                ViewSet(0.95f, false);
        }
        foreach (Vector ViewOn in _Card_GroundUnitViewOn_ClassList)
        {
            float QuickSave_DistancePow_Float = 
                Mathf.Pow(ViewOn.Distance(Center) / Range, 1.3f);
            float QuickSave_MyDistanceColor_Float =
                QuickSave_DistancePow_Float * 0.9f;//0-0,1-0.8
            _Map_GroundBoard_Dictionary[ViewOn.Vector2Int].
                ViewSet(QuickSave_MyDistanceColor_Float, false);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    #region - GroundQueueSet -
    //——————————————————————————————————————————————————————————————————————
    public _Map_FieldGroundUnit TakeGroundDeQueue(Vector MyCoordinate)//拿出
    {
        //----------------------------------------------------------------------------------------------------
        _Map_FieldGroundUnit Answer_Return_Script = null;
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        _Map_Manager.FieldDataClass QuickSave_Data_Class =
            _Map_Data_Dictionary[QuickSave_Map_String];

        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Data_Class.Data.TryGetValue(MyCoordinate.Vector2Int, out _Map_Manager.GroundDataClass DicValue))
        {
            if (_Map_GroundBoard_Dictionary.TryGetValue(MyCoordinate.Vector2Int, out _Map_FieldGroundUnit Value))
            {
                Answer_Return_Script = Value;
            }
            else
            {
                if (_Map_GroundPool_ScriptsQueue.Count > 0)
                {
                    Answer_Return_Script = _Map_GroundPool_ScriptsQueue.Dequeue();
                }
                else
                {
                    Answer_Return_Script =
                        Instantiate(_Map_GroundUnit_GameObject, _Map_Store_Transform).GetComponent<_Map_FieldGroundUnit>();
                }
                //開始
                Answer_Return_Script.SystemStart
                    (MyCoordinate,
                    _Math_CooridnateTransform_Vector2(MyCoordinate),
                    (int)(MyCoordinate.x + MyCoordinate.y * QuickSave_Data_Class.Size.x),
                    DicValue);

                if (DicValue.Event != "Null")
                {
                    Answer_Return_Script.EventViewSet(
                        _World_Manager._UI_Manager._UI_EventManager._Data_Event_Dictionary[DicValue.Event].OwnTag[0]);
                }
                else
                {
                    Answer_Return_Script.EventViewSet("Null");
                }
                _Map_GroundBoard_Dictionary.Add(MyCoordinate.Vector2Int, Answer_Return_Script);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void TakeGroundEnQueue(Vector Coordinate)//收回
    {
        //----------------------------------------------------------------------------------------------------
        /*
        if (_Map_GroundBoard_Dictionary.TryGetValue(Coordinate.Vector2Int, out _Map_FieldGroundUnit Value))
        {
            Value.ViewSet(0, true);
            /*
            Value.name = "Standby";
            Value.transform.localPosition = Vector3.zero;
            Value.transform.localScale = Vector3.zero;
            _Map_GroundPool_ScriptsQueue.Enqueue(Value);
            _Map_GroundBoard_Dictionary.Remove(Coordinate.Vector2Int);
        }
        else
        {

            //print("NoKey:" + Coordinate.Vector2Int);
        }*/
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion ViewSet

    #region CooridnateMath
    //場景轉換——————————————————————————————————————————————————————————————————————
    //轉譯座標----------------------------------------------------------------------------------------------------
    public Vector2 _Math_CooridnateTransform_Vector2(Vector InputCoordinate)
    {
        //座標
        Vector2 Answer_Coordinate_Vector2 = new Vector2(
            InputCoordinate.x * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float),
            -InputCoordinate.y * 0.5f * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float));
        return Answer_Coordinate_Vector2;
    }
    //----------------------------------------------------------------------------------------------------
    #endregion CooridnateMath

    #region Check
    //是否能過來——————————————————————————————————————————————————————————————————————
    public bool StayCheck(string Type/*View、Move*/, Vector Target, Vector Now, SourceClass UserSource)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        _Map_Manager.FieldDataClass QuickSave_Data_Class =
            _Map_Data_Dictionary[QuickSave_Map_String];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        #region - 
        /*
        //確認進入離開方向(可能有只能單向的Region)
        int QuickSave_NowDirection_Int = 0;
        int QuickSave_TargetDirection_Int = 0;
        int QuickSave_Xgap_Int = (int)(Target.x - Now.x);
        int QuickSave_Ygap_Int = (int)(Target.y - Now.y);
        if (QuickSave_Xgap_Int >= 0)
        {
            if (QuickSave_Ygap_Int >= 0)
            {
                QuickSave_NowDirection_Int = 1;
                QuickSave_TargetDirection_Int = 3;
            }
            else
            {
                QuickSave_NowDirection_Int = 4;
                QuickSave_TargetDirection_Int = 2;
            }
        }
        else
        {
            if (QuickSave_Ygap_Int >= 0)
            {
                QuickSave_NowDirection_Int = 2;
                QuickSave_TargetDirection_Int = 4;
            }
            else
            {
                QuickSave_NowDirection_Int = 3;
                QuickSave_TargetDirection_Int = 1;
            }
        }*/
        #endregion
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_Data_Class.Data.TryGetValue(Target.Vector2Int, out _Map_Manager.GroundDataClass TargetValue))
        {
            foreach (string Tag in TargetValue.Tags)
            {
                switch(Tag)
                {
                    case "UnStay":
                        {
                            if (Type == "Stay")
                            {
                                return false;
                            }
                        }
                        break;
                    case "UnPass":
                        {
                            if (Type == "Pass")
                            {
                                return false;
                            }
                        }
                        break;
                    case "UnView":
                        {
                            if (Type == "View")
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
        }
        else
        {
            return false;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Check
}
