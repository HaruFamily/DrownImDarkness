using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _UI_Adventure : MonoBehaviour
{
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    //----------------------------------------------------------------------------------------------------
    [HideInInspector] public string Adventure_AdventureSave_String;
    public List<_UI_MapUnit> Adventure_MapUnit_ScriptsList = new List<_UI_MapUnit>();
    //----------------------------------------------------------------------------------------------------

    //----------------------------------------------------------------------------------------------------
    public Transform[] Adventure_SummaryTransforms;
    public Transform Adventure_MapStore_Transform;
    public GameObject Adventure_Unit_GameObject;
    //----------------------------------------------------------------------------------------------------
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void StartSet()
    {
        //----------------------------------------------------------------------------------------------------
        _Map_Manager _Map_Manager = _World_Manager._Map_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _UI_MapUnit QuickSave_MapUnit_Script = Instantiate(Adventure_Unit_GameObject, Adventure_MapStore_Transform).GetComponent<_UI_MapUnit>();

        QuickSave_MapUnit_Script._Basic_Key_String = "Map_Gap";
        QuickSave_MapUnit_Script._Basic_Language_Class = _Map_Manager._Language_Map_Dictionary["Map_Gap"];

        QuickSave_MapUnit_Script._View_Name_Text.text = QuickSave_MapUnit_Script._Basic_Language_Class.Name;
        QuickSave_MapUnit_Script._View_Weather_Text.text = _Map_Manager._Language_Weather_Dictionary["Normal"].Name;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        Adventure_AdventureSave_String = "Adventure_Map";
        Adventure_SummaryTransforms[0].gameObject.SetActive(true);
        for (int a = 1; a < Adventure_SummaryTransforms.Length; a++)
        {
            Adventure_SummaryTransforms[a].gameObject.SetActive(false);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void EndSet()
    {
        //----------------------------------------------------------------------------------------------------
        for (int a  = 0; a < Adventure_MapUnit_ScriptsList.Count; a++)
        {
            Destroy(Adventure_MapUnit_ScriptsList[a].gameObject);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
}
