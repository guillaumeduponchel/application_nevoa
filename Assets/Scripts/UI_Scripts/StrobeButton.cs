using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrobeButton : MonoBehaviour
{
    public Button strobeButton;

    public Material matVP;
    private string[] EnumTable = {"STROBE_ENUM_ACTIVATED", "STROBE_ENUM_NORMAL"};

    private int state;
    void Start()
    {
        state = 0;
        matVP.DisableKeyword("STROBE_ENUM_ACTIVATED");
        matVP.EnableKeyword("STROBE_ENUM_NORMAL");
        matVP.SetFloat("STROBE_ENUM", 1);
        strobeButton.onClick.AddListener (delegate {onStrobeButtonClicked ();});
    }

    public void onStrobeButtonClicked()
	{
        if(state == 0)
        {
            state +=1;
            matVP.DisableKeyword("STROBE_ENUM_NORMAL");
            matVP.EnableKeyword("STROBE_ENUM_ACTIVATED");
            matVP.SetFloat("STROBE_ENUM", 0);
            float blinking_frequency = Mathf.Pow(2, state) * computeSineFrequencyFromMusicPlayed();
            matVP.SetFloat("BLINKING_STROBE_FREQUENCY", blinking_frequency);  
        }
        else if(state == 3)
        {
            state = 0;
            matVP.DisableKeyword("STROBE_ENUM_ACTIVATED");
            matVP.EnableKeyword("STROBE_ENUM_NORMAL");
            matVP.SetFloat("STROBE_ENUM", 1);
        }
        else
        {
            state += 1;
            float blinking_frequency = Mathf.Pow(2, state) * computeSineFrequencyFromMusicPlayed();
            matVP.SetFloat("BLINKING_STROBE_FREQUENCY", blinking_frequency);
        }
	}

    private float computeSineFrequencyFromMusicPlayed()
    {
        float bpm = 120;
        float frequency = bpm / 60;
        return frequency / 2; // See how it can be fixed when playing 120 bpm songs
    }
}
