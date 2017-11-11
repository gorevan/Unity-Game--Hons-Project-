using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShadowController : MonoBehaviour {

    public GameObject light;
    public Transform dropdownMenu;
	
	
	void Update () {
	
        if(dropdownMenu.GetComponent<Dropdown>().value == 0)
        {
            light.GetComponent<Light>().shadows = LightShadows.Soft;
        }

        if (dropdownMenu.GetComponent<Dropdown>().value == 1)
        {
            light.GetComponent<Light>().shadows = LightShadows.Hard;
        }

        if (dropdownMenu.GetComponent<Dropdown>().value == 2)
        {
            light.GetComponent<Light>().shadows = LightShadows.None;
        }


    }
}
