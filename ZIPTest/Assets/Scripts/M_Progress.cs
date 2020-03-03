using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Progress : MonoBehaviour
{
    public Image progressImage;

    private float value;

    public float Value { 
        get => value; 
        set
        {
            this.value = value;
            progressImage.fillAmount = value;
        }
    }
}
