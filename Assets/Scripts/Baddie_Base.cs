using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Baddie_Base : MonoBehaviour {

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
    public GameObject GManager;
    public IndieQuiltCommunicator Quilt;
    public GameMang Manager;
    public GameObject ForceDef;
    public FD_Ctrl ForceData;
    public Animator animate;
    

    // Use this for initialization
    void Start()
    {
        GManager = GameObject.Find("_GameManager");
        Quilt = GManager.GetComponent<IndieQuiltCommunicator>();
        Manager = GManager.GetComponent<GameMang>();
        Manager.EnemyList.Add(this.gameObject);
        level = Quilt.difficulty;

        if(Quilt.difficulty != 10 && Quilt.difficulty != -1)
        {
            ForceDef = GameObject.Find("Force Defender");
            ForceData = ForceDef.GetComponent<FD_Ctrl>();
        }
        
        
        animate = this.GetComponent<Animator>();
        if(this.gameObject.tag == "xi")
        {
            hp = 6;
            IntPos();
        }
        if(this.gameObject.tag == "xia")
        {
            hp = 4;
            IntPos();
        }
        if(this.gameObject.tag == "xidef")
        {
            hp = 20;
            if(Quilt.difficulty == 5)
            {
                panel_x = 2;
                panel_y = 2;
            }
            else if(Quilt.difficulty == 9)
            {
                IntPos();
            }
            else if(Quilt.difficulty == 10)
            {
                hp = 2;
                IntPos();
            }
            else
            {
                IntPos();
            }
        }
        
    }

    void Update()
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

        if(this.gameObject.tag == "xi")
        {
            if (hp != 0)
            {
                timer += Time.deltaTime;
                if (timer >= 1.5f)
                {
                    ChangePos();
                    timer = 0;
                }
            }
        }
        if (this.gameObject.tag == "xia")
        {
            if (hp != 0)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    ChangePos();
                    timer = 0;
                }
            }
        }
        if (this.gameObject.tag == "xidef")
        {
            if(Quilt.difficulty != 10 && Quilt.difficulty != -1)
            {
                if (hp != 0)
                {
                    if (fire == false)
                    {
                        timer += Time.deltaTime;
                        if (timer >= 0.2f && steps != 3)
                        {
                            ChangePos();
                            steps += 1;
                            timer = 0;
                        }
                        if (timer >= 1 && steps == 3)
                        {
                            if (EnemyID == 0 || EnemyID == 1 || Manager.EnemyList.Count == 1)
                            {
                                ChangePosY(ForceData.panel_y);
                            }
                            else
                            {
                                if (ForceData.panel_y == 1)
                                {
                                    int pos = Random.Range(1, 3);
                                    if (pos == 1)
                                    {
                                        ChangePosY(2);
                                    }
                                    else
                                    {
                                        ChangePosY(3);
                                    }

                                }
                                else if (ForceData.panel_y == 2)
                                {
                                    int pos = Random.Range(1, 3);
                                    if (pos == 1)
                                    {
                                        ChangePosY(1);
                                    }
                                    else
                                    {
                                        ChangePosY(3);
                                    }
                                }
                                else if (ForceData.panel_y == 3)
                                {
                                    int pos = Random.Range(1, 3);
                                    if (pos == 1)
                                    {
                                        ChangePosY(1);
                                    }
                                    else
                                    {
                                        ChangePosY(2);
                                    }
                                }
                            }
                            fire = true;
                            timer = 0;
                        }
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        if (timer >= 0.5f)
                        {
                            animate.SetTrigger("Firing");
                            Instantiate(blast, firepos, Quaternion.identity);
                            steps = 0;
                            fire = false;
                            timer = 0;

                        }


                    }

                }
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    animate.SetTrigger("Firing");
                    Instantiate(blast, firepos, Quaternion.identity);
                    timer = 0;

                }
            }
            
        }
    }

    //Used to Set Initial Location
    public void IntPos()
    {
        if(Quilt.difficulty != 9 && Quilt.difficulty != 10)
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
            panel_x = poss_x;
            panel_y = poss_y;
        }
        else if(Quilt.difficulty == 9)
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
        else if (Quilt.difficulty == 10)
        {
            int poss_ID = 0;
            Baddie_Base EnemyVar;
            if(Manager.EnemyList.Count == 1)
            {
                EnemyID = 1;
            }
            else if(Manager.EnemyList.Count > 1)
            {
                foreach (GameObject enemy in Manager.EnemyList)
                {
                    EnemyVar = enemy.GetComponent<Baddie_Base>();
                    if (EnemyVar.EnemyID == 1)
                    {
                        poss_ID = 2;
                    }
                    else if (EnemyVar.EnemyID == 2)
                    {
                        poss_ID = 3;
                    }
                }
            }
            EnemyID = poss_ID;

            if(EnemyID == 1)
            {
                panel_x = 1;
                panel_y = 1;
            }
            else if(EnemyID == 2)
            {
                panel_x = 2;
                panel_y = 1;
            }
            else if(EnemyID == 3)
            {
                panel_x = 3;
                panel_y = 1;
            }
            
            

        }
        
    }

    //Used for when baddies need to
    //Change position
    public void ChangePos()
    {
        int poss_x;
        int poss_y;
        bool TestMove = true;
        Baddie_Base EnemyVar;
        poss_x = Random.Range(1, 4);
        poss_y = Random.Range(1, 4);
        while(TestMove)
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

    //This is called for whenever
    //a baddie needs to change to
    //a specific panel location
    public void ChangePosX(int x)
    {
        animate.SetTrigger("Moving");
        panel_x = x;
    }
    public void ChangePosY(int y)
    {
        animate.SetTrigger("Moving");
        panel_y = y;
    }

    //This basically tells all the baddies
    //that if they get hit with a force
    //shot then they are fucked!!
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bullet")
        {
            hp -= 1;
        }
    }
   
    //Kills the enemy
    public void Kill()
    {
        Manager.EnemyList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

}
