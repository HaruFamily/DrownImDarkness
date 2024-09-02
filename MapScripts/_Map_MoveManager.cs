using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class _Map_MoveManager : MonoBehaviour
{
    #region Move
    #region - MovePreview -
    //移動諭示——————————————————————————————————————————————————————————————————————

    public int QuickSave_StrengthCount_Int = 0;
    public List<_Map_BattleObjectUnit> QuickSave_PassObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
    public _Map_BattleObjectUnit QuickSave_HitTarget_Script;
    public PathPreviewClass MovePreview(string Key, string Type,int Strength/*穿透強度:0=無法穿透*/, DirectionPathClass Path, 
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        PathPreviewClass Answer_Return_Class = new PathPreviewClass();
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;

        Vector QuickSave_FinalVector_Class = Path.Path[0];
        DirectionPathClass QuickSave_FinalPath_Class = Path;
        List<string> QuickSave_ScoreList_Class = new List<string>();

        List<Vector> QuickSave_Path_ClassList = new List<Vector>(Path.Path);
        QuickSave_Path_ClassList.Reverse();
        List<sbyte> QuickSave_Direction_ClassList = new List<sbyte>(Path.Direction);
        QuickSave_Direction_ClassList.Reverse();
        string QuickSave_Map_String = 
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        //穿透變數
        QuickSave_StrengthCount_Int = Strength;
        QuickSave_PassObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
        QuickSave_HitTarget_Script = null;

        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    _Map_FieldCreator _Map_FieldCreator = _Map_Manager._Map_FieldCreator;
                    //通過檢測
                    for (int a = QuickSave_Path_ClassList.Count - 1; a > 0; a--)
                    {
                        Vector QuickSave_MoveStartCoordinate_Class = QuickSave_Path_ClassList[a];
                        Vector QuickSave_MoveEndCoordinate_Class = QuickSave_Path_ClassList[a - 1];
                        int QuickSave_MoveStartDirection_Int = QuickSave_Direction_ClassList[a - 1];

                        //超出地圖
                        if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_MoveEndCoordinate_Class,
                            _Map_FieldCreator._Map_Data_Dictionary[QuickSave_Map_String].Size.x,
                            _Map_FieldCreator._Map_Data_Dictionary[QuickSave_Map_String].Size.y))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //碰撞
                        if (!_Map_FieldCreator.
                            StayCheck("Pass", 
                            QuickSave_MoveEndCoordinate_Class, QuickSave_MoveStartCoordinate_Class, UserSource))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //設置FinalVector
                        QuickSave_FinalVector_Class = new Vector(QuickSave_MoveEndCoordinate_Class);
                    }
                    //停留檢測
                    for (int a = QuickSave_Path_ClassList.Count - 1; a > 0; a--)
                    {
                        Vector QuickSave_MoveStartCoordinate_Class = QuickSave_Path_ClassList[a];
                        Vector QuickSave_MoveEndCoordinate_Class = QuickSave_Path_ClassList[a - 1];
                        int QuickSave_MoveStartDirection_Int = QuickSave_Direction_ClassList[a - 1];

                        //碰撞
                        if (!_Map_FieldCreator.
                            StayCheck("Stay",
                            QuickSave_MoveEndCoordinate_Class, QuickSave_MoveStartCoordinate_Class, UserSource))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //設置FinalVector
                        QuickSave_FinalVector_Class = new Vector(QuickSave_MoveEndCoordinate_Class);
                    }
                    QuickSave_Path_ClassList.Reverse();
                    QuickSave_Direction_ClassList.Reverse();
                    QuickSave_FinalPath_Class = new DirectionPathClass
                    {
                        Path = new List<Vector>(QuickSave_Path_ClassList),
                        Direction = new List<sbyte>(QuickSave_Direction_ClassList)
                    };
                }
                break;
            case "Battle":
                {
                    _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
                    //通過檢測
                    for (int a = QuickSave_Path_ClassList.Count - 1; a > 0; a--)
                    {
                        Vector QuickSave_MoveStartCoordinate_Class = QuickSave_Path_ClassList[a];
                        Vector QuickSave_MoveEndCoordinate_Class = QuickSave_Path_ClassList[a - 1];
                        _Map_BattleGroundUnit QuickSave_StartGround_Script =
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                            [QuickSave_MoveStartCoordinate_Class.X, QuickSave_MoveStartCoordinate_Class.Y];
                        _Map_BattleGroundUnit QuickSave_EndGround_Script =
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                            [QuickSave_MoveEndCoordinate_Class.X, QuickSave_MoveEndCoordinate_Class.Y];
                        int QuickSave_MoveStartDirection_Int = QuickSave_Direction_ClassList[a - 1];

                        //超出地圖
                        if (!_Map_Manager._Map_MapCheck_Bool(QuickSave_MoveEndCoordinate_Class,
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0), 
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1)))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //碰撞
                        if (!QuickSave_EndGround_Script.
                            StayCheck("Pass", TargetSource, QuickSave_StartGround_Script,
                             Time, Order))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //設置FinalVector
                        QuickSave_FinalVector_Class = new Vector(QuickSave_MoveEndCoordinate_Class);
                    }
                    //停留檢測
                    for (int a = QuickSave_Path_ClassList.Count - 1; a > 0; a--)
                    {
                        Vector QuickSave_MoveStartCoordinate_Class = QuickSave_Path_ClassList[a];
                        Vector QuickSave_MoveEndCoordinate_Class = QuickSave_Path_ClassList[a - 1];
                        _Map_BattleGroundUnit QuickSave_StartGround_Script =
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                            [QuickSave_MoveStartCoordinate_Class.X, QuickSave_MoveStartCoordinate_Class.Y];
                        _Map_BattleGroundUnit QuickSave_EndGround_Script =
                            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                            [QuickSave_MoveEndCoordinate_Class.X, QuickSave_MoveEndCoordinate_Class.Y];
                        int QuickSave_MoveStartDirection_Int = QuickSave_Direction_ClassList[a - 1];

                        //碰撞
                        if (!QuickSave_EndGround_Script.
                            StayCheck("Stay", TargetSource, QuickSave_StartGround_Script,
                            Time, Order))
                        {
                            QuickSave_Path_ClassList.RemoveRange(0, a);
                            QuickSave_Direction_ClassList.RemoveRange(0, a);
                            break;
                        }
                        //設置FinalVector
                        QuickSave_FinalVector_Class = new Vector(QuickSave_MoveEndCoordinate_Class);
                    }
                    QuickSave_Path_ClassList.Reverse();
                    QuickSave_Direction_ClassList.Reverse();
                    QuickSave_FinalPath_Class = new DirectionPathClass
                    {
                        Path = new List<Vector>(QuickSave_Path_ClassList),
                        Direction = new List<sbyte>(QuickSave_Direction_ClassList)
                    };
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_Class = new PathPreviewClass
        {
            Key = Key,
            UseObject = TargetSource.Source_BattleObject,
            FinalCoor = QuickSave_FinalVector_Class,
            FinalPath = QuickSave_FinalPath_Class,
            ScoreList = QuickSave_ScoreList_Class,
            PassObjects = new List<_Map_BattleObjectUnit>(QuickSave_PassObjects_ScriptsList),
            HitObject = QuickSave_HitTarget_Script
        };
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion MovePreview

    #region - Move -    
    //移動動畫——————————————————————————————————————————————————————————————————————
    public void Spawn(Vector SpawnPoint, SourceClass TargetSource, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    //傳送至目的地----------------------------------------------------------------------------------------------------
                    TargetSource.Source_FieldObject.transform.localPosition = 
                        _World_Manager._Map_Manager._Map_FieldCreator._Math_CooridnateTransform_Vector2(SpawnPoint);
                    if (TargetSource.Source_FieldObject.transform == 
                        _UI_Manager._Camera_TraceTarget_Transform)
                    {
                        _UI_Manager.CameraTraceTargetPosSet();
                    }
                    //----------------------------------------------------------------------------------------------------

                    //結束移動----------------------------------------------------------------------------------------------------
                    //移動物更新
                    TargetSource.Source_FieldObject.MovedSet(SpawnPoint, false);
                    //結束移動
                    //----------------------------------------------------------------------------------------------------
                }
                break;
            case "Battle":
                {
                    //傳送至目的地----------------------------------------------------------------------------------------------------
                    _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
                    _Object_CreatureUnit QuickSave_Creature_Script = null;
                    switch (TargetSource.SourceType)
                    {
                        case "Concept":
                            {
                                QuickSave_Creature_Script = TargetSource.Source_Creature;
                                QuickSave_Creature_Script.transform.localPosition =
                                    _Map_BattleCreator._Math_CooridnateTransform_Vector2(SpawnPoint); 
                                if (QuickSave_Creature_Script.transform ==
                                    _UI_Manager._Camera_TraceTarget_Transform)
                                {
                                    _UI_Manager.CameraTraceTargetPosSet();
                                }
                            }
                            break;
                    }
                    //----------------------------------------------------------------------------------------------------

                    //結束移動----------------------------------------------------------------------------------------------------
                    //移動物更新
                    StartCoroutine(TargetSource.Source_BattleObject.
                        MovedSet("Spawn", null, SpawnPoint, Time, Order, false));
                    //----------------------------------------------------------------------------------------------------
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public IEnumerator Move(string MoveType, 
        Vector StartCoordinate, Vector EndCoordinate, int Direction, 
        SourceClass TargetSource, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (MoveType)
        {
            case "Normal"://普通
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            {
                                _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
                                _Map_FieldObjectUnit QuickSave_Object_Script = TargetSource.Source_FieldObject;

                                //轉向
                                switch (Direction)
                                {
                                    case 1:
                                    case 4:
                                        QuickSave_Object_Script._Map_SpriteOffset_Transform.localScale = Vector3.one;
                                        break;
                                    case 2:
                                    case 3:
                                        QuickSave_Object_Script._Map_SpriteOffset_Transform.localScale = new Vector3(-1, 1, 1);
                                        break;
                                }
                                //設定位置
                                Vector2 QuickSave_MoveStartCoordinate_Vector =
                                    _Map_FieldCreator._Math_CooridnateTransform_Vector2(StartCoordinate);
                                Vector2 QuickSave_MoveEndCoordinate_Vector =
                                    _Map_FieldCreator._Math_CooridnateTransform_Vector2(EndCoordinate);
                                //開始移動
                                for (int t = 0; t <= 10 / (_World_Manager._Config_AnimationSpeed_Float * 0.01f); t++)
                                {

                                    QuickSave_Object_Script.transform.localPosition =
                                        Vector2.Lerp(QuickSave_MoveStartCoordinate_Vector, QuickSave_MoveEndCoordinate_Vector,
                                        t * 0.1f * (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                                    yield return new WaitForSeconds(0.01f);

                                }
                                QuickSave_Object_Script.transform.localPosition = 
                                    QuickSave_MoveEndCoordinate_Vector;
                                if (QuickSave_Object_Script.transform ==
                                    _UI_Manager._Camera_TraceTarget_Transform)
                                {
                                    _UI_Manager.CameraTraceTargetPosSet();
                                }
                                QuickSave_Object_Script.MovedSet(EndCoordinate, true);
                            }
                            break;
                        case "Battle":
                            {
                                _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
                                _Map_BattleObjectUnit QuickSave_Object_Script = TargetSource.Source_BattleObject;
                                _Object_CreatureUnit QuickSave_Creature_Script = null;
                                switch (TargetSource.SourceType)
                                {
                                    case "Concept":
                                        {
                                            QuickSave_Creature_Script = TargetSource.Source_Creature;
                                        }
                                        break;
                                }
                                //設定位置
                                Vector2 QuickSave_MoveStartCoordinate_Vector =
                                    _Map_BattleCreator._Math_CooridnateTransform_Vector2(StartCoordinate);
                                Vector2 QuickSave_MoveEndCoordinate_Vector =
                                    _Map_BattleCreator._Math_CooridnateTransform_Vector2(EndCoordinate);
                                //轉向
                                if (QuickSave_Creature_Script != null)
                                {
                                    switch (Direction)
                                    {
                                        case 1:
                                        case 4:
                                            QuickSave_Creature_Script._Map_Offset_Transform.localScale = Vector3.one;
                                            break;
                                        case 2:
                                        case 3:
                                            QuickSave_Creature_Script._Map_Offset_Transform.localScale = new Vector3(-1, 1, 1);
                                            break;
                                    }
                                    //開始移動
                                    for (int t = 0; t <= 10 / (_World_Manager._Config_AnimationSpeed_Float * 0.01f); t++)
                                    {
                                        QuickSave_Creature_Script.transform.localPosition =
                                            Vector2.Lerp(QuickSave_MoveStartCoordinate_Vector, QuickSave_MoveEndCoordinate_Vector,
                                            t * 0.1f * (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                                        yield return new WaitForSeconds(0.01f);
                                    }
                                    QuickSave_Object_Script.transform.localPosition = 
                                        QuickSave_MoveEndCoordinate_Vector;
                                    if (QuickSave_Creature_Script.transform ==
                                        _UI_Manager._Camera_TraceTarget_Transform)
                                    {
                                        _UI_Manager.CameraTraceTargetPosSet();
                                    }
                                }
                                StartCoroutine(QuickSave_Object_Script.MovedSet("Normal",
                                    StartCoordinate, EndCoordinate,
                                    Time, Order, true));
                                yield return new WaitForSeconds(0.125f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                            }
                            break;
                    }
                }
                break;
            case "Instant"://瞬間
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Field":
                            {
                                _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
                                _Map_FieldObjectUnit QuickSave_Object_Script = TargetSource.Source_FieldObject;

                                //轉向
                                switch (Direction)
                                {
                                    case 1:
                                    case 4:
                                        QuickSave_Object_Script._Map_SpriteOffset_Transform.localScale = Vector3.one;
                                        break;
                                    case 2:
                                    case 3:
                                        QuickSave_Object_Script._Map_SpriteOffset_Transform.localScale = new Vector3(-1, 1, 1);
                                        break;
                                }
                                //設定位置
                                /*Vector2 QuickSave_MoveStartCoordinate_Vector =
                                    _Map_FieldCreator._Math_CooridnateTransform_Vector2(QuickSave_MoveStartCoordinate_Class);*/
                                Vector2 QuickSave_MoveEndCoordinate_Vector =
                                    _Map_FieldCreator._Math_CooridnateTransform_Vector2(EndCoordinate);
                                //開始移動
                                QuickSave_Object_Script.transform.localPosition = 
                                    QuickSave_MoveEndCoordinate_Vector;
                                if (QuickSave_Object_Script.transform ==
                                    _UI_Manager._Camera_TraceTarget_Transform)
                                {
                                    _UI_Manager.CameraTraceTargetPosSet();
                                }

                                QuickSave_Object_Script.MovedSet(EndCoordinate, true);
                                yield return new WaitForSeconds(0.125f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                            }
                            break;
                        case "Battle":
                            {
                                _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
                                _Map_BattleObjectUnit QuickSave_Object_Script = TargetSource.Source_BattleObject;
                                _Object_CreatureUnit QuickSave_Creature_Script = null;
                                switch (TargetSource.SourceType)
                                {
                                    case "Concept":
                                        {
                                            QuickSave_Creature_Script = TargetSource.Source_Creature;
                                        }
                                        break;
                                }//設定位置
                                /*Vector2 QuickSave_MoveStartCoordinate_Vector =
                                    _Map_BattleCreator._Math_CooridnateTransform_Vector2(QuickSave_MoveStartCoordinate_Class);*/
                                Vector2 QuickSave_MoveEndCoordinate_Vector =
                                    _Map_BattleCreator._Math_CooridnateTransform_Vector2(EndCoordinate);
                                if (QuickSave_Creature_Script != null)
                                {
                                    //轉向
                                    switch (Direction)
                                    {
                                        case 1:
                                        case 4:
                                            QuickSave_Creature_Script._Map_Offset_Transform.localScale = Vector3.one;
                                            break;
                                        case 2:
                                        case 3:
                                            QuickSave_Creature_Script._Map_Offset_Transform.localScale = new Vector3(-1, 1, 1);
                                            break;
                                    }
                                    //開始移動
                                    QuickSave_Creature_Script.transform.localPosition = 
                                        QuickSave_MoveEndCoordinate_Vector;
                                    if (QuickSave_Creature_Script.transform ==
                                        _UI_Manager._Camera_TraceTarget_Transform)
                                    {
                                        _UI_Manager.CameraTraceTargetPosSet();
                                    }
                                }
                                StartCoroutine(QuickSave_Object_Script.MovedSet("Instant",
                                    StartCoordinate, EndCoordinate,
                                    Time, Order, true));
                                yield return new WaitForSeconds(0.125f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Coroutine - 
    private int MoveCoroutine_MoveCount_Int = 0;
    private List<int> MoveCoroutine_PathCount_IntList = new List<int>();
    private List<string> MoveCoroutine_MoveType_StringList = new List<string>();
    private List<DirectionPathClass> MoveCoroutine_Path_ClassList = new List<DirectionPathClass>();
    private Dictionary<string, PathPreviewClass> MoveCoroutine_PathPreview_Class;
    private SourceClass MoveCoroutine_UserSource_Class;
    private List<SourceClass> MoveCoroutine_TargetSource_ClassList = new List<SourceClass>();
    private int MoveCoroutine_Time_Int;
    private int MoveCoroutine_Order_Int;
    private bool MoveCoroutine_NoCard_Bool;
    public void MoveCoroutineCaller(string ActionType,
        string MoveType = null, DirectionPathClass Path = null,
        Dictionary<string, PathPreviewClass> PathPreview = null,
        SourceClass UserSource = null, SourceClass TargetSource = null, 
        int Time = 0, int Order = 0, bool ForceEnd = false/*不呼叫Card*/)
    {
        //----------------------------------------------------------------------------------------------------
        switch (ActionType)
        {
            case "Set":
                {
                    //檔案設置
                    MoveCoroutine_PathCount_IntList.Add(0);
                    MoveCoroutine_MoveType_StringList.Add(MoveType);
                    MoveCoroutine_Path_ClassList.Add(Path);
                    MoveCoroutine_PathPreview_Class = PathPreview;
                    MoveCoroutine_UserSource_Class = UserSource;
                    MoveCoroutine_TargetSource_ClassList.Add(TargetSource);
                    MoveCoroutine_Time_Int = Time;
                    MoveCoroutine_Order_Int = Order;
                    MoveCoroutine_NoCard_Bool = ForceEnd;
                    return;//由出發點自己MoveCoroutineCaller("Next")出
                }
                break;
            case "Next":
                {
                    if (MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int] < 
                        MoveCoroutine_Path_ClassList[MoveCoroutine_MoveCount_Int].Path.Count - 1)
                    {
                        Vector QuickSave_MoveStartCoordinate_Class = 
                            MoveCoroutine_Path_ClassList[MoveCoroutine_MoveCount_Int].
                            Path[MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int]];
                        Vector QuickSave_MoveEndCoordinate_Class = 
                            MoveCoroutine_Path_ClassList[MoveCoroutine_MoveCount_Int].
                            Path[MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int] + 1];
                        int QuickSave_MoveStartDirection_Int = 
                            MoveCoroutine_Path_ClassList[MoveCoroutine_MoveCount_Int].
                            Direction[MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int] + 1];
                        MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int]++;
                        StartCoroutine(
                            Move(MoveCoroutine_MoveType_StringList[MoveCoroutine_MoveCount_Int],
                            QuickSave_MoveStartCoordinate_Class, QuickSave_MoveEndCoordinate_Class, QuickSave_MoveStartDirection_Int,
                            MoveCoroutine_TargetSource_ClassList[MoveCoroutine_MoveCount_Int],
                            MoveCoroutine_Time_Int, 
                            MoveCoroutine_Order_Int));
                    }
                    else
                    {
                        MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int]++;
                    }
                    if (MoveCoroutine_PathCount_IntList[MoveCoroutine_MoveCount_Int] == 
                        MoveCoroutine_Path_ClassList[MoveCoroutine_MoveCount_Int].Path.Count)
                    {
                        //最終結束後再進入時變為相等
                        //最末移動(其他物件皆移動完成)
                        MoveCoroutine_MoveCount_Int++;
                        if (MoveCoroutine_MoveCount_Int == MoveCoroutine_PathCount_IntList.Count)
                        {
                            //已經沒有Object與Path要走了
                            if (!MoveCoroutine_NoCard_Bool &&
                                MoveCoroutine_PathPreview_Class != null && 
                                MoveCoroutine_UserSource_Class.Source_Card != null)
                            {
                                MoveCoroutine_UserSource_Class.Source_Card.
                                    UseCardEffectEnd(MoveCoroutine_PathPreview_Class);
                            }
                            //初始化
                            MoveCoroutine_MoveCount_Int = 0;
                            MoveCoroutine_PathCount_IntList.Clear();
                            MoveCoroutine_MoveType_StringList.Clear();
                            MoveCoroutine_Path_ClassList.Clear();
                            MoveCoroutine_PathPreview_Class = null;
                            MoveCoroutine_UserSource_Class = null;
                            MoveCoroutine_TargetSource_ClassList.Clear();
                            MoveCoroutine_Time_Int = 0;
                            MoveCoroutine_Order_Int = 0;
                        }
                        else
                        {
                            //往下組Path
                            MoveCoroutineCaller("Next");
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #endregion Move
}
