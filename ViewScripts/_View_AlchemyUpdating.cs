using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _View_AlchemyUpdating : MonoBehaviour
{
    public string _View_ProcessType_String;
    public RectTransform _View_Bar00_Transform;
    public RectTransform _View_Bar01_Transform;

    private float _View_TotalSpeed_Float = 0.5f;
    private float _View_UnitSpeed_Float = 1;
    private float _View_Acceleration_Float = 0;

    public float _View_Answer_Float;
    void Update()
    {
        _View_Acceleration_Float += Time.deltaTime;
        switch (_View_ProcessType_String)
        {
            case "Slash":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.24f;
                    float QuickSave_X_Float = 1 - (_View_Acceleration_Float * _View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3 (QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-108, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0.5f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    if (QuickSave_X_Float > 0.8f)
                    {
                        _View_Answer_Float = 2;
                    }
                    else
                    {
                        _View_Answer_Float = QuickSave_X_Float * 1.25f;
                    }
                    if (QuickSave_X_Float < 0)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Puncture":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.5f;
                    float QuickSave_X_Float = 1 - (_View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-108, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0.5f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    if (QuickSave_X_Float < 0.25f)
                    {
                        _View_Answer_Float = 2;
                    }
                    else
                    {
                        _View_Answer_Float = (QuickSave_X_Float * 1.25f) - 0.25f;
                    }
                    if (QuickSave_X_Float < 0)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Impact":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.4f;
                    float QuickSave_X_Float = (_View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-458, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    if (QuickSave_X_Float > 0.1f)
                    {
                        if (QuickSave_X_Float > 0.9f)
                        {
                            _View_Answer_Float = 2;
                        }
                        else
                        {
                            _View_Answer_Float = QuickSave_X_Float * 1.25f - 0.125f;
                        }
                    }
                    else
                    {
                        _View_Answer_Float = 2;
                    }

                    if (QuickSave_X_Float > 1)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Energy":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.2f;
                    float QuickSave_X_Float = (_View_Acceleration_Float * _View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-458, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    _View_Answer_Float = QuickSave_X_Float;
                    if (QuickSave_X_Float > 1)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Chaos":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.2f;
                    float QuickSave_X_Float = (_View_Acceleration_Float * _View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-108, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0.5f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    _View_Answer_Float = QuickSave_X_Float;
                    if (QuickSave_X_Float > 1)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Abstract":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.2f;
                    float QuickSave_X_Float = (_View_Acceleration_Float * _View_Acceleration_Float * _View_UnitSpeed_Float);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-458, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    if (QuickSave_X_Float > 0.4f)
                    {
                        if (QuickSave_X_Float > 0.5f)
                        {
                            if (QuickSave_X_Float > 0.9f)
                            {
                                _View_Answer_Float = 2;
                            }
                            else
                            {
                                _View_Answer_Float = QuickSave_X_Float * 1.25f - 0.125f;
                            }
                        }
                        else
                        {
                            _View_Answer_Float = 2;
                        }
                    }
                    else
                    {
                        _View_Answer_Float = QuickSave_X_Float * 1.25f;
                    }

                    if (QuickSave_X_Float > 1)
                    {
                        _View_Answer_Float = 2;
                        EndUpdate();
                    }
                }
                break;
            case "Stark":
                {
                    _View_UnitSpeed_Float = _View_TotalSpeed_Float * 0.5f;
                    if (_View_Acceleration_Float < _View_UnitSpeed_Float)
                    {
                        return;
                    }
                    _View_Acceleration_Float -= _View_UnitSpeed_Float;

                    float QuickSave_X_Float = Random.Range(0, 1f);
                    Vector3 QuickSave_X_Vector = new Vector3(QuickSave_X_Float, 1, 1);
                    _View_Bar00_Transform.localPosition = new Vector3(-458, -293, 0);
                    _View_Bar00_Transform.pivot = new Vector2(0f, 0.5f);
                    _View_Bar00_Transform.transform.localScale = QuickSave_X_Vector;
                    if (QuickSave_X_Float > 0.1f)
                    {
                        if (QuickSave_X_Float > 0.9f)
                        {
                            _View_Answer_Float = 2;
                        }
                        else
                        {
                            _View_Answer_Float = QuickSave_X_Float * 1.25f - 0.125f;
                        }
                    }
                    else
                    {
                        _View_Answer_Float = 2;
                    }
                }
                break;
        }
    }

    public void EndUpdate()
    {
        _View_Alchemy _View_Alchemy = _World_Manager._UI_Manager._UI_Camp_Class._View_Alchemy;
        _View_Alchemy.Alchemy_OnProcess_Script = null;
        _View_Alchemy.Alchemy_ProcessSet(
            _View_Alchemy._Alchemy_NowProcess_Int,
            _View_ProcessType_String,
            Mathf.RoundToInt(_View_Answer_Float * 100));
        Destroy(this);
    }
}
