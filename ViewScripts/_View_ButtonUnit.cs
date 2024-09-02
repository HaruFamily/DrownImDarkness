using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _View_ButtonUnit : MonoBehaviour
{
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    //----------------------------------------------------------------------------------------------------
    public Text _View_Text_Text;

    public string _View_BTNName_String;

    public int _View_BTNCode_Int;
    public string _View_BTNCode_String;
    public SourceClass _View_BTNCode_Class;
    //----------------------------------------------------------------------------------------------------
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void _OnLeftClickButton()
    {
        //----------------------------------------------------------------------------------------------------
        _View_TinyMenu _View_TinyMenu = _World_Manager._UI_Manager._UI_Camp_Class._View_TinyMenu;
        switch (_View_BTNName_String)
        {
            case "Sure":
                _View_TinyMenu.SureSet();
                break;
            case "Cancel":
                _View_TinyMenu.CancelSet();
                break;
            case "Remove":
                _View_TinyMenu.RemoveSet();
                break;
            case "Equip":
                _View_TinyMenu.EquipSet();
                break;
            case "RemoveEquip":
                _View_TinyMenu.RemoveEquipSet();
                break;
            case "SyndromePool_Add":
                _View_TinyMenu.TinyMenuSet("SyndromePool_Add_Select", this);
                break;
            case "SyndromePool_Remove":
                _View_TinyMenu.TinyMenuSet("SyndromePool_Remove_Select", this);
                break;
            case "Get_Inventory":
                _View_TinyMenu.TinyMenuSet("Get_Inventory_Select", _View_BTNCode_Class);
                break; 
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void _OnMouseEnter()
    {
        //----------------------------------------------------------------------------------------------------
        _View_TinyMenu _View_TinyMenu = _World_Manager._UI_Manager._UI_Camp_Class._View_TinyMenu;
        switch (_View_BTNName_String)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void _OnMouseExit()
    {
        //----------------------------------------------------------------------------------------------------
        _View_TinyMenu _View_TinyMenu = _World_Manager._UI_Manager._UI_Camp_Class._View_TinyMenu;
        switch (_View_BTNName_String)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
}
