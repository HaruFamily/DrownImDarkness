using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Skill_EnchanceUnit : MonoBehaviour
{
    #region Element
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    //�s��
    public string _Basic_Key_String;
    public string _Basic_Type_String;
    //���
    public _Skill_Manager.EnchanceDataClass _Basic_Data_Class;
    public SourceClass _Basic_Source_Class;
    //��r
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------

    //����----------------------------------------------------------------------------------------------------
    public _Skill_FactionUnit _Owner_Faction_Script;
    public _UI_Card_Unit _Owner_Card_Script;
    public _UI_Card_Unit _Owner_EnchanceTarget_Script;
    public _Effect_EffectCardUnit _Owner_EnchanceEffectCard_Script;
    //�Ȧs
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element

    #region KeyAction
    #region - Key_Delay -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int Key_DelayBefore(SourceClass BehaviorSource,_Map_BattleObjectUnit UsingObject)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //�B�~�W�[
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
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

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        Answer_Return_Float = _Basic_Data_Class.DelayBefore;
        switch (_Basic_Key_String)
        {
            case "Enchance_Spin_SpinAccelerate":
                {
                    //�W�[�ƭ�
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default�UUsing_Default_Default_Stack_Default_Default_EffectObject_Spin_Spin_0�U0";
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
        //�����
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------

    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int Key_DelayAfter(SourceClass BehaviorSource, _Map_BattleObjectUnit UsingObject)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        //�B�~�W�[
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
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

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        switch (_Basic_Key_String)
        {
            default:
                Answer_Return_Float = _Basic_Data_Class.DelayAfter;
                break;
        }
        //�����
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------

    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Delay

    #region - Key_Tag -
    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_SupplyTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
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

        //��¦�]�w----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.SupplyTag);
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_RequiredTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
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

        //��¦�]�w----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.RequiredTag);
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_AddenTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
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

        //��¦�]�w----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.AddenTag);
        //----------------------------------------------------------------------------------------------------

        //�B�~----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = 
            _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_RemoveTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
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

        //��¦�]�w----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.RemoveTag);
        //----------------------------------------------------------------------------------------------------

        //�B�~----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------


        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - View -
    public bool Key_UseLicense(_UI_Card_Unit EnchanceTarget, _Map_BattleObjectUnit UsingObject, bool Action)
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Enchance_Stone_RockCannon":
                {
                    //�ĪG�P�w
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
