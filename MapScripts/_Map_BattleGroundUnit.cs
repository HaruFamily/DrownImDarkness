using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Map_BattleGroundUnit : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    [HideInInspector] public string _Basic_Region_String;//所在區域(EX:幽影雪原)
    //[HideInInspector] public string _Basic_Field_String;//?
    public _Map_BattleObjectUnit _Basic_Object_Script;
    public PolygonCollider2D _Map_MouseSencer_Collider;

    //單位座標﹝X,Y,高度﹞
    [HideInInspector] public Vector _GroundUnit_Coordinate_Class;
    //----------------------------------------------------------------------------------------------------

    //放置區----------------------------------------------------------------------------------------------------
    //圖片渲染器放置區
    public SpriteRenderer _GroundUnit_Ground_SpriteRenderer;
    //選擇渲染器放置區
    public Transform _GroundUnit_HeightSelect_Transform;//整體高度(Select等)
    public List<SpriteRenderer> _GroundUnit_HeightSelect_SpriteRendererList;
    //文字測試機
    public TextMesh _GroundUnit_TestText_TextMesh;
    //----------------------------------------------------------------------------------------------------

    //狀態機----------------------------------------------------------------------------------------------------
    public int _Basic_SortingOrder_Int;
    //進入卡片範圍
    public bool _State_InRange_Bool;
    public bool _State_InRangePath_Bool;
    public bool _State_InRangeExtend_Bool;
    //視野範圍
    public bool _State_InView_Bool;//正在視野中
    public bool _State_BeenView_Bool;//曾在視野中

    public int _View_OwnObjectView_Int;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region Start 
    //——————————————————————————————————————————————————————————————————————
    public void SystemStart(string Region, string TestTag,Vector Coordinate,Vector3 ViewPos,int sortingOrder)
    {
        //----------------------------------------------------------------------------------------------------
        //待調整
        gameObject.name =
            "Coordinate:" + Coordinate.Vector3Int;

        _Basic_Region_String = Region;

        _GroundUnit_Coordinate_Class = Coordinate;
        transform.localPosition = ViewPos;

        _GroundUnit_HeightSelect_Transform.localPosition = new Vector3 { x = 0, y = 0, z = 0 };
        _Basic_SortingOrder_Int = sortingOrder;
        _GroundUnit_Ground_SpriteRenderer.sortingOrder = _Basic_SortingOrder_Int;
        for (int a = 0; a < _GroundUnit_HeightSelect_SpriteRendererList.Count; a++)
        {
            _GroundUnit_HeightSelect_SpriteRendererList[a].sortingOrder = _Basic_SortingOrder_Int + 3 + a;
            _GroundUnit_HeightSelect_SpriteRendererList[a].color = Color.clear;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //以下未來修改
        string QuickSave_GroundKey_String;
        if (TestTag == "Ground")
        {
            List<string> QuickSave_RandomGround_StringList =
                new List<string> 
                {
                    "Object_GloomyStrat_HardGround",
                };
            QuickSave_GroundKey_String = 
                QuickSave_RandomGround_StringList[UnityEngine.Random.Range(0, QuickSave_RandomGround_StringList.Count)];
        }
        else
        {
            QuickSave_GroundKey_String = "Object_GloomyStrat_HardWall";
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        SourceClass QuickSave_Source_Class =
            new SourceClass
            {
                SourceType = "Ground", 
                Source_BattleObject = _Basic_Object_Script
            };
        _Object_Manager.ObjectDataClass QuickSave_ObjectData_Class =
            _Object_Manager._Data_Object_Dictionary[QuickSave_GroundKey_String];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Basic_Object_Script.
            SystemStart(QuickSave_GroundKey_String, QuickSave_Source_Class, 
            QuickSave_ObjectData_Class, _Object_Manager._Language_Object_Dictionary[QuickSave_GroundKey_String]);
        ViewSet(true);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region ViewSet
    //——————————————————————————————————————————————————————————————————————
    public void ViewSet(bool IsInView)
    {
        if (_State_InView_Bool == IsInView)
        {
            return;
        }
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _State_InView_Bool = IsInView;
        if (_Basic_Object_Script._Basic_Key_String != "")
        {
            if (IsInView)
            {
                Sprite QuickSave_Sprite_Sprite =
                    _World_Manager._View_Manager.
                    GetSprite("BattleGround", "Ground", _Basic_Object_Script._Basic_Key_String);
                _GroundUnit_Ground_SpriteRenderer.sprite =
                    QuickSave_Sprite_Sprite;
                _GroundUnit_Ground_SpriteRenderer.color = Color.white;
            }
            else
            {
                _GroundUnit_Ground_SpriteRenderer.color =
                    _World_Manager._View_Manager.GetColor("Code", "Empty");
                _State_BeenView_Bool = true;
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion ViewSet

    #region Variaty
    //是否能過來——————————————————————————————————————————————————————————————————————
    public bool StayCheck(string Type, SourceClass EnterSource, 
        _Map_BattleGroundUnit LastGround/*從何而來*/,
        int Time, int Order)/*Stay/Pass*/
    {
        //----------------------------------------------------------------------------------------------------
        //範圍內目標
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
            new List<_Map_BattleObjectUnit>();
        QuickSave_Objects_ScriptsList.Add(_Basic_Object_Script);
        QuickSave_Objects_ScriptsList.AddRange(
            _World_Manager._Object_Manager.
            TimeObjects("Normal", EnterSource,
            Time, Order, _GroundUnit_Coordinate_Class));
        foreach (_Map_BattleObjectUnit OwnObject in QuickSave_Objects_ScriptsList)
        {
            switch (OwnObject.Key_ObjectCollider(Type, EnterSource, Time, Order))
            {
                case true://可以
                    break;
                case false://不可通過
                    return false;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
}
