using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Item_ItemUnit : MonoBehaviour
{
    #region Element
    #region - DataElement -
    //變數集——————————————————————————————————————————————————————————————————————
    //道具資料----------------------------------------------------------------------------------------------------
    //語言資料
    public Color32 _Basic_Color_Color;
    //持有者
    public _Object_CreatureUnit _Basic_Owner_Script;
    public _Map_BattleObjectUnit _Basic_Object_Script;
    //----------------------------------------------------------------------------------------------------

    //View----------------------------------------------------------------------------------------------------
    public Image _View_Image_Image;

    public _UI_HintEffect _View_Hint_Script;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Element

    #region StateSet
    #region - HoldToField -
    //——————————————————————————————————————————————————————————————————————
    public void HoldToField()
    {
        _Basic_Object_Script.gameObject.SetActive(true);
        _Basic_Object_Script.transform.localScale = Vector3.zero;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - FieldToHold -
    //——————————————————————————————————————————————————————————————————————
    public void FieldToHold()
    {
        _Basic_Object_Script.gameObject.SetActive(false);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - FieldToBattle -
    //——————————————————————————————————————————————————————————————————————
    public void FieldToBattle()
    {
        _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataAdd("Item", _Basic_Object_Script);
        _Basic_Object_Script.transform.localScale = Vector3.one;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - BattleToField -
    //——————————————————————————————————————————————————————————————————————
    public void BattleToField()
    {
        _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataRemove("Item", _Basic_Object_Script);
        _Basic_Object_Script._Map_TimePosition_Dictionary.Clear();
        _Basic_Object_Script.transform.localScale = Vector3.zero;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion StateSet
}
