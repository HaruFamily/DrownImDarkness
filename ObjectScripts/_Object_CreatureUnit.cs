using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class _Object_CreatureUnit : MonoBehaviour
{
    #region Element
    //�ܼƶ��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�����P�O----------------------------------------------------------------------------------------------------
    public string _Basic_Key_String;
    public string _Data_Sect_String;
    [HideInInspector] public LanguageClass _Basic_Language_Class;

    public _Object_PlayerUnit _Player_Script;
    public _Object_NPCUnit _NPC_Script;
    //[HideInInspector] public SourceClass _Basic_Source_Class;

    //���h��m
    public _Map_FieldObjectUnit _Creature_FieldObjectt_Script;
    //public _Map_BattleObjectUnit _Creature_BattleObjectt_Script;

    //�w�s��
    public Transform _UI_TotalStore_Transform;
    public Transform _View_SyndromeStore_Transform;
    //�a�ϥͩR��
    public Transform _UI_PointStore_Transform;
    public Transform _UI_MapMediumPoint_Transform;
    public Transform _UI_MapCatalystPoint_Transform;
    //----------------------------------------------------------------------------------------------------

    //��O�ȻP�ۦ�----------------------------------------------------------------------------------------------------
    //�D����
    public _Item_Object_Inventory _Object_Inventory_Script;
    //----------------------------------------------------------------------------------------------------

    //�԰��p��----------------------------------------------------------------------------------------------------
    public Transform _Map_Offset_Transform;
    public SpriteRenderer _Map_Sprite_SpriteRenderer;

    public int _Card_UsingObject_Int;
    public _Map_BattleObjectUnit _Card_UsingObject_Script;

    //�P�դ�
    public List<_UI_Card_Unit> _Card_CardsDeck_ScriptList = new List<_UI_Card_Unit>();
    //��P��
    public List<_UI_Card_Unit> _Card_CardsBoard_ScriptList = new List<_UI_Card_Unit>();
    //���𤤥d��
    public List<_UI_Card_Unit> _Card_CardsDelay_ScriptList = new List<_UI_Card_Unit>();
    //��ư�
    public List<_UI_Card_Unit> _Card_CardsCemetery_ScriptList = new List<_UI_Card_Unit>();
    //��v��
    public List<_UI_Card_Unit> _Card_CardsExiled_ScriptList = new List<_UI_Card_Unit>();
    //----------------------------------------------------------------------------------------------------

    //�^�X�p��----------------------------------------------------------------------------------------------------
    public string _Round_State_String;
    //�ݩR
    public bool _Round_Standby_Bool = true;

    public _Map_BattleObjectUnit _Basic_Object_Script;
    public List<_UI_Card_Unit> _Basic_CardRecentDeal_ScriptsList = new List<_UI_Card_Unit>();//�̪������d�P
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element

    #region - HoldToField -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void HoldToField()
    {
        _Object_Inventory_Script._Item_ReachObject_ScriptsList = 
            new List<_Map_BattleObjectUnit>(_Object_Inventory_Script._Item_EquipQueue_ScriptsList);
        _Object_Inventory_Script._Item_CarryObject_ScriptsList =
            new List<_Map_BattleObjectUnit>(_Object_Inventory_Script._Item_EquipQueue_ScriptsList);
        transform.localScale = Vector3.zero;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - FieldToHold -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FieldToHold()
    {
        #region - Faction -
        _Card_CardsDeck_ScriptList.Clear();
        _Card_CardsBoard_ScriptList.Clear();
        _Card_CardsDelay_ScriptList.Clear();
        _Card_CardsCemetery_ScriptList.Clear();
        _Card_CardsExiled_ScriptList.Clear();
        #endregion
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - FieldToBattle -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FieldToBattle()
    {
        transform.localScale = Vector3.one;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion

    #region - BattleToField -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void BattleToField()
    {
        _Object_Inventory_Script._Item_ReachObject_ScriptsList = 
            new List<_Map_BattleObjectUnit>(_Object_Inventory_Script._Item_EquipQueue_ScriptsList);
        _Object_Inventory_Script._Item_CarryObject_ScriptsList =
            new List<_Map_BattleObjectUnit>(_Object_Inventory_Script._Item_EquipQueue_ScriptsList);
        foreach (_Map_BattleObjectUnit DivingMapObject in _Object_Inventory_Script._Item_DrivingMapObject_ScriptsList)
        {
            DivingMapObject.StateSet("Abandoning",
                _Basic_Object_Script._Basic_Source_Class,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        }
        transform.localScale = Vector3.zero;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion


    #region Round
    //�^�X�_�l��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void BuildFront()
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Item_ConceptUnit QuickSave_Concept_Script = _Object_Inventory_Script._Item_EquipConcepts_Script;
        string QuickSave_CardsType_String = "";
        //----------------------------------------------------------------------------------------------------

        //�e���]�m----------------------------------------------------------------------------------------------------
        //�^�X�]�m
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                QuickSave_CardsType_String = "Explore";
                _Map_Manager.FieldStateSet("BuildFront", "�^�X�e���ظm�����A�}�l�d�P�ާ@");
                break;
            case "Battle":
                QuickSave_CardsType_String = "Behavior";
                _Map_Manager.BattleStateSet("BuildFront", "�P�_�ؼЬ��ͪ��A�ͪ��i��欰");
                break;
        }
        //�R�B�Ʀr
        //_Basic_Object_Script._Basic_SaveData_Class.ValueDataSet("LuckNumber", Random.Range(0, 65535));
        _World_Manager._UI_Manager._View_Battle.FocusSet(_Card_UsingObject_Script);
        //�ݩR�^�X
        if (_Round_Standby_Bool)
        {
            //��d
            _UI_Manager._UI_CardManager.
                CardDeal("Normal",
                Mathf.Clamp(QuickSave_Concept_Script.Key_Deal(), 0, 65535), null,
                _Basic_Object_Script._Basic_Source_Class, _Basic_Object_Script._Basic_Source_Class, null, 
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            _Round_Standby_Bool = false;
        }
        //----------------------------------------------------------------------------------------------------

        //�����u��(��ı)----------------------------------------------------------------------------------------------------
        #region View
        //��ı�]�w
        /*
        for (int a = 0; a < _Card_CardsBoard_ScriptList.Count; a++)
        {
            _Card_CardsBoard_ScriptList[a]._Basic_View_Script.SimpleSet(QuickSave_CardsType_String);
        }*/
        _UI_Manager._UI_CardManager.BoardRefresh(this);
        //�p�G�b�Y�ӫ���W
        if (_Player_Script != null && _UI_Manager._MouseTarget_GameObject != null)
        {
            switch (_UI_Manager._MouseTarget_GameObject.name)
            {
                //���ݾ����s�W
                case "UI_Standby":
                    if (_World_Manager._Authority_Scene_String == "Battle" &&
                        _Map_Manager._State_BattleState_String == "BuildFront" && 
                        !_Map_Manager._State_Reacting_Bool)
                    {
                        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
                        //�^�X�]�m
                        int QuickSave_DelayStandby_Int =
                            QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
                        RoundElementClass QuickSave_RoundUnit_Class = _Basic_Object_Script._Round_Unit_Class;
                        RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                            new RoundSequenceUnitClass
                            {
                                Type = "Preview",
                                Owner = _Basic_Object_Script,
                                RoundUnit = new List<RoundElementClass> { QuickSave_RoundUnit_Class }
                            };
                        QuickSave_RoundUnit_Class.DelayTime = QuickSave_DelayStandby_Int;
                        _Basic_Object_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Object_Script._Round_GroupUnit_Class, null);
                        //��ı
                        _Map_BattleRound.SequenceView();
                    }
                    if (_UI_Manager._MouseTarget_GameObject.TryGetComponent(out _UI_TextEffect QuickSave_Text_Script))
                    {
                        QuickSave_Text_Script.CoverOn();
                    }
                    break;

                //���d���W
                case "Card_Unit":
                    if (_UI_Manager._MouseTarget_GameObject.TryGetComponent(out _UI_Card_Unit QuickSave_Card_Script))
                    {
                        QuickSave_Card_Script._Basic_View_Script.
                            DetailSet(QuickSave_CardsType_String, 
                            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    }
                    break;
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //�^�X�i��----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                if (_Player_Script != null)
                {
                    //���a�^�X
                    _Map_Manager.FieldStateSet("SelectExplore", "�e���]�m�����A���a�i��欰");
                }                
                break;
            case "Battle":
                if (_Player_Script != null)
                {
                    //���a�^�X
                    _Map_Manager.BattleStateSet("PlayerBehavior", "�e���]�m�����A���a�i��欰");
                    //�}�_�ͪ���T���
                    _World_Manager._Object_Manager.ColliderTurnOn();
                }
                else if (_NPC_Script != null)
                {
                    //NPC�^�X
                    _Map_Manager.BattleStateSet("EnemySelect", "�e���]�m�����ANPC�i��欰");
                    StartCoroutine(_NPC_Script.NPC_AI_Action());
                    /*
                    //�����ݩR
                    _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
                    if (!_Map_Manager._State_Reacting_Bool)
                    {
                        //�^�X�]�m
                        int QuickSave_DelayStandby_Int =
                            QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
                        RoundElementClass QuickSave_RoundUnit_Class = _Basic_Object_Script._Round_Unit_Class;
                        RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                            new RoundSequenceUnitClass
                            {
                                Type = "Normal",
                                Owner = _Basic_Object_Script,
                                RoundUnit = new List<RoundElementClass> { QuickSave_RoundUnit_Class }
                            };
                        QuickSave_RoundUnit_Class.DelayTime = QuickSave_DelayStandby_Int;
                        _Basic_Object_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
                        _Map_BattleRound.RoundSequenceSet(
                            _Basic_Object_Script._Round_GroupUnit_Class, null);
                    }
                    BuildBack(Standby: !_Map_Manager._State_Reacting_Bool);
                    */
                    
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�^�X������ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void BuildBack(int FieldTime = 0, bool Standby = false)
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Item_ConceptUnit QuickSave_Concept_Script = _Object_Inventory_Script._Item_EquipConcepts_Script;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        switch (_World_Manager._Authority_Scene_String)
        {
            case "Field":
                if (_Object_Manager.ObjectRoundSet())
                {
                    _Map_Manager.FieldStateSet("BuildBack", "�����^�X�A�d�ݦ��L�ƥ�");
                    _Map_Manager.FieldStateSet("EventFront", "�^�X����ظm�����A�}�l�ƥ�");
                    _Round_Standby_Bool = Standby; 
                    _World_Manager._UI_Manager._UI_EventManager.
                        EventFinding(_Creature_FieldObjectt_Script._Map_Coordinate_Class, FieldTime, false);
                }
                break;
            case "Battle":
                {
                    _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
                    _Map_Manager.BattleStateSet("BuildBack", "�����^�X�A�i�J����ظm");
                    //��������
                    _Map_BattleRound.RoundSequenceSet(
                        null, _Map_BattleRound._Round_GroupUnit_Class);
                    //GroupUnit����Preview
                    _Basic_Object_Script._Round_GroupUnit_Class.Type = "Normal";

                    if (Standby && !_Map_Manager._State_Reacting_Bool)
                    {
                        _Round_State_String = "DelayStandby";
                        _Basic_Object_Script.SituationCaller(
                            "DelayStandby",null, 
                            QuickSave_Concept_Script._Basic_Object_Script._Basic_Source_Class, null,_Card_UsingObject_Script,
                            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                        _Round_Standby_Bool = Standby;
                    }
                    _Map_Manager.BattleStateSet("RoundTimes", "����ظm����");
                    StartCoroutine(_Map_BattleRound.RoundCall());
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Round

    #region Math
    #region - GetTagObject -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_Map_BattleObjectUnit> TagObject(string Type, string TagInput,
        SourceClass UserTarget, SourceClass TargetSource)
    {
        //----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> Answer_Return_ScriptsList = new List<_Map_BattleObjectUnit>();
        List<string> QS_TagSplit_StringList = new List<string>(TagInput.Split(","[0]));
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Driving":
                {
                    foreach (_Map_BattleObjectUnit DrivingObject in _Object_Inventory_Script._Item_DrivingObject_ScriptsList)
                    {
                        if (_Skill_Manager.TagContains(DrivingObject.Key_Tag(UserTarget, TargetSource), QS_TagSplit_StringList, true))
                        {
                            Answer_Return_ScriptsList.Add(DrivingObject);
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion Math

    #region React
    #region - ReactCall -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<string> ReactCall(SourceClass UserSource, 
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        List<string> QuickSave_BehaviorTag_StringList =
            UserSource.Source_Card._Card_BehaviorUnit_Script.Key_OwnTag(UserSource, null, _Card_UsingObject_Script);
        int QuickSave_ReactCount_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //�]�w----------------------------------------------------------------------------------------------------
        //�^��
        _World_Manager._Map_Manager._State_Reacting_Bool = true;
        //�ݭק�-�H�����iĲ�Ϊ��F��i�ϥΪ��ۦ���
        for (int a = 0; a < _Card_CardsBoard_ScriptList.Count; a++)
        {
            _UI_Card_Unit QuickSave_Cards_Script = _Card_CardsBoard_ScriptList[a];
            if (QuickSave_Cards_Script.UseLicense("Behavior", _Card_UsingObject_Script,
                HateTarget, Action, Time, Order,
                ReactTag: QuickSave_BehaviorTag_StringList))
            {
                QuickSave_ReactCount_Int++;
            }
        }
        _World_Manager._Map_Manager._State_Reacting_Bool = false;
        if (QuickSave_ReactCount_Int > 0)
        {
            Answer_Return_StringList.Add("React" + QuickSave_ReactCount_Int.ToString());
        }
        //----------------------------------------------------------------------------------------------------

        //�^��----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion
    #endregion React
}