using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Map_SelectUnit : MonoBehaviour
{
    #region Element
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    public SpriteRenderer _Map_Select_SpriteRenderer;
    public PolygonCollider2D _Map_MouseSencer_Collider;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Vector _Basic_Coordinate_Class;
    private _UI_Card_Unit _Range_UsingCard_Script;
    public List<PathSelectPairClass> _Range_PathSelect_ClassList;
    //----------------------------------------------------------------------------------------------------

    //���A��----------------------------------------------------------------------------------------------------
    //�i�J�d���d��
    public bool _State_InRange_Bool;
    public bool _State_InRangePath_Bool;
    public bool _State_InRangeExtend_Bool;

    private int _State_Wheel_Int;
    private int _State_WheelSave_Int = 0;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region Start 
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart(Vector Coordinate, Vector3 ViewPos, _UI_Card_Unit Card)
    {
        //----------------------------------------------------------------------------------------------------
        //����W�ٳ]�w
        _Basic_Coordinate_Class = Coordinate;
        //�����m�]�w
        gameObject.transform.localPosition = ViewPos;
        gameObject.transform.localScale = Vector3.one;
        //�������e
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start

    #region Colliders
    //�ƤJ�P�w�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void OnMouseOver()
    {
        //�I���P�w----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //����
        if (Input.GetMouseButtonDown(0))
        {
            _UI_Manager.SelectLeftClick(this);
        }
        //�k��
        if (Input.GetMouseButtonDown(1))
        {
            _UI_Manager.SelectRightClick(this);
        }
        //����
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�i�J�d��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void OnMouseEnter()
    {
        //�]�w�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager._Mouse_PositionOnSelect_Script = this;
        //----------------------------------------------------------------------------------------------------

        //�d�򤤷ƤJ----------------------------------------------------------------------------------------------------
        if (_State_InRange_Bool)
        {
            RangeSet("Select");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //���}�d��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void OnMouseExit()
    {
        //�]�w�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager._Mouse_PositionOnSelect_Script = null;
        //----------------------------------------------------------------------------------------------------

        //�d�򤤷ƥX----------------------------------------------------------------------------------------------------
        if (_State_InRange_Bool)
        {
            _World_Manager._Map_Manager.ViewOff("Select");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Colliders


    #region ColorSet
    //��m�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ColorSet(string State)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //���|
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        //�C��
        Color QuickSave_Color_Color = Color.clear;
        //----------------------------------------------------------------------------------------------------

        //�C��]�m----------------------------------------------------------------------------------------------------
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ColorSet


    #region RangeSet
    #region - RangeOn -
    //�]�w�d����ܡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    private void RangeSet(string Type)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
        List<Vector> QuickSave_Select_ClassList = new List<Vector>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //�d��
        if (_Range_UsingCard_Script != null)
        {
            int QuickSave_Size_Int = _Range_PathSelect_ClassList.Count;
            int QuickSave_Number_Int =
                ((_State_Wheel_Int % QuickSave_Size_Int) + QuickSave_Size_Int) % QuickSave_Size_Int;

            //�]�w
            _Range_UsingCard_Script._Range_UseData_Class = _Range_PathSelect_ClassList[QuickSave_Number_Int];
            //�PCoordinate,�PDirection
            List<PathUnitClass> QuickSave_PathData_ClassList = _Range_PathSelect_ClassList[QuickSave_Number_Int].Path;
            List<SelectUnitClass> QuickSave_SelectData_ClassList = _Range_PathSelect_ClassList[QuickSave_Number_Int].Select;
            //�[�J�ܵ�ı
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
                        //��ı�]�w
                        _Range_UsingCard_Script._Basic_View_Script.SequenceReSet();
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion RangeSet
}
