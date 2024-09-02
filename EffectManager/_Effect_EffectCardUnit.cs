using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class _Effect_EffectCardUnit : MonoBehaviour
{
    #region Element
    //變數集——————————————————————————————————————————————————————————————————————
    //載入變數----------------------------------------------------------------------------------------------------
    //編號
    public string _Basic_Key_String;
    public string _Basic_Type_String;
    [HideInInspector] public SourceClass _Basic_Source_Class;
    //數據資料
    [HideInInspector] public _Effect_Manager.EffectDataClass _Basic_Data_Class;
    //語言資料
    [HideInInspector] public LanguageClass _Basic_Language_Class;
    public _Skill_Manager.RangeDataClass _Basic_Range_Class;
    //----------------------------------------------------------------------------------------------------

    //個體差異----------------------------------------------------------------------------------------------------
    //持有者
    public _UI_Card_Unit _Basic_Owner_Script;
    public _Map_BattleObjectUnit _Basic_EnchanceUseObject_Script;
    //氣泡位置
    public Transform _Effect_Bubble_Transform;
    //當前層數
    public int _Effect_Stack_Int;
    //當前衰退
    public int _Effect_Decay_Int;
    //暫存集
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------

    //顯示----------------------------------------------------------------------------------------------------
    //效果圖示
    public Image _Effect_Icon_Image;
    //層數
    public Text _Effect_Stack_Text;
    //衰退圖示尺寸
    public Transform _Effect_Round_Tranform;
    //----------------------------------------------------------------------------------------------------
    #endregion Element


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart(_UI_Card_Unit Owner, string Key, _Skill_EnchanceUnit EnchancePlace = null)
    {
        //取得資料----------------------------------------------------------------------------------------------------
        string QS_Type_String = Key.Split("_"[0])[0];
        SourceClass QuickSave_OwnerSource_Class = Owner._Basic_Source_Class;

        _Basic_Owner_Script = Owner;
        _Basic_Key_String = Key;
        _Basic_Type_String = QS_Type_String;
        _Effect_Icon_Image.sprite = 
            _World_Manager._View_Manager.GetSprite(QS_Type_String, Key);
        switch (QS_Type_String)
        {
            case "Enchance":
                {
                    _Basic_EnchanceUseObject_Script = EnchancePlace._Owner_Card_Script._Card_UseObject_Script;
                    _Skill_EnchanceUnit QuickSave_Enchance_Script = EnchancePlace;
                    _Skill_Manager.EnchanceDataClass QuickSave_EnchanceData_Class =
                        QuickSave_Enchance_Script._Basic_Data_Class;
                    _Basic_Data_Class = new _Effect_Manager.EffectDataClass
                    {
                        EffectTag = new List<string> { QS_Type_String },
                        Decay = "Forever",
                        StackLimit = 1,
                        DecayTimes = 1,
                        Value = 0,
                        Numbers = QuickSave_EnchanceData_Class.Numbers,
                        Keys = QuickSave_EnchanceData_Class.Keys
                    };
                    _Basic_Language_Class = QuickSave_Enchance_Script._Basic_Language_Class;
                    _Basic_Range_Class.Range = QuickSave_Enchance_Script._Basic_Data_Class.Range;
                    _Basic_Range_Class.Path = QuickSave_Enchance_Script._Basic_Data_Class.Path;
                    _Basic_Range_Class.Select = QuickSave_Enchance_Script._Basic_Data_Class.Select;
                    _Basic_Source_Class = new SourceClass
                    {
                        SourceType = QS_Type_String,
                        Source_EffectCard = this,
                        Source_Creature = QuickSave_OwnerSource_Class.Source_Creature,
                        Source_BattleObject = QuickSave_OwnerSource_Class.Source_BattleObject,
                        Source_Card = Owner,
                        Source_NumbersData = _Basic_Data_Class.Numbers,
                        Source_KeysData = _Basic_Data_Class.Keys
                    };
                    //層數
                    _Effect_Stack_Text.text = "E";
                }
                break;
            case "EffectCard":
                {
                    _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;

                    _Basic_Data_Class = _Effect_Manager._Data_EffectCard_Dictionary[Key];
                    _Basic_Language_Class = _Effect_Manager._Language_EffectCard_Dictionary[Key];

                    _Basic_Source_Class = new SourceClass
                    {
                        SourceType = "EffectObject",
                        Source_EffectCard = this,
                        Source_Creature = QuickSave_OwnerSource_Class.Source_Creature,
                        Source_BattleObject = QuickSave_OwnerSource_Class.Source_BattleObject,
                        Source_Card = Owner,
                        Source_NumbersData = _Basic_Data_Class.Numbers,
                        Source_KeysData = _Basic_Data_Class.Keys
                    };
                    if (_Basic_Data_Class.Decay == "Sequence")
                    {
                        _World_Manager._Map_Manager._Map_BattleRound._Round_SequenceEffectCard_ScriptsList.Add(this);
                    }
                    Owner._Effect_Effect_ScriptsList.Add(this);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //設定變數----------------------------------------------------------------------------------------------------
        _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager._Map_BattleRound.
            _Round_TimesLimits_ClassList.Add(_Basic_TimesLimit_Class);
        Key_Effect_OwnStart();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region Math
    #region - Stack -
    //層數增加——————————————————————————————————————————————————————————————————————
    public void StackIncrease(int Number)
    {
        //數值變動----------------------------------------------------------------------------------------------------
        _Effect_Stack_Int += Number;
        //數值變動
        if (_Effect_Stack_Int > _Basic_Data_Class.StackLimit)
        {
            _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
        }
        _Effect_Stack_Int = Mathf.Clamp(_Effect_Stack_Int, 0, _Basic_Data_Class.StackLimit);
        //----------------------------------------------------------------------------------------------------

        //更新數值----------------------------------------------------------------------------------------------------
        //視覺更新
        ViewSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //層數增加——————————————————————————————————————————————————————————————————————
    public void StackDecrease(string Type, int Number)
    {
        //數值變動----------------------------------------------------------------------------------------------------
        _Effect_Stack_Int -= Number;
        _Effect_Stack_Int = Mathf.Clamp(_Effect_Stack_Int, 0, _Basic_Data_Class.StackLimit);
        //----------------------------------------------------------------------------------------------------

        //更新數值----------------------------------------------------------------------------------------------------
        //視覺更新
        switch (_Basic_Type_String)
        {
            case "EffectCard":
                ViewSet();
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //減少情況----------------------------------------------------------------------------------------------------
        //數值變動
        if (_Effect_Stack_Int <= 0)
        {
            switch (Type)
            {
                case "Set":
                    break;
                default:
                    break;
            }
            Destroy();
        }
        //----------------------------------------------------------------------------------------------------
    }

    public void Destroy()
    {
        //----------------------------------------------------------------------------------------------------
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Key_Effect_OwnEnd();
        switch (_Basic_Type_String)
        {
            case "Enchance":
                _Basic_Owner_Script._Effect_Enchance_ScriptsList.Remove(this);
                break;
            case "EffectCard":
                _Basic_Owner_Script._Effect_Effect_ScriptsList.Remove(this);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Basic_Data_Class.Decay == "Sequence")
        {
            _World_Manager._Map_Manager._Map_BattleRound._Round_SequenceEffectCard_ScriptsList.Remove(this);
        }
        Destroy(this.gameObject);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    #endregion

    #region - Round -
    //衰退變更——————————————————————————————————————————————————————————————————————
    public void RoundDecrease(int Number)
    {
        //數值變動----------------------------------------------------------------------------------------------------
        _Effect_Decay_Int -= Number;
        _Effect_Decay_Int = Mathf.Clamp(_Effect_Decay_Int, 0, _Basic_Data_Class.DecayTimes);
        //小額判定
        if (_Effect_Decay_Int == 0)
        {
            StackDecrease("RoundDecrease", 1);
            return;
        }
        //----------------------------------------------------------------------------------------------------

        //更新數值----------------------------------------------------------------------------------------------------
        //視覺更新
        ViewSet();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Math
    #region View
    public void ViewSet()
    {
        //層數
        _Effect_Stack_Text.text = _Effect_Stack_Int.ToString();
        //衰退
        float YSize = 1;
        if (_Basic_Data_Class.DecayTimes > 0)
        {
            YSize = Mathf.Clamp(1.0f - ((float)_Effect_Decay_Int / _Basic_Data_Class.DecayTimes), 0, 1);
        }
        _Effect_Round_Tranform.localScale = new Vector3(1, YSize, 1);
    }
    #endregion View

    #region KeyAction
    #region - Scene -
    #region - Key_Effect_Own -
    //效果獲得/與層數(Stack)無關——————————————————————————————————————————————————————————————————————
    public void Key_Effect_OwnStart()
    {
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Key_Effect_OwnEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_EasilyForgotten_0":
                            {
                                if (_Basic_Owner_Script._Card_NowPosition_String != "Board")
                                {
                                    break;
                                }
                                List<_UI_Card_Unit> QuickSave_ThrowTarget_ScriptsList = new List<_UI_Card_Unit>();
                                QuickSave_ThrowTarget_ScriptsList.Add(_Basic_Owner_Script);
                                _World_Manager._UI_Manager._UI_CardManager
                                    .CardThrow("Target", 0, QuickSave_ThrowTarget_ScriptsList,
                                    _Basic_Source_Class, _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #region - Key_Effect_Field -
    //——————————————————————————————————————————————————————————————————————
    public void Key_Effect_FieldStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Key_Effect_FieldEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #region - Key_Effect_Battle -
    //——————————————————————————————————————————————————————————————————————
    public void Key_Effect_BattleStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void Key_Effect_BattleEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_FightingFantasy_0":
                            {
                                _World_Manager._UI_Manager._UI_CardManager.
                                    CardsMove("Board_End_Cemetery",
                                    _Basic_Owner_Script._Basic_Source_Class.Source_Creature,
                                    new List<_UI_Card_Unit> { _Basic_Owner_Script },
                                    _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                StackDecrease("Set", 65535);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region - Status -
    #region - Key_Effect_GetStatusValue -
    public List<string> Key_Effect_GetStatusValueAdd(List<string> Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 0;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key[0])
        {
            #region - Battle -
            case "AttackNumber":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                case "Enchance_Common_SizeForce":
                                case "Enchance_Common_FormForce":
                                case "Enchance_Common_WeightForce":
                                case "Enchance_Common_PurityForce":
                                    {
                                        string QuickSave_EasyKey_String = _Basic_Key_String.Replace("Enchance_Common_", "").Replace("Force", "");
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Material_" + QuickSave_EasyKey_String + "_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;

                                case "Enchance_Common_MediumForce":
                                case "Enchance_Common_CatalystForce":
                                case "Enchance_Common_ConsciousnessForce":
                                case "Enchance_Common_VitalityForce":
                                case "Enchance_Common_StrengthForce":
                                case "Enchance_Common_PrecisionForce":
                                case "Enchance_Common_SpeedForce":
                                case "Enchance_Common_LuckForce":
                                    {
                                        string QuickSave_EasyKey_String = _Basic_Key_String.Replace("Enchance_Common_", "").Replace("Force", "");
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Status_" + QuickSave_EasyKey_String + "_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Common_DominateForce":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Point_ConsciousnessPoint_Point_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Common_AutonomyForce":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Target_Default_Default_Point_ConsciousnessPoint_Point_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                case "Enchance_Identity_PurelyStrength":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Status_Strength_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Survival_Odyssey":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Fighting_FightingEnhancement":
                                    {
                                        //增加數值
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
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Spin_SpinForce":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Using_Default_Default_Stack_Default_Default_EffectObject_Spin_Spin_0｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_ScarletKin_ScarletForce":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;
                                case "Enchance_Meka_AttackAlgorithm":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Data_CardsCount_Board_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float = QuickSave_Value_Float;
                                    }
                                    break;

                                case "Enchance_Float_CurrentForce":
                                    {
                                        //物件檢查
                                        Vector QuickSave_Pos_Class =
                                            UsingObject.TimePosition(Time, Order);
                                        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                            _World_Manager._Object_Manager.
                                            TimeObjects("Normal", UserSource,
                                            Time, Order, QuickSave_Pos_Class);
                                        _Map_BattleObjectUnit QuickSave_Check_Script = null;
                                        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                        {
                                            if (Object._Basic_Key_String == "Object_OceanCurrent_Normal")
                                            {
                                                QuickSave_Check_Script = Object;
                                                break;
                                            }
                                        }
                                        if (QuickSave_Check_Script == null)
                                        {
                                            //增加數值
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
                                            Answer_Return_Float = QuickSave_Value_Float;
                                        }
                                        else
                                        {
                                            _UI_Card_Unit QuickSave_Card_Script = UserSource.Source_Card;
                                            int QuickSave_Direction_Int =
                                                QuickSave_Card_Script._Range_UseData_Class.Select[0].Direction;
                                            if (QuickSave_Direction_Int == 
                                                QuickSave_Check_Script._Basic_Status_Dictionary["ComplexPoint"])//相同
                                            {
                                                //增加數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                Answer_Return_Float = QuickSave_Value_Float;
                                            }
                                            else if ((QuickSave_Direction_Int + 
                                                QuickSave_Check_Script._Basic_Status_Dictionary["ComplexPoint"])%2 == 0)//相反
                                            {
                                                //增加數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                Answer_Return_Float = QuickSave_Value_Float;
                                            }
                                            else
                                            {
                                                //增加數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject,
                                                    HateTarget, Action, Time, Order);
                                                Answer_Return_Float = QuickSave_Value_Float;
                                            }
                                            if (Action)
                                            {
                                                //破壞物件
                                                QuickSave_Check_Script.PointSet(
                                                    "Set", "MediumPoint",
                                                    -65535, 1,
                                                    _Basic_Source_Class, UsingObject,
                                                    HateTarget, Action, Time, Order, Action);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetStatusValueMultiply(List<string> Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 1;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key[0])
        {
            #region - Battle -
            case "Consume":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Item -
                                case "Enchance_Item_IncreaseDosage":
                                    {
                                        //增加數值
                                        string QuickSave_ValueKey_String =
                                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float *= 1 + QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Key_Effect_GetEnchanceValue -
    public List<string> Key_Effect_GetEnchanceValueAdd(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetEnchanceValueMultiply(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Key_EffectObject -
    public List<string> Key_Effect_GetEffectValueAdd(string Type, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetEffectValueMultiply(string Type, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 1;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_LostEffectValueAdd(string Type, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                }
                break;
            case "Card":
                switch (_Basic_Type_String)
                {
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_LostEffectValueMultiply(string Type, string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 1;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Enchance_Spin_FreeFlow":
                                    {
                                        //執行判定
                                        if (!Action)
                                        {
                                            break;
                                        }
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                            TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                            Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Spin" };
                                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            break;
                                        }
                                        //機率發動
                                        string QuickSave_ValueKey_String =
                                            "Probability_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);

                                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                        {
                                            Answer_Return_Float *= 0;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Key_Effect_GetStackValue -
    public List<string> Key_Effect_GetStackValueAdd(string Type, string KeyTag,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Tag":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
            case "Key":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetStackValueMultiply(string Type, string KeyTag,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        float Answer_Return_Float = 1;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Tag":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
            case "Key":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - ColliderCheck -
    public List<string> Key_Effect_IsColliderEnterCheck(string Type, bool NowState,
        SourceClass UserSource/*進入者*/, SourceClass TargetSource/*被進入者*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
            case "Stay":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse｜" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_IsColliderEnteredCheck(string Type, bool NowState,
        SourceClass UserSource/*進入者*/, SourceClass TargetSource/*被進入者*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
            case "Stay":
                {
                    switch (_Basic_Type_String)
                    {
                        case "Enchance":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectCard":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse｜" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - StateAction -
    public List<string> Key_Effect_IsReachCheck(int DriveDistance, bool NowState,
        SourceClass UserSource/*進入者*/, SourceClass TargetSource/*被進入者*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse｜" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_IsCarryCheck(int DriveDistance, bool NowState,
        SourceClass UserSource/*進入者*/, SourceClass TargetSource/*被進入者*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse｜" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Key_Effect_GetTag -
    public List<string> Key_Effect_GetTagAdd(string Type,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Tag"://物件
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "OwnTag"://卡片
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetTagRemove(string Type,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)//目標(EX:RequireTag)
        {
            case "Tag":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Key_Effect_KeyChange -
    public List<string> Key_Effect_KeyChange(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        string Answer_Return_String = Key;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Rope_Razor":
                            {
                                Answer_Return_String = Answer_Return_String.Replace("ImpactDamage", "SlashDamage");
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Key｜" + Answer_Return_String);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #endregion

    #region - Action -
    #region - State -
    public List<string> Key_Effect_Drive(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();//EX:
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Abandon(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();//EX:
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Get/LostEffect -    
    public List<string> Key_Effect_GetEffect(string Type, string Key, int BeforeStack, int IncreaseStack,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_LostEffect(string Type, string Key, int BeforeStack, int DecreaseStack,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_CauseGetEffect(string Type, string Key, int BeforeStack, int IncreaseStack,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_CauseLostEffect(string Type,string Key, int BeforeStack, int IncreaseStack,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "Enchance":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectCard":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }

    #endregion
    #region - Card -    
    public List<string> Key_Effect_CardsMove(string Key,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();//EX:
        List<string> QuickSave_Split_StringList = new List<string>(Key.Split("_"[0]));
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Meka_DirectiveLoop":
                            {
                                if (QuickSave_Split_StringList[0] == "Delay")
                                {
                                    if (QuickSave_Split_StringList[1] == "End")
                                    {
                                        if (QuickSave_Split_StringList[2] == "Cemetery")
                                        {
                                            QuickSave_Split_StringList[2] = "Board";
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_EngravedIntoSoul_0":
                            {
                                if (QuickSave_Split_StringList[2] == "Exiled")
                                {
                                    QuickSave_Split_StringList[2] = "Cemetery";
                                }
                            }
                            break;
                        case "EffectCard_Common_DailyInstinct_0":
                            {
                                if (TargetSource.SourceType == "Explore")
                                {
                                    if (QuickSave_Split_StringList[0] == "Board")
                                    {
                                        if (QuickSave_Split_StringList[1] == "Use")
                                        {
                                            if (QuickSave_Split_StringList[2] == "Cemetery")
                                            {
                                                QuickSave_Split_StringList[2] = "Board";
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "EffectCard_Common_StruggleMentality_0":
                            {
                                if (TargetSource.SourceType == "Behavior")
                                {
                                    if (QuickSave_Split_StringList[0] == "Delay")
                                    {
                                        if (QuickSave_Split_StringList[1] == "End")
                                        {
                                            if (QuickSave_Split_StringList[2] == "Cemetery")
                                            {
                                                QuickSave_Split_StringList[2] = "Board";
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "EffectCard_Common_SupplyOfInspiration_0":
                            {
                                if (TargetSource.SourceType == "Enchance")
                                {
                                    if (QuickSave_Split_StringList[0] == "Delay")
                                    {
                                        if (QuickSave_Split_StringList[1] == "Use")
                                        {
                                            if (QuickSave_Split_StringList[2] == "Cemetery")
                                            {
                                                QuickSave_Split_StringList[2] = "Board";
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case "EffectCard_Common_FarFetchedIdeas_0":
                        case "EffectCard_Common_FightingFantasy_0":
                            {
                                if (QuickSave_Split_StringList[2] == "Cemetery")
                                {
                                    QuickSave_Split_StringList[2] = "Exiled";
                                    if (Action)
                                    {
                                        StackDecrease("Set", 65535);
                                    }
                                }
                            }
                            break;
                        case "EffectCard_Common_EasilyForgotten_0":
                            {
                                if (QuickSave_Split_StringList[0] == "Board")
                                {
                                    if (QuickSave_Split_StringList[2] != "Board")
                                    {
                                        if (Action)
                                        {
                                            StackDecrease("Set", 65535);
                                        }
                                    }
                                }
                            }
                            break;
                        case "EffectCard_Common_InPlans_0":
                            {
                                if (QuickSave_Split_StringList[0] == "Board")
                                {
                                    if (QuickSave_Split_StringList[2] != "Board")
                                    {
                                        bool QuickSave_Check_Bool =
                                            _Basic_Owner_Script._Basic_SaveData_Class.BoolDataGet("AbandonResidue");
                                        if (QuickSave_Check_Bool)
                                        {
                                            _Basic_Owner_Script._Basic_SaveData_Class.BoolDataSet("AbandonResidue", false);
                                            QuickSave_Split_StringList[2] = "None";
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Key｜" + 
            (QuickSave_Split_StringList[0] + "_" + QuickSave_Split_StringList[1] + "_" + QuickSave_Split_StringList[2]));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Deal(float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();//EX:
        float Answer_Return_Float = Value;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value｜" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Throw(float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();//EX:
        float Answer_Return_Float = Value;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value｜" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Stage -
    public void Key_Effect_Operate(bool IsStandby)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_Forgetful_0":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectCard_Common_EasilyForgotten_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                                _World_Manager._Effect_Manager.
                                    GetEffectCard(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_Effect_Skill(SourceClass UserSource/*該Skill來源*/, _Map_BattleObjectUnit UsingObject/*使用物(EX:Card-Use)*/)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        //_Item_ConceptUnit QuickSave_Concept_Script = _Owner_Creature_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_Effect_React(SourceClass UserSource)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_Effect_EventEnd(SourceClass UserSource)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        string QuickSave_Key_String = "";
        float QuickSave_Value_Float = 0;
        string QuickSave_Effect_String = "";
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Card -
    public List<string> Key_Effect_DealPriority(bool NowState,
        SourceClass UserSource/*概念*/, SourceClass TargetSource/*卡片*/,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = false;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_Longing_0":
                            {
                                if (TargetSource.Source_Card == null)
                                {
                                    break;
                                }
                                if (TargetSource.Source_Card != _Basic_Owner_Script)
                                {
                                    break;
                                }
                                Answer_Return_Bool = true;
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolTrue｜" + (Answer_Return_Bool));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_AbandonResidue(bool NowState,
        SourceClass UserSource/*概念*/, SourceClass TargetSource/*卡片*/,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = false;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Common_InPlans_0":
                            {
                                if (TargetSource.Source_Card == null)
                                {
                                    break;
                                }
                                if (TargetSource.Source_Card != _Basic_Owner_Script)
                                {
                                    break;
                                }
                                _Basic_Owner_Script._Basic_SaveData_Class.BoolDataSet("AbandonResidue", true);
                                Answer_Return_Bool = true;
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolTrue｜" + (Answer_Return_Bool));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_UseLicense(
        bool NowState, List<string> Data,
        SourceClass UserSource/*概念*/, SourceClass TargetSource/*卡片*/, _Map_BattleObjectUnit UsingObject/*使用*/,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Carapace_CarapaceBust":
                            {
                                //情形判定
                                if(Data[0] != "Enchance")
                                {
                                    break;
                                }
                                //持有效果判定
                                if (UsingObject.Key_Stack("Tag", "Carapace", UserSource, TargetSource, UsingObject) > 0)
                                {
                                    break;
                                }
                                Answer_Return_Bool = false;
                            }
                            break;
                        case "Enchance_Spin_SpinForce":
                        case "Enchance_Spin_SpinAccelerate":
                        case "Enchance_Spin_FreeFlow":
                            {
                                //情形判定
                                if (Data[0] != "Enchance")
                                {
                                    break;
                                }
                                //持有效果判定
                                if (UsingObject.Key_Stack("Key", "EffectObject_Spin_Spin_0", UserSource, TargetSource, UsingObject) > 0)
                                {
                                    break;
                                }
                                Answer_Return_Bool = false;
                            }
                            break;
                        case "Enchance_FlashingTachi_SunReincarnation":
                            {
                                //情形判定
                                if (Data[0] != "Enchance")
                                {
                                    break;
                                }
                                //持有效果判定
                                if (UsingObject.Key_Stack("Tag", "DominateEffect", UserSource, TargetSource, UsingObject) > 0)
                                {
                                    break;
                                }
                                Answer_Return_Bool = false;
                            }
                            break;
                        case "Enchance_GatherCobble_RoveSatellite":
                            {
                                //情形判定
                                if (Data[0] != "Enchance")
                                {
                                    break;
                                }
                                //持有效果判定
                                if (UsingObject.Key_Stack("Key", "EffectObject_GatherCobble_CometBullets_0", UserSource, TargetSource, UsingObject) > 0)
                                {
                                    break;
                                }
                                Answer_Return_Bool = false;
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse｜" + (Answer_Return_Bool));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_BehaviorMiss(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        _Skill_BehaviorUnit QuickSave_Behavior_Script = UserSource.Source_Card._Card_BehaviorUnit_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_BehaviorUseEnd(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        _Skill_BehaviorUnit QuickSave_Behavior_Script = UserSource.Source_Card._Card_BehaviorUnit_Script;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Quality_MindChange":
                        case "Enchance_Bomb_ExBurst":
                            {
                                //自身抽卡
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
                        case "Enchance_Quality_MindClear":
                            {
                                //自身抽卡
                                string QuickSave_ValueKey_String =
                                    "Deal_Default_Default｜User_Concept_Default_Material_Purity_Default_Default｜0";
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
                        case "Enchance_Identity_Frank":
                            {
                                //自身抽卡
                                string QuickSave_ValueKey_String =
                                    "Deal_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Identity_Frank_0｜0";
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
                        case "Enchance_ScarletKin_ScarletThoughts":
                            {
                                //自身抽卡
                                string QuickSave_ValueKey_String =
                                    "Deal_Default_Default｜User_Concept_Default_Stack_Default_Default_EffectObject_Scarlet_ScarletBunch_0｜0";
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

                        case "Enchance_Arthropod_LarvaeIncubation":
                            {
                                //百分比數值
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", 
                                    QuickSave_PercentageValueKey_String, 
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default", 
                                    QuickSave_PercentageKey_String,_Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String], 
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                //目標消耗值計算直
                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Arthropod_SwarmEggs_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, QuickSave_ValueData_Float,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Arthropod_ArthropodStarkDestroy":
                            {
                                //使用者傷害
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key01_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey01_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value01_Float =
                                    _World_Manager.Key_NumbersUnit("Default", QuickSave_Key01_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey01_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "PursuitTimes_StarkDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_AttackType_String =
                                    QuickSave_ValueKey01_String.Split("_"[0])[1];
                                List<DamageClass> QuickSave_Damage_ClassList = new List<DamageClass>();
                                QuickSave_Damage_ClassList.Add(new DamageClass
                                {
                                    Source = _Basic_Source_Class,
                                    DamageType = QuickSave_AttackType_String,
                                    Damage = QuickSave_Value01_Float,
                                    Times = Mathf.RoundToInt(QuickSave_Value02_Float)
                                });
                                Answer_Return_StringList.AddRange(UsingObject.
                                    Damaged(
                                    QuickSave_Damage_ClassList, 
                                    UsingObject,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Soar_Soar":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Soar_Soar_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Stone_StoneCarapaceHyperplasia":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Stone_StoneCarapace_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Light_Alpha":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Hide_Hide_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Spin_FreeFlow":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Spin_Spin_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_FlashingTachi_SunReincarnation":
                            {
                                //消除效果
                                if (Action)
                                {
                                    foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                                    {
                                        List<string> QuickSave_Tag_StringList =
                                            Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            Effect.StackDecrease("Set", 65535);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "Enchance_Float_TideGuidance":
                            {
                                if (!Action)
                                {
                                    break;
                                }
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_OceanCurrent_Normal｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet(
                                        "Object", QuickSave_Type_String,
                                        UserSource.Source_Card._Card_StartCenter_Class, null,Time,Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        int QuickSave_Direction_Int =
                                            UserSource.Source_Card._Range_UseData_Class.Select[0].Direction;
                                        QuickSave_Object_Script._Basic_Status_Dictionary["ComplexPoint"] =
                                            QuickSave_Direction_Int;
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;

                        case "Enchance_Bush_BushGrowing":
                            {
                                if (!Action)
                                {
                                    break;
                                }
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_InkigoBush_Grass｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet(
                                        "Object", QuickSave_Type_String,
                                        UserSource.Source_Card._Card_UseCenter_Class, null, Time, Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        int QuickSave_Direction_Int =
                                            UserSource.Source_Card._Range_UseData_Class.Select[0].Direction;
                                        QuickSave_Object_Script._Basic_Status_Dictionary["ComplexPoint"] =
                                            QuickSave_Direction_Int;
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;

                        case "Enchance_SwarmBringer_LarvaeBorn":
                            {
                                //執行判定
                                if (!Action)
                                {
                                    break;
                                }
                                //層數消耗
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Basic_Object_Script._Basic_Source_Class;
                                foreach (_Effect_EffectObjectUnit Effect in ConceptSource.Source_BattleObject._Effect_Effect_Dictionary.Values)
                                {
                                    if (Effect._Basic_Key_String == "EffectObject_Arthropod_SwarmEggs_0")
                                    {
                                        string QuickSave_ValueKey02_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key02_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value02_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        if (Action)
                                        {
                                            Effect.StackDecrease("Set", Mathf.RoundToInt(QuickSave_Value02_Float));
                                        }
                                    }
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_Arthropod_Larva｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                        _World_Manager._Object_Manager.ObjectSet(
                                            "Object", QuickSave_Type_String,
                                            UserSource.Source_Card._Card_UseCenter_Class, _Basic_Source_Class, Time, Order);
                                    if (QuickSave_Object_Script == null)
                                    {
                                        break;
                                    }
                                    QuickSave_Object_Script.AdvanceSet();
                                    //數值百分比
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
                                    string QuickSave_ValueKey02_String =
                                        "ConstructNumber_EffectObject_Object_ResidualDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key02_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value02_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key02_String, QuickSave_ValueData_Float,
                                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    string QuickSave_Effect_String = QuickSave_Key02_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    _World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value02_Float),
                                        _Basic_Source_Class, QuickSave_Object_Script._Basic_Source_Class,
                                        HateTarget, Action, Time, Order);
                                }
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectCard_Profession_ScheduledItinerary_0":
                            {
                                //給予概念效果
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Basic_Object_Script._Basic_Source_Class;

                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Identity_Satisfaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                if (Action)
                                {
                                    StackDecrease("Set", 1);
                                }
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Damage -
    public List<string> Key_Effect_DamageValueAdd(string DamageType, float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();

        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd｜" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }

    public List<string> Key_Effect_DamageValueMultiply(string DamageType, float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();

        float Answer_Return_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply｜" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_DamageBlock(string DamageType, float OriginalDamage/*變化前傷害*/, float NowDamage,/*當前傷害*/
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();

        float Answer_Return_Float = NowDamage;//傷害值
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Answer_Return_Float > 0)
        {
            switch (_Basic_Type_String)
            {
                case "Enchance":
                    {
                        switch (_Basic_Key_String)
                        {
                        }
                    }
                    break;
                case "EffectCard":
                    {
                        switch (_Basic_Key_String)
                        {
                        }
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value｜" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }

    public List<string> Key_Effect_Damage(string DamageType, float Value, bool IsSuceess,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Sensation_StiffAttach":
                        case "Enchance_Survival_Odyssey":
                        case "Enchance_Toxic_StiffPoison":
                        case "Enchance_ScarletKin_ScarletForce":
                            {
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Sensation_FearAttach":
                            {
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Cnidocytes_CnidocytesAllergy":
                            {
                                //給予目標效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Cnidocytes_CnidocytesAllergy_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Enchance_Fighting_BattleReact":
                            {
                                //次數限制(不論目標只會觸發一次)
                                if (!_Basic_TimesLimit_Class.TimesLimit("Round", 1))
                                {
                                    break;
                                }
                                //概念
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                    _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Sensation_Reaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        case "Enchance_Vine_VineBundle":
                            {
                                //給予目標效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Constrain_Bind_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Damaged(string DamageType, float Value, bool IsSuceess,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Dead -
    public List<string> Key_Effect_DeadResist(bool IsResist, string Type/*死亡時傷害類型(效果、傷害等)*/, float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = false;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolTrue｜" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Dead(string Type/*死亡時傷害類型(效果、傷害等)*/, float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - EnchanceBehavior -
    //範圍/用於Behaiovr Key_Range
    public BoolRangeClass Key_Range()
    {
        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Object_Throw":
                        case "Enchance_Bullet_PoorBullet":
                        case "Enchance_Bullet_DisintegratingBullet":
                        case "Enchance_Arrow_PoorArrow":
                        case "Enchance_Dart_StoneDart":

                        case "Enchance_Cuisine_FailCuisine":
                        case "Enchance_Cuisine_InvigoratingAppetizer":
                        case "Enchance_Cuisine_HealingMainCourse":
                        case "Enchance_Cuisine_EnergeticDessert":
                        case "Enchance_Cuisine_MysteryCuisine":
                        case "Enchance_Cuisine_HerbDelight":
                        case "Enchance_Cuisine_GlosporeBeverage":

                        case "Enchance_Bomb_EnergyOverDrive":
                        case "Enchance_Bomb_WaterBurst":

                        case "Enchance_Stone_RockCannon":

                        case "Enchance_SpikeShooter_SpikeBudDart":
                            return _Basic_Range_Class.Select[0];
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return null;
        //----------------------------------------------------------------------------------------------------
    }
    //目標/用於Behaiovr Key_Target
    public List<_Map_BattleObjectUnit> Key_Target(_UI_Card_Unit Behavior, PathPreviewClass PathPreview, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> Answer_Return_ScriptsList = new List<_Map_BattleObjectUnit>();
        _Map_BattleCreator _Map_BattleCreator = _World_Manager._Map_Manager._Map_BattleCreator;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Object_Throw":
                        case "Enchance_Bullet_PoorBullet":
                        case "Enchance_Arrow_PoorArrow":
                        case "Enchance_Dart_StoneDart":
                        case "Enchance_Bullet_DisintegratingBullet":
                        case "Enchance_Stone_RockCannon":
                        case "Enchance_SpikeShooter_SpikeBudDart":
                            {
                                Answer_Return_ScriptsList.Add(PathPreview.HitObject);
                            }
                            break;


                        case "Enchance_Cuisine_FailCuisine":
                        case "Enchance_Cuisine_InvigoratingAppetizer":
                        case "Enchance_Cuisine_HealingMainCourse":
                        case "Enchance_Cuisine_EnergeticDessert":
                        case "Enchance_Cuisine_MysteryCuisine":
                        case "Enchance_Cuisine_HerbDelight":
                        case "Enchance_Cuisine_GlosporeBeverage":

                        case "Enchance_Bomb_EnergyOverDrive":
                        case "Enchance_Bomb_WaterBurst":
                            {
                                //Select範圍
                                List<SelectUnitClass> QuickSave_SelectRange_ClassList = new List<SelectUnitClass>();
                                string QuickSave_DicKey_String =
                                    _Basic_Key_String + "_" + GetInstanceID();
                                foreach (SelectUnitClass SelectRangeUnit in Behavior._Range_UseData_Class.Select)
                                {
                                    if(SelectRangeUnit.Key == QuickSave_DicKey_String)
                                    {
                                        QuickSave_SelectRange_ClassList.Add(SelectRangeUnit);
                                    }
                                }
                                List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                    _Map_BattleCreator.
                                    RangeTargets("Normal", QuickSave_SelectRange_ClassList,
                                    _Basic_Source_Class, Time, Order);
                                Answer_Return_ScriptsList.AddRange(QuickSave_Objects_ScriptsList);
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }

    public _Map_BattleObjectUnit Key_Objects(Vector StartCoordinate, _UI_Card_Unit Behavior, 
        _Map_BattleObjectUnit UsingObject/*附魔時的使用者*/, 
        bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        //實體
                        case "Enchance_Object_Throw":
                        case "Enchance_Bullet_DisintegratingBullet":

                        case "Enchance_Cuisine_FailCuisine":
                        case "Enchance_Cuisine_InvigoratingAppetizer":
                        case "Enchance_Cuisine_HealingMainCourse":
                        case "Enchance_Cuisine_EnergeticDessert":
                        case "Enchance_Cuisine_MysteryCuisine":
                        case "Enchance_Cuisine_HerbDelight":
                        case "Enchance_Cuisine_GlosporeBeverage":

                        case "Enchance_Bomb_EnergyOverDrive":
                        case "Enchance_Bomb_WaterBurst":
                            {
                                return UsingObject;
                            }
                            break;
                        //幻影
                        case "Enchance_Bullet_PoorBullet":
                        case "Enchance_Arrow_PoorArrow":
                        case "Enchance_Dart_StoneDart":
                        case "Enchance_SpikeShooter_SpikeBudDart":
                            {
                                //生成/投射物幻影
                                _Map_BattleObjectUnit QuickSave_Project_Script =
                                    _World_Manager._Object_Manager.ObjectSet("Object", "Object_Phantom_Normal",
                                    StartCoordinate, _Basic_Source_Class, Time, Order);
                                if (QuickSave_Project_Script != null)
                                {
                                    QuickSave_Project_Script._Basic_SaveData_Class.BoolDataSet("Creation", true);
                                    Behavior._Basic_SaveData_Class.ObjectListDataAdd("Project", QuickSave_Project_Script);
                                    return QuickSave_Project_Script;
                                }
                                return _Basic_Source_Class.Source_Card._Card_UseObject_Script;
                            }
                            break;
                        case "Enchance_Stone_RockCannon":
                            {
                                _Map_BattleObjectUnit QuickSave_Project_Script =
                                    _World_Manager._Object_Manager.ObjectSet(
                                        "Object", "Object_Stone_StoneCarapaceScarp",
                                        StartCoordinate, null, Time, Order);
                                if (QuickSave_Project_Script != null)
                                {
                                    QuickSave_Project_Script._Basic_SaveData_Class.BoolDataSet("Creation", true);
                                    return QuickSave_Project_Script;
                                }
                                return _Basic_Source_Class.Source_Card._Card_UseObject_Script;
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return null;
        //----------------------------------------------------------------------------------------------------
    }

    public List<string > Key_Consume(string Type,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Arthropod_SacrificeRaid":
                        case "Enchance_Cuisine_MysteryCuisineEnchance":
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
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);

                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;
                        case "Enchance_Arthropod_LarvaeIncubation":
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
                                    "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
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
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);

                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;
                        case "Enchance_SwarmBringer_LarvaeBorn":
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
                                    _Basic_Source_Class, _Basic_Source_Class.Source_Creature._Card_UsingObject_Script._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class.Source_Creature._Card_UsingObject_Script._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    _Basic_Source_Class.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_Creature._Card_UsingObject_Script,
                                    HateTarget, Action, Time, Order, Action);
                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;
                        //裝填
                        //不紀錄消耗
                        case "Enchance_Bullet_PoorBullet":
                        case "Enchance_Arrow_PoorArrow":
                        case "Enchance_Dart_StoneDart":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                //實際消耗值
                                /*float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    _Basic_Source_Class.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));*/

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);

                                //_Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;
                        //紀錄消耗
                        case "Enchance_Cuisine_FailCuisine":
                        case "Enchance_Cuisine_InvigoratingAppetizer":
                        case "Enchance_Cuisine_HealingMainCourse":
                        case "Enchance_Cuisine_EnergeticDessert":
                        case "Enchance_Cuisine_MysteryCuisine":
                        case "Enchance_Cuisine_HerbDelight":
                        case "Enchance_Cuisine_GlosporeBeverage":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                //實際消耗值
                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    _Basic_Source_Class.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);
                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;

                        case "Enchance_Bomb_EnergyOverDrive":
                        case "Enchance_Bomb_WaterBurst":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                    "Consume_MediumPoint_Default｜Using_Default_Default_Point_MediumPoint_Point_Default｜0";
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
                                //實際消耗值
                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    _Basic_Source_Class.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);

                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);
                            }
                            break;
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }

    public List<string> Key_Effect(string Type, _UI_Card_Unit Behavior,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        UsingObject = _Basic_Source_Class.Source_BattleObject;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Effect -

                        case "Enchance_Object_Throw":
                            {
                                //行為類型
                                print("Throw:");
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                //測試用，自我傷害
                                print("Damage:" + Behavior._Effect_Loading_Dictionary[this]._Basic_Key_String);
                                Answer_Return_StringList.AddRange(Behavior._Effect_Loading_Dictionary[this].
                                    Damaged(new List<DamageClass>{ new DamageClass
                                    {
                                        Source = _Basic_Source_Class,
                                        DamageType = "StarkDamage",
                                        Damage = 100,
                                        Times = 1
                                    } }, UsingObject, HateTarget, Action, Time, Order));
                            }
                            break;
                        //給予目標效果-Reaction
                        case "Enchance_Cuisine_FailCuisine":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                //標籤判定(目標)
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }
                                //數值設定
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_Key_String],
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
                        //給予目標效果-Reaction(消耗值)
                        case "Enchance_Cuisine_EnergeticDessert":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                            break;

                        case "Enchance_Stone_RockCannon":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                if (UserSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(
                                    "EffectObject_Stone_StoneCarapace_0", out _Effect_EffectObjectUnit Effect))
                                {
                                    Effect.StackDecrease("Set", 1);
                                }
                            }
                            break;
                        #endregion

                        #region - Deal -
                        //自身抽卡(消耗值)
                        case "Enchance_Cuisine_InvigoratingAppetizer":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                            break;
                        #endregion

                        #region - Recover -
                        case "Enchance_Cuisine_HealingMainCourse":
                        case "Enchance_Cuisine_HerbDelight":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                            }
                            break;
                        //目標回復MediumPoint(消耗值)並獲得效果-FluorescentPoint(消耗值)
                        case "Enchance_Cuisine_GlosporeBeverage":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                        #endregion

                        #region - Pursuit -
                        case "Enchance_Bullet_DisintegratingBullet":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                //自我造成傷害
                                _Basic_Owner_Script._Basic_Source_Class.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order), UsingObject,
                                        HateTarget, Action, Time, Order);
                            }
                            break;
                            #endregion
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Attack -
    //主要攻擊/作為裝填的傷害
    public List<DamageClass> Key_Attack(string Type, _UI_Card_Unit Behavior,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Bomb_WaterBurst":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                    "AttackNumber_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        case "Enchance_Bullet_PoorBullet":
                        case "Enchance_Arrow_PoorArrow":
                        case "Enchance_Dart_StoneDart":
                        case "Enchance_SpikeShooter_SpikeBudDart":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                        case "Enchance_Object_Throw":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                            break;

                        //衝擊傷害(重量增加)
                        case "Enchance_Stone_RockCannon":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                    "AttackTimes_ImpactDamage_Type0｜Complex_Default_Default_Default_Default_Default_Default｜0";
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

                        case "Enchance_Bomb_EnergyOverDrive":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                                    "AttackNumber_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        case "Enchance_Cuisine_FailCuisine":
                        case "Enchance_Cuisine_MysteryCuisine":
                            {
                                //當前類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                //標籤判定(目標)
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }
                                //百分比數值
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_PercentageValueKey_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                //目標消耗值計算直
                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);
                                //傷害
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
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }

    //攻擊附加/計算為主要傷害之一/於Behavior攻擊時追加
    public List<DamageClass> Key_AttackAdden(string Type,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        UsingObject = _Basic_Source_Class.Source_BattleObject;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------

        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Enchance_Phantom_PhantomBlade":
                            {
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                                    "PursuitTimes_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                        case "Enchance_Cnidocytes_CnidocytesAllergy":
                        case "Enchance_Vine_ThornPiercing":
                        case "Enchance_Toxic_StiffPoison":
                        case "Enchance_SpikeShooter_SpikeBud":
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
                            break;
                        case "Enchance_Cuisine_MysteryCuisineEnchance":
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
                            break;
                        case "Enchance_Doll_UnknowContents":
                        case "Enchance_Light_LightConcept":
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
                        case "Enchance_Doll_PastContents":
                            {
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_AbstractDamage_Type0｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
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
                        case "Enchance_Carapace_CarapaceBust":
                            {
                                if (!Action)
                                {
                                    break;
                                }
                                foreach (_Effect_EffectObjectUnit Effect in UsingObject._Effect_Effect_Dictionary.Values)
                                {
                                    //標籤判定
                                    List<string> QuickSave_Tag_StringList =
                                    Effect.Key_EffectTag(UserSource, TargetSource);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Carapace" };
                                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        //層數減少
                                        Effect.StackDecrease("Set", 1);
                                        break;
                                    }
                                }

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
                            break;
                        case "Enchance_Arthropod_SacrificeRaid":
                            {

                                //百分比數值
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default",
                                    QuickSave_PercentageValueKey_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                //目標消耗值計算直
                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //傷害
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_PunctureDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                            break;
                        case "Enchance_GatherCobble_RoveSatellite":
                            {
                                if (!Action)
                                {
                                    break;
                                }
                                //持有效果判定
                                string QuickSave_Effect_String = "EffectObject_GatherCobble_CometBullets_0";
                                int QuickSave_EffectStack_Int =
                                    UsingObject.Key_Stack("Key", QuickSave_Effect_String, UserSource, TargetSource, UsingObject);
                                //基礎層數判定
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
                                int QuickSave_Value_Int = Mathf.RoundToInt(QuickSave_Value_Float);

                                if (QuickSave_EffectStack_Int < QuickSave_Value_Int)
                                {
                                    break;
                                }
                                //層數減少
                                if (UsingObject._Effect_Effect_Dictionary.TryGetValue(
                                    QuickSave_Effect_String, out _Effect_EffectObjectUnit Effect))
                                {
                                    Effect.StackDecrease("Set", QuickSave_Value_Int);
                                }
                                //追擊
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
                            break;


                        //裝填
                        case "Enchance_Dart_StoneDart":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
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
                            break;
                        case "Enchance_Cuisine_MysteryCuisine":
                            {
                                //行為類型
                                if (Type != "Enchance")
                                {
                                    break;
                                }
                                //標籤判定(目標)
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject.Key_Tag(_Basic_Source_Class, TargetSource);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Mouth" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }

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
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //行為檢索附魔用/招式附魔對行為加成引導//並非Situation呼叫，而是透過被Situation呼叫的啟動(如Damage)——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Pursuit(
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)//附魔對象類型
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        UsingObject = _Basic_Source_Class.Source_BattleObject;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //數字設定
        switch (_Basic_Type_String)
        {
            case "Enchance":
                {
                    switch (_Basic_Key_String)
                    {
                        //裝填
                        case "Enchance_Bullet_DisintegratingBullet":
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
                    }
                }
                break;
            case "EffectCard":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //使用原本數值
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion
    #endregion KeyAction
}
