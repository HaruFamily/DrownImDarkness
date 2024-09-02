using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Skill_EnchanceUnit : MonoBehaviour
{
    #region Element
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //編號
    public string _Basic_Key_String;
    public string _Basic_Type_String;
    //資料
    public _Skill_Manager.EnchanceDataClass _Basic_Data_Class;
    public SourceClass _Basic_Source_Class;
    //文字
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------

    //分類----------------------------------------------------------------------------------------------------
    public _Skill_FactionUnit _Owner_Faction_Script;
    public _UI_Card_Unit _Owner_Card_Script;
    public _UI_Card_Unit _Owner_EnchanceTarget_Script;
    public _Effect_EffectCardUnit _Owner_EnchanceEffectCard_Script;
    //暫存
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion Element

    #region KeyAction
    #region - Key_Delay -
    //——————————————————————————————————————————————————————————————————————
    public int Key_DelayBefore(SourceClass BehaviorSource,_Map_BattleObjectUnit UsingObject)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "DelayBeforeEnchance" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, BehaviorSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, BehaviorSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        Answer_Return_Float = _Basic_Data_Class.DelayBefore;
        switch (_Basic_Key_String)
        {
            case "Enchance_Spin_SpinAccelerate":
                {
                    //增加數值
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default｜Using_Default_Default_Stack_Default_Default_EffectObject_Spin_Spin_0｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, BehaviorSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, BehaviorSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    Answer_Return_Float -= QuickSave_Value_Float;
                }
                break;
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------

    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public int Key_DelayAfter(SourceClass BehaviorSource, _Map_BattleObjectUnit UsingObject)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "DelayAfterEnchance" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, BehaviorSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, BehaviorSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Key_String)
        {
            default:
                Answer_Return_Float = _Basic_Data_Class.DelayAfter;
                break;
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------

    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Delay

    #region - Key_Tag -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_SupplyTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "SupplyTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.SupplyTag);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_RequiredTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "RequiredTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.RequiredTag);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_AddenTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList = new List<string>();
        List<string> QuickSave_Remove_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "AddenTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.AddenTag);
        //----------------------------------------------------------------------------------------------------

        //額外----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = 
            _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_RemoveTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "RemoveTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.RemoveTag);
        //----------------------------------------------------------------------------------------------------

        //額外----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------


        //設定數字----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - View -
    public bool Key_UseLicense(_UI_Card_Unit EnchanceTarget, _Map_BattleObjectUnit UsingObject, bool Action)
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Enchance_Stone_RockCannon":
                {
                    //效果判定
                    float QuickSave_Stack_Float = _Basic_Source_Class.Source_BattleObject.
                        Key_Stack("Key", "EffectObject_Stone_StoneCarapace_0",
                        EnchanceTarget._Basic_Source_Class, null, UsingObject);
                    if (QuickSave_Stack_Float > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return true;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #endregion
}
