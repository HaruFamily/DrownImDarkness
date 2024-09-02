using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class _Map_BattleCreator : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //地面預製物
    public GameObject _Map_GroundUnit_GameObject;
    public Transform _Map_Store_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //地圖尺寸置入區
    public Vector2Int _Map_MapSize_Vector2;
    //地圖高度區間
    public Vector2Int _Map_MapHeight_Vector2;
    //地圖地板圖尺寸
    public float _Map_GroundUnitSize_Float;
    //地圖地板間格
    public float _Map_GroundUnitSpacing_Float;
    //地圖高度縮放
    public float _Map_GroundUnitHeightScale_Float;
    //----------------------------------------------------------------------------------------------------

    //生成機率參數----------------------------------------------------------------------------------------------------
    //板塊生成最低數量百分比
    public float _Map_GroundUnitChance_Float;
    public int _Map_MinGround_Int;
    //地圖板塊隨機生成率
    public int _Map_BranchProbability_Int;
    //----------------------------------------------------------------------------------------------------

    //座標庫----------------------------------------------------------------------------------------------------
    //地圖地板物件總座標庫﹝存放地形用，連結製地圖板塊程式﹞
    public bool[,] _Map_PreCreate_BoolArray;
    public _Map_BattleGroundUnit[,] _Map_GroundBoard_ScriptsArray;
    public Dictionary<Vector2Int, _Map_SelectUnit> _Map_SelectBoard_Dictionary = new Dictionary<Vector2Int, _Map_SelectUnit>();
    //----------------------------------------------------------------------------------------------------

    //板塊顯示----------------------------------------------------------------------------------------------------
    public List<_Map_BattleGroundUnit> _Card_GroundUnitViewOn_ScriptsList = new List<_Map_BattleGroundUnit>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        Vector3 _QuickSave_FarPos_Vector3 = _World_Manager.Infinity;

        //地板物件池製造----------------------------------------------------------------------------------------------------
        _Map_PreCreate_BoolArray = new bool[_Map_MapSize_Vector2.x, _Map_MapSize_Vector2.y];
        _Map_GroundBoard_ScriptsArray = new _Map_BattleGroundUnit[_Map_MapSize_Vector2.x, _Map_MapSize_Vector2.y];
        //----------------------------------------------------------------------------------------------------

        //總座標庫生成----------------------------------------------------------------------------------------------------
        MapPreCreate();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region BattleMapCreate
    #region MapCreate
    //地面生成預定——————————————————————————————————————————————————————————————————————
    private List<Vector3Int> _Map_FindCoordinate_VectorList = new List<Vector3Int>();
    private int _Map_NowCount_Int = 0;
    //確認生成布林值
    private void MapPreCreate()
    {
        //生成呼叫----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_FindCoordinate_VectorList.Clear();
        _Map_NowCount_Int = 0;
        //設定起始點
        int QuickSave_HeightAnswer = 1;
        Vector3Int QuickSave_StartPoint_Vector3 = new Vector3Int(
                    Mathf.RoundToInt(_Map_MapSize_Vector2.x * 0.5f),
                    Mathf.RoundToInt(_Map_MapSize_Vector2.y * 0.5f),
                    QuickSave_HeightAnswer);
        if ((QuickSave_StartPoint_Vector3.x + QuickSave_StartPoint_Vector3.y) %2 != 0)
        {
            QuickSave_StartPoint_Vector3 = new Vector3Int(
                        Mathf.RoundToInt(_Map_MapSize_Vector2.x * 0.5f) + 1,
                        Mathf.RoundToInt(_Map_MapSize_Vector2.y * 0.5f),
                        QuickSave_HeightAnswer);
        }
        _Map_FindCoordinate_VectorList.Add(QuickSave_StartPoint_Vector3);
        _Map_NowCount_Int++;
        #region - BranchCreate
        while (_Map_FindCoordinate_VectorList.Count > 0)
        {
            Vector3Int QuickSave_NowCoordinate_Vector = _Map_FindCoordinate_VectorList[0];
            _Map_FindCoordinate_VectorList.RemoveAt(0);
            //位置確認----------------------------------------------------------------------------------------------------
            if (!_Map_Manager._Map_MapCheck_Bool(new Vector (QuickSave_NowCoordinate_Vector), _Map_PreCreate_BoolArray.GetLength(0), _Map_PreCreate_BoolArray.GetLength(1)))
            {
                continue;
            }
            if (_Map_PreCreate_BoolArray[QuickSave_NowCoordinate_Vector.x, QuickSave_NowCoordinate_Vector.y])
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //預製設置----------------------------------------------------------------------------------------------------
            if (QuickSave_NowCoordinate_Vector.z == 1)
            {
                _Map_PreCreate_BoolArray[QuickSave_NowCoordinate_Vector.x, QuickSave_NowCoordinate_Vector.y] = true;
            }
            //----------------------------------------------------------------------------------------------------

            //位移設置----------------------------------------------------------------------------------------------------
            //樹狀生成
            for (int a =1; a < 5; a++)
            {
                if (!(_Map_MinGround_Int - _Map_NowCount_Int > 0))
                {
                    if (_Map_GroundUnitChance_Float < Random.Range(0, 100))
                    {
                        continue;
                    }
                }

                Vector3Int QuickSave_NewCoordinate_Vector = new Vector3Int();
                switch (a)
                {
                    case 1:
                        QuickSave_NewCoordinate_Vector =
                            new Vector3Int(QuickSave_NowCoordinate_Vector.x + 1, QuickSave_NowCoordinate_Vector.y + 1, QuickSave_HeightAnswer);
                        break;
                    case 2:
                        QuickSave_NewCoordinate_Vector =
                            new Vector3Int(QuickSave_NowCoordinate_Vector.x - 1, QuickSave_NowCoordinate_Vector.y + 1, QuickSave_HeightAnswer);
                        break;
                    case 3:
                        QuickSave_NewCoordinate_Vector =
                            new Vector3Int(QuickSave_NowCoordinate_Vector.x - 1, QuickSave_NowCoordinate_Vector.y - 1, QuickSave_HeightAnswer);
                        break;
                    case 4:
                        QuickSave_NewCoordinate_Vector =
                            new Vector3Int(QuickSave_NowCoordinate_Vector.x + 1, QuickSave_NowCoordinate_Vector.y - 1, QuickSave_HeightAnswer);
                        break;
                }
                _Map_FindCoordinate_VectorList.Add(QuickSave_NewCoordinate_Vector);
                _Map_NowCount_Int++;
            }
            //----------------------------------------------------------------------------------------------------
        }
        #endregion

        MapCreate("Test");
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //清單生成——————————————————————————————————————————————————————————————————————
    private void MapCreate(string Region)
    {
        //取得索引字串----------------------------------------------------------------------------------------------------
        string QuickSave_Region_String = "Region";
        string QuickSave_Type_String = "";
        for (int x = 0; x < _Map_MapSize_Vector2.x; x++)
        {
            for (int y = 0; y < _Map_MapSize_Vector2.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    if (_Map_PreCreate_BoolArray[x,y])
                    {
                        QuickSave_Type_String = "Ground";
                    }
                    else
                    {
                        QuickSave_Type_String = "Air";
                    }
                    _Map_BattleGroundUnit _Answer_QueueTake_Scripts = 
                        Instantiate(_Map_GroundUnit_GameObject, this.transform.position, Quaternion.identity, _Map_Store_Transform).GetComponent<_Map_BattleGroundUnit>();
                    _Map_GroundBoard_ScriptsArray[x, y] = _Answer_QueueTake_Scripts;
                    Vector QuickSave_Coordinate_Class = new Vector(x, y);
                    _Answer_QueueTake_Scripts.SystemStart
                        (QuickSave_Region_String,
                        QuickSave_Type_String,
                        QuickSave_Coordinate_Class, 
                        _Math_CooridnateTransform_Vector2(QuickSave_Coordinate_Class),
                        (int)(x + y * _Map_MapSize_Vector2.x));
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //建置完成後動作----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager.MapGroundUnitComplete();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion MapCreate
    #endregion

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
        List<int> DirectionRange,Tuple PathRange,
        string RangeType, string PathType, string SelectType,
        BoolRangeClass Range, List<Vector> Path, BoolRangeClass Select,
        Vector UserCoordinate = null, Vector TargetCoordinate = null)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //分流範圍顯示----------------------------------------------------------------------------------------------------
        switch (RangeType)
        {
            case "Directional":
                Answer_Range_ClassList =
                    Range_Directional
                    (Key, PathDic, SelectDic,
                    DirectionRange, PathRange,
                    PathType, SelectType,
                    Range, Path, Select,
                    UserCoordinate, TargetCoordinate);
                break;
            case "Overall":
                Answer_Range_ClassList =
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
        return Answer_Range_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Find

    #region - Range -
    #region - Range_Directional -
    //方向性同設定——————————————————————————————————————————————————————————————————————
    private List<Vector> Range_Directional
        (string Key,
        List<PathUnitClass>  PathDic, List<SelectUnitClass> SelectDic,
        List<int> DirectionRange, Tuple PathRange,
        string PathType, string SelectType,
        BoolRangeClass RangeData, List<Vector> PathData, BoolRangeClass SelectData,
        Vector UserCoordinate, Vector TargetCoordinate)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
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
                    //Vector
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
                        _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
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
                //範圍存在判定
                if (!RangeData.Coordinate[x, y] &&
                    (RangeData.Center.x != x && RangeData.Center.y != y))
                {
                    continue;
                }
                if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
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
        Vector StartCoordinate/*選擇的地塊非使用者*/, Vector TargetCoordinate = null,
        int XOffset = 0, int YOffset = 0, List<Vector> PathData = null)
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
        public int X;
        public int Y;
        public int Distance;
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
            //Y
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
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
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
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();

        //與目標距離
        int[,] QuickSave_DistanceToTarget_IntArray =
            new int[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];
        //玩家移動距離
        int[,] QuickSave_DistancePlayerWalk_IntArray =
            new int[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];
        //來源方向﹝ 1 = 左上至右下： 2 = 右上至左下： 3 = 右下至左上： 4 = 左下至右上﹞
        sbyte[,] QuickSave_DirectionComeFrom_ByteArray =
            new sbyte[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];

        //搜尋目錄
        List<PathFindClass> QuickSave_FindingList_ClassList = new List<PathFindClass>();//不可Queueu(要Sort)
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        //與目標距離
        for (int y = 0; y <= _Map_MapSize_Vector2.y; y++)
        {
            for (int x = 0; x <= _Map_MapSize_Vector2.x; x++)
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
            X = StartCoordinate.X,
            Y = StartCoordinate.Y,
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
            if (QuickSave_Find_Class.X == TargetCoordinate.Vector2Int.x&&
                QuickSave_Find_Class.Y == TargetCoordinate.Vector2Int.y)
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
                    QuickSave_NextCoordinate_Class = new Vector(QuickSave_Find_Class.X + x, QuickSave_Find_Class.Y + y);

                    #region - Check -
                    //地塊類型判定
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_NextCoordinate_Class,
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
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
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 2;
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
                            QuickSave_DirectionComeFrom_ByteArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] = 4;
                        }
                    }
                    //設定搜尋單位
                    QuickSave_FindingList_ClassList.Add(new PathFindClass
                    {
                        X = QuickSave_NextCoordinate_Class.X,
                        Y = QuickSave_NextCoordinate_Class.Y,
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
                Center = new Vector2Int(Center.x, Center.y);
                QuickSave_Select_Class.Range = new BoolRangeClass { Center = Center, Coordinate = RangeData.Coordinate };
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        //return Range_ClassToVector(UserCoordinate, QuickSave_Select_Class);
        return QuickSave_Select_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion RangeFind

    #region Object 
    //目標——————————————————————————————————————————————————————————————————————
    public List<_Map_BattleObjectUnit> RangeTargets(        
        string Type/*目標類型*/, List<SelectUnitClass> SelectRange, 
        SourceClass UserSource,
        int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<_Map_BattleObjectUnit> Answer_Return_ScriptList = new List<_Map_BattleObjectUnit>();
        //捷徑
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<Vector> QuickSave_GroundUnits_ClassList = new List<Vector>();
        foreach (SelectUnitClass Select in SelectRange)
        {
            QuickSave_GroundUnits_ClassList.AddRange(_Map_Manager.Range_ClassToVector(Select.Vector, Select.Select));
        }
        foreach (Vector Key in QuickSave_GroundUnits_ClassList)
        {
            //高度確認;
            Answer_Return_ScriptList.AddRange(
                _Object_Manager.TimeObjects(Type, UserSource, Time, Order, Key));            
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion Object

    #region ViewSet
    //——————————————————————————————————————————————————————————————————————
    public void ViewSet(Vector Center,int Range)
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<_Map_BattleGroundUnit> QuickSave_InRangeGround_ScriptsList = new List<_Map_BattleGroundUnit>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int x = -Range; x <= Range; x++)
        {
            int XCoor = Center.X + x;
            for (int y = -Range; y <= Range; y++)
            {
                int YCoor = Center.Y + y;
                if (Mathf.Abs(x) <= Range &&
                    Mathf.Abs(y) <= Range &&
                    _Map_Manager._Map_MapCheck_Bool(new Vector (XCoor, YCoor),
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
                {
                    _Map_BattleGroundUnit QuickSave_Ground_Script = _Map_GroundBoard_ScriptsArray[XCoor, YCoor];
                    QuickSave_Ground_Script.ViewSet(true);
                    QuickSave_InRangeGround_ScriptsList.Add(QuickSave_Ground_Script);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < _Card_GroundUnitViewOn_ScriptsList.Count; a++)
        {
            if (!QuickSave_InRangeGround_ScriptsList.Contains(_Card_GroundUnitViewOn_ScriptsList[a]))
            {
                _Card_GroundUnitViewOn_ScriptsList[a].ViewSet(false);
            }
        }
        _Card_GroundUnitViewOn_ScriptsList = QuickSave_InRangeGround_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion ViewSet

    #region CooridnateMath
    //場景轉換——————————————————————————————————————————————————————————————————————
    //二維座標轉換為地面物件----------------------------------------------------------------------------------------------------
    public _Map_BattleGroundUnit _Math_CooridnateV2ToGroundUnit_Script(Vector InputCoordinate)
    {
        _Map_BattleGroundUnit Answer_Coordinate_Script = _Map_GroundBoard_ScriptsArray[InputCoordinate.X, InputCoordinate.Y];
        return Answer_Coordinate_Script;
    }
    //﹞----------------------------------------------------------------------------------------------------

    //畫面XY轉譯座標----------------------------------------------------------------------------------------------------
    public Vector2 _Math_CooridnateTransform_Vector2(Vector Coordinate)
    {
        Vector2 Answer_Return_Vector2 = new Vector2
            (Coordinate.x * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float),
            -Coordinate.y * 0.5f * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float) + 
            (_Map_GroundUnitHeightScale_Float * Coordinate.z));
        return Answer_Return_Vector2;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion CooridnateMath
}
