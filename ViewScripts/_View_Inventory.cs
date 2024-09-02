using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _View_Inventory : MonoBehaviour
{
    //Collection----------------------------------------------------------------------------------------------------
    [HideInInspector] public string Inventory_InventorySave_String;
    public Transform[] Inventory_SummaryTransforms;
    public _UI_TextEffect Inventory_DefaultMenu;
    /*
     * 00¡GWeapons
     * 01¡GItems
     * 02¡GCollections
     * 03¡GMaterials
     */
    //----------------------------------------------------------------------------------------------------

    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void StartSet()
    {
        Inventory_InventorySave_String = "Inventory_Concepts";
        for (int a = 0; a < Inventory_SummaryTransforms.Length; a++)
        {
            if (Inventory_SummaryTransforms[a] != null)
            {
                Inventory_SummaryTransforms[a].gameObject.SetActive(false);
            }
        }
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
}
