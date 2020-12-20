using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlendingModeDropdown : MonoBehaviour
{

    public Dropdown blendingModeDropDown;

    public Material matVP;

    private int curEnum = 0;
    private string[] EnumTable = {"BLENDING_ENUM_SCREEN", "BLENDING_ENUM_DIFFERENCE", "BLENDING_ENUM_MULTIPLY", "BLENDING_ENUM_OVERWRITE"};
    void Start()
    {
        foreach (string blendMode in EnumTable)
        {
            matVP.DisableKeyword(blendMode);
        }
        matVP.EnableKeyword(EnumTable[curEnum]);
        matVP.SetFloat("BLENDING_ENUM", 0);
        blendingModeDropDown.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }

    public void ValueChangeCheck()
	{
        int newEnum = blendingModeDropDown.value;
        matVP.DisableKeyword(EnumTable[curEnum]);
        matVP.EnableKeyword(EnumTable[newEnum]); 
        matVP.SetFloat("BLENDING_ENUM", newEnum);  
        curEnum = newEnum;
	}
}
