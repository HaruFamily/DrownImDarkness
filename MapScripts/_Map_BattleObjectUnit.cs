using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Map_BattleObjectUnit : MonoBehaviour
{
    #region ElementBox
    //變數集——————————————————————————————————————————————————————————————————————

    //位置相關----------------------------------------------------------------------------------------------------
    public Dictionary<int, Dictionary<int, Vector>> _Map_TimePosition_Dictionary = new Dictionary<int, Dictionary<int, Vector>>();//各時間位置
    //暫存
    public SaveDataClass _Basic_SaveData_Class = new SaveDataClass();
    //----------------------------------------------------------------------------------------------------

    //圖層設定----------------------------------------------------------------------------------------------------
    public List<_World_IsometicSorting> _IsometicSorting_Caller_ScriptList = new List<_World_IsometicSorting>();
    public Transform _Map_NumberStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //物件相關----------------------------------------------------------------------------------------------------
    //編號
    public string _Basic_BornScene_String;
    public string _Basic_Key_String;
    public string _Basic_SubNameSave_String;
    [HideInInspector] public LanguageClass _Basic_Language_Class;
    public SourceClass _Basic_Source_Class; 

    public string _Map_ObjectState_String;//使用情況(EX:驅動/待命)
    public _Object_CreatureUnit _Map_AffiliationOwner_Script;//所屬對象
    public _Map_BattleObjectUnit _Basic_DrivingOwner_Script;//驅動對象

    public Dictionary<string, float> _Basic_Status_Dictionary = new Dictionary<string, float>();//能力值
    public Dictionary<string, NumbericalValueClass> _Basic_Point_Dictionary = new Dictionary<string, NumbericalValueClass>();
    public _Item_Manager.MaterialDataClass _Basic_Material_Class = new _Item_Manager.MaterialDataClass();//素材值

    public Dictionary<string, _Skill_FactionUnit> _Skill_FactionList_Dictionary = new Dictionary<string, _Skill_FactionUnit>();
    public _Skill_FactionUnit _Skill_Faction_Script;

    public TimesLimitClass _Basic_TimesLimit_Class = new TimesLimitClass();//次數
    //----------------------------------------------------------------------------------------------------

    //效果----------------------------------------------------------------------------------------------------
    //效果列表
    public List<_Effect_EffectObjectUnit> _Effect_SpecialAffix_ScriptsList = 
        new List<_Effect_EffectObjectUnit>();
    public Dictionary<string, _Effect_EffectObjectUnit> _Effect_Passive_Dictionary = 
        new Dictionary<string, _Effect_EffectObjectUnit>();
    public Dictionary<string, _Effect_EffectObjectUnit> _Effect_Effect_Dictionary = 
        new Dictionary<string, _Effect_EffectObjectUnit>();
    public Transform _View_EffectStore_Transform;
    //----------------------------------------------------------------------------------------------------

    //時間單位----------------------------------------------------------------------------------------------------
    //自身時間單位
    public RoundElementClass _Round_Unit_Class;
    public RoundSequenceUnitClass _Round_GroupUnit_Class;
    //自身招式集(以此物為使用的招式)/(持有此物者)
    public List<RoundElementClass> _Round_UnitCards_ClassList = new List<RoundElementClass>();
    //----------------------------------------------------------------------------------------------------

    //動畫相關----------------------------------------------------------------------------------------------------
    public Transform _View_Store_Transform;
    //圖片
    public SpriteRenderer _View_MainSprite_SpriteRenderer;
    //說明高度
    public Transform _View_HeightOffset_Transform;
    //文字欄
    public TextMesh _View_Text_Text;
    //指示欄
    public SpriteRenderer _View_Icon_SpriteRenderer;

    //招式放置區
    public Transform _View_SkillStore_Transform;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion ElementBox

    #region Start
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SystemStart(string Key, SourceClass Source, 
        _Object_Manager.ObjectDataClass StatusData, LanguageClass Language)
    {
        //變數----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //來源設定
        _Basic_BornScene_String = _World_Manager._Authority_Scene_String;
        _Basic_Key_String = Key;
        _Basic_Source_Class = Source;
        _Basic_Language_Class = Language;
        _Basic_Source_Class.Source_BattleObject = this;
        //流派
        foreach (string FactionKey in StatusData.Faction)
        {
            _Skill_FactionList_Dictionary.Add(FactionKey, _Skill_Manager.FactionStartSet(
                _View_SkillStore_Transform,
                FactionKey, _Basic_Source_Class));
        }
        //標籤
        _Basic_Material_Class.Tag = new List<string>(StatusData.Tag);
        //設定素材能力值
        {
            _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
            _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;

            int QuickSave_Count_Int = 0;
            //基本素質/Size/Form......
            List<string> QuickSave_MaterialKey_StringList = _Item_Manager._Data_StatusMaterial_StringList;
            foreach (string StatusKey in QuickSave_MaterialKey_StringList)
            {
                _Basic_Material_Class.Status.Add(StatusKey, StatusData.MaterialData[QuickSave_Count_Int]);
                QuickSave_Count_Int++;
            }
            //設定基礎能力值
            QuickSave_Count_Int = 0;
            switch (Source.SourceType)
            {
                case "Concept":
                    {
                        List<string> QuickSave_StatusKey_StringList = 
                            _Item_Manager._Data_StatusConcept_StringList;
                        foreach (string StatusKey in QuickSave_StatusKey_StringList)
                        {
                            _Basic_Status_Dictionary.Add(StatusKey, StatusData.StatusData[QuickSave_Count_Int]);
                            QuickSave_Count_Int++;
                        }

                        List<string> QuickSave_PointKey_StringList = _Item_Manager._Data_PointConcept_StringList;
                        foreach (string PointKey in QuickSave_PointKey_StringList)
                        {
                            _Basic_Point_Dictionary.Add(PointKey, new NumbericalValueClass());
                        }
                    }
                    break;
                default:
                    {
                        List<string> QuickSave_StatusKey_StringList = 
                            _Item_Manager._Data_StatusObject_StringList;
                        foreach (string StatusKey in QuickSave_StatusKey_StringList)
                        {
                            _Basic_Status_Dictionary.Add(StatusKey, StatusData.StatusData[QuickSave_Count_Int]);
                            QuickSave_Count_Int++;
                        }

                        List<string> QuickSave_PointKey_StringList = _Item_Manager._Data_PointObject_StringList;
                        foreach (string PointKey in QuickSave_PointKey_StringList)
                        {
                            _Basic_Point_Dictionary.Add(PointKey, new NumbericalValueClass());
                        }
                    }
                    break;
            }
            //詞綴
            for (int a = 0; a < StatusData.SpecialAffix.Length; a++)
            {
                string QuickSave_SpcialAffix_String = StatusData.SpecialAffix[a];
                _Basic_Material_Class.SpecialAffix[a] = QuickSave_SpcialAffix_String;

                if (QuickSave_SpcialAffix_String == null ||
                    QuickSave_SpcialAffix_String == "")
                {
                    continue;
                }
                _Effect_Manager.GetEffectObject(
                    QuickSave_SpcialAffix_String, 1,
                    _Basic_Source_Class, _Basic_Source_Class,
                    null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
            }
        }

        if (_View_EffectStore_Transform == null)
        {
            print("NoTransSet:" + this.name);
        }

        switch (Source.SourceType)
        {
            case "Weapon":
            case "Item":
                {
                    _View_MainSprite_SpriteRenderer.sprite =
                        _World_Manager._View_Manager.GetSprite(Source.SourceType, "Map", Key);
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider =
                        _View_MainSprite_SpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider.enabled = true;

                    _World_Manager._Map_Manager._Map_BattleRound.
                        _Round_TimesLimits_ClassList.Add(_Basic_TimesLimit_Class);
                }
                break;
            case "Concept":
                {
                    _View_MainSprite_SpriteRenderer.sprite =
                        _World_Manager._View_Manager.GetSprite(Source.SourceType, "Map", Key);
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider =
                        _View_MainSprite_SpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider.enabled = true;

                    _World_Manager._Map_Manager._Map_BattleRound.
                        _Round_TimesLimits_ClassList.Add(_Basic_TimesLimit_Class);
                }
                break;
            case "Project":
                {
                    _View_MainSprite_SpriteRenderer.sprite = 
                        _World_Manager._View_Manager.GetSprite("Project",Key);
                    _View_HeightOffset_Transform.localPosition =
                        _World_Manager._View_Manager.GetVector3("ProjectHeightOffset", "Map", Key);
                    _Object_Manager._Basic_SaveData_Class.ObjectListDataAdd(Source.SourceType, this);
                }
                break;
            case "Object":
                {
                    //基本設定
                    _View_MainSprite_SpriteRenderer.sprite = 
                        _World_Manager._View_Manager.GetSprite("Object", "Map", Key);
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider =
                        _View_MainSprite_SpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider.enabled = true;
                    _Skill_Faction_Script = _Skill_FactionList_Dictionary[StatusData.Faction[0]];
                    StateSet("NoAffiliation", null, 
                        null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                    _Object_Manager._Basic_SaveData_Class.ObjectListDataAdd(Source.SourceType, this);

                    _World_Manager._Map_Manager._Map_BattleRound.
                        _Round_TimesLimits_ClassList.Add(_Basic_TimesLimit_Class);
                }
                break;
            case "TimePos":
                {
                    _View_MainSprite_SpriteRenderer.sprite =
                        _World_Manager._View_Manager.GetSprite("Basic", "Map", "TimePos");
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider = 
                        _View_MainSprite_SpriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
                    _IsometicSorting_Caller_ScriptList[0]._Map_MouseSencer_Collider.enabled = 
                        true;
                    _View_MainSprite_SpriteRenderer.color = 
                        _World_Manager._View_Manager.GetColor("Code", "Code");
                    _Basic_Key_String = Source.Source_Card._Basic_Key_String;
                    _View_MainSprite_SpriteRenderer.transform.localPosition = new Vector3(0, 0.24f, 0);
                    _View_MainSprite_SpriteRenderer.transform.localScale = new Vector3(3, 3, 3);
                    _Object_Manager._Basic_SaveData_Class.ObjectListDataAdd(Source.SourceType, this);
                }
                break;
            case "Material":
                {
                    //素材資料集
                    return;
                }
                break;
            case "Ground":
                {
                    _Object_Manager._Basic_SaveData_Class.ObjectListDataAdd(Source.SourceType, this);
                }
                break;

            default:
                print("Wrong Key with " + Source.SourceType);
                break;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public void AdvanceSet()
    {
        switch (_Basic_Key_String)
        {
            case "Object_OceanCurrent_Normal":
                {
                    string QuickSave_Name_String = "";
                    switch (_Basic_Status_Dictionary["ComplexPoint"])
                    {
                        case 1:
                            QuickSave_Name_String = "RightBottom";
                            break;
                        case 2:
                            QuickSave_Name_String = "LeftBottom";
                            break;
                        case 3:
                            QuickSave_Name_String = "LeftTop";
                            break;
                        case 4:
                            QuickSave_Name_String = "RightTop";
                            break;
                    }
                    _Basic_SubNameSave_String = QuickSave_Name_String;
                }
                break;
        }

    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start

    #region Set
    #region - Faction -
    //設置流派(多流派可選的情況下預設為0)——————————————————————————————————————————————————————————————————————
    public void FactionSet(string Key = "")
    {
        if(Key == "")
        {
            _Skill_Faction_Script = 
                _Skill_FactionList_Dictionary[new List<string>(_Skill_FactionList_Dictionary.Keys)[0]];
        }
        else
        {
            if (_Skill_FactionList_Dictionary.TryGetValue(Key, out _Skill_FactionUnit Value))
            {
                _Skill_Faction_Script = Value;
            }
        }
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Dirve -
    //意識消耗——————————————————————————————————————————————————————————————————————
    public List<string> StateSet(string Type, SourceClass UserSource,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        _UI_CardManager _UI_CardManager = _World_Manager._UI_Manager._UI_CardManager;
        List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = null;
        List<_UI_Card_Unit> QuickSave_BoardCards_ScriptsList = new List<_UI_Card_Unit>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - State -
            case "Affiliation"://所屬(未驅動時 帶著移動)
                //所有時自動匹配流派
                {
                    if (_Skill_Faction_Script == null)
                    {
                        FactionSet();
                        _Map_AffiliationOwner_Script = UserSource.Source_Creature;
                    }
                    _Map_ObjectState_String = Type;
                }
                break;
            case "NoAffiliation"://非所屬(未驅動時 會停留原地)
                {
                    _Map_ObjectState_String = Type;
                }
                break;
            case "Driving"://觸及驅動(物體離開驅動範圍時立即解除驅動)
                {
                    _Object_CreatureUnit QuickSave_Creature_Script = UserSource.Source_Creature;
                    _Map_BattleObjectUnit QuickSave_CreatureObject_Script =
                        QuickSave_Creature_Script._Basic_Object_Script;

                    _Basic_Source_Class.Source_Creature = QuickSave_Creature_Script;

                    _Skill_Faction_Script._Basic_Source_Class.Source_Creature = QuickSave_Creature_Script;
                    _Skill_Faction_Script._Basic_Source_Class.Source_BattleObject = this;

                    QuickSave_Cards_ScriptsList = _Skill_Faction_Script.Cards();
                    foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                    {
                        bool QuickSave_Check_Bool =
                            Card._Basic_SaveData_Class.BoolDataGet("AbandonResidue");
                        if (QuickSave_Check_Bool)
                        {
                            Card._Basic_SaveData_Class.BoolDataSet("AbandonResidue", false);
                            QuickSave_Cards_ScriptsList.Remove(Card);
                            continue;
                        }
                        Card._Basic_Source_Class.Source_Creature = QuickSave_Creature_Script;
                        Card._Basic_Source_Class.Source_BattleObject = this;
                    }
                    QuickSave_Creature_Script._Card_CardsDeck_ScriptList.AddRange(QuickSave_Cards_ScriptsList);
                    QuickSave_Creature_Script._Object_Inventory_Script._Item_DrivingObject_ScriptsList.Add(this);
                    if (!QuickSave_Creature_Script._Object_Inventory_Script.
                        _Item_EquipQueue_ScriptsList.Contains(this))
                    {
                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_DrivingMapObject_ScriptsList.Add(this);
                    }

                    _Map_NumberStore_Transform = QuickSave_CreatureObject_Script._Map_NumberStore_Transform;

                    _Basic_DrivingOwner_Script = QuickSave_CreatureObject_Script;
                    _Map_ObjectState_String = Type;
                    Dictionary<string, List<string>> QuickSave_Situation_Dictionary =
                        SituationCaller(
                            "Drive", null,
                            UserSource, _Basic_Source_Class, this,
                            HateTarget, Action,Time, Order);
                    Answer_Return_StringList.AddRange(
                        _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
                        QuickSave_Situation_Dictionary));

                    {
                        /*看已EffectObject評分是否足以負擔
                        //AI設置
                        if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == _Basic_Source_Class.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Hater_Driving");
                        }
                        else
                        {
                            if (UserSource.Source_Creature == TargetSource.Source_Creature)
                            {
                                Answer_Return_StringList.Add("Self_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                            }
                            else if (UserSource.Source_Creature._Data_Sect_String == TargetSource.Source_Creature._Data_Sect_String)
                            {
                                Answer_Return_StringList.Add("Friend_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                            }
                            else
                            {
                                Answer_Return_StringList.Add("Enemy_GetEffectCard｜" + Key + ":" + QuickSave_Increase_Int);
                            }
                        }*/
                    }
                }
                break;
            case "Abandoning"://取消驅動
            case "Break"://損毀
                {
                    _Object_CreatureUnit QuickSave_Creature_Script = UserSource.Source_Creature;
                    if (QuickSave_Creature_Script == null)
                    {
                        break;
                    }
                    if (_Map_AffiliationOwner_Script == null)
                    {
                        _Basic_Source_Class.Source_Creature = null;

                        _Skill_Faction_Script._Basic_Source_Class.Source_Creature = null;
                        _Skill_Faction_Script._Basic_Source_Class.Source_BattleObject = null;
                    }
                    QuickSave_Cards_ScriptsList = _Skill_Faction_Script.Cards();
                    foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                    {
                        //List<string> QuickSave_Data_StringList = new List<string> { Type };
                        bool QuickSave_SituationCheck_Bool = bool.Parse(
                            QuickSave_Creature_Script._Basic_Object_Script.SituationCaller(
                            "AbandonResidue", null,
                            QuickSave_Creature_Script._Basic_Object_Script._Basic_Source_Class, Card._Basic_Source_Class, this,
                            null, true, Time, Order)["BoolTrue"][0]);
                        if (QuickSave_SituationCheck_Bool)
                        {
                            QuickSave_Cards_ScriptsList.Remove(Card);
                            continue;
                        }

                        if (_Map_AffiliationOwner_Script == null)
                        {
                            Card._Basic_Source_Class.Source_Creature = null;
                            Card._Basic_Source_Class.Source_BattleObject = null;
                        }
                        switch (Card._Card_NowPosition_String)
                        {
                            case "Deck":
                                QuickSave_Creature_Script._Card_CardsDeck_ScriptList.Remove(Card);
                                break;
                            case "Board":
                                QuickSave_Creature_Script._Card_CardsBoard_ScriptList.Remove(Card);

                                _UI_CardManager.OutOfBoard_View(Card, null);
                                break;
                            case "Delay":
                                {
                                    _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
                                    _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
                                    print("DelayAfter");
                                    QuickSave_Creature_Script._Round_State_String = "DelayAfter";

                                    _Map_BattleRound.RoundSequenceSet(
                                        null,_Round_GroupUnit_Class);
                                    _Round_Unit_Class.DelayTime = 0;
                                    //刪除TimePos/Project
                                    Card.CreatureObjectDelete();
                                    //時間移除
                                    TimePositionRemove(Time, Order);

                                    QuickSave_Creature_Script._Card_CardsDelay_ScriptList.Remove(Card);
                                }
                                break;
                            case "Cemetery":
                                QuickSave_Creature_Script._Card_CardsCemetery_ScriptList.Remove(Card);
                                break;
                            case "Exiled":
                                QuickSave_Creature_Script._Card_CardsExiled_ScriptList.Remove(Card);
                                break;
                        }
                        Card._Card_NowPosition_String = "";
                    }
                    QuickSave_Creature_Script._Object_Inventory_Script._Item_DrivingObject_ScriptsList.Remove(this);
                    if (!QuickSave_Creature_Script._Object_Inventory_Script.
                        _Item_EquipQueue_ScriptsList.Contains(this))
                    {
                        QuickSave_Creature_Script._Object_Inventory_Script.
                            _Item_DrivingMapObject_ScriptsList.Remove(this);
                    }
                    _Basic_DrivingOwner_Script = null;
                    _Map_ObjectState_String = Type;

                    _UI_CardManager.BoardRefresh(QuickSave_Creature_Script);
                    switch (Type)
                    {
                        case "Abandoning":
                            {
                                Dictionary<string, List<string>> QuickSave_Situation_Dictionary =
                                    SituationCaller(
                                        "Abandon", null,
                                        UserSource, _Basic_Source_Class, this,
                                        HateTarget, Action, Time, Order);
                                Answer_Return_StringList.AddRange(
                                    _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
                                    QuickSave_Situation_Dictionary));
                                if (QuickSave_Creature_Script._Object_Inventory_Script._Item_EquipQueue_ScriptsList.Contains(this))
                                {
                                    StateSet("Affiliation", UserSource, HateTarget, Action, Time, Order);
                                }
                                else
                                {
                                    StateSet("NoAffiliation", UserSource, HateTarget, Action, Time, Order);
                                }
                            }
                            break;
                        case "Break":
                            break;
                    }
                    if (Action)
                    {
                        _World_Manager._UI_Manager._View_Battle.FocusSet(this);
                    }
                }
                break;
                #endregion
        }
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region PointSet
    #region - Set -
    #region - Damaged -
    //承受傷害/此為被命中者——————————————————————————————————————————————————————————————————————
    public List<string> Damaged(List<DamageClass> DamageClass /*NPC*/,_Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time,int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        _World_Manager _World_Manager = _World_Manager._World_GeneralManager;
        //回傳值
        List<string> Answer_Return_StringList = new List<string>();
        //命中判定
        bool QuickSave_Success_Bool = true;
        //陣營
        string QuickSave_Sect_String = "";

        _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
        //----------------------------------------------------------------------------------------------------

        //傷害----------------------------------------------------------------------------------------------------
        for (int a = 0; a < DamageClass.Count; a++)
        {
            //----------------------------------------------------------------------------------------------------
            for (int t = 0; t < DamageClass[a].Times; t++)
            {
                //變數----------------------------------------------------------------------------------------------------
                //傷害
                float QuickSave_Damage_Float = DamageClass[a].Damage;
                //----------------------------------------------------------------------------------------------------

                //效果減傷/迴避判定---------------------------------------------------------------------------------------------------
                //傷害變動
                if (QuickSave_Success_Bool)//未迴避
                {
                    //受擊方傷害增加/定額
                    Dictionary<string, List<string>> QuickSave_DamageValueAdd_Dictionary =
                        SituationCaller(
                            "DamageValueAdd", new List<string> { DamageClass[a].DamageType, QuickSave_Damage_Float.ToString() },
                            DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                    Answer_Return_StringList.AddRange(_World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
                        QuickSave_DamageValueAdd_Dictionary));
                    QuickSave_Damage_Float += float.Parse(QuickSave_DamageValueAdd_Dictionary["ValueAdd"][0]);
                    //受擊方傷害增加/倍率
                    Dictionary<string, List<string>> QuickSave_DamageValueMultiply_Dictionary =
                        SituationCaller(
                            "DamageValueMultiply", new List<string> { DamageClass[a].DamageType, QuickSave_Damage_Float.ToString() },
                            DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                    Answer_Return_StringList.AddRange(_World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
                        QuickSave_DamageValueMultiply_Dictionary));
                    QuickSave_Damage_Float *= float.Parse(QuickSave_DamageValueMultiply_Dictionary["ValueMultiply"][0]);
                    //傷害格檔
                    Dictionary<string, List<string>> QuickSave_DamageBlock_Dictionary =
                        SituationCaller(
                            "DamageBlock", new List<string> { DamageClass[a].DamageType, QuickSave_Damage_Float.ToString() },
                            DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order);
                    Answer_Return_StringList.AddRange(_World_Manager._World_GeneralManager.SituationCaller_TransToStringList(
                        QuickSave_DamageBlock_Dictionary));
                    QuickSave_Damage_Float = float.Parse(QuickSave_DamageBlock_Dictionary["Value"][0]);

                    //成功判定
                    if (QuickSave_Damage_Float <= 0)//未造成傷害
                    {
                        QuickSave_Success_Bool = false;
                    }
                }

                //造成傷害(即使為負數(傷害受到隔擋)也是發動)
                if (DamageClass[a].Source.Source_BattleObject != null)
                {
                    Answer_Return_StringList.AddRange(_World_Manager.SituationCaller_TransToStringList(
                        DamageClass[a].Source.Source_BattleObject.SituationCaller(
                            "Damage", new List<string> { DamageClass[a].DamageType, (QuickSave_Damage_Float).ToString(), QuickSave_Success_Bool.ToString() },
                            DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                            HateTarget, Action, Time, Order)));
                }
                _Map_BattleObjectUnit QuickSave_Object_Script = DamageClass[a].Source.Source_Creature._Basic_Object_Script;
                QuickSave_Object_Script._Basic_SaveData_Class.ValueDataSet(
                    "AttackTimes", QuickSave_Object_Script._Basic_SaveData_Class.ValueDataGet("AttackTimes", 1) + 1);
                //受到傷害(即使為負數(隔擋受到傷害)也是發動)
                Answer_Return_StringList.AddRange(_World_Manager.SituationCaller_TransToStringList(
                    SituationCaller(
                        "Damaged", new List<string> { DamageClass[a].DamageType, (QuickSave_Damage_Float).ToString(), QuickSave_Success_Bool.ToString() },
                        DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order)));
                //---------------------------------------------------------------------------------------------------

                //命中確認----------------------------------------------------------------------------------------------------
                //來源效果(攻擊命中時觸發)
                if (DamageClass[a].Source.SourceType == "Card")
                {
                    //受攻擊卡片效果
                    Answer_Return_StringList.AddRange(DamageClass[a].Source.Source_Card._Card_BehaviorUnit_Script.
                        Key_Damage(DamageClass[a].DamageType, QuickSave_Damage_Float, QuickSave_Success_Bool,
                        DamageClass[a].Source, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order));
                }

                if (QuickSave_Success_Bool)
                {
                    //執行傷害
                    Answer_Return_StringList.AddRange(PointSet(
                        "Damage", "MediumPoint", -QuickSave_Damage_Float, 1,
                        DamageClass[a].Source, UsingObject,
                        HateTarget, Action, Time, Order, Action));
                }
                if (DamageClass[0].Source.SourceType == "Behavior")//主傷害來源
                {
                    if (QuickSave_Success_Bool)
                    {
                        DamageClass[0].Source.Source_Card._Card_HitTargets_ScriptsList.Add(this);
                    }
                    DamageClass[0].Source.Source_Card._Card_AimTargets_ScriptsList.Add(this);
                }
                //----------------------------------------------------------------------------------------------------
            }
            //----------------------------------------------------------------------------------------------------
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - PointSet -
    //回復數值——————————————————————————————————————————————————————————————————————
    //Type = 類型(預視/設置/傷害)、 PointType = 要設置的類型(MediumPoint……)、 Number = 數值
    public List<string> PointSet(
        string Type/*Set:設置、Damage:傷害、Consume:消耗、Recover:回復*/, string PointType, float Number,int Times,
        SourceClass UserSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order, bool ViewCall)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = new List<string>();
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        //數值設定
        NumbericalValueClass Status = _Basic_Point_Dictionary[PointType];
        int QuickSave_PointTotal_Int =
            Key_Point(PointType, "Total", UserSource, _Basic_Source_Class);
        switch (PointType)
        {
            case "MediumPoint":
                {
                    //介質值不可超過(最大介質值、當前觸媒值)
                    int QuickSave_CatalystPoint_Int =
                        Key_Point("CatalystPoint", "Point", UserSource, _Basic_Source_Class);
                    if (QuickSave_CatalystPoint_Int < QuickSave_PointTotal_Int)
                    {
                        QuickSave_PointTotal_Int = QuickSave_CatalystPoint_Int;
                    }
                }
                break;
        }
        Number = Number * Times;
        Number = Mathf.RoundToInt(Mathf.Clamp(Number, -QuickSave_PointTotal_Int, QuickSave_PointTotal_Int - Status.Point));
        //----------------------------------------------------------------------------------------------------

        //數值設定----------------------------------------------------------------------------------------------------
        //特殊事項
        if (Action)
        {
            switch (PointType)
            {
                case "MediumPoint":
                case "CatalystPoint":
                case "ConsciousnessPoint":
                    {
                        Status.Point = Mathf.Clamp(Status.Point + Number, 0, 65535);                        
                    }
                    break;
            }
        }
        if (Number < 0 && !_World_Manager._Map_Manager._State_Reacting_Bool)
        {
            if (Type == "Damage" && PointType == "MediumPoint")
            {
                Answer_Return_StringList.AddRange(PointSet(
                    "Point", "CatalystPoint", -Number * 0.5f,1,
                    UserSource, UsingObject,
                    HateTarget, Action, Time, Order, Action));
            }
        }
        //陣營設置
        if (_Basic_Source_Class.Source_Creature != null)
        {
            if (HateTarget != null && HateTarget._Basic_Source_Class.Source_Creature == _Basic_Source_Class.Source_Creature)
            {
                Answer_Return_StringList.Add("Hater_" + PointType + "｜" + Number);
            }
            else
            {
                if (UserSource.Source_Creature == _Basic_Source_Class.Source_Creature)
                {
                    Answer_Return_StringList.Add("Self_" + PointType + "｜" + Number);
                }
                else if (UserSource.Source_Creature._Data_Sect_String == _Basic_Source_Class.Source_Creature._Data_Sect_String)
                {
                    Answer_Return_StringList.Add("Friend_" + PointType + "｜" + Number);
                }
                else
                {
                    Answer_Return_StringList.Add("Enemy_" + PointType + "｜" + Number);
                }
            }
        }
        else
        {
            Answer_Return_StringList.Add("Object_" + PointType + "｜" + Number);
        }
        //----------------------------------------------------------------------------------------------------

        //視覺效果----------------------------------------------------------------------------------------------------
        //判定與調整
        if (ViewCall && Action)
        {
            switch (PointType)
            {
                case "MediumPoint":
                    MediumPointView();
                    if (Number < 0)
                    {
                        switch (_World_Manager._Authority_Scene_String)
                        {
                            case "Field":
                                {
                                    if (_Basic_Source_Class.SourceType == "Concept" &&
                                        _Basic_Source_Class.Source_Creature._Player_Script != null)
                                    {
                                        StartCoroutine(_View_Manager.AnimeNumber(PointType, Mathf.RoundToInt(Number),
                                            _Basic_Source_Class.Source_Creature._Creature_FieldObjectt_Script._Map_NumberStore_Transform));
                                    }
                                }
                                break;
                            case "Battle":
                                {
                                    StartCoroutine(_View_Manager.AnimeNumber(PointType, Mathf.RoundToInt(Number),
                                        _Map_NumberStore_Transform));
                                }
                                break;
                        }
                    }
                    break;
                case "CatalystPoint":
                    CatalystPointView();
                    break;
                case "ConsciousnessPoint":
                    ConsciousnessPointView();
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------

        //死亡判定----------------------------------------------------------------------------------------------------
        if (PointType == "MediumPoint")//可考慮增加"CatalystPoint/有著：即使沒有過去但我現在仍然在這…的意思
        {
            if (_Basic_Point_Dictionary[PointType].Point <= 0)
            {
                bool QuickSave_Dead_Bool = true;
                bool QuickSave_DeadResist_Bool = false;
                //可否抵抗死亡
                Dictionary<string, List<string>> QuickSave_Dead_Dictionary =
                    SituationCaller(
                        "DeadResist", new List<string> { Type, Number.ToString() },
                        UserSource, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                Answer_Return_StringList.AddRange(
                    _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(QuickSave_Dead_Dictionary));
                QuickSave_DeadResist_Bool = bool.Parse(QuickSave_Dead_Dictionary["BoolTrue"][0]);
                if (QuickSave_DeadResist_Bool)/*true = 抵抗死亡*/
                {
                    QuickSave_Dead_Bool = false;
                }
                else
                {
                    //死亡 發動效果
                    QuickSave_Dead_Dictionary =
                    SituationCaller(
                        "Dead", new List<string> { Type, Number.ToString() },
                        UserSource, _Basic_Source_Class, UsingObject,
                        HateTarget, Action, Time, Order);
                    Answer_Return_StringList.AddRange(
                        _World_Manager._World_GeneralManager.SituationCaller_TransToStringList(QuickSave_Dead_Dictionary));
                }
                //死亡
                if (QuickSave_Dead_Bool)
                {
                    _State_Death_Bool = true;
                    if (_Basic_Source_Class.Source_Creature != null)
                    {
                        if (HateTarget != null && 
                            HateTarget._Basic_Source_Class.Source_Creature == _Basic_Source_Class.Source_Creature)
                        {
                            Answer_Return_StringList.Add("Hater_Dead｜" + 1);
                        }
                        else
                        {
                            if (UserSource.Source_Creature == _Basic_Source_Class.Source_Creature)
                            {
                                Answer_Return_StringList.Add("Self_Dead｜" + 1);
                            }
                            else if (UserSource.Source_Creature._Data_Sect_String == _Basic_Source_Class.Source_Creature._Data_Sect_String)
                            {
                                Answer_Return_StringList.Add("Friend_Dead｜" + 1);
                            }
                            else
                            {
                                Answer_Return_StringList.Add("Enemy_Dead｜" + 1);
                            }
                        }
                    }
                    else
                    {
                        Answer_Return_StringList.Add("Object_Dead｜" + 1);
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //回傳----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Death -
    public bool _State_Death_Bool = false;//死亡或損壞
    public bool _State_DeathCheck_Bool = false;//死亡或損壞
    //——————————————————————————————————————————————————————————————————————
    public bool Death()//是否繼續進行流程
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleRound _Map_BattleRound = _Map_Manager._Map_BattleRound;
        string QuickSave_Type_String = _Basic_Source_Class.SourceType;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _State_DeathCheck_Bool = true;
        switch (QuickSave_Type_String)
        {
            case "Concept":
                {
                    _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
                    if (QuickSave_Creature_Script._Player_Script != null)
                    {
                        _World_Manager._World_GeneralManager._World_ScenesManager.SwitchScenes("Camp");
                        return false;//終止
                    }
                    if (QuickSave_Creature_Script._NPC_Script != null)
                    {
                        switch (_World_Manager._Authority_Scene_String)
                        {
                            case "Battle":
                                {
                                    _Map_BattleRound._Round_RoundCreatures_ClassList.Remove(_Round_Unit_Class);
                                    _Map_BattleRound.RoundSequenceSet(
                                        null,_Round_GroupUnit_Class);

                                    List<_UI_Card_Unit> QuickSave_Cards_ScriptsList =
                                        QuickSave_Creature_Script._Card_CardsDelay_ScriptList;
                                    foreach (_UI_Card_Unit Card in QuickSave_Cards_ScriptsList)
                                    {
                                        _Map_BattleRound.RoundSequenceSet(
                                            null, Card._Round_GroupUnit_Class);
                                        //刪除TimePos/Project
                                        Card.CreatureObjectDelete();
                                    }

                                    //NPC死亡確認
                                    _Object_Manager._Object_NPCs_ScriptsList.Remove(_Basic_Source_Class.Source_Creature);
                                    if (_Object_Manager._Object_NPCs_ScriptsList.Count == 0)
                                    {
                                        _World_Manager._World_GeneralManager._World_ScenesManager.SwitchScenes("Field");
                                        return false;//終止
                                    }
                                }
                                break;
                        }
                    }
                }
                break;
            case "Weapon":
            case "Item":
            case "Object":
                {
                    _Object_Manager._Basic_SaveData_Class.ObjectListDataAdd("PreBreak", this);
                    _Object_Manager._Basic_SaveData_Class.SourceListDataAdd("PreBreak", _Basic_Source_Class);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------
        return true;//繼續
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region - View -
    #region - MediumPointView -
    //介質生命值圖示——————————————————————————————————————————————————————————————————————
    public void MediumPointView()
    {
        //變數----------------------------------------------------------------------------------------------------
        _View_Battle _View_Battle = _World_Manager._UI_Manager._View_Battle;
        NumbericalValueClass QuickSave_Status_Class = _Basic_Point_Dictionary["MediumPoint"];
        int QuickSave_Total_Int = Key_Point("MediumPoint", "Total", _Basic_Source_Class, null);
        int QuickSave_CatalystPoint_Int = Key_Point("CatalystPoint", "Point", _Basic_Source_Class, null);
        if (QuickSave_CatalystPoint_Int < QuickSave_Total_Int)
        {
            QuickSave_Total_Int = QuickSave_CatalystPoint_Int;
        }
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        float QuickSave_Precent_Float = 0;
        if (QuickSave_CatalystPoint_Int != 0)
        {
            QuickSave_Precent_Float = QuickSave_Status_Class.Point / QuickSave_Total_Int;
        }
        //介面
        if (_View_Battle.UI_FocusObject_Script == this)
        {
            _View_Battle.View_Points_ClassArray[0].BarValue.text =
                "MP:" + QuickSave_Status_Class.Point.ToString("0");
            _View_Battle.View_Points_ClassArray[0].BarTransform.localScale = new Vector3(QuickSave_Precent_Float, 1, 1);
        }
        switch (_Basic_Source_Class.SourceType)
        {
            case "Concept":
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Battle":
                            {
                                //地圖小血條
                                _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
                                if (QuickSave_Creature_Script == null)
                                {
                                    //生物初生成不需要
                                    break;
                                }
                                if (QuickSave_Status_Class.Point < QuickSave_Status_Class.Total() &&
                                    QuickSave_Precent_Float < 1)
                                {
                                    QuickSave_Creature_Script._UI_PointStore_Transform.localPosition =
                                        Vector3.zero;
                                    QuickSave_Creature_Script._UI_MapMediumPoint_Transform.localPosition =
                                        new Vector3(-2 + (2 * QuickSave_Precent_Float), 0, 0);
                                    QuickSave_Creature_Script._UI_MapMediumPoint_Transform.localScale =
                                        new Vector3(400 * (QuickSave_Precent_Float), 24, 1);
                                }
                                else
                                {
                                    QuickSave_Creature_Script._UI_PointStore_Transform.localPosition =
                                        new Vector3(65535, 0, 0);
                                }
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

    #region - CatalystPointView -
    //介質生命值圖示——————————————————————————————————————————————————————————————————————
    public void CatalystPointView()
    {
        //變數----------------------------------------------------------------------------------------------------
        _View_Battle _View_Battle = _World_Manager._UI_Manager._View_Battle;
        NumbericalValueClass QuickSave_Status_Class = _Basic_Point_Dictionary["CatalystPoint"];
        int QuickSave_Total_Int = Key_Point("CatalystPoint", "Total", _Basic_Source_Class, null);
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        float QuickSave_Precent_Float = 0;
        if (QuickSave_Total_Int != 0)
        {
            QuickSave_Precent_Float = QuickSave_Status_Class.Point / QuickSave_Total_Int;
        }

        //介面
        if (_View_Battle.UI_FocusObject_Script == this)
        {
            _View_Battle.View_Points_ClassArray[1].BarValue.text =
                "CP:" + QuickSave_Status_Class.Point.ToString("0");
            _View_Battle.View_Points_ClassArray[1].BarTransform.localScale = new Vector3(QuickSave_Precent_Float, 1, 1);
        }
        switch (_Basic_Source_Class.SourceType)
        {
            case "Concept":
                {
                    switch (_World_Manager._Authority_Scene_String)
                    {
                        case "Battle":
                            {
                                //地圖小血條
                                _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
                                if (QuickSave_Creature_Script == null)
                                {
                                    //生物初生成不需要
                                    break;
                                }
                                if (QuickSave_Precent_Float < 1)
                                {
                                    QuickSave_Creature_Script._UI_MapCatalystPoint_Transform.localPosition =
                                        new Vector3(-2 + (2 * QuickSave_Precent_Float), 0, 0);
                                    QuickSave_Creature_Script._UI_MapCatalystPoint_Transform.localScale =
                                        new Vector3(400 * (QuickSave_Precent_Float), 24, 1);
                                }
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

    #region - ConsciousnessPointView -
    private float QuickSave_ConsciousnessPointLimitSave_Float = 0;

    public object QuickSave_Object_Script { get; private set; }

    //操控值圖示——————————————————————————————————————————————————————————————————————
    public void ConsciousnessPointView()
    {
        //變數----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        _View_Battle _View_Battle = _UI_Manager._View_Battle;
        NumbericalValueClass QuickSave_Status_Class = _Basic_Point_Dictionary["ConsciousnessPoint"];

        float QuickSave_PointLimit = QuickSave_Status_Class.Total();
        int QuickSave_NewSize_Int = Mathf.CeilToInt(QuickSave_PointLimit / 4);
        Color QuickSave_Consciousness_Color = _World_Manager._View_Manager.GetColor("Status", "Consciousness");
        Color QuickSave_Dustal_Color = _World_Manager._View_Manager.GetColor("Code", "Empty");
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //介面
        if (_View_Battle.UI_FocusObject_Script == this)
        {
            //上限重新設定
            if (QuickSave_ConsciousnessPointLimitSave_Float != QuickSave_PointLimit)
            {
                if (_View_Battle._View_ConsciousnessPoint_Scripts.Count < QuickSave_NewSize_Int)
                {
                    for (int a = _View_Battle._View_ConsciousnessPoint_Scripts.Count; a < QuickSave_NewSize_Int; a++)
                    {
                        _View_Battle._View_ConsciousnessPoint_Scripts.Add(
                            Instantiate(_UI_Manager._UI_QuarterUnit_GameObject, _View_Battle._View_ConsciousnessPointStore_Transform).
                            GetComponent<_Object_ViewConsumeUnit>());
                    }
                }
                QuickSave_ConsciousnessPointLimitSave_Float = QuickSave_PointLimit;
            }

            //列表設定
            List<Image> QuickSave_BattleConsumeQuarter_ImageList = new List<Image>();
            for (int a = 0; a < _View_Battle._View_ConsciousnessPoint_Scripts.Count; a++)
            {
                QuickSave_BattleConsumeQuarter_ImageList.AddRange(_View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeQuarter_ImageArray);
                if (a < Mathf.CeilToInt(QuickSave_PointLimit / 4))
                {
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeHardBack_Image.color = Color.white;
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeTopBack_Image.color = Color.white;
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeLightBack_Image.color = Color.white;
                }
                else
                {
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeHardBack_Image.color = Color.clear;
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeTopBack_Image.color = Color.clear;
                    _View_Battle._View_ConsciousnessPoint_Scripts[a]._View_ConsumeLightBack_Image.color = Color.clear;
                }
            }
            //單位設定
            for (int a = 0; a < QuickSave_BattleConsumeQuarter_ImageList.Count; a++)
            {
                if (a < QuickSave_Status_Class.Point)
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = QuickSave_Consciousness_Color;
                }
                else if (a < QuickSave_PointLimit)
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = QuickSave_Dustal_Color;
                }
                else
                {
                    QuickSave_BattleConsumeQuarter_ImageList[a].color = new Color32(0, 0, 0, 0);
                }
            }
            //文字設定
            _View_Battle._View_ConsciousnessPoint_Text.text =
                QuickSave_Status_Class.Point.ToString("0");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion
    #endregion PointSet

    #region KeyAction
    #region - Key_Material -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_Material(string Type, SourceClass UserSource, SourceClass TargetSource)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, this,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, this,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        if (_Basic_Material_Class.Status.TryGetValue(Type, out int MatValue))
        {
            Answer_Return_Float = MatValue;
        }
        else
        {
            print("No:" + Type);
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.RoundToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Status -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public float Key_Status(string Type, SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        if (_Basic_Status_Dictionary.TryGetValue(Type, out float Value))
        {
            Answer_Return_Float = Value;
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 99);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Answer_Return_Float;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Point -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public int Key_Point(string Type, string SubType, SourceClass UserSource, SourceClass TargetSource)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { Type, SubType };
        QuickSave_AdvanceAdd_Float += float.Parse(
            SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        if (_Basic_Point_Dictionary.TryGetValue(Type, out NumbericalValueClass Value))
        {
            switch (SubType)
            {
                case "Point":
                    Answer_Return_Float = Value.Point;
                    break;
                case "Total":
                    Answer_Return_Float = Value.Total();
                    break;
                case "Gap":
                    Answer_Return_Float = Key_Point(Type, "Total", UserSource,TargetSource) - Value.Point;
                    break;
            }
        }
        else
        {
            print("No:" + Type);
        }
        //乘於比重
        Answer_Return_Float = Mathf.Clamp((Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float, 0, 65535);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_DelayTimely -
    //及時延遲(僵直等…會快速變動的延遲量——————————————————————————————————————————————————————————————————————
    public int Key_DelayTimely(string Type/*延遲類型*/, SourceClass TargetSource/*Concept or Card*/)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { "Timely" + Type };
        QuickSave_AdvanceAdd_Float += float.Parse(
            SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, this,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                _Basic_Source_Class, TargetSource, this,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        //-就是為0
        //乘於比重
        Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Stack -
    //效果層數——————————————————————————————————————————————————————————————————————
    public int Key_Stack(string Type,string KeyTag, 
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        float Answer_Return_Float = 0;
        //額外增加
        float QuickSave_AdvanceAdd_Float = 0;
        float QuickSave_AdvanceMultiply_Float = 1;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { Type, KeyTag };
        QuickSave_AdvanceAdd_Float += float.Parse(
            SituationCaller(
                "GetStackValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueAdd"][0]);
        QuickSave_AdvanceMultiply_Float *= float.Parse(
            SituationCaller(
                "GetStackValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, UsingObject,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["ValueMultiply"][0]);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        foreach (_Effect_EffectObjectUnit Effect in _Effect_Effect_Dictionary.Values)
        {
            Answer_Return_Float += Effect.Key_Stack(Type, KeyTag, UserSource, TargetSource, UsingObject);
        }
        //乘於比重
        if (Answer_Return_Float > 0)
        {
            Answer_Return_Float = (Answer_Return_Float + QuickSave_AdvanceAdd_Float) * QuickSave_AdvanceMultiply_Float;
        }
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        if (Type == "Default" && KeyTag == "EffectObject_Scarlet_ScarletBunch_0｜0")
        {
            print(Answer_Return_Float);
        }
        return Mathf.CeilToInt(Answer_Return_Float);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Tag -
    //顯示範圍——————————————————————————————————————————————————————————————————————
    public List<string> Key_Tag(SourceClass UserSource, SourceClass TargetSource)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        List<string> Answer_Return_StringList = null;
        //額外增加
        List<string> QuickSave_EffectAdden_StringList = new List<string>();//源於技能
        List<string> QuickSave_Add_StringList = null;
        List<string> QuickSave_Remove_StringList = null;
        //----------------------------------------------------------------------------------------------------

        //比照被動/效果/詞綴----------------------------------------------------------------------------------------------------
        //數值計算
        List<string> QuickSave_Data_StringList =
            new List<string> { "Tag" };
        QuickSave_Add_StringList =
            SituationCaller(
                "GetTagAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagAdd"];
        QuickSave_Remove_StringList =
            SituationCaller(
                "GetTagRemove", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int)["TagRemove"];
        foreach (_Effect_EffectObjectUnit Effect in _Effect_Effect_Dictionary.Values)
        {
            //效果Tag附加於物體上
            QuickSave_EffectAdden_StringList.AddRange(Effect.Key_EffectTag(Effect._Basic_Source_Class, _Basic_Source_Class));
        }
        QuickSave_Add_StringList.AddRange(QuickSave_EffectAdden_StringList);
        //----------------------------------------------------------------------------------------------------

        //設定數字----------------------------------------------------------------------------------------------------
        //設定基本值
        Answer_Return_StringList = new List<string>(_Basic_Material_Class.Tag);
        //----------------------------------------------------------------------------------------------------

        //回傳值----------------------------------------------------------------------------------------------------
        Answer_Return_StringList = _World_Manager._Skill_Manager.TagsSet(Answer_Return_StringList, QuickSave_Add_StringList, QuickSave_Remove_StringList);
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion KeyAction

    #region - Move -
    //——————————————————————————————————————————————————————————————————————
    public IEnumerator MovedSet(string MoveType, Vector StartCoordinate, Vector EndCoordinate,
        int Time, int Order, bool CallNext)
    {
        //----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        float QuickSave_Pincg_Float = 0.24f;//視覺間格
        int QuickSave_SortingLayerAdden_Int = 1;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (_Basic_Source_Class.SourceType)
        {
            case "Concept":
                {
                    //圖示移動
                    _Basic_Source_Class.Source_Creature._Map_Sprite_SpriteRenderer.sortingOrder =
                        (int)(EndCoordinate.x + EndCoordinate.y *
                        _World_Manager._Map_Manager._Map_BattleCreator._Map_MapSize_Vector2.x + 10);
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //新地塊移動----------------------------------------------------------------------------------------------------
        if (EndCoordinate != null)//通常不可能
        {
            List<_Map_BattleObjectUnit> QuickSave_TimeObjects_ScriptsList =
                new List<_Map_BattleObjectUnit>();
            switch (_Basic_Source_Class.SourceType)
            {
                case "TimePos":
                    {
                        TimePositionAdd(Time, Order, EndCoordinate);
                        QuickSave_TimeObjects_ScriptsList.Add(this);
                    }
                    break;
                default:
                    {
                        TimePositionAdd(Time, Order, EndCoordinate);
                        QuickSave_TimeObjects_ScriptsList =
                            _Object_Manager.
                            TimeObjects("All", null,
                            Time, Order, EndCoordinate);
                        QuickSave_TimeObjects_ScriptsList.Sort(new SourceTypeComparer("Normal"));
                    }
                    break;
            }
            //移動
            switch (MoveType)
            {
                case "Spawn":
                case "Instant":
                    {
                        foreach (_Map_BattleObjectUnit Object in QuickSave_TimeObjects_ScriptsList)
                        {
                            int QuickSave_Sorting_Int = QuickSave_TimeObjects_ScriptsList.IndexOf(Object);
                            Vector2 QuickSave_Coor_Vector =
                                _World_Manager._Map_Manager._Map_BattleCreator.
                                _Math_CooridnateTransform_Vector2(EndCoordinate);
                            QuickSave_Coor_Vector =
                                new Vector2(QuickSave_Coor_Vector.x, 
                                QuickSave_Coor_Vector.y + ((0.4f + QuickSave_Sorting_Int) * QuickSave_Pincg_Float));
                            Object.transform.localPosition = QuickSave_Coor_Vector;

                            foreach (_World_IsometicSorting SortingScripts in Object._IsometicSorting_Caller_ScriptList)
                            {
                                SortingScripts.SortingRefresh(EndCoordinate.X, EndCoordinate.Y,
                                    QuickSave_Sorting_Int + QuickSave_SortingLayerAdden_Int);
                            }
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;
                case "Normal":
                    {
                        foreach (_Map_BattleObjectUnit Object in QuickSave_TimeObjects_ScriptsList)
                        {
                            int QuickSave_Sorting_Int = QuickSave_TimeObjects_ScriptsList.IndexOf(Object);
                            Vector2 QuickSave_Coor_Vector =
                                _World_Manager._Map_Manager._Map_BattleCreator.
                                _Math_CooridnateTransform_Vector2(EndCoordinate);
                            QuickSave_Coor_Vector =
                                new Vector2(QuickSave_Coor_Vector.x, 
                                QuickSave_Coor_Vector.y + ((0.4f + QuickSave_Sorting_Int) * QuickSave_Pincg_Float));
                            for (int t = 0; t <= 10 / (_World_Manager._Config_AnimationSpeed_Float * 0.01f); t++)
                            {
                                Object.transform.localPosition =
                                Vector2.Lerp(Object.transform.localPosition,
                                QuickSave_Coor_Vector,
                                t * 0.1f * (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                                yield return new WaitForSeconds(0.005f);
                            }
                            foreach (_World_IsometicSorting SortingScripts in Object._IsometicSorting_Caller_ScriptList)
                            {
                                SortingScripts.SortingRefresh(EndCoordinate.X, EndCoordinate.Y,
                                    QuickSave_Sorting_Int + QuickSave_SortingLayerAdden_Int);
                            }
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------
        /*戰鬥視野
        if (_Basic_Source_Class.SourceType == "Creature" &&
            _Basic_Source_Class.Source_Creature._Player_Script != null)
        {
            _Item_ConceptUnit QuickSave_Concept_Script = 
                _Basic_Source_Class.Source_Creature._Object_Inventory_Script._Item_EquipConcepts_Script;
            _Map_BattleCreator.ViewSet(EndCoordinate, QuickSave_Concept_Script.Key_BattleVision());
        }*/
        //舊地塊調整----------------------------------------------------------------------------------------------------
        if (StartCoordinate != null)//生成等
        {
            List<_Map_BattleObjectUnit> QuickSave_TimeObjects_ScriptsList =
                new List<_Map_BattleObjectUnit>();
            switch (_Basic_Source_Class.SourceType)
            {
                case "TimePos":
                    {
                        yield break;
                    }
                    break;
                default:
                    {
                        QuickSave_TimeObjects_ScriptsList =
                            _Object_Manager.
                            TimeObjects("All", null,
                            Time, Order, StartCoordinate);
                        QuickSave_TimeObjects_ScriptsList.Sort(new SourceTypeComparer("Normal"));
                    }
                    break;
            }
            //移動
            switch (MoveType)
            {
                case "Spawn":
                case "Instant":
                    {
                        foreach (_Map_BattleObjectUnit Object in QuickSave_TimeObjects_ScriptsList)
                        {
                            int QuickSave_Sorting_Int = 
                                QuickSave_TimeObjects_ScriptsList.IndexOf(Object);
                            Vector2 QuickSave_Coor_Vector =
                                _World_Manager._Map_Manager._Map_BattleCreator.
                                _Math_CooridnateTransform_Vector2(StartCoordinate);
                            QuickSave_Coor_Vector = 
                                new Vector2(QuickSave_Coor_Vector.x, QuickSave_Coor_Vector.y + ((1 + QuickSave_Sorting_Int) * QuickSave_Pincg_Float));
                            Object.transform.localPosition = QuickSave_Coor_Vector;
                            foreach (_World_IsometicSorting SortingScripts in Object._IsometicSorting_Caller_ScriptList)
                            {
                                SortingScripts.SortingRefresh(EndCoordinate.X, EndCoordinate.Y, QuickSave_Sorting_Int + QuickSave_SortingLayerAdden_Int);
                            }
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;
                case "Normal":
                    {
                        foreach (_Map_BattleObjectUnit Object in QuickSave_TimeObjects_ScriptsList)
                        {
                            int QuickSave_Sorting_Int = 
                                QuickSave_TimeObjects_ScriptsList.IndexOf(Object);
                            Vector2 QuickSave_Coor_Vector =
                                _World_Manager._Map_Manager._Map_BattleCreator.
                                _Math_CooridnateTransform_Vector2(StartCoordinate);
                            QuickSave_Coor_Vector =
                                new Vector2(QuickSave_Coor_Vector.x, QuickSave_Coor_Vector.y + 
                                ((1 + QuickSave_Sorting_Int) * QuickSave_Pincg_Float));

                            for (int t = 0; t <= 10 / (_World_Manager._Config_AnimationSpeed_Float * 0.01f); t++)
                            {
                                Object.transform.localPosition =
                                Vector2.Lerp(Object.transform.localPosition,
                                QuickSave_Coor_Vector,
                                t * 0.1f * (_World_Manager._Config_AnimationSpeed_Float * 0.01f));
                                yield return new WaitForSeconds(0.005f);
                            }
                            foreach (_World_IsometicSorting SortingScripts in Object._IsometicSorting_Caller_ScriptList)
                            {
                                SortingScripts.SortingRefresh(StartCoordinate.X, StartCoordinate.Y,
                                    QuickSave_Sorting_Int + QuickSave_SortingLayerAdden_Int);
                            }
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                    break;
            }
        }
        if (CallNext)
        {
            //呼叫下個路徑目標
            _World_Manager._Map_Manager._Map_MoveManager.
                MoveCoroutineCaller("Next");
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Interactive -
    //_State_StateResist_StringList
    //——————————————————————————————————————————————————————————————————————
    public List<string> InteractiveSet(string Type,
        SourceClass UserSource, SourceClass TargetSource,_Map_BattleObjectUnit UsingObject, 
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order,
        int InterferenceValue = 0,
        string ShiftType = "", string ShiftPathType = "", int ShiftRange = 0, Vector ShiftCenter = null/*擊退/吸引中心點*/)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //免疫----------------------------------------------------------------------------------------------------
        if (_Map_ObjectState_String == "Affiliation")
        {
            return Answer_Return_StringList;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Interrupt":
                InterruptSet(Time, Order);
                break;
            case "Shift":
                Answer_Return_StringList = 
                    ShiftSet(ShiftType, ShiftRange, ShiftCenter, 
                    UserSource, TargetSource, UsingObject,
                    HateTarget, Action, Time, Order);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    
    //打斷——————————————————————————————————————————————————————————————————————
    private void InterruptSet(int Time,int Order)
    {
        //----------------------------------------------------------------------------------------------------
        string Answer_Return_String = "";
        _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
        //----------------------------------------------------------------------------------------------------

        ////目標選擇----------------------------------------------------------------------------------------------------
        if (_Basic_Source_Class.Source_Creature != null)
        {
            _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;

            RoundElementClass QuickSave_CreatureElement_Class = null;
            _UI_Card_Unit QuickSave_Card_Script = null;
            RoundElementClass QuickSave_CardElement_Class = null;
            switch (QuickSave_Creature_Script._Round_State_String)
            {
                case "DelayBefore":
                    QuickSave_CardElement_Class = 
                        QuickSave_Creature_Script._Basic_Object_Script._Round_UnitCards_ClassList[0];
                    QuickSave_Card_Script = QuickSave_CardElement_Class.Source.Source_Card;

                    //移除招式
                    QuickSave_Card_Script._Round_GroupUnit_Class.RoundUnit.Remove(QuickSave_CardElement_Class);
                    QuickSave_CardElement_Class.DelayTime = 0;
                    //刪除TimePos/Project
                    QuickSave_Card_Script.CreatureObjectDelete();
                    //時間移除
                    TimePositionRemove(Time, Order);
                    QuickSave_Card_Script.UseCardEnd();
                    break;
            }

        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private List<string> ShiftSet(string Type, int Range, Vector Center/*擊退/吸引中心點*/,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<string> Answer_Return_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        Vector QuickSave_NowCoordinate_Class =
            TimePosition(
                _Map_BattleRound._Round_Time_Int,
                _Map_BattleRound._Round_Order_Int);
        int QuickSave_XDis_Int = Center.X - QuickSave_NowCoordinate_Class.X;
        int QuickSave_YDis_Int = Center.Y - QuickSave_NowCoordinate_Class.Y;

        int QuickSave_XDirection_Int = 0;
        int QuickSave_YDirection_Int = 0;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        //右下
        if (QuickSave_XDis_Int < 0 &&
            QuickSave_YDis_Int <= 0)
        {
            QuickSave_XDirection_Int = 1;
            QuickSave_YDirection_Int = 1;
        }
        //左下
        if (QuickSave_XDis_Int >= 0 &&
            QuickSave_YDis_Int < 0)
        {
            QuickSave_XDirection_Int = -1;
            QuickSave_YDirection_Int = 1;
        }
        //左上
        if (QuickSave_XDis_Int > 0 &&
            QuickSave_YDis_Int >= 0)
        {
            QuickSave_XDirection_Int = -1;
            QuickSave_YDirection_Int = -1;
        }
        //右上
        if (QuickSave_XDis_Int <= 0 &&
            QuickSave_YDis_Int > 0)
        {
            QuickSave_XDirection_Int = 1;
            QuickSave_YDirection_Int = -1;
        }
        switch (Type)
        {
            case "Push":
                break;
            case "Pull":
                QuickSave_XDirection_Int *= -1;
                QuickSave_YDirection_Int *= -1;
                break;
        }
        //距離設置
        int QuickSave_Distance_Int = Range;
        List<string> QuickSave_Data_StringList =
            new List<string> { Type };
        QuickSave_Distance_Int += int.Parse(
            SituationCaller(
                "GetStatusValueAdd", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                HateTarget, Action, Time,Order)["ValueAdd"][0]);
        QuickSave_Distance_Int *= int.Parse(
            SituationCaller(
                "GetStatusValueMultiply", QuickSave_Data_StringList,
                UserSource, TargetSource, null,
                HateTarget, Action, Time, Order)["ValueMultiply"][0]);
        Answer_Return_StringList.Add(Type + "｜" + QuickSave_Distance_Int);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        DirectionPathClass QuickSave_Path_Class = 
            _Map_Manager._Map_BattleCreator.Path
            ("Normal", QuickSave_NowCoordinate_Class, 
            XOffset:QuickSave_XDirection_Int, YOffset: QuickSave_YDirection_Int, 
            PathData: _World_Manager._Skill_Manager._Data_Path_Dictionary["Path_Forward"]);
        QuickSave_Path_Class.Path = QuickSave_Path_Class.Path.GetRange(0, QuickSave_Distance_Int + 1);
        QuickSave_Path_Class.Direction = QuickSave_Path_Class.Direction.GetRange(0, QuickSave_Distance_Int + 1);
        PathPreviewClass QuickSave_PathPreview_Class = null;
        if (UserSource.Source_Card != null)
        {
            QuickSave_PathPreview_Class = 
                UserSource.Source_Card._Card_BehaviorUnit_Script.
                Key_MoveCall("Shift", QuickSave_NowCoordinate_Class, QuickSave_Path_Class,
                UsingObject, HateTarget, Action, Time, Order);
        }
        else
        {
            QuickSave_PathPreview_Class =
                _Map_Manager._Map_MoveManager.MovePreview(
                    "Shift", "Normal", 0, QuickSave_Path_Class,
                    UserSource, TargetSource, UsingObject, 
                    HateTarget, Action, Time, Order);
        }
        Answer_Return_StringList.AddRange(QuickSave_PathPreview_Class.ScoreList);

        if (Action)
        {
            print("如果要AI更聰明要紀錄被位移後的位置");
            _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Set", "Normal",
                QuickSave_PathPreview_Class.FinalPath, null,
                UserSource, TargetSource, Time, Order, ForceEnd: true);
            _Map_Manager._Map_MoveManager.MoveCoroutineCaller("Next");
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_StringList;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Interactive

    #region - Delete -
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void DeleteSet()
    {
        //設定----------------------------------------------------------------------------------------------------
        //地圖總管設定
        _World_Manager._Object_Manager._Basic_SaveData_Class.ObjectListDataRemove(_Basic_Source_Class.SourceType, this);
        //刪除
        for (int a = 0; a < _IsometicSorting_Caller_ScriptList.Count; a++)
        {
            Destroy(_IsometicSorting_Caller_ScriptList[a].gameObject);
        }
        //物件
        switch (_Basic_Source_Class.SourceType)
        {
            case "Concept":
                Destroy(_Basic_Source_Class.Source_Concept.gameObject);
                break;
            case "Weapon":
                Destroy(_Basic_Source_Class.Source_Weapon.gameObject);
                break;
            case "Item":
                Destroy(_Basic_Source_Class.Source_Item.gameObject);
                break;

            case "Project":
            case "TimePos":
                Destroy(this.gameObject);
                break;
        }
        Destroy(this.gameObject);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Set

    #region Position
    #region - Get -
    //取得目標時間位置——————————————————————————————————————————————————————————————————————
    public Vector TimePosition(int Time, int Order)
    {
        //變數----------------------------------------------------------------------------------------------------
        //回傳值
        int QuickSave_Time_Int;
        int QuickSave_Order_Int;
        QuickSave_Time_Int = Time;
        QuickSave_Order_Int = Order;

        bool QuickSave_TimeEqual_Bool = false;
        //----------------------------------------------------------------------------------------------------

        //Time----------------------------------------------------------------------------------------------------
        if (!_Map_TimePosition_Dictionary.ContainsKey(Time))
        {
            List<int> QuickSave_TimeKey_IntList = new List<int>(_Map_TimePosition_Dictionary.Keys);
            QuickSave_TimeKey_IntList.Sort();
            for (int t = QuickSave_TimeKey_IntList.Count - 1; t >= 0; t--)
            {
                int QuickSave_Min_Int = QuickSave_TimeKey_IntList[t];
                if (QuickSave_Min_Int <= QuickSave_Time_Int)
                {
                    if (QuickSave_Min_Int == QuickSave_Time_Int)
                    {
                        QuickSave_TimeEqual_Bool = true;
                    }
                    Time = QuickSave_Min_Int;
                    break;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------

        //Order----------------------------------------------------------------------------------------------------
        if (_Map_TimePosition_Dictionary.ContainsKey(Time))
        {
            List<int> QuickSave_OrderKey_IntList = new List<int>(_Map_TimePosition_Dictionary[Time].Keys);
            QuickSave_OrderKey_IntList.Sort();
            if (QuickSave_TimeEqual_Bool)
            {
                //Time相當(判斷Order)
                if (!_Map_TimePosition_Dictionary[Time].ContainsKey(Order))
                {
                    for (int o = QuickSave_OrderKey_IntList.Count - 1; o >= 0; o--)
                    {
                        int QuickSave_Min_Int = QuickSave_OrderKey_IntList[o];
                        if (QuickSave_Min_Int <= QuickSave_Order_Int)
                        {
                            Order = QuickSave_Min_Int;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (QuickSave_OrderKey_IntList.Count - 1 < 0)
                {
                    print(_Basic_Key_String + ":" + "<0");
                }
                //Time為超過(Order為最末端)
                Order = QuickSave_OrderKey_IntList[QuickSave_OrderKey_IntList.Count - 1];
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Map_TimePosition_Dictionary.TryGetValue(Time, out Dictionary<int, Vector> TimeValue))
        {
            if (TimeValue.TryGetValue(Order, out Vector OrderValue))
            {
                return OrderValue;
            }
        }
        return null;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Add -
    //建立目標時間位置——————————————————————————————————————————————————————————————————————
    public void TimePositionAdd(int Time, int Order, Vector Position)
    {
        //----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = new List<_Map_BattleObjectUnit>();
        SourceClass QuickSave_UseSource_Class = _Basic_Source_Class;
        if (QuickSave_UseSource_Class.SourceType == "Concept")
        {
            QuickSave_Objects_ScriptsList.AddRange(
                QuickSave_UseSource_Class.Source_Creature.
                _Object_Inventory_Script._Item_CarryObject_ScriptsList);
        }
        else
        {
            QuickSave_Objects_ScriptsList.Add(this);
        }
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
        {
            if (Object._Map_TimePosition_Dictionary.TryGetValue(Time, out Dictionary<int, Vector> TimeValue))
            {
                if (TimeValue.TryGetValue(Order, out Vector OrderValue))
                {
                    Object._Map_TimePosition_Dictionary[Time][Order] = Position;
                }
                else
                {
                    TimeValue.Add(Order, Position);
                }
            }
            else
            {
                Dictionary<int, Vector> QuickSave_OrderVector_Dictionary = new Dictionary<int, Vector>();
                QuickSave_OrderVector_Dictionary.Add(Order, Position);
                Object._Map_TimePosition_Dictionary.Add(Time, QuickSave_OrderVector_Dictionary);
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Remove -
    //移除目標時間位置——————————————————————————————————————————————————————————————————————
    public void TimePositionRemove(int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        List<_Map_BattleObjectUnit> QuickSave_Objects_ScriptsList = new List<_Map_BattleObjectUnit>();
        SourceClass QuickSave_UseSource_Class = _Basic_Source_Class;
        if (QuickSave_UseSource_Class.SourceType == "Concept")
        {
            QuickSave_Objects_ScriptsList.AddRange(
                QuickSave_UseSource_Class.Source_Creature.
                _Object_Inventory_Script._Item_CarryObject_ScriptsList);
        }
        else
        {
            QuickSave_Objects_ScriptsList.Add(this);
        }
        //----------------------------------------------------------------------------------------------------

        //設置----------------------------------------------------------------------------------------------------
        foreach (_Map_BattleObjectUnit Object in QuickSave_Objects_ScriptsList)
        {
            bool QuickSave_Remove_Bool = false;
            if (Object._Map_TimePosition_Dictionary.TryGetValue(Time, out Dictionary<int, Vector> TimeValue))
            {
                if (TimeValue.TryGetValue(Order, out Vector OrderValue))
                {
                    QuickSave_Remove_Bool = true;
                }
            }
            if (QuickSave_Remove_Bool)
            {
                Object._Map_TimePosition_Dictionary[Time].Remove(Order);
                if (Object._Map_TimePosition_Dictionary[Time].Count == 0)
                {
                    Object._Map_TimePosition_Dictionary.Remove(Time);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Position

    //場景物件用/如有需要可以再額外新增一個實體用來放置此
    #region KeyAction
    #region - Key_ObjectCollider -
    //可否接觸(於同一個區塊與高度)——————————————————————————————————————————————————————————————————————
    public bool Key_ObjectCollider(string Type/*通過方式 Pass:通過 Stay:停留*/,SourceClass EnterSource/*想進入者*/,
        int Time, int Order)
    {
        //設置----------------------------------------------------------------------------------------------------
        _Map_MoveManager _Map_MoveManager = _World_Manager._Map_Manager._Map_MoveManager;
        bool QuickSave_EnterCheck_Bool = true;//進入者確認
        bool QuickSave_EnteredCheck_Bool = true;//被進入者確認
        //----------------------------------------------------------------------------------------------------

        //被進入者----------------------------------------------------------------------------------------------------\
        _Map_BattleObjectUnit QuickSave_Object_Script = EnterSource.Source_BattleObject;
        //進入者
        QuickSave_EnterCheck_Bool = bool.Parse(
            QuickSave_Object_Script.SituationCaller(
                "IsColliderEnterCheck", new List<string> { Type, QuickSave_EnterCheck_Bool.ToString() },
                EnterSource, _Basic_Source_Class, null,
                null, false, Time, Order)["BoolFalse"][0]);
        //被進入者
        QuickSave_EnteredCheck_Bool = bool.Parse(
            SituationCaller(
                "IsColliderEnteredCheck", new List<string> { Type, QuickSave_EnteredCheck_Bool.ToString() },
                EnterSource, _Basic_Source_Class, null,
                null, false, Time, Order)["BoolFalse"][0]);
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (QuickSave_EnterCheck_Bool && QuickSave_EnteredCheck_Bool)
        {
            switch (Type)
            {
                case "Pass":
                    {
                        _Map_MoveManager.QuickSave_PassObjects_ScriptsList.Add(this);//加入經過物件
                    }
                    break;
            }
            return true;//可以通過
        }
        else
        {
            switch (Type)
            {
                case "Pass":
                    {
                        _Map_MoveManager.QuickSave_HitTarget_Script = this;//命中物件
                    }
                    break;
            }
            return false;//不可通過
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - Key_Object -
    //使用/攜帶與否——————————————————————————————————————————————————————————————————————
    public bool Key_StateAction(
        string Type/*Reach:可以作為Using使用 Carry:攜帶(移動時回/黏到持有者上)*/, int DriveDistance/*使用距離*/,
        _Map_BattleObjectUnit UserObject/*申請者*/,
        int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            case "Reach":
                {
                    bool QuickSave_Check_Bool = false;//可觸碰者
                    //基本設定
                    switch (_Map_ObjectState_String)
                    {
                        case "Affiliation":
                            {
                                if (_Map_AffiliationOwner_Script._Basic_Object_Script == UserObject)//為使用者所有
                                {
                                    QuickSave_Check_Bool = true;
                                }
                            }
                            break;
                        case "NoAffiliation":
                            {
                                QuickSave_Check_Bool = true;//無人所屬自由使用
                            }
                            break;
                        case "Driving":
                            {
                                if (_Basic_DrivingOwner_Script == UserObject)//為使用者所有
                                {
                                    Vector QuickSave_UserCoor_Class =
                                        UserObject.TimePosition(Time, Order);
                                    Vector QuickSave_MyCoor_Class =
                                        TimePosition(Time, Order);

                                    if (Mathf.Abs(QuickSave_UserCoor_Class.x - QuickSave_MyCoor_Class.x) <= DriveDistance &&
                                        Mathf.Abs(QuickSave_UserCoor_Class.y - QuickSave_MyCoor_Class.y) <= DriveDistance)
                                    {
                                        QuickSave_Check_Bool = true;//範圍內自由使用
                                    }
                                }
                            }
                            break;
                    }
                    //特殊變動
                    List<string> QuickSave_Data_StringList =
                        new List<string> { 
                            /*驅動距離*/DriveDistance.ToString(), 
                            /*當前狀況*/QuickSave_Check_Bool.ToString() };
                    QuickSave_Check_Bool = bool.Parse(
                        SituationCaller(
                            "IsReachCheck", QuickSave_Data_StringList,
                            UserObject._Basic_Source_Class, _Basic_Source_Class, null,
                            null, false, Time, Order)["BoolFalse"][0]);
                    //回傳
                    return QuickSave_Check_Bool;
                }
                break;
            case "Carry":
                {
                    bool QuickSave_Check_Bool = false;//可觸碰者
                    //基本設定
                    Vector QuickSave_UserCoor_Class =
                        UserObject.TimePosition(Time, Order);
                    Vector QuickSave_MyCoor_Class =
                        TimePosition(Time, Order);
                    switch (_Map_ObjectState_String)
                    {
                        case "Affiliation":
                            {
                                if (_Map_AffiliationOwner_Script._Basic_Object_Script == UserObject)
                                {
                                    if (Mathf.Abs(QuickSave_UserCoor_Class.x - QuickSave_MyCoor_Class.x) <= 0 &&
                                        Mathf.Abs(QuickSave_UserCoor_Class.y - QuickSave_MyCoor_Class.y) <= 0)
                                    {
                                        QuickSave_Check_Bool = true;//攜帶於身上
                                    }
                                }
                            }
                            break;
                        case "Driving":
                            {
                                if (Mathf.Abs(QuickSave_UserCoor_Class.x - QuickSave_MyCoor_Class.x) <= 0 &&
                                    Mathf.Abs(QuickSave_UserCoor_Class.y - QuickSave_MyCoor_Class.y) <= 0)
                                {
                                    QuickSave_Check_Bool = true;//攜帶於身上
                                }
                            }
                            break;
                    }
                    //特殊變動
                    List<string> QuickSave_Data_StringList =
                        new List<string> { 
                            /*驅動距離*/DriveDistance.ToString(), 
                            /*當前狀況*/QuickSave_Check_Bool.ToString() };
                    QuickSave_Check_Bool = bool.Parse(
                        SituationCaller(
                            "IsCarryCheck", QuickSave_Data_StringList,
                            UserObject._Basic_Source_Class, _Basic_Source_Class, null,
                            null, false, Time, Order)["BoolFalse"][0]);
                    //回傳
                    return QuickSave_Check_Bool;
                }
                break;
        }
        return false;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion KeyAction

    #region Situation
    #region - SituationCall -    
    //——————————————————————————————————————————————————————————————————————
    public Dictionary<string, List<string>> SituationCaller(
        string SituationKey, List<string> Data,
        SourceClass UserSource, SourceClass TargetSource, _Map_BattleObjectUnit UsingObject,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)//UserSource動作來源
    {
        //防止迴圈----------------------------------------------------------------------------------------------------
        bool QuickSave_FirstSituation_Bool = false;
        bool QuickSave_Skip_Bool = false;
        //哈希碼
        string QuickSave_HashCode_String = "";
        QuickSave_HashCode_String += (17 * 31 + this.GetHashCode()).ToString();
        {
            QuickSave_HashCode_String += SituationKey;
            if (Data != null)
            {
                foreach (string DataUnit in Data)
                {
                    QuickSave_HashCode_String += SituationKey;
                }
            }
            QuickSave_HashCode_String += 
                (17 * 31 + (UserSource != null ? UserSource.GetHashCode() : 0)).ToString();
            QuickSave_HashCode_String += 
                (17 * 31 + (TargetSource != null ? TargetSource.GetHashCode() : 0)).ToString();
            QuickSave_HashCode_String += 
                (17 * 31 + (UsingObject != null ? UsingObject.GetHashCode() : 0)).ToString();
        }
        if (_World_Manager.System_SituationHashCode_StringList == null)//第一次開始搜尋
        {
            QuickSave_FirstSituation_Bool = true;
            _World_Manager.System_SituationHashCode_StringList = new List<string>();

            if (!Action)//NPC次數記錄
            {
                //TimeLimit存取
                foreach (TimesLimitClass TimeLimitUnit in
                    _World_Manager._Map_Manager._Map_BattleRound._Round_TimesLimits_ClassList)
                {
                    if (TimeLimitUnit != null)
                    {
                        TimeLimitUnit.TimesLimit_Save();
                    }
                }
            }
        }
        else
        {
            if (_World_Manager.System_SituationHashCode_StringList.Contains(QuickSave_HashCode_String))
            {
                QuickSave_Skip_Bool = true;//跳出
            }
            else
            {
                _World_Manager.System_SituationHashCode_StringList.Add(QuickSave_HashCode_String);//紀錄當前Situation
            }
        }
        //----------------------------------------------------------------------------------------------------

        //設定變數----------------------------------------------------------------------------------------------------
        _Object_CreatureUnit QuickSave_Creature_Script = _Basic_Source_Class.Source_Creature;
        Dictionary<string, List<string>> Answer_Return_Dictionary = new Dictionary<string, List<string>>
        {
            //每次翻譯
            {"Value",new List<string>{ "0" } },/*全變動數值(有輸入值)*/
            {"Key",new List<string>{ "" } },/*全變動Key(有輸入值)*/
            {"Bool",new List<string>{ "false" } },/*全變動Bool(有輸入值)*/
            {"BoolFalse",new List<string>{ "True" } },/*全變動Bool(有輸入值)-有false就變為false*/
            {"BoolTrue",new List<string>{ "False" } },/*全變動Bool(有輸入值)-有true就變為true*/
            //非必要每次翻譯
            {"ValueAdd",new List<string>{"0"} },/*數值增減*/
            {"ValueMultiply",new List<string>{"1"} },/*數值增減*/
            {"TagAdd",new List<string>{""} },/*Tag增減*/
            {"TagRemove",new List<string>{""} },/*Tag增減*/
        };
        //AI搜尋
        if (!Action)
        {
            //查找資料
            List<string> QuickSave_Key_StringList =
                new List<string>(_World_Manager._Object_Manager.
                _Data_CreatureAI_Dictionary["Creature_Immo_0"].Keys);
            foreach (string Key in QuickSave_Key_StringList)
            {
                Answer_Return_Dictionary.Add(Key, new List<string>());
            }
        }
        //基礎變數設定
        switch (SituationKey)
        {
            case "DamageBlock":
            case "DeadResist":
            case "Dead":
                Answer_Return_Dictionary["Key"][0] = (Data[0]/*傷害類型*/);
                Answer_Return_Dictionary["Value"][0] = (Data[1]/*傷害數值*/);
                break;
            case "CardsMove":
            case "KeyChange":
                Answer_Return_Dictionary["Key"][0] = (Data[0]/*原始Key*/);
                break;
            case "UseLicense":
                Answer_Return_Dictionary["Bool"][0] = ("true"/*原始Key*/);
                break;
            case "DealPriority":
                Answer_Return_Dictionary["Bool"][0] = ("false"/*原始Key*/);
                break;
            case "IsColliderEnterCheck":
            case "IsColliderEnteredCheck":
            case "IsReachCheck":
            case "IsCarryCheck":
                Answer_Return_Dictionary["BoolFalse"][0] = (Data[1]/*原始Key*/);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //情況互動----------------------------------------------------------------------------------------------------
        #region - Call -
        if (!QuickSave_Skip_Bool)
        {
            //執行次數
            int QuickSave_Times_Int = 1;
            switch (SituationKey)
            {
                case "Operate":
                case "DeadResist":
                    {
                        string QuickSave_Times_String = SituationKey + "Times";

                        //額外次數增加
                        float QuickSave_AdvanceAdd_Float = 0;
                        float QuickSave_AdvanceMultiply_Float = 1;
                        List<string> QuickSave_Data_StringList =
                            new List<string>(Data);
                        QuickSave_Data_StringList.Insert(0, QuickSave_Times_String);

                        Dictionary<string, List<string>> QuickSave_AdvanceAdd_Dictionary =
                            SituationCaller("GetStatusValueAdd", QuickSave_Data_StringList,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                        QuickSave_AdvanceAdd_Float +=
                            int.Parse(QuickSave_AdvanceAdd_Dictionary["ValueAdd"][0]);
                        Dictionary<string, List<string>> QuickSave_AdvanceMultiply_Dictionary =
                            SituationCaller("GetStatusValueMultiply", QuickSave_Data_StringList,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                        QuickSave_AdvanceMultiply_Float *=
                            int.Parse(QuickSave_AdvanceMultiply_Dictionary["ValueMultiply"][0]);
                        QuickSave_Times_Int =
                            Mathf.RoundToInt(
                                Mathf.Clamp((QuickSave_Times_Int + QuickSave_AdvanceAdd_Float) *
                                QuickSave_AdvanceMultiply_Float, 0, 99));
                    }
                    break;
            }
            //執行
            for (int t = 0; t < QuickSave_Times_Int; t++)
            {
                List<_Map_BattleObjectUnit> QS_CalledObjects_ScriptsList = new List<_Map_BattleObjectUnit>();
                //當前地塊
                if (_World_Manager._Authority_Scene_String == "Battle")
                {
                    switch (_Basic_Source_Class.SourceType)
                    {
                        default:
                            _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
                            Vector QuickSave_NowPos_Class =
                                TimePosition(_Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
                            if (QuickSave_NowPos_Class == null)
                            {
                                break;
                            }
                            _Map_BattleGroundUnit QuickSave_Ground_Script =
                                _Map_Manager._Map_BattleCreator._Map_GroundBoard_ScriptsArray[QuickSave_NowPos_Class.X, QuickSave_NowPos_Class.Y];
                            if (QuickSave_Ground_Script != null)
                            {
                                Answer_Return_Dictionary =
                                    QuickSave_Ground_Script._Basic_Object_Script.
                                    Situation(Answer_Return_Dictionary, SituationKey, Data,
                                    UserSource, TargetSource, UsingObject,
                                    HateTarget, Action, Time, Order);
                                QS_CalledObjects_ScriptsList.Add(QuickSave_Ground_Script._Basic_Object_Script);
                            }
                            break;
                    }
                }
                if (QuickSave_Creature_Script != null)
                {
                    _Item_Object_Inventory QuickSave_Inventory_Script =
                        QuickSave_Creature_Script._Object_Inventory_Script;
                    //概念
                    if (QuickSave_Inventory_Script._Item_EquipConcepts_Script != null)
                    {
                        _Map_BattleObjectUnit QuickSave_Object_Script =
                            QuickSave_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
                        //卡片/變異
                        Answer_Return_Dictionary =
                            QuickSave_Inventory_Script._Item_EquipConcepts_Script.
                            Situation(Answer_Return_Dictionary, SituationKey, Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        //自身
                        Answer_Return_Dictionary =
                            QuickSave_Object_Script.
                            Situation(Answer_Return_Dictionary, SituationKey, Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QS_CalledObjects_ScriptsList.Add(QuickSave_Object_Script);

                    }
                    //武器
                    foreach (_Item_WeaponUnit Weapon in QuickSave_Inventory_Script._Item_EquipWeapons_ScriptsList)
                    {
                        Answer_Return_Dictionary = 
                            Weapon._Basic_Object_Script.
                            Situation(Answer_Return_Dictionary, SituationKey, Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QS_CalledObjects_ScriptsList.Add(Weapon._Basic_Object_Script);
                    }
                    //道具
                    foreach (_Item_ItemUnit Item in QuickSave_Inventory_Script._Item_EquipItems_ScriptsList)
                    {
                        Answer_Return_Dictionary =
                            Item._Basic_Object_Script.
                            Situation(Answer_Return_Dictionary, SituationKey, Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                        QS_CalledObjects_ScriptsList.Add(Item._Basic_Object_Script);
                    }
                    //物體效果/非概念/武器/道具的驅動物
                    foreach (_Map_BattleObjectUnit DrivingObject in QuickSave_Inventory_Script._Item_DrivingObject_ScriptsList)
                    {
                        if (!QS_CalledObjects_ScriptsList.Contains(DrivingObject))
                        {
                            Answer_Return_Dictionary =
                                DrivingObject.
                                Situation(Answer_Return_Dictionary, SituationKey, Data,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order);
                            QS_CalledObjects_ScriptsList.Add(DrivingObject);
                        }
                    }
                    if (!QS_CalledObjects_ScriptsList.Contains(this))//自身未作用
                    {
                        Answer_Return_Dictionary =
                            Situation(Answer_Return_Dictionary, SituationKey, Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order);
                    }
                }
                else
                {
                    Answer_Return_Dictionary =
                        Situation(Answer_Return_Dictionary, SituationKey, Data,
                        UserSource, TargetSource, UsingObject,
                        HateTarget, Action, Time, Order);
                }
            }
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //綜合調整----------------------------------------------------------------------------------------------------
        #region - ValueSet -
        /*
         * Value,Key,Bool,
         * ValueAdd,ValueMultiply,
         * TagAdd,TagRemove
         */
        Dictionary<string, List<string>> QuickSave_Change_Dictionary = new Dictionary<string, List<string>>();
        foreach (string Key in Answer_Return_Dictionary.Keys)
        {
            switch (Key)
            {
                case "Value":
                case "Key":
                case "Bool":
                    {
                        if (Answer_Return_Dictionary[Key].Count > 0)
                        {
                            QuickSave_Change_Dictionary.Add(Key,
                                Answer_Return_Dictionary[Key].GetRange(Answer_Return_Dictionary[Key].Count - 1, 1));
                        }
                    }
                    break;
                case "ValueAdd":
                    {
                        float QuickSave_Count_Float = 0;
                        foreach (string KeyInKey in Answer_Return_Dictionary[Key])
                        {
                            QuickSave_Count_Float += float.Parse(KeyInKey);
                        }
                        QuickSave_Change_Dictionary.Add(Key, new List<string> { QuickSave_Count_Float.ToString() });
                    }
                    break;
                case "ValueMultiply":
                    {
                        float QuickSave_Count_Float = 1;
                        foreach (string KeyInKey in Answer_Return_Dictionary[Key])
                        {
                            QuickSave_Count_Float *= float.Parse(KeyInKey);
                        }
                        QuickSave_Change_Dictionary.Add(Key, new List<string> { QuickSave_Count_Float.ToString() });
                    }
                    break;
                case "TagAdd":
                case "TagRemove":
                    {
                        List<string> QuickSave_Count_StringList = new List<string>();
                        foreach (string KeyInKey in Answer_Return_Dictionary[Key])
                        {
                            if (!QuickSave_Count_StringList.Contains(KeyInKey))
                            {
                                QuickSave_Count_StringList.Add(KeyInKey);
                            }
                        }
                        QuickSave_Change_Dictionary.Add(Key, new List<string>(QuickSave_Count_StringList));
                    }
                    break;
            }
        }
        foreach (string Key in QuickSave_Change_Dictionary.Keys)
        {
            Answer_Return_Dictionary[Key] = QuickSave_Change_Dictionary[Key];
        }
        #endregion
        //----------------------------------------------------------------------------------------------------

        //防止迴圈----------------------------------------------------------------------------------------------------
        if (QuickSave_FirstSituation_Bool)
        {
            _World_Manager.System_SituationHashCode_StringList = null;//還原
            if (!Action)//NPC次數還原
            {
                _Map_BattleRound _Map_BattleRound = _World_Manager._Map_Manager._Map_BattleRound;
                //TimeLimit存取
                foreach (TimesLimitClass TimeLimitUnit in _Map_BattleRound._Round_TimesLimits_ClassList)
                {
                    if (TimeLimitUnit != null)
                    {
                        TimeLimitUnit.TimesLimit_Load();
                    }
                }
                _Map_BattleRound._Round_TimesLimits_ClassList.RemoveAll(item => item == null);
            }
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - SituationCall -
    //——————————————————————————————————————————————————————————————————————
    public Dictionary<string, List<string>> Situation(
        Dictionary<string, List<string>> NowAnswer, string Situation, List<string> Data,
        SourceClass UserSource/*來源發動處*/, SourceClass TargetSource/*發動對象*/, _Map_BattleObjectUnit UsingObject/*發動物(不等於發動處(EX:Card))*/,
        _Map_BattleObjectUnit HateTarget, bool Action, int Time, int Order)
    {
        //----------------------------------------------------------------------------------------------------
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        Dictionary<string, List<string>> Answer_Return_Dictionary = NowAnswer;
        List<string> QuickSave_ReturnValue_StringList = new List<string>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Operate":
                {
                    //自身
                    _Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                    if (bool.Parse(Data[0]))
                    {
                        _Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                    }
                    //效果
                    foreach (_Effect_EffectObjectUnit Effect in _Effect_Effect_Dictionary.Values)
                    {
                        Effect._Basic_TimesLimit_Class.TimesLimit_Reset("Round");
                        if (bool.Parse(Data[0]))
                        {
                            Effect._Basic_TimesLimit_Class.TimesLimit_Reset("Standby");
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        #region - EffectObject -
        //避免效果反覆呼叫
        List<_Effect_EffectObjectUnit> QuickSave_EffectObject_ScriptsList = new List<_Effect_EffectObjectUnit>();
        QuickSave_EffectObject_ScriptsList.AddRange(_Effect_SpecialAffix_ScriptsList);
        QuickSave_EffectObject_ScriptsList.AddRange(_Effect_Passive_Dictionary.Values);
        QuickSave_EffectObject_ScriptsList.AddRange(_Effect_Effect_Dictionary.Values);
        foreach (_Effect_EffectObjectUnit Value in QuickSave_EffectObject_ScriptsList)
        {
            //執行
            switch (Situation)
            {
                #region - Scene -
                case "OwnStart":
                    Value.Key_Effect_OwnStart();
                    break;
                case "OwnEnd":
                    Value.Key_Effect_OwnEnd();
                    break;
                case "FieldStart":
                    Value.Key_Effect_FieldStart();
                    break;
                case "FieldEnd":
                    Value.Key_Effect_FieldEnd();
                    break;
                case "BattleStart":
                    Value.Key_Effect_BattleStart();
                    break;
                case "BattleEnd":
                    Value.Key_Effect_BattleEnd();
                    break;
                #endregion

                #region - Value -
                //基本數值
                case "GetStatusValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStatusValueAdd(
                            Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetStatusValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStatusValueMultiply(
                            Data,
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //行為附魔
                case "GetEnchanceValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEnchanceValueAdd(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetEnchanceValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEnchanceValueMultiply(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //效果數值
                case "GetEffectValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEffectValueAdd(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetEffectValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetEffectValueMultiply(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "LostEffectValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_LostEffectValueAdd(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "LostEffectValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_LostEffectValueMultiply(
                            Data[0], Data[1],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //效果層數
                case "GetStackValueAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStackValueAdd(
                                Data[0], Data[1],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    break;
                case "GetStackValueMultiply":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetStackValueMultiply(
                                Data[0], Data[1],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    break;

                //碰撞檢測
                case "IsColliderEnterCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsColliderEnterCheck(
                            Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "IsColliderEnteredCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsColliderEnteredCheck(
                            Data[0], bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //使用/攜帶檢測
                case "IsReachCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsReachCheck(
                            int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "IsCarryCheck":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_IsCarryCheck(
                            int.Parse(Data[0]), bool.Parse(NowAnswer["BoolFalse"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //標籤
                case "GetTagAdd":
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetTagAdd(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;
                case "GetTagRemove":

                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_GetTagRemove(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                //Key數值變動
                case "KeyChange":
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_KeyChange(
                            Data[0],
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Action -
                case "Drive":
                    QuickSave_ReturnValue_StringList.AddRange(
                    Value.Key_Effect_Drive(
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;
                case "Abandon":
                    QuickSave_ReturnValue_StringList.AddRange(
                    Value.Key_Effect_Abandon(
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;

                case "CauseGetEffect"://使得到效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseGetEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;
                case "CauseLostEffect"://使失去效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_CauseLostEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;
                case "GetEffect"://得到效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_GetEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;
                case "LostEffect"://失去效果
                    QuickSave_ReturnValue_StringList.AddRange(Value.Key_Effect_LostEffect(
                        Data[0], Data[1], Mathf.RoundToInt(float.Parse(Data[2])), Mathf.RoundToInt(float.Parse(Data[3])),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;

                case "CardsMove"://卡片移動
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_CardsMove(
                                NowAnswer["Key"][0],
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Stage -
                case "Operate":
                    Value.Key_Effect_Operate(bool.Parse(Data[0]));
                    break;

                case "Skill":
                    Value.Key_Effect_Skill(UserSource, UsingObject);
                    break;

                case "React":
                    Value.Key_Effect_React(UserSource);
                    break;

                case "EventEnd":
                    {
                        Value.Key_Effect_EventEnd(UserSource);
                    }
                    break;
                #endregion

                #region - Card -
                case "DealPriority":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_DealPriority(
                                bool.Parse(NowAnswer["BoolTrue"][0]),
                                UserSource, TargetSource, 
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "AbandonResidue":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_AbandonResidue(
                                bool.Parse(NowAnswer["BoolTrue"][0]),
                                UserSource, TargetSource, 
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "UseLicense":
                    {
                        //使用許可/回報Bool
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_UseLicense(
                                bool.Parse(NowAnswer["BoolFalse"][0]), Data,
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                //有回傳值
                case "BehaviorMiss":
                    {
                        //攻擊落空
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_BehaviorMiss(
                                UserSource, TargetSource, UsingObject, 
                                HateTarget, Action, Time, Order));
                    }
                    break;

                case "BehaviorUseEnd":
                    {
                        QuickSave_ReturnValue_StringList.AddRange(
                            Value.Key_Effect_BehaviorUseEnd(
                                UserSource, TargetSource, UsingObject, 
                                HateTarget, Action, Time, Order));
                    }
                    break;
                #endregion

                #region - Damage -
                case "DamageValueAdd":
                    QuickSave_ReturnValue_StringList =
                        Value.Key_Effect_DamageValueAdd(
                            Data[0], float.Parse(Data[1]), 
                            UserSource, TargetSource, UsingObject, 
                            HateTarget, Action, Time, Order);
                    break;
                case "DamageValueMultiply":
                    QuickSave_ReturnValue_StringList =
                        Value.Key_Effect_DamageValueMultiply(
                            Data[0], float.Parse(Data[1]), 
                            UserSource, TargetSource, UsingObject, 
                            HateTarget, Action, Time, Order);
                    break;

                case "DamageBlock"://傷害格黨
                    QuickSave_ReturnValue_StringList.AddRange(
                        Value.Key_Effect_DamageBlock(
                            Data[0], float.Parse(Data[1]), float.Parse(NowAnswer["Value"][0]),
                            UserSource, TargetSource, UsingObject,
                            HateTarget, Action, Time, Order));
                    break;

                case "Damage"://造成的傷害可為負數
                    QuickSave_ReturnValue_StringList.AddRange(Value.
                        Key_Effect_Damage(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;
                case "Damaged"://受到的傷害可為負數
                    QuickSave_ReturnValue_StringList.AddRange(Value.
                        Key_Effect_Damaged(Data[0], float.Parse(Data[1]), bool.Parse(Data[2]),
                        UserSource, TargetSource, UsingObject, 
                        HateTarget, Action, Time, Order));
                    break;

                case "DeadResist"://許可死亡
                    {
                        QuickSave_ReturnValue_StringList.AddRange(Value.
                            Key_Effect_DeadResist(
                                bool.Parse(NowAnswer["BoolTrue"][0]), NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                case "Dead"://死亡效果
                    {
                        QuickSave_ReturnValue_StringList.AddRange(Value.
                            Key_Effect_Dead(
                                NowAnswer["Key"][0], float.Parse(NowAnswer["Value"][0]),
                                UserSource, TargetSource, UsingObject,
                                HateTarget, Action, Time, Order));
                    }
                    break;
                    #endregion
            }
        }
        foreach (string Return in QuickSave_ReturnValue_StringList)
        {
            string QuickSave_Key_String = Return.Split("｜"[0])[0];
            string QuickSave_Value_String = Return.Replace(QuickSave_Key_String + "｜", "");
            if (Answer_Return_Dictionary.TryGetValue(QuickSave_Key_String, out List<string> DicValue))
            {
                switch (QuickSave_Key_String)
                {
                    case "Value":
                    case "Key":
                    case "Bool":
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                        }
                        break;
                    case "BoolTrue":
                        {
                            if (QuickSave_Value_String == "True")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto EffectObjectOut;
                            }
                        }
                        break;
                    case "BoolFalse":
                        {
                            if (QuickSave_Value_String == "False")
                            {
                                Answer_Return_Dictionary[QuickSave_Key_String][0] = QuickSave_Value_String;
                                goto EffectObjectOut;
                            }
                        }
                        break;
                    default:
                        {
                            Answer_Return_Dictionary[QuickSave_Key_String].Add(QuickSave_Value_String);
                        }
                        break;
                }
            }
            else
            {
                //print(Return);
            }
        }
        QuickSave_ReturnValue_StringList.Clear();
        if (QuickSave_ReturnValue_StringList.Count == 0)
        {
            Answer_Return_Dictionary = NowAnswer;
        }
        EffectObjectOut:
        #endregion
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        switch (Situation)
        {
            case "Operate":
                {
                    List<string> QuickSave_EffectKeys_StringList =
                        new List<string>(_Effect_Effect_Dictionary.Keys);
                    for (int a = 0; a < QuickSave_EffectKeys_StringList.Count; a++)
                    {
                        _Effect_EffectObjectUnit Effect =
                            _Effect_Effect_Dictionary[QuickSave_EffectKeys_StringList[a]];
                        switch (Effect._Basic_Data_Class.Decay)
                        {
                            case "Once":
                                Effect.StackDecrease("Decay", 65535);
                                break;
                            case "Round":
                                Effect.RoundDecrease(1);
                                break;
                            case "Standby":
                                if (bool.Parse(Data[0]))
                                {
                                    Effect.RoundDecrease(1);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return Answer_Return_Dictionary;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion Situation
}
