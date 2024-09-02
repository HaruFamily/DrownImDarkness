using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_HintEffect : MonoBehaviour
{
    #region Element
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    //----------------------------------------------------------------------------------------------------
    public string _View_HintType_String;
    public Image _View_HintIcon_Image;

    private string _HintTypeBefore_String;
    //----------------------------------------------------------------------------------------------------
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion Element

    #region HintSet
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    public void HintSet(string Type, string KeyType)
    {
        //----------------------------------------------------------------------------------------------------
        _View_Manager _View_Manager = _World_Manager._View_Manager;
        _UI_Manager _UI_Manager = _World_Manager._UI_Manager;
        //----------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------
        _HintTypeBefore_String = _View_HintType_String;
        _View_HintType_String = Type;
        switch (Type)
        {
            case "null":

                break;
            case "New":
                if (!_UI_Manager._UI_Hint_Dictionary["New"][KeyType].Contains(this))
                {
                    _UI_Manager._UI_Hint_Dictionary["New"][KeyType].Add(this);
                }
                break;
            case "UnNew":
                _UI_Manager._UI_Hint_Dictionary["New"][KeyType].Remove(this);
                _View_HintType_String = "null";
                break;
            case "Using":
                _UI_Manager._UI_Hint_Dictionary["Using"][KeyType].Add(this);
                break;
            case "UnUsing":
                _UI_Manager._UI_Hint_Dictionary["Using"][KeyType].Remove(this);
                _View_HintType_String = "null";
                if (_UI_Manager._UI_Hint_Dictionary["Using"][KeyType].Contains(this))
                {
                    return;
                }
                break;
        }
        _View_HintIcon_Image.color = _View_Manager.GetColor("Hint",_View_HintType_String);
        //----------------------------------------------------------------------------------------------------
    }
    //¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X¡X
    #endregion HintSet
}
