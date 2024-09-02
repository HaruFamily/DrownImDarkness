using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Skill_ExploreUnit : MonoBehaviour
{
    #region Element
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    //�s��
    public string _Basic_Key_String;
    //���
    public _Skill_Manager.ExploreDataClass _Basic_Data_Class;
    public SourceClass _Basic_Source_Class;
    //��r
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------

    //����----------------------------------------------------------------------------------------------------
    public _Skill_FactionUnit _Owner_Faction_Script;
    public _UI_Card_Unit _Owner_Card_Script;
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element

    #region KeyAction
    #region - Key_EventStatus -
    //�T�w�W�[��(��CardList�өI�s)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int Key_EventStatusValueAdd(string Type/*StatusType*/,
        SourceClass TargetSource/*�ƥ�*/, _Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        float Answer_Return_Float = 0;
        //�B�~�W�[
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
        //�ƥ��¦��
        List<string> QuickSave_Data_StringList =
            new List<string> { "EventStatusAdd", Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        switch (Type)
        {
            case "Medium":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_MediumForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Catalyst":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_CatalystForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Consciousness":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_ConsciousnessForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Vitality":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_VitalityForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Strength":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_StrengthForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Precision":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_PrecisionForce":
                    case "Explore_Tenticle_TenticleDexterity":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Speed":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_SpeedForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
            case "Luck":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_LuckForce":
                        {
                            //�W�[�ƭ�
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            Answer_Return_Float = QuickSave_Value_Float;
                        }
                        break;
                }
                break;
        }
        //�����
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�T�w�W�[��(��CardList�өI�s)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int Key_EventStatusValueMultiply(string Type/*StatusType*/,
        SourceClass TargetSource/*�ƥ�*/, _Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        float Answer_Return_Float = 0;
        //�B�~�W�[
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //��ӳQ��/�ĪG/����----------------------------------------------------------------------------------------------------
        //�ƥ��¦��
        List<string> QuickSave_Data_StringList =
            new List<string> { "EventStatusMultiply", Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        switch (Type)
        {
            case "Medium":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Catalyst":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Consciousness":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Vitality":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Strength":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Precision":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Speed":
                switch (_Basic_Key_String)
                {
                }
                break;
            case "Luck":
                switch (_Basic_Key_String)
                {
                }
                break;
        }
        //�����
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƥ󲾰�(Plot)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public string Key_Event(string Event)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        string Answer_Return_String = "";
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        switch (_Basic_Key_String)
        {
            case "Explore_Bomb_VeinExplosion":
                Answer_Return_String = Event + "_MineBlasting";
                break;
            case "Explore_Stone_Harden":
                Answer_Return_String = Event + "_Harden";
                break;
        }
        //----------------------------------------------------------------------------------------------------


        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ƥ󵲧��ӷl�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_EndEvent()
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //�]�w�Ʀr----------------------------------------------------------------------------------------------------
        //�]�w�򥻭�
        switch (_Basic_Key_String)
        {
            #region - Consume -
            case "Explore_Bomb_VeinExplosion":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    //_Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Tag -
    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_UseTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //�ۨ��B�~----------------------------------------------------------------------------------------------------
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", new List<string> { "UseTag" }, 
                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", new List<string> { "UseTag" },
                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        Answer_Return_StringList = 
            _World_Manager._Skill_Manager.TagsSet(_Basic_Data_Class.UseTag, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_OwnTag(_Map_BattleObjectUnit UsingObject)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        List<string> Answer_Return_StringList = null;
        //�B�~�W�[
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //�ۨ��B�~----------------------------------------------------------------------------------------------------
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", new List<string> { "OwnTag" },
                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", new List<string> { "OwnTag" },
                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        Answer_Return_StringList = 
            _World_Manager._Skill_Manager.TagsSet(_Basic_Data_Class.OwnTag, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Range -
    //��V��
    //Find_Directional_Block=��V��ܷj�M(�L����ê)
    //Find_Directional_All=��V����j�M(�L����ê)
    //Find_Directional_Radiation=��V�u�ʷj�M(�H��ê���C�d��)
    //����
    //Find_Overall_Distance=�Z���j�M(�L����ê)
    //Find_Overall_APlus=���|�t��j�M(�H��ê���C�d��)
    //Find_Overall_Block=�����ܷj�M(�L����ê)
    //Find_Overall_All=�������j�M(�L����ê)
    //Find_Overall_Radiation=����u�ʷj�M(�H��ê���C�d��)
    //��ܽd��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public Dictionary<string, List<Vector>> Key_Range(Vector StartCoordinate, _Map_BattleObjectUnit UsingObject)
    {
        //���|----------------------------------------------------------------------------------------------------
        Dictionary<string, List<Vector>> Answer_Return_Dictionary = new Dictionary<string, List<Vector>>();
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
        //�����̰_�l�I
        Vector QuickSave_StartCoordiante_Class;
        //----------------------------------------------------------------------------------------------------

        //�ܼƳ]�m----------------------------------------------------------------------------------------------------
        QuickSave_StartCoordiante_Class = StartCoordinate;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        _Owner_Card_Script._Range_Path_Class.Data.Clear();
        _Owner_Card_Script._Range_Select_Class.Data.Clear();
        switch (_Basic_Key_String)
        {
            #region - Move -
            case "Explore_Common_Walk":
            case "Explore_Common_Fly":
            case "Explore_Float_FloatGlide":
            case "Explore_Float_CornerGlide":
            case "Explore_Stone_StoneWalk":
            case "Explore_Stone_StoneRush":
                {
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);


                    Answer_Return_Dictionary.Add(_Basic_Key_String,
                        _Map_FieldCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data,_Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                        RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                        _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                        UserCoordinate: QuickSave_StartCoordiante_Class, TargetCoordinate: QuickSave_StartCoordiante_Class));
                }
                break;
            #endregion

            #region - Effect -
            case "Explore_Cuisine_MysteryCuisineEat":
            case "Explore_Cuisine_HerbDelightEat":
            case "Explore_Cuisine_GlosporeBeverageSplash":
            case "Explore_Cuisine_GlosporeBeverageEat":
            case "Explore_Part_RepairPartRepair":
            case "Explore_Arthropod_HiveDominate":
            case "Explore_Arthropod_SwarmEater":
            case "Explore_Arthropod_LarvaeIncubation":
            case "Explore_Maid_MaidGrace":
            case "Explore_Float_RoveDominate":
            case "Explore_Stone_StoneDominate":
            case "Explore_Medicine_HerbalSphereSmoke":
            case "Explore_Medicine_HerbalSphereEat":
            case "Explore_EternalLighthouse_SelfRebirth":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String,
                        _Map_FieldCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        null,null,
                        RangeType: "Overall", PathType: "Null", SelectType: "Block",
                        _Basic_Data_Class.Range[0], null, _Basic_Data_Class.Select[0],
                        UserCoordinate: QuickSave_StartCoordiante_Class, TargetCoordinate: QuickSave_StartCoordiante_Class));
                }
                break;
            #endregion

            #region - Other -
            default:
                print("Key_ViewOn�G" + _Basic_Key_String);
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Anime -
    //�R���ƥ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Anime()
    {
        //���|----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_MoveManager _Map_MoveManager = _Map_Manager._Map_MoveManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;

        Vector QuickSave_UseCenter_Class = _Owner_Card_Script._Card_UseCenter_Class;
        List<PathUnitClass> QuickSave_Path_ClassList = new List<PathUnitClass>();
        _Map_BattleObjectUnit QuickSave_Object_Script = null;
        Dictionary<string, PathPreviewClass> QuickSave_PathPreview_Dictionary =
            new Dictionary<string, PathPreviewClass>();
        //----------------------------------------------------------------------------------------------------


        //�e���]�w(�ͦ��ݭn���F��)----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            default:
                {
                    QuickSave_Path_ClassList = 
                        _Owner_Card_Script._Range_UseData_Class.Path;
                    if (QuickSave_Path_ClassList.Count == 0)
                    {
                        DirectionPathClass QuickSave_Path_Class = new DirectionPathClass
                        {
                            Path = new List<Vector> { QuickSave_UseCenter_Class },
                            Direction = new List<sbyte> { 0 }
                        };
                        QuickSave_Path_ClassList.Add(new PathUnitClass
                        {
                            Key = _Basic_Key_String,
                            Vector = QuickSave_UseCenter_Class,
                            Direction = 0,
                            Path = QuickSave_Path_Class
                        });
                    }
                    QuickSave_Object_Script = 
                        _Owner_Faction_Script._Basic_Source_Class.Source_Creature._Card_UsingObject_Script;
                }
                break;
        }
        _Map_Manager.FieldStateSet("AnimeMiddle", "�����ʵe����]�m�A�i��ʵe");
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Move -
            case "Explore_Common_Walk":
            case "Explore_Common_Fly":
            case "Explore_Float_FloatGlide":
            case "Explore_Float_CornerGlide":
            case "Explore_Stone_StoneWalk":
            case "Explore_Stone_StoneRush":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        _Map_MoveManager.MovePreview(_Basic_Key_String, "Normal", 0, QuickSave_Path_ClassList[0].Path,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, QuickSave_Object_Script,
                        null, true, 0, 0);
                    QuickSave_PathPreview_Dictionary.Add(_Basic_Key_String, QuickSave_PathPreview_Class);

                    _Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                        QuickSave_PathPreview_Class.FinalPath, QuickSave_PathPreview_Dictionary,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class,
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    _Map_MoveManager.MoveCoroutineCaller("Next");
                }
                break;
            #endregion

            #region - Effect -
            case "Explore_Cuisine_MysteryCuisineEat":
            case "Explore_Cuisine_HerbDelightEat":
            case "Explore_Cuisine_GlosporeBeverageSplash":
            case "Explore_Cuisine_GlosporeBeverageEat":
            case "Explore_Part_RepairPartRepair":
            case "Explore_Arthropod_HiveDominate":
            case "Explore_Arthropod_SwarmEater":
            case "Explore_Arthropod_LarvaeIncubation":
            case "Explore_Maid_MaidGrace":
            case "Explore_Float_RoveDominate":
            case "Explore_Stone_StoneDominate":
            case "Explore_Medicine_HerbalSphereSmoke":
            case "Explore_Medicine_HerbalSphereEat":
            case "Explore_EternalLighthouse_SelfRebirth":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        _Map_MoveManager.MovePreview(_Basic_Key_String, "Normal", 0, QuickSave_Path_ClassList[0].Path,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, QuickSave_Object_Script,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_PathPreview_Dictionary.Add(_Basic_Key_String, QuickSave_PathPreview_Class);

                    StartCoroutine(_Effect_Manager.EffectAnim(_Basic_Source_Class, QuickSave_PathPreview_Dictionary, true));
                }
                break;
            #endregion

            default:
                print("CodeNumber_Key�G��" + _Basic_Key_String + "��is Wrong Key");
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Attack -
    //�欰�˯����]��/�ۦ����]��欰�[���޾�//�ëDSituation�I�s�A�ӬO�z�L�QSituation�I�s���Ұ�(�pDamage)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<DamageClass> Key_Pursuit(SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject)//���]��H����
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        //�Ʀr�]�w
        switch (_Basic_Key_String)
        {

            case "Explore_Cuisine_MysteryCuisineEat":
                {
                    //���q
                    //��ƴ���
                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_PercentageValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //�ˮ`�]�m
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ChaosDamage_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, QuickSave_ValueData_Float,
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_ChaosDamage_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = _Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });

                    //���q
                    QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type1�UValue_Default_Default_Default_Default_Default_Default�U0";
                    QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    QuickSave_ValueKey02_String =
                        "PursuitTimes_StarkDamage_Type1�UValue_Default_Default_Default_Default_Default_Default�U0";
                    QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    QuickSave_AttackType_String =
                        QuickSave_ValueKey01_String.Split("_"[0])[1];

                    Answer_Return_ClassList.Add(new DamageClass
                    {
                        Source = _Basic_Source_Class,
                        DamageType = QuickSave_AttackType_String,
                        Damage = QuickSave_Value01_Float,
                        Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                    });
                }
                break;
        }
        //�ϥέ쥻�ƭ�
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Effect -
    //�R���ƥ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect(_Map_BattleObjectUnit UsingObject)
    {
        //���|----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;

        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Explore_Cuisine_MysteryCuisineEat":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼШ���ˮ`
                        _Basic_Source_Class.Source_BattleObject.
                            Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, _Basic_Source_Class,UsingObject), null,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Cuisine_HerbDelightEat":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

            case "Explore_Medicine_HerbalSphereSmoke":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float = 
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Sniff" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

            case "Explore_Cuisine_GlosporeBeverageSplash":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //�ؼ���o�ĪG
                    QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_ConcernsAboutUnsightlyDirt_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, QuickSave_ValueData_Float,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                    _World_Manager._Effect_Manager.
                        GetEffectObject(
                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, _Basic_Source_Class,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "Explore_Cuisine_GlosporeBeverageEat":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_ValueData01_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1";
                    QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_ValueData02_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData01_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_ConcernsAboutUnsightlyDirt_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData02_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, _Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Medicine_HerbalSphereEat":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_ValueData01_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1";
                    QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_ValueData02_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData01_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Medicine_MedicineResistance_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData02_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, _Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Part_RepairPartRepair":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Wood|Metal)" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

            #region - Dominate -
            case "Explore_Arthropod_HiveDominate":
                {
                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                        {
                            QuickSave_Tag_StringList =
                                Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                            //List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                                break;
                            }
                        }
                    }
                    else
                    {
                        //���Ӽƭ�
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_PercentageValueKey_String, 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Arthropod_HiveDominate_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Maid_MaidGrace":
                {
                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                        {
                            QuickSave_Tag_StringList =
                                Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                            //List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                                break;
                            }
                        }
                    }
                    else
                    {
                        //���Ӽƭ�
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default�UUser_Concept_Default_Point_ConsciousnessPoint_Total_Default�U0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                        //���ҧP�w(�ؼ�)
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        QuickSave_CheckTag_StringList = new List<string> { "Cleaning" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1";
                        }
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_PercentageValueKey_String, 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Maid_MaidGrace_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Float_RoveDominate":
                {
                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                        {
                            QuickSave_Tag_StringList =
                                Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                            //List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                                break;
                            }
                        }
                    }
                    else
                    {
                        //���Ӽƭ�
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_PercentageValueKey_String, 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Float_RoveDominate_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;

            case "Explore_Stone_StoneDominate":
                {
                    //���ҧP�w(�ؼ�)
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                        {
                            QuickSave_Tag_StringList =
                                Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                            //List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                                break;
                            }
                        }
                    }
                    else
                    {
                        //���Ӽƭ�
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default�UUser_Concept_Default_Point_ConsciousnessPoint_Total_Default�U0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_PercentageValueKey_String, 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //�ؼ���o�ĪG
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Stone_StoneDominate_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        _World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;
            #endregion


            case "Explore_Arthropod_SwarmEater":
                {
                    //�ؼЪv¡
                    string QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0�UUser_Concept_Default_Status_Consciousness_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    //�ޯ�h�ƶW�L
                    QuickSave_ValueKey_String =
                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    if (_Basic_Source_Class.Source_Creature._Basic_Object_Script.
                        Key_Stack("Key", "EffectObject_Arthropod_SwarmEggs_0", _Basic_Source_Class, _Basic_Source_Class,UsingObject) > QuickSave_Value_Float)
                    {
                        //�ؼЪv¡
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type1�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type1�UValue_Default_Default_Default_Default_Default_Default�U0";
                        QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

                //��o�ĪG
            case "Explore_Arthropod_LarvaeIncubation":
                {
                    //�ۨ���o�ĪG
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Explore_Arthropod_LarvaeIncubation�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                    _World_Manager._Effect_Manager.
                        GetEffectObject(
                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, UsingObject._Basic_Source_Class,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "Explore_EternalLighthouse_SelfRebirth":
                {
                    //���Ӽƭ�
                    string QuickSave_ValueKey_String =
                        "Consume_CatalystPoint_Default�UUser_Concept_Default_Point_CatalystPoint_Point_Default�U0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_PercentageValueKey_String, 
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    //�ؼЪv¡
                    QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, QuickSave_ValueData_Float,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_ValueKey02_String =
                        "HealTimes_MediumPoint_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Delete -
    //�R���ƥ�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Deleted()
    {
        //���|----------------------------------------------------------------------------------------------------
        string QuickSave_NewKey_String = "";
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion KeyAction
}
