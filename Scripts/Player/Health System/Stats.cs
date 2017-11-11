using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int maxHealth = 100;

    public static Stats instance;

    public int _curHealth;
    public int curHealth
    {
        get
        {
            return _curHealth;
        }
        set
        {
            { _curHealth = Mathf.Clamp(value, 0, maxHealth);  }
        }
    }

    



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        curHealth = maxHealth;
    }
	
}
