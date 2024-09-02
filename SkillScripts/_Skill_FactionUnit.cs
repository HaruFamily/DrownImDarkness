using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Skill_FactionUnit : MonoBehaviour
{
    #region Element
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    public Text _View_Name_Text;

    public Transform _Faction_SkillStore_Transform;

    public _UI_HintEffect _View_Hint_Script;
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public string _Basic_Key_String;
    public SourceClass _Basic_Source_Class;
    //Data
    public Dictionary<string, List<_UI_Card_Unit>> _Faction_SkillLeaves_Dictionary = 
        new Dictionary<string, List<_UI_Card_Unit>>();//��e�d�P(�i�ⵥ)
    public Dictionary<string, List<_UI_Card_Unit>> _Faction_ExtendSkillLeaves_Dictionary = 
        new Dictionary<string, List<_UI_Card_Unit>>();//�B�~�d�P(�S���p�~�ϥ�)

    public List<_UI_Card_Unit> _Faction_AddenLeaves_ScriptsList = new List<_UI_Card_Unit>();
    public List<_UI_Card_Unit> _Faction_RemoveLeaves_ScriptsList = new List<_UI_Card_Unit>();

    public List<_UI_Card_Unit> _Faction_PreDeleteLeaves_ScriptsList = new List<_UI_Card_Unit>();
    public List<_UI_Card_Unit> _Faction_DeleteLeaves_ScriptsList = new List<_UI_Card_Unit>();
    //��r
    public _Skill_Manager.FactionDataClass _Basic_Data_Class;
    public LanguageClass _Basic_Language_Class;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Element

    #region Set
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ViewSet()
    {
        _View_Name_Text.text = _Basic_Language_Class.Name;
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Set

    #region  - CardsSet -
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void AddSkillLeaf(_UI_Card_Unit Card,string Position, bool NonNative = false)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Key_String = Card._Basic_Key_String;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Faction_SkillLeaves_Dictionary.TryGetValue(QuickSave_Key_String, out List<_UI_Card_Unit> Value))
        {
            Value.Add(Card);
        }
        else
        {
            _Faction_SkillLeaves_Dictionary.Add(QuickSave_Key_String, new List<_UI_Card_Unit>());
            _Faction_SkillLeaves_Dictionary[QuickSave_Key_String].Add(Card);
        }
        _Faction_AddenLeaves_ScriptsList.Add(Card);

        switch (Position)
        {
            case "Deck":
                Card._Basic_Source_Class.Source_Creature._Card_CardsDeck_ScriptList.Add(Card);
                break;
            case "Board":
                Card._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList.Add(Card);
                break;
            case "Delay":
                Card._Basic_Source_Class.Source_Creature._Card_CardsDelay_ScriptList.Add(Card);
                break;
            case "Cemetery":
                Card._Basic_Source_Class.Source_Creature._Card_CardsCemetery_ScriptList.Add(Card);
                break;
            case "Exiled":
                Card._Basic_Source_Class.Source_Creature._Card_CardsExiled_ScriptList.Add(Card);
                break;
        }

        if (NonNative)
        {
            _Faction_PreDeleteLeaves_ScriptsList.Add(Card);
        }
        //----------------------------------------------------------------------------------------------------
    }

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void RemoveSkillLeaf(_UI_Card_Unit Card)
    {
        //----------------------------------------------------------------------------------------------------
        string QuickSave_Key_String = Card._Basic_Key_String;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        if (_Faction_SkillLeaves_Dictionary.TryGetValue(QuickSave_Key_String, out List<_UI_Card_Unit> Value))
        {
            Value.Remove(Card);
            if(Value.Count <= 0)
            {
                _Faction_SkillLeaves_Dictionary.Remove(QuickSave_Key_String);
                _Faction_RemoveLeaves_ScriptsList.Add(Card);
            }
        }
        switch (Card._Card_NowPosition_String)
        {
            case "Deck":
                Card._Basic_Source_Class.Source_Creature._Card_CardsDeck_ScriptList.Remove(Card);
                break;
            case "Board":
                Card._Basic_Source_Class.Source_Creature._Card_CardsBoard_ScriptList.Remove(Card);
                break;
            case "Delay":
                Card._Basic_Source_Class.Source_Creature._Card_CardsDelay_ScriptList.Remove(Card);
                break;
            case "Cemetery":
                Card._Basic_Source_Class.Source_Creature._Card_CardsCemetery_ScriptList.Remove(Card);
                break;
            case "Exiled":
                Card._Basic_Source_Class.Source_Creature._Card_CardsExiled_ScriptList.Remove(Card);
                break;
        }
        //----------------------------------------------------------------------------------------------------

        //�D��ͥd��----------------------------------------------------------------------------------------------------
        if (_Faction_PreDeleteLeaves_ScriptsList.Contains(Card))
        {
            _Faction_DeleteLeaves_ScriptsList.Add(Card);
            _Faction_PreDeleteLeaves_ScriptsList.Remove(Card);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void DestroySkillLeaves()
    {
        _Faction_DeleteLeaves_ScriptsList.Clear();
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion 

    #region Math
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public List<_UI_Card_Unit> Cards()
    {
        //----------------------------------------------------------------------------------------------------
        List<_UI_Card_Unit> QuickSave_Cards_ScriptsList = new List<_UI_Card_Unit>();
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        foreach (string Key in _Faction_SkillLeaves_Dictionary.Keys)
        {
            QuickSave_Cards_ScriptsList.AddRange(_Faction_SkillLeaves_Dictionary[Key]);
        }
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        return QuickSave_Cards_ScriptsList;
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Remove

}
