using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Skill_BehaviorUnit : MonoBehaviour
{
    #region Element
    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    //編號
    public string _Basic_Key_String;
    //資料
    public _Skill_Manager.BehaviorDataClass _Basic_Data_Class;
    public SourceClass _Basic_Source_Class;
    //文字
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------

    //分類----------------------------------------------------------------------------------------------------
    public _Skill_FactionUnit _Owner_Faction_Script;
    public _UI_Card_Unit _Owner_Card_Script;
    //暫存
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion Element

    #region KeyAction
    #region - Key_Delay -
    //——————————————————————————————————————————————————————————————————————
    public int Key_DelayBefore(SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        bool ContainEnchance, bool ContainTimeOffset)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "DelayBefore" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        Answer_Return_Float = _Basic_Data_Class.DelayBefore;
        switch (_Basic_Key_String)
        {
            case "Behavior_Blade_SideCutting":
            case "Behavior_Blade_BattoSan":
                {
                    //觸發時
                    if (!_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        break;
                    }
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    Answer_Return_Float -= QuickSave_Value_Float;
                }
                break;
            case "Behavior_Stone_RollingStone":
                {
                    //觸發時
                    if (!_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        break;
                    }
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    Answer_Return_Float -= QuickSave_Value_Float;
                }
                break;
            case "Behavior_Stone_BreakBurst":
                {
                    //層數紀錄
                    float QuickSave_Stack_Float = UsingObject.
                        Key_Stack("Key", "EffectObject_Stone_StoneCarapace_0", _Basic_Source_Class, TargetSource, UsingObject);
                    //層數限制
                    if (QuickSave_Stack_Float == 0)
                    {
                        break;
                    }
                    //數值設定
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    //資料提取
                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_PercentageValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    int QuickSave_Value_Int = Mathf.Clamp(
                        Mathf.RoundToInt(QuickSave_Stack_Float),0,Mathf.RoundToInt(QuickSave_Value_Float * QuickSave_PercentageValue_Float));
                    Answer_Return_Float -= QuickSave_Value_Int;
                }
                break;
        }
        //使用變動
        switch (UsingObject._Basic_Source_Class.SourceType)
        {
            case "Weapon":
            case "Item":
            case "Object":
                Answer_Return_Float +=
                    UsingObject.Key_Status("DelayBeforeWeight", _Basic_Source_Class, TargetSource, UsingObject);
                break;
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //附魔加算----------------------------------------------------------------------------------------------------
        if (ContainEnchance)
        {
            List<_UI_Card_Unit> QuickSave_Enchance_ScriptsList =
                _Owner_Card_Script._State_EnchanceStore_ScriptsList;
            string QuickSave_EnchanceType_String = Key_Enchance();

            foreach  (_UI_Card_Unit Enchance in QuickSave_Enchance_ScriptsList)
            {
                _Skill_EnchanceUnit QuickSave_Enchance_Script =
                    Enchance._Card_EnchanceUnit_Script;
                if (Enchance._Card_SpecialUnit_Dictionary.
                    TryGetValue(QuickSave_EnchanceType_String, out _Skill_EnchanceUnit DicValue))
                {
                    QuickSave_Enchance_Script = DicValue;
                }
                //計算數值總和
                Answer_Return_Float += QuickSave_Enchance_Script.
                    Key_DelayBefore(_Basic_Source_Class, UsingObject);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (ContainTimeOffset)
        {
            Answer_Return_Float += _Owner_Card_Script._Round_Unit_Class.DelayOffset;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.Clamp(Mathf.CeilToInt(Answer_Return_Float), 0, 65535);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public int Key_DelayAfter(SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        bool ContainEnchance, bool ContainTimeOffset)
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
            new List<string> { "DelayAfter" };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        Answer_Return_Float = _Basic_Data_Class.DelayAfter;
        switch (_Basic_Key_String)
        {
        }
        //使用變動
        switch (UsingObject._Basic_Source_Class.SourceType)
        {
            case "Weapon":
            case "Item":
            case "Object":
                Answer_Return_Float +=
                    UsingObject.Key_Status("DelayAfterWeight", _Basic_Source_Class, TargetSource, UsingObject);
                break;
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //附魔加算----------------------------------------------------------------------------------------------------
        if (ContainEnchance)
        {
            List<_UI_Card_Unit> QuickSave_Enchance_ScriptsList =
                _Owner_Card_Script._State_EnchanceStore_ScriptsList;
            string QuickSave_EnchanceType_String = Key_Enchance();

            foreach (_UI_Card_Unit Enchance in QuickSave_Enchance_ScriptsList)
            {
                _Skill_EnchanceUnit QuickSave_Enchance_Script =
                    Enchance._Card_EnchanceUnit_Script;
                if (Enchance._Card_SpecialUnit_Dictionary.
                    TryGetValue(QuickSave_EnchanceType_String, out _Skill_EnchanceUnit DicValue))
                {
                    QuickSave_Enchance_Script = DicValue;
                }
                //計算數值總和
                Answer_Return_Float += QuickSave_Enchance_Script.
                    Key_DelayAfter(_Basic_Source_Class, UsingObject);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (ContainTimeOffset)
        {
            Answer_Return_Float += _Owner_Card_Script._Round_Unit_Class.DelayOffset;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.Clamp(Mathf.CeilToInt(Answer_Return_Float), 0, 65535);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Delay

    #region - Key_Tag -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_UseTag(SourceClass TargetSource, _Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        //----------------------------------------------------------------------------------------------------

        //自身額外----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "UseTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.UseTag);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = 
            _World_Manager._Skill_Manager.
            TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_OwnTag(SourceClass TargetSource, _Map_BattleObjectUnit UsingObject, bool ContainEnchance)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = new List<string>(_Basic_Data_Class.OwnTag);
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //額外增加
        List<string> QuickSave_Add_StringList = new List<string>();
        List<string> QuickSave_Remove_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //自身額外----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "OwnTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        foreach (_UI_Card_Unit Card in _Owner_Card_Script._State_EnchanceStore_ScriptsList)
        {
            QuickSave_Add_StringList.AddRange(Card._Card_EnchanceUnit_Script.
                Key_AddenTag(UsingObject));
            QuickSave_Remove_StringList.AddRange(Card._Card_EnchanceUnit_Script.
                Key_RemoveTag(UsingObject));
        }
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.OwnTag);
        switch (_Basic_Key_String)
        {
            case "Behavior_Stone_RollingStone":
                {
                    //觸發時
                    if (!_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        break;
                    }
                    Answer_Return_StringList.Add("Burst");
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList =
            _World_Manager._Skill_Manager.
            TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_ReactTag(SourceClass TargetSource,_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList;
        List<string> QuickSave_Remove_StringList;
        _Object_CreatureUnit QuickSave_Owner_Script = _Owner_Card_Script._Basic_Source_Class.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //自身額外----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList =
            new List<string> { "ReactTag" };
        QuickSave_Add_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Source_Class.Source_BattleObject.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                _Basic_Source_Class, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //基礎設定----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.ReactTag);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList =
            _World_Manager._Skill_Manager.
            TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region - Key_Range -
    #region - View -
    public bool Key_UseLicense(_Map_BattleObjectUnit UsingObject, bool Action)
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Behavior_Stone_StoneCarapaceSublimation":
                {
                    //效果判定
                    float QuickSave_Stack_Float = _Basic_Source_Class.Source_BattleObject.
                        Key_Stack("Key", "EffectObject_Stone_StoneCarapace_0",
                        _Basic_Source_Class, null, UsingObject);
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

    //——————————————————————————————————————————————————————————————————————
    public string Key_Enchance()//附魔與否(裝填等…)
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Behavior_Throw_GentleToss_0":
            case "Behavior_Throw_ProjectilePop_0":
            case "Behavior_Throw_ProjectileFire_0":
            case "Behavior_Bow_ArrowShooting_0":
            case "Behavior_Shooter_StraightBlow_0":
            case "Behavior_Shooter_RapidBlow_0":
            case "Behavior_Shooter_ChargeBlow_0":
            case "Behavior_Chef_CuisineEntertain_0":
            case "Behavior_Stone_RockFort_0":
                return "Loading";
            default:
                return "Enchance";
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Key_UseReplaceUnit()
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Replace_String = "";
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Behavior_Throw_GentleToss_0":
            case "Behavior_Throw_ProjectilePop_0":
            case "Behavior_Throw_ProjectileFire_0":
            case "Behavior_Bow_ArrowShooting_0":
            case "Behavior_Shooter_StraightBlow_0":
            case "Behavior_Shooter_RapidBlow_0":
            case "Behavior_Shooter_ChargeBlow_0":
            case "Behavior_Chef_CuisineEntertain_0":
            case "Behavior_Stone_RockFort_0":
                {
                    if (_Owner_Card_Script._Effect_Loading_Dictionary.Count > 0)
                    {
                        QuickSave_Replace_String = _Basic_Key_String.Replace("0", "1");
                    }
                }
                break;
            case "Behavior_Throw_GentleToss_1":
            case "Behavior_Throw_ProjectilePop_1":
            case "Behavior_Throw_ProjectileFire_1":
            case "Behavior_Bow_ArrowShooting_1":
            case "Behavior_Shooter_StraightBlow_1":
            case "Behavior_Shooter_RapidBlow_1":
            case "Behavior_Shooter_ChargeBlow_1":
            case "Behavior_Chef_CuisineEntertain_1":
            case "Behavior_Stone_RockFort_1":
                {
                    QuickSave_Replace_String = _Basic_Key_String.Replace("1", "0");
                    //清空裝填
                    _Owner_Card_Script._Effect_Loading_Dictionary.Clear();
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //替換----------------------------------------------------------------------------------------------------
        if (QuickSave_Replace_String != "")
        {
            _Owner_Card_Script.ChangeUnit("Behavior", QuickSave_Replace_String);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //顯示範圍——————————————————————————————————————————————————————————————————————
    public Dictionary<string, List<Vector>> Key_Range(Vector StartCoordinate, _Map_BattleObjectUnit UsingObject,int Time, int Order)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        Dictionary<string, List<Vector>> Answer_Return_Dictionary = new Dictionary<string, List<Vector>>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //初始化
        _Owner_Card_Script._Range_Path_Class.Data.Clear();
        _Owner_Card_Script._Range_Select_Class.Data.Clear();
        //範圍(可選取)/路徑(行動路徑，受阻擋則停止)/範圍(抵達觸發目標後的範圍)
        //RangeType：
        //Directional：方向性同設定
        //Overall：全向性同設定
        //PathType：
        //Null：無路徑(瞬間出現)
        //AStar：路徑搜尋格數(起點開始搜尋)
        //Point：點對點(可能會斜線跳動)(起點至終點)
        //SelectType：
        //Block：同設定
        //All：等同於Range的範圍

        switch (_Basic_Key_String)
        {
            #region - Move -
            //基本
            case "Behavior_Common_AwkwardSprint":
            case "Behavior_Arthropod_StruggleCrawling":
            case "Behavior_Arthropod_ArthropodDash":
            case "Behavior_Maid_GraceStep":
            case "Behavior_Frank_FrankStep":
            case "Behavior_Survival_SurvivalIntuition":
            case "Behavior_Float_CurrentCircumvention":
            case "Behavior_Stone_StoneStep":
            case "Behavior_Stone_BreakBurst":
            case "Behavior_Bush_BushSneak":
            case "Behavior_Fluorescent_GlowCrawls":
            case "Behavior_ScarletKin_ScarletDash":
            case "Behavior_ScarletKin_ScarletSideWalk":
            case "Behavior_ScarletKin_ScarletPace":
            case "Behavior_ScarletKin_ScarletStride":
            //移動攻擊
            case "Behavior_Frank_FrankSwoop":
            case "Behavior_Brutal_BrutalDash":
            case "Behavior_Stone_RollingStone":
            case "Behavior_ScarletKin_ScarletBruteCharge":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                        RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                        _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;
            //傳送
            case "Behavior_ScarletKin_ScarletTransmit":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                        RangeType: "Directional", PathType: "Instant", SelectType: "Block",
                        _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;
            #region - Tag -
            //移動者有Tag(Concept)
            case "Behavior_Arthropod_HiveMindGuild":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Concept" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        QuickSave_ValueKey02_String =
                            "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                    }
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                        RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                        _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;
            //目標點有Tag(Fluorescent)
            case "Behavior_Arthropod_PhototropismFlying":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);


                    string QuickSave_ValueKey_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    int QuickSave_Value_Int = Mathf.RoundToInt(QuickSave_Value_Float);
                    List<int> QuickSave_NormalRange = new List<int> { 1, 2, 3, 4 };
                    List<int> QuickSave_ExtendRange = new List<int>();

                    _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
                    for (int x = -1; x <= 1; x += 2)
                    {
                        for (int y = -1; y <= 1; y += 2)
                        {
                            Vector QuickSave_Coordinate_Class = new Vector 
                            { 
                                x = StartCoordinate.X + (x * QuickSave_Value_Int) ,
                                y = StartCoordinate.Y + (y * QuickSave_Value_Int)
                            };
                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScripsList =
                                _Object_Manager.
                                TimeObjects("All", null,
                                Time, Order, QuickSave_Coordinate_Class);
                            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScripsList)
                            {
                                //標籤判定(目標)
                                List<string> QuickSave_Tag_StringList =
                                    Object.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Fluorescent" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    if (y == 1)
                                    {
                                        if (x == 1)
                                        {
                                            QuickSave_NormalRange.Remove(1);
                                            QuickSave_ExtendRange.Add(1);
                                        }
                                        else
                                        {
                                            QuickSave_NormalRange.Remove(2);
                                            QuickSave_ExtendRange.Add(2);
                                        }
                                    }
                                    else
                                    {
                                        if (x == -1)
                                        {
                                            QuickSave_NormalRange.Remove(3);
                                            QuickSave_ExtendRange.Add(3);
                                        }
                                        else
                                        {
                                            QuickSave_NormalRange.Remove(4);
                                            QuickSave_ExtendRange.Add(4);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (QuickSave_NormalRange.Count > 0)
                    {
                        Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                            _Map_BattleCreator.Find_Divert
                            (_Basic_Key_String,
                            _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            QuickSave_NormalRange, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                            RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                            _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                    if (QuickSave_ExtendRange.Count > 0)
                    {
                        Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                            _Map_BattleCreator.Find_Divert
                            (_Basic_Key_String,
                            _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            QuickSave_ExtendRange, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value_Float },
                            RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                            _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                }
                break;
            #endregion

            #region - Project -
            //造物 投射物
            case "Behavior_Blade_SwordWave":
            case "Behavior_Sharp_PunctureWave":
            case "Behavior_Fangs_FlightFangs":
            case "Behavior_Arthropod_PhototropismSacrificeShooting":
            case "Behavior_Stone_WhirlingStone":
            case "Behavior_Fluorescent_FluorescentMark":
            case "Behavior_Phantom_PhantomBug":
            case "Behavior_FlashingTachi_NewSun":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                    _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                        RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                        _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;
            #endregion

            #region - Special -
            //依照洋流方向改變距離
            case "Behavior_Float_CurrentDrift":
            case "Behavior_Float_CornerSurge":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    List<int> QuickSave_NormalRange = new List<int> { 1, 2, 3, 4 };

                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                        _World_Manager._Object_Manager.
                        TimeObjects("All", _Basic_Source_Class, 
                        Time, Order, StartCoordinate);
                    foreach(_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                    {
                        if (Object._Basic_Key_String == "Object_OceanCurrent_Normal")
                        {
                            string QuickSave_ValueKey03_String =
                                "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                            string QuickSave_Key03_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey03_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, true, Time, Order);
                            float QuickSave_Value03_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key03_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey03_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, true, Time, Order);

                            string QuickSave_ValueKey04_String =
                                "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜2";
                            string QuickSave_Key04_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey04_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, true, Time, Order);
                            float QuickSave_Value04_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key04_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey04_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                null, true, Time, Order);

                            List<int> QuickSave_ExtendRange = new List<int>();
                            List<int> QuickSave_ShortenRange = new List<int>();

                            switch (Object._Basic_SubNameSave_String)
                            {
                                case "RightBottom":
                                    QuickSave_ExtendRange.Add(1);
                                    QuickSave_NormalRange.Remove(1);
                                    QuickSave_ShortenRange.Add(3);
                                    QuickSave_NormalRange.Remove(3);
                                    break;
                                case "LeftBottom":
                                    QuickSave_ExtendRange.Add(2);
                                    QuickSave_NormalRange.Remove(2);
                                    QuickSave_ShortenRange.Add(4);
                                    QuickSave_NormalRange.Remove(4);
                                    break;
                                case "LeftTop":
                                    QuickSave_ExtendRange.Add(3);
                                    QuickSave_NormalRange.Remove(3);
                                    QuickSave_ShortenRange.Add(1);
                                    QuickSave_NormalRange.Remove(1);
                                    break;
                                case "RightTop":
                                    QuickSave_ExtendRange.Add(4);
                                    QuickSave_NormalRange.Remove(4);
                                    QuickSave_ShortenRange.Add(2);
                                    QuickSave_NormalRange.Remove(2);
                                    break;
                            }

                            for (int a = 0; a < _Basic_Data_Class.Path.Count; a++)
                            {
                                Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                                    _Map_BattleCreator.Find_Divert
                                    (_Basic_Key_String,
                                    _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                                    QuickSave_ExtendRange, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value03_Float },
                                    RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                                    _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[a], _Basic_Data_Class.Select[0],
                                    UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));

                                Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                                    _Map_BattleCreator.Find_Divert
                                    (_Basic_Key_String,
                                    _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                                    QuickSave_ShortenRange, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value04_Float },
                                    RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                                    _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[a], _Basic_Data_Class.Select[0],
                                    UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                            }
                        }
                    }
                    for (int a = 0; a < _Basic_Data_Class.Path.Count; a++)
                    {
                        Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                            _Map_BattleCreator.Find_Divert
                            (_Basic_Key_String,
                            _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            QuickSave_NormalRange, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                            RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                            _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[a], _Basic_Data_Class.Select[0],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                }
                break;
            #endregion
            #region - 錨點移動
            /*
        case "Behavior_Whiplike02_0":
            {
                //以目標為茅點移動
                string QuickSave_PathMinKey_String = "PathMin_Value_Value_Value_0";
                float QuickSave_PathMinValue_Float =
                    _World_Manager.Key_NumbersUnit("Default", QuickSave_PathMinKey_String,
                    _Basic_Data_Class.Numbers[QuickSave_PathMinKey_String], _Basic_Source_Class);
                string QuickSave_PathMaxKey_String = "PathMax_Value_Value_Value_0";
                float QuickSave_PathMaxValue_Float =
                    _World_Manager.Key_NumbersUnit("Default", QuickSave_PathMaxKey_String,
                    _Basic_Data_Class.Numbers[QuickSave_PathMaxKey_String], _Basic_Source_Class);

                Answer_Return_ClassList.AddRange(
                    _Map_BattleCreator.Find_Divert
                    (_Owner_Card_Script,
                    new List<int> { 1, 2, 3, 4 }, 
                    new Tuple { Min = QuickSave_PathMinValue_Float, Max = QuickSave_PathMaxValue_Float },
                    RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                    _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], _Basic_Data_Class.Select[0],
                    UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
            }
            break;
        #endregion*/
            #endregion
            #endregion

            #region - Attack -
            //方向範圍-區選擇
            case "Behavior_Ink_InkVolley":
            case "Behavior_Cleaning_EnvironmentClean":
            case "Behavior_ScarletKin_ScarletHeal":
            case "Behavior_投擲":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, null,
                        RangeType: "Directional", PathType: "Null", SelectType: "Block",
                        _Basic_Data_Class.Range[0], null, _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;

            //方向範圍-全選擇
            case "Behavior_Blade_SideCutting":
            case "Behavior_Blade_BattoSan":
            case "Behavior_Blade_UpwardSlash":
            case "Behavior_Blade_DownwardSlash":
            case "Behavior_Blade_CrossSlash":
            case "Behavior_Blade_CrossRising":
            case "Behavior_Blade_UpRising":
            case "Behavior_Sharp_SharpPuncture":
            case "Behavior_Sharp_BackhandThrust":
            case "Behavior_Flail_StarShake":
            case "Behavior_Flail_MeteorSpin":
            case "Behavior_Flail_CometStrike":
            case "Behavior_Scabbard_FlawStrike":
            case "Behavior_Fangs_ProbeBite":
            case "Behavior_Ink_InkBurst":
            case "Behavior_Tenticle_TenticleFlog":
            case "Behavior_Tenticle_TenticleFlagellate":
            case "Behavior_Tenticle_TenticleCoerce":
            case "Behavior_Arthropod_SwarmSurge":
            case "Behavior_Miner_StoneBreaker":
            case "Behavior_Miner_AccurateMine":
            case "Behavior_Guard_ProbeStrike":
            case "Behavior_Guard_FlawStrike":
            case "Behavior_Doll_ContentsStrike":
            case "Behavior_Doll_ContentsFall":
            case "Behavior_Guard_DelayingBind":
            case "Behavior_Frank_ProbeTouch":
            case "Behavior_Fighting_ProbeTap":
            case "Behavior_Fighting_SneakTap":
            case "Behavior_Fighting_SuccessiveTap":
            case "Behavior_Fighting_DestructionPunch":
            case "Behavior_Stone_BoulderOppression":
            case "Behavior_Stone_ShatterstoneVolley":
            case "Behavior_Stone_StalagmiteArrow":
            case "Behavior_Stone_RockSpring":
            case "Behavior_Vine_SweepingVine":
            case "Behavior_Vine_VineTwist":
            case "Behavior_Rope_ReverseFlog":
            case "Behavior_Rope_StiffWhip":
            case "Behavior_Rope_FearWhip":
            case "Behavior_Light_LightFlash":
            case "Behavior_LanspidDagger_TransparentBlade":
            case "Behavior_FlashingTachi_Twilight":
            case "Behavior_GatherCobble_CometRain":
            case "Behavior_OrganicGauntlets_DevourFinger":
            case "Behavior_Limu_LimuCatch":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        new List<int> { 1, 2, 3, 4 }, null,
                        RangeType: "Directional", PathType: "Null", SelectType: "All",
                        _Basic_Data_Class.Range[0], null, null,
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;

            //支配
            case "Behavior_Arthropod_HiveDominate":
            case "Behavior_Maid_MaidGrace":
            case "Behavior_Float_RoveDominate":
            case "Behavior_Stone_StoneDominate":
            case "Behavior_FlashingTachi_Sunrise":
            //全向範圍-區選擇
            case "Behavior_Object_DoublePrepare":
            case "Behavior_Flail_Astrology":
            case "Behavior_Cuisine_FailCuisine":
            case "Behavior_Cuisine_InvigoratingAppetizer":
            case "Behavior_Cuisine_HealingMainCourse":
            case "Behavior_Cuisine_EnergeticDessert":
            case "Behavior_Cuisine_MysteryCuisineEat":
            case "Behavior_Cuisine_HerbDelightEat":
            case "Behavior_Cuisine_GlosporeBeverageEat":
            case "Behavior_Part_RepairPartRepair":
            case "Behavior_Ink_InkSwirl":
            case "Behavior_Arthropod_LarvaeIncubation":
            case "Behavior_Profession_PlanSet":
            case "Behavior_Chef_CuisineRemaking":
            case "Behavior_Tailor_FabricRepair":
            case "Behavior_Fighting_FifthHighParry":
            case "Behavior_Stone_StoneCarapaceSublimation":
            case "Behavior_Stone_DelaySlate":
            case "Behavior_Vine_RattanCocoon":
            case "Behavior_Medicine_HealingPotion":
            case "Behavior_Medicine_HerbalSphereSmoke":
            case "Behavior_Medicine_HerbalSphereEat":
            case "Behavior_Symbiotic_SymbioticOffering":
            case "Behavior_Malogic_MalogicRewrite":
            case "Behavior_ScarletKin_ScarletPurify":
            case "Behavior_ScarletKin_ScarletPerception":
            case "Behavior_EternalLighthouse_SelfRebirth":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        null, null,
                        RangeType: "Overall", PathType: "Null", SelectType: "Block",
                        _Basic_Data_Class.Range[0], null, _Basic_Data_Class.Select[0],
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;

            //全向範圍-全選擇
            case "Behavior_Common_DominateEnergyDetonation":
            case "Behavior_Quality_SelfEnergyDetonation":
            case "Behavior_Crescent_MoonDance":
            case "Behavior_Maid_SpinCleanse":
            case "Behavior_Rope_FlogSwirl":
            case "Behavior_Toxic_PoisonRelease":
            case "Behavior_Cuisine_MysteryCuisineSplash":
            case "Behavior_Cuisine_GlosporeBeverageSplash":
            case "Behavior_Fluorescent_ProvokeFluorescent":
            case "Behavior_ScarletKin_ScarletShieldInfusion":
            case "Behavior_GatherCobble_SatelliteBullet":
            //裝填
            case "Behavior_Throw_GentleToss_0":
            case "Behavior_Throw_ProjectilePop_0":
            case "Behavior_Throw_ProjectileFire_0":
            case "Behavior_Bow_ArrowShooting_0":
            case "Behavior_Shooter_StraightBlow_0":
            case "Behavior_Shooter_RapidBlow_0":
            case "Behavior_Shooter_ChargeBlow_0":
            case "Behavior_Chef_CuisineEntertain_0":
            case "Behavior_Stone_RockFort_0":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                        _Map_BattleCreator.Find_Divert
                        (_Basic_Key_String,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                        null, null,
                        RangeType: "Overall", PathType: "Null", SelectType: "All",
                        _Basic_Data_Class.Range[0], null, null,
                        UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                }
                break;
            #endregion

            #region - Loading -
            //裝填(完畢)
            case "Behavior_Throw_GentleToss_1":
            case "Behavior_Throw_ProjectileFire_1":
            case "Behavior_Bow_ArrowShooting_1":
            case "Behavior_Shooter_StraightBlow_1":
            case "Behavior_Shooter_RapidBlow_1":
            case "Behavior_Shooter_ChargeBlow_1":
                {
                    Answer_Return_Dictionary.Add(_Basic_Key_String, new List<Vector>());
                    Dictionary<string, BoolRangeClass> QuickSave_Select_Dictionary =
                        new Dictionary<string, BoolRangeClass>();

                    QuickSave_Select_Dictionary.Add(_Basic_Key_String, _Basic_Data_Class.Select[0]);
                    //依照裝填物而定
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        BoolRangeClass QuickSave_Select_Class = Loading.Key_Range();
                        string QuickSave_DicKey_String = Loading._Basic_Key_String;

                        if (QuickSave_Select_Class != null)
                        {
                            if (!QuickSave_Select_Dictionary.ContainsKey(QuickSave_DicKey_String))
                            {
                                QuickSave_Select_Dictionary.Add(
                                    QuickSave_DicKey_String + "_" + Loading.GetInstanceID(), QuickSave_Select_Class);
                            }
                        }
                    }

                    //方向範圍-資料路徑-區選擇
                    string QuickSave_ValueKey01_String =
                        "Path_Min_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "Path_Max_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        null, true, Time, Order);

                    foreach (string DicKey in QuickSave_Select_Dictionary.Keys)
                    {
                        Answer_Return_Dictionary[_Basic_Key_String].AddRange(
                            _Map_BattleCreator.Find_Divert
                            (DicKey,
                        _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            new List<int> { 1, 2, 3, 4 }, new Tuple { Min = QuickSave_Value01_Float, Max = QuickSave_Value02_Float },
                            RangeType: "Directional", PathType: "Normal", SelectType: "Block",
                            _Basic_Data_Class.Range[0], _Basic_Data_Class.Path[0], QuickSave_Select_Dictionary[DicKey],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                }
                break;
            case "Behavior_Throw_ProjectilePop_1":
            case "Behavior_Stone_RockFort_1":
                {
                    Dictionary<string, BoolRangeClass> QuickSave_Select_Dictionary =
                        new Dictionary<string, BoolRangeClass>();

                    QuickSave_Select_Dictionary.Add(_Basic_Key_String, _Basic_Data_Class.Select[0]);
                    //依照裝填物而定
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        BoolRangeClass QuickSave_Select_Class = Loading.Key_Range();
                        string QuickSave_DicKey_String = Loading._Basic_Key_String;

                        if (QuickSave_Select_Class != null)
                        {
                            if (!QuickSave_Select_Dictionary.ContainsKey(QuickSave_DicKey_String))
                            {
                                QuickSave_Select_Dictionary.Add(
                                    QuickSave_DicKey_String + "_" + Loading.GetInstanceID(), QuickSave_Select_Class);
                            }
                        }
                    }

                    //方向範圍-區選擇
                    foreach (string DicKey in QuickSave_Select_Dictionary.Keys)
                    {
                        Answer_Return_Dictionary.Add(DicKey,
                            _Map_BattleCreator.Find_Divert
                            (DicKey,
                            _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            new List<int> { 1, 2, 3, 4 }, null,
                            RangeType: "Directional", PathType: "Null", SelectType: "Block",
                            _Basic_Data_Class.Range[0], null, QuickSave_Select_Dictionary[DicKey],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                }
                break;

            case "Behavior_Chef_CuisineEntertain_1":
                {
                    Dictionary<string, BoolRangeClass> QuickSave_Select_Dictionary =
                        new Dictionary<string, BoolRangeClass>();

                    QuickSave_Select_Dictionary.Add(_Basic_Key_String, _Basic_Data_Class.Select[0]);
                    //依照裝填物而定
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        BoolRangeClass QuickSave_Select_Class = Loading.Key_Range();
                        string QuickSave_DicKey_String = Loading._Basic_Key_String;

                        if (QuickSave_Select_Class != null)
                        {
                            if (!QuickSave_Select_Dictionary.ContainsKey(QuickSave_DicKey_String))
                            {
                                QuickSave_Select_Dictionary.Add(
                                    QuickSave_DicKey_String + "_" + Loading.GetInstanceID(), QuickSave_Select_Class);
                            }
                        }
                    }

                    //全向範圍-區選擇
                    foreach (string DicKey in QuickSave_Select_Dictionary.Keys)
                    {
                        Answer_Return_Dictionary.Add(DicKey,
                            _Map_BattleCreator.Find_Divert
                            (DicKey,
                            _Owner_Card_Script._Range_Path_Class.Data, _Owner_Card_Script._Range_Select_Class.Data,
                            null, null,
                            RangeType: "Overall", PathType: "Null", SelectType: "Block",
                            _Basic_Data_Class.Range[0], null, QuickSave_Select_Dictionary[DicKey],
                            UserCoordinate: StartCoordinate, TargetCoordinate: StartCoordinate));
                    }
                }
                break;
            #endregion

            #region - Other -
            default:
                print("No,Key_ViewOn:" + _Basic_Key_String);
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        EndPass:
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public List<_Map_BattleObjectUnit> Key_Target(string Key, PathPreviewClass PathPreview, int Time, int Order)
    {
        //回傳----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> Answer_Return_ScriptsList = new List<_Map_BattleObjectUnit>();
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        switch (Key)
        {
            case "Shift":
                switch (_Basic_Key_String)
                {
                    case "Behavior_投擲":
                        {
                            Answer_Return_ScriptsList.Add(PathPreview.HitObject);
                            Answer_Return_ScriptsList.Add(_Owner_Card_Script._Card_UseObject_Script);
                        }
                        break;
                }
                break;
            default:
                switch (_Basic_Key_String)
                {
                    //支配
                    case "Behavior_Arthropod_HiveDominate":
                    case "Behavior_Maid_MaidGrace":
                    case "Behavior_Float_RoveDominate":
                    case "Behavior_Stone_StoneDominate":
                    case "Behavior_FlashingTachi_Sunrise":
                    //無對象
                    case "Behavior_Common_AwkwardSprint":
                    case "Behavior_Object_DoublePrepare":
                    case "Behavior_Flail_Astrology":
                    case "Behavior_Arthropod_PhototropismFlying":
                    case "Behavior_Arthropod_StruggleCrawling":
                    case "Behavior_Arthropod_ArthropodDash":
                    case "Behavior_Arthropod_HiveMindGuild":
                    case "Behavior_Arthropod_LarvaeIncubation":
                    case "Behavior_Profession_PlanSet":
                    case "Behavior_Maid_GraceStep":
                    case "Behavior_Frank_FrankStep":
                    case "Behavior_Survival_SurvivalIntuition":
                    case "Behavior_Fighting_FifthHighParry":
                    case "Behavior_Float_CurrentDrift":
                    case "Behavior_Float_CurrentCircumvention":
                    case "Behavior_Float_CornerSurge":
                    case "Behavior_Stone_StoneStep":
                    case "Behavior_Stone_BreakBurst":
                    case "Behavior_Stone_StoneCarapaceSublimation":
                    case "Behavior_Stone_DelaySlate":
                    case "Behavior_Bush_BushSneak":
                    case "Behavior_Fluorescent_GlowCrawls":
                    case "Behavior_Symbiotic_SymbioticOffering":
                    case "Behavior_Malogic_MalogicRewrite":
                    case "Behavior_ScarletKin_ScarletDash":
                    case "Behavior_ScarletKin_ScarletSideWalk":
                    case "Behavior_ScarletKin_ScarletPace":
                    case "Behavior_ScarletKin_ScarletTransmit":
                    case "Behavior_ScarletKin_ScarletStride":
                    case "Behavior_ScarletKin_ScarletPerception":
                    case "Behavior_EternalLighthouse_SelfRebirth":
                    //裝填
                    case "Behavior_Throw_GentleToss_0":
                    case "Behavior_Throw_ProjectilePop_0":
                    case "Behavior_Throw_ProjectileFire_0":
                    case "Behavior_Bow_ArrowShooting_0":
                    case "Behavior_Shooter_StraightBlow_0":
                    case "Behavior_Shooter_RapidBlow_0":
                    case "Behavior_Shooter_ChargeBlow_0":
                    case "Behavior_Chef_CuisineEntertain_0":
                    case "Behavior_Stone_RockFort_0":
                        {
                            //自範圍
                            Answer_Return_ScriptsList.Add(_Basic_Source_Class.Source_Creature._Basic_Object_Script);
                        }
                        break;

                    //投射物-使用位置變為最終位置
                    case "Behavior_Blade_SwordWave":
                    case "Behavior_Sharp_PunctureWave":
                    case "Behavior_Fangs_FlightFangs":
                    case "Behavior_Arthropod_PhototropismSacrificeShooting":
                    case "Behavior_Stone_WhirlingStone":
                    case "Behavior_Fluorescent_FluorescentMark":
                    case "Behavior_Phantom_PhantomBug":
                    case "Behavior_FlashingTachi_NewSun":
                        {
                            //碰撞物/穿透物
                            Answer_Return_ScriptsList.Add(PathPreview.HitObject);
                            //Answer_Return_ScriptsList.AddRange(PathPreview.PassObjects);(穿透物會包含牆壁等)
                        }
                        break;
                    //裝填(完畢)
                    case "Behavior_Throw_GentleToss_1":
                    case "Behavior_Throw_ProjectilePop_1":
                    case "Behavior_Throw_ProjectileFire_1":
                    case "Behavior_Bow_ArrowShooting_1":
                    case "Behavior_Shooter_StraightBlow_1":
                    case "Behavior_Shooter_RapidBlow_1":
                    case "Behavior_Shooter_ChargeBlow_1":
                    case "Behavior_Chef_CuisineEntertain_1":
                    case "Behavior_Stone_RockFort_1":
                        {
                            //依照裝填物而定
                            foreach (_Effect_EffectCardUnit Loading in 
                                _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                            {
                                string QuickSave_DicKey_String = 
                                    Loading._Basic_Key_String + "_" + Loading.GetInstanceID();
                                if (Key == QuickSave_DicKey_String)
                                {
                                    Answer_Return_ScriptsList.AddRange(
                                        Loading.Key_Target(_Owner_Card_Script ,PathPreview, Time, Order));
                                }
                            }
                        }
                        break;

                    //移動攻擊
                    case "Behavior_Frank_FrankSwoop":
                    case "Behavior_Brutal_BrutalDash":
                    case "Behavior_Stone_RollingStone":
                    case "Behavior_ScarletKin_ScarletBruteCharge":
                        {
                            Answer_Return_ScriptsList.Add(PathPreview.HitObject);
                        }
                        break;

                    //受到自身驅動者
                    case "Behavior_Common_DominateEnergyDetonation":
                        {
                            List<SelectUnitClass> QuickSave_SelectRange_ClassList =
                                _Owner_Card_Script._Range_UseData_Class.Select;
                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                _Map_BattleCreator.
                                RangeTargets("UserDriving", QuickSave_SelectRange_ClassList, 
                                _Basic_Source_Class, Time, Order);
                            Answer_Return_ScriptsList.AddRange(QuickSave_Objects_ScriptsList);
                        }
                        break;

                    case "Behavior_Fluorescent_ProvokeFluorescent":
                        {
                            //應以輸出為地塊
                            _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
                            _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
                            //判定範圍
                            List<SelectUnitClass> QuickSave_SelectRange_ClassList =
                                _Owner_Card_Script._Range_UseData_Class.Select;
                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                _Map_BattleCreator.
                                RangeTargets("All", QuickSave_SelectRange_ClassList,
                                _Basic_Source_Class, Time, Order);
                            List<Vector> QuickSave_Coordinate_ClassList =
                                new List<Vector>();
                            //持有效果的物件的座標集
                            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    Object.Key_Tag(_Basic_Source_Class, Object._Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Fluorescent" };
                                if (_Skill_Manager.
                                    TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    Vector QuickSave_Coor_Class = Object.TimePosition(Time, Order);
                                    if (!QuickSave_Coordinate_ClassList.Contains(QuickSave_Coor_Class))
                                    {
                                        QuickSave_Coordinate_ClassList.Add(QuickSave_Coor_Class);
                                    }
                                }
                            }
                            //
                            foreach (Vector Vector in QuickSave_Coordinate_ClassList)
                            {
                                Answer_Return_ScriptsList.AddRange(
                                _Object_Manager.
                                TimeObjects("All", _Basic_Source_Class,
                                Time, Order, Vector));
                            }
                        }
                        break;

                    case "Behavior_Quality_SelfEnergyDetonation":
                        {
                            //Select範圍(不論敵我)
                            List<SelectUnitClass> QuickSave_SelectRange_ClassList =
                                _Owner_Card_Script._Range_UseData_Class.Select;
                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                _Map_BattleCreator.
                                RangeTargets("All", QuickSave_SelectRange_ClassList,
                                _Basic_Source_Class, Time, Order);
                            Answer_Return_ScriptsList.AddRange(QuickSave_Objects_ScriptsList);
                        }
                        break;

                    //通常目標(UseCenter等不會亂變動)
                    default:
                        {
                            //Select範圍
                            List<SelectUnitClass> QuickSave_SelectRange_ClassList =
                                _Owner_Card_Script._Range_UseData_Class.Select;
                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                _Map_BattleCreator.
                                RangeTargets("Normal", QuickSave_SelectRange_ClassList,
                                _Basic_Source_Class, Time, Order);
                            Answer_Return_ScriptsList.AddRange(QuickSave_Objects_ScriptsList);
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //刪除事件——————————————————————————————————————————————————————————————————————
    public Dictionary<string, PathPreviewClass> Key_Anime(
        _Map_BattleObjectUnit UsingObject, 
        _Map_BattleObjectUnit HateTarget, bool Action,int Time, int Order)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        Dictionary<string, PathPreviewClass> Answer_Return_Dictionary =
            new Dictionary<string, PathPreviewClass>();

        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_MoveManager _Map_MoveManager = _Map_Manager._Map_MoveManager;

        _Basic_Source_Class.Source_Coordinate = 
            new Vector(_Owner_Card_Script._Card_UseCenter_Class);
        _Map_BattleObjectUnit QuickSave_Object_Script = UsingObject;
        //----------------------------------------------------------------------------------------------------

        //前期設定(生成需要的東西)----------------------------------------------------------------------------------------------------
        _Map_Manager.BattleStateSet("AnimeMiddle", "完成動畫初期設置，進行動畫");
        //----------------------------------------------------------------------------------------------------

        //後期設定----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Move -
            case "Behavior_Common_AwkwardSprint":
            case "Behavior_Arthropod_PhototropismFlying":
            case "Behavior_Arthropod_StruggleCrawling":
            case "Behavior_Arthropod_ArthropodDash":
            case "Behavior_Arthropod_HiveMindGuild":
            case "Behavior_Maid_GraceStep":
            case "Behavior_Frank_FrankStep":
            case "Behavior_Survival_SurvivalIntuition":
            case "Behavior_Float_CurrentDrift":
            case "Behavior_Float_CurrentCircumvention":
            case "Behavior_Float_CornerSurge":
            case "Behavior_Stone_StoneStep":
            case "Behavior_Stone_BreakBurst":
            case "Behavior_Bush_BushSneak":
            case "Behavior_Fluorescent_GlowCrawls":
            case "Behavior_ScarletKin_ScarletDash":
            case "Behavior_ScarletKin_ScarletSideWalk":
            case "Behavior_ScarletKin_ScarletPace":
            case "Behavior_ScarletKin_ScarletStride":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        Key_MoveCall("MainUse", _Owner_Card_Script._Card_UseCenter_Class, _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                        QuickSave_Object_Script, HateTarget, Action, Time, Order);
                    Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                    if (Action)
                    {
                        _Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                        Answer_Return_Dictionary["MainUse"].FinalPath, Answer_Return_Dictionary,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, Time, Order);
                        _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                    }
                }
                break;
            //傳送
            case "Behavior_ScarletKin_ScarletTransmit":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        Key_MoveCall("MainUse", UsingObject.TimePosition(Time, Order), _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                        QuickSave_Object_Script, HateTarget, Action, Time, Order);
                    Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                    if (Action)
                    {
                        _Map_MoveManager.MoveCoroutineCaller("Set", "Instant",
                        Answer_Return_Dictionary["MainUse"].FinalPath, Answer_Return_Dictionary,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, Time, Order);
                        _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                    }
                }
                break;
            //移動攻擊
            case "Behavior_Frank_FrankSwoop":
            case "Behavior_Brutal_BrutalDash":
            case "Behavior_Stone_RollingStone":
            case "Behavior_ScarletKin_ScarletBruteCharge":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        Key_MoveCall("MainUse", _Owner_Card_Script._Card_UseCenter_Class, _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                        QuickSave_Object_Script, HateTarget, Action, Time, Order);
                    Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                    if (Action)
                    {
                        _Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                        Answer_Return_Dictionary["MainUse"].FinalPath, Answer_Return_Dictionary,
                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, Time, Order);
                        _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                    }
                }
                break;
            //投射物幻影
            case "Behavior_Blade_SwordWave":
            case "Behavior_Sharp_PunctureWave":
            case "Behavior_Fangs_FlightFangs":
            case "Behavior_Arthropod_PhototropismSacrificeShooting":
            case "Behavior_Stone_WhirlingStone":
            case "Behavior_Fluorescent_FluorescentMark":
            case "Behavior_Phantom_PhantomBug":
            case "Behavior_FlashingTachi_NewSun":
                {
                    //生成/投射物幻影
                    _Map_BattleObjectUnit QuickSave_Project_Script =
                        _World_Manager._Object_Manager.ObjectSet("Object", "Object_Phantom_Normal",
                        _Owner_Card_Script._Card_StartCenter_Class, _Basic_Source_Class, Time, Order);
                    if (QuickSave_Project_Script != null)
                    {
                        _Owner_Card_Script._Basic_SaveData_Class.ObjectListDataAdd("Project", QuickSave_Project_Script);
                        PathPreviewClass QuickSave_PathPreview_Class =
                            Key_MoveCall("MainUse", _Owner_Card_Script._Card_StartCenter_Class, _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                            QuickSave_Project_Script, HateTarget, Action, Time, Order);
                        Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                        //投射
                        if (Action)
                        {
                            //行動
                            _Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                            Answer_Return_Dictionary["MainUse"].FinalPath, Answer_Return_Dictionary,
                            _Basic_Source_Class, QuickSave_Project_Script._Basic_Source_Class, Time, Order);
                            _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                        }
                        else
                        {
                            if (QuickSave_Object_Script._Basic_SaveData_Class.BoolDataGet("Creation"))
                            {
                                //移除投射物
                                QuickSave_Object_Script.DeleteSet();
                            }
                            _Owner_Card_Script._Basic_SaveData_Class.ObjectListDataSet("Project", null);
                        }
                    }
                }
                break;
            #endregion

            #region - Loading -
            //裝填(完畢)
            case "Behavior_Throw_GentleToss_1":
            case "Behavior_Throw_ProjectileFire_1":
            case "Behavior_Bow_ArrowShooting_1":
            case "Behavior_Shooter_StraightBlow_1":
            case "Behavior_Shooter_RapidBlow_1":
            case "Behavior_Shooter_ChargeBlow_1":
                {
                    //依照裝填物而定
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        string QuickSave_DicKey_String =
                            Loading._Basic_Key_String + "_" + Loading.GetInstanceID();
                        QuickSave_Object_Script = Loading.
                            Key_Objects(UsingObject.TimePosition(Time, Order), _Owner_Card_Script,
                            _Owner_Card_Script._Effect_Loading_Dictionary[Loading],
                            Action, Time, Order);

                        //依照裝填後招式行動
                        PathPreviewClass QuickSave_PathPreview_Class =
                            Key_MoveCall(QuickSave_DicKey_String, _Owner_Card_Script._Card_StartCenter_Class, _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                            QuickSave_Object_Script, HateTarget, Action, Time, Order);
                        Answer_Return_Dictionary.Add(QuickSave_DicKey_String, QuickSave_PathPreview_Class);

                        //投射
                        if (Action)
                        {
                            //行動
                            _Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                            Answer_Return_Dictionary[QuickSave_DicKey_String].FinalPath, Answer_Return_Dictionary,
                            _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, Time, Order);
                        }
                    }
                    if (Action)
                    {
                        _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                    }
                    else
                    {
                        if (QuickSave_Object_Script._Basic_SaveData_Class.BoolDataGet("Creation"))
                        {
                            //移除投射物
                            QuickSave_Object_Script.DeleteSet();
                        }
                        _Owner_Card_Script._Basic_SaveData_Class.ObjectListDataSet("Project", null);
                    }
                }
                break;
            case "Behavior_Throw_ProjectilePop_1":
            case "Behavior_Stone_RockFort_1":
                {
                    //依照裝填物而定
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        string QuickSave_DicKey_String =
                            Loading._Basic_Key_String + "_" + Loading.GetInstanceID();
                        QuickSave_Object_Script = Loading.
                            Key_Objects(UsingObject.TimePosition(Time, Order), _Owner_Card_Script,
                            _Owner_Card_Script._Effect_Loading_Dictionary[Loading],
                            Action, Time, Order);

                        //依照裝填後招式行動
                        PathPreviewClass QuickSave_PathPreview_Class =
                            Key_MoveCall(QuickSave_DicKey_String, _Owner_Card_Script._Card_StartCenter_Class, _Owner_Card_Script._Range_UseData_Class.Path[0].Path,
                            QuickSave_Object_Script, HateTarget, Action, Time, Order);
                        Answer_Return_Dictionary.Add(QuickSave_DicKey_String, QuickSave_PathPreview_Class);

                        //投射
                        if (Action)
                        {
                            //行動
                            _Map_MoveManager.MoveCoroutineCaller("Set", "Instant",
                            Answer_Return_Dictionary[QuickSave_DicKey_String].FinalPath, Answer_Return_Dictionary,
                            _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, Time, Order);
                        }
                    }
                    if (Action)
                    {
                        _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
                    }
                    else
                    {
                        if (QuickSave_Object_Script._Basic_SaveData_Class.BoolDataGet("Creation"))
                        {
                            //移除投射物
                            QuickSave_Object_Script.DeleteSet();
                        }
                        _Owner_Card_Script._Basic_SaveData_Class.ObjectListDataSet("Project", null);
                    }
                }
                break;
            case "Behavior_Chef_CuisineEntertain_1":
                {
                    //依照裝填物而定
                    int QuickSave_Count_Int = 0;
                    foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
                    {
                        string QuickSave_DicKey_String =
                            Loading._Basic_Key_String + "_" + Loading.GetInstanceID();

                        QuickSave_Object_Script = Loading.
                            Key_Objects(UsingObject.TimePosition(Time, Order), _Owner_Card_Script,
                            _Owner_Card_Script._Effect_Loading_Dictionary[Loading],
                            Action, Time, Order);
                        //依照裝填後招式行動
                        PathPreviewClass QuickSave_PathPreview_Class =
                            Key_MoveCall(QuickSave_DicKey_String, _Owner_Card_Script._Card_UseCenter_Class, null,
                            QuickSave_Object_Script, HateTarget, Action, Time, Order);
                        Answer_Return_Dictionary.Add(QuickSave_DicKey_String, QuickSave_PathPreview_Class);
                        if (Action)
                        {
                            QuickSave_Count_Int++;
                            StartCoroutine(_World_Manager._Effect_Manager.
                                EffectAnim(_Basic_Source_Class, Answer_Return_Dictionary,
                                CallNext: (QuickSave_Count_Int == _Owner_Card_Script._Effect_Loading_Dictionary.Count)));
                        }
                    }
                }
                break;
            #endregion

            #region - Attack -
            case "Behavior_Common_DominateEnergyDetonation":
            case "Behavior_Quality_SelfEnergyDetonation":
            case "Behavior_Blade_SideCutting":
            case "Behavior_Blade_BattoSan":
            case "Behavior_Blade_UpwardSlash":
            case "Behavior_Blade_DownwardSlash":
            case "Behavior_Blade_CrossSlash":
            case "Behavior_Blade_CrossRising":
            case "Behavior_Blade_UpRising":
            case "Behavior_Sharp_SharpPuncture":
            case "Behavior_Sharp_BackhandThrust":
            case "Behavior_Flail_StarShake":
            case "Behavior_Flail_MeteorSpin":
            case "Behavior_Flail_CometStrike":
            case "Behavior_Crescent_MoonDance":
            case "Behavior_Fangs_ProbeBite":
            case "Behavior_Ink_InkVolley":
            case "Behavior_Ink_InkBurst":
            case "Behavior_Ink_InkSwirl":
            case "Behavior_Tenticle_TenticleFlog":
            case "Behavior_Tenticle_TenticleFlagellate":
            case "Behavior_Tenticle_TenticleCoerce":
            case "Behavior_Arthropod_SwarmSurge":
            case "Behavior_Miner_StoneBreaker":
            case "Behavior_Miner_AccurateMine":
            case "Behavior_Guard_ProbeStrike":
            case "Behavior_Guard_FlawStrike":
            case "Behavior_Doll_ContentsStrike":
            case "Behavior_Doll_ContentsFall":
            case "Behavior_Guard_DelayingBind":
            case "Behavior_Maid_SpinCleanse":
            case "Behavior_Frank_ProbeTouch":
            case "Behavior_Cleaning_EnvironmentClean":
            case "Behavior_Fighting_ProbeTap":
            case "Behavior_Fighting_SneakTap":
            case "Behavior_Fighting_SuccessiveTap":
            case "Behavior_Fighting_DestructionPunch":
            case "Behavior_Stone_BoulderOppression":
            case "Behavior_Stone_ShatterstoneVolley":
            case "Behavior_Stone_StalagmiteArrow":
            case "Behavior_Stone_RockSpring":
            case "Behavior_Vine_SweepingVine":
            case "Behavior_Vine_VineTwist":
            case "Behavior_Rope_ReverseFlog":
            case "Behavior_Rope_FlogSwirl":
            case "Behavior_Rope_StiffWhip":
            case "Behavior_Rope_FearWhip":
            case "Behavior_Toxic_PoisonRelease":
            case "Behavior_Fluorescent_ProvokeFluorescent":
            case "Behavior_Light_LightFlash":
            case "Behavior_LanspidDagger_TransparentBlade":
            case "Behavior_FlashingTachi_Twilight":
            case "Behavior_GatherCobble_SatelliteBullet":
            case "Behavior_GatherCobble_CometRain":
            case "Behavior_OrganicGauntlets_DevourFinger":
            case "Behavior_Limu_LimuCatch":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        Key_MoveCall("MainUse", UsingObject.TimePosition(Time, Order), null/*沒有路徑*/,
                        QuickSave_Object_Script, HateTarget, Action, Time, Order);
                    Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                    if (Action)
                    {
                        StartCoroutine(_World_Manager._Skill_Manager.
                            AttackAnim(_Basic_Source_Class, Answer_Return_Dictionary, CallNext: true));
                    }
                }
                break;
            #endregion
            #region - Effect -
            //支配
            case "Behavior_Arthropod_HiveDominate":
            case "Behavior_Maid_MaidGrace":
            case "Behavior_Float_RoveDominate":
            case "Behavior_Stone_StoneDominate":
            case "Behavior_FlashingTachi_Sunrise":
            //效果
            case "Behavior_Object_DoublePrepare":
            case "Behavior_Flail_Astrology":
            case "Behavior_投擲":
            case "Behavior_Cuisine_FailCuisine":
            case "Behavior_Cuisine_InvigoratingAppetizer":
            case "Behavior_Cuisine_HealingMainCourse":
            case "Behavior_Cuisine_EnergeticDessert":
            case "Behavior_Cuisine_MysteryCuisineSplash":
            case "Behavior_Cuisine_MysteryCuisineEat":
            case "Behavior_Cuisine_HerbDelightEat":
            case "Behavior_Cuisine_GlosporeBeverageSplash":
            case "Behavior_Cuisine_GlosporeBeverageEat":
            case "Behavior_Part_RepairPartRepair":
            case "Behavior_Arthropod_LarvaeIncubation":
            case "Behavior_Profession_PlanSet":
            case "Behavior_Chef_CuisineRemaking":
            case "Behavior_Tailor_FabricRepair":
            case "Behavior_Fighting_FifthHighParry":
            case "Behavior_Stone_StoneCarapaceSublimation":
            case "Behavior_Stone_DelaySlate":
            case "Behavior_Vine_RattanCocoon":
            case "Behavior_Medicine_HealingPotion":
            case "Behavior_Medicine_HerbalSphereSmoke":
            case "Behavior_Medicine_HerbalSphereEat":
            case "Behavior_Symbiotic_SymbioticOffering":
            case "Behavior_Malogic_MalogicRewrite":
            case "Behavior_ScarletKin_ScarletHeal":
            case "Behavior_ScarletKin_ScarletPurify":
            case "Behavior_ScarletKin_ScarletPerception":
            case "Behavior_ScarletKin_ScarletShieldInfusion":
            case "Behavior_EternalLighthouse_SelfRebirth":
            //裝填
            case "Behavior_Throw_GentleToss_0":
            case "Behavior_Throw_ProjectilePop_0":
            case "Behavior_Throw_ProjectileFire_0":
            case "Behavior_Bow_ArrowShooting_0":
            case "Behavior_Shooter_StraightBlow_0":
            case "Behavior_Shooter_RapidBlow_0":
            case "Behavior_Shooter_ChargeBlow_0":
            case "Behavior_Chef_CuisineEntertain_0":
            case "Behavior_Stone_RockFort_0":
                {
                    PathPreviewClass QuickSave_PathPreview_Class =
                        Key_MoveCall("MainUse", _Owner_Card_Script._Card_UseCenter_Class, null,
                        QuickSave_Object_Script, HateTarget, Action, Time, Order);
                    Answer_Return_Dictionary.Add("MainUse", QuickSave_PathPreview_Class);
                    if (Action)
                    {
                        StartCoroutine(_World_Manager._Effect_Manager.
                            EffectAnim(_Basic_Source_Class, Answer_Return_Dictionary, true));
                    }
                }
                break;
            #endregion

            #region - Other -
            default:
                print("Wrong Anime With " + _Basic_Key_String);
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #region - Call -
    //效果呼叫—————————————————————————————————————————————————————————————————————
    public List<string> Key_ActionCall(Dictionary<string, PathPreviewClass> PathPreview, 
        _Map_BattleObjectUnit HateTarget, bool Action,int Time,int Order)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        //回傳直
        List<string> Answer_Return_StringList = new List<string>();
        _Object_CreatureUnit QuickSave_Creature_Script = _Owner_Card_Script._Basic_Source_Class.Source_Creature;
        _Owner_Card_Script._Card_AimTargets_ScriptsList.Clear();
        _Owner_Card_Script._Card_HitTargets_ScriptsList.Clear();
        //----------------------------------------------------------------------------------------------------

        //目標設定(將範圍的物件評測能否作為目標)----------------------------------------------------------------------------------------------------
        Dictionary<string,List<_Map_BattleObjectUnit>> QuickSave_SkillTarget_Dictionary = 
            new Dictionary<string, List<_Map_BattleObjectUnit>>();
        //有被找到都是要用的
        foreach (string Key in PathPreview.Keys)
        {
            QuickSave_SkillTarget_Dictionary.Add(
                Key, Key_Target(Key, PathPreview[Key], Time, Order));
            Answer_Return_StringList.AddRange(PathPreview[Key].ScoreList);
        }
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //目標回饋
        bool QuickSave_IsMiss_Bool = true;
        foreach (string Key in QuickSave_SkillTarget_Dictionary.Keys)
        {
            List<_Map_BattleObjectUnit> QuickSave_Targets_ScritpsList = QuickSave_SkillTarget_Dictionary[Key];
            foreach (_Map_BattleObjectUnit Object in QuickSave_Targets_ScritpsList)
            {
                //行為----------------------------------------------------------------------------------------------------
                if (Object == null)
                {
                    continue;
                }
                QuickSave_IsMiss_Bool = false;
                List<string> QuickSave_TargetAction_StringList =
                    Key_SkillCall(Key,
                    Object._Basic_Source_Class, _Owner_Card_Script._Card_UseObject_Script, 
                    HateTarget, Action, Time, Order);
                Answer_Return_StringList.AddRange(QuickSave_TargetAction_StringList);
                if (Action && Object != null)
                {
                    /*仇恨值計算
                    if (Target[a]._NPC_Script != null &&
                    Target[a]._NPC_Script._AI_HateList_ScriptList.Contains(QuickSave_Creature_Script._Creature_BattleObjectt_Script))
                    {
                        int QuickSave_HateListPos =
                            Target[a]._NPC_Script._AI_HateList_ScriptList.IndexOf(QuickSave_Creature_Script._Creature_BattleObjectt_Script);
                        Target[a]._NPC_Script._AI_HateList_FloatList[a] -= _World_Manager._Object_Manager.ActionScore(Target[a]._NPC_Script._Creature_AI_Class, QuickSave_TargetAction_StringList, null);
                    }*/
                }
                //----------------------------------------------------------------------------------------------------
            }
        }

        if(QuickSave_IsMiss_Bool)
        {
            //----------------------------------------------------------------------------------------------------
            Dictionary<string, List<string>> QuickSave_BehaviorMiss_Dicitonary =
            _Basic_Source_Class.Source_BattleObject.SituationCaller
                ("BehaviorMiss", null, 
                _Basic_Source_Class, null, null, 
                HateTarget, Action, Time, Order);
            Answer_Return_StringList.AddRange(_World_Manager._World_GeneralManager.
                SituationCaller_TransToStringList(QuickSave_BehaviorMiss_Dicitonary));
            //----------------------------------------------------------------------------------------------------
        }
        //觸發
        List<string> QuickSave_ReactCall_StringList = 
            QuickSave_Creature_Script.ReactCall(_Basic_Source_Class, HateTarget, Action, Time, Order);
        if (QuickSave_ReactCall_StringList.Count > 0)
        {
            Answer_Return_StringList.AddRange(QuickSave_ReactCall_StringList);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Dictionary<string, List<string>> QuickSave_BehaviorUseEndSituation_Dicitonary =
        _Basic_Source_Class.Source_BattleObject.SituationCaller
            ("BehaviorUseEnd", null, 
            _Basic_Source_Class, null, _Owner_Card_Script._Card_UseObject_Script, 
            HateTarget, Action, Time, Order);
        Answer_Return_StringList.AddRange(_World_Manager._World_GeneralManager.
            SituationCaller_TransToStringList(QuickSave_BehaviorUseEndSituation_Dicitonary));
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //移動時獲得——————————————————————————————————————————————————————————————————————
    public PathPreviewClass Key_MoveCall(string Key, Vector UseCoor, DirectionPathClass InputData,
        _Map_BattleObjectUnit UsingObject, _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        PathPreviewClass Answer_Return_Class = null;
        _Map_MoveManager _Map_MoveManager = _World_Manager._Map_Manager._Map_MoveManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //基本移動
        switch (Key)
        {
            //通常情況
            default:
                {
                    switch (_Basic_Key_String)
                    {
                        case "Behavior_Common_AwkwardSprint":
                        case "Behavior_Arthropod_PhototropismFlying":
                        case "Behavior_Arthropod_StruggleCrawling":
                        case "Behavior_Arthropod_ArthropodDash":
                        case "Behavior_Arthropod_HiveMindGuild":
                        case "Behavior_Maid_GraceStep":
                        case "Behavior_Frank_FrankStep":
                        case "Behavior_Survival_SurvivalIntuition":
                        case "Behavior_Float_CurrentDrift":
                        case "Behavior_Float_CurrentCircumvention":
                        case "Behavior_Float_CornerSurge":
                        case "Behavior_Stone_StoneStep":
                        case "Behavior_Stone_BreakBurst":
                        case "Behavior_Bush_BushSneak":
                        case "Behavior_Fluorescent_GlowCrawls":
                        case "Behavior_ScarletKin_ScarletDash":
                        case "Behavior_ScarletKin_ScarletSideWalk":
                        case "Behavior_ScarletKin_ScarletPace":
                        case "Behavior_ScarletKin_ScarletStride":
                        //移動攻擊
                        case "Behavior_Frank_FrankSwoop":
                        case "Behavior_Brutal_BrutalDash":
                        case "Behavior_Stone_RollingStone":
                        case "Behavior_ScarletKin_ScarletBruteCharge":
                        //投射物
                        case "Behavior_Blade_SwordWave":
                        case "Behavior_Sharp_PunctureWave":
                        case "Behavior_Fangs_FlightFangs":
                        case "Behavior_Arthropod_PhototropismSacrificeShooting":
                        case "Behavior_Stone_WhirlingStone":
                        case "Behavior_Fluorescent_FluorescentMark":
                        case "Behavior_Phantom_PhantomBug":
                        case "Behavior_FlashingTachi_NewSun":
                        //裝填
                        case "Behavior_Throw_GentleToss_1":
                        case "Behavior_Throw_ProjectileFire_1":
                        case "Behavior_Bow_ArrowShooting_1":
                        case "Behavior_Shooter_StraightBlow_1":
                        case "Behavior_Shooter_RapidBlow_1":
                        case "Behavior_Shooter_ChargeBlow_1":
                            {
                                Answer_Return_Class =
                                    _Map_MoveManager.MovePreview(Key, "Normal", 0, InputData,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                            }
                            break;

                        //傳送
                        case "Behavior_ScarletKin_ScarletTransmit":
                        //裝填
                        case "Behavior_Throw_ProjectilePop_1":
                        case "Behavior_Stone_RockFort_1":
                            {
                                Answer_Return_Class =
                                    _Map_MoveManager.MovePreview(Key, "Instant", 0, InputData,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                            }
                            break;
                        default:
                            {
                                Answer_Return_Class = new PathPreviewClass
                                {
                                    UseObject = UsingObject,
                                    FinalCoor = UseCoor
                                };
                            }
                            break;
                    }
                }
                break;
            //強制位移
            case "Shift":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Behavior_Stone_RockSpring":
                            {
                                Answer_Return_Class =
                                    _Map_MoveManager.MovePreview(Key, "Normal", 0, InputData,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (UsingObject._Basic_Source_Class.Source_BattleObject == UsingObject)
        {
            Answer_Return_Class.ScoreList.Add("Move_Destination｜" + Answer_Return_Class.FinalCoor.Vector3Int);
        }
        return Answer_Return_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //效果分類(移動後續進行)——————————————————————————————————————————————————————————————————————
    public List<string> Key_SkillCall(string Key,
        SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action,int Time,int Order)
    //State：、Action：true 執行動作, false 純取得數值
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Map_BattleObjectUnit QuickSave_Target_Script = TargetSource.Source_BattleObject;
        //----------------------------------------------------------------------------------------------------

        //先行效果(通常為消耗等)----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - MediumPoint -
            //MediumPoint消耗值
            case "Behavior_Cuisine_FailCuisine":
            case "Behavior_Cuisine_InvigoratingAppetizer":
            case "Behavior_Cuisine_HealingMainCourse":
            case "Behavior_Cuisine_EnergeticDessert":
            case "Behavior_Cuisine_MysteryCuisineSplash":
            case "Behavior_Cuisine_MysteryCuisineEat":
            case "Behavior_Cuisine_HerbDelightEat":
            case "Behavior_Cuisine_GlosporeBeverageSplash":
            case "Behavior_Cuisine_GlosporeBeverageEat":
            case "Behavior_Part_RepairPartRepair":
            case "Behavior_Arthropod_PhototropismSacrificeShooting":
            case "Behavior_Medicine_HealingPotion":
            case "Behavior_Medicine_HerbalSphereSmoke":
            case "Behavior_Medicine_HerbalSphereEat":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));
                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
            //MediumPoint消耗值-使用Catalyst
            case "Behavior_Malogic_MalogicRewrite":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
            //MediumPoint消耗值-使用最大介質百分比
            case "Behavior_Vine_RattanCocoon":
            case "Behavior_Arthropod_LarvaeIncubation":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜User_Concept_Default_Point_MediumPoint_Total_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
            #endregion
            #region - CatalystPoint -
            //CatalystPoint消耗值-觸媒
            case "Behavior_Blade_SwordWave":
            case "Behavior_Sharp_PunctureWave":
            case "Behavior_Fangs_FlightFangs":
            case "Behavior_Phantom_PhantomBug":
            case "Behavior_FlashingTachi_NewSun":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_CatalystPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    QuickSave_ValueKey_String =
                        "Value_Default_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    QuickSave_Value_Float -=//數值增加
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                        HateTarget, Action, Time, Order, Action);
                }
                break;
            //CatalystPoint消耗值-使用當前CatalystPoint
            case "Behavior_EternalLighthouse_SelfRebirth":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_CatalystPoint_Default｜User_Concept_Default_Point_CatalystPoint_Point_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
            #endregion
            #region - ConsciousnessPoint -
            //ConsciousnessPoint消耗值
            case "Behavior_Arthropod_StruggleCrawling":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_ConsciousnessPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
            #endregion
            #region - Comple -
            case "Behavior_Symbiotic_SymbioticOffering":
                {
                    //次數判定
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1, "Consume"))
                    {
                        break;
                    }
                    //消耗判定
                    if (_Basic_SaveData_Class.ValueDataCheck(_Basic_Key_String))
                    {
                        break;
                    }
                    //消耗數值
                    string QuickSave_ValueKey_String =
                        "Consume_MediumPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    float QuickSave_Consume_Float =
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);

                    //消耗數值
                    QuickSave_ValueKey_String =
                        "Consume_CatalystPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                    QuickSave_Consume_Float +=
                        Mathf.Clamp(QuickSave_Value_Float, 0,
                        _Basic_Source_Class.Source_BattleObject.
                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                    _Basic_Source_Class.Source_BattleObject.PointSet(
                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                        _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order, Action);
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                }
                break;
                #endregion
        }

        #region - Special -
        //Enchance
        foreach (_Effect_EffectCardUnit Enchance in _Owner_Card_Script._Effect_Enchance_ScriptsList)
        {
            Answer_Return_StringList.AddRange(
                Enchance.Key_Consume(Key_Enchance(),
                    _Basic_Source_Class, TargetSource, UsingObject,
                    HateTarget, Action, Time, Order));
        }
        //Loading消耗
        foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
        {
            if (Loading._Basic_Key_String + "_" + Loading.GetInstanceID() == Key)
            {
                Answer_Return_StringList.AddRange(
                    Loading.Key_Consume(Key_Enchance(),
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order));
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //攻擊----------------------------------------------------------------------------------------------------
        {
            List<_Effect_EffectCardUnit> QuickSave_EffectCards_ScriptsList =
                new List<_Effect_EffectCardUnit>();
            QuickSave_EffectCards_ScriptsList.AddRange(_Owner_Card_Script._Effect_Effect_ScriptsList);
            QuickSave_EffectCards_ScriptsList.AddRange(_Owner_Card_Script._Effect_Enchance_ScriptsList);
            //主要傷害(會被變動)
            List<DamageClass> QuickSave_TotalDamage_ClassList = new List<DamageClass>();
            QuickSave_TotalDamage_ClassList.AddRange(
                Key_Attack(Key, _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order));
            #region - Special -
            //Loading
            foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
            {
                if (Loading._Basic_Key_String + "_" + Loading.GetInstanceID() == Key)
                {
                    List<DamageClass> QuickSave_Damage_ClassList = 
                        Loading.Key_Attack(Key_Enchance(), _Owner_Card_Script,
                            _Basic_Source_Class, TargetSource, _Owner_Card_Script._Effect_Loading_Dictionary[Loading], 
                            HateTarget, Action, Time, Order);
                    foreach (DamageClass Damage in QuickSave_Damage_ClassList)
                    {
                        Damage.Damage += 
                            Key_Effect_AttackAdd(
                                _Basic_Source_Class, TargetSource, _Owner_Card_Script._Effect_Loading_Dictionary[Loading], 
                                HateTarget, Action, Time, Order);
                        Damage.Damage *= 
                            Key_Effect_AttackMultiply(
                                _Basic_Source_Class, TargetSource, _Owner_Card_Script._Effect_Loading_Dictionary[Loading], 
                                HateTarget, Action, Time, Order);
                    }
                    QuickSave_TotalDamage_ClassList.AddRange(QuickSave_Damage_ClassList);
                }
            }
            #endregion

            //追加傷害(基本上恆定/被視為主要攻擊)
            QuickSave_TotalDamage_ClassList.AddRange(
                Key_AttackAdden(Key, _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order));
            //EffectCard
            foreach (_Effect_EffectCardUnit Effect in QuickSave_EffectCards_ScriptsList)
            {
                QuickSave_TotalDamage_ClassList.AddRange(Effect.
                    Key_AttackAdden(Key_Enchance(), _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order));
            }

            //傷害輸出
            Answer_Return_StringList.AddRange(
                QuickSave_Target_Script.Damaged(QuickSave_TotalDamage_ClassList, UsingObject, 
                HateTarget, Action, Time, Order));
        }
        //----------------------------------------------------------------------------------------------------


        //後期效果----------------------------------------------------------------------------------------------------
        {
            switch (_Basic_Key_String)
            {
                #region - Dominate
                case "Behavior_Arthropod_HiveDominate":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
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
                        }
                        else
                        {
                            //消耗數值
                            string QuickSave_ValueKey_String =
                                "Consume_ConsciousnessPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                            _Basic_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //目標獲得效果
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Arthropod_HiveDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            _World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                HateTarget, Action, Time, Order);
                        }
                    }
                    break;
                case "Behavior_Maid_MaidGrace":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
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
                        }
                        else
                        {
                            //消耗數值
                            string QuickSave_ValueKey_String =
                                "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                            _Basic_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            //標籤判定(目標)
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
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //目標獲得效果
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Maid_MaidGrace_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            _World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                HateTarget, Action, Time, Order);
                        }
                    }
                    break;
                case "Behavior_Float_RoveDominate":
                    {
                        //標籤判定(目標)
                        if (UsingObject == null)
                        {
                            print("NullUsing");
                        }
                        List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
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
                        }
                        else
                        {
                            //消耗數值
                            string QuickSave_ValueKey_String =
                                "Consume_ConsciousnessPoint_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                            _Basic_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //目標獲得效果
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Float_RoveDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            _World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                HateTarget, Action, Time, Order);
                        }
                    }
                    break;
                case "Behavior_Stone_StoneDominate":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
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
                        }
                        else
                        {
                            //消耗數值
                            string QuickSave_ValueKey_String =
                                "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                            _Basic_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //目標獲得效果
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Float_RoveDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            _World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                HateTarget, Action, Time, Order);
                        }
                    }
                    break;
                case "Behavior_FlashingTachi_Sunrise":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(
                            _Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
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
                        }
                        else
                        {
                            //消耗數值
                            string QuickSave_ValueKey_String =
                                "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                            _Basic_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //目標獲得效果
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_FlashingTachi_SunriseDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            _World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, 
                                HateTarget, Action, Time, Order);
                        }
                    }
                    break;
                #endregion
                #region - Effect -
                //給予使用效果-Spin
                case "Behavior_Flail_Astrology":
                case "Behavior_Flail_MeteorSpin":
                case "Behavior_Crescent_MoonDance":
                case "Behavior_Stone_RollingStone":
                case "Behavior_Rope_FlogSwirl":
                    {
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Spin_Spin_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;

                //給予目標效果-Reaction(消耗值)
                case "Behavior_Cuisine_EnergeticDessert":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Sensation_Reaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, TargetSource,
                                HateTarget, Action, Time, Order));
                        }
                    }
                    break;

                //給予目標效果-FluorescentPoint(消耗值)
                case "Behavior_Cuisine_GlosporeBeverageSplash":
                    {
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, TargetSource,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-騰空
                case "Behavior_Blade_UpRising":
                    {
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Soar_Soar_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                        Answer_Return_StringList.AddRange(
                            _World_Manager._Effect_Manager.
                            GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予出發點所有單位效果-FluorescentPoint
                case "Behavior_Fluorescent_GlowCrawls":
                    {
                        Vector QuickSave_Pos_Class = _Owner_Card_Script._Card_StartCenter_Class;
                        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                            _World_Manager._Object_Manager.
                                TimeObjects("All", _Basic_Source_Class,
                                Time, Order, QuickSave_Pos_Class); 

                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                        {
                            //數值設定
                            string QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, Object._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, Object._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, Object._Basic_Source_Class,
                                HateTarget, Action, Time, Order));
                        }
                    }
                    break;
                //給予使用效果-Frank
                case "Behavior_Frank_FrankStep":
                case "Behavior_Frank_FrankSwoop":
                    {
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Identity_Frank_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予目標效果-SwarmEggs(消耗值)
                case "Behavior_Arthropod_LarvaeIncubation":
                    {
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Arthropod_SwarmEggs_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, TargetSource,
                            HateTarget, Action, Time, Order));
                    }
                    break;

                //給予使用效果-FifthHighParry
                case "Behavior_Fighting_FifthHighParry":
                    {
                        //概念獲得效果
                        SourceClass ConceptSource =
                            _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                            _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Fighting_FifthHighParry_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, ConceptSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, ConceptSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, ConceptSource,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-StoneCarapace
                case "Behavior_Stone_StoneStep":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Stone_StoneCarapace_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-DelaySlate
                case "Behavior_Stone_DelaySlate":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Stone_DelaySlate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-RattanCocoon
                case "Behavior_Vine_RattanCocoon":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Vine_RattanCocoon_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //目標有Tag(Herb)物件時 給予使用效果-Hide
                case "Behavior_Bush_BushSneak":
                    {
                        Vector QuickSave_Pos_Class = UsingObject.TimePosition(Time, Order);
                        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                            _World_Manager._Object_Manager.
                                TimeObjects("All", _Basic_Source_Class,
                                Time, Order, QuickSave_Pos_Class);

                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                        {
                            List<string> QuickSave_Tag_StringList =
                                Object.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Bush" };
                            if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                //數值設定
                                string QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Hide_Hide_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        }
                    }
                    break;

                //消除且給予使用效果
                case "Behavior_Stone_StoneCarapaceSublimation":
                    {
                        //消除效果
                        float QuickSave_Stack_Float = TargetSource.Source_BattleObject.
                            Key_Stack("Key", "EffectObject_Stone_StoneCarapace_0", _Basic_Source_Class, TargetSource, UsingObject);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Stack_Float);
                        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(
                            "EffectObject_Stone_StoneCarapace_0",out _Effect_EffectObjectUnit Effect))
                        {
                            if (Action)
                            {
                                Effect.StackDecrease("Set", 65535);
                            }
                        }
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Stone_StoneSanctuary_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;

                //給予使用效果-ScarletBunch
                case "Behavior_ScarletKin_ScarletTransmit":
                case "Behavior_ScarletKin_ScarletPerception":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletBunch_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-ScarletBunch與ScarletShield(ScarletBunch))
                case "Behavior_ScarletKin_ScarletStride":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletBunch_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect01_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        //數值設定
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletShield_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect02_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        QuickSave_ValueKey_String =
                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value02_Float +=//數值增加
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        //執行
                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect01_String, Mathf.RoundToInt(QuickSave_Value01_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect02_String, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-ScarletBunch與ScarletArmor(Value+ScarletBunch)
                case "Behavior_ScarletKin_ScarletDash":
                case "Behavior_ScarletKin_ScarletSideWalk":
                case "Behavior_ScarletKin_ScarletPace":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletBunch_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));

                        //數值設定
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletArmor_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        QuickSave_ValueKey_String =
                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float +=//數值增加
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //給予使用效果-ScarletArmor(Value+ScarletBunch)(自身時增強)
                case "Behavior_ScarletKin_ScarletShieldInfusion":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Concept" };
                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            break;
                        }
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletShield_0｜Value_Default_Default_Default_Default_Default_Default｜1";
                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                        {
                            QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Scarlet_ScarletShield_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        }
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        QuickSave_ValueKey_String =
                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜1";
                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                        {
                            QuickSave_ValueKey_String =
                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                        }
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float +=//數值增加
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                    break;

                //消除效果
                case "Behavior_Stone_BreakBurst":
                    {
                        //消除效果
                        float QuickSave_Stack_Float = TargetSource.Source_BattleObject.
                            Key_Stack("Key", "EffectObject_Stone_StoneCarapace_0", _Basic_Source_Class, TargetSource, UsingObject);
                        _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Stack_Float);
                        if (TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(
                            "EffectObject_Stone_StoneCarapace_0", out _Effect_EffectObjectUnit Effect))
                        {
                            if (Action)
                            {
                                //數值設定
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                //資料提取
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_PercentageValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                Effect.StackDecrease("Set", Mathf.RoundToInt(QuickSave_Value_Float * QuickSave_PercentageValue_Float));
                            }
                        }
                    }
                    break;
                #endregion
                #region - Deal/Throw
                //自身抽卡
                case "Behavior_Symbiotic_SymbioticOffering":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(
                            _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                "Normal", Mathf.RoundToInt(QuickSave_Value_Float), UsingObject._Skill_Faction_Script.Cards(),
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;

                //自身抽卡-優先自身-給予卡牌效果-ScheduledItinerary
                case "Behavior_Profession_PlanSet":
                    {
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(
                            _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                "Priority", Mathf.RoundToInt(QuickSave_Value_Float), UsingObject._Skill_Faction_Script.Cards(),
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order));

                        //數值設定
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectCard_Profession_ScheduledItinerary_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        List<_UI_Card_Unit> QuickSave_DealCards_ScriptsList =
                            _Basic_Source_Class.Source_Creature._Basic_CardRecentDeal_ScriptsList;
                        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
                        foreach (_UI_Card_Unit Card in QuickSave_DealCards_ScriptsList)
                        {
                            Answer_Return_StringList.AddRange(_Effect_Manager.
                                GetEffectCard(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, Card._Basic_Source_Class,
                                HateTarget, Action, Time, Order));
                        }
                    }
                    break;
                //自身抽卡(消耗值)
                case "Behavior_Cuisine_InvigoratingAppetizer":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            Answer_Return_StringList.AddRange(
                                _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                    "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order));
                        }
                    }
                    break;
                #endregion
                #region - Shft -
                case "Behavior_投擲":
                    {
                        //彈飛目標(可能錯誤)
                        string QuickSave_ValueKey_String =
                            "Shift_Push_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string[] QuickSave_InteractiveSplit_String = QuickSave_Key_String.Split("_"[0]);
                        TargetSource.Source_BattleObject.
                            InteractiveSet(QuickSave_InteractiveSplit_String[0], _Basic_Source_Class, TargetSource,
                            UsingObject, HateTarget, Action, Time, Order,
                            ShiftType: QuickSave_InteractiveSplit_String[1],
                            ShiftRange: Mathf.RoundToInt(QuickSave_Value_Float),
                            ShiftCenter: _Basic_Source_Class.Source_BattleObject.TimePosition(Time, Order));
                    }
                    break;
                #endregion
                #region - Recover -
                //目標回復MediumPoint(回復值-精準量)
                case "Behavior_Tailor_FabricRepair":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cloth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜User_Concept_Default_Status_Precision_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_Key_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));
                        }
                    }
                    break;
                //目標回復MediumPoint(回復值-總介質值)
                case "Behavior_Chef_CuisineRemaking":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cuisine" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜Using_Default_Default_Point_MediumPoint_Total_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_Key_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));
                        }
                    }
                    break;
                //目標回復MediumPoint(消耗值)
                case "Behavior_EternalLighthouse_SelfRebirth":
                    {
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                            _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                            HateTarget, Action, Time, Order, Action));
                    }
                    break;
                case "Behavior_Cuisine_HealingMainCourse":
                case "Behavior_Cuisine_HerbDelightEat":
                case "Behavior_Medicine_HealingPotion":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));
                        }
                    }
                    break;
                case "Behavior_Medicine_HerbalSphereSmoke":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Sniff" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));
                        }
                    }
                    break;
                case "Behavior_Part_RepairPartRepair":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Wood|Metal)" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));
                        }
                    }
                    break;
                //目標回復MediumPoint(消耗值)並獲得效果-抗藥性
                case "Behavior_Medicine_HerbalSphereEat":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //數值設定
                            string QuickSave_ValueKey_String =
                                "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                            Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                HateTarget, Action, Time, Order, Action));

                            //數值設定
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Medicine_MedicineResistance_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                GetEffectObject(
                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, TargetSource,
                                HateTarget, Action, Time, Order));
                        }
                    }
                    break;
                //目標回復MediumPoint(消耗值)並獲得效果-FluorescentPoint(消耗值)
                case "Behavior_Cuisine_GlosporeBeverageEat":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            break;
                        }

                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                            _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                            HateTarget, Action, Time, Order, Action));

                        //資料提取
                        QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                        QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);
                        //數值設定
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, TargetSource,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                //目標回復MediumPoint(ScarletBunch加強)
                case "Behavior_ScarletKin_ScarletHeal":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Concept" };
                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            break;
                        }

                        //數值設定
                        string QuickSave_ValueKey_String =
                        "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_Key_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        QuickSave_ValueKey_String =
                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float +=
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_Key_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                            _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                            HateTarget, Action, Time, Order, Action));
                    }
                    break;
                //目標回復CatalystPoint(消耗值)
                case "Behavior_Malogic_MalogicRewrite":
                    {
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //數值設定
                        string QuickSave_ValueKey_String =
                            "HealNumber_CatalystPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, QuickSave_ValueData_Float,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                        string QuickSave_ValueKey02_String =
                            "HealTimes_CatalystPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                            _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                            HateTarget, Action, Time, Order, Action));
                    }
                    break;

                //淨化效果
                case "Behavior_ScarletKin_ScarletPurify":
                    {
                        //標籤判定(目標)
                        List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Concept" };
                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            break;
                        }
                        //數值設定
                        string QuickSave_ValueKey_String =
                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        foreach (_Effect_EffectObjectUnit Effect in TargetSource.Source_BattleObject._Effect_Effect_Dictionary.Values)
                        {
                            if (QuickSave_Value_Float <= 0)
                            {
                                break;
                            }
                            QuickSave_Tag_StringList =
                                TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                            QuickSave_CheckTag_StringList = new List<string> { "PolluteEffect" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                            }
                        }
                    }
                    break;
                #endregion
                #region - Pursuit -
                case "Behavior_Cuisine_FailCuisine":
                case "Behavior_Cuisine_MysteryCuisineEat":
                    {
                        //目標造成傷害
                        TargetSource.Source_BattleObject.
                            Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order), UsingObject,
                                HateTarget, Action, Time, Order);
                    }
                    break;
                #endregion

                #region - Special -
                //對自身以外所有驅動物追加傷害
                case "Behavior_Common_AwkwardSprint":
                    {
                        List<_Map_BattleObjectUnit> QuickSave_Object_ScriptsList =
                            new List<_Map_BattleObjectUnit>(
                            _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_DrivingObject_ScriptsList);
                        QuickSave_Object_ScriptsList.Remove(_Owner_Card_Script._Card_UseObject_Script);
                        foreach (_Map_BattleObjectUnit Object in QuickSave_Object_ScriptsList)
                        {
                            Answer_Return_StringList.AddRange(Object.
                                Damaged(Key_Pursuit(
                                    _Basic_Source_Class, Object._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order), UsingObject,
                                    HateTarget, Action, Time, Order));
                        }
                    }
                    break;
                    //複製卡片並給予效果
                case "Behavior_Object_DoublePrepare":
                    {
                        if (Action)
                        {
                            _UI_Card_Unit QuickSave_Card_Script =
                                _World_Manager._Skill_Manager.SkillLeafStartSet(
                                    _Owner_Faction_Script, _Owner_Card_Script._Basic_Key_String);
                            _Owner_Faction_Script.AddSkillLeaf(QuickSave_Card_Script, "Board", true);
                            _World_Manager._UI_Manager._UI_CardManager.
                                CardDeal("Target", 1, new List<_UI_Card_Unit> { QuickSave_Card_Script },
                                _Basic_Source_Class, _Basic_Source_Class, null,
                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        }
                    }
                    break;
                    #endregion
            }
            #region - Special -
            //Loading
            foreach (_Effect_EffectCardUnit Loading in _Owner_Card_Script._Effect_Loading_Dictionary.Keys)
            {
                if (Loading._Basic_Key_String + "_" + Loading.GetInstanceID() == Key)
                {
                    Answer_Return_StringList.AddRange(
                        Loading.Key_Effect(Key_Enchance(), _Owner_Card_Script,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                }
            }
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Attack -
    //傷害選擇——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Attack(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        //回傳值
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //行為攻擊傷害
        switch (Key)
        {
            default:
                switch (_Basic_Key_String)
                {
                    #region - Slash -
                    //基本-劈斬傷害
                    case "Behavior_Blade_SideCutting":
                    case "Behavior_Blade_BattoSan":
                    case "Behavior_Blade_UpwardSlash":
                    case "Behavior_Blade_DownwardSlash":
                    case "Behavior_Blade_CrossSlash":
                    case "Behavior_Blade_CrossRising":
                    case "Behavior_Blade_UpRising":
                    case "Behavior_Crescent_MoonDance":
                    case "Behavior_Vine_SweepingVine":
                    case "Behavior_FlashingTachi_NewSun":
                    case "Behavior_FlashingTachi_Twilight":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //劈斬傷害(觸媒增加)
                    case "Behavior_Blade_SwordWave":
                        {
                            //傷害
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            QuickSave_ValueKey01_String =
                                "Value_Default_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜1";
                            QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value01_Float +=//數值增加
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    #endregion
                    #region - Puncture -
                    //基本-穿刺傷害
                    case "Behavior_Sharp_SharpPuncture":
                    case "Behavior_Sharp_BackhandThrust":
                    case "Behavior_Fangs_FlightFangs":
                    case "Behavior_Fangs_ProbeBite":
                    case "Behavior_Arthropod_SwarmSurge":
                    case "Behavior_Stone_StalagmiteArrow":
                    case "Behavior_Stone_ShatterstoneVolley":
                    case "Behavior_Vine_VineTwist":
                    case "Behavior_Fluorescent_ProvokeFluorescent":
                    case "Behavior_Phantom_PhantomBug":
                    case "Behavior_OrganicGauntlets_DevourFinger":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //穿刺傷害(觸媒增加)
                    case "Behavior_Sharp_PunctureWave":
                        {
                            //傷害
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            QuickSave_ValueKey01_String =
                                "Value_Default_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜1";
                            QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value01_Float +=//數值增加
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //消耗額-穿刺傷害
                    case "Behavior_Arthropod_PhototropismSacrificeShooting":
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //傷害設置
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, QuickSave_ValueData_Float,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //穿刺傷害-目標持有Tag(Stone)提升傷害
                    case "Behavior_Miner_AccurateMine":
                        {
                            List<string> QuickSave_Tag_StringList =
                                TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                            if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey01_String =
                                "AttackNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                            else
                            {
                                string QuickSave_ValueKey01_String =
                                "AttackNumber_PunctureDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_PunctureDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                        }
                        break;
                    #endregion
                    #region - Impact -
                    //基本-衝擊傷害
                    case "Behavior_Flail_StarShake":
                    case "Behavior_Flail_MeteorSpin":
                    case "Behavior_Flail_CometStrike":
                    case "Behavior_Scabbard_FlawStrike":
                    case "Behavior_Doll_ContentsStrike":
                    case "Behavior_Doll_ContentsFall":
                    case "Behavior_Tenticle_TenticleFlog":
                    case "Behavior_Tenticle_TenticleFlagellate":
                    case "Behavior_Tenticle_TenticleCoerce":
                    case "Behavior_Guard_ProbeStrike":
                    case "Behavior_Guard_FlawStrike":
                    case "Behavior_Guard_DelayingBind":
                    case "Behavior_Maid_SpinCleanse":
                    case "Behavior_Frank_ProbeTouch":
                    case "Behavior_Cleaning_EnvironmentClean":
                    case "Behavior_Fighting_ProbeTap":
                    case "Behavior_Fighting_SneakTap":
                    case "Behavior_Fighting_SuccessiveTap":
                    case "Behavior_Fighting_DestructionPunch":
                    case "Behavior_Brutal_BrutalDash":
                    case "Behavior_Stone_WhirlingStone":
                    case "Behavior_Stone_RockSpring":
                    case "Behavior_Rope_ReverseFlog":
                    case "Behavior_Rope_FlogSwirl":
                    case "Behavior_Rope_StiffWhip":
                    case "Behavior_Rope_FearWhip":
                    case "Behavior_GatherCobble_SatelliteBullet":
                    case "Behavior_GatherCobble_CometRain":
                    case "Behavior_Limu_LimuCatch":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            if(!_Basic_Data_Class.Numbers.ContainsKey(QuickSave_ValueKey01_String))
                            {
                                print(QuickSave_ValueKey01_String);
                            }
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //衝擊傷害-使用者重量係數
                    case "Behavior_Frank_FrankSwoop":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type0｜Using_Default_Default_Material_Weight_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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

                    //衝擊傷害(重量增加)
                    case "Behavior_Stone_BoulderOppression":
                        {
                            //傷害
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            QuickSave_ValueKey01_String =
                                "Value_Default_Default｜Using_Default_Default_Material_Weight_Default_Default｜0";
                            QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value01_Float +=//數值增加
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //衝擊傷害-ScarletBunch效果增強
                    case "Behavior_ScarletKin_ScarletBruteCharge":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            QuickSave_ValueKey01_String =
                                "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                            QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value01_Float +=
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //衝擊傷害-目標持有Tag(Stone)提升傷害
                    case "Behavior_Miner_StoneBreaker":
                        {
                            List<string> QuickSave_Tag_StringList =
                                TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                            if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                            else
                            {
                                string QuickSave_ValueKey01_String =
                                "AttackNumber_ImpactDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_ImpactDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                        }
                        break;
                    //衝擊傷害-隨Effect(EffectObject_Stone_StoneCarapace_0)提升，觸發時增強
                    case "Behavior_Stone_RollingStone":
                        {
                            if (!_World_Manager._Map_Manager._State_Reacting_Bool)
                            {
                                string QuickSave_ValueKey01_String =
                                    "AttackNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                QuickSave_ValueKey01_String =
                                "Value_Default_Default｜Using_Default_Default_Stack_Default_Default_EffectObject_Stone_StoneCarapace_0｜0";
                                QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value01_Float +=
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                            else
                            {
                                string QuickSave_ValueKey01_String =
                                    "AttackNumber_ImpactDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                QuickSave_ValueKey01_String =
                                "Value_Default_Default｜Using_Default_Default_Stack_Default_Default_EffectObject_Stone_StoneCarapace_0｜1";
                                QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey01_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value01_Float +=
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "AttackTimes_ImpactDamage_Type1｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_ValueKey02_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];

                                Answer_Return_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                            }
                        }
                        break;
                    #endregion
                    #region - Energy -
                    //基本-能量傷害
                    case "Behavior_Common_DominateEnergyDetonation":
                    case "Behavior_Quality_SelfEnergyDetonation":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    #endregion
                    #region - Chaos -
                    //基本-渾沌傷害
                    case "Behavior_Ink_InkVolley":
                    case "Behavior_Ink_InkBurst":
                    case "Behavior_Ink_InkSwirl":
                    case "Behavior_Toxic_PoisonRelease":
                    case "Behavior_Fluorescent_FluorescentMark":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    //消耗額-渾沌傷害
                    case "Behavior_Cuisine_MysteryCuisineSplash":
                        {
                            //資料提取
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);
                            //傷害設置
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, QuickSave_ValueData_Float,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                    #endregion
                    #region - Abstract -
                    //抽象傷害
                    case "Behavior_Light_LightFlash":
                    case "Behavior_LanspidDagger_TransparentBlade":
                        {
                            string QuickSave_ValueKey01_String =
                                "AttackNumber_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key01_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey01_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value01_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_ValueKey02_String =
                                "AttackTimes_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                            string QuickSave_Key02_String =
                                _World_Manager.Key_KeysUnit("Default",
                                QuickSave_ValueKey02_String,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value02_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_AttackType_String =
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
                        #endregion
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //傷害選擇——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_AttackAdden(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time , int Order)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        //回傳值
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        //變數
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        #region - Behavior -
        //行為攻擊傷害
        switch (_Basic_Key_String)
        {
            #region - Puncture -
            //追加穿刺傷害-目標持有效果Tag(Constrain)
            case "Behavior_Vine_VineTwist":
                {
                    if (TargetSource.Source_BattleObject.Key_Stack("Tag", "Constrain",
                        UserSource, TargetSource, UsingObject) > 0)
                    {
                        string QuickSave_ValueKey01_String =
                            "PursuitNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });
                    }
                }
                break;
            //追加穿刺傷害-持有效果(EffectObject_Arthropod_SwarmEggs_0時消耗層數)
            case "Behavior_Arthropod_SwarmSurge":
                {
                    if (UsingObject.Key_Stack("Key", "EffectObject_Arthropod_SwarmEggs_0",
                        UserSource, TargetSource, UsingObject) > 0)
                    {
                        string QuickSave_ValueKey01_String =
                            "PursuitNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });

                        if (Action)
                        {
                            //層數減少
                            if (UsingObject._Effect_Effect_Dictionary.TryGetValue(
                                "EffectObject_Arthropod_SwarmEggs_0", out _Effect_EffectObjectUnit Value))
                            {
                                Value.StackDecrease("Set", 1);
                            }
                        }
                    }
                }
                break;
            //追加穿刺傷害-持有效果(Tag(Filth))
            case "Behavior_Maid_SpinCleanse":
                {
                    if (UsingObject.Key_Stack("Tag", "Filth",
                        UserSource, TargetSource, UsingObject) > 0)
                    {
                        string QuickSave_ValueKey01_String =
                            "PursuitNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });
                        List<_Effect_EffectObjectUnit> QuickSave_Effects_ScriptsList =
                            new List<_Effect_EffectObjectUnit>(UsingObject._Effect_Effect_Dictionary.Values);
                        foreach (_Effect_EffectObjectUnit Effect in QuickSave_Effects_ScriptsList)
                        {
                            //層數減少
                            List<string> QuickSave_Tag_StringList =
                                Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Filth" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Effect.StackDecrease("Set", 65535);
                            }
                        }
                    }
                }
                break;
            #endregion
            #region - Impact -
            //追加衝擊傷害-持有效果(EffectObject_GatherCobble_CometBullets_0時消耗層數)-參考使用者介質
            case "Behavior_GatherCobble_SatelliteBullet":
                {
                    if (UsingObject.Key_Stack("Key", "EffectObject_GatherCobble_CometBullets_0",
                        UserSource, TargetSource, UsingObject) > 0)
                    {
                        if (Action)
                        {
                            //層數減少
                            if (UsingObject._Effect_Effect_Dictionary.TryGetValue(
                                "EffectObject_GatherCobble_CometBullets_0", out _Effect_EffectObjectUnit Value))
                            {
                                Value.StackDecrease("Set", 1);
                            }
                        }

                        string QuickSave_ValueKey01_String =
                            "PursuitNumber_ImpactDamage_Type0｜Using_Default_Default_Status_Medium_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });
                    }
                }
                break;
            //追加衝擊傷害-持有效果(EffectObject_GatherCobble_CometBullets_0時移除效果)-參考使用者介質
            case "Behavior_GatherCobble_CometRain":
                {
                    float QuickSave_Stack_Float = 
                        UsingObject.Key_Stack("Key", "EffectObject_GatherCobble_CometBullets_0",
                        UserSource, TargetSource, UsingObject);
                    if (!(QuickSave_Stack_Float > 0))
                    {
                        break;
                    }
                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Stack_Float);
                    if (Action)
                    {
                        //層數減少
                        if (UsingObject._Effect_Effect_Dictionary.TryGetValue(
                            "EffectObject_GatherCobble_CometBullets_0", out _Effect_EffectObjectUnit Value))
                        {
                            Value.StackDecrease("Set", 65535);
                        }
                    }

                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ImpactDamage_Type0｜Using_Default_Default_Status_Medium_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    //資料提取
                    string QuickSave_PercentageValueKey_String =
                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_PercentageKey_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_PercentageValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_PercentageValue_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    float QuickSave_ValueData_Float =
                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String] * QuickSave_ValueData_Float,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);


                    string QuickSave_AttackType_String =
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
            #endregion
            #region - Energy -
            //追加能量傷害-觸發時
            case "Behavior_FlashingTachi_NewSun":
                {
                    if (!_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        break;
                    }
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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
            #endregion
            #region - Abstract -
            //追加抽象傷害
            case "Behavior_Doll_ContentsStrike":
            case "Behavior_Doll_ContentsFall":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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
            //追加抽象傷害(Hide層數)
            case "Behavior_LanspidDagger_TransparentBlade":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    QuickSave_ValueKey01_String =
                        "Value_Default_Default｜Using_Default_Default_Stack_Default_Default_EffectObject_Hide_Hide_0｜0";
                    QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    QuickSave_Value01_Float +=
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_AbstractDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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
            #endregion
            #region - Stark -
            //追加絕對傷害
            case "Behavior_Cuisine_MysteryCuisineEat":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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
            #endregion
            #region - Special -
            //依層數變化攻擊類型
            case "Behavior_OrganicGauntlets_DevourFinger":
                {
                    int QuickSave_Stack_Int =
                        UsingObject.Key_Stack("Key", "EffectObject_OrganicGauntlets_HungryTouch_0",
                        UserSource, TargetSource, UsingObject);
                    if (QuickSave_Stack_Int > 0)
                    {
                        string QuickSave_Type_String = "Puncture";
                        switch (QuickSave_Stack_Int)
                        {
                            case 1:
                                QuickSave_Type_String = "Slash";
                                break;
                            case 3:
                                QuickSave_Type_String = "Impact";
                                break;
                            case 4:
                                QuickSave_Type_String = "Energy";
                                break;
                            case 5:
                                QuickSave_Type_String = "Abstract";
                                break;
                        }

                        string QuickSave_ValueKey01_String =
                        "PursuitNumber_"+ QuickSave_Type_String + "Damage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_"+ QuickSave_Type_String + "Damage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });
                    }
                }
                break;
                #endregion
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //追加傷害(並非附加於攻擊之上(如因效果傷害自身))——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Pursuit(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)//附魔對象類型
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            //衝擊傷害
            case "Behavior_Common_AwkwardSprint":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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

            //Mouth時追加傷害
            case "Behavior_Cuisine_FailCuisine":
            case "Behavior_Cuisine_MysteryCuisineEat":
                {
                    //標籤判定(目標)
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //資料提取
                        string QuickSave_PercentageValueKey_String =
                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_PercentageKey_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_PercentageValueKey_String,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_PercentageValue_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        float QuickSave_ValueData_Float =
                            _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                        //傷害設置
                        string QuickSave_ValueKey01_String =
                            "PursuitNumber_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key01_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey01_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value01_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key01_String, QuickSave_ValueData_Float,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "PursuitTimes_ChaosDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default",
                            QuickSave_ValueKey02_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_AttackType_String =
                            QuickSave_ValueKey01_String.Split("_"[0])[1];

                        Answer_Return_ClassList.Add(new DamageClass
                        {
                            Source = _Basic_Source_Class,
                            DamageType = QuickSave_AttackType_String,
                            Damage = QuickSave_Value01_Float,
                            Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                        });
                    }
                }
                break;

            case "Behavior_投擲":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey01_String,
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_ImpactDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key02_String =
                        _World_Manager.Key_KeysUnit("Default",
                        QuickSave_ValueKey02_String,
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    float QuickSave_Value02_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                    string QuickSave_AttackType_String =
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
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //使用原本數值
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    public List<string> Key_Damage(string DamageType, float Value, bool IsSuceess,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Effect -
            //成功命中-給予目標效果-僵直
            case "Behavior_Blade_BattoSan":
            case "Behavior_Sharp_SharpPuncture":
            case "Behavior_Sharp_BackhandThrust":
            case "Behavior_Flail_MeteorSpin":
            case "Behavior_Flail_CometStrike":
            case "Behavior_Cuisine_FailCuisine":
            case "Behavior_Guard_DelayingBind":
            case "Behavior_Rope_StiffWhip":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-僵直(觸發變動)
            case "Behavior_Scabbard_FlawStrike":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    if (_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜1";
                    }
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-僵直、彈飛
            case "Behavior_Stone_RockSpring":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));

                    //彈飛目標(可能錯誤)
                    QuickSave_ValueKey_String =
                        "Shift_Push_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string[] QuickSave_InteractiveSplit_String = QuickSave_Key_String.Split("_"[0]);
                    TargetSource.Source_BattleObject.
                        InteractiveSet(QuickSave_InteractiveSplit_String[0],
                        _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order,
                        ShiftType: QuickSave_InteractiveSplit_String[1],
                        ShiftRange: Mathf.RoundToInt(QuickSave_Value_Float),
                        ShiftCenter: UserSource.Source_BattleObject.TimePosition(Time, Order));
                }
                break;
            //成功命中-給予目標效果-恐懼
            case "Behavior_Tenticle_TenticleCoerce":
            case "Behavior_Rope_FearWhip":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Sensation_Fear_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-騰空
            case "Behavior_Blade_UpRising":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Soar_Soar_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;

            //成功命中-給予目標效果-傷口穿刺(效果Tag(Constrain)加強)
            case "Behavior_Vine_VineTwist":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Quality_PunctureWound_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    if (TargetSource.Source_BattleObject.Key_Stack("Tag", "Constrain",
                            UserSource, TargetSource, UsingObject) > 0)
                    {
                        QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Quality_PunctureWound_0｜Value_Default_Default_Default_Default_Default_Default｜1";
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                        QuickSave_Value_Float +=
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    }
                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-毒素
            case "Behavior_Toxic_PoisonRelease":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Quality_Toxin_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-閃光症
            case "Behavior_Light_LightFlash":
            case "Behavior_LanspidDagger_TransparentBlade":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Light_FlashDisease_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-光點/光標記(Tag(Fluorescent))
            case "Behavior_Fluorescent_FluorescentMark":
                {
                    //命中判定
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //給予目標效果
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Fluorescent" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Fluorescent_FluorescentMark_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    }
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-給予目標效果-InkSpray
            case "Behavior_Ink_InkVolley":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Ink_InkSpray_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //給予目標效果-InkSpray
            case "Behavior_Ink_InkBurst":
            case "Behavior_Ink_InkSwirl":
                {
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Ink_InkSpray_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            //成功命中-移除目標效果-Tag(Filth)
            case "Behavior_Cleaning_EnvironmentClean":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                    {
                        //層數減少
                        List<string> QuickSave_Tag_StringList =
                            Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Filth" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            if (Action)
                            {
                                Effect.StackDecrease("Set", 65535);
                            }
                        }
                    }
                }
                break;
            //成功命中-給予目標效果-CarvingOfSun
            case "Behavior_FlashingTachi_Twilight":
                {
                    if (!IsSuceess)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_FlashingTachi_CarvingOfSun_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(
                        _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, TargetSource,
                        HateTarget, Action, Time, Order));
                }
                break;
            #endregion

            #region - Deal/Throw -
            //目標持有Tag(Fluorescent)抽卡
            case "Behavior_Arthropod_PhototropismSacrificeShooting":
                {
                    //次數限制
                    if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1))
                    {
                        break;
                    }
                    //標籤判定
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Fluorescent" };
                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        break;
                    }
                    //數值設定
                    string QuickSave_ValueKey_String =
                        "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);

                    Answer_Return_StringList.AddRange(
                        _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                            "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order));
                }
                break;
            //目標丟卡
            case "Behavior_Guard_FlawStrike":
                {
                    //僅以概念為目標
                    if (TargetSource.Source_Concept == null)
                    {
                        break;
                    }
                    //次數限制(目標觸發次數)
                    if (!TargetSource.Source_BattleObject.
                        _Basic_TimesLimit_Class.TimesLimit("Round", 1, _Basic_Key_String))
                    {
                        break;
                    }
                    //數值設定
                    string QuickSave_ValueKey_String =
                        "Throw_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    Answer_Return_StringList.AddRange(
                        _World_Manager._UI_Manager._UI_CardManager.CardThrow(
                            "Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                            _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region- Key_Value -
    //數值-特殊情況用，如Loading增加傷害
    public float Key_Effect_AttackAdd(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Behavior_Throw_ProjectilePop_1":
            case "Behavior_Throw_ProjectileFire_1":
            case "Behavior_Bow_ArrowShooting_1":
            case "Behavior_Shooter_StraightBlow_1":
            case "Behavior_Shooter_ChargeBlow_1":
            case "Behavior_Stone_RockFort_1":
                {
                    //數值設定
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    Answer_Return_Float += QuickSave_Value_Float;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Float;
        //----------------------------------------------------------------------------------------------------
    }

    public float Key_Effect_AttackMultiply(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 1;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Float;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Key_Delete -
    //刪除事件——————————————————————————————————————————————————————————————————————
    public void Key_Deleted()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion KeyAction
}
