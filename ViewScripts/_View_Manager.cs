using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class _View_Manager : MonoBehaviour
{
    #region Elementv
    #region - DataElement -
    //Color——————————————————————————————————————————————————————————————————————
    //輸入區----------------------------------------------------------------------------------------------------
    public List<ColorClass> _Color_CodeInput_ClassList;
    public List<ColorClass> _Color_StatusInput_ClassList;
    public List<ColorClass> _Color_HintInput_ClassList;
    public List<ColorClass> _Color_SectInput_ClassList;
    public List<ColorClass> _Color_RareInput_ClassList;
    public List<ColorClass> _Color_GroundSelectInput_ClassList;
    //----------------------------------------------------------------------------------------------------

    //索引區----------------------------------------------------------------------------------------------------
    private Dictionary<string, Color> _Color_Code_Dictionary = new Dictionary<string, Color>();
    private Dictionary<string, Color> _Color_Status_Dictionary = new Dictionary<string, Color>();
    private Dictionary<string, Color> _Color_Hint_Dictionary = new Dictionary<string, Color>();
    private Dictionary<string, Color> _Color_Sect_Dictionary = new Dictionary<string, Color>();
    private Dictionary<string, Color> _Color_Rare_Dictionary = new Dictionary<string, Color>();
    private Dictionary<string, Color> _Color_GroundSelect_Dictionary = new Dictionary<string, Color>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //Sprite——————————————————————————————————————————————————————————————————————
    //輸入區----------------------------------------------------------------------------------------------------
    //基本
    public List<AdvanceSpriteDataClass> _Sprite_BasicInput_ClassList;

    //營地
    public List<SpriteDataClass> _Sprite_CampBackGroundInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_CampMenuSpriteInput_ClassList;
    public List<AdvancePositionDataClass> _Data_CampMenuPositionInput_ClassList;

    //物件
    public List<AdvanceSpriteListDataClass> _Sprite_FieldGroundInput_ClassList;
    public List<AdvanceSpriteListDataClass> _Sprite_BattleGroundInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_EventInput_ClassList;

    //Object
    public List<AdvanceSpriteDataClass> _Sprite_CreatureInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ObjectInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ProjectInput_ClassList;
    public List<AdvancePositionDataClass> _Data_ProjectHeightOffsetInput_ClassList;

    //物品資料庫
    public List<AdvanceSpriteDataClass> _Sprite_WeaponInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ItemInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ConceptInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_MaterialInput_ClassList;

    public List<AdvanceSpriteDataClass> _Sprite_WeaponRecipeInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ItemRecipeInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ConceptRecipeInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_MaterialRecipeInput_ClassList;

    public List<SpriteDataClass> _Sprite_SpecialAffixInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_SyndromeInput_ClassList;

    //招式
    public List<AdvanceSpriteDataClass> _Sprite_PassiveInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_ExploreInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_BehaviorInput_ClassList;
    public List<AdvanceSpriteDataClass> _Sprite_EnchanceInput_ClassList;

    //效果
    public List<SpriteDataClass> _Sprite_EffectObjectInput_ClassList;
    public List<SpriteDataClass> _Sprite_EffectCardInput_ClassList;
    //標籤
    public List<AdvanceSpriteDataClass> _Sprite_IconInput_ClassList;
    //----------------------------------------------------------------------------------------------------

    //索引區----------------------------------------------------------------------------------------------------
    //基本
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Basic_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();

    //營地
    private Dictionary<string, Sprite> _Sprite_CampBackGround_Dictionary = new Dictionary<string, Sprite>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_CampMenuSprite_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Vector3>> _Data_CampMenuPosition_Dictionary = new Dictionary<string, Dictionary<string, Vector3>>();

    //地板
    private Dictionary<string, Dictionary<string, List<Sprite>>> _Sprite_FieldGround_Dictionary = new Dictionary<string, Dictionary<string, List<Sprite>>>();
    private Dictionary<string, Dictionary<string, List<Sprite>>> _Sprite_BattleGround_Dictionary = new Dictionary<string, Dictionary<string, List<Sprite>>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Event_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();

    //物體
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Creature_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Object_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Project_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Vector3>> _Data_ProjectHeightOffset_Dictionary = new Dictionary<string, Dictionary<string, Vector3>>();

    //道具
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Weapon_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Item_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Concept_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Material_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_WeaponRecipe_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_ItemRecipe_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_ConceptRecipe_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_MaterialRecipe_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Sprite> _Sprite_SpecialAffix_Dictionary = new Dictionary<string, Sprite>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Syndrome_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();

    //招式
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Passive_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Explore_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Behavior_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Enchance_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();

    //效果
    private Dictionary<string, Sprite> _Sprite_EffectObject_Dictionary = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> _Sprite_EffectCard_Dictionary = new Dictionary<string, Sprite>();

    //標籤
    private Dictionary<string, Dictionary<string, Sprite>> _Sprite_Icon_Dictionary = new Dictionary<string, Dictionary<string, Sprite>>();
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    //----------------------------------------------------------------------------------------------------
    public RectTransform _Bubble_TracerStore_Transform;
    public List<_View_BubbleUnit> _Bubble_Tracer_ScriptsList;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public GameObject _Anim_Number_GameObject;
    private int _Anim_NumberDelay_Int;
    //----------------------------------------------------------------------------------------------------
    //——————————————————————————————————————————————————————————————————————
    #endregion

    #region - ClassElement -
    //顏色輸入類別
    [System.Serializable]
    public class ColorClass
    {
        public string Key;
        public Color Color;
    }

    //名稱與圖片
    [System.Serializable]
    public class SpriteDataClass
    {
        public string Key;
        public Sprite Sprite;
    }
    [System.Serializable]
    public class AdvanceSpriteDataClass
    {
        public string Key;
        public List<SpriteDataClass> Class;
    }

    [System.Serializable]
    public class SpriteListDataClass
    {
        public string Key;
        public List<Sprite> Sprites;
    }
    [System.Serializable]
    public class AdvanceSpriteListDataClass
    {
        public string Key;
        public List<SpriteListDataClass> Class;
    }


    [System.Serializable]
    //名稱與座標
    public class PositionDataClass
    {
        public string Key;
        public Vector3 Position;
    }
    [System.Serializable]
    public class AdvancePositionDataClass
    {
        public string Key;
        public List<PositionDataClass> Class;
    }
    #endregion
    #endregion Element

    #region FirstBuild
    //第一行動——————————————————————————————————————————————————————————————————————
    void Awake()
    {
        //設定變數----------------------------------------------------------------------------------------------------
        _World_Manager._View_Manager = this;
        //----------------------------------------------------------------------------------------------------

        //建立資料----------------------------------------------------------------------------------------------------
        //顏色庫
        ColorSet();
        //圖片設定
        SpriteSet();

        TracerBubbleOff();
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion FirstBuild

    #region DataBaseSet
    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void ColorSet()
    {
        //數值----------------------------------------------------------------------------------------------------
        ColorInput(_Color_CodeInput_ClassList, _Color_Code_Dictionary);
        ColorInput(_Color_StatusInput_ClassList, _Color_Status_Dictionary);
        ColorInput(_Color_HintInput_ClassList, _Color_Hint_Dictionary);
        ColorInput(_Color_SectInput_ClassList, _Color_Sect_Dictionary);
        ColorInput(_Color_RareInput_ClassList, _Color_Rare_Dictionary);
        ColorInput(_Color_GroundSelectInput_ClassList, _Color_GroundSelect_Dictionary);
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //起始呼叫——————————————————————————————————————————————————————————————————————
    public void SpriteSet()
    {
        #region - Basic -
        //----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_BasicInput_ClassList, _Sprite_Basic_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion
        #region - Camp -
        //----------------------------------------------------------------------------------------------------
        SpriteInput(_Sprite_CampBackGroundInput_ClassList, _Sprite_CampBackGround_Dictionary);
        AdvanceSpriteInput(_Sprite_CampMenuSpriteInput_ClassList, _Sprite_CampMenuSprite_Dictionary);

        for (int a = 0; a < _Data_CampMenuPositionInput_ClassList.Count; a++)
        {
            AdvancePositionDataClass QuickSave_NowInput_Class = _Data_CampMenuPositionInput_ClassList[a];
            Dictionary<string, Vector3> QuickSave_Input_Dictionary = new Dictionary<string, Vector3>();
            for (int b = 0; b < QuickSave_NowInput_Class.Class.Count; b++)
            {
                PositionDataClass QuickSave_Input_Class = QuickSave_NowInput_Class.Class[b];
                QuickSave_Input_Dictionary.Add(QuickSave_Input_Class.Key, QuickSave_Input_Class.Position);
            }
            _Data_CampMenuPosition_Dictionary.Add(QuickSave_NowInput_Class.Key, QuickSave_Input_Dictionary);
        }
        _Data_CampMenuPositionInput_ClassList = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Ground -
        //----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_EventInput_ClassList, _Sprite_Event_Dictionary);
        AdvanceSpriteInput(_Sprite_FieldGroundInput_ClassList, _Sprite_FieldGround_Dictionary);
        AdvanceSpriteInput(_Sprite_BattleGroundInput_ClassList, _Sprite_BattleGround_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Object -
        //----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_CreatureInput_ClassList, _Sprite_Creature_Dictionary);
        AdvanceSpriteInput(_Sprite_ObjectInput_ClassList, _Sprite_Object_Dictionary);

        AdvanceSpriteInput(_Sprite_ProjectInput_ClassList, _Sprite_Project_Dictionary);
        for (int a = 0; a < _Data_ProjectHeightOffsetInput_ClassList.Count; a++)
        {
            AdvancePositionDataClass QuickSave_NowInput_Class = _Data_ProjectHeightOffsetInput_ClassList[a];
            Dictionary<string, Vector3> QuickSave_Input_Dictionary = new Dictionary<string, Vector3>();
            for (int b = 0; b < QuickSave_NowInput_Class.Class.Count; b++)
            {
                PositionDataClass QuickSave_Input_Class = QuickSave_NowInput_Class.Class[b];
                QuickSave_Input_Dictionary.Add(QuickSave_Input_Class.Key, QuickSave_Input_Class.Position);
            }
            _Data_ProjectHeightOffset_Dictionary.Add(QuickSave_NowInput_Class.Key, QuickSave_Input_Dictionary);
        }
        _Data_ProjectHeightOffsetInput_ClassList = null;
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Item -
        //武器----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_WeaponInput_ClassList, _Sprite_Weapon_Dictionary);
        AdvanceSpriteInput(_Sprite_ItemInput_ClassList, _Sprite_Item_Dictionary);
        AdvanceSpriteInput(_Sprite_ConceptInput_ClassList, _Sprite_Concept_Dictionary);
        AdvanceSpriteInput(_Sprite_MaterialInput_ClassList, _Sprite_Material_Dictionary);
        AdvanceSpriteInput(_Sprite_WeaponRecipeInput_ClassList, _Sprite_WeaponRecipe_Dictionary);
        AdvanceSpriteInput(_Sprite_ItemRecipeInput_ClassList, _Sprite_ItemRecipe_Dictionary);
        AdvanceSpriteInput(_Sprite_ConceptRecipeInput_ClassList, _Sprite_ConceptRecipe_Dictionary);
        AdvanceSpriteInput(_Sprite_MaterialRecipeInput_ClassList, _Sprite_MaterialRecipe_Dictionary);
        SpriteInput(_Sprite_SpecialAffixInput_ClassList, _Sprite_SpecialAffix_Dictionary);
        AdvanceSpriteInput(_Sprite_SyndromeInput_ClassList, _Sprite_Syndrome_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Skill - 
        //----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_PassiveInput_ClassList, _Sprite_Passive_Dictionary);
        AdvanceSpriteInput(_Sprite_ExploreInput_ClassList, _Sprite_Explore_Dictionary);
        AdvanceSpriteInput(_Sprite_BehaviorInput_ClassList, _Sprite_Behavior_Dictionary);
        AdvanceSpriteInput(_Sprite_EnchanceInput_ClassList, _Sprite_Enchance_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Effect - 
        //----------------------------------------------------------------------------------------------------
        SpriteInput(_Sprite_EffectObjectInput_ClassList, _Sprite_EffectObject_Dictionary);
        SpriteInput(_Sprite_EffectCardInput_ClassList, _Sprite_EffectCard_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion

        #region - Normal - 
        //----------------------------------------------------------------------------------------------------
        AdvanceSpriteInput(_Sprite_IconInput_ClassList, _Sprite_Icon_Dictionary);
        //----------------------------------------------------------------------------------------------------
        #endregion
    }
    //——————————————————————————————————————————————————————————————————————


    #region - QuickInput -
    //——————————————————————————————————————————————————————————————————————
    private void ColorInput(List<ColorClass> Input, Dictionary<string, Color> Output)
    {
        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Input.Count; a++)
        {
            ColorClass QuickSave_NowInput_Class = Input[a];
            Output.Add(QuickSave_NowInput_Class.Key, QuickSave_NowInput_Class.Color);
        }
        Input = null;
        //----------------------------------------------------------------------------------------------------     
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private void SpriteInput(List<SpriteDataClass> Input, Dictionary<string, Sprite> Output)
    {
        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Input.Count; a++)
        {
            SpriteDataClass QuickSave_NowInput_Class = Input[a];
            Output.Add(QuickSave_NowInput_Class.Key, QuickSave_NowInput_Class.Sprite);
        }
        Input = null;
        //----------------------------------------------------------------------------------------------------     
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private void AdvanceSpriteInput(List<AdvanceSpriteDataClass> Input,
        Dictionary<string, Dictionary<string, Sprite>> Output)
    {
        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Input.Count; a++)
        {
            AdvanceSpriteDataClass QuickSave_NowInput_Class = Input[a];
            Dictionary<string, Sprite> QuickSave_Input_Dictionary = new Dictionary<string, Sprite>();
            for (int b = 0; b < QuickSave_NowInput_Class.Class.Count; b++)
            {
                SpriteDataClass QuickSave_Input_Class = QuickSave_NowInput_Class.Class[b];
                QuickSave_Input_Dictionary.Add(QuickSave_Input_Class.Key, QuickSave_Input_Class.Sprite);
            }
            Output.Add(QuickSave_NowInput_Class.Key, QuickSave_Input_Dictionary);
        }
        Input = null;
        //----------------------------------------------------------------------------------------------------        
    }
    //——————————————————————————————————————————————————————————————————————
    
    //——————————————————————————————————————————————————————————————————————
    private void AdvanceSpriteInput(List<AdvanceSpriteListDataClass> Input,
        Dictionary<string, Dictionary<string, List<Sprite>>> Output)
    {
        //----------------------------------------------------------------------------------------------------
        for (int a = 0; a < Input.Count; a++)
        {
            AdvanceSpriteListDataClass QuickSave_NowInput_Class = Input[a];
            Dictionary<string, List<Sprite>> QuickSave_Input_Dictionary = 
                new Dictionary<string, List<Sprite>>();
            for (int b = 0; b < QuickSave_NowInput_Class.Class.Count; b++)
            {
                SpriteListDataClass QuickSave_Input_Class = QuickSave_NowInput_Class.Class[b];
                QuickSave_Input_Dictionary.Add(QuickSave_Input_Class.Key, new List<Sprite>(QuickSave_Input_Class.Sprites));
            }
            Output.Add(QuickSave_NowInput_Class.Key, QuickSave_Input_Dictionary);
        }
        Input = null;
        //----------------------------------------------------------------------------------------------------        
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion DataBaseSet


    #region GetData
    //——————————————————————————————————————————————————————————————————————
    public Color GetColor(string Type, string FirstKey, string SubKey = "")
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Color -
            case "Code":
                if (_Color_Code_Dictionary.TryGetValue(FirstKey, out Color Code))
                {
                    return Code;
                }
                return _Color_Code_Dictionary["null"];
            case "Status":
                if (_Color_Status_Dictionary.TryGetValue(FirstKey, out Color Status))
                {
                    return Status;
                }
                return _Color_Status_Dictionary["null"];
            case "Hint":
                if (_Color_Hint_Dictionary.TryGetValue(FirstKey, out Color Hint))
                {
                    return Hint;
                }
                return _Color_Hint_Dictionary["null"];
            case "Sect":
                if (_Color_Sect_Dictionary.TryGetValue(FirstKey, out Color Sect))
                {
                    return Sect;
                }
                return _Color_Sect_Dictionary["null"];
            case "Rare":
                if (_Color_Rare_Dictionary.TryGetValue(FirstKey, out Color Rare))
                {
                    return Rare;
                }
                return _Color_Rare_Dictionary["null"];
            case "GroundSelect":
                if (_Color_GroundSelect_Dictionary.TryGetValue(FirstKey, out Color GroundSelect))
                {
                    return GroundSelect;
                }
                else
                {
                    print(FirstKey);
                }
                return _Color_GroundSelect_Dictionary["null"];
            #endregion

            default:
                return Color.clear;
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    private Sprite SpriteSelect(Dictionary<string, Dictionary<string, List<Sprite>>> Dictionary, 
        string FirstKey, string SubKey)
    {
        if (Dictionary.TryGetValue(FirstKey, out Dictionary<string, List<Sprite>> Contain))
        {
            if (Contain.TryGetValue(SubKey, out List<Sprite> Value))
            {
                if (Value.Count > 0)
                {
                    return Value[Random.Range(0, Value.Count)];
                }
            }
            else if (Contain.TryGetValue("null", out List<Sprite> NullValue))
            {
                if (NullValue.Count > 0)
                {
                    return NullValue[Random.Range(0, NullValue.Count)];
                }
            }
        }
        return Dictionary["null"]["null"][0];
    }
    private Sprite SpriteSelect(Dictionary<string, Dictionary<string, Sprite>> Dictionary,string FirstKey, string SubKey)
    {
        if (Dictionary.TryGetValue(FirstKey, out Dictionary<string, Sprite> Contain))
        {
            if (Contain.TryGetValue(SubKey, out Sprite Value))
            {
                return Value;
            }
            else if (Contain.TryGetValue("null", out Sprite NullValue))
            {
                return NullValue;
            }
        }
        return Dictionary["null"]["null"];
    }
    private Sprite SpriteSelect(Dictionary<string, Sprite> Dictionary,string FirstKey)
    {
        if (Dictionary.TryGetValue(FirstKey, out Sprite Value))
        {
            return Value;
        }
        return Dictionary["null"];
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public Sprite GetSprite(string Type, string FirstKey, string SubKey = "",string Remove = "")
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Basic -
            case "Basic":
                return SpriteSelect(_Sprite_Basic_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Camp -
            case "CampBackGround":
                return SpriteSelect(_Sprite_CampBackGround_Dictionary, FirstKey);
            case "CampMenuSprite":
                return SpriteSelect(_Sprite_CampMenuSprite_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Field/Ground -
            case "Event":
                return SpriteSelect(_Sprite_Event_Dictionary, FirstKey, SubKey);
            case "FieldGround":
                return SpriteSelect(_Sprite_FieldGround_Dictionary, FirstKey, SubKey);
            case "BattleGround":
                return SpriteSelect(_Sprite_BattleGround_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Object -
            case "Creature":
                return SpriteSelect(_Sprite_Creature_Dictionary, FirstKey, SubKey);
            case "Object":
                return SpriteSelect(_Sprite_Object_Dictionary, FirstKey, SubKey);
            case "Project":
                return SpriteSelect(_Sprite_Project_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Item -
            case "Weapon":
                return SpriteSelect(_Sprite_Weapon_Dictionary, FirstKey, SubKey);
            case "Item":
                return SpriteSelect(_Sprite_Item_Dictionary, FirstKey, SubKey);
            case "Concept":
                return SpriteSelect(_Sprite_Concept_Dictionary, FirstKey, SubKey);
            case "Material":
                return SpriteSelect(_Sprite_Material_Dictionary, FirstKey, SubKey);
            case "WeaponRecipe":
                return SpriteSelect(_Sprite_WeaponRecipe_Dictionary, FirstKey, SubKey);
            case "ItemRecipe":
                return SpriteSelect(_Sprite_ItemRecipe_Dictionary, FirstKey, SubKey);
            case "ConceptRecipe":
                return SpriteSelect(_Sprite_ConceptRecipe_Dictionary, FirstKey, SubKey);
            case "MaterialRecipe":
                return SpriteSelect(_Sprite_MaterialRecipe_Dictionary, FirstKey, SubKey);
            case "SpecialAffix":
                return SpriteSelect(_Sprite_SpecialAffix_Dictionary, FirstKey);
            case "Syndrome":
                return SpriteSelect(_Sprite_Syndrome_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Skill -
            case "Passive":
                return SpriteSelect(_Sprite_Passive_Dictionary, FirstKey, SubKey);
            case "Explore":
                return SpriteSelect(_Sprite_Explore_Dictionary, FirstKey, SubKey);
            case "Behavior":
                return SpriteSelect(_Sprite_Behavior_Dictionary, FirstKey, SubKey);
            case "Enchance":
                return SpriteSelect(_Sprite_Enchance_Dictionary, FirstKey, SubKey);
            #endregion

            #region - Effect -
            case "EffectObject":
                return SpriteSelect(_Sprite_EffectObject_Dictionary, FirstKey);
            #endregion

            #region - Other -
            case "Icon":
                return SpriteSelect(_Sprite_Icon_Dictionary, FirstKey, SubKey);
            #endregion
            default:
                return _Sprite_Basic_Dictionary["null"]["null"];
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————

    //——————————————————————————————————————————————————————————————————————
    public Vector3 GetVector3(string Type, string FirstKey, string SubKey = "")
    {
        //----------------------------------------------------------------------------------------------------
        switch (Type)
        {
            #region - Camp -
            case "CampMenuPosition":
                if (_Data_CampMenuPosition_Dictionary.TryGetValue(FirstKey, out Dictionary<string, Vector3> CampMenuPosition))
                {
                    if (CampMenuPosition.TryGetValue(SubKey, out Vector3 Value))
                    {
                        return Value;
                    }
                }
                return _Data_CampMenuPosition_Dictionary["null"]["null"];
            #endregion
            default:
                return new Vector3();
        }
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion GetData

    #region ViewAnim
    #region - AnimNumber -
    //——————————————————————————————————————————————————————————————————————
    public IEnumerator AnimeNumber(string Key, int Number, Transform Store)
    {
        //----------------------------------------------------------------------------------------------------
        _Anim_NumberDelay_Int++;
        yield return new WaitForSeconds(0.1f + (0.15f * _Anim_NumberDelay_Int));
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _View_NumberAnim QuickSave_Anim_Script =
            Instantiate(_Anim_Number_GameObject, Store).GetComponent<_View_NumberAnim>();
        QuickSave_Anim_Script._View_Text_Text.text = Number.ToString();
        //
        if (Number > 0)
        {
            QuickSave_Anim_Script._View_Text_Text.color = GetColor("Status", Key.Replace("Point",""));
        }
        else
        {
            QuickSave_Anim_Script._View_Text_Text.color = GetColor("Status", Key.Replace("Point", ""));
        }
        QuickSave_Anim_Script._View_Anim_Animation.clip =
            QuickSave_Anim_Script._View_Anims_ClipList[Random.Range(0, QuickSave_Anim_Script._View_Anims_ClipList.Count)];
        QuickSave_Anim_Script._View_Anim_Animation.Play();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        yield return new WaitForSeconds(1f + (0.05f * _Anim_NumberDelay_Int));
        Destroy(QuickSave_Anim_Script.gameObject);
        _Anim_NumberDelay_Int--;
        //----------------------------------------------------------------------------------------------------
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion
    #endregion

    #region Tracer
    //——————————————————————————————————————————————————————————————————————
    public void TracerBubbleOn(SourceClass Source, string Key, LanguageClass LanguageClass)
    {
        _Bubble_TracerStore_Transform.gameObject.SetActive(true);
        switch (Source.SourceType)
        {
            default:
                {
                    _Bubble_Tracer_ScriptsList[0].gameObject.SetActive(true);
                    _Bubble_Tracer_ScriptsList[0].BubbleSet(Source, Key, LanguageClass);
                }
                break;
        }
    }
    //——————————————————————————————————————————————————————————————————————


    //——————————————————————————————————————————————————————————————————————
    public void TracerBubbleOff()
    {
        foreach (_View_BubbleUnit Bubble in _Bubble_Tracer_ScriptsList)
        {
            Bubble.gameObject.SetActive(false);
        }
        _Bubble_TracerStore_Transform.gameObject.SetActive(false);
    }
    //——————————————————————————————————————————————————————————————————————
    #endregion

    public IEnumerator EquipStatusPolygonAnimate(UIPolygon Polygon)
    {
        Polygon.enabled = true;
        yield return new WaitForSeconds(0.001f);
        Polygon.enabled = false;
        yield return new WaitForSeconds(0.001f);
        Polygon.enabled = true;
    }
}
