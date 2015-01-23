using UnityEngine;
using System.Collections;

public class Anti_Ctrl : MonoBehaviour {

    public Vector3 position;
    public Vector3 firepos;
    public int level;
    public int hp;
    public int panel_x;
    public int panel_y;
    public bool fire = false;
    public int EnemyID = 0;
    public int steps = 0;
    public float timer = 0.0f;
    public GameObject blast;
    public GameObject XiDef; 
    public GameObject GManager;
    public IndieQuiltCommunicator Quilt;
    public GameMang Manager;
    public GameObject ForceDef;
    public FD_Ctrl ForceData;
    public Animator animate;

	// Use this for initialization
	void Start () 
    {
        ForceDef = GameObject.Find("Force Defender");
        ForceData = ForceDef.GetComponent<FD_Ctrl>();
        GManager = GameObject.Find("_GameManager");
        Quilt = GManager.GetComponent<IndieQuiltCommunicator>();
        Manager = GManager.GetComponent<GameMang>();
        //Manager.EnemyList.Add(this.gameObject);
        level = Quilt.difficulty;
        animate = this.GetComponent<Animator>();

        hp = 15;
        panel_x = 1;
        panel_y = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        position = transform.position;
        firepos = transform.position;
        firepos.x = firepos.x + 0.10f;

        //This part of the code denote the positioning based
        //on which panel you are standing on based off the y asix
        if (panel_y == 1)
        {
            position.y = -0.36f;
            position.z = -2.0f;
            transform.position = position;
        }
        else if (panel_y == 2)
        {
            position.y = 0.41f;
            position.z = -1.0f;
            transform.position = position;
        }
        else if (panel_y == 3)
        {
            position.y = 1.22f;
            position.z = 0.0f;
            transform.position = position;
        }

        //This part of the code denote the positioning based
        //on which panel you are standing on based off the x asix
        if (panel_x == 1)
        {
            position.x = 0.63f;
            transform.position = position;
        }
        else if (panel_x == 2)
        {
            position.x = 1.89f;
            transform.position = position;
        }
        else if (panel_x == 3)
        {
            position.x = 3.15f;
            transform.position = position;
        }
        //If HP is Zero then you are outta here
        if (hp == 0)
        {
            Kill();
        }

        if (hp != 0)
        {
            if (fire == false)
            {
                timer += Time.deltaTime;

                if (timer >= 0.2f && steps == 0)
                {
                    ChangePosY(2);
                    Instantiate(XiDef, transform.position, Quaternion.identity);
                    timer = 0;
                    steps = 1;
                }
                else if(timer >= 0.2f && steps == 1)
                {
                    ChangePosY(3);
                    Instantiate(XiDef, transform.position, Quaternion.identity);
                    timer = 0;
                    steps = 2;
                }
                else if(timer >= 0.2f && steps == 2)
                {
                    Instantiate(XiDef, transform.position, Quaternion.identity);
                    timer = 0;
                    steps = 3;
                    fire = true;
                }

                /*
                else if (timer >= 1 && steps == 3)
                {

                    ChangePos();
                    timer = 0;
                }
                 */
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    ChangePos();
                    animate.SetTrigger("Firing");
                    Instantiate(blast, firepos, Quaternion.identity);
                    timer = 0;
                }

                if(Manager.EnemyList.Count == 0)
                {
                    steps = 0;
                    fire = false;

                }
            }

        }
	}

    public void IntPos()
    {
            int poss_x = 2;
            int poss_y = 3;
            EnemyID = 1;
            bool TestMove = true;
            Baddie_Base EnemyVar;
            while (TestMove)
            {
                TestMove = false;
                foreach (GameObject enemy in Manager.EnemyList)
                {
                    EnemyVar = enemy.GetComponent<Baddie_Base>();
                    if (EnemyVar.panel_x == poss_x && EnemyVar.panel_y == poss_y)
                    {
                        poss_x = 2;
                        poss_y = 1;
                        //Mark this Xi Defender as Enemy 2 for coding purposes
                        EnemyID = 2;
                    }
                }
            }
            panel_x = poss_x;
            panel_y = poss_y;

        

    }

    public void ChangePos()
    {
        int poss_x;
        int poss_y;
        bool TestMove = true;
        Baddie_Base EnemyVar;
        poss_x = Random.Range(1, 4);
        poss_y = Random.Range(1, 4);
        while (TestMove)
        {
            TestMove = false;
            foreach (GameObject enemy in Manager.EnemyList)
            {
                EnemyVar = enemy.GetComponent<Baddie_Base>();
                if (EnemyVar.panel_x == poss_x && EnemyVar.panel_y == poss_y)
                {
                    poss_x = Random.Range(1, 4);
                    poss_y = Random.Range(1, 4);
                    TestMove = true;
                }
            }
        }
        animate.SetTrigger("Moving");
        panel_x = poss_x;
        panel_y = poss_y;

    }

    public void ChangePosX(int x)
    {
        
        panel_x = x;
        animate.SetTrigger("Moving");
    }

    public void ChangePosY(int y)
    {
        
        panel_y = y;
        animate.SetTrigger("Moving");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bullet")
        {
            hp -= 1;
        }
    }

    public void Kill()
    {
        Manager.Level10Win = true;
        Destroy(this.gameObject);
    }
}
