using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class _Map_FieldGroundUnit : MonoBehaviour
{
    #region ElementBox
    //�l���󶰡X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    //��m��----------------------------------------------------------------------------------------------------
    //�Ϥ���V����m��
    public SpriteRenderer _GroundUnit_Ground_SpriteRenderer;
    public SpriteRenderer _GroundUnit_Part_SpriteRenderer;
    public Transform _GroundUnit_EventOffset_Transform;
    public List<SpriteRenderer> _GroundUnit_Events_SpriteRendererList;
    //��r���վ�
    public PolygonCollider2D _Map_MouseSencer_Collider;
    private int QuickSave_SoringOrder_Int = 0;
    //���
    public _Map_Manager.GroundDataClass _Basic_Data_Class;
    //----------------------------------------------------------------------------------------------------
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion ElementBox

    #region Start 
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void SystemStart(Vector MyCoordinate, 
        Vector3 ViewPos, int sortingOrder, _Map_Manager.GroundDataClass Data)
    {
        //----------------------------------------------------------------------------------------------------
        QuickSave_SoringOrder_Int = sortingOrder;
        _Basic_Data_Class = Data;
        //����W�ٳ]�w
        this.name =
            "Coordinate" + MyCoordinate.x + "," + MyCoordinate.y;
        //�����m�]�w
        gameObject.transform.localPosition = ViewPos;
        gameObject.transform.localScale = Vector3.one;
        //�Ϥ�
        Sprite QuickSave_Sprite_Sprite =
            _World_Manager._View_Manager.
            GetSprite("FieldGround", "Ground", Data.Region);
        _GroundUnit_Ground_SpriteRenderer.sprite =
            QuickSave_Sprite_Sprite;
        _GroundUnit_Ground_SpriteRenderer.sortingOrder = sortingOrder + 1;
        QuickSave_Sprite_Sprite =
            _World_Manager._View_Manager.
            GetSprite("FieldGround", "Part", Data.Region);
        _GroundUnit_Part_SpriteRenderer.sprite = QuickSave_Sprite_Sprite;
        _GroundUnit_Part_SpriteRenderer.sortingOrder = sortingOrder + 2;
        //----------------------------------------------------------------------------------------------------
    }
    public void EventViewSet(string EventType)
    {
        //----------------------------------------------------------------------------------------------------
        if (EventType != "Null")
        {
            _GroundUnit_EventOffset_Transform.gameObject.SetActive(true);
            _GroundUnit_Events_SpriteRendererList[0].sortingOrder = QuickSave_SoringOrder_Int + 10;
            _GroundUnit_Events_SpriteRendererList[1].sortingOrder = QuickSave_SoringOrder_Int + 11;
            _GroundUnit_Events_SpriteRendererList[1].sprite =
                _World_Manager._View_Manager.GetSprite("Event", "Icon", EventType);
        }
        else
        {
            _GroundUnit_EventOffset_Transform.gameObject.SetActive(false);
        }
        //----------------------------------------------------------------------------------------------------
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X

    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    public void ViewSet(float MyDistanceColor, bool Out)
    {
        Color QuickSave_MyColor_Color =
            Color.Lerp(Color.white, Camera.main.backgroundColor, MyDistanceColor);
        _GroundUnit_Ground_SpriteRenderer.color = QuickSave_MyColor_Color;
        _GroundUnit_Part_SpriteRenderer.color = QuickSave_MyColor_Color;
        _GroundUnit_Events_SpriteRendererList[0].color = QuickSave_MyColor_Color;
        _GroundUnit_Events_SpriteRendererList[1].color = QuickSave_MyColor_Color;
        /*
        if (!Out)
        {
        }
        else
        {
        }*/
    }
    //�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X�X
    #endregion Start
}
