using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class _Object_NPCUnit : MonoBehaviour
{
    #region Element
    //變數集——————————————————————————————————————————————————————————————————————
    //父階層狀態
    [HideInInspector] public _Object_CreatureUnit _Basic_Owner_Script;
    [HideInInspector] public _Map_BattleObjectUnit _Map_Coordinate_Script;

    //AI
    //Dat
    [HideInInspector] public Dictionary<string, float> _Creature_AI_Class;
    //仇恨目標
    [HideInInspector] public _Map_BattleObjectUnit _AI_HateTarget_Script;
    //仇恨值清單
    [HideInInspector] public Dictionary<_Map_BattleObjectUnit, float> _AI_HateList_Dictionary = new Dictionary<_Map_BattleObjectUnit, float>();
    //——————————————————————————————————————————————————————————————————————
    #endregion Element


    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    public void Awake()
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //路徑設定----------------------------------------------------------------------------------------------------
        //登錄至目錄
        //座標點
        _Map_BattleObjectUnit QuickSave_CoordinateUnit_Script = this.transform.GetComponent<_Map_BattleObjectUnit>();
        //生物資訊
        _Basic_Owner_Script = this.transform.GetComponent<_Object_CreatureUnit>();
        _Basic_Owner_Script._NPC_Script = this;
        _Map_Coordinate_Script = QuickSave_CoordinateUnit_Script;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild


    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart(string Key)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //出生設置----------------------------------------------------------------------------------------------------
        //設定捷徑
        _Basic_Owner_Script._NPC_Script = this;

        //基本設定(編號、派系、能力值)
        _Object_Manager.CreatureStartSet(_Basic_Owner_Script, Key);
        //設定AI
        _Creature_AI_Class = new Dictionary<string, float>(_Object_Manager._Data_CreatureAI_Dictionary[Key]);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void FieldBattleStartSet()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //原野設置----------------------------------------------------------------------------------------------------
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

        //戰鬥設置----------------------------------------------------------------------------------------------------
        _Object_Manager.FieldToBattle(_Basic_Owner_Script);
        _Basic_Owner_Script._Basic_Object_Script._Round_Unit_Class =
            _World_Manager._Map_Manager._Map_BattleRound.RoundCreatureAdd(_Basic_Owner_Script);
        _Basic_Owner_Script._Round_Standby_Bool = true;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    
    //——————————————————————————————————————————————————————————————————————
    public void FieldBattleEndSet(Vector SpawnPoint)
    {
        //捷徑----------------------------------------------------------------------------------------------------
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
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


    #region NPC_AI
    //變數——————————————————————————————————————————————————————————————————————
    //AI搜尋紀錄
    public class AIFindSave
    {
        public _Map_BattleObjectUnit BehaviorUsingObject;
        //主行為卡
        public _UI_Card_Unit Behavior;
        //附魔卡
        public List<_Map_BattleObjectUnit> EnchancesUsingObjects;
        public List<_UI_Card_Unit> Enchances;

        //順序編號
        public string Key;
        //行動時間
        public int StartTime;
        //行動位置
        public Vector StartCoordinate;

        public PathSelectPairClass PathSelect;

        //評分
        public float Score;
    }
    //——————————————————————————————————————————————————————————————————————

    //敵人行為——————————————————————————————————————————————————————————————————————
    public IEnumerator NPC_AI_Action()
    {
        //變數----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        List<_Map_BattleObjectUnit> QuickSave_Using_ScriptsList = 
            _Basic_Owner_Script._Object_Inventory_Script._Item_ReachObject_ScriptsList;
        List<_UI_Card_Unit> QuickSave_Cards_ScriptList =
            _Basic_Owner_Script._Card_CardsBoard_ScriptList;
        _Item_ConceptUnit QuickSave_Concept_Script =
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script;

        /*
        //卡片回報
        print("Start--------------------------------------------");
        foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptList)
        {
            print(Card._Basic_Key_String);
        }
        */
        //----------------------------------------------------------------------------------------------------

        //目標設定----------------------------------------------------------------------------------------------------
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

        //排序招式----------------------------------------------------------------------------------------------------
        #region - Permutations -
        List<List<KeyValuePair<_Map_BattleObjectUnit,string>>> QuickSave_SampPool_PairList = 
            new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>();
        //基本清單
        foreach (_Map_BattleObjectUnit Object in QuickSave_Using_ScriptsList)
        {
            for (int a = 0; a < QuickSave_Cards_ScriptList.Count; a++)
            {
                _UI_Card_Unit QuickSave_BehaviorCard_Script = QuickSave_Cards_ScriptList[a];
                //使用許可
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
                //附魔
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
        組合回報
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

        //數量抽樣(混亂生物選擇)----------------------------------------------------------------------------------------------------
        List<List<KeyValuePair<_Map_BattleObjectUnit, string>>> QuickSave_AISampPool_PairList = 
            new List<List<KeyValuePair<_Map_BattleObjectUnit, string>>>();
        int QuickSave_SamplingSize_Int = Mathf.FloorToInt(_Creature_AI_Class["SamplingSize"]);//抽樣數
        if (QuickSave_SampPool_PairList.Count >= QuickSave_SamplingSize_Int)
        {
            //Pool大於抽樣數
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
            //Pool小於抽樣數
            QuickSave_AISampPool_PairList = QuickSave_SampPool_PairList;
        }
        //待命新增
        QuickSave_AISampPool_PairList.Add(
            new List<KeyValuePair<_Map_BattleObjectUnit, string>> {
                new KeyValuePair<_Map_BattleObjectUnit, string>(null, "Standby") });
        //----------------------------------------------------------------------------------------------------

        //得分運算----------------------------------------------------------------------------------------------------
        #region - Score -
        //得分欄
        List<AIFindSave> QuickSave_ScorePermutations_FloatList = new List<AIFindSave>();//得分排序
        //開始判定/抽樣欄
        for (int a = 0; a < QuickSave_AISampPool_PairList.Count; a++)
        {
            //順序搜尋/逐一評分----------------------------------------------------------------------------------------------------
            //當前新增紀錄/  不同排序抽樣/該抽樣順序            
            yield return StartCoroutine(NPC_AI_Find(QuickSave_AISampPool_PairList[a]));//等待完成
            yield return new WaitForSeconds(0.1f);//體驗優化
            QuickSave_ScorePermutations_FloatList.AddRange(new List<AIFindSave>(QuickSave_AIFind_ClassList));

            //抽樣回報
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
        //清理暫存
        QuickSave_SampPool_PairList = null;
        QuickSave_AISampPool_PairList = null;
        #endregion
        //----------------------------------------------------------------------------------------------------

        //評分比較----------------------------------------------------------------------------------------------------
        #region - Compare -
        //最高分解
        AIFindSave QuickSave_BestAction_Class = null;
        //最高分
        float QuickSave_BestScore_Float = -65535;

        //比較
        //print("Compare:-----------------------------------------------");
        foreach (AIFindSave AIFind in QuickSave_ScorePermutations_FloatList)
        {
            //比較回報
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
                print("Key：" + AIFind.Key + "\n" +
                    "BehaviorUsing：" + AIFind.BehaviorUsingObject._Basic_Key_String + "\n" +
                    "BehaviorKey：" + AIFind.Behavior._Card_BehaviorUnit_Script._Basic_Key_String + "\n" +
                    "Enchance：" + QuickSave_Enchances_String + "\n" +
                    "EnchanceObject：" + QuickSave_EnchancesObject_String + "\n" +
                    "CoordinateHater：" + _AI_HateTarget_Script.TimePosition(AIFind.StartTime, 65535).Vector3Int + "\n" +
                    "CoordinateStart：" + AIFind.StartCoordinate.Vector3Int + "\n" +
                    "CoordinateTarget：" + AIFind.PathSelect.Select[0].Vector.Vector3Int + "\n" +
                    "StartTime：" + AIFind.StartTime + "/" +
                    _Map_BattleRound._Round_Time_Int + "\n" +
                    "Score：" + AIFind.Score + "\n");
            }
            else
            {
                print("Key：" + AIFind.Key + "\n" +
                    "StartTime：" + AIFind.StartTime + "/" + 
                    _Map_BattleRound._Round_Time_Int + "\n" +
                    "Score：" + AIFind.Score + "\n");
            }
            */
            if (AIFind.Score > QuickSave_BestScore_Float)
            {
                //設置
                QuickSave_BestAction_Class = AIFind;
                QuickSave_BestScore_Float = AIFind.Score;
            }
            else if(AIFind.Score == QuickSave_BestScore_Float)
            {
                if (Random.Range(0,2) == 1)//相同時1/2替換
                {
                    //設置
                    QuickSave_BestAction_Class = AIFind;
                    QuickSave_BestScore_Float = AIFind.Score;
                }
            }
        }
        //清理暫存
        QuickSave_ScorePermutations_FloatList = null;
        QuickSave_AIFind_ClassList = null;
        #endregion
        //----------------------------------------------------------------------------------------------------

        //動作----------------------------------------------------------------------------------------------------
        #region - Action -
        _Map_BattleObjectUnit QuickSave_Object_Script = _Basic_Owner_Script._Basic_Object_Script;
        //卡片
        if (QuickSave_BestAction_Class == null)
        {
            print("Null Wrong");
        }
        if (QuickSave_BestAction_Class.Behavior != null)
        {
            //變數
            _UI_Card_Unit QuickSave_Card_Script = QuickSave_BestAction_Class.Behavior;
            _Map_BattleObjectUnit QuickSave_Using_Script = QuickSave_BestAction_Class.BehaviorUsingObject;
            string QuickSave_EnchanceType_String = 
                QuickSave_BestAction_Class.Behavior._Card_BehaviorUnit_Script.Key_Enchance();
            //行為設置
            QuickSave_Card_Script._Range_UseData_Class = QuickSave_BestAction_Class.PathSelect;
            QuickSave_Card_Script._Card_UseObject_Script = QuickSave_BestAction_Class.BehaviorUsingObject;
            _Basic_Owner_Script._Card_UsingObject_Script = QuickSave_Using_Script;
            //附魔設置
            for (int a = 0; a < QuickSave_BestAction_Class.Enchances.Count; a++)
            {
                QuickSave_BestAction_Class.Enchances[a].
                    EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_Card_Script, QuickSave_BestAction_Class.EnchancesUsingObjects[a]);
            }
            //時間軸設置
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

            //回合設置
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
            //新增回合
            if (!_Map_Manager._State_Reacting_Bool)
            {
                _Map_BattleRound.RoundSequenceSet(
                    QuickSave_RoundSequence_Class, null);
            }
            else
            {
                //觸發
                _UI_Card_Unit QuickSave_ReactCard_Script =
                    _World_Manager._UI_Manager._UI_CardManager._React_CallerCard_Script;
                _Map_BattleRound.RoundSequenceSet(
                    QuickSave_RoundSequence_Class, QuickSave_ReactCard_Script._Round_GroupUnit_Class);
            }
            //呼叫動作
            QuickSave_Card_Script.UseCardStart("AI",FindOrder: QuickSave_BestAction_Class);
            _World_Manager._UI_Manager._UI_CardManager.BoardRefresh(_Basic_Owner_Script);
        }
        else
        {
            if (!_Map_Manager._State_Reacting_Bool)
            {
                //延遲時間
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
    //附魔亂數選擇
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
        //卡牌隨機排序
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
        //SampCard放上Object
        foreach (_UI_Card_Unit[] CardSamp in QuickSave_EnchanceSamp_ScriptsList)
        {
            List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>
                QuickSave_Samping_PairList =
            new List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>();
            foreach (_UI_Card_Unit Card in CardSamp)
            {
                //過往附魔群
                List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>> QuickSave_SampingSearching_PairList = 
                    new List<List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>>(QuickSave_Samping_PairList);
                QuickSave_Samping_PairList.Clear();
                foreach (List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> Samping in QuickSave_SampingSearching_PairList)
                {
                    //可附魔武器量
                    List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> QuickSave_CardUsing_PairList =
                        new List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>(Samping);
                    //行為附魔
                    foreach (KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit> EnchancePair in QuickSave_CardUsing_PairList)
                    {
                        EnchancePair.Value.EnchanceSet("Add", QuickSave_EnchanceType_String, Behavior, EnchancePair.Key);
                    }
                    //物件確認
                    foreach (_Map_BattleObjectUnit Object in Objects)
                    {
                        //該武器無法用於附魔
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
                        //新增至Samping中
                        KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit> QuickSave_Pair_Pair =
                            new KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>(Object, Card);
                        List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>> QuickSave_SampingInput_PairList =
                            new List<KeyValuePair<_Map_BattleObjectUnit, _UI_Card_Unit>>(QuickSave_CardUsing_PairList);
                        QuickSave_SampingInput_PairList.Add(QuickSave_Pair_Pair);
                        QuickSave_Samping_PairList.Add(QuickSave_SampingInput_PairList);
                    }
                    //所有武器皆無法附魔(該路線失效)
                    if (QuickSave_CardUsing_PairList.Count < 0)
                    {
                        break;
                    }
                    //附魔初始化
                    Behavior.EnchanceSet("Clear", QuickSave_EnchanceType_String, null, null);
                }
            }
            //完成Objects新增
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
    //——————————————————————————————————————————————————————————————————————

    //範圍擴充(範圍/路徑選擇延伸)——————————————————————————————————————————————————————————————————————
    List<AIFindSave> QuickSave_AIFind_ClassList = new List<AIFindSave>();
    public IEnumerator NPC_AI_Find(List<KeyValuePair<_Map_BattleObjectUnit, string>> Key)
    {
        //變數----------------------------------------------------------------------------------------------------
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

        //搜尋並打分----------------------------------------------------------------------------------------------------
        if (Key[0].Value != "Standby")
        {
            //卡牌庫
            List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = 
                _Basic_Owner_Script._Card_CardsBoard_ScriptList;
            //行為變數設定
            _UI_Card_Unit QuickSave_MainCard_Script =
                QuickSave_Cards_ScriptsList[int.Parse(Key[0].Value)];
            _Map_BattleObjectUnit QuickSave_MainObject_Script = Key[0].Key;
            QuickSave_MainCard_Script._Card_UseObject_Script = QuickSave_MainObject_Script;
            string QuickSave_EnchanceType_String =
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.Key_Enchance();
            //附魔變數設定
            List<KeyValuePair<_Map_BattleObjectUnit, string>> QuickSave_CardObjects_PairList =
                new List<KeyValuePair<_Map_BattleObjectUnit, string>>(Key);
            QuickSave_CardObjects_PairList.RemoveAt(0);//方便取得Enchance
            List<_UI_Card_Unit> QuickSave_Enchances_ScriptsList = new List<_UI_Card_Unit>();
            List<_Map_BattleObjectUnit> QuickSave_EnchancesObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
            foreach (KeyValuePair<_Map_BattleObjectUnit, string> Pair in QuickSave_CardObjects_PairList)
            {
                _UI_Card_Unit QuickSave_Card_Script = QuickSave_Cards_ScriptsList[int.Parse(Pair.Value)];
                QuickSave_Enchances_ScriptsList.Add(QuickSave_Card_Script);
                QuickSave_EnchancesObjects_ScriptsList.Add(Pair.Key);
                QuickSave_Card_Script.EnchanceSet("Add", QuickSave_EnchanceType_String, QuickSave_MainCard_Script, Pair.Key);//附魔作用
            }

            //卡片記錄變數
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
            //起始相關                
            Vector QuickSave_StartCoordinate_Class =
                QuickSave_MainObject_Script.TimePosition(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);

            //顯示範圍
            Dictionary<string, List<Vector>> QuickSave_Range_Dictionary =
                QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                Key_Range(QuickSave_StartCoordinate_Class, QuickSave_MainObject_Script, 
                QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
            //範圍
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
            //有可選擇範圍
            List<PathSelectPairClass> QuickSave_PathSelect_ClassList = 
                _Map_Manager.PathSelectPair(
                    QuickSave_MainCard_Script._Range_Path_Class,
                    QuickSave_MainCard_Script._Range_Select_Class);
            foreach (PathSelectPairClass Pair in QuickSave_PathSelect_ClassList)
            {
                float QuickSave_Score_Float = 0;
                List<string> QuickSave_ScoreText_StringList = new List<string>();
                QuickSave_ScoreText_StringList.Add("DelayBefore｜" + QuickSave_DelayBefore_Int);
                QuickSave_ScoreText_StringList.Add("DelayAfter｜" + QuickSave_DelayAfter_Int);
                QuickSave_MainCard_Script._Range_UseData_Class = Pair;
                QuickSave_MainCard_Script._Card_UseCenter_Class = Pair.Select[0].Vector;
                //自身評分------------
                Dictionary<string, PathPreviewClass> QuickSave_PathPreview_Dictionay =
                    QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                    Key_Anime(QuickSave_MainObject_Script,
                    _AI_HateTarget_Script, false, QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
                //增加時間
                foreach (PathPreviewClass PathPreviewValue in QuickSave_PathPreview_Dictionay.Values)
                {
                    PathPreviewValue.UseObject.
                        TimePositionAdd(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int, PathPreviewValue.FinalCoor);
                }
                //行為
                QuickSave_ScoreText_StringList.AddRange(
                    QuickSave_MainCard_Script._Card_BehaviorUnit_Script.
                    Key_ActionCall(QuickSave_PathPreview_Dictionay, _AI_HateTarget_Script, false,
                    QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int));
                //評分項目(保留卡片)
                int QuickSave_RecerseCard_Int =
                    _Basic_Owner_Script._Card_CardsBoard_ScriptList.Count -
                    1 - QuickSave_Enchances_ScriptsList.Count;
                QuickSave_ScoreText_StringList.Add("Reverse｜" + QuickSave_RecerseCard_Int);
                //分數轉換
                QuickSave_Score_Float +=
                    _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_ScoreText_StringList, 
                    QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int, _AI_HateTarget_Script);
                //時間軸評分------------
                //時間軸得分
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
                            //評分
                            if (RoundElement.Source.Source_Creature != null)
                            {
                                if (RoundElement.Source.Source_Creature == _Basic_Owner_Script)
                                {
                                    //自身
                                    QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                }
                                else
                                {
                                    if (RoundElement.Source.Source_Creature._Data_Sect_String ==
                                        _Basic_Owner_Script._Data_Sect_String)
                                    {
                                        //友方
                                        QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Friend", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                    }
                                    else
                                    {
                                        //敵方
                                        QuickSave_Score_Float +=
                                        _Object_Manager.ActionScore("Enemy", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                                    }
                                }
                            }
                            else
                            {
                                //物件
                                QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Object", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                        RoundSequence.Time, a, null);
                            }
                            yield return new WaitForSeconds(0.05f);//體驗優化延遲
                        }
                    }
                }

                //增加時間
                foreach (PathPreviewClass PathPreview in QuickSave_PathPreview_Dictionay.Values)
                {
                    _Map_BattleObjectUnit QuickSave_Object_Script =
                        PathPreview.UseObject;
                    QuickSave_Object_Script.
                        TimePositionRemove(QuickSave_DelayBeforeTime_Int, QuickSave_Order_Int);
                }

                //設置變數
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
                yield return new WaitForSeconds(0.05f);//體驗優化延遲
            }
            //還原
            foreach (_UI_Card_Unit Card in QuickSave_Enchances_ScriptsList)
            {
                Card.EnchanceSet("Remove", QuickSave_EnchanceType_String, QuickSave_MainCard_Script,null);//附魔作用
            }
        }
        if (QuickSave_AIFind_ClassList.Count == 0)
        {
            float QuickSave_Score_Float = 0;
            //無使用//卡片使用為Null時
            List<string> QuickSave_ScoreText_StringList = new List<string>();
            //待命時間
            int QuickSave_DelayStandby_Int =
                QuickSave_Concept_Script.Key_DelayStandby(ContainTimeOffset: false);
            //時間軸評分------------
            //時間軸得分
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
                        //評分
                        if (RoundElement.Source.Source_Creature != null)
                        {
                            if (RoundElement.Source.Source_Creature == _Basic_Owner_Script)
                            {
                                //自身
                                QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                            }
                            else
                            {
                                if (RoundElement.Source.Source_Creature._Data_Sect_String ==
                                    _Basic_Owner_Script._Data_Sect_String)
                                {
                                    //友方
                                    QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Friend", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                                }
                                else
                                {
                                    //敵方
                                    QuickSave_Score_Float +=
                                    _Object_Manager.ActionScore("Enemy", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                                }
                            }
                        }
                        else
                        {
                            //物件
                            QuickSave_Score_Float +=
                                _Object_Manager.ActionScore("Object", _Creature_AI_Class, QuickSave_CardActionData_StringList,
                                    RoundSequence.Time, a, null);
                        }
                    }
                }
            }
            //NPC卡片---------------------
            //待命加算
            QuickSave_ScoreText_StringList.Add(
                "Move_Destination｜" + _Basic_Owner_Script._Basic_Object_Script.
                TimePosition(_Map_BattleRound._Round_Time_Int + QuickSave_DelayStandby_Int, _Map_BattleRound._Round_Order_Int).Vector3Int);//位置
            QuickSave_ScoreText_StringList.Add("DelayAfter｜" + QuickSave_DelayStandby_Int);//延遲量
            QuickSave_ScoreText_StringList.Add("Reverse｜" + _Basic_Owner_Script._Card_CardsBoard_ScriptList.Count);//保留量
            QuickSave_ScoreText_StringList.AddRange(
                _World_Manager._UI_Manager._UI_CardManager.CardDeal("Normal",
                Mathf.Clamp(QuickSave_Concept_Script.Key_Deal(), 0, 65535), null,
                QuickSave_ConceptSource_Class, QuickSave_ConceptSource_Class, null,
                null, false, _Map_BattleRound._Round_Time_Int + QuickSave_DelayStandby_Int, _Map_BattleRound._Round_Order_Int));//抽卡量
            //分數運算
            QuickSave_Score_Float += 
                _Object_Manager.ActionScore("Self", _Creature_AI_Class, QuickSave_ScoreText_StringList, 65534, 65534, _AI_HateTarget_Script);
            //設置變數
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
    //——————————————————————————————————————————————————————————————————————
    #endregion NPC_AI


    #region Key_Action
    #region - Key_CardSelect -
    #endregion
    #endregion
}
