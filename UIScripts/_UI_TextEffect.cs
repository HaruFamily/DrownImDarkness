using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_TextEffect : MonoBehaviour
{
    //子物件集——————————————————————————————————————————————————————————————————————
    #region ElementBox
    //基本設定----------------------------------------------------------------------------------------------------
    //文字
    public Text _Effect_ChangedText_Text;
    //邊框
    public List<Image> _Effect_ChangedImage_Image = new List<Image>();
    //鎖定文字
    [HideInInspector] public bool _Effect_LockText_Bool = false;
    //----------------------------------------------------------------------------------------------------
    #endregion ElementBox
    //——————————————————————————————————————————————————————————————————————

    //設定滑過效果——————————————————————————————————————————————————————————————————————
    #region Cover
    public void CoverOn()
    {
        //----------------------------------------------------------------------------------------------------
        if (!_Effect_LockText_Bool)
        {
            if (_Effect_ChangedText_Text != null)
            {
                _Effect_ChangedText_Text.material = _World_Manager._World_GeneralManager._UI_HDRText_Material;
            }
            for (int a= 0; a < _Effect_ChangedImage_Image.Count; a++ )
            {
                _Effect_ChangedImage_Image[a].material = _World_Manager._World_GeneralManager._UI_HDRSprite_Material;//
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    public void CoverOff()
    {
        //----------------------------------------------------------------------------------------------------
        if (!_Effect_LockText_Bool)
        {
            if (_Effect_ChangedText_Text != null)
            {
                _Effect_ChangedText_Text.material = null;
            }
            for (int a = 0; a < _Effect_ChangedImage_Image.Count; a++)
            {
                _Effect_ChangedImage_Image[a].material = null;
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
    #endregion Cover
    //——————————————————————————————————————————————————————————————————————
}
