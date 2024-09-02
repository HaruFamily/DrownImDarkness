using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class _Map_BattleRound : MonoBehaviour
{

    #region Round
    //�ܼƶ��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #region Variable
    //������----------------------------------------------------------------------------------------------------
    //�ͪ���
    public List<RoundElementClass> _Round_RoundCreatures_ClassList = new List<RoundElementClass>();
    
    //��J����(�笰�Ʃw����)//�L��Time�ӬOType
    public List<RoundSequenceUnitClass> _Round_RoundSequencePriority_ClassList = new List<RoundSequenceUnitClass>();
    //��{����
    public List<RoundSequenceUnitClass> _Round_RoundSequence_ClassList = new List<RoundSequenceUnitClass>();
    //----------------------------------------------------------------------------------------------------

    //�ɶ���----------------------------------------------------------------------------------------------------
    //��e�ɶ����g�L-�Ω�p��^�X
    static public int _Round_Time_Int;
    static public int _Round_Order_Int;
    //�ɶ����
    public float _Round_TimeUnit_Float;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    //Round
    public RoundSequenceUnitClass _Round_GroupUnit_Class;//������e�̳��ݪ�RoundUnit
    //Sequence
    public List<_Effect_EffectObjectUnit> _Round_SequenceEffectObject_ScriptsList = 
        new List<_Effect_EffectObjectUnit>();
    public List<_Effect_EffectCardUnit> _Round_SequenceEffectCard_ScriptsList =
        new List<_Effect_EffectCardUnit>();
    //Skill
    public List<TimesLimitClass> _Round_TimesLimits_ClassList = 
        new List<TimesLimitClass>();
    //----------------------------------------------------------------------------------------------------
    #endregion Variable


    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SequenceSort()
    {
        //----------------------------------------------------------------------------------------------------
        Dictionary<int, RoundSequenceUnitClass> QuickSave_NewRoundSequence_Dictionary = 
            new Dictionary<int, RoundSequenceUnitClass>();
        //----------------------------------------------------------------------------------------------------

        //�إ�NewRoundSequence----------------------------------------------------------------------------------------------------
        foreach (RoundSequenceUnitClass RoundUnit in _Round_RoundSequencePriority_ClassList)
        {
            bool QuickSave_Preview_Bool = (RoundUnit.Type == "Preview") ? true : false;
            _Map_BattleObjectUnit QuickSave_Object_Script = RoundUnit.Owner;
            _Object_CreatureUnit QuickSave_Creature_Script = QuickSave_Object_Script._Basic_Source_Class.Source_Creature;
            int QuickSave_DelayOffset_Int = 0;
            foreach (RoundElementClass RoundUnitElement in RoundUnit.RoundUnit)
            {
                //�����ͪ��]�w()
                int QuickSave_DelayTimely_Int = 0;
                switch (RoundUnitElement.Source.SourceType)
                {
                    case "Concept":
                        QuickSave_DelayTimely_Int =
                            QuickSave_Object_Script.Key_DelayTimely(QuickSave_Creature_Script._Round_State_String, RoundUnitElement.Source);
                        break;
                    case "Card":
                        QuickSave_DelayTimely_Int =
                            QuickSave_Object_Script.Key_DelayTimely("DelayBefore", RoundUnitElement.Source);
                        break;
                }
                int QuickSave_Time_Int = 
                    RoundUnitElement.AccumulatedTime + 
                    RoundUnitElement.DelayTime +
                    QuickSave_DelayTimely_Int;
                if (!QuickSave_Preview_Bool)
                {
                    QuickSave_Time_Int += RoundUnitElement.DelayOffset;
                }
                //����C���e�ɶ�
                QuickSave_Time_Int = (QuickSave_Time_Int < _Round_Time_Int) ? _Round_Time_Int : QuickSave_Time_Int;
                //�P�_
                foreach (RoundSequenceUnitClass Sequence in _Round_RoundSequence_ClassList)
                {
                    if (Sequence.Time > QuickSave_Time_Int + QuickSave_DelayOffset_Int)
                    {
                        break;
                    }
                    if (Sequence.Time == QuickSave_Time_Int + QuickSave_DelayOffset_Int)
                    {
                        if (Sequence.Owner._Basic_Source_Class.Source_Creature != QuickSave_Creature_Script)
                        {
                            QuickSave_DelayOffset_Int++;
                        }
                    }
                }
                //�ƭȳ]�m
                if (QuickSave_Preview_Bool)
                {
                    RoundUnitElement.DelayOffset = QuickSave_DelayOffset_Int;
                }
                else
                {
                    RoundUnitElement.DelayOffset += QuickSave_DelayOffset_Int;
                }
                QuickSave_Time_Int += QuickSave_DelayOffset_Int;
                RoundUnitElement.TargetTime = QuickSave_Time_Int;//��e�ɶ���m
                //�s�W���`��
                if (QuickSave_NewRoundSequence_Dictionary.TryGetValue(QuickSave_Time_Int, out RoundSequenceUnitClass NewRoundUnit))
                {
                    NewRoundUnit.RoundUnit.Add(RoundUnitElement);
                }
                else
                {
                    QuickSave_NewRoundSequence_Dictionary.Add(QuickSave_Time_Int,
                        new RoundSequenceUnitClass
                        {
                            Time = QuickSave_Time_Int,
                            Owner = QuickSave_Object_Script,
                            RoundUnit = new List<RoundElementClass> { RoundUnitElement }
                        });
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //��Ƴ]�w----------------------------------------------------------------------------------------------------
        List<RoundSequenceUnitClass> QuickSave_NewRoundSequence_ClassList = new List<RoundSequenceUnitClass>();
        foreach (RoundSequenceUnitClass RoundUnit in QuickSave_NewRoundSequence_Dictionary.Values)
        {
            QuickSave_NewRoundSequence_ClassList.Add(RoundUnit);
        }
        QuickSave_NewRoundSequence_ClassList.Sort((x, y) => x.Time.CompareTo(y.Time));
        _Round_RoundSequence_ClassList = new List<RoundSequenceUnitClass>(QuickSave_NewRoundSequence_ClassList);
        SequenceView();
        //----------------------------------------------------------------------------------------------------
    }
    public List<RoundElementClass> RoundUnit(int Time)
    {
        //----------------------------------------------------------------------------------------------------
        foreach (RoundSequenceUnitClass Sequence in _Round_RoundSequence_ClassList)
        {
            if (Sequence.Time == Time)
            {
                return Sequence.RoundUnit;
            }
        }
        return new List<RoundElementClass>();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //��ı�]�w�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SequenceView()
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _View_Battle _View_Battle = _World_Manager._UI_Manager._View_Battle;
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        List<RoundElementClass> QuickSave_Sequence_ClassList = new List<RoundElementClass>();
        //�ɶ�
        int QuickSave_Time_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //�إߩҦ��欰�M��----------------------------------------------------------------------------------------------------
        foreach (RoundSequenceUnitClass RoundSequence in _Round_RoundSequence_ClassList)
        {
            QuickSave_Sequence_ClassList.AddRange(RoundSequence.RoundUnit);
        }
        //����ɥR(�p�]�i����ܤ���)
        if (QuickSave_Sequence_ClassList.Count > _View_Battle._View_Sequences_ScriptsList.Count)
        {
            for (int a = _View_Battle._View_Sequences_ScriptsList.Count; a < QuickSave_Sequence_ClassList.Count; a++)
            {
                _Map_BattleSequenceUnit QuickSave_SequenceUnit_Script =
                    Instantiate(_View_Battle._View_SequenceUnit_GameObject, _View_Battle._View_SequenceStore_Transform).
                    GetComponent<_Map_BattleSequenceUnit>();
                _View_Battle._View_Sequences_ScriptsList.Add(QuickSave_SequenceUnit_Script);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�M��إ߻P��z----------------------------------------------------------------------------------------------------
        //��z
        int QuickSave_TimeSave_Int = 0;
        int QuickSave_OrderCount_Int = 0;
        for (int a = 0; a < _View_Battle._View_Sequences_ScriptsList.Count; a++)
        {
            //���|
            _Map_BattleSequenceUnit QuickSave_SequenceView_Script = _View_Battle._View_Sequences_ScriptsList[a];
            if (a < QuickSave_Sequence_ClassList.Count)
            {
                //����ܦ�
                RoundElementClass QuickSave_Sequence_Class = QuickSave_Sequence_ClassList[a];
                //�ɶ��]�w(Time && Order)
                QuickSave_Time_Int = QuickSave_Sequence_Class.TargetTime;
                if (QuickSave_TimeSave_Int == QuickSave_Time_Int)
                {
                    QuickSave_OrderCount_Int++;
                }
                else
                {
                    QuickSave_TimeSave_Int = QuickSave_Time_Int;
                    QuickSave_OrderCount_Int = 0;
                }
                //��ı�]�w
                switch (QuickSave_Sequence_Class.Source.SourceType)
                {
                    case "Concept":
                        {
                            //����-�ާ@�^�X
                            _Object_CreatureUnit QuickSave_Creature_Script =
                                QuickSave_Sequence_Class.Source.Source_Creature;
                            //��ı�]�w
                            QuickSave_SequenceView_Script._View_Name_Text.text =
                                QuickSave_Creature_Script._Basic_Language_Class.Name;
                            QuickSave_SequenceView_Script._View_Image_Image.sprite =
                                _View_Manager.GetSprite("Creature", "Sequence", QuickSave_Creature_Script._Basic_Key_String);
                            QuickSave_SequenceView_Script._View_Time_Text.text =
                                (QuickSave_Time_Int - _Round_Time_Int).ToString();
                            QuickSave_SequenceView_Script._View_Time_Text.fontSize =
                                Mathf.RoundToInt(180 - (30 * _World_Manager._World_GeneralManager.DecimalCount(QuickSave_Time_Int)));
                            //��Ƴ]�w
                            QuickSave_SequenceView_Script.ViewSet(
                                null, QuickSave_Creature_Script, 
                                QuickSave_Time_Int, QuickSave_OrderCount_Int);
                        }
                        break;
                    case "Card":
                        {
                            //�d��-�ĪG�^�X
                            //���|/�ܼ�
                            _UI_Card_Unit QuickSave_Card_Script =
                                QuickSave_Sequence_ClassList[a].Source.Source_Card;
                            _Skill_FactionUnit QuickSave_Faction_Script =
                                QuickSave_Card_Script._Card_OwnerFaction_Script;
                            //��ı�]�w
                            QuickSave_SequenceView_Script._View_Name_Text.text =
                                QuickSave_Card_Script._Card_BehaviorUnit_Script._Basic_Language_Class.Name;
                            QuickSave_SequenceView_Script._View_Image_Image.sprite =
                                _View_Manager.GetSprite("Behavior", "Sequence", QuickSave_Card_Script._Card_BehaviorUnit_Script._Basic_Key_String);
                            switch (QuickSave_Faction_Script._Basic_Source_Class.SourceType)
                            {
                                case "Concept":
                                    break;
                                case "Weapon":
                                    break;
                                case "Item":
                                    break;
                                case "Object":
                                    break;
                            }
                            QuickSave_SequenceView_Script._View_Time_Text.text = (QuickSave_Time_Int - _Round_Time_Int).ToString();
                            QuickSave_SequenceView_Script._View_Time_Text.fontSize = Mathf.RoundToInt(180 - (30 * _World_Manager._World_GeneralManager.DecimalCount(QuickSave_Time_Int)));
                            //�ܼƳ]�w
                            QuickSave_SequenceView_Script.ViewSet(
                                QuickSave_Card_Script, QuickSave_Card_Script._Basic_Source_Class.Source_Creature,
                                QuickSave_Time_Int, QuickSave_OrderCount_Int);
                        }
                        break;
                }
                //�W���C��]�w
                List<RoundElementClass> QuickSave_CardsElement_ScriptsList =
                    QuickSave_SequenceView_Script._Battle_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList;
                if (QuickSave_CardsElement_ScriptsList.Count > 0)
                {
                    if (QuickSave_CardsElement_ScriptsList[0] == (QuickSave_Sequence_ClassList[a]))
                    {
                        QuickSave_SequenceView_Script._View_Image_Image.color = Color.white;
                    }
                    else
                    {
                        QuickSave_SequenceView_Script._View_Image_Image.color = Color.gray;
                    }
                }
                else
                {
                    QuickSave_SequenceView_Script._View_Image_Image.color = Color.white;
                }
                //��ܳ]�w
                if (a != 0)
                {
                    QuickSave_SequenceView_Script.gameObject.SetActive(true);
                }
            }
            else
            {
                //���ݭn��ܫh����
                QuickSave_SequenceView_Script.gameObject.SetActive(false);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�W�[�ͪ����X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public RoundElementClass RoundCreatureAdd(_Object_CreatureUnit Owner)
    {
        //�ͪ���Ʈw�]�m----------------------------------------------------------------------------------------------------
        _Map_BattleObjectUnit QuickSave_Object_Script = Owner._Basic_Object_Script;
        RoundElementClass Answer_RoundUnit_Class = new RoundElementClass
        {
            Source = QuickSave_Object_Script._Basic_Source_Class,
            DelayType = "Creature",
            DelayTime = 0,
            AccumulatedTime = _Round_Time_Int,
            TargetTime = 0
        };
        //�[�J�ܥͪ��C(�ΥH�]�m�ݩR����)
        _Round_RoundCreatures_ClassList.Add(Answer_RoundUnit_Class);
        QuickSave_Object_Script._Round_Unit_Class = Answer_RoundUnit_Class;
        //�ݩR����]�m
        CreatureTotalDelaySet();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_RoundUnit_Class;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�t���`�M�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    [HideInInspector]public float _Round_HighestSpeed_Float;
    [HideInInspector] public float _Round_LowestSpeed_Float;
    readonly public float _Round_DelayMend_Float = 2.5f;
    public void CreatureTotalDelaySet()
    {
        //----------------------------------------------------------------------------------------------------
        float QuickSave_LowestSpeed_Float = 65535;
        float QuickSave_HighestSpeed_Float = 0;

        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < _Round_RoundCreatures_ClassList.Count; a++)
        {
            _Item_ConceptUnit QuickSave_Concept_Script =
                _Round_RoundCreatures_ClassList[a].Source.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
            float QuickSave_Speed_Float =
                QuickSave_Concept_Script._Basic_Object_Script.
                Key_Status("Speed", QuickSave_Concept_Script._Basic_Object_Script._Basic_Source_Class, null, null);
            if (QuickSave_Speed_Float > QuickSave_HighestSpeed_Float)
            {
                QuickSave_HighestSpeed_Float = QuickSave_Speed_Float;
            }
            if (QuickSave_Speed_Float < QuickSave_LowestSpeed_Float)
            {
                QuickSave_LowestSpeed_Float = QuickSave_Speed_Float;
            }
        }
        _Round_HighestSpeed_Float = QuickSave_HighestSpeed_Float;
        _Round_LowestSpeed_Float = QuickSave_LowestSpeed_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�H���إߧ���Delay�]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void CreatureDelaySet()
    {
        //----------------------------------------------------------------------------------------------------
        foreach (RoundElementClass Round in _Round_RoundCreatures_ClassList)
        {
            _Object_CreatureUnit QuickSave_Creature_Script = 
                Round.Source.Source_Creature;
            _Map_BattleObjectUnit QuickSave_Object_Script =
                QuickSave_Creature_Script._Basic_Object_Script;
            //�إ�Group
            RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                new RoundSequenceUnitClass
                {
                    Type = "Normal",
                    Owner = QuickSave_Creature_Script._Basic_Object_Script,
                    RoundUnit = new List<RoundElementClass> { QuickSave_Creature_Script._Basic_Object_Script._Round_Unit_Class }
                };
            QuickSave_Object_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
            //�Ӷ��]�m
            int QuickSave_DelayStandby_Int =
                QuickSave_Creature_Script._Object_Inventory_Script.
                _Item_EquipConcepts_Script.Key_DelayStandby(ContainTimeOffset: false);
            QuickSave_Object_Script._Round_Unit_Class.DelayTime = QuickSave_DelayStandby_Int;
            RoundSequenceSet(
                QuickSave_Object_Script._Round_GroupUnit_Class, null);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�I�s��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public IEnumerator RoundCall(_UI_Card_Unit FinishRemove = null)
    {
        //�]�w���P�_----------------------------------------------------------------------------------------------------
        //���`�P�w
        if (!_World_Manager._Object_Manager.ObjectRoundSet())
        {
            yield break;
        }
        //�Ƨ�
        SequenceSort();
        //���o�ְ̧ʧ@�� �� �Ȧs
        RoundSequenceUnitClass QuickSave_RoundSequenceUnitUnit_Class = _Round_RoundSequence_ClassList[0];
        RoundElementClass QuickSave_DuplicateRoundElement_Class = QuickSave_RoundSequenceUnitUnit_Class.RoundUnit[0];
        RoundElementClass QuickSave_RoundElement_Class = new RoundElementClass
        {
            Source = QuickSave_DuplicateRoundElement_Class.Source,
            DelayType = QuickSave_DuplicateRoundElement_Class.DelayType + "_Now",
            DelayTime = QuickSave_DuplicateRoundElement_Class.DelayTime,
            AccumulatedTime = QuickSave_DuplicateRoundElement_Class.AccumulatedTime,
            TargetTime = QuickSave_DuplicateRoundElement_Class.TargetTime
        };
        _Round_GroupUnit_Class = new RoundSequenceUnitClass
        {
            Type = "Normal",
            Owner = QuickSave_RoundSequenceUnitUnit_Class.Owner,
            RoundUnit = new List<RoundElementClass> { QuickSave_RoundElement_Class }
        };
        //�ӷ��貾��
        switch (QuickSave_RoundSequenceUnitUnit_Class.Owner._Basic_Source_Class.SourceType)
        {
            case "Concep":
                {
                    QuickSave_RoundSequenceUnitUnit_Class.Owner._Round_UnitCards_ClassList.
                        Remove(QuickSave_DuplicateRoundElement_Class);
                }
                break;
            case "Card":/*���K���� �����W��*/
                {
                    _Object_CreatureUnit QuickSave_Creature_Script =
                        QuickSave_RoundSequenceUnitUnit_Class.Owner._Basic_Source_Class.Source_Creature;
                    int QuickSave_Index_Int =//�]���R����N�L�k��(Remove)�R��
                        QuickSave_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList.
                        IndexOf(QuickSave_DuplicateRoundElement_Class);
                    QuickSave_RoundSequenceUnitUnit_Class.Owner._Round_UnitCards_ClassList.
                        Remove(QuickSave_DuplicateRoundElement_Class);
                    QuickSave_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList.
                        RemoveAt(QuickSave_Index_Int);
                }
                break;
        }
        //�ɶ��ˬd
        if (_Round_Time_Int == QuickSave_RoundSequenceUnitUnit_Class.Time)
        {
            _Round_Order_Int++;
        }
        else
        {
            //�]�m�ɶ�
            int QuickSave_TimePinch_Int = 
                QuickSave_RoundSequenceUnitUnit_Class.Time - _Round_Time_Int;
            List<_Effect_EffectObjectUnit> QuickSave_EffectObject_ScriptsList =
                new List<_Effect_EffectObjectUnit>(_Round_SequenceEffectObject_ScriptsList);
            foreach (_Effect_EffectObjectUnit Effect in QuickSave_EffectObject_ScriptsList)
            {
                Effect.RoundDecrease(QuickSave_TimePinch_Int);
            }
            List<_Effect_EffectCardUnit> QuickSave_EffectCard_ScriptsList =
                new List<_Effect_EffectCardUnit>(_Round_SequenceEffectCard_ScriptsList);
            foreach (_Effect_EffectCardUnit Effect in _Round_SequenceEffectCard_ScriptsList)
            {
                Effect.RoundDecrease(QuickSave_TimePinch_Int);
            }
            //print("Time:" + _Round_Time_Int + "->" + QuickSave_RoundSequenceUnitUnit_Class.Time);
            _Round_Time_Int = QuickSave_RoundSequenceUnitUnit_Class.Time;
            _Round_Order_Int = 0;
        }
        //��e�ӷ��]�m
        QuickSave_DuplicateRoundElement_Class.DelayTime = 0;//��e����
        QuickSave_DuplicateRoundElement_Class.DelayOffset = 0;//��������
        QuickSave_DuplicateRoundElement_Class.AccumulatedTime = _Round_Time_Int;//�֭p�ɶ�
        //��ı�ĪG-����Ĳ�o
        _World_Manager._UI_Manager.Effect("React", false);
        _World_Manager._Map_Manager._State_Reacting_Bool = false;
        //RoundUnit���/�]�w��ı
        RoundSequenceUnitClass QuickSave_targetSequence_Class = null;
        foreach (RoundSequenceUnitClass Rounds in _Round_RoundSequencePriority_ClassList)
        {
            foreach (RoundElementClass RoundUnit in Rounds.RoundUnit)
            {
                if (RoundUnit == QuickSave_DuplicateRoundElement_Class)
                {
                    QuickSave_targetSequence_Class = Rounds;
                    goto ForeachEnd;
                }
            }
        }
        ForeachEnd:
        if (QuickSave_targetSequence_Class != null)
        {
            QuickSave_targetSequence_Class.RoundUnit.Remove(QuickSave_DuplicateRoundElement_Class);
            if (QuickSave_targetSequence_Class.RoundUnit.Count == 0)
            {
                _Round_RoundSequencePriority_ClassList.Remove(QuickSave_targetSequence_Class);
            }
        }
        else
        {
            print("Wrong");
        }
        RoundSequenceSet(
            _Round_GroupUnit_Class, null);

        //�d���I�s
        if (FinishRemove != null)
        {
            //�����d�P
            FinishRemove.UseCardEnd();
        }
        //����RoundSequence
        switch (QuickSave_RoundElement_Class.Source.SourceType)
        {
            case "Concept":
                //���ͪ�
                {
                    //��ı
                    _Object_CreatureUnit QuickSave_Creature_Script =
                        QuickSave_RoundElement_Class.Source.Source_Creature;
                    _World_Manager._UI_Manager.
                        ChangeTraceTarget(QuickSave_Creature_Script.transform);
                    yield return new WaitForSeconds(1f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                    QuickSave_Creature_Script._Round_State_String = "Operate";
                    QuickSave_Creature_Script._Basic_Object_Script.
                        SituationCaller("Operate", new List<string> { QuickSave_Creature_Script._Round_Standby_Bool.ToString() },
                        QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null, null,
                        null, true, _Round_Time_Int, _Round_Order_Int);
                    QuickSave_Creature_Script.BuildFront();
                    //print("Concept:" + QuickSave_Creature_Script._Basic_Key_String);
                }
                break;
            case "Card":
                //���欰
                {
                    //��ı
                    _UI_Card_Unit QuickSave_Card_Script = QuickSave_RoundElement_Class.Source.Source_Card;
                    _Object_CreatureUnit QuickSave_Creature_Script = QuickSave_Card_Script._Basic_Source_Class.Source_Creature;
                    _World_Manager._UI_Manager.
                        ChangeTraceTarget(QuickSave_Creature_Script.transform);
                    yield return new WaitForSeconds(1f / (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                    //Skill�ĪG
                    QuickSave_Creature_Script._Round_State_String = "Skill";
                    QuickSave_Creature_Script._Basic_Object_Script.
                        SituationCaller("Skill", null,
                        QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, null, null,
                        null, true, _Round_Time_Int, _Round_Order_Int);
                    QuickSave_Card_Script.UseCardEffect();
                    //print("Card:" + QuickSave_Card_Script._Basic_Key_String);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�ɶ��b�[�J/�����]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void RoundSequenceSet(RoundSequenceUnitClass AddRoundUnits, RoundSequenceUnitClass RemoveRoundUnits)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //##�O�ѰODelayTime�n�]�m!
        if (AddRoundUnits != null)
        {
            _Round_RoundSequencePriority_ClassList.Add(AddRoundUnits);
        }
        if (RemoveRoundUnits != null)
        {
            _Round_RoundSequencePriority_ClassList.Remove(RemoveRoundUnits);
        }
        SequenceSort();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    //�ɶ��b�[�J/�����]�m�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void RoundSequenceSet(List<RoundSequenceUnitClass> AddRoundUnits, List<RoundSequenceUnitClass> RemoveRoundUnits)        
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        //##�O�ѰODelayTime�n�]�m!
        if (AddRoundUnits != null)
        {
            _Round_RoundSequencePriority_ClassList.AddRange(AddRoundUnits);
        }
        if (RemoveRoundUnits != null)
        {
            foreach (RoundSequenceUnitClass RoundUnit in RemoveRoundUnits)
            {
                _Round_RoundSequencePriority_ClassList.Remove(RoundUnit);
            }
        }
        SequenceSort();
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Round
}
