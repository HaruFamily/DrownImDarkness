using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Item_SyndromeUnit : MonoBehaviour
{
    #region Element
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //���J�ܼ�----------------------------------------------------------------------------------------------------
    //�s��
    public string _Basic_Key_String;
    [HideInInspector] public SourceClass _Basic_Source_Class;
    //�ƾڸ��
    [HideInInspector] public _Item_Manager.SyndromeDataClass _Basic_Data_Class;
    //�y�����
    [HideInInspector] public List<LanguageClass> _Basic_Language_ClassList = new List<LanguageClass>();
    //----------------------------------------------------------------------------------------------------

    //����t��----------------------------------------------------------------------------------------------------
    //������
    public _Item_ConceptUnit _Basic_Owner_Script;
    //��w��m
    public Transform _Syndrome_Bubble_Transform;
    //��e�h��
    public int _Syndrome_Rank_Int;
    public int _Syndrome_Stack_Int;
    //�Ȧs
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();
    //----------------------------------------------------------------------------------------------------

    //���----------------------------------------------------------------------------------------------------
    //�ĪG�ϥ�
    public Image _Syndrome_Icon_Image;
    //�h��
    public Text _Syndrome_Stack_Text;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element

    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart(_Object_CreatureUnit Owner, string Key, int StartStack)
    {
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;

        //���o���----------------------------------------------------------------------------------------------------
        _Basic_Owner_Script = Owner._Object_Inventory_Script._Item_EquipConcepts_Script;
        _Basic_Key_String = Key;
        _Basic_Data_Class = _Item_Manager._Data_Syndrome_Dictionary[Key];
        _Syndrome_Icon_Image.sprite = _World_Manager._View_Manager.GetSprite("Syndrome", "Icon", Key);
        _Basic_Language_ClassList.Add(_Item_Manager._Language_Syndrome_Dictionary[Key]);
        for (int a = 1; a < _Basic_Data_Class.Rank.Count; a++)
        {
            _Basic_Language_ClassList.Add(_Item_Manager._Language_Syndrome_Dictionary[Key.Replace("0"[0], a.ToString()[0])]);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Basic_Source_Class = new SourceClass
        {
            SourceType = "Syndrome",
            Source_Syndrome = this,
            Source_Creature = Owner,
            Source_Concept = _Basic_Owner_Script,
            Source_NumbersData = _Basic_Data_Class.Numbers,
            Source_KeysData = _Basic_Data_Class.Keys
        };
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager._Map_BattleRound.
            _Round_TimesLimits_ClassList.Add(_Basic_TimesLimit_Class);
        StackIncrease(StartStack);
        Key_Effect_OwnStart();
        _Basic_Owner_Script._Syndrome_Syndrome_Dictionary.Add(Key, this);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start

    #region Math
    #region - Stack -
    //�h�ƼW�[�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void StackIncrease(int Number)
    {
        //�ƭ��ܰ�----------------------------------------------------------------------------------------------------
        _Syndrome_Stack_Int += Number;
        for (int a = 0; a < _Basic_Data_Class.Rank.Count; a++)
        {
            if (_Syndrome_Stack_Int >= _Basic_Data_Class.Rank[a])
            {
                _Syndrome_Rank_Int = a;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //��s�ƭ�----------------------------------------------------------------------------------------------------
        ViewSet();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    
    //�h�ƼW�[�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void StackDecrease(string Type, int Number)
    {
        //�ƭ��ܰ�----------------------------------------------------------------------------------------------------
        _Syndrome_Stack_Int -= Number;
        _Syndrome_Stack_Int = Mathf.Clamp(_Syndrome_Stack_Int, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //��s�ƭ�----------------------------------------------------------------------------------------------------
        //��ı��s
        ViewSet();
        //----------------------------------------------------------------------------------------------------

        //��ֱ��p----------------------------------------------------------------------------------------------------
        //�ƭ��ܰ�
        if (_Syndrome_Stack_Int <= 0)
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
        _Basic_Owner_Script._Syndrome_Syndrome_Dictionary.Remove(_Basic_Key_String);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Destroy(this.gameObject);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion

    #region View
    public void ViewSet()
    {
        //�h��
        if (_Basic_Data_Class.Rank.Count >= _Syndrome_Rank_Int + 1)
        {
            _Syndrome_Stack_Text.text =
                _Syndrome_Stack_Int.ToString() + "<size=90>/" + _Basic_Data_Class.Rank[_Syndrome_Rank_Int + 1].ToString() + "</Size>";
        }
        else
        {
            _Syndrome_Stack_Text.text =
                _Syndrome_Stack_Int.ToString() + "<size=90>/" + _Basic_Data_Class.Rank[_Syndrome_Rank_Int].ToString() + "</Size>";
        }
    }
    #endregion View


    #region KeyAction
    #region - Scene -
    #region - Key_Effect_Own -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_OwnStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_OwnEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Effect_Field -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_FieldStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_FieldEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Key_Effect_Battle -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_BattleStart()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_IronMoon_1":
            case "Syndrome_IronMoon_2":
            case "Syndrome_IronMoon_3":
                {
                    //�����ۨ��ĪG
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Light_HolyAura_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        _Basic_Source_Class, ConceptSource,null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                        _Basic_Source_Class, ConceptSource, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                    _World_Manager._Effect_Manager.
                        GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float), 
                        _Basic_Source_Class, ConceptSource, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Key_Effect_BattleEnd()
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key[0])
        {
            #region - MaterialStatus -
            case "Size":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SizeIsForce_2":
                    case "Syndrome_SizeIsForce_3":
                    case "Syndrome_SizeIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                }
                break;
            case "Form":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_FormIsForce_2":
                    case "Syndrome_FormIsForce_3":
                    case "Syndrome_FormIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

                    case "Syndrome_ChaoticOvergrowth_1":
                    case "Syndrome_ChaoticOvergrowth_2":
                    case "Syndrome_ChaoticOvergrowth_3":
                        {
                            List<string> QuickSave_Tag_StringList =
                                UsingObject.Key_Tag(UserSource, TargetSource);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Herb:Vine)" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1" + QuickSave_Number_String;
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
                break;
            case "Weight":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_WeightIsForce_2":
                    case "Syndrome_WeightIsForce_3":
                    case "Syndrome_WeightIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_AncientBedrock_1":
                    case "Syndrome_AncientBedrock_2":
                    case "Syndrome_AncientBedrock_3":
                        {
                            if (UsingObject._Basic_DrivingOwner_Script != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    UsingObject.Key_Tag(UserSource, TargetSource);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1" + QuickSave_Number_String;
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
                        }
                        break;
                }
                break;
            case "Purity":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_PurityIsForce_2":
                    case "Syndrome_PurityIsForce_3":
                    case "Syndrome_PurityIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

                    case "Syndrome_ChaoticOvergrowth_1":
                    case "Syndrome_ChaoticOvergrowth_2":
                    case "Syndrome_ChaoticOvergrowth_3":
                        {
                            List<string> QuickSave_Tag_StringList = 
                                UsingObject.Key_Tag(UserSource, TargetSource);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Herb:Vine)" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U2" + QuickSave_Number_String;
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
                break;
            #endregion

            #region - Status -
            case "Medium":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_MediumCondensed_1":
                    case "Syndrome_MediumCondensed_2":
                    case "Syndrome_MediumCondensed_3":
                    case "Syndrome_MediumCondensed_4":
                        {
                            //�����X��
                            if (UsingObject._Basic_DrivingOwner_Script != null)
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_AncientBedrock_1":
                    case "Syndrome_AncientBedrock_2":
                    case "Syndrome_AncientBedrock_3":
                        {
                            //�����X��
                            if (UsingObject._Basic_DrivingOwner_Script != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UsingObject.Key_Tag(UserSource, TargetSource);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U2" + QuickSave_Number_String;
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
                        }
                        break;
                }
                break;
            case "Catalyst":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_CatalystCondensed_1":
                    case "Syndrome_CatalystCondensed_2":
                    case "Syndrome_CatalystCondensed_3":
                    case "Syndrome_CatalystCondensed_4":
                        {
                            //�����X��
                            if (UsingObject._Basic_DrivingOwner_Script != null)
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_InheritedMartial_1":
                    case "Syndrome_InheritedMartial_2":
                    case "Syndrome_InheritedMartial_3":
                    case "Syndrome_InheritedMartial_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                }
                break;
            case "Luck":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_Chosen_1":
                    case "Syndrome_Chosen_2":
                    case "Syndrome_Chosen_3":
                    case "Syndrome_Chosen_4":
                        {
                            //�������ϥ�
                            if (UsingObject == ConceptSource.Source_BattleObject)
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                break;

            case "MediumPoint":
                switch (Key[1])
                {
                    case "Point":
                        break;
                    case "Total":
                        switch (_Basic_Key_String)
                        {
                            case "Syndrome_PhysicalTraining_1":
                            case "Syndrome_PhysicalTraining_2":
                                {
                                    //�������ϥ�
                                    if (UsingObject == ConceptSource.Source_BattleObject)
                                    {
                                        string QuickSave_ValueKey_String =
                                            "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        break;
                }
                break;
            #endregion

            #region - Data -
            case "Deal":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SphericalGuide_1":
                    case "Syndrome_SphericalGuide_2":
                    case "Syndrome_SphericalGuide_3":
                    case "Syndrome_SphericalGuide_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UUser_Concept_Default_Data_CardsCount_Board_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_TranquilHeart_1":
                    case "Syndrome_TranquilHeart_2":
                    case "Syndrome_TranquilHeart_3":
                    case "Syndrome_TranquilHeart_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_BattlefieldDread_3":
                    case "Syndrome_BattlefieldDread_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1" + QuickSave_Number_String;
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
                }
                break;
            case "CardLimit":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SystematicPrecision_1":
                    case "Syndrome_SystematicPrecision_2":
                    case "Syndrome_SystematicPrecision_3":
                    case "Syndrome_SystematicPrecision_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                }
                break;
            case "DelayBefore":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_LoadingExpertise_1":
                    case "Syndrome_LoadingExpertise_2":
                    case "Syndrome_LoadingExpertise_3":
                    case "Syndrome_LoadingExpertise_4":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Loading" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;
                    case "Syndrome_ItemExpertise_1":
                    case "Syndrome_ItemExpertise_2":
                    case "Syndrome_ItemExpertise_3":
                    case "Syndrome_ItemExpertise_4":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Item" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;

                    case "Syndrome_ChaoticAction_1":
                    case "Syndrome_ChaoticAction_2":
                    case "Syndrome_ChaoticAction_3":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UUser_Driving_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_ClumsyLoading_1":
                    case "Syndrome_ClumsyLoading_2":
                    case "Syndrome_ClumsyLoading_3":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Loading" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;
                }
                break;
            case "GetSyndrome":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SyndromeDeterioration_1":
                    case "Syndrome_SyndromeDeterioration_2":
                    case "Syndrome_SyndromeDeterioration_3":
                    case "Syndrome_SyndromeDeterioration_4":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            string QuickSave_ValueKey_String =
                                "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                            {
                                QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default", 
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                Answer_Return_Float += QuickSave_Value_Float;
                            }
                        }
                        break;
                }
                break;
            #endregion

            #region - Battle -
            case "AttackNumber":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SizeIsForce_1":
                    case "Syndrome_SizeIsForce_2":
                    case "Syndrome_SizeIsForce_3":
                    case "Syndrome_SizeIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UUsing_Default_Default_Material_Size_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_FormIsForce_1":
                    case "Syndrome_FormIsForce_2":
                    case "Syndrome_FormIsForce_3":
                    case "Syndrome_FormIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UUsing_Default_Default_Material_Form_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_WeightIsForce_1":
                    case "Syndrome_WeightIsForce_2":
                    case "Syndrome_WeightIsForce_3":
                    case "Syndrome_WeightIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UUsing_Default_Default_Material_Weight_Default_Default�U" + QuickSave_Number_String;
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
                    case "Syndrome_PurityIsForce_1":
                    case "Syndrome_PurityIsForce_2":
                    case "Syndrome_PurityIsForce_3":
                    case "Syndrome_PurityIsForce_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UUsing_Default_Default_Material_Purity_Default_Default�U" + QuickSave_Number_String;
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

                    case "Syndrome_FightingExperience_1":
                    case "Syndrome_FightingExperience_2":
                    case "Syndrome_FightingExperience_3":
                    case "Syndrome_FightingExperience_4":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Fighting" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;

                    case "Syndrome_BluntedBlades_1":
                    case "Syndrome_BluntedBlades_2":
                    case "Syndrome_BluntedBlades_3":
                    case "Syndrome_BluntedBlades_4":
                        {
                            List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(UserSource, TargetSource);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Blade" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey_String =
                                    "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;

                    case "Syndrome_BattlefieldDread_1":
                    case "Syndrome_BattlefieldDread_2":
                    case "Syndrome_BattlefieldDread_3":
                    case "Syndrome_BattlefieldDread_4":
                        {
                            string QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U2" + QuickSave_Number_String;
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
                }
                break;
            case "AttackTimes":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_StrikeMistake_1":
                    case "Syndrome_StrikeMistake_2":
                    case "Syndrome_StrikeMistake_3":
                    case "Syndrome_StrikeMistake_4":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            string QuickSave_ValueKey_String =
                                "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                            {
                                QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                Answer_Return_Float -= QuickSave_Value_Float;
                            }
                        }
                        break;
                }
                break;
                #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + Answer_Return_Float);
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Key[0])
        {
            #region - Data -
            case "SyndromeTime":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SyndromeBarrier_1":
                    case "Syndrome_SyndromeBarrier_2":
                    case "Syndrome_SyndromeBarrier_3":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            string QuickSave_ValueKey_String =
                                "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                }
                break;
            #endregion
            #region - Battle -
            case "AttackNumber":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_SupremeRadiance_1":
                    case "Syndrome_SupremeRadiance_2":
                    case "Syndrome_SupremeRadiance_3":
                    case "Syndrome_SupremeRadiance_4":
                        {
                            if (UserSource.Source_Card != null)
                            {
                                List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.
                                Key_OwnTag(TargetSource, UsingObject, true);
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Light" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;
                }
                break;
            #endregion
            #region - SituationTimes -
            case "DeadResistTimes":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_FatalFlaw_1":
                        {
                            //���`��ܵL��
                            Answer_Return_Float = 0;
                        }
                        break;
                }
                break;
                #endregion SituationTimes
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + Answer_Return_Float);
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
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion

            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + Answer_Return_Float);
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
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion

            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + Answer_Return_Float);
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
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_ProliferativeCarapace_1":
                    case "Syndrome_ProliferativeCarapace_2":
                    case "Syndrome_ProliferativeCarapace_3":
                    case "Syndrome_ProliferativeCarapace_4":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            if (TargetSource.Source_BattleObject == UsingObject)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    UsingObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Carapace" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        QuickSave_ValueKey_String =
                                            "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                        QuickSave_Key_String =
                                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        QuickSave_Value_Float =
                                            _World_Manager.Key_NumbersUnit("Default",
                                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order);
                                        Answer_Return_Float += QuickSave_Value_Float;
                                    }
                                }
                            }
                        }
                        break;

                    case "Syndrome_ShadowWalker_1":
                    case "Syndrome_ShadowWalker_2":
                    case "Syndrome_ShadowWalker_3":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            if (TargetSource.Source_BattleObject == UsingObject)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Hide" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        if (UserSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit DicValue))
                                        {
                                            QuickSave_ValueKey_String =
                                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                            QuickSave_Key_String =
                                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                                HateTarget, Action, Time, Order);
                                            QuickSave_Value_Float =
                                                _World_Manager.Key_NumbersUnit("Default",
                                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                                HateTarget, Action, Time, Order);
                                            Answer_Return_Float += QuickSave_Value_Float;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Syndrome_EndlessSpin_1":
                    case "Syndrome_EndlessSpin_2":
                    case "Syndrome_EndlessSpin_3":
                        {
                            if (TargetSource.Source_BattleObject == UsingObject)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Spin" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        }
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + Answer_Return_Float);
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
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager.EffectDataClass QuickSave_EffectData_Class =
            _World_Manager._Effect_Manager._Data_EffectObject_Dictionary[Key];
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Key_String)
                {

                    case "Syndrome_IneffectiveRewards_1":
                    case "Syndrome_IneffectiveRewards_2":
                    case "Syndrome_IneffectiveRewards_3":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            if (TargetSource.Source_BattleObject == UsingObject)
                            {
                                List<string> QuickSave_Tag_StringList =
                                    TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                    Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                                List<string> QuickSave_CheckTag_StringList = new List<string> { "Reward" };
                                if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                {
                                    string QuickSave_ValueKey_String =
                                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    string QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    float QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        Answer_Return_Float = 0;
                                    }
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + Answer_Return_Float);
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
                switch (_Basic_Key_String)
                {
                    #region - Common -
                    #endregion

                    default:
                        break;
                }
                break;
            case "Card":
                switch (_Basic_Key_String)
                {
                    #region - Common -
                    #endregion

                    default:
                        break;
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + Answer_Return_Float);
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Object":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_MillionArthropod_1":
                    case "Syndrome_MillionArthropod_2":
                    case "Syndrome_MillionArthropod_3":
                    case "Syndrome_MillionArthropod_4":
                        {
                            if (!Action)
                            {
                                break;
                            }
                            List<string> QuickSave_Tag_StringList =
                                TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                                Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Arthropoda" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                string QuickSave_ValueKey_String =
                                    "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U2" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                {
                                    Answer_Return_Float = 0;
                                }
                            }
                        }
                        break;
                    default:

                        break;
                }
                break;
            case "Card":
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + Answer_Return_Float);
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
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Key":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_GetStackValueMultiply(string Type, string KeyTag,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        float Answer_Return_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Tag":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Key":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + Answer_Return_Float);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - ColliderCheck -
    public List<string> Key_Effect_IsColliderEnterCheck(string Type, bool NowState,
        SourceClass UserSource/*�i�J��*/, SourceClass TargetSource/*�Q�i�J��*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        //----------------------------------------------------------------------------------------------------

        //�q��----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Stay":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse�U" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_IsColliderEnteredCheck(string Type, bool NowState,
        SourceClass UserSource/*�i�J��*/, SourceClass TargetSource/*�Q�i�J��*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        //----------------------------------------------------------------------------------------------------

        //�q��----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Pass":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
            case "Stay":
                {
                    switch (_Basic_Key_String)
                    {
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse�U" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - StateAction -
    public List<string> Key_Effect_IsReachCheck(int DriveDistance, bool NowState,
        SourceClass UserSource/*�i�J��*/, SourceClass TargetSource/*�Q�i�J��*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //�q��----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse�U" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_IsCarryCheck(int DriveDistance, bool NowState,
        SourceClass UserSource/*�i�J��*/, SourceClass TargetSource/*�Q�i�J��*/, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = NowState;
        //----------------------------------------------------------------------------------------------------

        //�q��----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse�U" + Answer_Return_Bool.ToString());
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
        SourceClass ConceptSource =
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (UserSource == _Basic_Source_Class)
        {
            return Answer_Return_StringList;//�קK����
        }
        switch (Type)//�ؼ�(EX:RequireTag)
        {
            //Object����
            #region - Tag -
            case "Tag":
                switch (_Basic_Key_String)
                {
                    case "Syndrome_HerbaceousVine_1":
                        {
                            List<string> QuickSave_Tag_StringList = 
                                UserSource.Source_BattleObject._Basic_Material_Class.Tag;
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Herb" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                Answer_Return_StringList.Add("TagAdd�U" + "Vine");
                            }
                        }
                        break;

                    case "Syndrome_Cnidocytes_1":
                    case "Syndrome_Cnidocytes_2":
                    case "Syndrome_Cnidocytes_3":
                    case "Syndrome_Cnidocytes_4":
                        {
                            //�ϥά�����
                            if (UsingObject == ConceptSource.Source_BattleObject)
                            {
                                if (UserSource.Source_Card != null)
                                {
                                    List<string> QuickSave_Tag_StringList =
                                    UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Attach" };
                                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                                    {
                                        Answer_Return_StringList.Add("TagAdd�U" + "Cnidocytes");
                                    }
                                }
                            }
                        }
                        break;
                }
                break;
                #endregion
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
        switch (Type)//�ؼ�(EX:RequireTag)
        {
            default:
                break;
        }
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion

            default:
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
        switch (_Basic_Key_String)
        {
            case "Syndrome_ShadowWalker_1":
            case "Syndrome_ShadowWalker_2":
            case "Syndrome_ShadowWalker_3":
                {
                    Answer_Return_String = 
                        Answer_Return_String.Replace("EffectObject_Hide_Hide_0", "EffectObject_Hide_AlphaHide_0");
                }
                break;
            case "Syndrome_PhantasmalFear_1":
            case "Syndrome_PhantasmalFear_2":
            case "Syndrome_PhantasmalFear_3":
            case "Syndrome_PhantasmalFear_4":
                {
                    Answer_Return_String = 
                        Answer_Return_String.Replace("EffectObject_Sensation_Sensation_Fear_0", "EffectObject_Sensation_DeepFear_0");
                }
                break;
            case "Syndrome_EndlessSpin_1":
            case "Syndrome_EndlessSpin_2":
            case "Syndrome_EndlessSpin_3":
                {
                    Answer_Return_String = 
                        Answer_Return_String.Replace("EffectObject_Spin_Spin_0", "EffectObject_Spin_EndlessSpin_0");
                }
                break;
            default:
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Key�U" + Answer_Return_String);
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - SummonedLoyalty -
            case "Syndrome_SummonedLoyalty_1":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Summoned" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�����ϥήĪG
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Object_SummonedLoyalty_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                }
                break;
            #endregion
            #region - DriveGuardian -
            case "Syndrome_DriveGuardian_1":
            case "Syndrome_DriveGuardian_2":
            case "Syndrome_DriveGuardian_3":
                {
                    //���������ĪG
                    string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Guard_DriveGuardian_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                    Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                        GetEffectObject(
                        QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                        _Basic_Source_Class, QuickSave_Source_Class,
                        HateTarget, Action, Time, Order));
                }
                break;
            #endregion
            #region - DominatorGame -
            case "Syndrome_DominatorGame_1":
            case "Syndrome_DominatorGame_2":
            case "Syndrome_DominatorGame_3":
            case "Syndrome_DominatorGame_4":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Symbiotic" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���������ĪG
                        string QuickSave_ValueKey_String =
                        "ConstructNumber_EffectObject_Object_DominatorGame_" + (int.Parse(QuickSave_Number_String) - 1).ToString() +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String; 
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                            GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, QuickSave_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                }
                break;
            #endregion
            #region - MassUprising -
            case "Syndrome_MassUprising_1":
            case "Syndrome_MassUprising_2":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Summoned" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            Answer_Return_StringList.AddRange(QuickSave_Source_Class.Source_BattleObject.
                                Damaged(
                                Key_Pursuit(
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order),
                                    UsingObject, 
                                    HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;
            #endregion
            #region - BetrayalParasitic -
            case "Syndrome_BetrayalParasitic_1":
            case "Syndrome_BetrayalParasitic_2":
            case "Syndrome_BetrayalParasitic_3":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Symbiotic" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�������Ӽƭ�
                        string QuickSave_ValueKey_String =
                            "Consume_CatalystPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        /*float QuickSave_Consume_Float =
                            Mathf.Clamp(QuickSave_Value_Float, 0,
                            _Basic_Source_Class.Source_BattleObject.
                            Key_Point(QuickSave_Type_String, "Point",
                            _Basic_Source_Class, _Basic_Source_Class));*/

                        QuickSave_Source_Class.Source_BattleObject.PointSet(
                            "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                            _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order, Action);
                        //ValueDataSet("Consume", QuickSave_Consume_Float);

                        //�������d�P
                        QuickSave_ValueKey_String =
                            "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        _World_Manager._UI_Manager._UI_CardManager.
                            CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                    }
                }
                break;
                #endregion
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
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();//EX:

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_ProliferativeCarapace_1":
            case "Syndrome_ProliferativeCarapace_2":
            case "Syndrome_ProliferativeCarapace_3":
            case "Syndrome_ProliferativeCarapace_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Carapace" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�ۥ[��j��W����
                            if ((BeforeStack + IncreaseStack) > Effect.Key_StackLimit())
                            {
                                //������d
                                QuickSave_ValueKey_String =
                                    "Deal_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default", 
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                Answer_Return_StringList.AddRange(
                                    _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                        "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                        _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order));
                            }
                        }
                    }
                }
                break;

            case "Syndrome_PaidEmployment_1":
            case "Syndrome_PaidEmployment_2":
            case "Syndrome_PaidEmployment_3":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Reward" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�����ȿ�
                        string QuickSave_ValueKey_String =
                            "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        //�ϥΪ��ȿ�
                        _World_Manager._Item_Manager.
                            DustSet("Get", QuickSave_Value_Float, _Basic_Source_Class, QuickSave_Source_Class);
                    }
                }
                break;

            case "Syndrome_NumericRetribution_1":
            case "Syndrome_NumericRetribution_2":
            case "Syndrome_NumericRetribution_3":
            case "Syndrome_NumericRetribution_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "CountEffect" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //��֥ؼЮĪG�h��
                            QuickSave_ValueKey_String =
                                "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default", 
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                            float QuickSave_Stack_Float = Effect.Key_Stack("Default", null, null, null, null) * QuickSave_Value_Float;

                            Answer_Return_StringList.AddRange(_Effect_Manager.LostEffectObject(
                                Key, Mathf.RoundToInt(QuickSave_Stack_Float),
                                _Basic_Source_Class, TargetSource,
                                HateTarget, Action, Time, Order));

                        }
                    }
                }
                break;

            case "Syndrome_CatalystVanishing_1":
            case "Syndrome_CatalystVanishing_2":
            case "Syndrome_CatalystVanishing_3":
            case "Syndrome_CatalystVanishing_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Hide" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", 
                            QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�������Ӽƭ�
                            QuickSave_ValueKey_String =
                                "Consume_CatalystPoint_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                            /*float QuickSave_Consume_Float =
                                Mathf.Clamp(QuickSave_Value_Float, 0,
                                _Basic_Source_Class.Source_BattleObject.
                                Key_Point(QuickSave_Type_String, "Point",
                                _Basic_Source_Class, _Basic_Source_Class));*/

                            QuickSave_Source_Class.Source_BattleObject.PointSet(
                                "Set", QuickSave_Type_String, -QuickSave_Value_Float, 1,
                                _Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order, Action);
                            //ValueDataSet("Consume", QuickSave_Consume_Float);
                        }
                    }
                }
                break;

            case "Syndrome_LightfieldShadow_1":
            case "Syndrome_LightfieldShadow_2":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Light" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�������d��
                            QuickSave_ValueKey_String =
                                "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            Answer_Return_StringList.AddRange(
                                _World_Manager._UI_Manager._UI_CardManager.
                                CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_MaliciousThorn_1":
            case "Syndrome_MaliciousThorn_2":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Vine" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ۧڶˮ`
                        Answer_Return_StringList.AddRange(
                            QuickSave_Source_Class.Source_BattleObject.Damaged(
                                Key_Pursuit(
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order),
                                    UsingObject, 
                                    HateTarget, Action, Time, Order));
                    }
                }
                break;

            case "Syndrome_ExcessiveShellborn_1":
            case "Syndrome_ExcessiveShellborn_2":
            case "Syndrome_ExcessiveShellborn_3":
            case "Syndrome_ExcessiveShellborn_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Carapace" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�����ؼЮĪG
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Carapace_ShellbornBurden_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default", 
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(
                                _World_Manager._Effect_Manager.
                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, TargetSource,
                                HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_UncontrolledWhirl_1":
            case "Syndrome_UncontrolledWhirl_2":
            case "Syndrome_UncontrolledWhirl_3":
            case "Syndrome_UncontrolledWhirl_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Spin" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�ˮ`����
                            Answer_Return_StringList.AddRange(
                                QuickSave_Source_Class.Source_BattleObject.Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_ExcessiveCherishing_1":
            case "Syndrome_ExcessiveCherishing_2":
            case "Syndrome_ExcessiveCherishing_3":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���v�P�w
                    string QuickSave_ValueKey_String =
                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                    {
                        //�������d��
                        QuickSave_ValueKey_String =
                            "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        Answer_Return_StringList.AddRange(
                                _World_Manager._UI_Manager._UI_CardManager.
                                CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                }
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
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
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
        switch (_Basic_Key_String)
        {
            case "Syndrome_PhantasmalFear_1":
            case "Syndrome_PhantasmalFear_2":
            case "Syndrome_PhantasmalFear_3":
            case "Syndrome_PhantasmalFear_4":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        TargetSource.Source_BattleObject._Effect_Effect_Dictionary.TryGetValue(Key, out _Effect_EffectObjectUnit Effect) ?
                        Effect.Key_EffectTag(UserSource, TargetSource) : QuickSave_EffectData_Class.EffectTag;
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Fear" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ˮ`�ؼ�
                        Answer_Return_StringList.AddRange(
                        TargetSource.Source_BattleObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                    }
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
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_ExiledDrift_1":
                {
                    if (!Action)
                    {
                        break;
                    }
                    if (QuickSave_Split_StringList[2] != "Cemetery")
                    {
                        break;
                    }
                    //���v�P�w
                    string QuickSave_ValueKey_String =
                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                    {
                        QuickSave_Split_StringList[2] = "Exiled";
                    }
                }
                break;

            case "Syndrome_MillionArthropod_1":
            case "Syndrome_MillionArthropod_2":
            case "Syndrome_MillionArthropod_3":
            case "Syndrome_MillionArthropod_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    if (QuickSave_Split_StringList[2] != "Cemetery")
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                            TargetSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Arthropoda" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U1" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            QuickSave_Split_StringList[2] = "Board";
                        }
                    }
                }
                break;

            case "Syndrome_ComplicatedThoughts_1":
            case "Syndrome_ComplicatedThoughts_2":
            case "Syndrome_ComplicatedThoughts_3":
            case "Syndrome_ComplicatedThoughts_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    if (QuickSave_Split_StringList[1] != "Deal")
                    {
                        break;
                    }
                    //���v�P�w
                    string QuickSave_ValueKey_String =
                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                        UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                    {
                        //�������d��
                        QuickSave_ValueKey_String =
                                "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        Answer_Return_StringList.AddRange(
                            _World_Manager._UI_Manager._UI_CardManager.
                            CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order));
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Key�U" + 
            (QuickSave_Split_StringList[0] + "_" + QuickSave_Split_StringList[1] + "_" + QuickSave_Split_StringList[2]));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Deal(float Value,
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();//EX:
        float Answer_Return_Float = Value;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value�U" + (Answer_Return_Float));
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
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value�U" + (Answer_Return_Float));
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
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_TheSeaOfCurrent_1":
            case "Syndrome_TheSeaOfCurrent_2":
            case "Syndrome_TheSeaOfCurrent_3":
            case "Syndrome_TheSeaOfCurrent_4":
                {
                    if (IsStandby)
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            _Basic_Source_Class, QuickSave_Source_Class, null,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, null,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�ͦ��P�w
                            Vector QuickSave_OwnerPos_Class = _Basic_Owner_Script._Basic_Object_Script.TimePosition(_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            List<_Map_BattleObjectUnit> QuickSave_GroundObjects_ScriptsList = 
                                _World_Manager._Object_Manager.
                                TimeObjects("All", QuickSave_Source_Class,
                                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int, QuickSave_OwnerPos_Class);
                            foreach (_Map_BattleObjectUnit Object in QuickSave_GroundObjects_ScriptsList)
                            {
                                if (Object._Basic_Key_String == "Object_OceanCurrent_Normal")
                                {
                                    //������d
                                    QuickSave_ValueKey_String =
                                        "Deal_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, QuickSave_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default", 
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, QuickSave_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                                    _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                                            "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                        _Basic_Source_Class, QuickSave_Source_Class, _Basic_Source_Class.Source_BattleObject,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                                else
                                {
                                    //�ͦ�����
                                    QuickSave_ValueKey_String =
                                        "Create_Object_OceanCurrent_Normal�UValue_Default_Default_Default_Default_Default_Default�U1";
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        _Basic_Source_Class, _Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        _Basic_Source_Class, _Basic_Source_Class, null,
                                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Replace("Create_", "");

                                    for (int a = 0; a < QuickSave_Value_Float; a++)
                                    {
                                        _Map_BattleObjectUnit QuickSave_Object_Script =
                                        _World_Manager._Object_Manager.ObjectSet
                                            ("Object", QuickSave_Type_String,
                                            QuickSave_OwnerPos_Class, null, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                        if (QuickSave_Object_Script != null)
                                        {
                                            QuickSave_Object_Script._Basic_Status_Dictionary["ComplexPoint"] =
                                        _World_Manager.DiceRandom(1, 1, 5);//1~4
                                            QuickSave_Object_Script.AdvanceSet();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                break;

            case "Syndrome_WeightBurden_1":
            case "Syndrome_WeightBurden_2":
            case "Syndrome_WeightBurden_3":
            case "Syndrome_WeightBurden_4":
                {
                    if (IsStandby)
                    {
                        QuickSave_Source_Class.Source_BattleObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, QuickSave_Source_Class, null,
                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int),
                                null, 
                                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_Effect_Skill(SourceClass UserSource/*��Skill�ӷ�*/, _Map_BattleObjectUnit UsingObject/*�ϥΪ�(EX:Card-Use)*/)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void Key_Effect_React(SourceClass UserSource)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_FightingExperience_2":
            case "Syndrome_FightingExperience_3":
            case "Syndrome_FightingExperience_4":
                {
                    //������d
                    string QuickSave_ValueKey_String =
                        "Deal_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        _Basic_Source_Class, QuickSave_Source_Class, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        _Basic_Source_Class, QuickSave_Source_Class, UserSource.Source_Card._Card_UseObject_Script, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    _World_Manager._UI_Manager._UI_CardManager.CardDeal(
                        "Normal", Mathf.RoundToInt(QuickSave_Value_Float), null,
                        _Basic_Source_Class, QuickSave_Source_Class, _Basic_Source_Class.Source_BattleObject,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
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
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Card -
    public List<string> Key_Effect_DealPriority(bool NowState,
        SourceClass UserSource/*����*/, SourceClass TargetSource/*�d��*/,
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
        Answer_Return_StringList.Add("BoolTrue�U" + (Answer_Return_Bool));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_AbandonResidue(bool NowState,
        SourceClass UserSource/*����*/, SourceClass TargetSource/*�d��*/,
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
        Answer_Return_StringList.Add("BoolTrue�U" + (Answer_Return_Bool));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }

    public List<string> Key_Effect_UseLicense(
        bool NowState, List<string> Data,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();
        bool Answer_Return_Bool = true;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolFalse�U" + (Answer_Return_Bool));
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_MourningCoral_1":
            case "Syndrome_MourningCoral_2":
            case "Syndrome_MourningCoral_3":
            case "Syndrome_MourningCoral_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���v�P�w
                    string QuickSave_ValueKey_String =
                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        UserSource, UserSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        UserSource, UserSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                    {
                        //�ϥΨ���ˮ`
                        Answer_Return_StringList.AddRange(UserSource.Source_BattleObject.
                            Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                    }
                }
                break;
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_SplashGravel_1":
            case "Syndrome_SplashGravel_2":
            case "Syndrome_SplashGravel_3":
            case "Syndrome_SplashGravel_4":
                {
                    if (UserSource.Source_Card != null)
                    {
                        List<string> QuickSave_Tag_StringList =
                            UserSource.Source_Card._Card_BehaviorUnit_Script.
                            Key_OwnTag(TargetSource, UsingObject, true);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Herb|Wood|Vine)" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //�ˮ`�ؼ�
                            Answer_Return_StringList.AddRange(
                                UsingObject.Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_PlantArmy_1":
            case "Syndrome_PlantArmy_2":
            case "Syndrome_PlantArmy_3":
            case "Syndrome_PlantArmy_4":
                {
                    //�d���P�w
                    if (UserSource.Source_Card == null)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                        UserSource.Source_Card._Card_BehaviorUnit_Script.
                        Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Select(Herb|Wood|Vine)" };
                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        break;
                    }
                    //�ˮ`�ؼ�
                    Answer_Return_StringList.AddRange(
                        UsingObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                }
                break;

            case "Syndrome_ThornyVines_1":
            case "Syndrome_ThornyVines_2":
            case "Syndrome_ThornyVines_3":
            case "Syndrome_ThornyVines_4":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                            UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Vine" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ϥΪ���o�ĪG
                        string QuickSave_ValueKey_String =
                            "ConstructNumber_EffectObject_Vine_Thorn_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                        Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.GetEffectObject(
                            QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                            _Basic_Source_Class, UsingObject._Basic_Source_Class,
                            HateTarget, Action, Time, Order));
                    }
                }
                break;

            case "Syndrome_Cnidocytes_1":
            case "Syndrome_Cnidocytes_2":
            case "Syndrome_Cnidocytes_3":
            case "Syndrome_Cnidocytes_4":
                {
                    //�ϥΪ̬��ۨ�
                    if (UserSource.Source_BattleObject != _Basic_Owner_Script._Basic_Object_Script)
                    {
                        break;
                    }
                    //�d���P�w
                    if (UserSource.Source_Card == null)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                    UserSource.Source_Card._Card_BehaviorUnit_Script.
                    Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Attach" };
                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        break;
                    }
                    //�ˮ`�ؼ�
                    Answer_Return_StringList.AddRange(
                        UsingObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                }
                break;

            case "Syndrome_InheritedMartial_1":
            case "Syndrome_InheritedMartial_2":
            case "Syndrome_InheritedMartial_3":
            case "Syndrome_InheritedMartial_4":
                {
                    //�d���P�w
                    if (UserSource.Source_Card == null)
                    {
                        break;
                    }
                    //�ˮ`�ؼ�
                    Answer_Return_StringList.AddRange(
                        UsingObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                }
                break;

            case "Syndrome_ThousandEdges_1":
            case "Syndrome_ThousandEdges_2":
            case "Syndrome_ThousandEdges_3":
            case "Syndrome_ThousandEdges_4":
                {
                    //�d���P�w
                    if (UserSource.Source_Card == null)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                    UserSource.Source_Card._Card_BehaviorUnit_Script.
                    Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Blade" };
                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        break;
                    }
                    //�ˮ`�ؼ�
                    Answer_Return_StringList.AddRange(
                        UsingObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                }
                break;

            case "Syndrome_IronMoon_1":
            case "Syndrome_IronMoon_2":
            case "Syndrome_IronMoon_3":
                {
                    //�ϥΪ̬��ۨ�
                    if (UserSource.Source_BattleObject != _Basic_Owner_Script._Basic_Object_Script)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                    UserSource.Source_BattleObject.Key_Tag(
                    UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "HolyAura" };
                    if (!_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        break;
                    }
                    //�ˮ`�ؼ�
                    Answer_Return_StringList.AddRange(
                        UsingObject.Damaged(
                            Key_Pursuit(
                                _Basic_Source_Class, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order),
                                UsingObject, 
                                HateTarget, Action, Time, Order));
                }
                break;

            case "Syndrome_DancingDolls_1":
            case "Syndrome_DancingDolls_2":
            case "Syndrome_DancingDolls_3":
            case "Syndrome_DancingDolls_4":
                {
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Doll" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //�ۨ��v¡
                        string QuickSave_ValueKey_String =
                            "HealNumber_CatalystPoint_Type" + QuickSave_Number_String +
                            "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_ValueKey02_String =
                            "HealTimes_CatalystPoint_Type" + QuickSave_Number_String +
                            "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key02_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey02_String,
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value02_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key02_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey02_String],
                            _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        string QuickSave_Type_String = QuickSave_ValueKey_String.Split("�U"[0])[0].Split("_"[0])[1];

                        float QuickSave_PointPoint_Float =
                            UserSource.Source_BattleObject.Key_Point(QuickSave_Type_String, "Point", UserSource, TargetSource);
                        float QuickSave_TotalPoint_Float =
                            UserSource.Source_BattleObject.Key_Point(QuickSave_Type_String, "Total", UserSource, TargetSource);
                        float QuickSave_OverPoint_Float =
                            QuickSave_PointPoint_Float + (QuickSave_Value_Float * QuickSave_Value02_Float) - QuickSave_TotalPoint_Float;

                        Answer_Return_StringList.AddRange(UsingObject.PointSet(
                            "Recover", QuickSave_Type_String, QuickSave_Value_Float, Mathf.RoundToInt(QuickSave_Value02_Float),
                            _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order, Action));

                        //���X�q�ഫ
                        if (QuickSave_OverPoint_Float > 0)
                        {
                            _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, QuickSave_OverPoint_Float);

                            //���Ӧʤ���
                            string QuickSave_PercentageValueKey_String =
                                "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            string QuickSave_PercentageKey_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_PercentageValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_PercentageValue_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_PercentageKey_String, _Basic_Data_Class.Numbers[QuickSave_PercentageValueKey_String],
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            float QuickSave_ValueData_Float =
                                _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String, QuickSave_PercentageValue_Float);

                            //�����ϥήĪG
                            QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Quality_CatalystOverDrive_0�UValue_Default_Default_Default_Default_Default_Default�U0";
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default", 
                                QuickSave_Key_String, QuickSave_ValueData_Float,
                                _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float),
                                _Basic_Source_Class, UsingObject._Basic_Source_Class,
                                HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_AbnormalControl_1":
            case "Syndrome_AbnormalControl_2":
            case "Syndrome_AbnormalControl_3":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���v�P�w
                    string QuickSave_ValueKey_String =
                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                    {
                        //�������d��
                        QuickSave_ValueKey_String =
                                "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                        QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);

                        _World_Manager._UI_Manager._UI_CardManager.
                            CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                            _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                    }
                }
                break;

            case "Syndrome_FlawedItem_1":
            case "Syndrome_FlawedItem_2":
            case "Syndrome_FlawedItem_3":
            case "Syndrome_FlawedItem_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                            UsingObject.Key_Tag(UserSource, TargetSource);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Item" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default",
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�������d��
                            QuickSave_ValueKey_String =
                                    "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                            QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default",
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);

                            _World_Manager._UI_Manager._UI_CardManager.
                                CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order);
                        }
                    }
                }
                break;

            case "Syndrome_UnexpectedShattering_1":
            case "Syndrome_UnexpectedShattering_2":
            case "Syndrome_UnexpectedShattering_3":
            case "Syndrome_UnexpectedShattering_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //���ҧP�w
                    List<string> QuickSave_Tag_StringList =
                    UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Stone" };
                    if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        //���v�P�w
                        string QuickSave_ValueKey_String =
                            "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                        string QuickSave_Key_String =
                            _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        float QuickSave_Value_Float =
                            _World_Manager.Key_NumbersUnit("Default", 
                            QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);

                        if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                        {
                            //�ˮ`�ϥΪ�
                            Answer_Return_StringList.AddRange(
                                UsingObject.Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, UsingObject._Basic_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                        }
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];

        float Answer_Return_Float = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueAdd�U" + (Answer_Return_Float));
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
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];

        float Answer_Return_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_CrisisEvasion_1":
            case "Syndrome_CrisisEvasion_2":
            case "Syndrome_CrisisEvasion_3":
            case "Syndrome_CrisisEvasion_4":
                {
                    if (!Action)
                    {
                        break;
                    }
                    //�ƭȧP�w
                    string QuickSave_ValueKey_String =
                        "Value_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                    string QuickSave_Key_String =
                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value_Float =
                        _World_Manager.Key_NumbersUnit("Default",
                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    if (Value > QuickSave_Value_Float)
                    {
                        switch (DamageType)
                        {
                            case "StarkDamage":
                                break;
                            default:
                                {
                                    //���v�P�w
                                    QuickSave_ValueKey_String =
                                        "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                    QuickSave_Key_String =
                                        _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);
                                    QuickSave_Value_Float =
                                        _World_Manager.Key_NumbersUnit("Default",
                                        QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                        UserSource, TargetSource, UsingObject,
                                        HateTarget, Action, Time, Order);

                                    if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                                    {
                                        Answer_Return_Float *= 0;
                                    }
                                }
                                break;
                        }
                    }

                }
                break;

            case "Syndrome_BondConnection_1":
            case "Syndrome_BondConnection_2":
            case "Syndrome_BondConnection_3":
            case "Syndrome_BondConnection_4":
                {
                    switch (DamageType)
                    {
                        case "StarkDamage":
                            break;
                        default:
                            {
                                //�ۨ��ʤ���ˮ`����
                                string QuickSave_ValueKey_String =
                                    "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, _Basic_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                Answer_Return_Float *= (1 - QuickSave_Value_Float);

                                List<_Map_BattleObjectUnit> QuickSave_RandomDrivingObject_ScriptsList =
                                    _Basic_Owner_Script._Basic_Owner_Script._Object_Inventory_Script._Item_DrivingObject_ScriptsList;
                                if (QuickSave_RandomDrivingObject_ScriptsList.Count > 1)
                                {
                                    _Map_BattleObjectUnit QuickSave_RandomObject_Script =
                                    QuickSave_RandomDrivingObject_ScriptsList[Mathf.RoundToInt(_World_Manager.DiceRandom(1, 0, QuickSave_RandomDrivingObject_ScriptsList.Count))];

                                    _Basic_SaveData_Class.ValueDataSet(_Basic_Key_String, Value * QuickSave_Value_Float);
                                    _Basic_SaveData_Class.StringDataSet(_Basic_Key_String, DamageType);
                                    Answer_Return_StringList.AddRange(QuickSave_RandomObject_Script.
                                        Damaged(
                                        Key_Pursuit(
                                            _Basic_Source_Class, QuickSave_RandomObject_Script._Basic_Source_Class, UsingObject,
                                            HateTarget, Action, Time, Order),
                                            UsingObject, 
                                            HateTarget, Action, Time, Order));
                                }
                            }
                            break;
                    }
                }
                break;

            case "Syndrome_ExistenceFrailty_1":
            case "Syndrome_ExistenceFrailty_2":
            case "Syndrome_ExistenceFrailty_3":
                {
                    switch (DamageType)
                    {
                        case "StarkDamage":
                            break;
                        default:
                            {
                                //�W�[�ˮ`
                                string QuickSave_ValueKey_String =
                                    "Percentage_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);

                                Answer_Return_Float *= (1 + QuickSave_Value_Float);
                            }
                            break;
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("ValueMultiply�U" + (Answer_Return_Float));
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_DamageBlock(string DamageType, float OriginalDamage/*�ܤƫe�ˮ`*/, float NowDamage,/*��e�ˮ`*/
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        List<string> Answer_Return_StringList = new List<string>();

        float Answer_Return_Float = NowDamage;//�ˮ`��
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Answer_Return_Float > 0)
        {
            switch (_Basic_Key_String)
            {
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("Value�U" + (Answer_Return_Float));
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
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Key_String)
        {
            case "Syndrome_Cnidocytes_1":
            case "Syndrome_Cnidocytes_2":
            case "Syndrome_Cnidocytes_3":
            case "Syndrome_Cnidocytes_4":
                {
                    //�ϥΪ̬��ۨ�
                    if (UsingObject == _Basic_Owner_Script._Basic_Object_Script)
                    {
                        if (UserSource.Source_Card != null)
                        {
                            //���ҧP�w
                            List<string> QuickSave_Tag_StringList =
                            UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                            List<string> QuickSave_CheckTag_StringList = new List<string> { "Attach" };
                            if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                            {
                                //�����ؼЮĪG
                                string QuickSave_ValueKey_String =
                                    "ConstructNumber_EffectObject_Cnidocytes_CnidocytesAllergy_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                string QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                float QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                                Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                    GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float), 
                                    _Basic_Source_Class, TargetSource, 
                                    HateTarget, Action, Time, Order));
                            }
                        }
                    }
                }
                break;

            case "Syndrome_DemonSacrifice_1":
            case "Syndrome_DemonSacrifice_2":
            case "Syndrome_DemonSacrifice_3":
            case "Syndrome_DemonSacrifice_4":
                {
                    if (UserSource.Source_Creature != null &&
                        TargetSource.Source_Creature != null)
                    {
                        if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            //�����ۨ��ĪG
                            string QuickSave_ValueKey_String =
                                "ConstructNumber_EffectObject_Dark_DemonSacrifice_0�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                _Basic_Source_Class, UserSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default", 
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                _Basic_Source_Class, UserSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            string QuickSave_Effect_String = QuickSave_Key_String.Split("�U"[0])[0].Replace("ConstructNumber_", "");

                            Answer_Return_StringList.AddRange(_World_Manager._Effect_Manager.
                                GetEffectObject(QuickSave_Effect_String, Mathf.RoundToInt(QuickSave_Value_Float), 
                                _Basic_Source_Class, UserSource, 
                                HateTarget, Action, Time, Order));
                        }
                    }
                }
                break;

            case "Syndrome_LightfieldShadow_1":
            case "Syndrome_LightfieldShadow_2":
                {
                    if(!Action)
                    {
                        break;
                    }
                    if (UserSource.Source_Card != null)
                    {
                        //���ҧP�w
                        List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Light" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //���v�P�w
                            string QuickSave_ValueKey_String =
                                "Probability_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                            string QuickSave_Key_String =
                                _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String, 
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            float QuickSave_Value_Float =
                                _World_Manager.Key_NumbersUnit("Default", 
                                QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String], 
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);

                            if (_World_Manager.DiceRandom(1, 0, 1) < QuickSave_Value_Float)
                            {
                                //�������d��
                                QuickSave_ValueKey_String =
                                    "Throw_Default_Default�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
                                QuickSave_Key_String =
                                    _World_Manager.Key_KeysUnit("Default", QuickSave_ValueKey_String,
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QuickSave_Value_Float =
                                    _World_Manager.Key_NumbersUnit("Default",
                                    QuickSave_Key_String, _Basic_Data_Class.Numbers[QuickSave_ValueKey_String],
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);

                                _World_Manager._UI_Manager._UI_CardManager.
                                    CardThrow("Random", Mathf.RoundToInt(QuickSave_Value_Float), null,
                                    _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                    HateTarget, Action, Time, Order);
                            }
                        }
                    }
                }
                break;

            case "Syndrome_MaliciousThorn_1":
            case "Syndrome_MaliciousThorn_2":
                {
                    if (UserSource.Source_Card != null)
                    {
                        List<string> QuickSave_Tag_StringList =
                                UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(TargetSource, UsingObject, true);
                        List<string> QuickSave_CheckTag_StringList = new List<string> { "Vine" };
                        if (_World_Manager._Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                        {
                            //�ۧڶˮ`
                            Answer_Return_StringList.AddRange(
                                QuickSave_Source_Class.Source_BattleObject.Damaged(
                                    Key_Pursuit(
                                        _Basic_Source_Class, QuickSave_Source_Class, UsingObject,
                                        HateTarget, Action, Time, Order),
                                        UsingObject, 
                                        HateTarget, Action, Time, Order));
                        }
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
        SourceClass UserSource,SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
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
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion

    #region - Dead -
    public List<string> Key_Effect_DeadResist(bool IsResist, string Type/*���`�ɶˮ`����(�ĪG�B�ˮ`��)*/, float Value,
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
        switch (_Basic_Key_String)
        {
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Answer_Return_StringList.Add("BoolTrue�U" + Answer_Return_Bool.ToString());
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    public List<string> Key_Effect_Dead(string Type/*���`�ɶˮ`����(�ĪG�B�ˮ`��)*/, float Value,
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
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    #endregion
    #region - Attack -

    //�欰�˯����]��/�ۦ����]��欰�[���޾�//�ëDSituation�I�s�A�ӬO�z�L�QSituation�I�s���Ұ�(�pDamage)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<DamageClass> Key_Pursuit(SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)//���]��H����
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<DamageClass> Answer_Return_ClassList = new List<DamageClass>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        string QuickSave_Number_String = _Basic_Key_String.Split("_"[0])[2];
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        //�Ʀr�]�w

        switch (_Basic_Key_String)
        {
            case "Syndrome_SplashGravel_1":
            case "Syndrome_SplashGravel_2":
            case "Syndrome_SplashGravel_3":
            case "Syndrome_SplashGravel_4":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "PursuitTimes_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_PlantArmy_1":
            case "Syndrome_PlantArmy_2":
            case "Syndrome_PlantArmy_3":
            case "Syndrome_PlantArmy_4":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UUser_Default_Default_Data_CardsCount_Board_Select(Herb|Wood|Vine)�U" + QuickSave_Number_String;
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
                        "PursuitTimes_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_Cnidocytes_1":
            case "Syndrome_Cnidocytes_2":
            case "Syndrome_Cnidocytes_3":
            case "Syndrome_Cnidocytes_4":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "PursuitTimes_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_InheritedMartial_1":
            case "Syndrome_InheritedMartial_2":
            case "Syndrome_InheritedMartial_3":
            case "Syndrome_InheritedMartial_4":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_StarkDamage_Type" + QuickSave_Number_String +
                        "�UUser_Default_Default_Status_Catalyst_Default_Default�U" + QuickSave_Number_String;
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
                        "PursuitTimes_StarkDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_ThousandEdges_1":
            case "Syndrome_ThousandEdges_2":
            case "Syndrome_ThousandEdges_3":
            case "Syndrome_ThousandEdges_4":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_SlashDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "PursuitTimes_SlashDamage_Type" + QuickSave_Number_String +
                        "�UUser_Driving_Blade_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_IronMoon_1":
            case "Syndrome_IronMoon_2":
            case "Syndrome_IronMoon_3":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_AbstractDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "PursuitTimes_AbstractDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_BondConnection_1":
            case "Syndrome_BondConnection_2":
            case "Syndrome_BondConnection_3":
            case "Syndrome_BondConnection_4":
                {
                    string QuickSave_AttackType_String = 
                        _Basic_SaveData_Class.StringDataGet(_Basic_Key_String);
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_"+ QuickSave_AttackType_String + "_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
                    string QuickSave_Key01_String =
                        _World_Manager.Key_KeysUnit("Default", 
                        QuickSave_ValueKey01_String, 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                    float QuickSave_Value01_Float =
                        _World_Manager.Key_NumbersUnit("Default", 
                        QuickSave_Key01_String, _Basic_SaveData_Class.ValueDataGet(_Basic_Key_String), 
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);

                    string QuickSave_ValueKey02_String =
                        "PursuitTimes_"+ QuickSave_AttackType_String + "_Type0�UValue_Default_Default_Default_Default_Default_Default�U0";
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

            case "Syndrome_PhantasmalFear_1":
            case "Syndrome_PhantasmalFear_2":
            case "Syndrome_PhantasmalFear_3":
                {
                    string QuickSave_ValueKey01_String =
                        "PursuitNumber_AbstractDamage_Type" + QuickSave_Number_String +
                        "�UTarget_Default_Default_Stack_Default_Default_Fear�U" + QuickSave_Number_String;
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
                        "PursuitTimes_AbstractDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_WeightBurden_1":
            case "Syndrome_WeightBurden_2":
            case "Syndrome_WeightBurden_3":
            case "Syndrome_WeightBurden_4":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_ImpactDamage_Type" + QuickSave_Number_String +
                        "�UUser_Driving_Default_Status_Weight_Default_Defult�U" + QuickSave_Number_String;
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
                        "DamageTimes_ImpactDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_MassUprising_1":
            case "Syndrome_MassUprising_2":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_StarkDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "DamageTimes_StarkDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_UnexpectedShattering_1":
            case "Syndrome_UnexpectedShattering_2":
            case "Syndrome_UnexpectedShattering_3":
            case "Syndrome_UnexpectedShattering_4":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "DamageTimes_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_MaliciousThorn_1":
            case "Syndrome_MaliciousThorn_2":
            case "Syndrome_MaliciousThorn_3":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "DamageTimes_PunctureDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_UncontrolledWhirl_1":
            case "Syndrome_UncontrolledWhirl_2":
            case "Syndrome_UncontrolledWhirl_3":
            case "Syndrome_UncontrolledWhirl_4":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_ImpactDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "DamageTimes_ImpactDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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

            case "Syndrome_MourningCoral_1":
            case "Syndrome_MourningCoral_2":
            case "Syndrome_MourningCoral_3":
            case "Syndrome_MourningCoral_4":
                {
                    string QuickSave_ValueKey01_String =
                        "DamageValue_StarkDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
                        "DamageTimes_StarkDamage_Type" + QuickSave_Number_String +
                        "�UValue_Default_Default_Default_Default_Default_Default�U" + QuickSave_Number_String;
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
        //�ϥέ쥻�ƭ�
        return Answer_Return_ClassList;
        //----------------------------------------------------------------------------------------------------
    }
    public string Key_AttackType(string Type, string Key)
    {
        //�^��----------------------------------------------------------------------------------------------------
        string Answer_Return_String = _Basic_Data_Class.Keys[Key];
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_String;
        //----------------------------------------------------------------------------------------------------

    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - Effect -
    //�欰�˯����]��/�ۦ����]��欰�[���޾ɡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> Key_Effect(SourceClass Source, SourceClass Target, _Map_BattleObjectUnit HateTarget, bool Action)//���]��H����
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        int QuickSave_EffectNumber_Int = 0;
        string QuickSave_EffectKey_String = "";
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        //�Ʀr�]�w
        switch (_Basic_Key_String)
        {
            #region - Common -
            #endregion
        }
        //�ϥέ쥻�ƭ�
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion
    #endregion KeyAction
}
