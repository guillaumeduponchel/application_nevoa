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
        opacitySliderObject.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }

    public void ValueChangeCheck()
	{
		Debug.Log (opacitySliderObject.value);
        VPMaterial.SetFloat("OPACITY_VECTOR", opacitySliderObject.value);
	}
}