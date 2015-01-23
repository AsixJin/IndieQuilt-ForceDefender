using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    private GameObject GameManager;
    private GameObject Clock;
    private GameObject Panel;
    private GameObject HpBar1;
    private GameObject HPBar2;
    private GameObject Force;
    private GameObject Anti;
    private IndieQuiltCommunicator Quilt;
    private GameMang Manager;
    private FD_Ctrl Defender1;
    private Anti_Ctrl DDefender;
    private Text timer;
    private Text winloss;
    private Text FD_HP;
    private Text AD_HP;

    private float display_time;
    private bool display_swtich = true;
    private bool first_switch = true;

	// Use this for initialization
	void Start () 
    {
        GameManager = GameObject.Find("_GameManager");
        Quilt = GameManager.GetComponent<IndieQuiltCommunicator>();
        Manager = GameManager.GetComponent<GameMang>();
        if(Quilt.difficulty != -1)
        {
            Clock = GameObject.Find("Timer");
            timer = Clock.GetComponent<Text>();
            Panel = GameObject.Find("Success?");
            winloss = Panel.GetComponent<Text>();

        }
        if(Application.loadedLevel == 3)
        {
            HpBar1 = GameObject.Find("FD_HP");
            FD_HP = HpBar1.GetComponent<Text>();
            HPBar2 = GameObject.Find("AD_HP");
            AD_HP = HPBar2.GetComponent<Text>();

            Force = GameObject.Find("Force Defender");
            Defender1 = Force.GetComponent<FD_Ctrl>();
            Anti = GameObject.Find("Anti Defender(Clone)");
            DDefender = Anti.GetComponent<Anti_Ctrl>();
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Application.loadedLevel >= 1)
        {
            if(Manager.SetDone == false)
            {
                float Timey = 32;
                Timey -= Manager.Timer;
                timer.text = Timey.ToString("F0");
            }
            
            if(Manager.SetDone)
            {
               
                if(first_switch && Quilt.success)
                {
                    winloss.text = "You Win";
                }
                else if(first_switch && Quilt.success == false)
                {
                    winloss.text = "You Lose";
                }

                display_time += Time.deltaTime;
                timer.text = "0";
                if(Quilt.success)
                {
                   
                    if(display_time >= 2 && display_swtich)
                    {
                        winloss.text = "You Win";
                        display_swtich = false;
                        display_time = 0;
                        first_switch = false;
                    }
                    if(display_time >= 2 && display_swtich == false)
                    {
                        winloss.text = "Press E to Continue";
                        display_swtich = true;
                        display_time = 0;
                    }

                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        Manager.SetDone = false;
                        Manager.Level10Win = false;
                        Manager.EnemyList.Clear();
                        Application.LoadLevel("Title");
                    }
                }
                else if(Quilt.success == false)
                {
                 
                    if (display_time >= 2 && display_swtich)
                    {
                        winloss.text = "You Lose";
                        display_swtich = false;
                        display_time = 0;
                        first_switch = false;
                    }
                    if (display_time >= 2 && display_swtich == false)
                    {
                        winloss.text = "Press E to Continue";
                        display_swtich = true;
                        display_time = 0;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Manager.SetDone = false;
                        Manager.EnemyList.Clear();
                        Application.LoadLevel("Title");
                    }
                }
            }
        }

        if(Application.loadedLevel == 3)
        {
            FD_HP.text = Defender1.hp.ToString();
            AD_HP.text = DDefender.hp.ToString();
        }
	}

    public void StartLevel(int lv)
    {
        Manager.FDA.SetTrigger("GameStart");
        Manager.Title.Stop();
        Quilt.difficulty = lv;
    }
}
