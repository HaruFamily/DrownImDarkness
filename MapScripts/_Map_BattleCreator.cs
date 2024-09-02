using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class _Map_BattleCreator : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    //�a���w�s��
    public GameObject _Map_GroundUnit_GameObject;
    public Transform _Map_Store_Transform;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //�a�Ϥؤo�m�J��
    public Vector2Int _Map_MapSize_Vector2;
    //�a�ϰ��װ϶�
    public Vector2Int _Map_MapHeight_Vector2;
    //�a�Ϧa�O�Ϥؤo
    public float _Map_GroundUnitSize_Float;
    //�a�Ϧa�O����
    public float _Map_GroundUnitSpacing_Float;
    //�a�ϰ����Y��
    public float _Map_GroundUnitHeightScale_Float;
    //----------------------------------------------------------------------------------------------------

    //�ͦ����v�Ѽ�----------------------------------------------------------------------------------------------------
    //�O���ͦ��̧C�ƶq�ʤ���
    public float _Map_GroundUnitChance_Float;
    public int _Map_MinGround_Int;
    //�a�ϪO���H���ͦ��v
    public int _Map_BranchProbability_Int;
    //----------------------------------------------------------------------------------------------------

    //�y�Юw----------------------------------------------------------------------------------------------------
    //�a�Ϧa�O�����`�y�Юw���s��a�ΥΡA�s���s�a�ϪO���{����
    public bool[,] _Map_PreCreate_BoolArray;
    public _Map_BattleGroundUnit[,] _Map_GroundBoard_ScriptsArray;
    public Dictionary<Vector2Int, _Map_SelectUnit> _Map_SelectBoard_Dictionary = new Dictionary<Vector2Int, _Map_SelectUnit>();
    //----------------------------------------------------------------------------------------------------

    //�O�����----------------------------------------------------------------------------------------------------
    public List<_Map_BattleGroundUnit> _Card_GroundUnitViewOn_ScriptsList = new List<_Map_BattleGroundUnit>();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox

    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart()
    {
        Vector3 _QuickSave_FarPos_Vector3 = _World_Manager.Infinity;

        //�a�O������s�y----------------------------------------------------------------------------------------------------
        _Map_PreCreate_BoolArray = new bool[_Map_MapSize_Vector2.x, _Map_MapSize_Vector2.y];
        _Map_GroundBoard_ScriptsArray = new _Map_BattleGroundUnit[_Map_MapSize_Vector2.x, _Map_MapSize_Vector2.y];
        //----------------------------------------------------------------------------------------------------

        //�`�y�Юw�ͦ�----------------------------------------------------------------------------------------------------
        MapPreCreate();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start

    #region BattleMapCreate
    #region MapCreate
    //�a���ͦ��w�w�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private List<Vector3Int> _Map_FindCoordinate_VectorList = new List<Vector3Int>();
    private int _Map_NowCount_Int = 0;
    //�T�{�ͦ����L��
    private void MapPreCreate()
    {
        //�ͦ��I�s----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_FindCoordinate_VectorList.Clear();
        _Map_NowCount_Int = 0;
        //�]�w�_�l�I
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
            //��m�T�{----------------------------------------------------------------------------------------------------
            if (!_Map_Manager._Map_MapCheck_Bool(new Vector (QuickSave_NowCoordinate_Vector), _Map_PreCreate_BoolArray.GetLength(0), _Map_PreCreate_BoolArray.GetLength(1)))
            {
                continue;
            }
            if (_Map_PreCreate_BoolArray[QuickSave_NowCoordinate_Vector.x, QuickSave_NowCoordinate_Vector.y])
            {
                continue;
            }
            //----------------------------------------------------------------------------------------------------

            //�w�s�]�m----------------------------------------------------------------------------------------------------
            if (QuickSave_NowCoordinate_Vector.z == 1)
            {
                _Map_PreCreate_BoolArray[QuickSave_NowCoordinate_Vector.x, QuickSave_NowCoordinate_Vector.y] = true;
            }
            //----------------------------------------------------------------------------------------------------

            //�첾�]�m----------------------------------------------------------------------------------------------------
            //�𪬥ͦ�
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�M��ͦ��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private void MapCreate(string Region)
    {
        //���o���ަr��----------------------------------------------------------------------------------------------------
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

        //�ظm������ʧ@----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager.MapGroundUnitComplete();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion MapCreate
    #endregion

    #region RangeFind
    //�d��(�i���)/���|(��ʸ��|�A�����׫h����)/�d��(��FĲ�o�ؼЫ᪺�d��)
    //RangeType�G
    //Directional�G��V�ʦP�]�w
    //Overall�G���V�ʦP�]�w
    //PathType�G
    //Null�G�L���|(�����X�{)
    //AStar�G���|�j�M���(�_�I�}�l�j�M)
    //Point�G�I���I(�i��|�׽u����)(�_�I�ܲ��I)
    //SelectType�G
    //Block�G�P�]�w
    //All�G���P��Range���d��
    #region - FindDivert -
    //�M����y�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    
    public List<Vector> Find_Divert
        (string Key,
        List<PathUnitClass> PathDic, List<SelectUnitClass> SelectDic,
        List<int> DirectionRange,Tuple PathRange,
        string RangeType, string PathType, string SelectType,
        BoolRangeClass Range, List<Vector> Path, BoolRangeClass Select,
        Vector UserCoordinate = null, Vector TargetCoordinate = null)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //���y�d�����----------------------------------------------------------------------------------------------------
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
                print("Wrong Key in RangeType with��" + RangeType + "��");
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Range_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Find

    #region - Range -
    #region - Range_Directional -
    //��V�ʦP�]�w�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private List<Vector> Range_Directional
        (string Key,
        List<PathUnitClass>  PathDic, List<SelectUnitClass> SelectDic,
        List<int> DirectionRange, Tuple PathRange,
        string PathType, string SelectType,
        BoolRangeClass RangeData, List<Vector> PathData, BoolRangeClass SelectData,
        Vector UserCoordinate, Vector TargetCoordinate)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //�|�P�d�����----------------------------------------------------------------------------------------------------

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

            //�إ߲M��----------------------------------------------------------------------------------------------------
            for (int y = 0; y < RangeData.Coordinate.GetLength(1); y++)
            {
                for (int x = 0; x < RangeData.Coordinate.GetLength(0); x++)
                {
                    //�]�w�ܼ�----------------------------------------------------------------------------------------------------
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

                    //�ҥ~�P�w----------------------------------------------------------------------------------------------------
                    //�d��s�b�P�w              
                    if (!RangeData.Coordinate[x, y] &&
                        (RangeData.Center.x != x && RangeData.Center.y != y))
                    {
                        continue;
                    }
                    //�a�ϻ�����X�P�w
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                        _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
                    {
                        continue;
                    }
                    //----------------------------------------------------------------------------------------------------

                    //�����]�m----------------------------------------------------------------------------------------------------
                    List<Vector> QuickSave_SelectExtend_ClassList = new List<Vector>();//�����I
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
                                //�s�W
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
                            //�s�W
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Range_Overall -
    //��V�ʽu�ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private List<Vector> Range_Overall
        (string Key,
        List<PathUnitClass> PathDic, List<SelectUnitClass> SelectDic,
        Tuple PathRange,
        string PathType, string SelectType,
        BoolRangeClass RangeData, List<Vector> PathData, BoolRangeClass SelectData,
        Vector UserCoordinate, Vector TargetCoordinate)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<Vector> Answer_Range_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //�d�����----------------------------------------------------------------------------------------------------
        for (int y = 0; y < RangeData.Coordinate.GetLength(1); y++)
        {
            for (int x = 0; x < RangeData.Coordinate.GetLength(0); x++)
            {
                //�]�w�ܼ�----------------------------------------------------------------------------------------------------
                int QuickSave_XCoordinate_Int = UserCoordinate.X +
                        ((x - RangeData.Center.x) + (y - RangeData.Center.y));
                int QuickSave_YCoordinate_Int = UserCoordinate.Y -
                        ((x - RangeData.Center.x) - (y - RangeData.Center.y));
                Vector QuickSave_Coordinate_Class =
                    new Vector(QuickSave_XCoordinate_Int, QuickSave_YCoordinate_Int);
                //----------------------------------------------------------------------------------------------------

                //�ҥ~�P�w----------------------------------------------------------------------------------------------------
                //�d��s�b�P�w
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

                //�����]�m----------------------------------------------------------------------------------------------------
                List<Vector> QuickSave_SelectExtend_ClassList = new List<Vector>();//�����I
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
                            //�s�W
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
                        //�s�W
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion

    #region - Path -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public DirectionPathClass Path(
        string PathType,
        Vector StartCoordinate/*��ܪ��a���D�ϥΪ�*/, Vector TargetCoordinate = null,
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ܼơX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�j�M������O
    class PathFindClass
    {
        public int X;
        public int Y;
        public int Distance;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    #region - Data -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public DirectionPathClass Path_Data(
        int XDirection, int YDirection, Vector StartCoordinate, List<Vector> Path)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();
        List<sbyte> QuickSave_Direction_IntList = new List<sbyte>();
        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        //----------------------------------------------------------------------------------------------------

        //�j�M----------------------------------------------------------------------------------------------------
        //�i��W�X�a�Ͻd��
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
            //�a�ϻ�����X�P�w
            if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_Coordinate_Class,
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
            {
                break;
            }

            QuickSave_Direction_IntList.Add(QuickSave_Direction_Int);
            QuickSave_Path_ClassList.Add(QuickSave_Coordinate_Class);
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        Answer_Return_Class.Direction = QuickSave_Direction_IntList;
        Answer_Return_Class.Path = QuickSave_Path_ClassList;
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Path_PointToPoint -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Path_AStar -
    //���|�j�M��ơX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public DirectionPathClass Path_AStar(
        Vector StartCoordinate, Vector TargetCoordinate)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        DirectionPathClass Answer_Return_Class = new DirectionPathClass();

        //�P�ؼжZ��
        int[,] QuickSave_DistanceToTarget_IntArray =
            new int[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];
        //���a���ʶZ��
        int[,] QuickSave_DistancePlayerWalk_IntArray =
            new int[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];
        //�ӷ���V�� 1 = ���W�ܥk�U�G 2 = �k�W�ܥ��U�G 3 = �k�U�ܥ��W�G 4 = ���U�ܥk�W��
        sbyte[,] QuickSave_DirectionComeFrom_ByteArray =
            new sbyte[_Map_MapSize_Vector2.x + 1,
            _Map_MapSize_Vector2.y + 1];

        //�j�M�ؿ�
        List<PathFindClass> QuickSave_FindingList_ClassList = new List<PathFindClass>();//���iQueueu(�nSort)
        //----------------------------------------------------------------------------------------------------

        //��¦�]�w----------------------------------------------------------------------------------------------------
        //�P�ؼжZ��
        for (int y = 0; y <= _Map_MapSize_Vector2.y; y++)
        {
            for (int x = 0; x <= _Map_MapSize_Vector2.x; x++)
            {
                //�Z���]�m
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
        //�̰�¦�j�M�I
        QuickSave_FindingList_ClassList.Add(new PathFindClass 
        { 
            X = StartCoordinate.X,
            Y = StartCoordinate.Y,
            Distance = 0 
        });
        //----------------------------------------------------------------------------------------------------

        //�j�M----------------------------------------------------------------------------------------------------
        while (QuickSave_FindingList_ClassList.Count > 0)
        {
            //��e�j�M��
            PathFindClass QuickSave_Find_Class = QuickSave_FindingList_ClassList[0];
            QuickSave_FindingList_ClassList.RemoveAt(0);
            //���X�P�w
            if (QuickSave_Find_Class.X == TargetCoordinate.Vector2Int.x&&
                QuickSave_Find_Class.Y == TargetCoordinate.Vector2Int.y)
            {
                break;
            }

            //�|�V�����j�M
            //�U�ӶZ��
            int QuickSave_NextDistance_Int = QuickSave_Find_Class.Distance + 1;
            Vector QuickSave_NextCoordinate_Class = new Vector();
            #region - QuadrantFinding -
            for (int y = -1; y <= 1; y += 2)
            {
                for (int x = -1; x <= 1; x += 2)
                {
                    QuickSave_NextCoordinate_Class = new Vector(QuickSave_Find_Class.X + x, QuickSave_Find_Class.Y + y);

                    #region - Check -
                    //�a�������P�w
                    if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_NextCoordinate_Class,
                            _Map_GroundBoard_ScriptsArray.GetLength(0), _Map_GroundBoard_ScriptsArray.GetLength(1)))
                    {
                        continue;
                    }

                    //�O�_�w�j�d
                    if (QuickSave_DistancePlayerWalk_IntArray[QuickSave_NextCoordinate_Class.X, QuickSave_NextCoordinate_Class.Y] != 0)
                    {
                        continue;
                    }
                    #endregion

                    //�]�w�@�ɰO��
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
                    //�]�w�j�M���
                    QuickSave_FindingList_ClassList.Add(new PathFindClass
                    {
                        X = QuickSave_NextCoordinate_Class.X,
                        Y = QuickSave_NextCoordinate_Class.Y,
                        Distance = QuickSave_NextDistance_Int
                    });

                }
            }
            #endregion

            //��z����
            //�̷ӶZ���Ƨ�
            QuickSave_FindingList_ClassList.Sort((x, y) => x.Distance.CompareTo(y.Distance));
        }
        //----------------------------------------------------------------------------------------------------

        //���o���|----------------------------------------------------------------------------------------------------
        //�f�V�j�M��
        Vector QuickSave_BackFind_Class = new Vector(TargetCoordinate);
        List<Vector> QuickSave_PathCoordinate_ClassList = new List<Vector>(); ;
        List<sbyte> QuickSave_PathDirection_ByteList = new List<sbyte>();

        //�}�l�j�M
        while (QuickSave_BackFind_Class.Vector2Int != StartCoordinate.Vector2Int)
        {
            sbyte QuickSave_DirectionComeFrom = QuickSave_DirectionComeFrom_ByteArray[QuickSave_BackFind_Class.X, QuickSave_BackFind_Class.Y];
            //�P�_��B�L��
            QuickSave_PathDirection_ByteList.Add(QuickSave_DirectionComeFrom);
            QuickSave_PathCoordinate_ClassList.Add(
                new Vector(QuickSave_BackFind_Class));
            switch (QuickSave_DirectionComeFrom)
            {
                case 1:
                    //���W��k�U
                    QuickSave_BackFind_Class.x -= 1;
                    QuickSave_BackFind_Class.y -= 1; 

                    break;
                case 2:
                    //�k�W�쥪�U
                    QuickSave_BackFind_Class.x -= 1;
                    QuickSave_BackFind_Class.y += 1;
                    break;
                case 3:
                    //�k�U�쥪�W
                    QuickSave_BackFind_Class.x += 1;
                    QuickSave_BackFind_Class.y += 1;
                    break;
                case 4:
                    //���U��k�W
                    QuickSave_BackFind_Class.x += 1;
                    QuickSave_BackFind_Class.y -= 1;
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        Answer_Return_Class.Direction = QuickSave_PathDirection_ByteList;
        Answer_Return_Class.Path = QuickSave_PathCoordinate_ClassList;
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #endregion

    #region - Select -
    //�P�]�w�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public DirectionRangeClass Select
        (string SelectType,
        Vector UserCoordinate,
        Vector2Int Direction,
        Vector2Int Center,
        BoolRangeClass RangeData, BoolRangeClass SelectData)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        DirectionRangeClass QuickSave_Select_Class = new DirectionRangeClass();
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
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

        //�^��----------------------------------------------------------------------------------------------------
        //return Range_ClassToVector(UserCoordinate, QuickSave_Select_Class);
        return QuickSave_Select_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion RangeFind

    #region Object 
    //�ؼСX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Map_BattleObjectUnit> RangeTargets(        
        string Type/*�ؼ�����*/, List<SelectUnitClass> SelectRange, 
        SourceClass UserSource,
        int Time, int Order)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<_Map_BattleObjectUnit> Answer_Return_ScriptList = new List<_Map_BattleObjectUnit>();
        //���|
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
            //���׽T�{;
            Answer_Return_ScriptList.AddRange(
                _Object_Manager.TimeObjects(Type, UserSource, Time, Order, Key));            
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion Object

    #region ViewSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ViewSet

    #region CooridnateMath
    //�����ഫ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�G���y���ഫ���a������----------------------------------------------------------------------------------------------------
    public _Map_BattleGroundUnit _Math_CooridnateV2ToGroundUnit_Script(Vector InputCoordinate)
    {
        _Map_BattleGroundUnit Answer_Coordinate_Script = _Map_GroundBoard_ScriptsArray[InputCoordinate.X, InputCoordinate.Y];
        return Answer_Coordinate_Script;
    }
    //��----------------------------------------------------------------------------------------------------

    //�e��XY��Ķ�y��----------------------------------------------------------------------------------------------------
    public Vector2 _Math_CooridnateTransform_Vector2(Vector Coordinate)
    {
        Vector2 Answer_Return_Vector2 = new Vector2
            (Coordinate.x * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float),
            -Coordinate.y * 0.5f * (_Map_GroundUnitSpacing_Float + _Map_GroundUnitSize_Float) + 
            (_Map_GroundUnitHeightScale_Float * Coordinate.z));
        return Answer_Return_Vector2;
    }
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion CooridnateMath
}
