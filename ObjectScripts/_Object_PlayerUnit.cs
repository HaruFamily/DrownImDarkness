using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static _Object_Manager;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class _Object_PlayerUnit : MonoBehaviour
{
    #region Element
    //變數集——————————————————————————————————————————————————————————————————————
    //父階層狀態
    public _Object_CreatureUnit _Basic_Owner_Script;
    [HideInInspector] public List<string> _Item_Recipes_StringList = new List<string>();
    //——————————————————————————————————————————————————————————————————————
    #endregion Element

    #region Start
    //起始設定——————————————————————————————————————————————————————————————————————
    public void SystemStart()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        //----------------------------------------------------------------------------------------------------

        //設定----------------------------------------------------------------------------------------------------
        //基本設定(編號、派系、能力值)
         _Object_Manager.CreatureStartSet(_Basic_Owner_Script , "Creature_Immo_0");
        //測試資料
        
        #region - Concept
        List<string> QuickSave_ConceptKey_StringList = new List<string> 
        { 
            "ConceptRecipe_StoneGuardian","ConceptRecipe_SentinelMaid","ConceptRecipe_SwarmBringer" 
        };
        foreach (string Key in QuickSave_ConceptKey_StringList)
        {
            CustomRecipeMakeClass QuickSave_Items_Class = new CustomRecipeMakeClass
            {
                Target = Key,
                MaterialKey = new List<string> { "Material_GMFCloth" },
                MaterialStatus = new List<string> { "10,10,10,10" },
                MaterialSpecialAffix = new List<string[]> { new string[4] { "", "", "", "" } },
                Process = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("Stark", 50) }
            };
            _Item_Manager.ConceptStartSet(_Basic_Owner_Script, QuickSave_Items_Class.Target, 1, false,
                CustomMake: QuickSave_Items_Class);
        }
        #endregion

        #region - Weapon
        List<string> QuickSave_WeaponKey_StringList = new List<string> 
        { 
            "WeaponRecipe_LanspidDagger","WeaponRecipe_FlashingTachi","WeaponRecipe_HeavySling",
            "WeaponRecipe_SpikeShooter","WeaponRecipe_WhitalBowBlade","WeaponRecipe_OvergrownBall",
            "WeaponRecipe_GatherCobble", "WeaponRecipe_OrganicGauntlets"
        };
        foreach (string Key in QuickSave_WeaponKey_StringList)
        {
            CustomRecipeMakeClass QuickSave_Items_Class = new CustomRecipeMakeClass
            {
                Target = Key,
                MaterialKey = new List<string> { "Material_GMFCloth" },
                MaterialStatus = new List<string> { "10,10,10,10" },
                MaterialSpecialAffix = new List<string[]> { new string[4] { "", "", "", "" } },
                Process = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("Stark", 50) }
            };
            _Item_Manager.WeaponStartSet(_Basic_Owner_Script, QuickSave_Items_Class.Target, 1, false,
                CustomMake: QuickSave_Items_Class);
        }
        #endregion
        
        #region - Item -
        List<string> QuickSave_ItemKey_StringList = new List<string> 
        {
            "ItemRecipe_MysteryCuisine","ItemRecipe_HerbDelight","ItemRecipe_GlosporeBeverage",
            "ItemRecipe_HerbalSphere","ItemRecipe_RepairPart","ItemRecipe_ScrapBullets",
            "ItemRecipe_ScrapArrows","ItemRecipe_BrokenRockDarts","ItemRecipe_ThermalBomb","ItemRecipe_HydroslashBomb"
        };
        foreach (string Key in QuickSave_ItemKey_StringList)
        {
            CustomRecipeMakeClass QuickSave_Items_Class = new CustomRecipeMakeClass
            {
                Target = Key,
                MaterialKey = new List<string> { "Material_GMFCloth" },
                MaterialStatus = new List<string> { "10,10,10,10" },
                MaterialSpecialAffix = new List<string[]> { new string[4] { "", "", "", "" } },
                Process = new List<KeyValuePair<string, int>> { new KeyValuePair<string, int>("Stark", 50) }
            };
            _Item_Manager.ItemStartSet(_Basic_Owner_Script, QuickSave_Items_Class.Target, 1, false,
                CustomMake: QuickSave_Items_Class);
        }
        #endregion
        
        #region - Material -
        List<string> QuickSave_MaterialKey_StringList = new List<string>
        {
            "Material_GlobularMossFluff"
        };
        foreach (string Key in QuickSave_MaterialKey_StringList)
        {
            _Item_Manager.MaterialStartSet(_Basic_Owner_Script, Key, 2, false);
        }
        //_World_Manager._UI_Manager._UI_Camp_Class._View_ItemInfo.ItemInfoShow();
        #endregion

        //配方
        _Item_Manager.RecipeStartSet("Concept", "ConceptRecipe_EternalLighthouse");
        _Item_Manager.RecipeStartSet("Weapon", "WeaponRecipe_MemoryPlushToy");
        _Item_Manager.RecipeStartSet("Item", "ItemRecipe_MysteryCuisine");
        _Item_Manager.RecipeStartSet("Item", "ItemRecipe_HerbDelight");
        _Item_Manager.RecipeStartSet("Item", "ItemRecipe_HerbalSphere");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_AmbiguousWood");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_AmbiguousString");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_AmbiguousRope");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_AmbiguousMetal");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_HerbFiber");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_Boucle");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_Hay");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_PreservedDriedFruits");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_Flannel");
        _Item_Manager.RecipeStartSet("Material", "MaterialRecipe_GMFCloth");
        //----------------------------------------------------------------------------------------------------

        //Language與Data檢查----------------------------------------------------------------------------------------------------
        #region - Check -
        /*
        _World_TextManager _World_TextManager = _World_Manager._World_GeneralManager._World_TextManager;
        _Skill_Manager _Skill_Manager = _World_Manager._Skill_Manager;
        print("Passive---------------------------");
        foreach (string Key in _Skill_Manager._Language_Passive_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Skill_Manager._Data_Passive_Dictionary[Key].Numbers,
                Source_KeysData = _Skill_Manager._Data_Passive_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Skill_Manager._Language_Passive_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        print("Explore---------------------------");
        foreach (string Key in _Skill_Manager._Language_Explore_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Skill_Manager._Data_Explore_Dictionary[Key].Numbers,
                Source_KeysData = _Skill_Manager._Data_Explore_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Skill_Manager._Language_Explore_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        print("Behavior---------------------------");
        foreach (string Key in _Skill_Manager._Language_Behavior_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Skill_Manager._Data_Behavior_Dictionary[Key].Numbers,
                Source_KeysData = _Skill_Manager._Data_Behavior_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Skill_Manager._Language_Behavior_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        print("Enchance---------------------------");
        foreach (string Key in _Skill_Manager._Language_Enchance_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Skill_Manager._Data_Enchance_Dictionary[Key].Numbers,
                Source_KeysData = _Skill_Manager._Data_Enchance_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Skill_Manager._Language_Enchance_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        //_Item_Manager _Item_Manager = _World_Manager._Item_Manager;
        print("Syndrome---------------------------");
        foreach (string Key in _Item_Manager._Language_Syndrome_Dictionary.Keys)
        {
            //print(Key);
            string[] QuickSave_Split_StringArray = Key.Split("_"[0]);
            string QuickSave_Split_String = QuickSave_Split_StringArray[0] + "_" + QuickSave_Split_StringArray[1] + "_0";
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Item_Manager._Data_Syndrome_Dictionary[QuickSave_Split_String].Numbers,
                Source_KeysData = _Item_Manager._Data_Syndrome_Dictionary[QuickSave_Split_String].Keys
            };
            string QuickSave_Summary_String =
                _Item_Manager._Language_Syndrome_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        print("SpecialAffix---------------------------");
        foreach (string Key in _Item_Manager._Language_SpecialAffix_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Item_Manager._Data_SpecialAffix_Dictionary[Key].Numbers,
                Source_KeysData = _Item_Manager._Data_SpecialAffix_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Item_Manager._Language_SpecialAffix_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        _Effect_Manager _Effect_Manager = _World_Manager._Effect_Manager;
        print("EffectObject---------------------------");
        foreach (string Key in _Effect_Manager._Language_EffectObject_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Effect_Manager._Data_EffectObject_Dictionary[Key].Numbers,
                Source_KeysData = _Effect_Manager._Data_EffectObject_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Effect_Manager._Language_EffectObject_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        print("EffectCard---------------------------");
        foreach (string Key in _Effect_Manager._Language_EffectCard_Dictionary.Keys)
        {
            //print(Key);
            SourceClass QuickSave_Source_Class = new SourceClass
            {
                Source_BattleObject = _Basic_Owner_Script._Basic_Object_Script,
                Source_NumbersData = _Effect_Manager._Data_EffectCard_Dictionary[Key].Numbers,
                Source_KeysData = _Effect_Manager._Data_EffectCard_Dictionary[Key].Keys
            };
            string QuickSave_Summary_String =
                _Effect_Manager._Language_EffectCard_Dictionary[Key].Summary;
            string QuickSave_Answer_String =
                    _World_TextManager.TextmeshProTranslater
                    ("VarietyTest", QuickSave_Summary_String, 0,
                    QuickSave_Source_Class, null, null,
                    0, 0);
        }
        */
        #endregion
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //原野起始呼叫——————————————————————————————————————————————————————————————————————
    public void FieldStart(Vector StartPos)
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        //----------------------------------------------------------------------------------------------------

        //變異----------------------------------------------------------------------------------------------------
        {
            _UI_EventManager _UI_EventManager = _World_Manager._UI_Manager._UI_EventManager;
            //變異池
            List<_Map_BattleObjectUnit> QuickSave_EquipObjects_ScriptsList =
                _Basic_Owner_Script._Object_Inventory_Script._Item_EquipQueue_ScriptsList;
            _UI_EventManager._Syndrome_EquipSyndromePool_StringList.Clear();
            foreach (_Map_BattleObjectUnit Object in QuickSave_EquipObjects_ScriptsList)
            {
                List<string> QuickSave_Syndrome_StringList =
                    Object._Skill_Faction_Script._Basic_Data_Class.Syndrome;
                _UI_EventManager._Syndrome_EquipSyndromePool_StringList.AddRange(QuickSave_Syndrome_StringList);
            }
            //新增基礎變異
            int QuickSave_StartSyndrome_Int = 
                _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script.Key_StartSyndrome();
            for (int a =0; a < QuickSave_StartSyndrome_Int; a++ )
            {
                _UI_EventManager.SyndromePoolSet("Add", "Random", null);
            }
        }
        //----------------------------------------------------------------------------------------------------


        //設定自身位置----------------------------------------------------------------------------------------------------

        //注視目標
        _Map_BattleObjectUnit QuickSave_ConceptObject_Script =
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script;
        SourceClass QuickSave_Source_Class = QuickSave_ConceptObject_Script._Basic_Source_Class;
        _Object_Manager.HoldToField(_Basic_Owner_Script);

        _World_Manager._UI_Manager._View_Battle.CreatureFocus(_Basic_Owner_Script);
        _World_Manager._UI_Manager._View_Battle.UsingObjectSet
            ("Target", Target:QuickSave_ConceptObject_Script);

        _Basic_Owner_Script._Basic_Object_Script.SituationCaller(
            "FieldStart",null, 
            QuickSave_Source_Class, QuickSave_Source_Class, null, 
            null, true, _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //設定座標
        _World_Manager._Map_Manager._Map_MoveManager.
            Spawn(
            StartPos,
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class,
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);

        _Basic_Owner_Script.BuildBack(0, true);

        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————


    //戰鬥起始呼叫——————————————————————————————————————————————————————————————————————
    public void BattleStart()
    {
        //捷徑----------------------------------------------------------------------------------------------------
        _Object_Manager _Object_Manager = _World_Manager._Object_Manager;
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        _Map_BattleCreator _Map_BattleCreator = _Map_Manager._Map_BattleCreator;

        SourceClass QuickSave_Source_Class = _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class;
        //----------------------------------------------------------------------------------------------------

        //招式與武器----------------------------------------------------------------------------------------------------
        _Object_Manager.FieldToBattle(_Basic_Owner_Script);
        //----------------------------------------------------------------------------------------------------

        //設定自身位置----------------------------------------------------------------------------------------------------
        //隨機取得號碼
        Vector QuickSave_StartCooridnate_Class = new Vector(
            Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.x),
            Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.y));
        _Map_BattleGroundUnit QuickSave_StartGround_Script =
            _Map_BattleCreator._Map_GroundBoard_ScriptsArray
            [QuickSave_StartCooridnate_Class.X, QuickSave_StartCooridnate_Class.Y];

        while (!_Map_Manager._Map_MapCheck_Bool(QuickSave_StartCooridnate_Class,
                _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(0),
                _Map_BattleCreator._Map_GroundBoard_ScriptsArray.GetLength(1)) ||
                !QuickSave_StartGround_Script.
                StayCheck("Stay", _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class, null,
                _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int))
        {
            QuickSave_StartCooridnate_Class = new Vector(
                Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.x),
                Random.Range(0, _Map_BattleCreator._Map_MapSize_Vector2.y));
            QuickSave_StartGround_Script =
                _Map_BattleCreator._Map_GroundBoard_ScriptsArray
                [QuickSave_StartCooridnate_Class.X, QuickSave_StartCooridnate_Class.Y];
        }
        //移至位置
        _World_Manager._Map_Manager._Map_MoveManager.
            Spawn(QuickSave_StartCooridnate_Class, 
            _Basic_Owner_Script._Basic_Object_Script._Basic_Source_Class, 
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //----------------------------------------------------------------------------------------------------

        //初始設置----------------------------------------------------------------------------------------------------
        _World_Manager._UI_Manager._View_Battle.FocusSet(
            _Basic_Owner_Script._Object_Inventory_Script._Item_EquipConcepts_Script._Basic_Object_Script);
        //時間單位
        _Basic_Owner_Script._Basic_Object_Script._Round_Unit_Class = _Map_Manager._Map_BattleRound.RoundCreatureAdd(_Basic_Owner_Script);

        _Basic_Owner_Script._Basic_Object_Script.SituationCaller(
            "BattleStart", null, 
            QuickSave_Source_Class, QuickSave_Source_Class, null,
            null, true,
            _Map_BattleRound._Round_Time_Int, _Map_BattleRound._Round_Order_Int);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion Start


}
