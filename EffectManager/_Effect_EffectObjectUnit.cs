using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class _Effect_EffectObjectUnit : MonoBehaviour
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
    //----------------------------------------------------------------------------------------------------

    //個體差異----------------------------------------------------------------------------------------------------
    //持有者
    public _Map_BattleObjectUnit _Basic_Owner_Script;
    //氣泡位置
    public Transform _Effect_Bubble_Transform;
    //當前層數
    private int _Effect_Stack_Int;
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
    public void SystemStart(_Map_BattleObjectUnit Owner, string Key)
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
            case "SpecialAffix":
                {
                    _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
                    _Item_Manager.SpecialAffixDataClass QS_Data_Class = _Item_Manager._Data_SpecialAffix_Dictionary[Key];
                    _Basic_Data_Class = new _Effect_Manager.EffectDataClass
                    {
                        EffectTag = new List<string> { QS_Type_String },
                        Decay = "Forever",
                        StackLimit = 1,
                        DecayTimes = 1,
                        Value = 0,
                        Numbers = QS_Data_Class.Numbers,
                        Keys = QS_Data_Class.Keys
                    };
                    _Basic_Language_Class = _Item_Manager._Language_SpecialAffix_Dictionary[Key];
                    _Basic_Source_Class = new SourceClass
                    {
                        SourceType = QS_Type_String,
                        Source_EffectObject = this,
                        Source_Creature = QuickSave_OwnerSource_Class.Source_Creature,
                        Source_Concept = QuickSave_OwnerSource_Class.Source_Concept,
                        Source_Weapon = QuickSave_OwnerSource_Class.Source_Weapon,
                        Source_Item = QuickSave_OwnerSource_Class.Source_Item,
                        Source_BattleObject = Owner,
                        Source_NumbersData = _Basic_Data_Class.Numbers,
                        Source_KeysData = _Basic_Data_Class.Keys
                    };
                    _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
                    Owner._Effect_SpecialAffix_ScriptsList.Add(this);
                    //層數
                    _Effect_Stack_Text.text = "S";
                }
                break;
            case "Passive":
                {
                    _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
                    if (!_Skill_Manager._Data_Passive_Dictionary.ContainsKey(Key))
                    {
                        print(Key);
                    }
                    _Skill_Manager.PassiveDataClass QS_Data_Class = _Skill_Manager._Data_Passive_Dictionary[Key];
                    _Basic_Data_Class = new _Effect_Manager.EffectDataClass
                    {
                        EffectTag = new List<string> { QS_Type_String },
                        Decay = "Forever",
                        StackLimit = 1,
                        DecayTimes = 1,
                        Value = 0,
                        Numbers = QS_Data_Class.Numbers,
                        Keys = QS_Data_Class.Keys
                    };
                    _Basic_Language_Class = _Skill_Manager._Language_Passive_Dictionary[Key];
                    _Basic_Source_Class = new SourceClass
                    {
                        SourceType = QS_Type_String,
                        Source_EffectObject = this,
                        Source_Creature = QuickSave_OwnerSource_Class.Source_Creature,
                        Source_Concept = QuickSave_OwnerSource_Class.Source_Concept,
                        Source_Weapon = QuickSave_OwnerSource_Class.Source_Weapon,
                        Source_Item = QuickSave_OwnerSource_Class.Source_Item,
                        Source_BattleObject = Owner,
                        Source_NumbersData = _Basic_Data_Class.Numbers,
                        Source_KeysData = _Basic_Data_Class.Keys
                    };
                    _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
                    Owner._Effect_Passive_Dictionary.Add(_Basic_Key_String, this);
                    //層數
                    _Effect_Stack_Text.text = "P";
                }
                break;
            case "EffectObject":
                {
                    _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
                    if (!_Effect_Manager._Data_EffectObject_Dictionary.ContainsKey(Key))
                    {
                        print(Key);
                    }
                    _Basic_Data_Class = _Effect_Manager._Data_EffectObject_Dictionary[Key];
                    _Basic_Language_Class = _Effect_Manager._Language_EffectObject_Dictionary[Key];
                    _Basic_Source_Class = new SourceClass
                    {
                        SourceType = QS_Type_String,
                        Source_EffectObject = this,
                        Source_Creature = QuickSave_OwnerSource_Class.Source_Creature,
                        Source_Concept = QuickSave_OwnerSource_Class.Source_Concept,
                        Source_Weapon = QuickSave_OwnerSource_Class.Source_Weapon,
                        Source_Item = QuickSave_OwnerSource_Class.Source_Item,
                        Source_BattleObject = Owner,
                        Source_NumbersData = _Basic_Data_Class.Numbers,
                        Source_KeysData = _Basic_Data_Class.Keys
                    };
                    _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
                    Owner._Effect_Effect_Dictionary.Add(_Basic_Key_String, this);

                    if (_Basic_Data_Class.Decay == "Sequence")
                    {
                        _World_Manager._Map_Manager._Map_BattleRound._Round_SequenceEffectObject_ScriptsList.Add(this);
                    }
                }
                break;
        }
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
        if (_Effect_Stack_Int > Key_StackLimit())
        {
            _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
        }
        _Effect_Stack_Int = Mathf.Clamp(_Effect_Stack_Int, 0, Key_StackLimit());
        //----------------------------------------------------------------------------------------------------

        //更新數值----------------------------------------------------------------------------------------------------
        //視覺更新
        switch (_Basic_Type_String)
        {
            case "EffectObject":
                ViewSet();
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //層數增加——————————————————————————————————————————————————————————————————————
    public void StackDecrease(string Type, int Number)
    {
        //數值變動----------------------------------------------------------------------------------------------------
        _Effect_Stack_Int = Mathf.Clamp(_Effect_Stack_Int - Number, 0, Key_StackLimit());
        //----------------------------------------------------------------------------------------------------

        //更新數值----------------------------------------------------------------------------------------------------
        //視覺更新
        switch (_Basic_Type_String)
        {
            case "EffectObject":
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
        Key_Effect_OwnEnd();
        switch (_Basic_Type_String)
        {
            case "SpecialAffix":
                _Basic_Owner_Script._Effect_SpecialAffix_ScriptsList.Remove(this);
                break;
            case "Passive":
                _Basic_Owner_Script._Effect_Passive_Dictionary.Remove(_Basic_Key_String);
                break;
            case "EffectObject":
                _Basic_Owner_Script._Effect_Effect_Dictionary.Remove(_Basic_Key_String);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Basic_Data_Class.Decay == "Sequence")
        {
            _World_Manager._Map_Manager._Map_BattleRound._Round_SequenceEffectObject_ScriptsList.Remove(this);
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
        _Effect_Decay_Int = Mathf.Clamp(_Effect_Decay_Int - Number, 0, _Basic_Data_Class.DecayTimes);
        //小額判定
        if (_Effect_Decay_Int == 0)
        {
            _Effect_Decay_Int = _Basic_Data_Class.DecayTimes;
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

    #region DataKey
    #region - Stack -
    //相對層數——————————————————————————————————————————————————————————————————————
    public int Key_Stack(string Type, string KeyTag,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (Type)
        {
            case "Default":
                {
                    if (_Basic_Key_String == KeyTag)
                    {
                        Answer_Return_Float = _Effect_Stack_Int;
                    }
                }
                break;
            case "Tag":
                {
                    switch (_Basic_Type_String)
                    {
                        case "EffectObject":
                            switch (_Basic_Key_String)
                            {
                                default:
                                    {
                                        List<string> QuickSave_Tag_StringList = new List<string>(KeyTag.Split(","[0]));
                                        if (_World_Manager._Skill_Manager.TagContains(
                                            Key_EffectTag(UserSource, TargetSource), QuickSave_Tag_StringList, true))
                                        {
                                            Answer_Return_Float += _Effect_Stack_Int;
                                        }
                                    }
                                    break;

                                case "EffectObject_Sensation_DeepFear_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            Key_EffectTag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Fear" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            Answer_Return_Float += _Effect_Stack_Int * QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                            break;
                        case "SpecialAffix":
                            switch (_Basic_Key_String)
                            {
                            }
                            break;
                    }
                }
                break;
            case "Key":
                {
                    switch (_Basic_Type_String)
                    {
                        case "EffectObject":
                            switch (_Basic_Key_String)
                            {
                                default:
                                    {
                                        if (_Basic_Key_String == KeyTag)
                                        {
                                            Answer_Return_Float += _Effect_Stack_Int;
                                        }
                                    }
                                    break;
                            }
                            break;
                        case "SpecialAffix":
                            switch (_Basic_Key_String)
                            {
                            }
                            break;
                    }
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
    #endregion

    #region - Key_Tag -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_EffectTag(SourceClass UserSource, SourceClass TargetSource)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_Add_StringList = null;
        List<string> QuickSave_Remove_StringList = null;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { "EffectTag", _Basic_Type_String };
        QuickSave_Add_StringList =
            _Basic_Owner_Script.SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList, 
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            _Basic_Owner_Script.SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList, 
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        Answer_Return_StringList = new List<string>(_Basic_Data_Class.EffectTag);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - StackLimit -
    //——————————————————————————————————————————————————————————————————————
    public int Key_StackLimit()
    {
        //----------------------------------------------------------------------------------------------------
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Basic_Type_String != "EffectObject")
        {
            return _Basic_Data_Class.StackLimit;
        }
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //前置延遲
        List<string> QuickSave_Data_StringList =
            new List<string> { "StackLimit", _Basic_Key_String };
        QuickSave_AdvanceAdd_Float += float.Parse(
            _Basic_Owner_Script.SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList, 
            _Basic_Source_Class, _Basic_Source_Class, _Basic_Owner_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            _Basic_Owner_Script.SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList, 
            _Basic_Source_Class, _Basic_Source_Class, _Basic_Owner_Script,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        switch (_Basic_Key_String)
        {
            default:
                Answer_Return_Float = _Basic_Data_Class.StackLimit;
                break;
        }
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_Float = Mathf.Clamp(Answer_Return_Float, 0, 65535);
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------

    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion DataKey

    #region KeyAction
    #region - Scene -
    public void Key_SkillExtend(_Skill_FactionUnit Faction)//額外招式
    {
        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Quality_SelfEnergyDetonation":
                            {
                                //紀錄要用到的卡
                                _UI_Card_Unit QuickSave_Card_Script =
                                    _World_Manager._Skill_Manager.
                                    SkillLeafStartSet(
                                        Faction, "SkillLeaves_Quality_SelfEnergyDetonation");
                                Faction._Faction_ExtendSkillLeaves_Dictionary.
                                    Add(_Basic_Key_String, new List<_UI_Card_Unit> { QuickSave_Card_Script });
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    #region - Key_Effect_Own -
    //效果獲得/與層數(Stack)無關——————————————————————————————————————————————————————————————————————
    public void Key_Effect_OwnStart()
    {
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        //驅動
                        case "EffectObject_Object_BeforeDominate_0":
                        case "EffectObject_Object_ResidualDominate_0":
                        case "EffectObject_Arthropod_HiveDominate_0":
                        case "EffectObject_Maid_MaidGrace_0":
                        case "EffectObject_Float_RoveDominate_0":
                        case "EffectObject_Stone_StoneDominate_0":
                        case "EffectObject_FlashingTachi_SunriseDominate_0":
                            {
                                _World_Manager._Object_Manager._Basic_SaveData_Class.
                                    ObjectListDataAdd("PreDriving", _Basic_Owner_Script);
                                _World_Manager._Object_Manager._Basic_SaveData_Class.
                                    SourceListDataAdd("PreDriving", _Basic_Source_Class);
                            }
                            break;
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        //驅動
                        case "EffectObject_Object_BeforeDominate_0":
                        case "EffectObject_Object_ResidualDominate_0":
                        case "EffectObject_Arthropod_HiveDominate_0":
                        case "EffectObject_Maid_MaidGrace_0":
                        case "EffectObject_Float_RoveDominate_0":
                        case "EffectObject_Stone_StoneDominate_0":
                        case "EffectObject_FlashingTachi_SunriseDominate_0":
                            {
                                _World_Manager._Object_Manager._Basic_SaveData_Class.
                                    ObjectListDataAdd("PreAbandoning", _Basic_Owner_Script);
                                _World_Manager._Object_Manager._Basic_SaveData_Class.
                                    SourceListDataAdd("PreAbandoning", _Basic_Source_Class);
                            }
                            break;
                        case "EffectObject_EternalLighthouse_PastPlunder_0":
                            {
                                //自身獲得效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_EternalLighthouse_PastDefects_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                                _World_Manager._Effect_Manager.GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                        case "EffectObject_Dark_DemonSacrifice_0":
                            {
                                //自我造成傷害
                                _Basic_Owner_Script._Basic_Source_Class.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class,null,
                                        null,true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int),
                                        null,
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Arthropod_ArmyOfInsects":
                            {
                                //概念獲得效果
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                    _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;

                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Arthropod_SwarmEggs_0｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, ConceptSource,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "Passive":
                {

                    switch (_Basic_Key_String)
                    {
                        case "Passive_Object_AutoArm":
                            {
                                if (_Basic_Source_Class.Source_Creature == null)
                                {
                                    break;
                                }
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                    _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;

                                //概念消耗額
                                string QuickSave_ValueKey_String =
                                    "Consume_ConsciousnessPoint_Default｜User_Concept_Default_Point_ConsciousnessPoint_Total_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0, ConceptSource.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point", ConceptSource, _Basic_Source_Class));
                                ConceptSource.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);

                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                                //消耗百分比
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    _Basic_Source_Class, ConceptSource, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //自身獲得效果
                                QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Object_BeforeDominate_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, QuickSave_ValueData_Float,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                        case "Passive_GatherCobble_CometBullets":
                            {
                                //自身獲得效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_GatherCobble_CometBullets_0｜User_Default_Default_Status_Medium_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Common -
                        #endregion
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Identity_ConcernsAboutUnsightlyDirt_0":
                            {
                                //層數減少
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key[0])
        {
            #region - MaterialStatus -
            case "Size":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Small -
                                case "SpecialAffix_Small_0":
                                case "SpecialAffix_Small_1":
                                case "SpecialAffix_Small_2":
                                case "SpecialAffix_Small_3":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Large -
                                case "SpecialAffix_Large_0":
                                case "SpecialAffix_Large_1":
                                case "SpecialAffix_Large_2":
                                case "SpecialAffix_Large_3":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Bushy -
                                case "SpecialAffix_Bushy_0":
                                case "SpecialAffix_Bushy_1":
                                case "SpecialAffix_Bushy_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Flaw -
                                case "SpecialAffix_Flaw_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Dirty -
                                case "SpecialAffix_Dirty_1":
                                case "SpecialAffix_Dirty_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rust -
                                case "SpecialAffix_Rust_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rot -
                                case "SpecialAffix_Rot_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - OreCluster -
                                case "SpecialAffix_OreCluster_0":
                                case "SpecialAffix_OreCluster_1":
                                case "SpecialAffix_OreCluster_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Mature -
                                case "SpecialAffix_Mature_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Offline -
                                case "SpecialAffix_Offline_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Germination -
                                case "SpecialAffix_Germination_0":
                                case "SpecialAffix_Germination_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - GlobularMossFluff -
                                case "SpecialAffix_GlobularMossFluff_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Form":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Bushy -
                                case "SpecialAffix_Bushy_0":
                                case "SpecialAffix_Bushy_1":
                                case "SpecialAffix_Bushy_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Flaw -
                                case "SpecialAffix_Flaw_0":
                                case "SpecialAffix_Flaw_1":
                                case "SpecialAffix_Flaw_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rust -
                                case "SpecialAffix_Rust_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rot -
                                case "SpecialAffix_Rot_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Mature -
                                case "SpecialAffix_Mature_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Offline -
                                case "SpecialAffix_Offline_0":
                                case "SpecialAffix_Offline_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Tie -
                                case "SpecialAffix_Tie_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Germination -
                                case "SpecialAffix_Germination_0":
                                case "SpecialAffix_Germination_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Penetration -
                                case "SpecialAffix_Penetration_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - GlobularMossFluff -
                                case "SpecialAffix_GlobularMossFluff_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Weight":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Small -
                                case "SpecialAffix_Small_1":
                                case "SpecialAffix_Small_2":
                                case "SpecialAffix_Small_3":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Large -
                                case "SpecialAffix_Large_1":
                                case "SpecialAffix_Large_2":
                                case "SpecialAffix_Large_3":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Flaw -
                                case "SpecialAffix_Flaw_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rust -
                                case "SpecialAffix_Rust_1":
                                case "SpecialAffix_Rust_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Fragile -
                                case "SpecialAffix_Fragile_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Lissom -
                                case "SpecialAffix_Lissom_0":
                                case "SpecialAffix_Lissom_1":
                                case "SpecialAffix_Lissom_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rot -
                                case "SpecialAffix_Rot_1":
                                case "SpecialAffix_Rot_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - OreCluster -
                                case "SpecialAffix_OreCluster_0":
                                case "SpecialAffix_OreCluster_1":
                                case "SpecialAffix_OreCluster_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Mature -
                                case "SpecialAffix_Mature_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Hypersecretion -
                                case "SpecialAffix_Hypersecretion_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "SpecialAffix_Hypersecretion_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Germination -
                                case "SpecialAffix_Germination_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Penetration -
                                case "SpecialAffix_Penetration_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Light":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
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
                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                case "Passive_Identity_Heavy":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
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
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Carapace_ShellbornBurden_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Stone_StoneCarapace_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "Purity":
                switch (_Basic_Type_String)
                {
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_GatherCobble_CometBullets_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Mysophobia":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Bushy -
                                case "SpecialAffix_Bushy_1":
                                case "SpecialAffix_Bushy_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Flaw -
                                case "SpecialAffix_Flaw_0":
                                case "SpecialAffix_Flaw_1":
                                case "SpecialAffix_Flaw_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Dirty -
                                case "SpecialAffix_Dirty_0":
                                case "SpecialAffix_Dirty_1":
                                case "SpecialAffix_Dirty_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rust -
                                case "SpecialAffix_Rust_0":
                                case "SpecialAffix_Rust_1":
                                case "SpecialAffix_Rust_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Fragile -
                                case "SpecialAffix_Fragile_0":
                                case "SpecialAffix_Fragile_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Rot -
                                case "SpecialAffix_Rot_0":
                                case "SpecialAffix_Rot_1":
                                case "SpecialAffix_Rot_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Pure -
                                case "SpecialAffix_Pure_0":
                                case "SpecialAffix_Pure_1":
                                case "SpecialAffix_Pure_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - OreCluster -
                                case "SpecialAffix_OreCluster_0":
                                case "SpecialAffix_OreCluster_1":
                                case "SpecialAffix_OreCluster_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Mature -
                                case "SpecialAffix_Mature_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "SpecialAffix_Mature_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Hypersecretion -
                                case "SpecialAffix_Hypersecretion_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Germination -
                                case "SpecialAffix_Germination_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Doping -
                                case "SpecialAffix_Doping_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Penetration -
                                case "SpecialAffix_Penetration_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Ancent -
                                case "SpecialAffix_Ancent_0":
                                case "SpecialAffix_Ancent_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - GlobularMossFluff -
                                case "SpecialAffix_GlobularMossFluff_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                }
                break;
            #endregion

            #region - Status -
            case "Medium":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Fragile -
                                case "SpecialAffix_Fragile_0":
                                case "SpecialAffix_Fragile_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Catalyst":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - WoodenEchoes -
                                case "SpecialAffix_WoodenEchoes_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Ancent -
                                case "SpecialAffix_Ancent_0":
                                case "SpecialAffix_Ancent_1":
                                case "SpecialAffix_Ancent_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Consciousness":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                case "Passive_Identity_Arrogant":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Vitality":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Strength":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Precision":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;
            case "Speed":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessOverDrive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                }
                break;

            case "AttackValue":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Gather -
                                case "SpecialAffix_Gather_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Identity_LeadershipInfluence_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "HealValue":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Nutrition -
                                case "SpecialAffix_Nutrition_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cusine" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default", 
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                #endregion

                                #region - Taste -
                                case "SpecialAffix_Taste_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cusine" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default", 
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                #endregion

                                #region - Gather -
                                case "SpecialAffix_Gather_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - SelfExpectation -
                                case "SpecialAffix_SelfExpectation_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Point_MediumPoint_Total_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        if (_Basic_Owner_Script.Key_Point("MediumPoint","Point",UserSource,TargetSource) > QuickSave_Value_Float)
                                        {
                                            //大於
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default", 
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                        else
                                        {
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜4" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default", 
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Identity_LeadershipInfluence_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "ConstructValue":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Taste -
                                case "SpecialAffix_Taste_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cusine" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                #endregion

                                #region - Gather -
                                case "SpecialAffix_Gather_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                case "SpecialAffix_Gather_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - SelfExpectation -
                                case "SpecialAffix_SelfExpectation_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Point_MediumPoint_Total_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        if (_Basic_Owner_Script.Key_Point("MediumPoint", "Point", UserSource, TargetSource) > QuickSave_Value_Float)
                                        {
                                            //大於
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                        else
                                        {
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜5" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Identity_LeadershipInfluence_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜2";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "EnchanceValue":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Malogic_FullEnchantmentCarrier":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            _Map_BattleObjectUnit QuickSave_Object_Script =
                                                _Basic_Source_Class.Source_BattleObject;
                                            int QuickSave_Point_Int =
                                                QuickSave_Object_Script.Key_Point("MediumPoint", "Point", UserSource, TargetSource);
                                            int QuickSave_Total_Int =
                                                QuickSave_Object_Script.Key_Point("MediumPoint", "Total", UserSource, TargetSource);
                                            if (QuickSave_Total_Int - QuickSave_Point_Int <= 0)
                                            {
                                                //提升數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;

            case "CatalystPoint":
                {
                    switch (Key[1])
                    {
                        case "Point":
                            switch (_Basic_Type_String)
                            {
                                case "SpecialAffix":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                        }
                                    }
                                    break;
                                case "Passive":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                        }
                                    }
                                    break;
                                case "EffectObject":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                        }
                                    }
                                    break;
                            }
                            break;
                        case "Total":
                            switch (_Basic_Type_String)
                            {
                                case "SpecialAffix":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                        }
                                    }
                                    break;
                                case "Passive":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                        }
                                    }
                                    break;
                                case "EffectObject":
                                    {
                                        switch (_Basic_Key_String)
                                        {
                                            case "EffectObject_EternalLighthouse_PastPlunder_0":
                                                {
                                                    string QuickSave_ValueKey_String =
                                                        "Value_Default_Default｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                                                    string QuickSave_Key_String =
                                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                    float QuickSave_Value_Float =
                                                        _World_Manager.Key_NumbersUnit("Default",
                                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                    Answer_Return_Float -= QuickSave_Value_Float;
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                break;
            #endregion

            #region - Data -
            case "Deal":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Arrogant":
                                case "Passive_Identity_SurvivalThoughts":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                case "Passive_Meka_Android":
                                case "Passive_Meka_UnusualAndroid":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Object_DominatorGame_0":
                                case "EffectObject_Object_DominatorGame_1":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Object_DominatorGame_3":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;

            case "DelayStandby":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_SurvivalThoughts":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_BattleReactor_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_EffectObject_Sensation_BattleReactor_0｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Light_FlashDisease_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                            UserSource.Source_BattleObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "ViewEye" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                        //標籤判定
                                        QuickSave_Tag_StringList =
                                            UserSource.Source_BattleObject.Key_Tag(UserSource, TargetSource);
                                        QuickSave_CheckTag_StringList = new List<string> { "LightEye" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "DelayBefore":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Stench -
                                case "SpecialAffix_Stench_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                #endregion

                                #region - Taste -
                                case "SpecialAffix_Taste_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Cusine" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                #endregion

                                #region - SelfExpectation -
                                case "SpecialAffix_SelfExpectation_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Point_MediumPoint_Total_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit(
                                                "Default", QuickSave_ValueKey_String, 
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default", 
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                            UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        if (_Basic_Owner_Script.Key_Point("MediumPoint", "Point", UserSource, TargetSource) > QuickSave_Value_Float)
                                        {
                                            //大於
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜3" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                        else
                                        {
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜6" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Item_ItemMaster":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Item" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += -QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_Identity_Positive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                    }
                                    break;

                                case "Passive_Float_GoWithTheFlow":
                                    {
                                        if (!Action)
                                        {
                                            break;
                                        }
                                        //使用該物件
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //為因卡片觸發
                                        if (UserSource.Source_Card == null)
                                        {
                                            break;
                                        }
                                        //有選擇位置
                                        _UI_Card_Unit QuickSave_Card_Script = UserSource.Source_Card;
                                        if (QuickSave_Card_Script._Range_UseData_Class.Select.Count == 0)
                                        {
                                            break;
                                        }

                                        List<string> QuickSave_Tag_StringList =
                                        QuickSave_Card_Script._Card_BehaviorUnit_Script.
                                        Key_OwnTag(TargetSource, UsingObject, true);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DirectionRange" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            //確認地塊物件效果/洋流方向
                                            Vector QuickSave_Pos_Class = _Basic_Source_Class.Source_BattleObject.TimePosition(Time, Order);
                                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                                _World_Manager._Object_Manager.
                                                TimeObjects("All", _Basic_Source_Class,
                                                Time, Order, QuickSave_Pos_Class);
                                            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                            {
                                                if (Object._Basic_Key_String == "Object_OceanCurrent_Normal")
                                                {
                                                    int QuickSave_ObjectDirection_Int = Mathf.RoundToInt(
                                                        Object.Key_Status("ComplexPoint", UserSource, TargetSource, null));
                                                    int QuickSave_Direction_Int = QuickSave_Card_Script._Range_UseData_Class.Select[0].Direction;
                                                    //順向
                                                    if (QuickSave_Direction_Int == QuickSave_ObjectDirection_Int)
                                                    {
                                                        string QuickSave_ValueKey_String =
                                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                        string QuickSave_Key_String =
                                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                        float QuickSave_Value_Float =
                                                            _World_Manager.Key_NumbersUnit("Default",
                                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                        Answer_Return_Float += -QuickSave_Value_Float;
                                                    }
                                                    //逆向
                                                    else if ((QuickSave_Direction_Int + QuickSave_ObjectDirection_Int) % 2 == 0)
                                                    {
                                                        string QuickSave_ValueKey_String =
                                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                        string QuickSave_Key_String =
                                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                        float QuickSave_Value_Float =
                                                            _World_Manager.Key_NumbersUnit("Default",
                                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                        Answer_Return_Float += QuickSave_Value_Float;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_ScarletKin_ScarletKinScute":
                                    {
                                        //受驅動時有效
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script != null)
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_BattleReactor_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_EffectObject_Sensation_BattleReactor_0｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Quality_Wet_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;

                                case "EffectObject_Carapace_ShellbornBurden_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜1";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;

                                case "EffectObject_Stone_DelaySlate_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //卡片
                                        if (UserSource.Source_Card == null)
                                        {
                                            break;
                                        }
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                        UserSource.Source_Card._Card_BehaviorUnit_Script.
                                            Key_OwnTag(TargetSource, UsingObject, true);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Spell|Stone)" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "DelayAfter":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Positive":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;

                                case "Passive_Float_GoWithTheFlow":
                                    {
                                        if (!Action)
                                        {
                                            break;
                                        }
                                        //使用該物件
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //為因卡片觸發
                                        if (UserSource.Source_Card == null)
                                        {
                                            break;
                                        }
                                        //有選擇位置
                                        _UI_Card_Unit QuickSave_Card_Script = UserSource.Source_Card;
                                        if (QuickSave_Card_Script._Range_UseData_Class.Select.Count == 0)
                                        {
                                            break;
                                        }

                                        List<string> QuickSave_Tag_StringList =
                                        QuickSave_Card_Script._Card_BehaviorUnit_Script.
                                        Key_OwnTag(TargetSource, UsingObject, true);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "DirectionRange" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            //確認地塊物件效果/洋流方向
                                            Vector QuickSave_Pos_Class = _Basic_Source_Class.Source_BattleObject.TimePosition(Time, Order);
                                            List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                                _World_Manager._Object_Manager.
                                                TimeObjects("All", _Basic_Source_Class,
                                                Time, Order, QuickSave_Pos_Class);
                                            foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                            {
                                                if (Object._Basic_Key_String == "Object_OceanCurrent_Normal")
                                                {
                                                    int QuickSave_ObjectDirection_Int = Mathf.RoundToInt(
                                                        Object.Key_Status("ComplexPoint", UserSource, TargetSource, null));
                                                    int QuickSave_Direction_Int = QuickSave_Card_Script._Range_UseData_Class.Select[0].Direction;
                                                    //順向
                                                    if (QuickSave_Direction_Int == QuickSave_ObjectDirection_Int)
                                                    {
                                                        string QuickSave_ValueKey_String =
                                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                        string QuickSave_Key_String =
                                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                        float QuickSave_Value_Float =
                                                            _World_Manager.Key_NumbersUnit("Default",
                                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                        Answer_Return_Float += -QuickSave_Value_Float;
                                                    }
                                                    //逆向
                                                    else if ((QuickSave_Direction_Int + QuickSave_ObjectDirection_Int) % 2 == 0)
                                                    {
                                                        string QuickSave_ValueKey_String =
                                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                        string QuickSave_Key_String =
                                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                        float QuickSave_Value_Float =
                                                            _World_Manager.Key_NumbersUnit("Default",
                                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                        Answer_Return_Float += QuickSave_Value_Float;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_BattleReactor_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_EffectObject_Sensation_BattleReactor_0｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;

                                case "EffectObject_Stone_DelaySlate_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //卡片
                                        if (UserSource.Source_Card == null)
                                        {
                                            break;
                                        }
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                        UserSource.Source_Card._Card_BehaviorUnit_Script.
                                            Key_OwnTag(TargetSource, UsingObject, true);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Spell|Stone)" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;

            //變動性延遲(EX:僵直等影響)
            case "TimelyDelayStandby":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_Stiff_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Tissue" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_ShortCircuit_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Meka" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Fear_0":
                                case "EffectObject_Sensation_DeepFear_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Reaction_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "TimelyDelayBefore":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_Stiff_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Tissue" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_ShortCircuit_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Meka" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Fear_0":
                                case "EffectObject_Sensation_DeepFear_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Reaction_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "TimelyDelayAfter":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Sensation_Stiff_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Tissue" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_ShortCircuit_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Meka" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Fear_0":
                                case "EffectObject_Sensation_DeepFear_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "EffectObject_Sensation_Reaction_0":
                                    {
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_Source_Class.SourceType == "Concept" ||
                                            UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            Answer_Return_Float -= QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            #endregion

            #region - Battle -
            case "AttackNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Blowgun_SpiralTube":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            if (UserSource.Source_Card != null)
                                            {
                                                List<string> QuickSave_Tag_StringList =
                                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                                    Key_OwnTag(TargetSource, UsingObject, true);
                                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Loading" };
                                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                                {
                                                    break;
                                                }

                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Status_ConstructValue_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_Stone_DirtyGravel":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //標籤判定
                                            List<string> QuickSave_Tag_StringList =
                                            UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                //提升數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Material_Purity_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_LanspidDagger_BrokenSharp":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //標籤判定
                                            List<string> QuickSave_Tag_StringList =
                                                UsingObject.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Blade:Sharp)" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                //提升數值
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);

                                                float QuickSave_PointPercentage_Float =
                                                    UsingObject.Key_Point("MediumPoint", "Point", UserSource, TargetSource) /
                                                    UsingObject.Key_Point("MediumPoint", "Total", UserSource, TargetSource);

                                                Answer_Return_Float += (QuickSave_Value_Float * QuickSave_PointPercentage_Float);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Object_DominatorGame_0":
                                case "EffectObject_Object_DominatorGame_1":
                                case "EffectObject_Object_DominatorGame_2":
                                case "EffectObject_Object_DominatorGame_3":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Concept_Default_Data_CardsCount_Board_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Soar_Soar_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Material_Weight_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Dark_DemonSacrifice_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_EternalLighthouse_PastDefects_0":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "HealNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_EternalLighthouse_PastDefects_0":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜1";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                                case "EffectObject_Medicine_MedicineResistance_0":
                                    {
                                        //機率於未使用時不計算
                                        if (Action != true)
                                        {
                                            break;
                                        }
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //卡片
                                        if (UserSource.Source_Card == null)
                                        {
                                            break;
                                        }
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                        UserSource.Source_Card._Card_BehaviorUnit_Script.
                                            Key_OwnTag(TargetSource, UsingObject, true);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Medicine" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            //機率發動
                                            string QuickSave_ValueKey_String =
                                                "Probability_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                            {
                                                QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                                QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float -= QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "ConstructNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_EternalLighthouse_PastDefects_0":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜2";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float -= QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            #endregion

            #region SituationTimes
            case "OperateTimes":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Leisurely":
                                    {
                                        //使用該物件
                                        //未受傷時增加次數/true=未受傷
                                        if (_Basic_SaveData_Class.BoolDataGet(_Basic_Key_String))
                                        {
                                            //提升數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default", QuickSave_Key_String,
                                                _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                            break;
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
                #endregion SituationTimes
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
            #region - MaterialStatus -
            case "Weight":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Spin_ExtraordinarilyGravity":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<_Effect_EffectObjectUnit> QuickSave_Effect_ScriptsList =
                                            new List<_Effect_EffectObjectUnit>(UserSource.Source_BattleObject._Effect_Effect_Dictionary.Values);
                                            foreach (_Effect_EffectObjectUnit Effect in QuickSave_Effect_ScriptsList)
                                            {
                                                List<string> QuickSave_Tag_StringList =
                                                    Effect.Key_EffectTag(UserSource, TargetSource);
                                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Spin" };
                                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                                {
                                                    string QuickSave_ValueKey_String =
                                                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                    string QuickSave_Key_String =
                                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                    float QuickSave_Value_Float =
                                                        _World_Manager.Key_NumbersUnit("Default",
                                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                    Answer_Return_Float += QuickSave_Value_Float;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;

            case "Occupancy":
                switch (_Basic_Type_String)
                {
                    case "Passive":
                        switch (Key[1])
                        {
                            case "Size":
                            case "Form":
                            case "Weight":
                            case "Purity":
                                {
                                    switch (_Basic_Key_String)
                                    {
                                        case "Passive_Material_MaterialSorting":
                                        case "Passive_Material_MaterialManagement":
                                        case "Passive_Material_MaterialCarrying":
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float *= QuickSave_Value_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
                }
                break;
            #endregion

            #region - Data -
            case "StackLimit"://Key[1]不一樣喔(為Effect的Key)!
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Common -
                                #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Arthropod_CarapaceCarrier":
                                    {
                                        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
                                            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key[1]];
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key[1], out _Effect_EffectObjectUnit Effect) ?
                                                Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Carapace" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float *= QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;

            #endregion

            #region - Battle -
            case "AttackNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Object_SummonedLoyalty_0":
                                    {
                                        //目標為Record對象
                                        if (TargetSource.Source_BattleObject ==
                                            _Basic_SaveData_Class.ObjectDataGet(_Basic_Key_String))
                                        {
                                            switch (Key[1])
                                            {
                                                case "StarkDamage":
                                                    {

                                                    }
                                                    break;
                                                default:
                                                    {
                                                        Answer_Return_Float *= 0;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    break;

                                case "EffectObject_Spin_Spin_0":
                                case "EffectObject_Spin_EndlessSpin_0":
                                case "EffectObject_Hide_Hide_0":
                                case "EffectObject_Hide_AlphaHide_0":
                                    {
                                        //使用為自身
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        string QuickSave_ValueKey_String =
                                            "Percentage_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float *= (1 + QuickSave_Value_Float);
                                    }
                                    break;

                                case "EffectObject_Limu_AdjustForce_0":
                                case "EffectObject_Limu_AdjustForce_1":
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        Answer_Return_Float *= QuickSave_Value_Float;
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;
            case "HealNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Bless_LivingBlessing":
                                    {
                                        //對象為驅動自身者
                                        if (TargetSource == null)
                                        {
                                            break;
                                        }
                                        if (TargetSource.Source_BattleObject ==
                                            _Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script)
                                        {
                                            //提升數值
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;

            case "Consume":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Frugal":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                UserSource.Source_Creature._Card_UsingObject_Script.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Item" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                                Answer_Return_Float += QuickSave_Value_Float;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "PursuitNumber":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Object_SummonedLoyalty_0":
                                    {
                                        //目標為Record對象
                                        if (TargetSource.Source_BattleObject == 
                                            _Basic_SaveData_Class.ObjectDataGet(_Basic_Key_String))
                                        {
                                            switch (Key[1])
                                            {
                                                case "StarkDamage":
                                                    {

                                                    }
                                                    break;
                                                default:
                                                    {
                                                        Answer_Return_Float = 0;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
                break;

            case "Path":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Ground_StableFoundation":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //不可移動
                                            Answer_Return_Float = 0;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                }
                break;
            case "Shift":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Ground_StableFoundation":
                                    {
                                        //使用該物件
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //不可推拉
                                            Answer_Return_Float = 0;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
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
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class = null;
        if (_World_Manager._Effect_Manager._Data_EffectObject_Dictionary.TryGetValue(
            Key, out _Effect_Manager.EffectDataClass EffectData))
        {
            QuickSave_EffectData_Class = EffectData;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {

                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Sleag_SleagInstinct":
                                    {
                                        //對象為自身
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            if (UserSource.Source_EffectObject != null)
                                            {
                                                List<string> QuickSave_Tag_StringList =
                                                    TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Poison" };
                                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                                {
                                                    Answer_Return_Float = 0;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_Maid_ProfessionalMaid":
                                    {
                                        //對象為自身
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            if (UserSource.Source_EffectObject != null)
                                            {
                                                List<string> QuickSave_Tag_StringList =
                                                    TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Filth" };
                                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                                {
                                                    Answer_Return_Float = 0;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
                        {

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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Type_String)
                    {
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "EffectObject":
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
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //通用----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Type_String)
                    {
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                    case "Passive_Object_RealEntity":
                                        {
                                            if (TargetSource.Source_Creature == null)
                                            {
                                                Answer_Return_Bool = false;
                                            }
                                            else
                                            {
                                                if (UserSource.Source_Creature != TargetSource.Source_Creature)
                                                {
                                                    Answer_Return_Bool = false;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject":
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
                        case "SpecialAffix":
                            {
                                switch (_Basic_Key_String)
                                {
                                }
                            }
                            break;
                        case "Passive":
                            {
                                switch (_Basic_Key_String)
                                {
                                    case "Passive_Object_RealEntity":
                                        {
                                            if (TargetSource.Source_Creature == null)
                                            {
                                                Answer_Return_Bool = false;
                                            }
                                            else
                                            {
                                                if (UserSource.Source_Creature != TargetSource.Source_Creature)
                                                {
                                                    Answer_Return_Bool = false;
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
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
        if (UserSource == _Basic_Source_Class)
        {
            return Answer_Return_StringList;//避免重複
        }
        switch (Type)
        {
            case "Tag"://物件
                switch (_Basic_Type_String)
                {
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Rot -
                                case "SpecialAffix_Rot_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Glue");
                                    }
                                    break;
                                #endregion

                                #region - Offline -
                                case "SpecialAffix_Offline_0":
                                case "SpecialAffix_Offline_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "String");
                                    }
                                    break;
                                #endregion

                                #region - Toxicity -
                                case "SpecialAffix_Toxicity_0":
                                case "SpecialAffix_Toxicity_1":
                                case "SpecialAffix_Toxicity_2":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Toxic");
                                    }
                                    break;
                                #endregion

                                #region - Toxicity -
                                case "SpecialAffix_Tie_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Polymer");
                                    }
                                    break;
                                #endregion

                                #region - Hypersecretion -
                                case "SpecialAffix_Hypersecretion_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Glue");
                                    }
                                    break;
                                #endregion

                                #region - Stench -
                                case "SpecialAffix_Stench_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Volatile");
                                    }
                                    break;
                                #endregion

                                #region - Germination -
                                case "SpecialAffix_Germination_0":
                                case "SpecialAffix_Germination_1":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Herb");
                                    }
                                    break;
                                #endregion

                                #region - WithFruits -
                                case "SpecialAffix_WithFruits_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Fruit");
                                    }
                                    break;
                                #endregion

                                #region - Doping -
                                case "SpecialAffix_Doping_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Gem");
                                    }
                                    break;
                                #endregion

                                #region - Penetration -
                                case "SpecialAffix_Penetration_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Liquid");
                                    }
                                    break;
                                #endregion

                                #region - WoodenEchoes -
                                case "SpecialAffix_WoodenEchoes_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Wood");
                                    }
                                    break;
                                #endregion

                                #region - Illuminator -
                                case "SpecialAffix_Glow_0":
                                    {
                                        if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        Answer_Return_StringList.Add("TagAdd｜" + "Illuminator");
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
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
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_FlashingTachi_WoodSoul":
                                    {
                                        //使用為自身
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //標籤判定
                                            _Map_BattleObjectUnit QuickSave_Object_Script =
                                                UserSource.Source_Creature._Card_UsingObject_Script;
                                            List<string> QuickSave_Tag_StringList =
                                                QuickSave_Object_Script.Key_Tag(UserSource, TargetSource);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Wood" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                Answer_Return_StringList.Add("TagAdd｜" + "Wood");
                                            }
                                            break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
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
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                                #region - Toxicity -
                                case "SpecialAffix_Tie_0":
                                    {
                                        Answer_Return_StringList.Add("TagRemove｜" + "String");
                                        Answer_Return_StringList.Add("TagRemove｜" + "Rope");
                                    }
                                    break;
                                #endregion

                                #region - Hypersecretion -
                                case "SpecialAffix_Hypersecretion_0":
                                    {
                                        Answer_Return_StringList.Add("TagRemove｜" + "Glue");
                                    }
                                    break;
                                    #endregion
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_FlashingTachi_WoodSoul":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //標籤判定
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                        UserSource.Source_Creature._Card_UsingObject_Script;
                                    List<string> QuickSave_Tag_StringList =
                                        QuickSave_Object_Script.Key_Tag(UserSource, TargetSource);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Wood" };
                                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        Answer_Return_String =
                                            Answer_Return_String.Replace("SlashDamage", "ImpactDamage");
                                        Answer_Return_String =
                                            Answer_Return_String.Replace("PunctureDamage", "ImpactDamage");

                                    }
                                    break;
                                }
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Item_QuickUse":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    string QuickSave_ValueKey_String =
                                        "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                    Answer_Return_StringList.AddRange(
                                        _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                        "Priority", Mathf.RoundToInt(QuickSave_Value_Float), 
                                        _Basic_Owner_Script._Skill_Faction_Script.Cards(),
                                        _Basic_Source_Class, _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                        HateTarget, Action, Time, Order));
                                }
                            }
                            break;
                        case "Passive_Identity_Leadership":
                            {
                                //自身以外
                                if (UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //自身相關
                                if (UserSource.Source_Creature != _Basic_Source_Class.Source_Creature)
                                {
                                    break;
                                }
                                //對象獲得效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Identity_LeadershipInfluence_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    UserSource, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    UserSource, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "Passive_FlashingTachi_SenkoBatto":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //標籤判定
                                    List<string> QuickSave_Tag_StringList =
                                        UsingObject.Key_Tag(UserSource, TargetSource);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Scabbard" };
                                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        //給予自身效果
                                        string QuickSave_ValueKey_String =
                                            "ConstructNumber_EffectObject_FlashingTachi_SunriseSanko_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, _Basic_Source_Class,
                                            HateTarget, Action, Time, Order));

                                        //優先抽卡
                                        QuickSave_ValueKey_String =
                                            "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                        Answer_Return_StringList.AddRange(_World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                            "Priority", Mathf.RoundToInt(QuickSave_Value_Float), _Basic_Owner_Script._Skill_Faction_Script.Cards(),
                                            _Basic_Source_Class, _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                            HateTarget, Action, Time, Order));
                                    }
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_FlashingTachi_CarvingOfSun_0":
                            {
                                //目標為指定
                                if (TargetSource.Source_BattleObject !=
                                    _Basic_SaveData_Class.ObjectDataGet(_Basic_Key_String))
                                {
                                    break;
                                }
                                //造成傷害
                                Answer_Return_StringList.AddRange(_Basic_Owner_Script._Basic_Source_Class.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order), 
                                        UsingObject,
                                        HateTarget, Action, Time, Order));
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
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Identity_Selfish":
                                    {
                                        //自身對象為自身
                                        if (UsingObject == UserSource.Source_BattleObject)
                                        {
                                            if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                            {
                                                //對象回復
                                                string QuickSave_ValueKey_String =
                                                    "HealNumber_CatalystPoint_Type0｜User_Concept_Default_Status_Catalyst_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                                string QuickSave_ValueKey02_String =
                                                    "HealTimes_CatalystPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key02_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value02_Float =
                                                    _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                                    _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                                Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                                    "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                    HateTarget, Action, Time, Order, Action));

                                                //對象抽卡
                                                QuickSave_ValueKey_String =
                                                    "Deal_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_StringList.AddRange(
                                                    _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                                        "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                                        _Basic_Source_Class, TargetSource, _Basic_Source_Class.Source_BattleObject,
                                                        HateTarget, Action, Time, Order));
                                            }
                                        }
                                    }
                                    break;
                                case "Passive_Malogic_ChantConductor":
                                    {
                                        //驅動者獲得效果時
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script)
                                        {
                                            //自身同步獲得效果
                                            _World_Manager._Effect_Manager.
                                                GetEffectObject(
                                                Key, Mathf.RoundToInt(IncreaseStack),
                                                _Basic_Source_Class, _Basic_Source_Class,
                                                HateTarget, Action, Time, Order);
                                        }
                                    }
                                    break;
                                case "Passive_OrganicGauntlets_BiologicalFusion":
                                    {
                                        //自身獲得效果時
                                        if (_Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script == null)
                                        {
                                            break;
                                        }
                                        //效果類型
                                        List<string> QuickSave_Tag_StringList =
                                            TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                            Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "StanceEffect" };
                                        if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            break;
                                        }
                                        //驅動者獲得效果時
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script)
                                        {
                                            //自身同步獲得效果
                                            _World_Manager._Effect_Manager.
                                                GetEffectObject(
                                                Key, Mathf.RoundToInt(IncreaseStack),
                                                _Basic_Source_Class, _Basic_Source_Class,
                                                HateTarget, Action, Time, Order);
                                        }
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            //驅動者同步獲得效果
                                            _World_Manager._Effect_Manager.
                                                GetEffectObject(
                                                Key, Mathf.RoundToInt(IncreaseStack),
                                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script._Basic_Source_Class,
                                                HateTarget, Action, Time, Order);
                                        }
                                    }
                                    break;
                            }

                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Object_SummonedLoyalty_0":
                                    {
                                        //對象為自身
                                        if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //獲得自效果
                                        if (_Basic_Key_String != Key)
                                        {
                                            break;
                                        }
                                        print("姑且OK?");
                                        //紀錄/給予該效果 來源者
                                        _Basic_SaveData_Class.ObjectDataSet(_Basic_Key_String, UserSource.Source_Creature._Basic_Object_Script);
                                    }
                                    break;

                                case "EffectObject_FlashingTachi_CarvingOfSun_0":
                                    {
                                        //對象為自身
                                        if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                        {
                                            break;
                                        }
                                        //獲得自效果
                                        if (_Basic_Key_String != Key)
                                        {
                                            break;
                                        }
                                        print("姑且OK?");
                                        //紀錄/給予該效果 使用者
                                        _Basic_SaveData_Class.ObjectDataSet(_Basic_Key_String, UsingObject);
                                    }
                                    break;
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
    public List<string> Key_Effect_LostEffect(string Type,string Key, int BeforeStack, int DecreaseStack,
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
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "Passive_Quality_ConsciousnessLife":
                                    {
                                        if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                                Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "DominateEffect" };
                                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                //傷害自身
                                                Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                                    "Set", "Medium", -65535, 16,
                                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                    HateTarget, Action, Time, Order, Action));
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "EffectObject":
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
                    case "SpecialAffix":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "Passive":
                        {
                            switch (_Basic_Key_String)
                            {
                            }
                        }
                        break;
                    case "EffectObject":
                        {
                            switch (_Basic_Key_String)
                            {
                                case "EffectObject_Identity_ConcernsAboutUnsightlyDirt_0":
                                    {
                                        if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                                Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Filth" };
                                            if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                            {
                                                break;
                                            }
                                            //自身獲得效果
                                            string QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Identity_Satisfaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.GetEffectObject(
                                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                                HateTarget, Action, Time, Order));

                                            //層數減少
                                            if (Action)
                                            {
                                                StackDecrease("Set", 1);
                                            }
                                        }
                                    }
                                    break;
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
            case "SpecialAffix":
                {
                    #region - Gather -
                    switch (_Basic_Key_String)
                    {
                        case "SpecialAffix_Gather_0":
                            {
                                if (IsStandby)
                                {
                                    //是否已經生成
                                    Vector QuickSave_Pos_Class = 
                                        _Basic_Owner_Script.
                                        TimePosition(_Map_BattleRound._Round_Time_Int,_Map_BattleRound._Round_Order_Int);
                                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                        _World_Manager._Object_Manager.
                                        TimeObjects("All", _Basic_Source_Class,
                                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, QuickSave_Pos_Class);
                                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                    {
                                        List<string> QuickSave_Tag_StringList =
                                                Object.Key_Tag(Object._Basic_Source_Class, null);
                                        List<string> QuickSave_CheckTag_StringList =
                                            _Basic_Owner_Script.Key_Tag(_Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class);
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, false))
                                        {
                                            string QuickSave_ValueKey_String =
                                                "HealNumber_MediumPoint｜User_Default_Default_Status_ConstructValue_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                            string QuickSave_Type_String = QuickSave_Key_String.Split("｜"[0])[0].Split("_"[0])[1];

                                            string QuickSave_ValueKey02_String =
                                                "HealTimes_MediumPoint｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key02_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                                _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            float QuickSave_Value02_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                                _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                            _Basic_Owner_Script.PointSet(
                                                "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                                _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                null, true,_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                    #endregion
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Identity_Lazy":
                            {
                                if (IsStandby)
                                {
                                    if (_Basic_Source_Class.Source_Creature == null)
                                    {
                                        break;
                                    }
                                    SourceClass ConceptSource =
                                        _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                        _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;

                                    //機率發動
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, ConceptSource, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, ConceptSource, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        //概念丟棄卡片
                                        QuickSave_ValueKey_String =
                                            "Throw_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, ConceptSource, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, ConceptSource, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                        _World_Manager._UI_Manager._UI_CardManager.
                                            CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                            _Basic_Source_Class, ConceptSource, _Basic_Source_Class.Source_BattleObject,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    }
                                }
                            }
                            break;

                        case "Passive_Identity_Leisurely":
                            {
                                if (IsStandby)
                                {
                                    //還原變數
                                    if (!_Basic_SaveData_Class.BoolDataGet(_Basic_Key_String))
                                    {
                                        _Basic_SaveData_Class.BoolDataSet(_Basic_Key_String, true);
                                    }
                                }
                            }
                            break;

                        case "Passive_Identity_Mysophobia":
                            {
                                //確認地塊物件效果
                                Vector QuickSave_Pos_Class = 
                                    _Basic_Source_Class.Source_BattleObject.
                                    TimePosition(_Map_BattleRound._Round_Time_Int,_Map_BattleRound._Round_Order_Int);
                                _Map_BattleGroundUnit QuickSave_Ground_Script =
                                    _World_Manager._Map_Manager._Map_BattleCreator.
                                    _Map_GroundBoard_ScriptsArray[QuickSave_Pos_Class.X, QuickSave_Pos_Class.Y];
                                List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                    new List<_Map_BattleObjectUnit>();
                                QuickSave_Objects_ScriptsList.Add(QuickSave_Ground_Script._Basic_Object_Script);
                                QuickSave_Objects_ScriptsList.AddRange(
                                    _World_Manager._Object_Manager.
                                    TimeObjects("All", _Basic_Source_Class,
                                    _Map_BattleRound._Round_Time_Int,_Map_BattleRound._Round_Order_Int, QuickSave_Pos_Class));

                                foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                {
                                    foreach (_Effect_EffectObjectUnit Effect in Object._Effect_Effect_Dictionary.Values)
                                    {
                                        //標籤判定
                                        List<string> QuickSave_Tag_StringList =
                                        Effect.Key_EffectTag(Object._Basic_Source_Class, _Basic_Source_Class);
                                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Filth" };
                                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                        {
                                            //給予自身效果
                                            string QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Quality_Wet_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, null,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                                            _World_Manager._Effect_Manager.
                                                GetEffectObject(
                                                QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, _Basic_Source_Class,
                                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                            return;
                                        }
                                    }
                                }
                            }
                            break;

                        case "Passive_Stone_StoneCarapace":
                            {
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                    _Basic_Source_Class.Source_BattleObject.Key_Tag(_Basic_Source_Class, _Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    //給予自身效果
                                    string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Stone_StoneCarapace_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default", QuickSave_Key_String,
                                        _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                                    _World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                        _Basic_Source_Class, _Basic_Source_Class,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                            }
                            break;

                        case "Passive_ScarletKin_ScarletInduction":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Scarlet_ScarletBunch_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;

                        case "Passive_LanspidDagger_ExistenceDilution":
                            {
                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Hide_Hide_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");
                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;

                        case "Passive_SpikeShooter_SpikeBud":
                            {
                                //受驅動時有效
                                if (_Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script == null)
                                {
                                    break;
                                }
                                //比較卡片數量
                                int QuickSave_SkillLeavesCount_Int = 0;
                                if (_Basic_Owner_Script._Skill_Faction_Script._Faction_SkillLeaves_Dictionary.TryGetValue(
                                    "SkillLeaves_SpikeShooter_SpikeBud", out List<_UI_Card_Unit> DicValue))
                                {
                                    QuickSave_SkillLeavesCount_Int = DicValue.Count;
                                }

                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜User_Default_Default_Status_ComplexPoint_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                if (QuickSave_SkillLeavesCount_Int < QuickSave_Value_Float)
                                {
                                    //新增卡片
                                    _UI_Card_Unit QuickSave_Card_Script =
                                        _World_Manager._Skill_Manager.SkillLeafStartSet(
                                            _Basic_Owner_Script._Skill_Faction_Script, "SkillLeaves_SpikeShooter_SpikeBud");
                                    _Basic_Owner_Script._Skill_Faction_Script.AddSkillLeaf(QuickSave_Card_Script, "Deck", true);
                                    _World_Manager._UI_Manager._UI_CardManager.
                                        CardDeal("Target",1, new List<_UI_Card_Unit> { QuickSave_Card_Script },
                                        _Basic_Source_Class, _Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                            }
                            break;
                        case "Passive_OvergrownBall_GrowingMend":
                            {
                                //消耗數值
                                string QuickSave_ValueKey_String =
                                    "Consume_CatalystPoint_Default｜User_Default_Default_Point_MediumPoint_Gap_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    _Basic_Source_Class.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point",
                                    _Basic_Source_Class, _Basic_Source_Class));

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                                //消耗百分比
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //自身治癒
                                QuickSave_ValueKey_String =
                                    "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, QuickSave_ValueData_Float,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                string QuickSave_ValueKey02_String =
                                    "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                _Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, true);
                            }
                            break;

                        case "Passive_Limu_AdjustForce":
                            {
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                    _Basic_Owner_Script.Key_Tag(_Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "MendEffect" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }

                                //自身獲得效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Limu_AdjustForce_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                _World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Quality_PunctureWound_0":
                        case "EffectObject_Quality_Toxin_0":
                            {
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                    _Basic_Owner_Script.Key_Tag(_Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Tissue" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }
                                //造成傷害
                                _Basic_Owner_Script._Basic_Source_Class.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int),
                                        null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            }
                            break;

                        case "EffectObject_Sensation_Stiff_0":
                        case "EffectObject_Sensation_ShortCircuit_0":
                        case "EffectObject_Sensation_Reaction_0":
                            {
                                //移除此效果
                                StackDecrease("Set", 65535);
                            }
                            break;

                        case "EffectObject_Sensation_DeepFear_0":
                            {
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                    _Basic_Owner_Script.Key_Tag(_Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Spirit" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    //目標丟棄卡牌
                                    string QuickSave_ValueKey_String =
                                        "Throw_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                    _World_Manager._UI_Manager._UI_CardManager.
                                        CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                            }
                            break;

                        case "EffectObject_Constrain_Bind_0":
                            {
                                //待命判定
                                if (!IsStandby)
                                {
                                    break;
                                }
                                //層數消除
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜User_Default_Default_Status_Strength_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, null,
                                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                StackDecrease("Set", Mathf.RoundToInt(QuickSave_Value_Float));
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Spin_GravityPath":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                List<_Effect_EffectObjectUnit> QuickSave_Effect_ScriptsList =
                                    new List<_Effect_EffectObjectUnit>(UsingObject._Effect_Effect_Dictionary.Values);
                                foreach (_Effect_EffectObjectUnit Effect in QuickSave_Effect_ScriptsList)
                                {
                                    List<string> QuickSave_Tag_StringList =
                                        Effect.Key_EffectTag(_Basic_Source_Class, _Basic_Source_Class);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Spin" };
                                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        break;
                                    }
                                    //機率發動
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default｜User_Default_Default_Material_Weight_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        //自身獲得效果
                                        QuickSave_ValueKey_String =
                                            "ConstructNumber_EffectObject_Spin_Spin_0｜User_Default_Default_Material_Weight_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                        _World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, _Basic_Source_Class,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    }
                                    break;
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Stone_DelaySlate_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //卡片
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                    Key_OwnTag(_Basic_Source_Class, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Spell|Stone)" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }
                                //已可被移除
                                _Basic_SaveData_Class.BoolDataSet(_Basic_Key_String, true);
                            }
                            break;
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
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
        switch (_Basic_Key_String)
        {
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Ink_InkSpray_0":
                            {
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                    _Basic_Owner_Script.Key_Tag(_Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(ViewEye:LightEye)" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    //卡片
                                    if (TargetSource.Source_Card == null)
                                    {
                                        break;
                                    }

                                    int QuickSave_DeckSize_Int =
                                        UserSource.Source_Creature._Card_CardsDeck_ScriptList.Count;
                                    //次數限制 - 只有第一次會決定當回合不可用卡片
                                    if (_Basic_TimesLimit_Class.TimesLimit("Round", 1))
                                    {
                                        //--產生隨機值
                                        List<float> QuickSave_RandomNumber_FloatList =
                                            new List<float>();
                                        while (QuickSave_RandomNumber_FloatList.Count < QuickSave_DeckSize_Int)
                                        {
                                            int QuickSave_Number_Int = UnityEngine.Random.Range(0, QuickSave_DeckSize_Int);
                                            if (!QuickSave_RandomNumber_FloatList.Contains(QuickSave_Number_Int))
                                            {
                                                QuickSave_RandomNumber_FloatList.Add(QuickSave_Number_Int);
                                            }
                                        }
                                        _Basic_SaveData_Class.ValueListDataSet(_Basic_Key_String, QuickSave_RandomNumber_FloatList);
                                    }

                                    //X張卡片無法使用
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    List<float> QuickSave_ValueListData_FloatList =
                                        new List<float>(_Basic_SaveData_Class.ValueListDataGet(_Basic_Key_String)).GetRange(0, (int)QuickSave_Value_Float);
                                    if (QuickSave_ValueListData_FloatList.Contains(TargetSource.Source_Card._Card_Position_Int))
                                    {
                                        Answer_Return_Bool = false;
                                    }
                                }
                            }
                            break;
                        case "EffectObject_Constrain_Bind_0":
                            {
                                //來源/使用為自身
                                if 
                                    (UserSource.Source_BattleObject != UsingObject &&
                                    (UserSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject ||
                                    UsingObject == _Basic_Source_Class.Source_BattleObject))
                                {
                                    //卡片
                                    if (TargetSource.Source_Card == null)
                                    {
                                        break;
                                    }
                                    //標籤判定(否定)
                                    List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_Card._Card_BehaviorUnit_Script.
                                        Key_OwnTag(TargetSource, UsingObject, true);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "AntiConstrain" };
                                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        Answer_Return_Bool = false;
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
            case "SpecialAffix":
                {
                    string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                    switch (_Basic_Key_String)
                    {
                        case "SpecialAffix_Shatter_0":
                        case "SpecialAffix_Shatter_1":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //卡片
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //消耗數值
                                string QuickSave_ValueKey_String =
                                    "Consume_MediumPoint_Point｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                float QuickSave_Consume_Float =
                                    Mathf.Clamp(QuickSave_Value_Float, 0,
                                    TargetSource.Source_BattleObject.
                                    Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, TargetSource));

                                TargetSource.Source_BattleObject.PointSet(
                                    "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action);
                                //ValueDataSet("Consume", QuickSave_Consume_Float);
                                //目標受到傷害
                                Answer_Return_StringList.AddRange(_Basic_Owner_Script._Basic_Source_Class.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order), 
                                        UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Fluorescent_StarFalling":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //給予地塊所有物件效果
                                    string QuickSave_ValueKey_String =
                                        "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, null, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, null, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    Vector QuickSave_Pos_Class = _Basic_Source_Class.Source_BattleObject.TimePosition(Time,Order);
                                    _Map_BattleGroundUnit QuickSave_Ground_Script =
                                        _World_Manager._Map_Manager._Map_BattleCreator.
                                        _Map_GroundBoard_ScriptsArray[QuickSave_Pos_Class.X, QuickSave_Pos_Class.Y];
                                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                        new List<_Map_BattleObjectUnit>();
                                    QuickSave_Objects_ScriptsList.Add(QuickSave_Ground_Script._Basic_Object_Script);
                                    QuickSave_Objects_ScriptsList.AddRange(
                                        _World_Manager._Object_Manager.
                                        TimeObjects("Normal",_Basic_Source_Class,
                                        Time, Order, QuickSave_Pos_Class));

                                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                    {
                                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, Object._Basic_Source_Class,
                                            HateTarget, Action, Time ,Order));
                                    }
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Stone_DelaySlate_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //卡片
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //尚未發動過
                                if (!_Basic_SaveData_Class.BoolDataGet(_Basic_Key_String))
                                {
                                    break;
                                }
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                    Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Spell|Stone)" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }
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
    //受擊增減值 /格檔前
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_ScarletKin_ScarletKinScute":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                        case "PunctureDamage":
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_Float += -QuickSave_Value_Float;
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Cnidocytes_CnidocytesAllergy_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //卡片
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //標籤判定
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                    Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Cnidocytes" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                    Answer_Return_Float += QuickSave_Value_Float;
                                }
                            }
                            break;
                        case "EffectObject_Soar_Soar_0":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜Target_Default_Default_Material_Weight_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                Answer_Return_Float += QuickSave_Value_Float;
                            }
                            break;
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Quality_Weeken":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "StarkDamage":
                                            {

                                            }
                                            break;
                                        default:
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_Float *= (1 + QuickSave_Value_Float);
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Passive_Maid_ProfessionalMaid":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "ChaosDamage":
                                            {
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜User_Concept_Default_Status_Consciousness_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_Float *= (1 - QuickSave_Value_Float);
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Passive_ScarletKin_ScarletKinScute":
                            {
                                //對象為驅動者時
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject._Basic_DrivingOwner_Script)
                                {
                                    //分攤傷害
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                        case "PunctureDamage":
                                            {
                                                //自身百分比傷害分擔
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_Float *= (1 - QuickSave_Value_Float);

                                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value * QuickSave_Value_Float);
                                                _Basic_SaveData_Class.StringDataSet(_Basic_Key_String, DamageType);
                                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.
                                                    Damaged(
                                                    Key_Pursuit(
                                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                                        HateTarget, Action, Time, Order), 
                                                        UsingObject,
                                                        HateTarget, Action, Time, Order));
                                            }
                                            break;
                                        case "ImpactDamage":
                                        case "ChaosDamage":
                                            {
                                                //自身百分比傷害分擔
                                                string QuickSave_ValueKey_String =
                                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                                Answer_Return_Float *= (1 - QuickSave_Value_Float);

                                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value * QuickSave_Value_Float);
                                                _Basic_SaveData_Class.StringDataSet(_Basic_Key_String, DamageType);
                                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.
                                                    Damaged(
                                                    Key_Pursuit(
                                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                                        HateTarget, Action, Time, Order),
                                                        UsingObject, 
                                                        HateTarget, Action, Time, Order));
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Passive_Ground_Unbreakable":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //免疫一切傷害
                                    Answer_Return_Float *= 0;
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Survival -
                        case "EffectObject_Survival_SlashAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "SlashDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_PunctureAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "PunctureDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_ImpactAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "ImpactDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_EnergyAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "EnergyDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_ChaosAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "ChaosDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_AbstractAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "AbstractDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Survival_StarkAdapt_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "StarkDamage":
                                        {
                                            //固定數值
                                            string QuickSave_ValueKey_String =
                                                "Value_Default_Default｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float += -QuickSave_Value_Float;
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion
                        case "EffectObject_Vine_RattanCocoon_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "ImpactDamage":
                                    case "ChaosDamage":
                                    case "AbstractDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            Answer_Return_Float *= (1 - QuickSave_Value_Float);
                                        }
                                        break;
                                    case "SlashDamage":
                                    case "PunctureDamage":
                                    case "EnergyDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            Answer_Return_Float *= (1 - QuickSave_Value_Float);
                                        }
                                        break;
                                }
                            }
                            break;
                        case "EffectObject_Fighting_FifthHighParry_0":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //卡片
                                if (UserSource.Source_Card == null)
                                {
                                    break;
                                }
                                //標籤判斷
                                List<string> QuickSave_Tag_StringList =
                                        UserSource.Source_Card._Card_BehaviorUnit_Script.
                                        Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Upward" };
                                if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    break;
                                }

                                switch (DamageType)
                                {
                                    case "SlashDamage":
                                    case "ImpactDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float *= (1 - QuickSave_Value_Float);

                                            //自身(對象)獲得效果
                                            QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Sensation_Reaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, TargetSource, 
                                                HateTarget, Action, Time, Order));
                                        }
                                        break;
                                    case "EnergyDamage":
                                    case "ChaosDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                            Answer_Return_Float *= (1 - QuickSave_Value_Float);

                                            //自身(對象)獲得效果
                                            QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Sensation_Reaction_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, TargetSource, 
                                                HateTarget, Action, Time, Order));
                                        }
                                        break;
                                }
                            }
                            break;
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
                case "SpecialAffix":
                    {
                        switch (_Basic_Key_String)
                        {
                        }
                    }
                    break;
                case "Passive":
                    {
                        switch (_Basic_Key_String)
                        {
                        }

                    }
                    break;
                case "EffectObject":
                    {
                        switch (_Basic_Key_String)
                        {
                            case "EffectObject_Quality_CatalystOverDrive_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "ChaosDamage":
                                        case "AbstractDamage":
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                //阻擋量
                                                float QuickSave_BlockValue_Float =
                                                    Mathf.Clamp(Answer_Return_Float, 0, (QuickSave_Value_Float * _Effect_Stack_Int));
                                                //層數移除
                                                int QuickSave_EffectConsume_Int =
                                                    Mathf.CeilToInt(QuickSave_BlockValue_Float / QuickSave_Value_Float);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", QuickSave_EffectConsume_Int);
                                                }

                                                Answer_Return_Float += -QuickSave_BlockValue_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "EffectObject_Carapace_ShellbornBurden_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                        case "PunctureDamage":
                                        case "ImpactDamage":
                                        case "ChaosDamage":
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                //阻擋量
                                                float QuickSave_BlockValue_Float =
                                                    Mathf.Clamp(Answer_Return_Float, 0, (QuickSave_Value_Float * _Effect_Stack_Int));
                                                //層數移除
                                                int QuickSave_EffectConsume_Int =
                                                    Mathf.CeilToInt(QuickSave_BlockValue_Float / QuickSave_Value_Float);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", QuickSave_EffectConsume_Int);
                                                }

                                                Answer_Return_Float += -QuickSave_BlockValue_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "EffectObject_Guard_DriveGuardian_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "StarkDamage":
                                            {

                                            }
                                            break;
                                        default:
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                //阻擋量
                                                float QuickSave_BlockValue_Float =
                                                    Mathf.Clamp(Answer_Return_Float, 0, (QuickSave_Value_Float * _Effect_Stack_Int));
                                                //層數移除
                                                int QuickSave_EffectConsume_Int =
                                                    Mathf.CeilToInt(QuickSave_BlockValue_Float / QuickSave_Value_Float);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", QuickSave_EffectConsume_Int);
                                                }

                                                Answer_Return_Float += -QuickSave_BlockValue_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "EffectObject_Stone_StoneCarapace_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                        case "PunctureDamage":
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                //阻擋量
                                                float QuickSave_BlockValue_Float =
                                                    Mathf.Clamp(Answer_Return_Float, 0, (QuickSave_Value_Float * _Effect_Stack_Int));
                                                //層數移除
                                                int QuickSave_EffectConsume_Int =
                                                    Mathf.CeilToInt(QuickSave_BlockValue_Float / QuickSave_Value_Float);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", QuickSave_EffectConsume_Int);
                                                }

                                                Answer_Return_Float += -QuickSave_BlockValue_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "EffectObject_Stone_StoneSanctuary_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "StarkDamage":
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", 1);
                                                }
                                                Answer_Return_Float += -QuickSave_Value_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "EffectObject_Scarlet_ScarletArmor_0":
                            case "EffectObject_Scarlet_ScarletShield_0":
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                    {
                                        break;
                                    }
                                    switch (DamageType)
                                    {
                                        case "ImpactDamage":
                                        case "ChaosDamage":
                                        case "AbstractDamage":
                                            {
                                                //單層抵抗量
                                                string QuickSave_ValueKey_String =
                                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                                string QuickSave_Key_String =
                                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                float QuickSave_Value_Float =
                                                    _World_Manager.Key_NumbersUnit("Default",
                                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                                //阻擋量
                                                float QuickSave_BlockValue_Float =
                                                    Mathf.Clamp(Answer_Return_Float, 0, (QuickSave_Value_Float * _Effect_Stack_Int));
                                                //層數移除
                                                int QuickSave_EffectConsume_Int =
                                                    Mathf.CeilToInt(QuickSave_BlockValue_Float / QuickSave_Value_Float);
                                                if (Action)
                                                {
                                                    StackDecrease("Set", QuickSave_EffectConsume_Int);
                                                }
                                                Answer_Return_Float += -QuickSave_BlockValue_Float;
                                            }
                                            break;
                                    }
                                }
                                break;
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        case "SpecialAffix_Toxicity_0":
                        case "SpecialAffix_Toxicity_1":
                        case "SpecialAffix_Toxicity_2":
                            {
                                switch (DamageType)
                                {
                                    case "SlashDamage":
                                    case "PunctureDamage":
                                    case "ChaosDamage":
                                        {
                                            if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                            {
                                                break;
                                            }
                                            string QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Quality_Toxin_0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, TargetSource, 
                                                HateTarget, Action, Time, Order));
                                        }
                                        break;
                                }
                            }
                            break;

                        #region - Grece -
                        case "SpecialAffix_Grece_0":
                            {
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "EnergyDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Material_GreceResonance_0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            //給予自身
                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, 
                                                HateTarget, Action, Time, Order));
                                        }
                                        break;
                                }
                            }
                            break;
                            #endregion
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Identity_Ruthless":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //消耗值設定
                                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value);

                                    string QuickSave_PercentageValueKey_String =
                                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_PercentageKey_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_PercentageValue_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    float QuickSave_ValueData_Float =
                                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                    //自身治癒
                                    string QuickSave_ValueKey_String =
                                        "HealNumber_ConsciousnessPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, QuickSave_ValueData_Float,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_ValueKey02_String =
                                        "HealTimes_ConsciousnessPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key02_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value02_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                    Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.PointSet(
                                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                        _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                        HateTarget, Action, Time, Order, Action));
                                }
                            }
                            break;

                        case "Passive_Crescent_TwinMoonArc":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                        case "PunctureDamage":
                                            {
                                                //傷害目標
                                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value);
                                                _Basic_SaveData_Class.StringDataSet(_Basic_Key_String, DamageType);
                                                Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.
                                                    Damaged(
                                                    Key_Pursuit(
                                                        _Basic_Source_Class, TargetSource, UsingObject,
                                                        HateTarget, Action, Time, Order),
                                                        UsingObject,
                                                        HateTarget, Action, Time, Order));
                                            }
                                            break;
                                    }
                                }
                            }
                            break;

                        case "Passive_Guard_TargetSubdue":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //給予對象效果
                                    string QuickSave_ValueKey_String =
                                        "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                        _Basic_Source_Class, TargetSource,
                                        HateTarget, Action, Time, Order));
                                }
                            }
                            break;

                        case "Passive_MemoryPlushToy_InsideSurprise":
                            {
                                if (!Action)
                                {
                                    break;
                                }
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //機率發動
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        //發動次數
                                        QuickSave_ValueKey_String =
                                            "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                        if (!_Basic_TimesLimit_Class.TimesLimit("Round", Mathf.RoundToInt(QuickSave_Value_Float)))
                                        {
                                            break;
                                        }
                                        //給予目標效果
                                        QuickSave_ValueKey_String =
                                            "ConstructNumber_EffectObject_Sensation_Fear_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, TargetSource,
                                            HateTarget, Action, Time, Order));
                                    }
                                }
                            }
                            break;

                        case "Passive_OrganicGauntlets_HungryTouch":
                            {
                                //使用為自身
                                if (UsingObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //給予自身效果
                                    string QuickSave_ValueKey_String =
                                        "ConstructNumber_EffectObject_OrganicGauntlets_HungryTouch_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default", 
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                        _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                        _Basic_Source_Class, _Basic_Source_Class,
                                        HateTarget, Action, Time, Order));
                                }
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Spin_Spin_0":
                        case "EffectObject_Hide_Hide_0":
                        case "EffectObject_Hide_AlphaHide_0":
                        case "EffectObject_Soar_Soar_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (Action)
                                {
                                    StackDecrease("Set", 65535);
                                }
                            }
                            break;
                        case "EffectObject_Spin_EndlessSpin_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                string QuickSave_ValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);

                                if (Action)
                                {
                                    StackDecrease("Set", Mathf.RoundToInt(_Effect_Stack_Int * QuickSave_Value_Float));
                                }
                            }
                            break;

                        case "EffectObject_Vine_Thorn_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //目標受到傷害
                                Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                            }
                            break;
                        case "EffectObject_Dark_DemonSacrifice_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //紀錄造成傷害
                                _Basic_SaveData_Class.
                                    ValueDataSet(_Basic_Key_String, Value + _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, 1));
                            }
                            break;
                        case "EffectObject_Material_GreceResonance_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //層數判定
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                if (_Effect_Stack_Int >= QuickSave_Value_Float)
                                {
                                    //目標受到傷害
                                    Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.
                                        Damaged(
                                        Key_Pursuit(
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order),
                                            UsingObject, 
                                            HateTarget, Action, Time, Order));
                                }
                                if (Action)
                                {
                                    StackDecrease("Set", 65535);
                                }
                            }
                            break;
                        case "EffectObject_FlashingTachi_SunriseSanko_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //目標丟棄卡牌
                                string QuickSave_ValueKey_String =
                                    "Throw_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                _World_Manager._UI_Manager._UI_CardManager.
                                    CardThrow("Random",Mathf.RoundToInt(QuickSave_Value_Float),null,
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                            }
                            break;
                        case "EffectObject_FlashingTachi_SunriseDominate_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //次數判定
                                if (!_Basic_TimesLimit_Class.TimesLimit("Times", 1))
                                {
                                    break;
                                }
                                //目標獲得效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_FlashingTachi_CarvingOfSun_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, TargetSource,
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                        case "EffectObject_OrganicGauntlets_HungryTouch_0":
                            {
                                //使用為自身
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //層數判定
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                if (_Effect_Stack_Int >= QuickSave_Value_Float)
                                {
                                    //目標受到傷害
                                    Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.
                                        Damaged(
                                        Key_Pursuit(
                                            _Basic_Source_Class, TargetSource, UsingObject,
                                            HateTarget, Action, Time, Order),
                                            UsingObject, 
                                            HateTarget, Action, Time, Order));
                                }

                                //自身治癒
                                QuickSave_ValueKey_String =
                                    "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.PointSet(
                                    "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action));
                                //消除效果
                                StackDecrease("Set", 65535);
                            }
                            break;
                        case "EffectObject_Limu_AdjustForce_0":
                            {
                                //命中判定
                                if (!IsSuceess)
                                {
                                    break;
                                }

                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                    _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;

                                //給予概念效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Limu_AdjustForce_1｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                                    GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, ConceptSource,
                                    HateTarget, Action, Time, Order));

                                if (Action)
                                {
                                    StackDecrease("Set", 65535);
                                }
                            }
                            break;
                        case "EffectObject_Limu_AdjustForce_1":
                            {
                                //命中判定
                                if (!IsSuceess)
                                {
                                    break;
                                }

                                if (Action)
                                {
                                    StackDecrease("Set", 65535);
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
    public List<string> Key_Effect_Damaged(string DamageType, float Value, bool IsSuceess,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action,int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Type_String)
        {
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        #region - Lanspid -
                        case "SpecialAffix_Lanspid_0":
                            {
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (UserSource.Source_Card != null)
                                {
                                    List<string> QuickSave_Tag_StringList =
                                            UserSource.Source_Card._Card_BehaviorUnit_Script.
                                            Key_OwnTag(TargetSource, UsingObject, true);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Light" };
                                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                                        string QuickSave_ValueKey_String =
                                            "HealNumber_CatalystPoint｜User_Default_Default_Point_CatalystPoint_Total_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        float QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                        string QuickSave_ValueKey02_String =
                                            "HealTimes_CatalystPoint｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                        string QuickSave_Key02_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                            _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                        float QuickSave_Value02_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                            _Basic_Source_Class, _Basic_Owner_Script._Basic_Source_Class, null,
                                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                        //回復
                                        Answer_Return_StringList.AddRange(UserSource.Source_BattleObject.PointSet(
                                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                            _Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order, Action));
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region - Grece -
                        case "SpecialAffix_Grece_0":
                            {
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                switch (DamageType)
                                {
                                    case "EnergyDamage":
                                        {
                                            string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                                            string QuickSave_ValueKey_String =
                                                "ConstructNumber_EffectObject_Material_GreceResonance_0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                            string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                            //給予自身
                                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, 
                                                HateTarget, Action, Time, Order));
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion

                        #region - Adapt -
                        case "SpecialAffix_Adapt_0":
                            {
                                if (UsingObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Survive_" + DamageType.Replace("Damage", "") + "Adapt_0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                //給予自身
                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, 
                                    HateTarget, Action, Time, Order));
                            }
                            break;
                            #endregion
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Item_SlashReact":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "SlashDamage":
                                            {
                                                //傷害自身
                                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.PointSet(
                                                    "Damage", "Medium", -65535, 16,
                                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                    HateTarget, Action, Time, Order, Action));
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Passive_Item_PunctureReact":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "PunctureDamage":
                                            {
                                                //傷害自身
                                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.PointSet(
                                                    "Damage", "Medium", -65535, 16,
                                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                    HateTarget, Action, Time, Order, Action));
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        case "Passive_Item_EnergyReact":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    switch (DamageType)
                                    {
                                        case "EnergyDamage":
                                            {
                                                //傷害自身
                                                Answer_Return_StringList.AddRange(_Basic_Source_Class.Source_BattleObject.PointSet(
                                                    "Damage", "Medium", -65535, 16,
                                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                                    HateTarget, Action, Time, Order, Action));
                                            }
                                            break;
                                    }
                                }
                            }
                            break;

                        case "Passive_Sensation_SensitivePain":
                        case "Passive_Sensation_NormalPain":
                        case "Passive_Sensation_DullPain":
                        case "Passive_Sensation_NoPain":
                            {
                                //戰鬥時觸發
                                if (_World_Manager._Authority_Scene_String != "Battle")
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }

                                //消耗值設定
                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value);

                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                    UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);

                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Sensation_Stiff_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, QuickSave_ValueData_Float,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(
                                    QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                    _Basic_Source_Class, _Basic_Source_Class,
                                    HateTarget, Action, Time, Order));
                            }
                            break;

                        case "Passive_Sensation_SensitiveDisconnected":
                        case "Passive_Sensation_NormalDisconnected":
                        case "Passive_Sensation_DullDisconnected":
                        case "Passive_Sensation_NoDisconnected":
                            {
                                //戰鬥時觸發
                                if (_World_Manager._Authority_Scene_String != "Battle")
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }

                                //消耗值設定
                                _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value);

                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                                //給予自身效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Sensation_ShortCircuit_0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        case "Passive_Quality_FilledWater":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //給予地塊所有物件效果
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Quality_Wet_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, null, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, null, UsingObject,
                                    HateTarget, Action, Time, Order);

                                string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                Vector QuickSave_Pos_Class = _Basic_Source_Class.Source_BattleObject.TimePosition(Time, Order);
                                _Map_BattleGroundUnit QuickSave_Ground_Script =
                                    _World_Manager._Map_Manager._Map_BattleCreator.
                                    _Map_GroundBoard_ScriptsArray[QuickSave_Pos_Class.X, QuickSave_Pos_Class.Y];
                                List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = new List<_Map_BattleObjectUnit>();
                                QuickSave_Objects_ScriptsList.Add(QuickSave_Ground_Script._Basic_Object_Script);
                                QuickSave_Objects_ScriptsList.AddRange(
                                    _World_Manager._Object_Manager.
                                    TimeObjects("Normal", _Basic_Source_Class,
                                    Time, Order, QuickSave_Pos_Class));

                                foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                {
                                    Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                        _Basic_Source_Class, Object._Basic_Source_Class,
                                        HateTarget, Action, Time, Order));
                                }
                            }
                            break;

                        case "Passive_Identity_Leisurely":
                            {
                                //變數判定
                                if (_Basic_SaveData_Class.BoolDataGet(_Basic_Key_String))
                                {
                                    //對象為自身
                                    if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                    {
                                        //true=未受傷
                                        _Basic_SaveData_Class.BoolDataSet(_Basic_Key_String, false);
                                    }
                                }
                            }
                            break;

                        case "Passive_Identity_Panic":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //傷害所有驅動物
                                List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_DrivingObject_ScriptsList;
                                QuickSave_Objects_ScriptsList.Remove(_Basic_Source_Class.Source_BattleObject);//移除自身
                                foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                {
                                    Answer_Return_StringList.AddRange(Object.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, Object._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject,
                                        HateTarget, Action, Time, Order));
                                }
                            }
                            break;

                        case "Passive_Fluorescent_StarRelease":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //給予地塊所有物件效果
                                    string QuickSave_ValueKey_String =
                                        "ConstructNumber_EffectObject_Fluorescent_FluorescentPoint_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, null, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, null, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    Vector QuickSave_Pos_Class = _Basic_Source_Class.Source_BattleObject.TimePosition(Time, Order);
                                    _Map_BattleGroundUnit QuickSave_Ground_Script =
                                        _World_Manager._Map_Manager._Map_BattleCreator._Map_GroundBoard_ScriptsArray[QuickSave_Pos_Class.X, QuickSave_Pos_Class.Y];
                                    List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList =
                                        new List<_Map_BattleObjectUnit>();
                                    QuickSave_Objects_ScriptsList.Add(QuickSave_Ground_Script._Basic_Object_Script);
                                    QuickSave_Objects_ScriptsList.AddRange(
                                        _World_Manager._Object_Manager.
                                        TimeObjects("Normal", _Basic_Source_Class,
                                        Time, Order, QuickSave_Pos_Class));

                                    foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
                                    {
                                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, Object._Basic_Source_Class,
                                            HateTarget, Action, Time, Order));
                                    }
                                }
                            }
                            break;

                        case "Passive_Meka_Android":
                        case "Passive_Meka_UnusualAndroid":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (_Basic_Source_Class.Source_Creature == null)
                                {
                                    break;
                                }
                                SourceClass ConceptSource =
                                    _Basic_Source_Class.Source_Creature._Object_Inventory_Script.
                                    _Item_EquipConcepts_Script._Basic_Object_Script._Basic_Source_Class;

                                //觸發次數限制
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                if (!_Basic_TimesLimit_Class.TimesLimit("Standby", Mathf.RoundToInt(QuickSave_Value_Float)))
                                {
                                    break;
                                }
                                //概念丟棄卡牌
                                QuickSave_ValueKey_String =
                                    "Throw_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, ConceptSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    UserSource, ConceptSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                _World_Manager._UI_Manager._UI_CardManager.
                                    CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                    _Basic_Source_Class, ConceptSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                            }
                            break;

                        case "Passive_Meka_BattleReactor":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //比較受到傷害量
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default｜User_Default_Default_Point_MediumPoint_Total_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    if (Value >= QuickSave_Value_Float)
                                    {
                                        //給予自身效果
                                        QuickSave_ValueKey_String =
                                            "ConstructNumber_EffectObject_Sensation_BattleReactor_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                        string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                            GetEffectObject(
                                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                            _Basic_Source_Class, _Basic_Source_Class,
                                            HateTarget, Action, Time, Order));
                                    }
                                }
                            }
                            break;

                    }
                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        //受傷害時減少層數
                        case "EffectObject_Arthropod_HiveDominate_0":
                        case "EffectObject_Light_HolyAura_0":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (Action)
                                {
                                    StackDecrease("Set", 1);
                                }
                            }
                            break;
                        //受傷害時移除效果
                        case "EffectObject_Object_ResidualDominate_0":
                        case "EffectObject_Hide_Hide_0":
                        case "EffectObject_Hide_AlphaHide_0":
                        case "EffectObject_Soar_Soar_0":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                if (Action)
                                {
                                    StackDecrease("Set", 65535);
                                }
                            }
                            break;
                        //受傷害時減少層數，並回收意識值
                        case "EffectObject_Float_RoveDominate_0":
                        case "EffectObject_Stone_StoneDominate_0":
                        case "EffectObject_FlashingTachi_SunriseDominate_0":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }

                                if (Action)
                                {
                                    StackDecrease("Set", 1);
                                }

                                //自身(對象)回復
                                string QuickSave_ValueKey_String =
                                    "HealNumber_ConsciousnessPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                string QuickSave_ValueKey02_String =
                                    "HealTimes_ConsciousnessPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default", QuickSave_Key02_String,
                                    _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                    _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                    "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                    _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                    HateTarget, Action, Time, Order, Action));
                            }
                            break;

                        case "EffectObject_Vine_Thorn_0":
                            {
                                //目標為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //來源受到傷害
                                Answer_Return_StringList.AddRange(UserSource.Source_BattleObject.
                                    Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, UserSource, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                                //層數消除
                                switch (DamageType)
                                {
                                    case "SlashDamage":
                                    case "ChaosDamage":
                                        {
                                            string QuickSave_ValueKey_String =
                                                "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                            string QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                            float QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,HateTarget, Action, Time, Order);

                                            if (Action)
                                            {
                                                StackDecrease("Set", Mathf.RoundToInt(_Effect_Stack_Int * QuickSave_Value_Float));
                                            }
                                        }
                                        break;
                                    case "EnergyDamage":
                                        {
                                            if (Action)
                                            {
                                                StackDecrease("Set", 65535);
                                            }
                                        }
                                        break;
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

    #region - Dead -
    public List<string> Key_Effect_DeadResist(bool IsResist, string Type/*死亡時傷害類型(效果、傷害等)*/, float Value,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time,int Order)
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_EternalLighthouse_Immortal":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //抵抗死亡時跳出
                                    if (IsResist)
                                    {
                                        break;
                                    }
                                    //沒有效果
                                    Dictionary<string, _Effect_EffectObjectUnit> QuickSave_Effect_Dictionary =
                                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary;
                                    if (QuickSave_Effect_Dictionary.ContainsKey("EffectObject_EternalLighthouse_PastPlunder_0"))
                                    {
                                        break;
                                    }

                                    //消耗數值
                                    string QuickSave_ValueKey_String =
                                        "Consume_CatalystPoint_Default｜User_Concept_Default_Point_MediumPoint_Total_Default｜0";
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                    float QuickSave_Consume_Float =
                                        Mathf.Clamp(QuickSave_Value_Float, 0,
                                        TargetSource.Source_BattleObject.
                                        Key_Point(QuickSave_Type_String, "Point", _Basic_Source_Class, TargetSource));

                                    //消耗額判定
                                    if (QuickSave_Consume_Float <= 0)
                                    {
                                        break;
                                    }

                                    TargetSource.Source_BattleObject.PointSet(
                                        "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                        _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                        HateTarget, Action, Time, Order, Action);
                                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_Consume_Float);

                                    string QuickSave_PercentageValueKey_String =
                                        "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_PercentageKey_String =
                                        _World_Manager.Key_KeysUnit("Default",
                                        QuickSave_PercentageValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_PercentageValue_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    float QuickSave_ValueData_Float =
                                        _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);
                                    print("Reduce:" + QuickSave_Consume_Float + "\nAdd:" + QuickSave_ValueData_Float);

                                    //給予自身效果
                                    QuickSave_ValueKey_String =
                                        "ConstructNumber_EffectObject_EternalLighthouse_PastPlunder_0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_Effect_String = QuickSave_Key_String.Split("｜"[0])[0].Replace("ConstructNumber_", "");

                                    _World_Manager._Effect_Manager.
                                        GetEffectObject(
                                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                        _Basic_Source_Class, TargetSource,
                                        HateTarget, Action, Time, Order);

                                    //自身治癒
                                    QuickSave_ValueKey_String =
                                        "HealNumber_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, QuickSave_ValueData_Float,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    string QuickSave_ValueKey02_String =
                                        "HealTimes_MediumPoint_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                    string QuickSave_Key02_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);
                                    float QuickSave_Value02_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                                        _Basic_Source_Class, TargetSource, UsingObject, HateTarget, Action, Time, Order);

                                    QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Split("_"[0])[1];

                                    Answer_Return_StringList.AddRange(TargetSource.Source_BattleObject.PointSet(
                                        "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                                        _Basic_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                        HateTarget, Action, Time, Order, Action));

                                    //消除效果
                                    if (Action)
                                    {
                                        foreach (_Effect_EffectObjectUnit Effect in QuickSave_Effect_Dictionary.Values)
                                        {
                                            List<string> QuickSave_Tag_StringList =
                                                Effect.Key_EffectTag(TargetSource, _Basic_Source_Class);
                                            List<string> QuickSave_CheckTag_StringList = new List<string> { "TraumaEffect", "PolluteEffect" };
                                            if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, false))
                                            {
                                                break;
                                            }
                                            Effect.StackDecrease("Set", 65535);
                                        }
                                    }
                                    //抵抗死亡
                                    Answer_Return_Bool = true;
                                }
                            }
                            break;
                    }

                }
                break;
            case "EffectObject":
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
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Item_BreakReact":
                            {
                                //執行判定
                                if (!Action)
                                {
                                    break;
                                }
                                //場景判定
                                if (_World_Manager._Authority_Scene_String != "Battle")
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //數值判定
                                if (Value == 0)
                                {
                                    break;
                                }
                                List<_UI_Card_Unit> QuickSave_Cards_ScriptsList =
                                    _Basic_Owner_Script._Skill_Faction_Script.Cards();
                                _Object_CreatureUnit QuickSave_Creature_Script =
                                    _Basic_Source_Class.Source_Creature;
                                _Map_BattleObjectUnit QuickSave_CreatureObject_Script =
                                    _Basic_Source_Class.Source_Creature._Basic_Object_Script;
                                _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _Basic_Source_Class.Source_BattleObject;
                                Vector QuickSave_CreatureCoordinate_Class =
                                    QuickSave_CreatureObject_Script.TimePosition(Time, Order);
                                Vector QuickSave_Coordinate_Class =
                                    QuickSave_Object_Script.TimePosition(Time, Order);
                                foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                                {
                                    foreach (_Effect_EffectCardUnit Effect in Card._Effect_Effect_ScriptsList)
                                    {
                                        if (Effect._Basic_Key_String == "EffectCard_Item_BreakReact_0")
                                        {
                                            //被使用
                                            switch (Card._Card_NowPosition_String)
                                            {
                                                //case "Deck"://可發動
                                                //case "Board"://可發動
                                                case "Delay":
                                                //case "Cemetery":
                                                case "Exiled":
                                                    return Answer_Return_StringList;
                                            }                                                
                                            //消耗值設定
                                            Card._Card_BehaviorUnit_Script._Basic_SaveData_Class.
                                                ValueDataSet(Card._Card_BehaviorUnit_Script._Basic_Key_String, -Value);
                                            Key_Behavior(Card,
                                                QuickSave_CreatureObject_Script, QuickSave_CreatureCoordinate_Class,
                                                QuickSave_Object_Script, QuickSave_Coordinate_Class);
                                            return Answer_Return_StringList;
                                        }
                                    }
                                }
                            }
                            break;

                        case "Passive_Quality_SelfEnergyDetonation":
                            {
                                //執行判定
                                if (!Action)
                                {
                                    break;
                                }
                                //場景判定
                                if (_World_Manager._Authority_Scene_String != "Battle")
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //排定行為
                                _Object_CreatureUnit QuickSave_Creature_Script =
                                    _Basic_Source_Class.Source_Creature;
                                _Map_BattleObjectUnit QuickSave_CreatureObject_Script =
                                    _Basic_Source_Class.Source_Creature._Basic_Object_Script;
                                _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _Basic_Source_Class.Source_BattleObject;
                                Vector QuickSave_CreatureCoordinate_Class =
                                    QuickSave_CreatureObject_Script.TimePosition(Time, Order);
                                Vector QuickSave_Coordinate_Class =
                                    QuickSave_Object_Script.TimePosition(Time, Order);

                                Key_Behavior(_Basic_Owner_Script._Skill_Faction_Script.
                                    _Faction_ExtendSkillLeaves_Dictionary[_Basic_Key_String][0],
                                    QuickSave_CreatureObject_Script, QuickSave_CreatureCoordinate_Class,
                                    QuickSave_Object_Script, QuickSave_Coordinate_Class);
                            }
                            break;

                        case "Passive_ScarletKin_ScarletBloodline":
                            {
                                //對象為自身
                                if (TargetSource.Source_BattleObject == _Basic_Source_Class.Source_BattleObject)
                                {
                                    //持有相同被動者獲得效果
                                    List<_Map_BattleObjectUnit> QuickSave_AllCreature_ScriptsList =
                                    _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataGet("Concept");
                                    List<_Map_BattleObjectUnit> QuickSave_Object_ScriptsList =
                                        new List<_Map_BattleObjectUnit>();
                                    foreach (_Map_BattleObjectUnit Object in QuickSave_AllCreature_ScriptsList)
                                    {
                                        _Object_CreatureUnit QuickSave_Creature_Script = Object._Basic_Source_Class.Source_Creature;
                                        if (_Basic_Source_Class.Source_Creature != QuickSave_Creature_Script &&
                                            _Basic_Source_Class.Source_Creature._Data_Sect_String == QuickSave_Creature_Script._Data_Sect_String)
                                        {
                                            string QuickSave_TargetKey_String = "Passive_ScarletKin_ScarletBloodline";
                                            if (Object._Effect_Passive_Dictionary.
                                                TryGetValue(QuickSave_TargetKey_String, out _Effect_EffectObjectUnit DicValue))
                                            {
                                                QuickSave_Object_ScriptsList.Add(Object);
                                            }
                                        }
                                    }

                                    if (_Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script.
                                        _Effect_Effect_Dictionary.TryGetValue("EffectObject_Scarlet_ScarletBunch_0", out _Effect_EffectObjectUnit Effect))
                                    {
                                        int QuickSave_EffectStack_Int =
                                            Effect.Key_Stack("Default", null, null, null, null);
                                        int[] QuickSave_ObjectAdd_IntArray = new int[QuickSave_Object_ScriptsList.Count];
                                        for (int a = 0; a < QuickSave_EffectStack_Int; a++)
                                        {
                                            QuickSave_ObjectAdd_IntArray[UnityEngine.Random.Range(0, QuickSave_ObjectAdd_IntArray.Length)]++;
                                        }
                                        for (int a = 0; a < QuickSave_ObjectAdd_IntArray.Length; a++)
                                        {
                                            _World_Manager._Effect_Manager.
                                                GetEffectObject(
                                                "EffectObject_Scarlet_ScarletBunch_0", QuickSave_ObjectAdd_IntArray[a],
                                                _Basic_Source_Class, QuickSave_Object_ScriptsList[a]._Basic_Source_Class,
                                                HateTarget, Action, Time, Order);
                                        }
                                    }
                                }
                            }
                            break;

                        case "Passive_Ground_WreckageOfDestruction．GloomyStrataRock":
                            {
                                //是否執行
                                if (!Action)
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_GloomyStrata_Rock｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet
                                        ("Object", QuickSave_Type_String,
                                        UsingObject.TimePosition(Time, Order), null, Time, Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;

                        case "Passive_Ground_WreckageOfDestruction．GloomyStrataBoulder":
                            {
                                //是否執行
                                if (!Action)
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_GloomyStrata_Boulder｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet
                                        ("Object", QuickSave_Type_String,
                                        UsingObject.TimePosition(Time, Order), null, Time, Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;

                        case "Passive_Ground_WreckageOfDestruction．AlbinoCoroneDust":
                            {
                                //是否執行
                                if (!Action)
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_AlbinoCorone_Dust｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet
                                        ("Object", QuickSave_Type_String,
                                        UsingObject.TimePosition(Time, Order), null, Time, Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;

                        case "Passive_Ground_WreckageOfDestruction．MetalPoleLoose":
                            {
                                //是否執行
                                if (!Action)
                                {
                                    break;
                                }
                                //對象為自身
                                if (TargetSource.Source_BattleObject != _Basic_Source_Class.Source_BattleObject)
                                {
                                    break;
                                }
                                //生成物件
                                string QuickSave_ValueKey_String =
                                    "Create_Object_MetalPole_Loose｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                string QuickSave_Type_String = QuickSave_ValueKey_String.Split("｜"[0])[0].Replace("Create_", "");

                                for (int a = 0; a < QuickSave_Value_Float; a++)
                                {
                                    _Map_BattleObjectUnit QuickSave_Object_Script =
                                    _World_Manager._Object_Manager.ObjectSet
                                        ("Object", QuickSave_Type_String,
                                        UsingObject.TimePosition(Time,Order), null, Time, Order);
                                    if (QuickSave_Object_Script != null)
                                    {
                                        QuickSave_Object_Script.AdvanceSet();
                                    }
                                }
                            }
                            break;
                    }
                }
                break;
            case "EffectObject":
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
    //行為檢索附魔用/招式附魔對行為加成引導//並非Situation呼叫，而是透過被Situation呼叫的啟動(如Damage)——————————————————————————————————————————————————————————————————————
    public List<DamageClass> Key_Pursuit(
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)//附魔對象類型
    {
        //變數----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //數字設定
        switch (_Basic_Type_String)
        {
            case "SpecialAffix":
                {
                    switch (_Basic_Key_String)
                    {
                        case "SpecialAffix_Shatter_0":
                        case "SpecialAffix_Shatter_1":
                            {
                                if (UserSource.Source_Card != null)
                                {
                                    string QuickSave_ValueKey01_String =
                                        "PursuitNumber_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
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
                                        "PursuitTimes_SlashDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜" + QuickSave_Number_String;
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
                    }
                }
                break;
            case "Passive":
                {
                    switch (_Basic_Key_String)
                    {
                        case "Passive_Identity_Panic":
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

                        case "Passive_Crescent_TwinMoonArc":
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
                                    "PursuitNumber_EnergyDamage_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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

                        case "Passive_ScarletKin_ScarletKinScute":
                            {
                                //消耗值設定
                                string QuickSave_AttackType_String = _Basic_SaveData_Class.StringDataGet(_Basic_Key_String);
                                //百分比數值
                                string QuickSave_PercentageValueKey_String = "";
                                switch (QuickSave_AttackType_String)
                                {
                                    case "SlashDamage":
                                    case "PunctureDamage":
                                        QuickSave_PercentageValueKey_String =
                                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                        break;
                                    case "ImpactDamage":
                                    case "ChaosDamage":
                                        QuickSave_PercentageValueKey_String =
                                            "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜1";
                                        break;
                                }
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
                                    "PursuitNumber_" + QuickSave_AttackType_String + "_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
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
                                    "PursuitTimes_" + QuickSave_AttackType_String + "_Type0｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_Key02_String =
                                    _World_Manager.Key_KeysUnit("Default", 
                                    QuickSave_ValueKey02_String, 
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value02_Float =
                                    _World_Manager.Key_NumbersUnit("Default", 
                                    QuickSave_Key02_String, 1, 
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

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

                }
                break;
            case "EffectObject":
                {
                    switch (_Basic_Key_String)
                    {
                        case "EffectObject_Quality_PunctureWound_0":
                        case "EffectObject_Vine_Thorn_0":
                            {
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_PunctureDamage_Type0｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
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

                        case "EffectObject_Quality_Toxin_0":
                            {
                                string QuickSave_ValueKey01_String =
                                    "PursuitNumber_ChaosDamage_Type0｜User_Default_Default_Stack_Default_Default_" + _Basic_Key_String + "｜0";
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
                            }
                            break;

                        case "EffectObject_Dark_DemonSacrifice_0":
                            {
                                //消耗百分比
                                string QuickSave_PercentageValueKey_String =
                                    "Percentage_Default_Default｜Value_Default_Default_Default_Default_Default_Default｜0";
                                string QuickSave_PercentageKey_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);
                                float QuickSave_PercentageValue_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject, HateTarget, Action, Time, Order);

                                float QuickSave_ValueData_Float =
                                    _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);
                                //造成傷害
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
                            break;

                        case "EffectObject_Material_GreceResonance_0":
                            {
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

                        case "EffectObject_OrganicGauntlets_HungryTouch_0":
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
                            }
                            break;
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

    #region - Other -
    public void Key_Behavior(_UI_Card_Unit Card, 
        _Map_BattleObjectUnit CreatureObject, Vector CreatureObjectCoordiante,
         _Map_BattleObjectUnit UseObject, Vector UseCoordinate)//執行動作
    {

        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;

        //範圍設置
        _World_Manager._UI_Manager._UI_CardManager._Card_UsingCard_Script = Card;//設置使用者
        Card._Card_UseObject_Script = UseObject;
        Dictionary<string, List<Vector>> QuickSave_Range_Dictionary =
            Card._Card_BehaviorUnit_Script.
            Key_Range(UseCoordinate, UseObject,
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        List<Vector> QuickSave_Target_ClassList = new List<Vector>();
        List<Vector> QuickSave_Range_ClassList = new List<Vector>();
        List<Vector> QuickSave_Path_ClassList = new List<Vector>();
        List<Vector> QuickSave_Select_ClassList = new List<Vector>();
        foreach (string Key in QuickSave_Range_Dictionary.Keys)
        {
            QuickSave_Target_ClassList.AddRange(Card._Range_Select_Class.Vector());
            QuickSave_Range_ClassList.AddRange(QuickSave_Range_Dictionary[Key]);
            QuickSave_Path_ClassList.AddRange(
                Card._Range_Path_Class.AllVectors(
                    Card._Range_Path_Class.PathUnits(Key)));
            QuickSave_Select_ClassList.AddRange(
                Card._Range_Select_Class.AllVectors(
                    Card._Range_Select_Class.SelectUnits(Key)));
        }
        _World_Manager._Map_Manager.ViewOn
            ("Range", null,
            QuickSave_Target_ClassList,
            QuickSave_Range_ClassList,
            QuickSave_Path_ClassList,
            QuickSave_Select_ClassList);
        //Select設置
        Card._Range_UseData_Class =
            _Map_Manager._Map_BattleCreator._Map_SelectBoard_Dictionary
            [UseCoordinate.Vector2Int]._Range_PathSelect_ClassList[0];
        //回合設置
        RoundElementClass QuickSave_CardRound_Class = Card._Round_Unit_Class;
        RoundSequenceUnitClass QuickSave_RoundSequence_Class =
            new RoundSequenceUnitClass
            {
                Type = "Normal",
                Owner = UseObject,
                RoundUnit = new List<RoundElementClass> { QuickSave_CardRound_Class }
            };
        QuickSave_CardRound_Class.AccumulatedTime = _Map_BattleRound._Round_Time_Int;
        QuickSave_CardRound_Class.DelayTime = 0;
        _Basic_Owner_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
        //新增回合
        Card._State_AddenAction_Bool = true;//禁止觸發
        _Map_Manager._Map_BattleRound.RoundSequenceSet(
            _Basic_Owner_Script._Round_GroupUnit_Class, null);
        //設定技能中心
        Card._Card_StartCenter_Class =
            new Vector(CreatureObjectCoordiante);
        Card._Card_UseCenter_Class =
            new Vector(UseCoordinate);

        //關閉範圍
        _Map_Manager.ViewOff("Select");
        _Map_Manager.ViewOff("Path");
        _Map_Manager.ViewOff("Range");

        //加入行為表
        UseObject._Round_UnitCards_ClassList.Add(Card._Round_Unit_Class);
        CreatureObject._Round_UnitCards_ClassList.Add(Card._Round_Unit_Class);
    }
    #endregion
    #endregion KeyAction
}
