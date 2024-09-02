using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _UI_BTN : MonoBehaviour
{
    public GameObject _BTN_MenuBTN_GameObject;
    public Image _BTN_MapSprite_Image;

    public void Awake()
    {
        _BTN_MapSprite_Image.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void ImageSet(Sprite sprite,Vector3 Position)
    {
        if (Position == Vector3.zero)
        {
            _BTN_MenuBTN_GameObject.SetActive(false);
        }
        else
        {
            _BTN_MenuBTN_GameObject.SetActive(true);
            _BTN_MapSprite_Image.sprite = sprite;
            _BTN_MapSprite_Image.SetNativeSize();
            _BTN_MapSprite_Image.transform.localPosition = Position;
        }
    }
}
