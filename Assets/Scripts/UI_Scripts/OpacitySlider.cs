using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacitySlider : MonoBehaviour
{

    public Slider opacitySliderObject;
    public Material VPMaterial;
    
    void Start()
    {
        VPMaterial.SetFloat("OPACITY_VECTOR", 0);
        opacitySliderObject.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }

    public void ValueChangeCheck()
	{
        VPMaterial.SetFloat("OPACITY_VECTOR", opacitySliderObject.value);
	}
}