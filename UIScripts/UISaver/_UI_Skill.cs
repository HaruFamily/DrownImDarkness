using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_Skill : MonoBehaviour
{
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //----------------------------------------------------------------------------------------------------
    public string Skill_SkillSave_String;
    public Transform[] Skill_SummaryTransforms;
    public Transform Skill_SelectSkillStore_Transform;
    /*
     * 0�GFaction
     * 1�GSkills
     */
    //----------------------------------------------------------------------------------------------------

    private _Skill_FactionUnit Skill_ViewingFaction_Script;
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X


    #region Start
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void StartSet()
    {
        Skill_SkillSave_String = "Skill_Faction";
        Skill_SummaryTransforms[0].gameObject.SetActive(true);
        for (int a = 0; a < Skill_SummaryTransforms.Length; a++)
        {
            Skill_SummaryTransforms[a].gameObject.SetActive(false);
        }
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start

    #region SkillSet
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void FactionSet(_Skill_FactionUnit Target)
    {
        //�ܼ�----------------------------------------------------------------------------------------------------
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //�y��----------------------------------------------------------------------------------------------------
        /*
        if (Skill_ViewingFaction_Script != null)
        {

            if (Skill_ViewingFaction_Script._Faction_PassiveData_Dictionary.Count > 0)
            {
                Skill_ViewingFaction_Script._Faction_PassiveStore_Transform.SetParent(Skill_ViewingFaction_Script._Faction_SkillStore_Transform);
                Target._Faction_PassiveStore_Transform.localPosition = Vector3.zero;
                Target._Faction_PassiveStore_Transform.localScale = Vector3.zero;
            }
            if (Skill_ViewingFaction_Script._Faction_ExploreData_Dictionary.Count > 0)
            {
                Skill_ViewingFaction_Script._Faction_ExploreStore_Transform.SetParent(Skill_ViewingFaction_Script._Faction_SkillStore_Transform);
                Target._Faction_ExploreStore_Transform.localPosition = Vector3.zero;
                Target._Faction_ExploreStore_Transform.localScale = Vector3.zero;
            }
            if (Skill_ViewingFaction_Script._Faction_BehaviorData_Dictionary.Count > 0)
            {
                Skill_ViewingFaction_Script._Faction_BehaviorStore_Transform.SetParent(Skill_ViewingFaction_Script._Faction_SkillStore_Transform);
                Target._Faction_BehaviorStore_Transform.localPosition = Vector3.zero;
                Target._Faction_BehaviorStore_Transform.localScale = Vector3.zero;
            }
            if (Skill_ViewingFaction_Script._Faction_EnchanceData_Dictionary.Count > 0)
            {
                Skill_ViewingFaction_Script._Faction_EnchanceStore_Transform.SetParent(Skill_ViewingFaction_Script._Faction_SkillStore_Transform);
                Target._Faction_EnchanceStore_Transform.localPosition = Vector3.zero;
                Target._Faction_EnchanceStore_Transform.localScale = Vector3.zero;
            }
        }
        Skill_ViewingFaction_Script = Target;

        //New�]�m
        if (_UI_Manager._UI_NewHint_Dictionary["Item"].Contains(Target._View_Hint_Script) &&
            !_UI_Manager._UI_Hint_Dictionary["Using"]["Item"].Contains(Target._View_Hint_Script))
        {
            Target._View_Hint_Script.HintSet("null", "Faction");
            Target._View_Hint_Script.HintSet("UnNew", "Faction");
        }

        if (Target._Faction_PassiveData_Dictionary.Count > 0)
        {
            Target._Faction_PassiveStore_Transform.SetParent(Skill_SelectSkillStore_Transform);
            Target._Faction_PassiveStore_Transform.localPosition = Vector3.zero;
            Target._Faction_PassiveStore_Transform.localScale = Vector3.one;
        }
        if (Target._Faction_ExploreData_Dictionary.Count > 0)
        {
            Target._Faction_ExploreStore_Transform.SetParent(Skill_SelectSkillStore_Transform);
            Target._Faction_ExploreStore_Transform.localPosition = Vector3.zero;
            Target._Faction_ExploreStore_Transform.localScale = Vector3.one;
        }
        if (Target._Faction_BehaviorData_Dictionary.Count > 0)
        {
            Target._Faction_BehaviorStore_Transform.SetParent(Skill_SelectSkillStore_Transform);
            Target._Faction_BehaviorStore_Transform.localPosition = Vector3.zero;
            Target._Faction_BehaviorStore_Transform.localScale = Vector3.one;
        }
        if (Target._Faction_EnchanceData_Dictionary.Count > 0)
        {
            Target._Faction_EnchanceStore_Transform.SetParent(Skill_SelectSkillStore_Transform);
            Target._Faction_EnchanceStore_Transform.localPosition = Vector3.zero;
            Target._Faction_EnchanceStore_Transform.localScale = Vector3.one;
        }


        _UI_Manager.UISet("Skill_Skills");
        _UI_Manager._UI_Camp_Class._View_TinyMenu.TinyMenuOut();*/
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion SkillSet
}
