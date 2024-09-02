using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Map_FieldObjectUnit : MonoBehaviour
{
    #region ElementBox
    //�ܼƶ��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��m����----------------------------------------------------------------------------------------------------
    //�Ҧb��m
    public Vector _Map_Coordinate_Class;
    //----------------------------------------------------------------------------------------------------

    //�ϼh�]�w----------------------------------------------------------------------------------------------------
    public Transform _Map_SpriteOffset_Transform;
    public List<_World_IsometicSorting> _IsometicSorting_Caller_ScriptList;
    public Transform _Map_NumberStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //�������----------------------------------------------------------------------------------------------------
    //���a
    public _Object_PlayerUnit _Player_Script;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox


    #region MovedSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void MovedSet(Vector TargetCoordinate, bool CallNext)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Map_String =
            _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
        _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
        Dictionary<Vector2Int, _Map_Manager.GroundDataClass> QuickSave_Data_Dictionary =
            _Map_FieldCreator._Map_Data_Dictionary[QuickSave_Map_String].Data;
        //----------------------------------------------------------------------------------------------------

        //�a�O�����ܧ�----------------------------------------------------------------------------------------------------
        if (QuickSave_Data_Dictionary.
            TryGetValue(_Map_Coordinate_Class.Vector2Int,out _Map_Manager.GroundDataClass PassValue))
        {
            PassValue.Objects.Remove(this);
        }
        _Map_Coordinate_Class = TargetCoordinate;
        if (QuickSave_Data_Dictionary.
            TryGetValue(TargetCoordinate.Vector2Int, out _Map_Manager.GroundDataClass NowValue))
        {
            NowValue.Objects.Add(this);
        }
        //----------------------------------------------------------------------------------------------------

        //��s�ϼh���Y----------------------------------------------------------------------------------------------------
        _Item_ConceptUnit QuickSave_Concept_Script =
            _World_Manager._Object_Manager.
            _Object_Player_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
        int QuickSave_FieldVision_Int = QuickSave_Concept_Script.Key_FieldVision();
        _Map_FieldCreator.ViewSet(TargetCoordinate, QuickSave_FieldVision_Int);
        IsometicRefresh();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (CallNext)
        {
            //�I�s�U�Ӹ��|�ؼ�
            _World_Manager._Map_Manager._Map_MoveManager.
                MoveCoroutineCaller("Next");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void IsometicRefresh()
    {
        //��s�ϼh���Y----------------------------------------------------------------------------------------------------
        Vector2Int QuickSave_PlayerCoor_Vector = _Map_Coordinate_Class.Vector2Int;
        if (_IsometicSorting_Caller_ScriptList.Count > 0)
        {
            foreach (_World_IsometicSorting SortingScripts in _IsometicSorting_Caller_ScriptList)
            {
                SortingScripts.SortingRefresh(QuickSave_PlayerCoor_Vector.x, QuickSave_PlayerCoor_Vector.y, 0);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion MovedSet
}
