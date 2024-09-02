using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class _UI_Card_Unit : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //����----------------------------------------------------------------------------------------------------
    //�S��
    public _View_Card_Unit _Basic_View_Script;
    //----------------------------------------------------------------------------------------------------

    //Data----------------------------------------------------------------------------------------------------
    public string _Basic_Key_String;
    //�Ȧs
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    //Data
    public _Skill_ExploreUnit _Card_ExploreUnit_Script;
    public _Skill_BehaviorUnit _Card_BehaviorUnit_Script;
    public _Skill_EnchanceUnit _Card_EnchanceUnit_Script;

    public Dictionary<string, _Skill_ExploreUnit> _Card_ReplaceExplore_Dicitonary = 
        new Dictionary<string, _Skill_ExploreUnit>();//��������
    public Dictionary<string, _Skill_BehaviorUnit> _Card_ReplaceBehavior_Dicitonary = 
        new Dictionary<string, _Skill_BehaviorUnit>();//��������
    public Dictionary<string, _Skill_EnchanceUnit> _Card_ReplaceEnchance_Dicitonary = 
        new Dictionary<string, _Skill_EnchanceUnit>();//��������
    public Dictionary<string, _Skill_EnchanceUnit> _Card_SpecialUnit_Dictionary = //�S����(�p�GLoading�˶�)
        new Dictionary<string, _Skill_EnchanceUnit>();

    public bool _State_ExploreCanUse_Bool = false;
    public bool _State_BehaviorCanUse_Bool = false;
    public bool _State_EnchanceCanUse_Bool = false;
    public string _State_CannotUseCause_String;

    public List<_UI_Card_Unit> _State_EnchanceStore_ScriptsList = new List<_UI_Card_Unit>();
    public List<_Effect_EffectCardUnit> _Effect_Effect_ScriptsList = new List<_Effect_EffectCardUnit>();
    public List<_Effect_EffectCardUnit> _Effect_Enchance_ScriptsList = new List<_Effect_EffectCardUnit>();
    public Dictionary<_Effect_EffectCardUnit, _Map_BattleObjectUnit> _Effect_Loading_Dictionary = 
        new Dictionary<_Effect_EffectCardUnit, _Map_BattleObjectUnit>();
    public bool _State_EnchanceEnchancing_Bool = false;//���]���L�d����
    public bool _State_AddenAction_Bool = false;

    public SourceClass _Basic_Source_Class;
    //----------------------------------------------------------------------------------------------------

    //�d���ϥ�----------------------------------------------------------------------------------------------------
    //�d��C
    public PathCollectClass _Range_Path_Class = new PathCollectClass();
    public SelectCollectClass _Range_Select_Class = new SelectCollectClass();
    public PathSelectPairClass _Range_UseData_Class = new PathSelectPairClass();
    //�ޯ�I�񤤤�//�Q�I�����a��
    public Vector _Card_StartCenter_Class;//�}�l�B
    public Vector _Card_UseCenter_Class;//�ﶵ(Select)(���I)�B
    public _Map_BattleObjectUnit _Card_UseObject_Script;
    public _Map_BattleObjectUnit _Card_LastUseObject_Script;//�W�ӨϥΪ�(����ڧ@��)
    public List<_Map_BattleObjectUnit> _Card_MoveObject_ScriptsList = new List<_Map_BattleObjectUnit>();
    public Transform _View_EffectStore_Transform;
    public int _Round_DelayBefore_Int;
    public int _Round_DelayAfter_Int;
    //----------------------------------------------------------------------------------------------------

    //����----------------------------------------------------------------------------------------------------
    //�d�ծɦ�m/�ΥH�٭�
    public int _Card_Position_Int;

    //������
    public _Skill_FactionUnit _Card_OwnerFaction_Script;
    //���
    public RoundElementClass _Round_Unit_Class;
    public RoundSequenceUnitClass _Round_GroupUnit_Class;

    //��w�ؼ�
    public List<_Map_BattleObjectUnit> _Card_AimTargets_ScriptsList = new List<_Map_BattleObjectUnit>();//�˷ǳQ��/�L�P�w�e
    public List<_Map_BattleObjectUnit> _Card_HitTargets_ScriptsList = new List<_Map_BattleObjectUnit>();//�R���ؼ�/�Q�j�ҫh�L��

    public string _Card_NowPosition_String;
    //���B�Ʀr/�ΥH�Ϥ��d���H����
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    void Awake()
    {
        //----------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild

    #region UseCard
    //�d�P�ϥΫe�m(�]�m�P����)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void UseCardStart(string Type, Vector UseCoordinate = null,_Object_NPCUnit.AIFindSave FindOrder = null)
    {
        //�]�w----------------------------------------------------------------------------------------------------
        //���|
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;

        _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //�a�Ͻվ�----------------------------------------------------------------------------------------------------
        //�s���d��
        switch (Type)
        {
            case "Field":
                _Basic_View_Script.MouseOverOut("Explore");
                //_Basic_View_Script.MouseClickOff("Explore", false);//---
                //_Basic_View_Script.CanUseSet(_State_ExploreCanUse_Bool, true);
                _Basic_View_Script._View_Offset_Transform.localPosition = Vector3.zero;

                _Card_StartCenter_Class =
                    new Vector(QuickSave_Creature_Script._Creature_FieldObjectt_Script._Map_Coordinate_Class);
                _Card_UseCenter_Class =
                    new Vector(UseCoordinate);
                if (QuickSave_Creature_Script._Card_UsingObject_Script == null)
                {
                    print("CardStartOn");
                }
                _Card_UseObject_Script =
                    QuickSave_Creature_Script._Card_UsingObject_Script;

                //�����d��
                _Map_Manager.ViewOff("Select");
                _Map_Manager.ViewOff("Path");
                _Map_Manager.ViewOff("Range");
                //����
                UseCardEffect();
                break;
            case "Battle":
            case "AI":
                {
                    switch (Type)
                    {
                        case "Battle":
                            {
                                //��ı�]�w
                                for (int a = 0; a < QuickSave_Creature_Script._Card_CardsBoard_ScriptList.Count; a++)
                                {
                                    //�������s�ܭ�l�欰���A
                                    QuickSave_Creature_Script._Card_CardsBoard_ScriptList[a]._Basic_View_Script.SimpleSet("Behavior");
                                }
                                _Basic_View_Script.MouseOverOut("Behavior");
                                foreach (_UI_Card_Unit Enchance in _State_EnchanceStore_ScriptsList)
                                {
                                    Enchance._Basic_View_Script.MouseOverOut("Enchance", true);
                                }

                                //�]�w�ޯत��
                                _Card_StartCenter_Class =
                                    new Vector(QuickSave_Creature_Script._Basic_Object_Script.
                                    TimePosition(_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int));
                                _Card_UseCenter_Class =
                                    new Vector(UseCoordinate);
                            }
                            break;
                        case "AI":
                            {
                                //�]�w�ޯत��
                                _Card_StartCenter_Class = new Vector(FindOrder.StartCoordinate);
                                _Card_UseCenter_Class = new Vector(_Range_UseData_Class.Select[0].Vector);
                            }
                            break;
                    }
                    //GroupUnit����Preview
                    _Round_GroupUnit_Class.Type = "Normal";
                    //�����d��
                    _Map_Manager.ViewOff("Select");
                    _Map_Manager.ViewOff("Path");
                    _Map_Manager.ViewOff("Range");

                    //Combo�p��
                    float QuickSave_Combo_Float =
                        QuickSave_Creature_Script._Basic_Object_Script._Basic_SaveData_Class.ValueDataGet("Combo", 1) + 1;
                    QuickSave_Creature_Script._Basic_Object_Script._Basic_SaveData_Class.ValueDataSet(
                        "Combo", QuickSave_Creature_Script._Basic_Object_Script._Basic_SaveData_Class.ValueDataGet("Combo", 1) + 1);
                    _Basic_SaveData_Class.ValueDataSet("Combo", QuickSave_Combo_Float);

                    //�[�J�欰��
                    _Card_UseObject_Script._Round_UnitCards_ClassList.Add(_Round_Unit_Class);
                    QuickSave_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList.Add(_Round_Unit_Class);
                    //��m�ܧ����
                    int QuickSave_Order_Int =
                        _Map_Manager._Map_BattleRound.RoundUnit(_Round_DelayBefore_Int).Count - 1;
                    if (_World_Manager._Skill_Manager.TagContains(
                        _Card_BehaviorUnit_Script.Key_OwnTag(null, QuickSave_Creature_Script._Card_UsingObject_Script, true),
                        new List<string> { "Select(ProgressiveMove|InstantMove|UniqueMove)" }, false))
                    {
                        _Map_BattleObjectUnit QuickSave_TimePos_Script =
                            _World_Manager._Object_Manager.
                            ObjectSet("TimePos", "",
                            _Card_UseCenter_Class, SupportSource: _Basic_Source_Class,
                            _Round_DelayBefore_Int, QuickSave_Order_Int);
                        _Basic_SaveData_Class.ObjectDataSet("TimePos", QuickSave_TimePos_Script);
                    }
                    //�ɶ���m
                    Dictionary<string, PathPreviewClass> QuickSave_PathPreview_Class =
                        _Card_BehaviorUnit_Script.
                        Key_Anime(_Card_UseObject_Script, null, false, _Round_DelayBefore_Int, QuickSave_Order_Int);
                    foreach (PathPreviewClass PathPreview in QuickSave_PathPreview_Class.Values)
                    {
                        _Map_BattleObjectUnit QuickSave_Object_Script =
                            PathPreview.UseObject;
                        QuickSave_Object_Script.
                            TimePositionAdd(_Round_DelayBefore_Int, QuickSave_Order_Int, PathPreview.FinalCoor);
                        _Card_MoveObject_ScriptsList.Add(QuickSave_Object_Script);
                    }
                    //�]�w��ı
                    _Map_Manager._Map_BattleRound.SequenceView();

                    //�����d��
                    UseCardEnd(Delay: true);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�d�P�ϥΤ��m-�ĪG/�}�l�d���ϥΡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void UseCardEffect()
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        //----------------------------------------------------------------------------------------------------

        //�^�X�i��----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    _Object_CreatureUnit QuickSave_Creature_Script =
                        _Basic_Source_Class.Source_Creature;
                    //�I�s
                    QuickSave_Creature_Script._Round_State_String = "Skill";
                    QuickSave_Creature_Script._Basic_Object_Script.
                        SituationCaller("Skill", null,
                        QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null, null,
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    _Map_Manager.FieldStateSet("AnimeMiddle", "�����ʵe����]�m�A�i��ʵe");
                    //�ʵe
                    _Card_ExploreUnit_Script.Key_Anime();

                }
                break;
            case "Battle":
                _Map_Manager.BattleStateSet("BuildMiddle", "�P�_�ؼЧ����A�ؼжi��欰");
                if (!_State_AddenAction_Bool)
                {
                    //������m�w��
                    int QuickSave_Order_Int =
                        _Map_Manager._Map_BattleRound.RoundUnit(_Round_DelayBefore_Int).Count - 1;
                    foreach (_Map_BattleObjectUnit Object in _Card_MoveObject_ScriptsList)
                    {
                        Object.
                            TimePositionRemove(_Round_DelayBefore_Int, QuickSave_Order_Int);
                    }
                    //�ʵe
                    if (_Card_UseObject_Script == null)
                    {
                        print("Wrong:" + _Card_BehaviorUnit_Script._Basic_Key_String);
                    }
                }
                _Card_BehaviorUnit_Script.Key_Anime(_Card_UseObject_Script, 
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�d�P�ϥΤ��m-�����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void UseCardEffectEnd(Dictionary<string, PathPreviewClass> PathPreview/*�w�g�˴��L�F(ActionCall���ݭn�A����)*/)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                _Map_Manager.FieldStateSet("AnimeBack", "�����ʵe�A�����P�ĪG");
                _Card_ExploreUnit_Script.Key_Effect(_Card_UseObject_Script);
                _Card_ExploreUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Times");
                UseCardEnd();
                break;
            case "Battle":
                {
                    _Map_Manager.BattleStateSet("AnimeBack", "�����ʵe�A�����P�ĪG");
                    //�R��TimePos/Project
                    CreatureObjectDelete();
                    //�����H
                    _Map_BattleObjectUnit QuickSave_HateObject_Script = null;
                    if (QuickSave_Creature_Script._NPC_Script != null)
                    {
                        QuickSave_HateObject_Script =
                            QuickSave_Creature_Script._NPC_Script._AI_HateTarget_Script;
                    }
                    //�ؼ�
                    List<string> QuickSave_ActionCall_StringList = _Card_BehaviorUnit_Script.
                        Key_ActionCall(PathPreview, 
                        QuickSave_HateObject_Script, true, 
                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

                    _Card_BehaviorUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Times");
                    _Card_EnchanceUnit_Script._Basic_TimesLimit_Class.TimesLimit_Reset("Times");
                    foreach (_Skill_EnchanceUnit Special in _Card_SpecialUnit_Dictionary.Values)
                    {
                        Special._Basic_TimesLimit_Class.TimesLimit_Reset("Times");
                    }

                    //Ĳ�o�P�w
                    //if(_World_Manager._Privilege_Miio_Bool)   �o�� �W��Ĳ�o(���u���� ��n���� �s�P �e�m���� ������);
                    if (!_State_AddenAction_Bool &&
                        QuickSave_Creature_Script._Player_Script != null &&
                        QuickSave_ActionCall_StringList.Count > 0 &&
                            QuickSave_ActionCall_StringList[QuickSave_ActionCall_StringList.Count - 1].Contains("React"))
                    {
                        _World_Manager._UI_Manager.Effect("React", true);
                        _Map_Manager._State_Reacting_Bool = true;
                        _Map_Manager._State_ReactTag_StringList =
                            _Card_BehaviorUnit_Script.Key_OwnTag(null, _Card_UseObject_Script, true);

                        _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script = this;
                        UseCardEnd();
                        _World_Manager._UI_Manager._View_Battle.FocusSet(_Card_UseObject_Script);
                        QuickSave_Creature_Script._Basic_Object_Script.
                            SituationCaller("React", null,
                            _Card_BehaviorUnit_Script._Basic_Source_Class, null, _Card_UseObject_Script,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        //���q
                        if (QuickSave_Creature_Script._Player_Script != null)
                        {
                            //�}�_�ͪ���T���
                            _World_Manager._Object_Manager.ColliderTurnOn();
                            _Map_Manager.BattleStateSet("PlayerBehavior", "Ĳ�o���\�A��ܥd��");
                            return;
                        }
                        if (QuickSave_Creature_Script._NPC_Script != null)
                        {
                            _Map_Manager.BattleStateSet("EnemySelect", "�e���]�m�����ANPC�i��欰");
                            print("EnemyReact:�i�঳���~");
                            StartCoroutine(QuickSave_Creature_Script._NPC_Script.NPC_AI_Action());
                            return;
                        }
                    }
                    else
                    {
                        //��n
                        QuickSave_Creature_Script._Round_State_String = "DelayAfter";
                        QuickSave_Creature_Script._Basic_Object_Script.
                            SituationCaller("DelayAfter", null,
                            QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null, _Card_UseObject_Script,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        //��������
                        _Map_BattleRound.RoundSequenceSet(
                            null, _Map_BattleRound._Round_GroupUnit_Class);
                        //�^�X����
                        _Map_Manager.BattleStateSet("RoundTimes", "�欰�^�X�����A�^���ܦ欰");
                        //�^�X�I�s
                        StartCoroutine(_Map_BattleRound.RoundCall(this));
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void CreatureObjectDelete()
    {
        _Map_BattleObjectUnit QuickSave_Object_Script =
            _Basic_SaveData_Class.ObjectDataGet("TimePos");
        if (QuickSave_Object_Script != null)
        {
            QuickSave_Object_Script.DeleteSet();
            _Basic_SaveData_Class.ObjectDataSet("TimePos", null);
        }

        List<_Map_BattleObjectUnit> QuickSave_Object_ScriptsList =
            _Basic_SaveData_Class.ObjectListDataGet("Project");
        foreach (_Map_BattleObjectUnit Object in QuickSave_Object_ScriptsList)
        {
            Object.DeleteSet();
        }
        _Basic_SaveData_Class.ObjectListDataSet("Project", null);
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�d�P�ϥΫ�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void UseCardEnd(bool Delay = false, bool SelectUsing = false, 
        List<_UI_Card_Unit> EventCards = null)
    {
        //----------------------------------------------------------------------------------------------------
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //�R���P���m----------------------------------------------------------------------------------------------------
        _Card_UseObject_Script._Round_UnitCards_ClassList.Remove(_Round_Unit_Class);
        QuickSave_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList.Remove(_Round_Unit_Class);
        //�إ߲M��
        List<_UI_Card_Unit> QuickSave_UsedList_ScriptList = new List<_UI_Card_Unit>();
        if (!SelectUsing)
        {
            QuickSave_UsedList_ScriptList.Add(this);
        }
        if (EventCards == null)
        {
            QuickSave_UsedList_ScriptList.AddRange(_State_EnchanceStore_ScriptsList);
        }
        else
        {
            QuickSave_UsedList_ScriptList.AddRange(EventCards);
        }
        //�Y�N�R�����X�M��
        if (!SelectUsing)//�DSelect
        {
            foreach (_UI_Card_Unit Card in _Card_OwnerFaction_Script._Faction_DeleteLeaves_ScriptsList)
            {
                if (QuickSave_UsedList_ScriptList.Contains(Card))
                {
                    QuickSave_UsedList_ScriptList.Remove(Card);
                }
            }
        }
        //��ı�٭�
        for (int a= 0; a < QuickSave_UsedList_ScriptList.Count; a++)
        {
            QuickSave_UsedList_ScriptList[a]._Basic_View_Script._View_Name_Text.material = null;
            QuickSave_UsedList_ScriptList[a].transform.localPosition = Vector3.zero;
        }
        //�̷Ӧ�m�Ƨ�
        //QuickSave_UsedList_ScriptList.Sort((x, y) => x._Card_Position_Int.CompareTo(y._Card_Position_Int));
        //����{�s�d��
        _UI_CardManager.BoardRefresh(QuickSave_Creature_Script);
        //��l��
        _UI_CardManager._Card_UsingCard_Script = null;
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                {
                    switch (_Map_Manager._State_FieldState_String)
                    {
                        case "AnimeBack":
                            //���m��/����
                            _Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 6;
                            //�����d��
                            _UI_CardManager.
                                CardsMove("Board_Use_Cemetery", QuickSave_Creature_Script,
                                QuickSave_UsedList_ScriptList,
                                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            //��l��
                            _Card_UseObject_Script = null;
                            //�ɶ�
                            int QuickSave_FieldTime_Unt =
                                _Card_ExploreUnit_Script._Basic_Data_Class.TimePass;
                            QuickSave_Creature_Script.BuildBack(QuickSave_FieldTime_Unt);
                            break;
                        case "EventBack":
                            if (!SelectUsing)//�DSelect
                            {
                                //���m��/����
                                _Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 8;
                                //��l��
                                _Card_UseObject_Script = null;
                            }
                            //�����d��
                            _UI_CardManager.
                                CardsMove("Board_Use_Cemetery", QuickSave_Creature_Script,
                                QuickSave_UsedList_ScriptList,
                                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            for (int a = 1; a < QuickSave_UsedList_ScriptList.Count; a++)
                            {
                                //���m��/���]
                                QuickSave_UsedList_ScriptList[a]._Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 1;
                            }
                            break;
                    }
                }
                break;
            case "Battle":
                {
                    string QuickSave_EnchanceType_String = _Card_BehaviorUnit_Script.Key_Enchance();
                    //�R���P���m
                    if (Delay)//�[�J����
                    {
                        //�d���ܩ���
                        _UI_CardManager.
                            CardsMove("Board_Use_Delay", QuickSave_Creature_Script, QuickSave_UsedList_ScriptList,
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        //�e�m����ĪG
                        QuickSave_Creature_Script.BuildBack();
                        QuickSave_Creature_Script._Round_State_String = "DelayBefore";
                        QuickSave_Creature_Script._Basic_Object_Script.
                            SituationCaller("DelayBefore", null,
                            QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null, _Card_UseObject_Script,
                             null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                    else//�ĪG����
                    {
                        //���m��/�欰
                        _Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 4;
                        //AddenAction���X
                        if (_State_AddenAction_Bool)
                        {
                            _State_AddenAction_Bool = false;
                            return;
                        }
                        //�����d��
                        _Card_LastUseObject_Script = _Card_UseObject_Script;
                        _Card_UseObject_Script = null;
                        //�欰
                        QuickSave_UsedList_ScriptList.Remove(this);
                        _UI_CardManager.
                            CardsMove("Delay_End_Cemetery",
                            QuickSave_Creature_Script, new List<_UI_Card_Unit> { this },
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        //���]
                        switch (QuickSave_EnchanceType_String)
                        {
                            case "Loading":
                                {
                                    if (_Effect_Enchance_ScriptsList.Count > 0)
                                    {
                                        foreach (_Effect_EffectCardUnit Enchance in _Effect_Enchance_ScriptsList)
                                        {
                                            Enchance._Basic_Owner_Script._Card_UseObject_Script.
                                                _Basic_SaveData_Class.SourceListDataAdd("Loading", _Basic_Source_Class);
                                            Enchance._Basic_Owner_Script._Card_UseObject_Script.
                                                _Basic_SaveData_Class.BoolDataSet("Loading", true);
                                            _Effect_Loading_Dictionary.Add(
                                                Enchance, Enchance._Basic_Owner_Script._Card_UseObject_Script);
                                        }
                                    }
                                    //���]�ܹӦa
                                    foreach (_UI_Card_Unit Card in QuickSave_UsedList_ScriptList)
                                    {
                                        //���m��/���]
                                        Card._Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 1;
                                        //���]����
                                        Card.EnchanceSet("Remove", _Card_BehaviorUnit_Script.Key_Enchance(), this, null);
                                    }
                                }
                                break;
                            default:
                                {
                                    //���]�ܹӦa
                                    foreach (_UI_Card_Unit Card in QuickSave_UsedList_ScriptList)
                                    {
                                        //���m��/���]
                                        Card._Card_UseObject_Script._Basic_Status_Dictionary["Proficiency"] += 1;
                                        //���]����
                                        Card.EnchanceSet("Remove", _Card_BehaviorUnit_Script.Key_Enchance(), this, null);
                                    }
                                    //�˶�s�W
                                    foreach (_Effect_EffectCardUnit Enchance in _Effect_Loading_Dictionary.Keys)
                                    {
                                        _UI_Card_Unit QuickSave_Card_Script = Enchance._Basic_Owner_Script;
                                        QuickSave_UsedList_ScriptList.Add(QuickSave_Card_Script);
                                        _Effect_Loading_Dictionary[Enchance]._Basic_SaveData_Class.SourceListDataSet("Loading", null);
                                        _Effect_Loading_Dictionary[Enchance]._Basic_SaveData_Class.BoolDataSet("Loading", false);
                                    }
                                    _UI_CardManager.
                                        CardsMove("Delay_End_Cemetery",
                                        QuickSave_Creature_Script, QuickSave_UsedList_ScriptList,
                                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                                break;
                        }
                        /* �{�b�˶�ݭn�A����d�~��ϥ�
                        switch (QuickSave_EnchanceType_String)
                        {
                            case "Enchance":
                                {
                                }
                                break;
                            case "Loading":
                                {
                                    //�^���P
                                    _UI_CardManager.
                                        CardsMove("Delay_Add_Board",
                                        QuickSave_Creature_Script, new List<_UI_Card_Unit> { this },
                                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                    //�ܹӦa
                                    QuickSave_UsedList_ScriptList.Remove(this);
                                    _UI_CardManager.
                                        CardsMove("Delay_End_Cemetery",
                                        QuickSave_Creature_Script, QuickSave_UsedList_ScriptList,
                                        _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                                }
                                break;
                        }*/
                        _Card_BehaviorUnit_Script.Key_UseReplaceUnit();
                    }
                }
                break;
        }
        if (!SelectUsing)
        {
            _Card_OwnerFaction_Script.DestroySkillLeaves();//����(�ͦ���)�M��(�ëD�M���ۤv!)
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion UseCard

    #region Math
    #region - ChangeUnit -
    ///�ܴ�Ex/Be/En�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ChangeUnit(string Type, string Key)
    {
        switch (Type)
        {
            case "Explore":
                {
                    _Card_ExploreUnit_Script = _Card_ReplaceExplore_Dicitonary[Key];
                }
                break;
            case "Behavior":
                {
                    _Card_BehaviorUnit_Script = _Card_ReplaceBehavior_Dicitonary[Key];
                }
                break;
            case "Enchance":
                {
                    _Card_EnchanceUnit_Script = _Card_ReplaceEnchance_Dicitonary[Key];
                }
                break;
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    #endregion
    #region - UseLicense -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public bool UseLicense(string Type, _Map_BattleObjectUnit UsingObject, 
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order,
        _UI_Card_Unit EnchanceTarget = null, List<string> ReactTag = null)
    {
        //----------------------------------------------------------------------------------------------------
        bool Answer_Return_Bool = true;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        if (UsingObject == null)
        {
            _State_CannotUseCause_String = "NoUsing";
            return true;
        }
        _Map_BattleObjectUnit QuickSave_Object_Script = 
            _Basic_Source_Class.Source_BattleObject;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        List<string> QuickSave_Data_StringList = new List<string> { Type };
        bool QuickSave_SituationCheck_Bool = bool.Parse(
            QuickSave_Object_Script.SituationCaller(
            "UseLicense", QuickSave_Data_StringList,
            QuickSave_Object_Script._Basic_Source_Class, _Basic_Source_Class, UsingObject,
            HateTarget, Action, Time, Order)["BoolFalse"][0]);
        if (!QuickSave_SituationCheck_Bool)
        {
            return false;
        }
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Explore -
            case "Explore":
                {
                    if (_World_Manager._Authority_Scene_String == "Battle")
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "SceneReturn";
                        goto ExploreEnd;
                    }
                    //�ϥΪ���P�w
                    List<string> QuickSave_CheckTag_StringList = UsingObject.Key_Tag(QuickSave_Object_Script._Basic_Source_Class, _Basic_Source_Class);
                    List<string> QuickSave_UseTags_StringList = _Card_ExploreUnit_Script.Key_UseTag(UsingObject);

                    if (!_Skill_Manager.TagContains(QuickSave_CheckTag_StringList, QuickSave_UseTags_StringList, true))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "UsingReturn";
                        goto ExploreEnd;
                    }
                    //�ۨ����ҧP�w
                    List<string> QuickSave_OwnTags_StringList = _Card_ExploreUnit_Script.Key_OwnTag(UsingObject);
                    //�ƥ����
                    List<string> QuickSave_EventTag_StringList = null;//�ϥα���
                    if (_Map_Manager._State_FieldState_String == "EventSelect" &&
                                        _UI_CardManager._Card_UsingCard_Script == null)
                    {
                        _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
                        QuickSave_EventTag_StringList = 
                            _UI_EventManager.
                            _Data_Event_Dictionary[_UI_EventManager._Event_NowEventKey_String].OwnTag;
                    }
                    for (int a = 0; a < QuickSave_OwnTags_StringList.Count; a++)
                    {
                        string QuickSave_Tag_String = QuickSave_OwnTags_StringList[a];
                        switch (QuickSave_Tag_String)
                        {
                            case "Empty":
                                Answer_Return_Bool = false;
                                _State_CannotUseCause_String = "TagReturn";
                                goto ExploreEnd;
                            case "Explore":
                                switch (_Map_Manager._State_FieldState_String)
                                {
                                    case "EventSelect":
                                    case "EventFrame":
                                        Answer_Return_Bool = false;
                                        _State_CannotUseCause_String = "TagReturn";
                                        goto ExploreEnd;
                                }
                                break;
                            case "Determine":
                                if (_Map_Manager._State_FieldState_String != "EventFrame" ||
                                    _UI_CardManager._Card_UsingCard_Script == null)
                                {
                                    Answer_Return_Bool = false;
                                    _State_CannotUseCause_String = "TagReturn";
                                    goto ExploreEnd;
                                }
                                break;
                            case "Plot":
                                {
                                    if (_Map_Manager._State_FieldState_String == "EventSelect" &&
                                        _UI_CardManager._Card_UsingCard_Script == null)
                                    {
                                        bool QuickSave_TagHave_Bool = false;
                                        foreach (string Key in QuickSave_EventTag_StringList)
                                        {
                                            if (QuickSave_OwnTags_StringList.Contains(Key))
                                            {
                                                QuickSave_TagHave_Bool = true;
                                                break;
                                            }
                                        }
                                        //���ŦX�ϥα���
                                        if (!QuickSave_TagHave_Bool)
                                        {
                                            Answer_Return_Bool = false;
                                            _State_CannotUseCause_String = "TagReturn";
                                        }
                                        //�������}
                                        goto ExploreEnd;
                                    }
                                    else
                                    {
                                        Answer_Return_Bool = false;
                                        _State_CannotUseCause_String = "TagReturn";
                                        goto ExploreEnd;
                                    }
                                }
                                break;
                            case "ProgressiveMove":
                            case "UniqueMove":
                                switch (UsingObject._Basic_Source_Class.SourceType)
                                {
                                    case "Weapon":
                                    case "Item":
                                    case "Object":
                                        if (UsingObject._Map_ObjectState_String != "Driving")
                                        {
                                            Answer_Return_Bool = false;
                                            _State_CannotUseCause_String = "TagReturn";
                                            goto ExploreEnd;
                                        }
                                        break;
                                }
                                break;
                            case "State":
                                switch (_Map_Manager._State_FieldState_String)
                                {
                                    case "EventSelect":
                                    case "EventFrame":
                                        Answer_Return_Bool = false;
                                        _State_CannotUseCause_String = "TagReturn";
                                        goto ExploreEnd;
                                }
                                switch (UsingObject._Map_ObjectState_String)
                                {
                                    case "Break":
                                        Answer_Return_Bool = false;
                                        _State_CannotUseCause_String = "BreakReturn";
                                        goto ExploreEnd;
                                }
                                break;
                        }
                    }
                    ExploreEnd:
                    _State_ExploreCanUse_Bool = Answer_Return_Bool;
                }
                break;
            #endregion

            #region - Behavior -
            case "Behavior":
                {
                    if (_World_Manager._Authority_Scene_String == "Field")
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "SceneReturn";
                        goto BehaviorEnd;
                    }
                    //�S��
                    //�Q�˶�
                    if (UsingObject._Basic_SaveData_Class.BoolDataGet("Loading") &&
                        !UsingObject._Basic_SaveData_Class.SourceListDataContain("Loading", _Basic_Source_Class))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "LoadingReturn";
                        goto BehaviorEnd;
                    }
                    //�ϥΪ���P�w
                    List<string> QuickSave_Tag_StringList = UsingObject.Key_Tag(_Basic_Source_Class, null);
                    List<string> QuickSave_UseTags_StringList = _Card_BehaviorUnit_Script.Key_UseTag(null, UsingObject);
                    if (!_Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_UseTags_StringList, true))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "UsingReturn";
                        goto BehaviorEnd;
                    }

                    //�ۦ�Tag����
                    List<string> QuickSave_OwnTags_StringList = _Card_BehaviorUnit_Script.Key_OwnTag(null, UsingObject, true);
                    for (int a = 0; a < QuickSave_OwnTags_StringList.Count; a++)
                    {
                        string QuickSave_Tag_String = QuickSave_OwnTags_StringList[a];
                        switch (QuickSave_Tag_String)
                        {
                            #region - Special -
                            case "Empty":
                                Answer_Return_Bool = false;
                                _State_CannotUseCause_String = "TagReturn";
                                goto BehaviorEnd;
                            case "ProgressiveMove":
                            case "UniqueMove":
                                switch (UsingObject._Basic_Source_Class.SourceType)
                                {
                                    case "Weapon":
                                    case "Item":
                                    case "Object":
                                        if (UsingObject._Map_ObjectState_String != "Driving")
                                        {
                                            Answer_Return_Bool = false;
                                            _State_CannotUseCause_String = "TagReturn";
                                            goto BehaviorEnd;
                                        }
                                        break;
                                }
                                break;
                            case "State":
                                switch (UsingObject._Map_ObjectState_String)
                                {
                                    case "Break":
                                        Answer_Return_Bool = false;
                                        _State_CannotUseCause_String = "BreakReturn";
                                        goto BehaviorEnd;
                                }
                                break;
                            #endregion
                        }
                    }
                    //�ۦ��S��
                    if (!_Card_BehaviorUnit_Script.Key_UseLicense(UsingObject, Action))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "UnitReturn";
                        goto BehaviorEnd;
                    }
                    //Ĳ�o
                    if (_World_Manager._Map_Manager._State_Reacting_Bool)
                    {
                        if (ReactTag != null)
                        {
                            List<string> QuickSave_ReactTag_StringList = _Card_BehaviorUnit_Script.Key_ReactTag(null, UsingObject);
                            if (!_Skill_Manager.TagContains(ReactTag, QuickSave_ReactTag_StringList, true))
                            {
                                Answer_Return_Bool = false;
                                _State_CannotUseCause_String = "ReactReturn";
                                goto BehaviorEnd;
                            }
                        }
                        else
                        {
                            Answer_Return_Bool = false;
                            _State_CannotUseCause_String = "ReactReturn";
                            goto BehaviorEnd;
                        }
                    }
                    BehaviorEnd:
                    _State_BehaviorCanUse_Bool = Answer_Return_Bool;
                }
                break;
            #endregion

            #region - Enchance -
            case "Enchance":
            case "Loading":
                {
                    if (_World_Manager._Authority_Scene_String == "Field")
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "SceneReturn";
                        goto EnchanceEnd;
                    }
                    if (EnchanceTarget == null)
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "EnchantReturn";
                        goto EnchanceEnd;
                    }
                    //�S��P�w
                    //�Q�˶�
                    if (UsingObject._Basic_SaveData_Class.BoolDataGet("Loading") &&
                        !UsingObject._Basic_SaveData_Class.SourceListDataContain("Loading", _Basic_Source_Class))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "LoadingReturn";
                        goto EnchanceEnd;
                    }
                    //�P�w
                    _Skill_Manager.BehaviorDataClass QuickSave_BehaviorData_Class = 
                        EnchanceTarget._Card_BehaviorUnit_Script._Basic_Data_Class;
                    _Skill_EnchanceUnit QuickSave_Enchance_Script = _Card_EnchanceUnit_Script;
                    if (_Card_SpecialUnit_Dictionary.TryGetValue(Type , out _Skill_EnchanceUnit DicValue))
                    {
                        QuickSave_Enchance_Script = DicValue;
                    }

                    if (EnchantCount(EnchanceTarget) >= QuickSave_BehaviorData_Class.Enchant)
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "EnchantReturn";
                        goto EnchanceEnd;
                    }

                    //�ͪ�Tag����/���䤤�@�ӴN�����
                    List<string> QuickSave_CreatureLimitState_StringList =
                        _Basic_Source_Class.Source_Creature._Basic_Object_Script.Key_Tag(_Basic_Source_Class, null);
                    List<string> QuickSave_CheckTag_StringList = new List<string> { "Stun" };
                    if (_Skill_Manager.TagContains(QuickSave_CreatureLimitState_StringList, QuickSave_CheckTag_StringList, false))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "EffectReturn";
                        goto EnchanceEnd;
                    }

                    //���󭭨�
                    List<string> QuickSave_Tag_StringList =
                        UsingObject.Key_Tag(_Basic_Source_Class, null);
                    QuickSave_CheckTag_StringList =
                        QuickSave_Enchance_Script.Key_SupplyTag(UsingObject);
                    if (!_Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "UsingReturn";
                        goto EnchanceEnd;
                    }

                    //�ۦ�Tag����
                    QuickSave_Tag_StringList =
                        EnchanceTarget._Card_BehaviorUnit_Script.
                        Key_OwnTag(null, EnchanceTarget._Card_UseObject_Script, true);
                    QuickSave_CheckTag_StringList =
                        QuickSave_Enchance_Script.Key_RequiredTag(UsingObject);
                    if (!_Skill_Manager.TagContains(QuickSave_Tag_StringList, QuickSave_CheckTag_StringList, true))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "EnchanceReturn";
                        goto EnchanceEnd;
                    }
                    //�ۦ��S��
                    if (!_Card_EnchanceUnit_Script.Key_UseLicense(EnchanceTarget,UsingObject,Action))
                    {
                        Answer_Return_Bool = false;
                        _State_CannotUseCause_String = "UnitReturn";
                        goto EnchanceEnd;
                    }
                    EnchanceEnd:
                    _State_EnchanceCanUse_Bool = Answer_Return_Bool;
                }
                break;
            #endregion
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (Action)
        {
            _Basic_View_Script.CanUseSet(Answer_Return_Bool, true);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------        
        return Answer_Return_Bool;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - EnchantCount -
    //���]���q�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public int EnchantCount(_UI_Card_Unit EnchanceTarget)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //�^�ǭ�
        int Answer_Count_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        for(int a =0; a< EnchanceTarget._State_EnchanceStore_ScriptsList.Count; a++)
        {
            Answer_Count_Int += 
                Mathf.RoundToInt(EnchanceTarget._State_EnchanceStore_ScriptsList[a]._Card_EnchanceUnit_Script._Basic_Data_Class.Enchant);
        }
        //----------------------------------------------------------------------------------------------------

        //�^�ǭ�----------------------------------------------------------------------------------------------------
        return Answer_Count_Int;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - EnchanceSet -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void EnchanceSet(string ActionType, string BehaviorType, _UI_Card_Unit Behavior, _Map_BattleObjectUnit UsingObject)
    {
        //----------------------------------------------------------------------------------------------------
        _Effect_EffectCardUnit QuickSave_Effect_Script = null;
        switch (BehaviorType)
        {
            case "Enchance":
                {
                    QuickSave_Effect_Script =
                        _Card_EnchanceUnit_Script._Owner_EnchanceEffectCard_Script;
                }
                break;
            case "Loading":
                {
                    QuickSave_Effect_Script =
                        _Card_SpecialUnit_Dictionary[BehaviorType]._Owner_EnchanceEffectCard_Script;
                }
                break;
        }
        switch (ActionType)
        {
            case "Add":
                {
                    //�[�J�ܸ�Ʈw
                    _State_EnchanceEnchancing_Bool = true;
                    Behavior._State_EnchanceStore_ScriptsList.Add(this);
                    Behavior._Effect_Enchance_ScriptsList.Add(QuickSave_Effect_Script);
                    _Card_EnchanceUnit_Script._Owner_EnchanceTarget_Script = Behavior;
                    _Card_UseObject_Script = UsingObject;
                }
                break;
            case "Remove":
                {
                    _State_EnchanceEnchancing_Bool = false;
                    Behavior._State_EnchanceStore_ScriptsList.Remove(this);
                    Behavior._Effect_Enchance_ScriptsList.Remove(QuickSave_Effect_Script);
                    _Card_EnchanceUnit_Script._Owner_EnchanceTarget_Script = null;
                    _Card_UseObject_Script = null;
                }
                break;
            case "Clear":
                {
                    //Behavior�ϥ�
                    foreach (_UI_Card_Unit Enchance in _State_EnchanceStore_ScriptsList)
                    {
                        Enchance.EnchanceSet("Remove", BehaviorType, this, null);
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion - EnchanceSet -
    #endregion Math
}
