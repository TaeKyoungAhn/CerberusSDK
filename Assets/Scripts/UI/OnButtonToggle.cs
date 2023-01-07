using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnButtonToggle : MonoBehaviour
{
    public Image currentImage;

    public Sprite onImage;
    public Sprite offImage;

    public bool isOn = false;

    public void OneButtonToggle()
    {
        isOn = !isOn;

        if(!isOn)
        {
            currentImage.sprite = onImage;
            OnMethod();
        }
        else
        {
            currentImage.sprite = offImage;
            OffMethod();
        }
    }

    public void OnMethod()
    {
        Debug.Log("On Event");
    }

    public void OffMethod()
    {
        Debug.Log("Off Event");
    }

}
