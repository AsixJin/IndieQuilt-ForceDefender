using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonLock : MonoBehaviour {

    private GameObject GameManager;
    private GameMang Manager;
    private Button ThisBut;

    public int Level;

	// Use this for initialization
	void Start () 
    {
        GameManager = GameObject.Find("_GameManager");
        Manager = GameManager.GetComponent<GameMang>();
        ThisBut = this.GetComponent<Button>();
        ThisBut.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Level <= Manager.Level_Beaten)
        {
            ThisBut.enabled = true;
        }
	}
}
