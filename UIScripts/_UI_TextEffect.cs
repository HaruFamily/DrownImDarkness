using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_TextEffect : MonoBehaviour
{
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #region ElementBox
    //�򥻳]�w----------------------------------------------------------------------------------------------------
    //��r
    public Text _Effect_ChangedText_Text;
    //���
    public List<Image> _Effect_ChangedImage_Image = new List<Image>();
    //��w��r
    [HideInInspector] public bool _Effect_LockText_Bool = false;
    //----------------------------------------------------------------------------------------------------
    #endregion ElementBox
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�]�w�ƹL�ĪG�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
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
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
}
