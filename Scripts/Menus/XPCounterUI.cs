using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class XPCounterUI : MonoBehaviour {

    private Text xpText;
    PauseController pc;

	void Awake () {
        xpText = GetComponent<Text>();
        pc = GetComponent<PauseController>();
	}
	
	
	void Update () {
        xpText.text = "XP: " + PauseController.XPMoney.ToString();
	}
}
