using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Skill_ExploreUnit : MonoBehaviour
{
    #region Element
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //編號
    public string _Basic_Key_String;
    //資料
    public _Skill_Manager.ExploreDataClass _Basic_Data_Class;
    public SourceClass _Basic_Source_Class;
    //文字
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------

    //分類----------------------------------------------------------------------------------------------------
    public _Skill_FactionUnit _Owner_Faction_Script;
    public _UI_Card_Unit _Owner_Card_Script;
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion Element

    #region KeyAction
    #region - Key_EventStatus -
    //固定增加值(由CardList來呼叫)——————————————————————————————————————————————————————————————————————
    public int Key_EventStatusValueAdd(string Type/*StatusType*/,
        SourceClass TargetSource/*事件*/, _Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //事件基礎值
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

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (Type)
        {
            case "Medium":
                switch (_Basic_Key_String)
                {
                    case "Explore_Common_MediumForce":
                        {
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            //增加數值
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //固定增加值(由CardList來呼叫)——————————————————————————————————————————————————————————————————————
    public int Key_EventStatusValueMultiply(string Type/*StatusType*/,
        SourceClass TargetSource/*事件*/, _Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //事件基礎值
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

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
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
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //事件移動(Plot)——————————————————————————————————————————————————————————————————————
    public string Key_Event(string Event)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        string Answer_Return_String = "";
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
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


        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //事件結束耗損——————————————————————————————————————————————————————————————————————
    public void Key_EndEvent()
    {
        //變數----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Key_String)
        {
            #region - Consume -
            case "Explore_Bomb_VeinExplosion":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Tag -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_UseTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //自身額外----------------------------------------------------------------------------------------------------
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

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_OwnTag(_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //自身額外----------------------------------------------------------------------------------------------------
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

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Range -
    //方向性
    //Find_Directional_Block=方向選擇搜尋(無視障礙)
    //Find_Directional_All=方向全體搜尋(無視障礙)
    //Find_Directional_Radiation=方向線性搜尋(隨障礙降低範圍)
    //整體
    //Find_Overall_Distance=距離搜尋(無視障礙)
    //Find_Overall_APlus=路徑演算搜尋(隨障礙降低範圍)
    //Find_Overall_Block=全體選擇搜尋(無視障礙)
    //Find_Overall_All=全體全體搜尋(無視障礙)
    //Find_Overall_Radiation=全體線性搜尋(隨障礙降低範圍)
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public Dictionary<string, List<Vector>> Key_Range(Vector StartCoordinate, _Map_BattleObjectUnit UsingObject)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        Dictionary<string, List<Vector>> Answer_Return_Dictionary = new Dictionary<string, List<Vector>>();
        //
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_FieldCreator _Map_FieldCreator = _World_Manager._Map_Manager._Map_FieldCreator;
        //持有者起始點
        Vector QuickSave_StartCoordiante_Class;
        //----------------------------------------------------------------------------------------------------

        //變數設置----------------------------------------------------------------------------------------------------
        QuickSave_StartCoordiante_Class = StartCoordinate;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
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
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                print("Key_ViewOn：" + _Basic_Key_String);
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Anime -
    //刪除事件——————————————————————————————————————————————————————————————————————
    public void Key_Anime()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_MoveManager _Map_MoveManager = _Map_Manager._Map_MoveManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;

        Vector QuickSave_UseCenter_Class = _Owner_Card_Script._Card_UseCenter_Class;
        List<PathUnitClass> QuickSave_Path_ClassList = new List<PathUnitClass>();
        _Map_BattleObjectUnit QuickSave_Object_Script = null;
        Dictionary<string, PathPreviewClass> QuickSave_PathPreview_Dictionary =
            new Dictionary<string, PathPreviewClass>();
        //----------------------------------------------------------------------------------------------------


        //前期設定(生成需要的東西)----------------------------------------------------------------------------------------------------
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
        _Map_Manager.FieldStateSet("AnimeMiddle", "完成動畫初期設置，進行動畫");
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
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
                print("CodeNumber_Key：﹝" + _Basic_Key_String + "﹞is Wrong Key");
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Attack -
    //行為檢索附魔用/招式附魔對行為加成引導//並非Situation呼叫，而是透過被Situation呼叫的啟動(如Damage)——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Pursuit(SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject)//附魔對象類型
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //數字設定
        switch (_Basic_Key_String)
        {

            case "Explore_Cuisine_MysteryCuisineEat":
                {
                    //首段
                    //資料提取
                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //傷害設置
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "PursuitTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //次段
                    QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "PursuitTimes_StarkDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
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
        //使用原本數值
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Effect -
    //刪除事件——————————————————————————————————————————————————————————————————————
    public void Key_Effect(_Map_BattleObjectUnit UsingObject)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;

        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Explore_Cuisine_MysteryCuisineEat":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標受到傷害
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
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

            case "Explore_Medicine_HerbalSphereSmoke":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Sniff" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

            case "Explore_Cuisine_GlosporeBeverageSplash":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //目標獲得效果
                    QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_ConcernsAboutUnsightlyDirt_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, QuickSave_ValueData_Float,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    _World_Manager._Effect_Manager.
                        GetEffectObject(
                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, _Basic_Source_Class,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "Explore_Cuisine_GlosporeBeverageEat":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
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

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_ConcernsAboutUnsightlyDirt_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData02_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
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

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Medicine_MedicineResistance_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData02_Float,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        _Basic_Source_Class.Source_BattleObject.Key_Tag(
                        _Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Wood|Metal)" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                    //標籤判定(目標)
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
                        //消耗數值
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Arthropod_HiveDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //標籤判定(目標)
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
                        //消耗數值
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                        //標籤判定(目標)
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        QuickSave_CheckTag_StringList = new List<string> { "Cleaning" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
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

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Maid_MaidGrace_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //標籤判定(目標)
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
                        //消耗數值
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Float_RoveDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //標籤判定(目標)
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
                        //消耗數值
                        string QuickSave_ValueKey_String =
                            "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        //目標獲得效果
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Stone_StoneDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

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
                    //目標治癒
                    string QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0｜User_Concept_Default_Status_Consciousness_Default_Default｜0";
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
                        "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    //技能層數超過
                    QuickSave_ValueKey_String =
                        "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        //目標治癒
                        QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            "HealTimes_MediumPoint_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                        QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        _Basic_Source_Class.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                    }
                }
                break;

                //獲得效果
            case "Explore_Arthropod_LarvaeIncubation":
                {
                    //自身獲得效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Explore_Arthropod_LarvaeIncubation｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    _World_Manager._Effect_Manager.
                        GetEffectObject(
                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, UsingObject._Basic_Source_Class,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;

            case "Explore_EternalLighthouse_SelfRebirth":
                {
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_CatalystPoint_Default｜User_Concept_Default_Point_CatalystPoint_Point_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

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
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                    //目標治癒
                    QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, false, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                        _Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Delete -
    //刪除事件——————————————————————————————————————————————————————————————————————
    public void Key_Deleted()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        string QuickSave_NewKey_String = "";
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion KeyAction
}
