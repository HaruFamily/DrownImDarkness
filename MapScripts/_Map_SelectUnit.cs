using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Map_SelectUnit : MonoBehaviour
{
    #region Element
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public SpriteRenderer _Map_Select_SpriteRenderer;
    public PolygonCollider2D _Map_MouseSencer_Collider;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Vector _Basic_Coordinate_Class;
    private _UI_Card_Unit _Range_UsingCard_Script;
    public List<PathSelectPairClass> _Range_PathSelect_ClassList;
    //----------------------------------------------------------------------------------------------------

    //狀態機----------------------------------------------------------------------------------------------------
    //進入卡片範圍
    public bool _State_InRange_Bool;
    public bool _State_InRangePath_Bool;
    public bool _State_InRangeExtend_Bool;

    private int _State_Wheel_Int;
    private int _State_WheelSave_Int = 0;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region Start 
    //——————————————————————————————————————————————————————————————————————
    public void SystemStart(Vector Coordinate, Vector3 ViewPos, _UI_Card_Unit Card)
    {
        //----------------------------------------------------------------------------------------------------
        //物件名稱設定
        _Basic_Coordinate_Class = Coordinate;
        //物件位置設定
        gameObject.transform.localPosition = ViewPos;
        gameObject.transform.localScale = Vector3.one;
        //內部內容
        if (Card != null)
        {
            _Range_UsingCard_Script = Card;
            _Range_PathSelect_ClassList =
                _World_Manager._Map_Manager.PathSelectPair(
                    Card._Range_Path_Class,
                    Card._Range_Select_Class,
                    _Basic_Coordinate_Class);
        }
        else
        {
            _Range_UsingCard_Script = null;
            _Range_PathSelect_ClassList = null;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region Colliders
    //滑入判定——————————————————————————————————————————————————————————————————————
    void OnMouseOver()
    {
        //點擊判定----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //左鍵
        if (Input.GetMouseButtonDown(0))
        {
            _UI_Manager.SelectLeftClick(this);
        }
        //右鍵
        if (Input.GetMouseButtonDown(1))
        {
            _UI_Manager.SelectRightClick(this);
        }
        //中鍵
        if (Input.GetMouseButtonDown(2))
        {
            _UI_Manager.SelectCenterClick(this);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_State_InRange_Bool || _State_InRangePath_Bool)
        {
            _State_Wheel_Int += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
            if (_State_Wheel_Int != _State_WheelSave_Int)
            {
                RangeSet("Select");
                _State_WheelSave_Int = _State_Wheel_Int;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //進入範圍——————————————————————————————————————————————————————————————————————
    void OnMouseEnter()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Mouse_PositionOnSelect_Script = this;
        //----------------------------------------------------------------------------------------------------

        //範圍中滑入----------------------------------------------------------------------------------------------------
        if (_State_InRange_Bool)
        {
            RangeSet("Select");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //離開範圍——————————————————————————————————————————————————————————————————————
    void OnMouseExit()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._Mouse_PositionOnSelect_Script = null;
        //----------------------------------------------------------------------------------------------------

        //範圍中滑出----------------------------------------------------------------------------------------------------
        if (_State_InRange_Bool)
        {
            _World_Manager._Map_Manager.ViewOff("Select");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Colliders


    #region ColorSet
    //色彩設置——————————————————————————————————————————————————————————————————————
    public void ColorSet(string State)
    {
        //變數----------------------------------------------------------------------------------------------------
        //捷徑
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        //顏色
        Color QuickSave_Color_Color = Color.clear;
        //----------------------------------------------------------------------------------------------------

        //顏色設置----------------------------------------------------------------------------------------------------
        switch (State)
        {
            #region - Range -
            case "Range":
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "Range");
                break;
            #endregion

            #region - RangePath -
            case "RangePath":
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "RangePath");
                break;
            #endregion

            #region - RangeExtend -
            case "RangeExtend":
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "RangeExtend");
                break;
            #endregion

            #region - Path -
            case "Path":
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "Path");
                break;
            #endregion

            #region - Select -
            case "Select":
                if (_World_Manager._Mouse_PositionOnSelect_Script == this)
                {
                    QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "Select");
                }
                else
                {
                    QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "SelectExtend");
                }
                break;
            case "SelectNone":
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", "SelectNone");
                break;
            #endregion

            #region - Clear -
            case "Clear":
                if (_State_InRange_Bool)
                {
                    ColorSet("Range");
                    return;
                }
                else if (_State_InRangePath_Bool)
                {
                    ColorSet("RangePath");
                    return;
                }
                else if (_State_InRangeExtend_Bool)
                {
                    ColorSet("RangeExtend");
                    return;
                }
                else
                {
                    QuickSave_Color_Color = Color.clear;
                }
                break;
            #endregion

            #region - Alpha -
            case "Alpha":
                QuickSave_Color_Color = Color.clear;
                break;
            #endregion

            default:
                QuickSave_Color_Color = _View_Manager.GetColor("GroundSelect", State);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Map_Select_SpriteRenderer.color = QuickSave_Color_Color;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion ColorSet


    #region RangeSet
    #region - RangeOn -
    //設定範圍顯示——————————————————————————————————————————————————————————————————————
    private void RangeSet(string Type)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
        List<Vector> QuickSave_Select_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //卡片
        if (_Range_UsingCard_Script != null)
        {
            int QuickSave_Size_Int = _Range_PathSelect_ClassList.Count;
            int QuickSave_Number_Int =
                ((_State_Wheel_Int % QuickSave_Size_Int) + QuickSave_Size_Int) % QuickSave_Size_Int;

            //設定
            _Range_UsingCard_Script._Range_UseData_Class = _Range_PathSelect_ClassList[QuickSave_Number_Int];
            //同Coordinate,同Direction
            List<PathUnitClass> QuickSave_PathData_ClassList = _Range_PathSelect_ClassList[QuickSave_Number_Int].Path;
            List<SelectUnitClass> QuickSave_SelectData_ClassList = _Range_PathSelect_ClassList[QuickSave_Number_Int].Select;
            //加入至視覺
            foreach (PathUnitClass PathUnit in QuickSave_PathData_ClassList)
            {
                QuickSave_Path_ClassList.AddRange(PathUnit.Path.Path);
            }
            foreach (SelectUnitClass SelectUnit in QuickSave_SelectData_ClassList)
            {
                QuickSave_Select_ClassList.AddRange(
                    _Map_Manager.Range_ClassToVector(_Basic_Coordinate_Class, SelectUnit.Select));
            }
            if (QuickSave_Select_ClassList != null)
            {
                _Map_Manager.ViewOff(Type);
                switch (Type)
                {
                    case "Select":
                        _Map_Manager.ViewOn(
                            Type, _Basic_Coordinate_Class,
                            null,
                            null,
                            QuickSave_Path_ClassList,
                            QuickSave_Select_ClassList);
                        break;
                }
            }
            switch (_World_Manager._Authority_Scene_String)
            {
                case "Battle":
                    {
                        //視覺設定
                        _Range_UsingCard_Script._Basic_View_Script.SequenceReSet();
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion RangeSet
}
