using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Map_BattleGroundUnit : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    [HideInInspector] public string _Basic_Region_String;//�Ҧb�ϰ�(EX:�ռv����)
    //[HideInInspector] public string _Basic_Field_String;//?
    public _Map_BattleObjectUnit _Basic_Object_Script;
    public PolygonCollider2D _Map_MouseSencer_Collider;

    //���y�С�X,Y,���ס�
    [HideInInspector] public Vector _GroundUnit_Coordinate_Class;
    //----------------------------------------------------------------------------------------------------

    //��m��----------------------------------------------------------------------------------------------------
    //�Ϥ���V����m��
    public SpriteRenderer _GroundUnit_Ground_SpriteRenderer;
    //��ܴ�V����m��
    public Transform _GroundUnit_HeightSelect_Transform;//���鰪��(Select��)
    public List<SpriteRenderer> _GroundUnit_HeightSelect_SpriteRendererList;
    //��r���վ�
    public TextMesh _GroundUnit_TestText_TextMesh;
    //----------------------------------------------------------------------------------------------------

    //���A��----------------------------------------------------------------------------------------------------
    public int _Basic_SortingOrder_Int;
    //�i�J�d���d��
    public bool _State_InRange_Bool;
    public bool _State_InRangePath_Bool;
    public bool _State_InRangeExtend_Bool;
    //�����d��
    public bool _State_InView_Bool;//���b������
    public bool _State_BeenView_Bool;//���b������

    public int _View_OwnObjectView_Int;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox

    #region Start 
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart(string Region, string TestTag,Vector Coordinate,Vector3 ViewPos,int sortingOrder)
    {
        //----------------------------------------------------------------------------------------------------
        //�ݽվ�
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
        //�H�U���ӭק�
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start

    #region ViewSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ViewSet

    #region Variaty
    //�O�_��L�ӡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public bool StayCheck(string Type, SourceClass EnterSource, 
        _Map_BattleGroundUnit LastGround/*�q��Ө�*/,
        int Time, int Order)/*Stay/Pass*/
    {
        //----------------------------------------------------------------------------------------------------
        //�d�򤺥ؼ�
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
                case true://�i�H
                    break;
                case false://���i�q�L
                    return false;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
}
