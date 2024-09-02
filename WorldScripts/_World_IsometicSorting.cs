using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _World_IsometicSorting : MonoBehaviour
{
    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //基本設定----------------------------------------------------------------------------------------------------
    //座標
    public _Map_BattleObjectUnit _Map_BattleCoordinate_Script;
    public PolygonCollider2D _Map_MouseSencer_Collider;
    //目標
    public SpriteRenderer _IsometicSorting_Target_SpriteRenderer;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
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
    //連續執行——————————————————————————————————————————————————————————————————————
    public void SortingRefresh(int x, int y,int Adden)
    {
        //設定物件圖層----------------------------------------------------------------------------------------------------
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
    //——————————————————————————————————————————————————————————————————————
    #endregion Refresh
}
