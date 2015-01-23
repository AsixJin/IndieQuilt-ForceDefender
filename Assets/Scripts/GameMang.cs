using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMang : MonoBehaviour {

    public static GameMang control; 
    public IndieQuiltCommunicator Quilt;
    public AudioSource Title;
    public AudioSource Battle;
    public AudioSource MidBoss;
    public AudioSource Final;
    public GUISkin skin;
    public List<GameObject> EnemyList = new List<GameObject>();
    public GameObject FD;
    public Animator FDA;
    public GameObject Xi;
    public GameObject Xia;
    public GameObject Xi_Def;
    public GameObject Anti;
    int Xi_Type;
    int Xia_Type;
    public int Level_Beaten = 0;
    public int EnemiesRemain = 0;
    public float SpawnTime = 0;
    public float Timer = 0;
    public bool MusicSwitch = true;
    public bool SetDone = false;
    public bool Level10Win = false;
    public bool Level10lose = false;

    void Awake()
    {
        Screen.SetResolution(850, 638, false);
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            DontDestroyOnLoad(Battle);
            DontDestroyOnLoad(MidBoss);
            DontDestroyOnLoad(Title);
            DontDestroyOnLoad(Final);
        }
        else if (control != this)
        {
            Destroy(this.gameObject);
        }
        
    }
   
    // Use this for initialization
    void Start()
    {
        Title.Play();
    }

    void OnLevelWasLoaded()
    {
        Level10lose = false;
        EnemyList.Clear();
        if (Quilt.difficulty == -1)
        {
            FD = GameObject.Find("FD");
            FDA = FD.GetComponent<Animator>();
            if (Battle.isPlaying)
            {
                Battle.Stop();
            }
            if (MidBoss.isPlaying)
            {
                MidBoss.Stop();
            }
            if (Final.isPlaying)
            {
                Final.Stop();
            }
            Title.Play();
        }
        if (Quilt.difficulty == 0)
        {
            Instantiate(Xi_Def);
        }
        if (Quilt.difficulty == 1)
        {
            Battle.Play();
            Instantiate(Xi);
        }
        if (Quilt.difficulty == 2)
        {
            Battle.Play();
            Instantiate(Xi);
            Instantiate(Xi);
            Instantiate(Xi);

        }
        if (Quilt.difficulty == 3)
        {
            Battle.Play();
            Instantiate(Xi);
            Instantiate(Xia);
            Instantiate(Xia);

        }
        if (Quilt.difficulty == 4)
        {

            Battle.Play();
            Instantiate(Xia);
            Instantiate(Xia);
            Instantiate(Xia);
            Instantiate(Xia);
        }
        if (Quilt.difficulty == 5)
        {
            MidBoss.Play();
            Instantiate(Xi_Def);
        }
        if (Quilt.difficulty == 6)
        {
            Battle.Play();
            Instantiate(Xi_Def);
            Instantiate(Xi);
            Instantiate(Xi);
        }
        if (Quilt.difficulty == 7)
        {
            Battle.Play();
            Instantiate(Xi_Def);
            Instantiate(Xia);
            Instantiate(Xia);
        }
        if (Quilt.difficulty == 8)
        {
            Battle.Play();
            Instantiate(Xi_Def);
            Instantiate(Xi);
            Instantiate(Xi);
            Instantiate(Xia);
            Instantiate(Xia);
        }
        if (Quilt.difficulty == 9)
        {
            MidBoss.Play();
            Instantiate(Xi_Def);
            if(SpawnTime >= 1)
            {
                Instantiate(Xi_Def);
            }
            
        }
        if (Quilt.difficulty == 10)
        {
            Final.Play();
            Instantiate(Anti);
        }
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        EnemiesRemain = EnemyList.Count;
        if(SetDone == false)
        {
            SpawnTime += Time.deltaTime;
        }

        //Debug Powers
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Debug Stats");
            Debug.Log("Enemies in Scene: " + EnemyList.Count);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(MusicSwitch)
            {
                MusicSwitch = false;
            }
            else if(MusicSwitch == false)
            {
                MusicSwitch = true;
            }
            MusicControl();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            Level_Beaten = 10;
            Debug.Log("All Levels Unlocked");
        }

        if (Quilt.difficulty != -1)
        {
            
            Timer += Time.deltaTime;
            
            if (Timer >= 32 || Level10lose)
            {
                Debug.Log("30 Seconds has passed! You lose!!");
                Quilt.difficulty = -1;
                Timer = 0;
                Quilt.success = false;
                SetDone = true;
                
            }
            if (Application.loadedLevel != 0 && Application.loadedLevel < 3 && EnemyList.Count == 0)
            {
                    Debug.Log("You've defeated all enemies! You win!");
                    if (Level_Beaten < Quilt.difficulty)
                    {
                        Level_Beaten = Quilt.difficulty;
                    }
                    Quilt.difficulty = -1;
                    Timer = 0;
                    Quilt.success = true;
                    SetDone = true;
            }
            if(Application.loadedLevel == 3 && Level10Win)
            {
                Debug.Log("You've defeated all enemies! You win!");
                if (Level_Beaten < Quilt.difficulty)
                {
                    Level_Beaten = Quilt.difficulty;
                }
                Quilt.difficulty = -1;
                Timer = 0;
                Quilt.success = true;
                SetDone = true;
            }
            


        }
        
        
	}

    
    public void StartLevel(int lv)
    {
        FDA.SetTrigger("GameStart");
        Title.Stop();
        Quilt.difficulty = lv;
    }

    void MusicControl()
    {
        if(MusicSwitch == false)
        {
            if (Title.isPlaying)
            {
                Title.volume = 0;
            }
            if(MidBoss.isPlaying)
            {
                MidBoss.volume = 0;
            }
            if(Battle.isPlaying)
            {
                Battle.volume = 0;
            }
        }
        else if (MusicSwitch)
        {
            if (Title.isPlaying)
            {
                Title.volume = 1;
            }
            if (MidBoss.isPlaying)
            {
                MidBoss.volume = 1;
            }
            if (Battle.isPlaying)
            {
                Battle.volume = 1;
            }
        }
        
        
    }
}
