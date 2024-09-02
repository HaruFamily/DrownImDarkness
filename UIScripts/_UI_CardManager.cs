using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System;

public class _UI_CardManager : MonoBehaviour
{
    #region FollowerBox
    //底下資料集——————————————————————————————————————————————————————————————————————
    //——————————————————————————————————————————————————————————————————————
    #endregion FollowerBox

    #region ElementBox
    //子物件集——————————————————————————————————————————————————————————————————————
    //物件集----------------------------------------------------------------------------------------------------
    //路徑實體處存區
    public GameObject _Store_PathCoordinatte_GameObject;
    //----------------------------------------------------------------------------------------------------

    //戰鬥卡牌庫----------------------------------------------------------------------------------------------------
    //被選擇戰鬥卡片
    [HideInInspector] public _UI_Card_Unit _Card_UsingCard_Script;
    [HideInInspector] public _UI_Card_Unit _React_CallerCard_Script;
    [HideInInspector] public List<_UI_Card_Unit> _Card_EventingCard_ScriptsList;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public _Object_CreatureUnit _Battle_TargetCreature_Script;
    //----------------------------------------------------------------------------------------------------
    //子物件集——————————————————————————————————————————————————————————————————————
    #endregion ElementBox


    #region DictionarySet
    //各類圖片匯入區——————————————————————————————————————————————————————————————————————
    //設定資料類別----------------------------------------------------------------------------------------------------    
    //名稱與圖片
    [System.Serializable]
    public class PictureDataClass
    {
        public string Key;
        public Sprite Sprite;
    }
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion DictionarySet

    #region Crad
    #region - CardDeal -
    //發戰鬥卡——————————————————————————————————————————————————————————————————————
    public List<string> CardDeal(
        string Type/*Normal 普通、Target 指定(在Faction這裡做)、Priority 優先/TypeCards設置*/, 
        int DealNumber, List<_UI_Card_Unit> TypeCards,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //建立資料庫----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        List<_UI_Card_Unit> QuickSave_Target_ScriptsList = new List<_UI_Card_Unit>();

        _Object_CreatureUnit QuickSave_TargetCreature_Script = TargetSource.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            //基本
            case "Normal":
            case "Priority":
                {
                    //優先設定
                    List<_UI_Card_Unit> QuickSave_Priority_ScriptsList =
                        new List<_UI_Card_Unit>();
                    {
                        switch (Type)
                        {
                            case "Priority":
                                {
                                    QuickSave_Priority_ScriptsList.AddRange(TypeCards);
                                }
                                break;
                        }

                        List<_UI_Card_Unit> QuickSave_DeckAndCemetery_ScriptsList =
                            new List<_UI_Card_Unit>();
                        QuickSave_DeckAndCemetery_ScriptsList.AddRange(QuickSave_TargetCreature_Script._Card_CardsDeck_ScriptList);
                        QuickSave_DeckAndCemetery_ScriptsList.AddRange(QuickSave_TargetCreature_Script._Card_CardsCemetery_ScriptList);
                        foreach (_UI_Card_Unit Card in QuickSave_DeckAndCemetery_ScriptsList)
                        {
                            if (QuickSave_Priority_ScriptsList.Contains(Card))
                            {
                                continue;
                            }

                            //List<string> QuickSave_Data_StringList = new List<string> { Type };
                            bool QuickSave_SituationCheck_Bool = bool.Parse(
                                TargetSource.Source_BattleObject.SituationCaller(
                                "DealPriority", null,
                                TargetSource, Card._Basic_Source_Class, UsingObject,
                                HateTarget, Action, Time, Order)["BoolTrue"][0]);
                            if (QuickSave_SituationCheck_Bool)
                            {
                                QuickSave_Priority_ScriptsList.Add(Card);
                            }
                        }
                    }

                    //取卡
                    List<_UI_Card_Unit> QuickSave_Deck_ScriptsList =
                        new List<_UI_Card_Unit>(QuickSave_TargetCreature_Script._Card_CardsDeck_ScriptList);
                    List<_UI_Card_Unit> QuickSave_CemeteryAddDeck_ScriptsList =
                        new List<_UI_Card_Unit>(QuickSave_TargetCreature_Script._Card_CardsCemetery_ScriptList);
                    bool QuickSave_CemeteryAddDeck_Bool = false;
                    while (QuickSave_Target_ScriptsList.Count < DealNumber)
                    {
                        //排庫補充
                        if (QuickSave_Deck_ScriptsList.Count == 0)
                        {
                            if (QuickSave_TargetCreature_Script._Card_CardsCemetery_ScriptList.Count == 0)
                            {
                                break;
                            }
                            foreach (_UI_Card_Unit CemeteryCard in QuickSave_CemeteryAddDeck_ScriptsList)
                            {
                                if (QuickSave_Target_ScriptsList.Contains(CemeteryCard))
                                {
                                    continue;
                                }
                                QuickSave_Deck_ScriptsList.Add(CemeteryCard);
                            }
                            QuickSave_CemeteryAddDeck_Bool = true;
                        }

                        //卡片
                        _UI_Card_Unit QuickSave_RandomCard_Script = null;
                        //優先卡牌
                        List<_UI_Card_Unit> QuickSave_PriorityCard_ScriptsList =
                            new List<_UI_Card_Unit>(QuickSave_Priority_ScriptsList);
                        while (QuickSave_PriorityCard_ScriptsList.Count > 0)
                        {
                            int QuickSave_RandomSelect = UnityEngine.Random.Range(0, QuickSave_PriorityCard_ScriptsList.Count);
                            QuickSave_RandomCard_Script = QuickSave_PriorityCard_ScriptsList[QuickSave_RandomSelect];
                            //離開
                            if (QuickSave_Deck_ScriptsList.Contains(QuickSave_RandomCard_Script))
                            {
                                //在Deck之中
                                break;
                            }
                            else
                            {
                                QuickSave_PriorityCard_ScriptsList.Remove(QuickSave_RandomCard_Script);
                            }
                        }
                        if (QuickSave_RandomCard_Script != null)
                        {
                            QuickSave_Target_ScriptsList.Add(QuickSave_RandomCard_Script);
                            QuickSave_Deck_ScriptsList.Remove(QuickSave_RandomCard_Script);
                            //跳出
                            continue;
                        }

                        //選擇加入
                        List<_UI_Card_Unit> QuickSave_NowDeck_ScriptsList =
                            new List<_UI_Card_Unit>(QuickSave_Deck_ScriptsList);
                        if (QuickSave_NowDeck_ScriptsList.Count > 0)
                        {
                            int QuickSave_RandomSelect = UnityEngine.Random.Range(0, QuickSave_NowDeck_ScriptsList.Count);
                            QuickSave_RandomCard_Script = QuickSave_NowDeck_ScriptsList[QuickSave_RandomSelect];
                        }
                        if (QuickSave_RandomCard_Script != null)
                        {
                            QuickSave_Target_ScriptsList.Add(QuickSave_RandomCard_Script);
                            QuickSave_Deck_ScriptsList.Remove(QuickSave_RandomCard_Script);
                            //跳出
                            continue;
                        }
                        if (QuickSave_RandomCard_Script == null)
                        {
                            break;
                        }
                    }
                    //抽卡
                    if (Action)
                    {
                        if (QuickSave_CemeteryAddDeck_Bool)
                        {
                            CardsMove("Cemetery_Add_Deck", QuickSave_TargetCreature_Script, QuickSave_CemeteryAddDeck_ScriptsList,
                                Time, Order);
                        }
                        QuickSave_TargetCreature_Script._Basic_CardRecentDeal_ScriptsList = QuickSave_Target_ScriptsList;
                        CardsMove("Deck_Deal_Board", QuickSave_TargetCreature_Script, QuickSave_Target_ScriptsList,
                            Time, Order);
                    }
                }
                break;
            //指定
            case "Target":
                {
                    QuickSave_Target_ScriptsList.AddRange(TypeCards);
                    QuickSave_TargetCreature_Script._Basic_CardRecentDeal_ScriptsList = QuickSave_Target_ScriptsList;
                    CardsMove("Deck_Deal_Board", QuickSave_TargetCreature_Script, QuickSave_Target_ScriptsList,
                        Time, Order);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //設置
        int QuickSave_Value_Int = QuickSave_Target_ScriptsList.Count;
        if (!Action)
        {
            if (TargetSource.Source_Creature != null)
            {
                if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                {
                    Answer_Return_StringList.Add("Hater_DealCard｜" + QuickSave_Value_Int);
                }
                else
                {
                    if (UserSource.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Self_DealCard｜" + QuickSave_Value_Int);
                    }
                    else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                    {
                        Answer_Return_StringList.Add("Friend_DealCard｜" + QuickSave_Value_Int);
                    }
                    else
                    {
                        Answer_Return_StringList.Add("Enemy_DealCard｜" + QuickSave_Value_Int);
                    }
                }
            }
            else
            {
                Answer_Return_StringList.Add("Object_DealCard｜" + DealNumber);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion 

    #region - CardThrow -
    //丟卡——————————————————————————————————————————————————————————————————————
    public List<string> CardThrow(string Type/*Random,Target*/, int Random, List<_UI_Card_Unit> Target,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //建立資料庫----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        List<_UI_Card_Unit> QuickSave_Target_ScriptsList = new List<_UI_Card_Unit>();
        _Object_CreatureUnit QuickSave_TargetCreature_Script = TargetSource.Source_Creature;
        List<_UI_Card_Unit> QuickSave_Board_ScriptsList = 
            new List<_UI_Card_Unit>(QuickSave_TargetCreature_Script._Card_CardsBoard_ScriptList);
        int QuickSave_Value_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Random":
                {
                    if (QuickSave_Board_ScriptsList.Count > Random)
                    {
                        while (QuickSave_Target_ScriptsList.Count < Random)
                        {
                            _UI_Card_Unit QuickSave_RandomCard_Script =
                                QuickSave_Board_ScriptsList[UnityEngine.Random.Range(0, QuickSave_Board_ScriptsList.Count)];
                            QuickSave_Target_ScriptsList.Add(QuickSave_RandomCard_Script);
                            QuickSave_Board_ScriptsList.Remove(QuickSave_RandomCard_Script);
                        }
                    }
                    else
                    {
                        QuickSave_Target_ScriptsList.AddRange(QuickSave_Board_ScriptsList);
                    }
                }
                break;
            case "Target":
                {
                    foreach (_UI_Card_Unit Card in Target)
                    {
                        if (QuickSave_Board_ScriptsList.Contains(Card))
                        {
                            QuickSave_Target_ScriptsList.Add(Card);
                        }
                    }
                }
                break;
        }
        //設置
        QuickSave_Value_Int = QuickSave_Target_ScriptsList.Count;
        if (Action)
        {
            CardsMove("Board_Throw_Cemetery", QuickSave_TargetCreature_Script, QuickSave_Target_ScriptsList,
                Time, Order);
        }
        else
        {
            if (UserSource == null && TargetSource == null)
            {
                if (TargetSource.Source_Creature != null)
                {
                    if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == TargetSource.Source_Creature)
                    {
                        Answer_Return_StringList.Add("Hater_ThrowCard｜" + QuickSave_Value_Int);
                    }
                    else
                    {
                        if (UserSource.Source_Creature == TargetSource.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Self_ThrowCard｜" + QuickSave_Value_Int);
                        }
                        else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                        {
                            Answer_Return_StringList.Add("Friend_ThrowCard｜" + QuickSave_Value_Int);
                        }
                        else
                        {
                            Answer_Return_StringList.Add("Enemy_ThrowCard｜" + QuickSave_Value_Int);
                        }
                    }
                }
                else
                {
                    Answer_Return_StringList.Add("Object_ThrowCard｜" + QuickSave_Target_ScriptsList.Count);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - CardSet -
    //卡片變動——————————————————————————————————————————————————————————————————————
    public void CardsMove(string Key, _Object_CreatureUnit Creature, List<_UI_Card_Unit> Cards,
        int Time, int Order)
    {
        //變換行為----------------------------------------------------------------------------------------------------
        Dictionary<string, List<_UI_Card_Unit>> QuickSave_CardsMove_Dictionary = new Dictionary<string, List<_UI_Card_Unit>>();
        foreach (_UI_Card_Unit Card in Cards)
        {
            Dictionary<string, List<string>> QuickSave_SituationCaller_Dictionary =
                Creature._Basic_Object_Script.SituationCaller(
                "CardsMove",  new List<string> { Key },
                Creature._Basic_Object_Script._Basic_Source_Class, Card._Basic_Source_Class, Creature._Card_UsingObject_Script,
                null, true, Time, Order);
            string QuickSave_Key_String = QuickSave_SituationCaller_Dictionary["Key"][0];
            if(QuickSave_CardsMove_Dictionary.TryGetValue(QuickSave_Key_String, out List<_UI_Card_Unit> DicValue))
            {
                QuickSave_CardsMove_Dictionary[QuickSave_Key_String].Add(Card);
            }
            else
            {
                QuickSave_CardsMove_Dictionary.Add(QuickSave_Key_String, new List<_UI_Card_Unit> { Card });
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        foreach (string DicKey in QuickSave_CardsMove_Dictionary.Keys)
        {
            List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = QuickSave_CardsMove_Dictionary[DicKey];
            List<string> QuickSave_Split_StringList = new List<string>(DicKey.Split("_"[0]));
            foreach (_UI_Card_Unit DicCard in QuickSave_Cards_ScriptsList)
            {
                DicCard._Card_NowPosition_String = QuickSave_Split_StringList[2];
                #region - To -
                switch (QuickSave_Split_StringList[2])
                {
                    case "Select":
                        {

                        }
                        break;
                    case "Deck":
                        {
                            Creature._Card_CardsDeck_ScriptList.Add(DicCard);
                        }
                        break;
                    case "Board":
                        {
                            Creature._Card_CardsBoard_ScriptList.Add(DicCard);
                            InOfBoard_View(Creature, DicCard);
                        }
                        break;
                    case "Delay":
                        {
                            Creature._Card_CardsDelay_ScriptList.Add(DicCard);
                        }
                        break;
                    case "Cemetery":
                        {
                            Creature._Card_CardsCemetery_ScriptList.Add(DicCard);
                        }
                        break;
                    case "Exiled":
                        {
                            Creature._Card_CardsExiled_ScriptList.Add(DicCard);
                        }
                        break;
                }
                #endregion
                #region - From -
                switch (QuickSave_Split_StringList[0])
                {
                    case "Select":
                        {

                        }
                        break;
                    case "Deck":
                        {
                            Creature._Card_CardsDeck_ScriptList.Remove(DicCard);
                        }
                        break;
                    case "Board":
                        {
                            Creature._Card_CardsBoard_ScriptList.Remove(DicCard);
                        }
                        break;
                    case "Delay":
                        {
                            Creature._Card_CardsDelay_ScriptList.Remove(DicCard);
                        }
                        break;
                    case "Cemetery":
                        {
                            Creature._Card_CardsCemetery_ScriptList.Remove(DicCard);
                        }
                        break;
                    case "Exiled":
                        {
                            Creature._Card_CardsExiled_ScriptList.Remove(DicCard);
                        }
                        break;
                }
                #endregion

                #region - How -
                switch (QuickSave_Split_StringList[1])
                {
                    case "Add"://增加
                    case "Throw"://丟棄
                    case "Use"://使用
                    case "End"://延續/僅DelayToCemetery
                        {

                        }
                        break;
                    case "Deal"://抽卡
                        {
                            BoardLimit(Creature);
                        }
                        break;
                }
                #endregion
            }

            if (QuickSave_Split_StringList[0] == "Board")
            {
                if (QuickSave_Split_StringList[2] == "Select")
                {
                    foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                    {
                        OutOfBoard_View(Card,
                            _World_Manager._UI_Manager._UI_EventManager._Event_SelectStore_Transform);
                    }
                }
                else
                {
                    foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                    {
                        OutOfBoard_View(Card, null);//必須是null
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------        
        BoardRefresh(Creature);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void InOfBoard_View(_Object_CreatureUnit Creature, _UI_Card_Unit Card)
    {
        //----------------------------------------------------------------------------------------------------

        Card.transform.SetParent(Creature._Object_Inventory_Script._Skill_CardsStore_Transform);
        Card.transform.localScale = Vector3.one;
        Card.transform.localPosition = Vector3.zero;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void OutOfBoard_View(_UI_Card_Unit Card,Transform Store)
    {
        //----------------------------------------------------------------------------------------------------
        if (Store != null)
        {
            Card.transform.SetParent(Store);
            Card.transform.localScale = Vector3.zero;
        }
        else
        {
            Card.transform.SetParent(Card._Card_OwnerFaction_Script._Faction_SkillStore_Transform);
            Card.transform.localScale = Vector3.zero;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Refresh -
    //手牌上限——————————————————————————————————————————————————————————————————————
    public void BoardLimit(_Object_CreatureUnit Creature)
    {
        int QuickSave_DeleteCount_Int = 0;
        List<_UI_Card_Unit> QuickSave_Selects_ScriptsList = 
            new List<_UI_Card_Unit>(_World_Manager._UI_Manager._UI_EventManager._Event_Selects_Dictionary.Values);
        int QuickSave_Limit_Int = Creature._Object_Inventory_Script._Item_EquipConcepts_Script.Key_CardLimit();
        if (Creature._Card_CardsBoard_ScriptList.Count - QuickSave_Limit_Int > 23)
        {
            //總持卡量(包含Event)大於23
            QuickSave_DeleteCount_Int =
                Creature._Card_CardsBoard_ScriptList.Count -
                QuickSave_Limit_Int;
        }
        else
        {
            //不計算事件過量刪除
            //(EX:10(5c,5E)/7不刪除(10-5-7=-2)
            //(EX:10(8c,2E)/7刪除(10-2-7=1)
            QuickSave_DeleteCount_Int =
                Creature._Card_CardsBoard_ScriptList.Count - QuickSave_Selects_ScriptsList.Count - 
                QuickSave_Limit_Int;
        }
        //需要丟卡
        if (QuickSave_DeleteCount_Int > 0)
        {
            List<_UI_Card_Unit> QuickSave_Cards_ScriptList =
                new List<_UI_Card_Unit>();
            foreach (_UI_Card_Unit BoardCard in Creature._Card_CardsBoard_ScriptList)
            {
                if (!(QuickSave_DeleteCount_Int > 0))
                {
                    break;
                }
                if (QuickSave_Selects_ScriptsList.Contains(BoardCard))
                {
                    continue;
                }
                QuickSave_Cards_ScriptList.Add(BoardCard);
                QuickSave_DeleteCount_Int--;
            }
            CardsMove("Board_Limit_Cemetery", Creature, QuickSave_Cards_ScriptList,
                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        }
    }
    //——————————————————————————————————————————————————————————————————————

    //整理卡——————————————————————————————————————————————————————————————————————
    public void BoardRefresh(_Object_CreatureUnit Creature)
    {
        //整理卡片----------------------------------------------------------------------------------------------------        
        //設定
        for (int a = 0; a < Creature._Card_CardsBoard_ScriptList.Count; a++)
        {
            _UI_Card_Unit QuickSave_Card_Script = Creature._Card_CardsBoard_ScriptList[a];
            QuickSave_Card_Script.transform.SetAsLastSibling();
            //變更位置
            QuickSave_Card_Script._Card_Position_Int = a;
            //變更名稱
            QuickSave_Card_Script._Basic_View_Script._View_Offset_Transform.name = "Number：" + a +
                "_Key：" + QuickSave_Card_Script._Basic_Key_String;
            switch (_World_Manager._Authority_Scene_String)
            {
                case "Field":
                    QuickSave_Card_Script._Basic_View_Script.SimpleSet("Explore");
                    break;
                case "Battle":
                    switch (_Map_Manager._State_BattleState_String)
                    {
                        case "PlayerEnchance":
                            {
                                if (_Card_UsingCard_Script == null ||
                                    QuickSave_Card_Script == _Card_UsingCard_Script)
                                {
                                    QuickSave_Card_Script._Basic_View_Script.SimpleSet("Behavior");
                                }
                                else
                                {
                                    QuickSave_Card_Script._Basic_View_Script.SimpleSet("Enchance");
                                }
                            }
                            break;
                        default:
                            QuickSave_Card_Script._Basic_View_Script.SimpleSet("Behavior");
                            break;
                    }
                    break;
            }
        }
        if (Creature._Player_Script != null)
        {
            _View_Battle _View_Battle = _World_Manager._UI_Manager._View_Battle;
            _View_Battle._View_CardsDeck_Text.text = 
                Creature._Card_CardsDeck_ScriptList.Count.ToString();
            _View_Battle._View_CardsCemetery_Text.text = 
                Creature._Card_CardsCemetery_ScriptList.Count.ToString();
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Card
}
