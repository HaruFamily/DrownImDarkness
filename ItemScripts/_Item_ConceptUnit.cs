using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Item_ConceptUnit : MonoBehaviour
{
    #region Element
    #region - DataElement -
    //變數集——————————————————————————————————————————————————————————————————————
    //武器資料----------------------------------------------------------------------------------------------------
    public Color32 _Basic_Color_Color;
    //持有者
    public _Object_CreatureUnit _Basic_Owner_Script;
    public _Map_BattleObjectUnit _Basic_Object_Script;
    //----------------------------------------------------------------------------------------------------

    //概念體----------------------------------------------------------------------------------------------------
    public Dictionary<string, _Item_SyndromeUnit> _Syndrome_Syndrome_Dictionary = 
        new Dictionary<string, _Item_SyndromeUnit>();
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
        //驅動設置
        _Basic_Owner_Script._Basic_Object_Script = _Basic_Object_Script;
        _Basic_Object_Script.StateSet("Driving", _Basic_Object_Script._Basic_Source_Class,
            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //變異Store
        _Basic_Owner_Script._View_SyndromeStore_Transform = _Basic_Object_Script._View_EffectStore_Transform;

        _Basic_Object_Script.gameObject.SetActive(true);
        _Basic_Object_Script.transform.localScale = Vector3.zero;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - FieldToHold -
    //——————————————————————————————————————————————————————————————————————
    public void FieldToHold()
    {
        #region - Loot -
        _Item_Object_Inventory QuickSave_Inventory_Script =
            _Basic_Owner_Script._Object_Inventory_Script;
        List<_Map_BattleObjectUnit> QuickSave_Loots_ScriptsList =
            QuickSave_Inventory_Script._Item_Loots_ScriptsList;
        foreach (_Map_BattleObjectUnit Loot in QuickSave_Loots_ScriptsList)
        {
            {
                SourceClass _Basic_Source_Class = Loot._Basic_Source_Class;
                QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[0].Point -=
                    Loot.Key_Material("Size", _Basic_Source_Class, _Basic_Source_Class);
                QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[1].Point -=
                    Loot.Key_Material("Form", _Basic_Source_Class, _Basic_Source_Class);
                QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[2].Point -=
                    Loot.Key_Material("Weight", _Basic_Source_Class, _Basic_Source_Class);
                QuickSave_Inventory_Script._Item_EquipStatus_ClassArray[3].Point -=
                    Loot.Key_Material("Purity", _Basic_Source_Class, _Basic_Source_Class);
                QuickSave_Inventory_Script._Item_Loots_ScriptsList.Remove(Loot);
            }
            switch (Loot._Basic_Source_Class.SourceType)
            {
                case "Concept":
                    QuickSave_Inventory_Script._Item_Concepts_ScriptsList.
                        Add(Loot._Basic_Source_Class.Source_Concept);
                    break;
                case "Weapon":
                    QuickSave_Inventory_Script._Item_Weapons_ScriptsList.
                        Add(Loot._Basic_Source_Class.Source_Weapon);
                    break;
                case "Item":
                    QuickSave_Inventory_Script._Item_Items_ScriptsList.
                        Add(Loot._Basic_Source_Class.Source_Item);
                    break;
                case "Material":
                    QuickSave_Inventory_Script._Item_Materials_ScriptsList.
                        Add(Loot._Basic_Source_Class.Source_Material);
                    break;
            }
        }
        if (QuickSave_Inventory_Script._Item_Loots_ScriptsList.Count > 0)
        {
            print("Wrong");
        }
        _Basic_Object_Script.gameObject.SetActive(false);
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - FieldToBattle -
    //——————————————————————————————————————————————————————————————————————
    public void FieldToBattle()
    {
        _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataAdd("Concept", _Basic_Object_Script);
        _Basic_Object_Script.transform.localScale = Vector3.one;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - BattleToField -
    //——————————————————————————————————————————————————————————————————————
    public void BattleToField()
    {
        _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataRemove("Concept", _Basic_Object_Script);
        _Basic_Object_Script._Map_TimePosition_Dictionary.Clear();
        _Basic_Object_Script.transform.localScale = Vector3.zero;
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion StateSet

    #region KeyAction
    #region - Carry -    
    //素材搬運倍率(越高越接近原本；越低可搬運越多(蒐集效率越高))——————————————————————————————————————————————————————————————————————
    public float Key_Occupancy(string Type)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 1;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "Occupancy", Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //乘於比重
        Answer_Return_Float = 
            Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 
            0, 1);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    #endregion

    #region - Delay -    
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_DelayStandby(bool ContainTimeOffset)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "DelayStandby" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        {
            float QuickSave_Lowest_Float = _Map_BattleRound._Round_LowestSpeed_Float;
            float QuickSave_Highest_Float = _Map_BattleRound._Round_HighestSpeed_Float;
            Answer_Return_Float = _Map_BattleRound._Round_DelayMend_Float *
                Mathf.Pow((QuickSave_Highest_Float* QuickSave_Lowest_Float) / 
                (_Basic_Object_Script.Key_Status("Speed", _Basic_Source_Class, _Basic_Source_Class, null) + QuickSave_Highest_Float), 2);
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (ContainTimeOffset)
        {
            Answer_Return_Float += _Basic_Object_Script._Round_Unit_Class.DelayOffset;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.Clamp(Mathf.CeilToInt(Answer_Return_Float), 0, 65535);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Distance -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_FieldVision()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "FieldVision" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Object_Script._Basic_Key_String)
        {
            default:
                Answer_Return_Float = 5;
                break;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_BattleVision()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "BattleVision" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Object_Script._Basic_Key_String)
        {
            default:
                Answer_Return_Float = 3;
                break;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_DriveDistance()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "DriveDistance" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Object_Script._Basic_Key_String)
        {
            default:
                Answer_Return_Float = 1;
                break;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Card -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_Deal()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "Deal" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Object_Script._Basic_Key_String)
        {
            default:
                Answer_Return_Float = _Basic_Object_Script._Basic_Status_Dictionary["Vitality"] * 0.4f;
                break;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 1, 32);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_CardLimit()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "CardLimit" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //額外增加
        float QuickSave_NowEquip_Float = 0;
        float QuickSave_MaxEquip_Float = 0;
        NumbericalValueClass[] QuickSave_EquipStatus_ClassArray = 
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipStatus_ClassArray;
        for (int a = 0; a < 4; a++)
        {
            QuickSave_NowEquip_Float += QuickSave_EquipStatus_ClassArray[a].Point;
            QuickSave_MaxEquip_Float += QuickSave_EquipStatus_ClassArray[a].Total();
        }
        //設定基本值
        switch (_Basic_Object_Script._Basic_Key_String)
        {
            default:
                Answer_Return_Float = Mathf.RoundToInt((1-(QuickSave_NowEquip_Float / QuickSave_MaxEquip_Float)) * 10);
                break;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 1, 23);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Syndrome -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_StandbyTime()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "Key_StandbyTime" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        {
            Answer_Return_Float = 10;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 65535);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_StartSyndrome()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 5;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
        new List<string> { "StartSyndrome" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 1, 32);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_GetSyndrome(int Value)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = Value;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
        new List<string> { "GetSyndrome" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 1, 32);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_SyndromeTime()
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 200;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        SourceClass _Basic_Source_Class = _Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
        new List<string> { "SyndromeTime" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Object_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, _Basic_Source_Class, _Basic_Object_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 1, 65535);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion KeyAction

    #region Situation
    #region - SituationCall -
    //只有Syndrome//一般的用Object——————————————————————————————————————————————————————————————————————
    public Dictionary<string, List<string>> Situation(
        Dictionary<string, List<string>> NowAnswer,string Situation, List<string> Data, 
        SourceClass UserSource/*來源發動處*/, SourceClass TargetSource/*發動對象*/, _Map_BattleObjectUnit UsingObject/*發動物(不等於發動處(EX:Card))*/, 
        _Map_BattleObjectUnit HateTarget, bool Action,int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<string, List<string>> Answer_Return_Dictionary = NowAnswer;
        List<string> QuickSave_ReturnValue_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Operate":
                #region - Card -
                {
                    List<_UI_Card_Unit> QuickSave_Cards_ScriptsList =
                        _Basic_Owner_Script._Card_CardsDeck_ScriptList;
                    //卡片限制次數消退
                    for (int a = 0; a < QuickSave_Cards_ScriptsList.Count; a++)
                    {
                        _UI_Card_Unit QuickSave_Card_Script = QuickSave_Cards_ScriptsList[a];
                        QuickSave_Card_Script._Card_ExploreUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        QuickSave_Card_Script._Card_BehaviorUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        QuickSave_Card_Script._Card_EnchanceUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        foreach (_Skill_EnchanceUnit Special in QuickSave_Card_Script._Card_SpecialUnit_Dictionary.Values)
                        {
                            Special._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        }
                        if (bool.Parse(Data[0]))
                        {
                            QuickSave_Card_Script._Card_ExploreUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                            QuickSave_Card_Script._Card_BehaviorUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                            QuickSave_Card_Script._Card_EnchanceUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                            foreach (_Skill_EnchanceUnit Special in QuickSave_Card_Script._Card_SpecialUnit_Dictionary.Values)
                            {
                                Special._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                            }
                        }
                        for (int e = 0; e < QuickSave_Card_Script._Effect_Effect_ScriptsList.Count; e++)
                        {
                            _Effect_EffectCardUnit Effect =
                                QuickSave_Card_Script._Effect_Effect_ScriptsList[e];
                            Effect._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                            if (bool.Parse(Data[0]))
                            {
                                Effect._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                            }
                        }
                    }
                }
                #endregion

                #region - Syndrome -
                {
                    foreach (_Item_SyndromeUnit Syndrome in _Syndrome_Syndrome_Dictionary.Values)
                    {
                        Syndrome._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        if (bool.Parse(Data[0]))
                        {
                            Syndrome._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                        }
                    }
                }
                #endregion

                #region - State -
                {
                    if (_World_Manager._Authority_Scene_String == "Battle")
                    {
                        //----------------------------------------------------------------------------------------------------
                        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
                        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
                        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;

                        _Item_Object_Inventory _Object_Inventory_Script = 
                            _Basic_Owner_Script._Object_Inventory_Script;
                        Vector QuickSave_NowPos_Class = _Basic_Object_Script.
                            TimePosition(Time, Order);
                        int QuickSave_DriveDistance_Int = Key_DriveDistance();

                        HashSet<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsHashSet = 
                            new HashSet<_Map_BattleObjectUnit>();
                        List<_Map_BattleObjectUnit> QuickSave_ReachObjects_ScriptsList =
                            new List<_Map_BattleObjectUnit>();
                        List<_Map_BattleObjectUnit> QuickSave_CarryObjects_ScriptsList =
                            new List<_Map_BattleObjectUnit>();
                        //----------------------------------------------------------------------------------------------------

                        //物件對象----------------------------------------------------------------------------------------------------
                        //地板範圍
                        for (int x = -QuickSave_DriveDistance_Int; x <= QuickSave_DriveDistance_Int; x++)
                        {
                            int XCoor = QuickSave_NowPos_Class.X + x;
                            for (int y = -QuickSave_DriveDistance_Int; y <= QuickSave_DriveDistance_Int; y++)
                            {
                                int YCoor = QuickSave_NowPos_Class.Y + y;
                                Vector Quick_Coordinate_Class = new Vector(XCoor, YCoor);
                                if (!_Map_Manager._Map_MapCheck_Bool(Quick_Coordinate_Class,
                                        _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0),
                                        _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1)))
                                {
                                    continue;
                                }
                                List<_Map_BattleObjectUnit> QuickSave_GroundObject_ScriptsList =
                                    _World_Manager._Object_Manager.
                                    TimeObjects("All", _Basic_Object_Script._Basic_Source_Class,
                                        Time, Order, Quick_Coordinate_Class);
                                QuickSave_Objects_ScriptsHashSet.UnionWith(QuickSave_GroundObject_ScriptsList);
                            }
                        }
                        //驅動物
                        QuickSave_Objects_ScriptsHashSet.UnionWith(
                            _Object_Inventory_Script._Item_DrivingObject_ScriptsList);
                        //----------------------------------------------------------------------------------------------------

                        //觸及----------------------------------------------------------------------------------------------------
                        //檢測
                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsHashSet)
                        {
                            if (Object.Key_StateAction("Reach", QuickSave_DriveDistance_Int,
                                _Basic_Object_Script,
                                Time, Order))
                            {
                                QuickSave_ReachObjects_ScriptsList.Add(Object);
                            }
                        }
                        //設置
                        _Object_Inventory_Script._Item_ReachObject_ScriptsList = 
                            new List<_Map_BattleObjectUnit>(QuickSave_ReachObjects_ScriptsList);
                        //----------------------------------------------------------------------------------------------------    

                        //攜帶----------------------------------------------------------------------------------------------------
                        //檢測
                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsHashSet)
                        {
                            if (Object.Key_StateAction("Carry", QuickSave_DriveDistance_Int,
                                _Basic_Object_Script,
                                Time, Order))
                            {
                                QuickSave_CarryObjects_ScriptsList.Add(Object);
                            }
                        }
                        //設置
                        _Object_Inventory_Script._Item_CarryObject_ScriptsList =
                            new List<_Map_BattleObjectUnit>(QuickSave_CarryObjects_ScriptsList);
                        //----------------------------------------------------------------------------------------------------               
                    }
                }
                #endregion
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        #region - Card -
        /*
        switch (UserSource.SourceType)
        {
                case "Explore":
                case "Behavior":
                case "Enchance":
                    break;
                default:
                    goto CardOut;
        }*/
        {
            List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = new List<_UI_Card_Unit>();
            if (UserSource != null && UserSource.Source_Card != null)
            {
                QuickSave_Cards_ScriptsList.Add(UserSource.Source_Card);
            }
            if (TargetSource != null && TargetSource.Source_Card != null)
            {
                QuickSave_Cards_ScriptsList.Add(TargetSource.Source_Card);
            }
            if (QuickSave_Cards_ScriptsList.Count == 0)
            {
                goto CardOut;
            }
            foreach(_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
            {
                List<_Effect_EffectCardUnit> QuickSave_EffectCard_ScriptsList = new List<_Effect_EffectCardUnit>();
                QuickSave_EffectCard_ScriptsList.AddRange(Card._Effect_Effect_ScriptsList);
                QuickSave_EffectCard_ScriptsList.AddRange(Card._Effect_Enchance_ScriptsList);
                //額外
                QuickSave_EffectCard_ScriptsList.AddRange(Card._Effect_Loading_Dictionary.Keys);
                foreach (_Effect_EffectCardUnit Value in QuickSave_EffectCard_ScriptsList)
                {
                    switch (Situation)
                    {
                        #region - Scene -
                        case "OwnStart":
                            Value.Key_Effect_OwnStart();
                            break;
                        case "OwnEnd":
                            Value.Key_Effect_OwnEnd();
                            break;
                        case "FieldStart":
                            Value.Key_Effect_FieldStart();
                            break;
                        case "FieldEnd":
                            Value.Key_Effect_FieldEnd();
                            break;
                        case "BattleStart":
                            Value.Key_Effect_BattleStart();
                            break;
                        case "BattleEnd":
                            Value.Key_Effect_BattleEnd();
                            break;
                        #endregion

                        #region - Value -
                        //基本數值
                        case "GetStatusValueAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetStatusValueAdd(
                                    Data,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "GetStatusValueMultiply":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetStatusValueMultiply(
                                    Data,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;

                        //行為附魔
                        case "GetEnchanceValueAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetEnchanceValueAdd(
                                    Data[0],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "GetEnchanceValueMultiply":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetEnchanceValueMultiply(
                                    Data[0],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;

                        //效果數值
                        case "GetEffectValueAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetEffectValueAdd(
                                    Data[0], Data[1],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "GetEffectValueMultiply":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetEffectValueMultiply(
                                    Data[0], Data[1],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "LostEffectValueAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_LostEffectValueAdd(
                                    Data[0], Data[1],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "LostEffectValueMultiply":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_LostEffectValueMultiply(
                                    Data[0], Data[1],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;

                        //效果層數
                        case "GetStackValueAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetStackValueAdd(
                                        Data[0], Data[1],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            break;
                        case "GetStackValueMultiply":
                            QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_GetStackValueMultiply(
                                        Data[0], Data[1],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            break;

                        //碰撞檢測
                        case "IsColliderEnterCheck":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_IsColliderEnterCheck(
                                    Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "IsColliderEnteredCheck":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_IsColliderEnteredCheck(
                                    Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        //使用/攜帶檢測
                        case "IsReachCheck":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_IsReachCheck(
                                    int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "IsCarryCheck":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_IsCarryCheck(
                                    int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break; ;

                        //標籤
                        case "GetTagAdd":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_GetTagAdd(
                                    Data[0],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;
                        case "GetTagRemove":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_GetTagRemove(
                                    Data[0],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;

                        //Key數值變動
                        case "KeyChange":
                            {
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_KeyChange(
                                    Data[0],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        #endregion

                        #region - Action -
                        case "Drive":
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_Drive(
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;
                        case "Abandon":
                            QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_Abandon(
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;

                        case "CauseGetEffect"://使得到效果
                            QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseGetEffect(
                                Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;
                        case "CauseLostEffect"://使失去效果
                            QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseLostEffect(
                                Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;
                        case "GetEffect"://得到效果
                            QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_GetEffect(
                                Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;
                        case "LostEffect"://失去效果
                            QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_LostEffect(
                                Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;

                        case "CardsMove"://卡片移動
                            {
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_CardsMove(
                                        NowAnswer["Key"][0],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        #endregion

                        #region - Stage -
                        case "Operate":
                            Value.Key_Effect_Operate(bool.Parse(Data[0]));
                            break;

                        case "Skill":
                            Value.Key_Effect_Skill(UserSource, UsingObject);
                            break;

                        case "React":
                            Value.Key_Effect_React(UserSource);
                            break;

                        case "EventEnd":
                            {
                                Value.Key_Effect_EventEnd(UserSource);
                            }
                            break;
                        #endregion

                        #region - Card -
                        case "DealPriority":
                            {
                                //使用許可/回報Bool
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_DealPriority(
                                        bool.Parse(NowAnswer["BoolTrue"][0]),
                                        UserSource, TargetSource,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        case "AbandonResidue":
                            {
                                //使用許可/回報Bool
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_AbandonResidue(
                                        bool.Parse(NowAnswer["BoolTrue"][0]),
                                        UserSource, TargetSource,
                                        HateTarget, Action, Time, Order));
                            }
                            break;

                        case "UseLicense":
                            {
                                //使用許可/回報Bool
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_UseLicense(
                                        bool.Parse(NowAnswer["BoolFalse"][0]), Data,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        case "BehaviorMiss":
                            {
                                //攻擊落空
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_BehaviorMiss(
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;

                        case "BehaviorUseEnd":
                            {
                                QuickSave_ReturnValue_StringList.AddRange(
                                    Value.Key_Effect_BehaviorUseEnd(
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        #endregion

                        #region - Damage -
                        case "DamageValueAdd":
                            QuickSave_ReturnValue_StringList =
                                Value.Key_Effect_DamageValueAdd(
                                    Data[0], float.Parse(Data[1]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                            break;
                        case "DamageValueMultiply":
                            QuickSave_ReturnValue_StringList =
                                Value.Key_Effect_DamageValueMultiply(
                                    Data[0], float.Parse(Data[1]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                            break;

                        case "DamageBlock"://傷害格黨
                            QuickSave_ReturnValue_StringList.AddRange(
                                Value.Key_Effect_DamageBlock(
                                    Data[0], float.Parse(Data[1]), float.Parse(NowAnswer["Value"][0]),
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                            break;

                        case "Damage"://造成的傷害可為負數
                            QuickSave_ReturnValue_StringList.AddRange(Value.
                                Key_Effect_Damage(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;
                        case "Damaged"://受到的傷害可為負數
                            QuickSave_ReturnValue_StringList.AddRange(Value.
                                Key_Effect_Damaged(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                            break;

                        case "DeadResist"://許可死亡
                            {
                                QuickSave_ReturnValue_StringList.AddRange(Value.
                                    Key_Effect_DeadResist(
                                        bool.Parse(NowAnswer["BoolTrue"][0]), NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Dead"://死亡效果
                            {
                                QuickSave_ReturnValue_StringList.AddRange(Value.
                                    Key_Effect_Dead(
                                        NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                            #endregion
                    }
                }
            }
        }
        foreach (string Return in QuickSave_ReturnValue_StringList)
        {
            string QuickSave_Key_String = Return.Split("｜"[0])[0];
            string QuickSave_Value_String = Return.Replace(QuickSave_Key_String + "｜", "");
            if (Answer_Return_Dictionary.TryGetValue(QuickSave_Key_String, out List<string> DicValue))
            {
                switch (QuickSave_Key_String)
                {
                    case "Value":
                    case "Key":
                    case "Bool":
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                        }
                        break;
                    case "BoolTrue":
                        {
                            if (QuickSave_Value_String == "True")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto CardOut;
                            }
                        }
                        break;
                    case "BoolFalse":
                        {
                            if (QuickSave_Value_String == "False")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto CardOut;
                            }
                        }
                        break;
                    default:
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String].Add(QuickSave_Value_String);
                        }
                        break;
                }
            }
            else
            {
                print(Situation +":" + Return);
                foreach (string Key in Answer_Return_Dictionary.Keys)
                {
                    print(Key);
                }
                Answer_Return_Dictionary["Other"].Add(QuickSave_Value_String);
            }
        }
        QuickSave_ReturnValue_StringList.Clear();
        CardOut:
        #endregion
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        #region - Syndrome -
        foreach (_Item_SyndromeUnit Value in _Syndrome_Syndrome_Dictionary.Values)
        {
            switch (Situation)
            {
                #region - Scene -
                case "OwnStart":
                    Value.Key_Effect_OwnStart();
                    break;
                case "OwnEnd":
                    Value.Key_Effect_OwnEnd();
                    break;
                case "FieldStart":
                    Value.Key_Effect_FieldStart();
                    break;
                case "FieldEnd":
                    Value.Key_Effect_FieldEnd();
                    break;
                case "BattleStart":
                    Value.Key_Effect_BattleStart();
                    break;
                case "BattleEnd":
                    Value.Key_Effect_BattleEnd();
                    break;
                #endregion

                #region - Value -
                //基本數值
                case "GetStatusValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStatusValueAdd(
                            Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetStatusValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStatusValueMultiply(
                            Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //行為附魔
                case "GetEnchanceValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEnchanceValueAdd(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetEnchanceValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEnchanceValueMultiply(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //效果數值
                case "GetEffectValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEffectValueAdd(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetEffectValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEffectValueMultiply(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "LostEffectValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_LostEffectValueAdd(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "LostEffectValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_LostEffectValueMultiply(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //效果層數
                case "GetStackValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStackValueAdd(
                                Data[0], Data[1],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    break;
                case "GetStackValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStackValueMultiply(
                                Data[0], Data[1],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    break;

                //碰撞檢測
                case "IsColliderEnterCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsColliderEnterCheck(
                            Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "IsColliderEnteredCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsColliderEnteredCheck(
                            Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                //使用/攜帶檢測
                case "IsReachCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsReachCheck(
                            int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "IsCarryCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsCarryCheck(
                            int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //標籤
                case "GetTagAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetTagAdd(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetTagRemove":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetTagRemove(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //Key數值變動
                case "KeyChange":
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_KeyChange(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Action -
                case "Drive":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_Drive(
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "Abandon":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_Abandon(
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                case "CauseGetEffect"://使得到效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseGetEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;
                case "CauseLostEffect"://使失去效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseLostEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;
                case "GetEffect"://得到效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_GetEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;
                case "LostEffect"://失去效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_LostEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;

                case "CardsMove"://卡片移動
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_CardsMove(
                                Data[0],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Stage -
                case "Operate":
                    Value.Key_Effect_Operate(bool.Parse(Data[0]));
                    break;

                case "Skill":
                    Value.Key_Effect_Skill(UserSource, UsingObject);
                    break;

                case "React":
                    Value.Key_Effect_React(UserSource);
                    break;

                case "EventEnd":
                    {
                        Value.Key_Effect_EventEnd(UserSource);
                    }
                    break;
                #endregion

                #region - Card -
                case "DealPriority":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_DealPriority(
                                bool.Parse(NowAnswer["BoolTrue"][0]),
                                UserSource, TargetSource, 
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "AbandonResidue":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_AbandonResidue(
                                bool.Parse(NowAnswer["BoolTrue"][0]),
                                UserSource, TargetSource,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "UseLicense":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_UseLicense(
                                bool.Parse(NowAnswer["BoolFalse"][0]), Data,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "BehaviorMiss":
                    {
                        //攻擊落空
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_BehaviorMiss(
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;

                case "BehaviorUseEnd":
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_BehaviorUseEnd(
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Damage -
                case "DamageValueAdd":
                    QuickSave_ReturnValue_StringList =
                        Value.Key_Effect_DamageValueAdd(
                            Data[0], float.Parse(Data[1]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    break;
                case "DamageValueMultiply":
                    QuickSave_ReturnValue_StringList =
                        Value.Key_Effect_DamageValueMultiply(
                            Data[0], float.Parse(Data[1]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    break;

                case "DamageBlock"://傷害格黨
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_DamageBlock(
                            Data[0], float.Parse(Data[1]), float.Parse(NowAnswer["Value"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                case "Damage"://造成的傷害可為負數
                    QuickSave_ReturnValue_StringList.AddRange(Value.
                        Key_Effect_Damage(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;
                case "Damaged"://受到的傷害可為負數
                    QuickSave_ReturnValue_StringList.AddRange(Value.
                        Key_Effect_Damaged(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
                    break;

                case "DeadResist"://許可死亡
                    {
                        QuickSave_ReturnValue_StringList.AddRange(Value.
                            Key_Effect_DeadResist(
                                bool.Parse(NowAnswer["BoolTrue"][0]), NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "Dead"://死亡效果
                    {
                        QuickSave_ReturnValue_StringList.AddRange(Value.
                            Key_Effect_Dead(
                                NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                    #endregion
            }
        }
        foreach (string Return in QuickSave_ReturnValue_StringList)
        {
            string QuickSave_Key_String = Return.Split("｜"[0])[0];
            string QuickSave_Value_String = Return.Replace(QuickSave_Key_String + "｜", "");
            if (Answer_Return_Dictionary.TryGetValue(QuickSave_Key_String, out List<string> DicValue))
            {
                switch (QuickSave_Key_String)
                {
                    case "Value":
                    case "Key":
                    case "Bool":
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                        }
                        break;
                    case "BoolTrue":
                        {
                            if (QuickSave_Value_String == "True")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto SyndromeOut;
                            }
                        }
                        break;
                    case "BoolFalse":
                        {
                            if (QuickSave_Value_String == "False")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto SyndromeOut;
                            }
                        }
                        break;
                    default:
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String].Add(QuickSave_Value_String);
                        }
                        break;
                }
            }
            else
            {
                Answer_Return_Dictionary["Other"].Add(QuickSave_Value_String);
            }
        }
        QuickSave_ReturnValue_StringList.Clear();
        if (QuickSave_ReturnValue_StringList.Count == 0)
        {
            Answer_Return_Dictionary = NowAnswer;
        }
        SyndromeOut:
        #endregion
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Operate":
                #region - Card -
                {
                    List<_UI_Card_Unit> QuickSave_Cards_ScriptsList =
                        _Basic_Owner_Script._Card_CardsDeck_ScriptList;
                    //卡片限制次數消退
                    for (int a = 0; a < QuickSave_Cards_ScriptsList.Count; a++)
                    {
                        _UI_Card_Unit QuickSave_Card_Script = QuickSave_Cards_ScriptsList[a];
                        for (int e = 0; e < QuickSave_Card_Script._Effect_Effect_ScriptsList.Count; e++)
                        {
                            _Effect_EffectCardUnit Effect =
                                QuickSave_Card_Script._Effect_Effect_ScriptsList[e];
                            switch (Effect._Basic_Data_Class.Decay)
                            {
                                case "Once":
                                    Effect.StackDecrease("Decay", 65535);
                                    break;
                                case "Round":
                                    Effect.RoundDecrease(1);
                                    break;
                                case "Standby":
                                    if (bool.Parse(Data[0]))
                                    {
                                        Effect.RoundDecrease(1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Situation
}
