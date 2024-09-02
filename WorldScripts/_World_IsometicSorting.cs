using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _World_IsometicSorting : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�򥻳]�w----------------------------------------------------------------------------------------------------
    //�y��
    public _Map_BattleObjectUnit _Map_BattleCoordinate_Script;
    public PolygonCollider2D _Map_MouseSencer_Collider;
    //�ؼ�
    public SpriteRenderer _IsometicSorting_Target_SpriteRenderer;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox

    #region MouseEvent
    #region - Click -
    void OnMouseOver ()
    {
        if (_Map_BattleCoordinate_Script != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _World_Manager._UI_Manager.ObjectLeftClick(_Map_BattleCoordinate_Script);
            }
            if (Input.GetMouseButtonUp(1))
            {
                _World_Manager._UI_Manager.ObjectRightClick(_Map_BattleCoordinate_Script);
            }
        }
    }
    #endregion
    #endregion MouseEvent

    #region Refresh
    //�s�����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SortingRefresh(int x, int y,int Adden)
    {
        //�]�w����ϼh----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    string QuickSave_Map_String =
                        _World_Manager._Authority_Map_String + "_" + _World_Manager._Authority_Weather_String;
                    _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
                    int QuickSave_SizeX_Int = 
                        _Map_FieldCreator._Map_Data_Dictionary[QuickSave_Map_String].Size.x;
                    _IsometicSorting_Target_SpriteRenderer.sortingOrder =
                        (y * QuickSave_SizeX_Int) + x + 5 + Adden;
                }
                break;
            case "Battle":
                {
                    _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
                    _IsometicSorting_Target_SpriteRenderer.sortingOrder =
                        (y * _Map_BattleCreator._Map_MapSize_Vector2.x) + x + Adden;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Refresh
}
