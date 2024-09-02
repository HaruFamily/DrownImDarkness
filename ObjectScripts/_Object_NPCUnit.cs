using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class _Object_NPCUnit : MonoBehaviour
{
    #region Element
    //�ܼƶ��X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //�����h���A
    [HideInInspector] public _Object_CreatureUnit _Basic_Owner_Script;
    [HideInInspector] public _Map_BattleObjectUnit _Map_Coordinate_Script;

    //AI
    //Dat
    [HideInInspector] public Dictionary<string, float> _Creature_AI_Class;
    //����ؼ�
    [HideInInspector] public _Map_BattleObjectUnit _AI_HateTarget_Script;
    //����ȲM��
    [HideInInspector] public Dictionary<_Map_BattleObjectUnit, float> _AI_HateList_Dictionary = new Dictionary<_Map_BattleObjectUnit, float>();
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element


    #region FirstBuild
    //�Ĥ@��ʡX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void Awake()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //���|�]�w----------------------------------------------------------------------------------------------------
        //�n���ܥؿ�
        //�y���I
        _Map_BattleObjectUnit QuickSave_CoordinateUnit_Script = this.transform.GetComponent<_Map_BattleObjectUnit>();
        //�ͪ���T
        _Basic_Owner_Script = this.transform.GetComponent<_Object_CreatureUnit>();
        _Basic_Owner_Script._NPC_Script = this;
        _Map_Coordinate_Script = QuickSave_CoordinateUnit_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion FirstBuild


    #region Start
    //�_�l�I�s�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart(string Key)
    {
        //���|----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //�X�ͳ]�m----------------------------------------------------------------------------------------------------
        //�]�w���|
        _Basic_Owner_Script._NPC_Script = this;

        //�򥻳]�w(�s���B���t�B��O��)
        _Object_Manager.CreatureStartSet(_Basic_Owner_Script, Key);
        //�]�wAI
        _Creature_AI_Class = new Dictionary<string, float>(_Object_Manager._Data_CreatureAI_Dictionary[Key]);
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FieldBattleStartSet()
    {
        //���|----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //�쳥�]�m----------------------------------------------------------------------------------------------------
        _Object_Manager.HoldToField(_Basic_Owner_Script);
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
        SourceClass QuickSave_Source_Class = QuickSave_ConceptObject_Script._Basic_Source_Class;
        _Basic_Owner_Script._Card_UsingObject_Script = QuickSave_ConceptObject_Script;
        _Basic_Owner_Script._Basic_Object_Script.SituationCaller(
            "FieldStart", null, 
            QuickSave_Source_Class, QuickSave_Source_Class, null,
            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //----------------------------------------------------------------------------------------------------

        //�԰��]�m----------------------------------------------------------------------------------------------------
        _Object_Manager.FieldToBattle(_Basic_Owner_Script);
        _Basic_Owner_Script._Basic_Object_Script._Round_Unit_Class =
            _World_Manager._Map_Manager._Map_BattleRound.RoundCreatureAdd(_Basic_Owner_Script);
        _Basic_Owner_Script._Round_Standby_Bool = true;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FieldBattleEndSet(Vector SpawnPoint)
    {
        //���|----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _World_Manager._Map_Manager._Map_MoveManager.
            Spawn(
            SpawnPoint,
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class,
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

        _Basic_Owner_Script._Basic_Object_Script.SituationCaller(
            "BattleStart", null, 
            QuickSave_Source_Class, QuickSave_Source_Class, null,
            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _AI_HateTarget_Script =
            _Object_Manager._Object_Player_Script._Basic_Object_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start


    #region NPC_AI
    //�ܼơX�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //AI�j�M����
    public class AIFindSave
    {
        public _Map_BattleObjectUnit BehaviorUsingObject;
        //�D�欰�d
        public _UI_Card_Unit Behavior;
        //���]�d
        public List<_Map_BattleObjectUnit> EnchancesUsingObjects;
        public List<_UI_Card_Unit> Enchances;

        //���ǽs��
        public string Key;
        //��ʮɶ�
        public int StartTime;
        //��ʦ�m
        public Vector StartCoordinate;

        public PathSelectPairClass PathSelect;

        //����
        public float Score;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�ĤH�欰�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public IEnumerator NPC_AI_Action()
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        List<_Map_BattleObjectUnit> QuickSave_Using_ScriptsList = 
            _Basic_Owner_Script._Object_Inventory_Script._Item_ReachObject_ScriptsList;
        List<_UI_Card_Unit> QuickSave_Cards_ScriptList =
            _Basic_Owner_Script._Card_CardsBoard_ScriptList;
        _Item_ConceptUnit QuickSave_Concept_Script =
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script;

        /*
        //�d���^��
        print("Start--------------------------------------------");
        foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptList)
        {
            print(Card._Basic_Key_String);
        }
        */
        //----------------------------------------------------------------------------------------------------

        //�ؼг]�w----------------------------------------------------------------------------------------------------
        float QuickSave_Hatest_Float = 0;
        foreach(_Map_BattleObjectUnit Key in _AI_HateList_Dictionary.Keys)
        {
            if (_AI_HateList_Dictionary[Key] > QuickSave_Hatest_Float)
            {
                QuickSave_Hatest_Float = _AI_HateList_Dictionary[Key];
                _AI_HateTarget_Script = Key;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //�Ƨǩۦ�----------------------------------------------------------------------------------------------------
        #region - Permutations -
        List<List<KeyValuePair<_Map_BattleObjectUnit,string>>> QuickSave_SampPool_PairList = 
            new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>();
        //�򥻲M��
        foreach (_Map_BattleObjectUnit Object in QuickSave_Using_ScriptsList)
        {
            for (int a = 0; a < QuickSave_Cards_ScriptList.Count; a++)
            {
                _UI_Card_Unit QuickSave_BehaviorCard_Script = QuickSave_Cards_ScriptList[a];
                //�ϥγ\�i
                int QuickSave_DelayBefore_Int =
                    QuickSave_BehaviorCard_Script._Card_BehaviorUnit_Script.
                    Key_DelayBefore(null, Object, ContainEnchance: true, ContainTimeOffset: false);
                int QuickSave_DelayBeforeTime_Int =
                    _Map_BattleRound._Round_Time_Int + QuickSave_DelayBefore_Int;
                if (!QuickSave_BehaviorCard_Script.UseLicense(
                    "Behavior", Object, _AI_HateTarget_Script, false, QuickSave_DelayBeforeTime_Int, 65535, 
                    ReactTag: _Map_Manager._State_ReactTag_StringList))
                {
                    continue;
                }
                KeyValuePair<_Map_BattleObjectUnit, string> QuickSave_Behavior_Pair =
                    new KeyValuePair<_Map_BattleObjectUnit, string>(Object, a.ToString());
                {
                    List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_Behavior_PairList =
                        new List<KeyValuePair<_Map_BattleObjectUnit, string>>();
                    QuickSave_Behavior_PairList.Add(QuickSave_Behavior_Pair);
                    QuickSave_SampPool_PairList.Add(QuickSave_Behavior_PairList);
                }
                //���]
                List<_UI_Card_Unit> QuickSave_ChangeCards_ScriptList =
                    new List<_UI_Card_Unit>(_Basic_Owner_Script._Card_CardsBoard_ScriptList);
                QuickSave_ChangeCards_ScriptList.Remove(QuickSave_BehaviorCard_Script);
                List<List<KeyValuePair<_Map_BattleObjectUnit, string>>> QuickSave_EnchanceSavePairList_PairList =
                    new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>(
                        EnchancePermutations(QuickSave_BehaviorCard_Script,
                        QuickSave_ChangeCards_ScriptList, QuickSave_Using_ScriptsList));
                foreach (List<KeyValuePair<_Map_BattleObjectUnit, string>> EnchancePairList in
                    QuickSave_EnchanceSavePairList_PairList)
                {
                    List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_Enchance_PairList =
                        new List<KeyValuePair<_Map_BattleObjectUnit, string>>();
                    QuickSave_Enchance_PairList.Add(QuickSave_Behavior_Pair);
                    QuickSave_Enchance_PairList.AddRange(EnchancePairList);

                    QuickSave_SampPool_PairList.Add(QuickSave_Enchance_PairList);
                }
            }
        }
        #endregion
        /*
        �զX�^��
        print("SampStart-----------------------------------------------");
        foreach (List<KeyValuePair<_Map_BattleObjectUnit, string>> Samp in QuickSave_SampPool_PairList)
        {
            print("Samp:");
            foreach (KeyValuePair<_Map_BattleObjectUnit, string> SampUnit in Samp)
            {
                if (SampUnit.Value == "Standby")
                {
                    print(SampUnit.Value);
                }
                else
                {
                    print(QuickSave_Cards_ScriptList[int.Parse(SampUnit.Value)]._Basic_Key_String +":"+ 
                        SampUnit.Value + ":" + 
                        SampUnit.Key._Basic_Key_String);
                }
            }
        }*/
        //----------------------------------------------------------------------------------------------------

        //�ƶq���(�V�åͪ����)----------------------------------------------------------------------------------------------------
        List<List<KeyValuePair<_Map_BattleObjectUnit, string>>> QuickSave_AISampPool_PairList = 
            new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>();
        int QuickSave_SamplingSize_Int = Mathf.FloorToInt(_Creature_AI_Class["SamplingSize"]);//��˼�
        if (QuickSave_SampPool_PairList.Count >= QuickSave_SamplingSize_Int)
        {
            //Pool�j���˼�
            while (QuickSave_AISampPool_PairList.Count < QuickSave_SamplingSize_Int)
            {
                List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_Samp_PairList =
                    QuickSave_SampPool_PairList[Random.Range(0, QuickSave_SampPool_PairList.Count)];
                if (!QuickSave_AISampPool_PairList.Contains(QuickSave_Samp_PairList))
                {
                    QuickSave_AISampPool_PairList.Add(QuickSave_Samp_PairList);
                }
            }
            QuickSave_SampPool_PairList.Clear();
        }
        else
        {
            //Pool�p���˼�
            QuickSave_AISampPool_PairList = QuickSave_SampPool_PairList;
        }
        //�ݩR�s�W
        QuickSave_AISampPool_PairList.Add(
            new List<KeyValuePair<_Map_BattleObjectUnit, string>> {
                new KeyValuePair<_Map_BattleObjectUnit, string>(null, "Standby") });
        //----------------------------------------------------------------------------------------------------

        //�o���B��----------------------------------------------------------------------------------------------------
        #region - Score -
        //�o����
        List<AIFindSave> QuickSave_ScorePermutations_FloatList = new List<AIFindSave>();//�o���Ƨ�
        //�}�l�P�w/�����
        for (int a = 0; a < QuickSave_AISampPool_PairList.Count; a++)
        {
            //���Ƿj�M/�v�@����----------------------------------------------------------------------------------------------------
            //��e�s�W����/  ���P�Ƨǩ��/�ө�˶���            
            yield return StartCoroutine(NPC_AI_Find(QuickSave_AISampPool_PairList[a]));//���ݧ���
            yield return new WaitForSeconds(0.1f);//�����u��
            QuickSave_ScorePermutations_FloatList.AddRange(new List<AIFindSave>(QuickSave_AIFind_ClassList));

            //��˦^��
            /*
            print("Score:"+ a + " -----------------------------------------------");
            if (QuickSave_AIFind_ClassList[0].Key == "Standby")
            {
                print("Standby" + "\n" +
                    QuickSave_AIFind_ClassList[0].Score);
            }
            else
            {
                foreach (AIFindSave FindSave in QuickSave_AIFind_ClassList)
                {
                    print(FindSave.Behavior._Basic_Key_String + "\n" +
                        FindSave.PathSelect.Select[0].Vector.Vector3Int + "\n" +
                        FindSave.BehaviorUsingObject._Basic_Key_String + "\n" +
                        FindSave.Score);
                }
            }
            */
            //----------------------------------------------------------------------------------------------------
        }
        //�M�z�Ȧs
        QuickSave_SampPool_PairList = null;
        QuickSave_AISampPool_PairList = null;
        #endregion
        //----------------------------------------------------------------------------------------------------

        //�������----------------------------------------------------------------------------------------------------
        #region - Compare -
        //�̰�����
        AIFindSave QuickSave_BestAction_Class = null;
        //�̰���
        float QuickSave_BestScore_Float = -65535;

        //���
        //print("Compare:-----------------------------------------------");
        foreach (AIFindSave AIFind in QuickSave_ScorePermutations_FloatList)
        {
            //����^��
            /*
            if (AIFind.Behavior != null)
            {
                string QuickSave_Enchances_String = "";
                string QuickSave_EnchancesObject_String = "";
                for (int b = 0; b < AIFind.Enchances.Count; b++)
                {
                    QuickSave_Enchances_String += AIFind.Enchances[b]._Basic_Key_String + ",";
                    QuickSave_EnchancesObject_String += AIFind.EnchancesUsingObjects[b]._Basic_Key_String + ",";
                }
                print("Key�G" + AIFind.Key + "\n" +
                    "BehaviorUsing�G" + AIFind.BehaviorUsingObject._Basic_Key_String + "\n" +
                    "BehaviorKey�G" + AIFind.Behavior._Card_BehaviorUnit_Script._Basic_Key_String + "\n" +
                    "Enchance�G" + QuickSave_Enchances_String + "\n" +
                    "EnchanceObject�G" + QuickSave_EnchancesObject_String + "\n" +
                    "CoordinateHater�G" + _AI_HateTarget_Script.TimePosition(AIFind.StartTime, 65535).Vector3Int + "\n" +
                    "CoordinateStart�G" + AIFind.StartCoordinate.Vector3Int + "\n" +
                    "CoordinateTarget�G" + AIFind.PathSelect.Select[0].Vector.Vector3Int + "\n" +
                    "StartTime�G" + AIFind.StartTime + "/" +
                    _Map_BattleRound._Round_Time_Int + "\n" +
                    "Score�G" + AIFind.Score + "\n");
            }
            else
            {
                print("Key�G" + AIFind.Key + "\n" +
                    "StartTime�G" + AIFind.StartTime + "/" + 
                    _Map_BattleRound._Round_Time_Int + "\n" +
                    "Score�G" + AIFind.Score + "\n");
            }
            */
            if (AIFind.Score > QuickSave_BestScore_Float)
            {
                //�]�m
                QuickSave_BestAction_Class = AIFind;
                QuickSave_BestScore_Float = AIFind.Score;
            }
            else if(AIFind.Score == QuickSave_BestScore_Float)
            {
                if (Random.Range(0,2) == 1)//�ۦP��1/2����
                {
                    //�]�m
                    QuickSave_BestAction_Class = AIFind;
                    QuickSave_BestScore_Float = AIFind.Score;
                }
            }
        }
        //�M�z�Ȧs
        QuickSave_ScorePermutations_FloatList = null;
        QuickSave_AIFind_ClassList = null;
        #endregion
        //----------------------------------------------------------------------------------------------------

        //�ʧ@----------------------------------------------------------------------------------------------------
        #region - Action -
        _Map_BattleObjectUnit QuickSave_Object_Script = _Basic_Owner_Script._Basic_Object_Script;
        //�d��
        if (QuickSave_BestAction_Class == null)
        {
            print("Null Wrong");
        }
        if (QuickSave_BestAction_Class.Behavior != null)
        {
            //�ܼ�
            _UI_Card_Unit QuickSave_Card_Script = QuickSave_BestAction_Class.Behavior;
            _Map_BattleObjectUnit QuickSave_Using_Script = QuickSave_BestAction_Class.BehaviorUsingObject;
            string QuickSave_EnchanceType_String = 
                QuickSave_BestAction_Class.Behavior._Card_BehaviorUnit_Script.Key_Enchance();
            //�欰�]�m
            QuickSave_Card_Script._Range_UseData_Class = QuickSave_BestAction_Class.PathSelect;
            QuickSave_Card_Script._Card_UseObject_Script = QuickSave_BestAction_Class.BehaviorUsingObject;
            _Basic_Owner_Script._Card_UsingObject_Script = QuickSave_Using_Script;
            //���]�]�m
            for (int a = 0; a < QuickSave_BestAction_Class.Enchances.Count; a++)
            {
                QuickSave_BestAction_Class.Enchances[a].
                    EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_Card_Script, QuickSave_BestAction_Class.EnchancesUsingObjects[a]);
            }
            //�ɶ��b�]�m
            RoundElementClass QuickSave_CreatureRound_Class =
                _Basic_Owner_Script._Basic_Object_Script._Round_Unit_Class;
            RoundElementClass QuickSave_CardRound_Class =
                QuickSave_Card_Script._Round_Unit_Class;
            int QuickSave_DelayBefore_Int =
                QuickSave_Card_Script._Card_BehaviorUnit_Script.
                Key_DelayBefore(null, QuickSave_Using_Script, ContainEnchance: true, ContainTimeOffset: false);
            int QuickSave_DelayAfter_Int =
                QuickSave_DelayBefore_Int +
                QuickSave_Card_Script._Card_BehaviorUnit_Script.
                Key_DelayAfter(null, QuickSave_Using_Script, ContainEnchance: true, ContainTimeOffset: false);
            int QuickSave_Order_Int =
                _Map_BattleRound.RoundUnit(_Map_BattleRound._Round_Time_Int).Count;
            QuickSave_Card_Script._Round_DelayBefore_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayBefore_Int;
            QuickSave_Card_Script._Round_DelayAfter_Int = _Map_BattleRound._Round_Time_Int + QuickSave_DelayAfter_Int;

            //�^�X�]�m
            RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                new RoundSequenceUnitClass
                {
                    Type = "Preview",
                    Owner = QuickSave_Object_Script,
                    RoundUnit = new List<RoundElementClass> { QuickSave_CardRound_Class, QuickSave_CreatureRound_Class }
                };
            QuickSave_CardRound_Class.AccumulatedTime = _Map_BattleRound._Round_Time_Int;
            QuickSave_CardRound_Class.DelayTime = QuickSave_DelayBefore_Int;
            QuickSave_CreatureRound_Class.DelayTime = QuickSave_DelayAfter_Int;
            QuickSave_Card_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
            //�s�W�^�X
            if (!_Map_Manager._State_Reacting_Bool)
            {
                _Map_BattleRound.RoundSequenceSet(
                    QuickSave_RoundSequence_Class, null);
            }
            else
            {
                //Ĳ�o
                _UI_Card_Unit QuickSave_ReactCard_Script =
                    _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script;
                _Map_BattleRound.RoundSequenceSet(
                    QuickSave_RoundSequence_Class, QuickSave_ReactCard_Script._Round_GroupUnit_Class);
            }
            //�I�s�ʧ@
            QuickSave_Card_Script.UseCardStart("AI",FindOrder: QuickSave_BestAction_Class);
            _World_Manager._UI_Manager._UI_CardManager.BoardRefresh(_Basic_Owner_Script);
        }
        else
        {
            if (!_Map_Manager._State_Reacting_Bool)
            {
                //����ɶ�
                int QuickSave_DelayStandby_Int =
                    QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
                RoundElementClass QuickSave_RoundUnit_Class = QuickSave_Object_Script._Round_Unit_Class;
                RoundSequenceUnitClass QuickSave_RoundSequence_Class =
                    new RoundSequenceUnitClass
                    {
                        Type = "Preview",
                        Owner = QuickSave_Object_Script,
                        RoundUnit = new List<RoundElementClass> { QuickSave_RoundUnit_Class }
                    };
                QuickSave_RoundUnit_Class.DelayTime = QuickSave_DelayStandby_Int;
                QuickSave_Object_Script._Round_GroupUnit_Class = QuickSave_RoundSequence_Class;
                _Map_BattleRound.RoundSequenceSet(
                    QuickSave_Object_Script._Round_GroupUnit_Class, null);
            }
            _Basic_Owner_Script.BuildBack(Standby: !_Map_Manager._State_Reacting_Bool);
        }
        #endregion
        //----------------------------------------------------------------------------------------------------
    }
    //���]�üƿ��
    private List<List<KeyValuePair<_Map_BattleObjectUnit, string>>> EnchancePermutations(
        _UI_Card_Unit Behavior,
        List<_UI_Card_Unit> Cards, List<_Map_BattleObjectUnit> Objects)
    {
        //----------------------------------------------------------------------------------------------------
        List<List<KeyValuePair<_Map_BattleObjectUnit, string>>> Answer_Return_PairList = 
            new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>();
        string QuickSave_EnchanceType_String =
            Behavior._Card_BehaviorUnit_Script.Key_Enchance();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //�d�P�H���Ƨ�
        List<_UI_Card_Unit[]> QuickSave_EnchanceSamp_ScriptsList = new List<_UI_Card_Unit[]>();
        foreach (_UI_Card_Unit s in Cards)
        {
            List<_UI_Card_Unit[]> lst =
                new List<_UI_Card_Unit[]>(QuickSave_EnchanceSamp_ScriptsList);
            _UI_Card_Unit[] QuickSave_PooSamp_ScriptsList = { s };
            QuickSave_EnchanceSamp_ScriptsList.Add(QuickSave_PooSamp_ScriptsList);
            foreach (_UI_Card_Unit[] ss in lst)
            {
                _UI_Card_Unit[] QuickSave_ArrayCombine_ScriptsArray = 
                    new _UI_Card_Unit[ss.Length + QuickSave_PooSamp_ScriptsList.Length];
                ss.CopyTo(QuickSave_ArrayCombine_ScriptsArray, 0);
                QuickSave_PooSamp_ScriptsList.CopyTo(QuickSave_ArrayCombine_ScriptsArray, ss.Length);
                QuickSave_EnchanceSamp_ScriptsList.Add(QuickSave_ArrayCombine_ScriptsArray);
            }
        }
        //SampCard��WObject
        foreach (_UI_Card_Unit[] CardSamp in QuickSave_EnchanceSamp_ScriptsList)
        {
            List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>
                QuickSave_Samping_PairList =
            new List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>();
            foreach (_UI_Card_Unit Card in CardSamp)
            {
                //�L�����]�s
                List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>> QuickSave_SampingSearching_PairList = 
                    new List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>(QuickSave_Samping_PairList);
                QuickSave_Samping_PairList.Clear();
                foreach (List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> Samping in QuickSave_SampingSearching_PairList)
                {
                    //�i���]�Z���q
                    List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> QuickSave_CardUsing_PairList =
                        new List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>(Samping);
                    //�欰���]
                    foreach (KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit> EnchancePair in QuickSave_CardUsing_PairList)
                    {
                        EnchancePair.Value.EnchanceSet("Add", QuickSave_EnchanceType_String, Behavior, EnchancePair.Key);
                    }
                    //����T�{
                    foreach (_Map_BattleObjectUnit Object in Objects)
                    {
                        //�ӪZ���L�k�Ω���]
                        int QuickSave_DelayBefore_Int =
                            Behavior._Card_BehaviorUnit_Script.
                            Key_DelayBefore(null, Object, ContainEnchance: true, ContainTimeOffset: false);
                        int QuickSave_DelayBeforeTime_Int =
                            _Map_BattleRound._Round_Time_Int + QuickSave_DelayBefore_Int;
                        if (!Card.UseLicense(
                            "Enchance", Object, 
                            _AI_HateTarget_Script, false, QuickSave_DelayBeforeTime_Int, 65535, 
                            Behavior))
                        {
                            break;
                        }
                        //�s�W��Samping��
                        KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit> QuickSave_Pair_Pair =
                            new KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>(Object, Card);
                        List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> QuickSave_SampingInput_PairList =
                            new List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>(QuickSave_CardUsing_PairList);
                        QuickSave_SampingInput_PairList.Add(QuickSave_Pair_Pair);
                        QuickSave_Samping_PairList.Add(QuickSave_SampingInput_PairList);
                    }
                    //�Ҧ��Z���ҵL�k���](�Ӹ��u����)
                    if (QuickSave_CardUsing_PairList.Count < 0)
                    {
                        break;
                    }
                    //���]��l��
                    Behavior.EnchanceSet("Clear", QuickSave_EnchanceType_String, null, null);
                }
            }
            //����Objects�s�W
            foreach (List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> PairList in QuickSave_Samping_PairList)
            {
                List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_PairList_PaitList =
                    new List<KeyValuePair<_Map_BattleObjectUnit, string>>();
                foreach (KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit> PairUnit in PairList)
                {
                    KeyValuePair<_Map_BattleObjectUnit, string> QuickSave_PairUnit_Pair =
                        new KeyValuePair<_Map_BattleObjectUnit, string>(PairUnit.Key, PairUnit.Value._Card_Position_Int.ToString());
                    QuickSave_PairList_PaitList.Add(QuickSave_PairUnit_Pair);
                }
                Answer_Return_PairList.Add(QuickSave_PairList_PaitList);
            }            
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_PairList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�d���X�R(�d��/���|��ܩ���)�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    List<AIFindSave> QuickSave_AIFind_ClassList = new List<AIFindSave>();
    public IEnumerator NPC_AI_Find(List<KeyValuePair<_Map_BattleObjectUnit, string>> Key)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        QuickSave_AIFind_ClassList = new List<AIFindSave>();
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        _Item_ConceptUnit QuickSave_Concept_Script =
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script;
        SourceClass QuickSave_ConceptSource_Class =
            QuickSave_Concept_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //�j�M�å���----------------------------------------------------------------------------------------------------
        if (Key[0].Value != "Standby")
        {
            //�d�P�w
            List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = 
                _Basic_Owner_Script._Card_CardsBoard_ScriptList;
            //�欰�ܼƳ]�w
            _UI_Card_Unit QuickSave_MainCard_Script =
                QuickSave_Cards_ScriptsList[int.Parse(Key[0].Value)];
            _Map_BattleObjectUnit QuickSave_MainObject_Script = Key[0].Key;
            QuickSave_MainCard_Script._Card_UseObject_Script = QuickSave_MainObject_Script;
            string QuickSave_EnchanceType_String =
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
            //���]�ܼƳ]�w
            List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_CardObjects_PairList =
                new List<KeyValuePair<_Map_BattleObjectUnit, string>>(Key);
            QuickSave_CardObjects_PairList.RemoveAt(0);//��K���oEnchance
            List<_UI_Card_Unit> QuickSave_Enchances_ScriptsList = new List<_UI_Card_Unit>();
            List<_Map_BattleObjectUnit> QuickSave_EnchancesObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
            foreach (KeyValuePair<_Map_BattleObjectUnit, string> Pair in QuickSave_CardObjects_PairList)
            {
                _UI_Card_Unit QuickSave_Card_Script = QuickSave_Cards_ScriptsList[int.Parse(Pair.Value)];
                QuickSave_Enchances_ScriptsList.Add(QuickSave_Card_Script);
                QuickSave_EnchancesObjects_ScriptsList.Add(Pair.Key);
                QuickSave_Card_Script.EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_MainCard_Script, Pair.Key);//���]�@��
            }

            //�d���O���ܼ�
            int QuickSave_DelayBefore_Int =
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                Key_DelayBefore(null, QuickSave_MainObject_Script, ContainEnchance: true, ContainTimeOffset: false);
            int QuickSave_DelayBeforeTime_Int = 
                _Map_BattleRound._Round_Time_Int + QuickSave_DelayBefore_Int;

            int QuickSave_DelayAfter_Int =
                QuickSave_DelayBefore_Int +
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                Key_DelayAfter(null, QuickSave_MainObject_Script, ContainEnchance: true, ContainTimeOffset: false);
            int QuickSave_DelayAfterTime_Int =
                _Map_BattleRound._Round_Time_Int + QuickSave_DelayAfter_Int;
            int QuickSave_Order_Int =
                _Map_BattleRound.RoundUnit(_Map_BattleRound._Round_Time_Int).Count;
            //�_�l����                
            Vector QuickSave_StartCoordinate_Class =
                QuickSave_MainObject_Script.TimePosition(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);

            //��ܽd��
            Dictionary<string, List<Vector>> QuickSave_Range_Dictionary =
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                Key_Range(QuickSave_StartCoordinate_Class, QuickSave_MainObject_Script, 
                QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
            //�d��
            List<Vector> QuickSave_Target_ClassList = new List<Vector>();
            List<Vector> QuickSave_Range_ClassList = new List<Vector>();
            List<Vector> QuickSave_Path_ClassList = new List<Vector>();
            List<Vector> QuickSave_Select_ClassList = new List<Vector>();
            foreach (string DicKey in QuickSave_Range_Dictionary.Keys)
            {
                QuickSave_Target_ClassList.AddRange(QuickSave_MainCard_Script._Range_Select_Class.Vector());
                QuickSave_Range_ClassList.AddRange(QuickSave_Range_Dictionary[DicKey]);
                QuickSave_Path_ClassList.AddRange(
                    QuickSave_MainCard_Script._Range_Path_Class.AllVectors(
                        QuickSave_MainCard_Script._Range_Path_Class.PathUnits(DicKey)));
                QuickSave_Select_ClassList.AddRange(
                    QuickSave_MainCard_Script._Range_Select_Class.AllVectors(
                        QuickSave_MainCard_Script._Range_Select_Class.SelectUnits(DicKey)));
            }
            //���i��ܽd��
            List<PathSelectPairClass> QuickSave_PathSelect_ClassList = 
                _Map_Manager.PathSelectPair(
                    QuickSave_MainCard_Script._Range_Path_Class,
                    QuickSave_MainCard_Script._Range_Select_Class);
            foreach (PathSelectPairClass Pair in QuickSave_PathSelect_ClassList)
            {
                float QuickSave_Score_Float = 0;
                List<string> QuickSave_ScoreText_StringList = new List<string>();
                QuickSave_ScoreText_StringList.Add("DelayBefore�U" + QuickSave_DelayBefore_Int);
                QuickSave_ScoreText_StringList.Add("DelayAfter�U" + QuickSave_DelayAfter_Int);
                QuickSave_MainCard_Script._Range_UseData_Class = Pair;
                QuickSave_MainCard_Script._Card_UseCenter_Class = Pair.Select[0].Vector;
                //�ۨ�����------------
                Dictionary<string, PathPreviewClass> QuickSave_PathPreview_Dictionay =
                    QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                    Key_Anime(QuickSave_MainObject_Script,
                    _AI_HateTarget_Script, false, QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
                //�W�[�ɶ�
                foreach (PathPreviewClass PathPreviewValue in QuickSave_PathPreview_Dictionay.Values)
                {
                    PathPreviewValue.UseObject.
                        TimePositionAdd(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int, PathPreviewValue.FinalCoor);
                }
                //�欰
                QuickSave_ScoreText_StringList.AddRange(
                    QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                    Key_ActionCall(QuickSave_PathPreview_Dictionay, _AI_HateTarget_Script, false,
                    QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int));
                //��������(�O�d�d��)
                int QuickSave_RecerseCard_Int =
                    _Basic_Owner_Script._Card_CardsBoard_ScriptList.Count -
                    1 - QuickSave_Enchances_ScriptsList.Count;
                QuickSave_ScoreText_StringList.Add("Reverse�U" + QuickSave_RecerseCard_Int);
                //�����ഫ
                QuickSave_Score_Float +=
                    _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_ScoreText_StringList, 
                    QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int, _AI_HateTarget_Script);
                //�ɶ��b����------------
                //�ɶ��b�o��
                foreach (RoundSequenceUnitClass RoundSequence in _Map_BattleRound._Round_RoundSequence_ClassList)
                {
                    for (int a = 0; a < RoundSequence.RoundUnit.Count; a++)
                    {
                        RoundElementClass RoundElement = RoundSequence.RoundUnit[a];
                        if (RoundElement.DelayType == "Card")
                        {
                            _UI_Card_Unit QuickSave_Card_Script = RoundElement.Source.Source_Card;

                            List<string> QuickSave_CardActionData_StringList =
                                new List<string>();
                            Dictionary<string, PathPreviewClass> QuickSave_SequencePathPreview_Dictionay =
                                QuickSave_Card_Script._Card_BehaviorUnit_Script.
                                Key_Anime(QuickSave_Card_Script._Card_UseObject_Script,
                                null, false, RoundSequence.Time, a);
                            QuickSave_CardActionData_StringList.AddRange(
                                QuickSave_Card_Script._Card_BehaviorUnit_Script.
                                Key_ActionCall(QuickSave_SequencePathPreview_Dictionay,
                                null, false, RoundSequence.Time, a));
                            //����
                            if (RoundElement.Source.Source_Creature != null)
                            {
                                if (RoundElement.Source.Source_Creature == _Basic_Owner_Script)
                                {
                                    //�ۨ�
                                    QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                }
                                else
                                {
                                    if (RoundElement.Source.Source_Creature._Data_Sect_String ==
                                        _Basic_Owner_Script._Data_Sect_String)
                                    {
                                        //�ͤ�
                                        QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Friend", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                    }
                                    else
                                    {
                                        //�Ĥ�
                                        QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Enemy", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                    }
                                }
                            }
                            else
                            {
                                //����
                                QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Object", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                            }
                            yield return new WaitForSeconds(0.05f);//�����u�Ʃ���
                        }
                    }
                }

                //�W�[�ɶ�
                foreach (PathPreviewClass PathPreview in QuickSave_PathPreview_Dictionay.Values)
                {
                    _Map_BattleObjectUnit QuickSave_Object_Script =
                        PathPreview.UseObject;
                    QuickSave_Object_Script.
                        TimePositionRemove(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
                }

                //�]�m�ܼ�
                QuickSave_AIFind_ClassList.Add(
                    new AIFindSave
                    {
                        Behavior = QuickSave_MainCard_Script,
                        BehaviorUsingObject = QuickSave_MainObject_Script,
                        Enchances = QuickSave_Enchances_ScriptsList,
                        EnchancesUsingObjects = QuickSave_EnchancesObjects_ScriptsList,
                        Key = QuickSave_MainCard_Script._Card_BehaviorUnit_Script._Basic_Key_String,
                        StartCoordinate = QuickSave_StartCoordinate_Class,
                        StartTime = QuickSave_DelayBeforeTime_Int,
                        PathSelect =Pair,
                        Score = QuickSave_Score_Float
                    });
                yield return new WaitForSeconds(0.05f);//�����u�Ʃ���
            }
            //�٭�
            foreach (_UI_Card_Unit Card in QuickSave_Enchances_ScriptsList)
            {
                Card.EnchanceSet("Remove", QuickSave_EnchanceType_String, QuickSave_MainCard_Script,null);//���]�@��
            }
        }
        if (QuickSave_AIFind_ClassList.Count == 0)
        {
            float QuickSave_Score_Float = 0;
            //�L�ϥ�//�d���ϥά�Null��
            List<string> QuickSave_ScoreText_StringList = new List<string>();
            //�ݩR�ɶ�
            int QuickSave_DelayStandby_Int =
                QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
            //�ɶ��b����------------
            //�ɶ��b�o��
            foreach (RoundSequenceUnitClass RoundSequence in _Map_BattleRound._Round_RoundSequence_ClassList)
            {
                for (int a = 0; a < RoundSequence.RoundUnit.Count; a++)
                {
                    RoundElementClass RoundElement = RoundSequence.RoundUnit[a];
                    if (RoundElement.DelayType == "Card")
                    {
                        _UI_Card_Unit QuickSave_Card_Script = RoundElement.Source.Source_Card;

                        List<string> QuickSave_CardActionData_StringList =
                            new List<string>();
                        Dictionary<string, PathPreviewClass> QuickSave_SequencePathPreview_Dictionay =
                            QuickSave_Card_Script._Card_BehaviorUnit_Script.
                            Key_Anime(QuickSave_Card_Script._Card_UseObject_Script,
                            null, false, RoundSequence.Time, a);
                        QuickSave_CardActionData_StringList.AddRange(
                            QuickSave_Card_Script._Card_BehaviorUnit_Script.
                            Key_ActionCall(QuickSave_SequencePathPreview_Dictionay,
                            null, false, RoundSequence.Time, a));
                        //����
                        if (RoundElement.Source.Source_Creature != null)
                        {
                            if (RoundElement.Source.Source_Creature == _Basic_Owner_Script)
                            {
                                //�ۨ�
                                QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                            }
                            else
                            {
                                if (RoundElement.Source.Source_Creature._Data_Sect_String ==
                                    _Basic_Owner_Script._Data_Sect_String)
                                {
                                    //�ͤ�
                                    QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Friend", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                                }
                                else
                                {
                                    //�Ĥ�
                                    QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Enemy", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                                }
                            }
                        }
                        else
                        {
                            //����
                            QuickSave_Score_Float +=
                                _Object_Manager.ActionScore("Object", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                        }
                    }
                }
            }
            //NPC�d��---------------------
            //�ݩR�[��
            QuickSave_ScoreText_StringList.Add(
                "Move_Destination�U" + _Basic_Owner_Script._Basic_Object_Script.
                TimePosition(_Map_BattleRound._Round_Time_Int + QuickSave_DelayStandby_Int, _Map_BattleRound._Round_Order_Int).Vector3Int);//��m
            QuickSave_ScoreText_StringList.Add("DelayAfter�U" + QuickSave_DelayStandby_Int);//����q
            QuickSave_ScoreText_StringList.Add("Reverse�U" + _Basic_Owner_Script._Card_CardsBoard_ScriptList.Count);//�O�d�q
            QuickSave_ScoreText_StringList.AddRange(
                _World_Manager._UI_Manager._UI_CardManager.CardDeal("Normal",
                Mathf.Clamp(QuickSave_Concept_Script.Key_Deal(), 0, 65535), null,
                QuickSave_ConceptSource_Class, QuickSave_ConceptSource_Class, null,
                null, false, _Map_BattleRound._Round_Time_Int + QuickSave_DelayStandby_Int, _Map_BattleRound._Round_Order_Int));//��d�q
            //���ƹB��
            QuickSave_Score_Float += 
                _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_ScoreText_StringList, 65534, 65534, _AI_HateTarget_Script);
            //�]�m�ܼ�
            QuickSave_AIFind_ClassList.Add(
                new AIFindSave
                {
                    Behavior = null,
                    BehaviorUsingObject = null,
                    Enchances = null,
                    EnchancesUsingObjects = null,
                    Key = "Standby",
                    StartTime = 0,
                    StartCoordinate = null,
                    PathSelect = null,
                    Score = QuickSave_Score_Float
                });
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion NPC_AI


    #region Key_Action
    #region - Key_CardSelect -
    #endregion
    #endregion
}
