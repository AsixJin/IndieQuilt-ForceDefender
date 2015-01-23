using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FD_Ctrl : MonoBehaviour
{

    Vector3 position;
    public Vector3 firepos;
    public GameObject forceshot;
    public GameObject GamMan;
    public GameMang GameMan;
    public IndieQuiltCommunicator Quilt;
    public Animator animator;
    public bool frozen = false;
    public bool locked = false;
    public bool Reload = false;
    public int panel_x = 2;
    public int panel_y = 2;
    public int bullet_fired = 0;
    public int hp = 15;
    float Timer;
    float ReloadT;

    // Use this for initialization
    void Start()
    {
        if(Application.loadedLevel == 0)
        {
            locked = true;   
        }
        GamMan = GameObject.Find("_GameManager");
        GameMan = GamMan.GetComponent<GameMang>();
        Quilt = GamMan.GetComponent<IndieQuiltCommunicator>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        position = transform.position;
        firepos.x = transform.position.x + 0.50f;
        firepos.y = position.y;
        firepos.z = transform.position.z;

        if(hp == 0)
        {
            Kill();
        }

        if (frozen || locked)
        {
            transform.position = transform.position;
            Timer += Time.deltaTime;
            if(Timer >= 2)
            {
                frozen = false;
                Timer = 0;
            }

        }
        else
        {
            //This part of the code controls movement
            //based on the 3x3 grid
            if (Input.GetKeyDown(KeyCode.W) && panel_y != 3)
            {
                animator.SetTrigger("MoveTo");
                panel_y += 1;

            }
            if (Input.GetKeyDown(KeyCode.S) && panel_y != 1)
            {
                animator.SetTrigger("MoveTo");
                panel_y -= 1;
            }

            if (Input.GetKeyDown(KeyCode.A) && panel_x != 1)
            {
                animator.SetTrigger("MoveTo");
                panel_x -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D) && panel_x != 3)
            {
                animator.SetTrigger("MoveTo");
                panel_x += 1;
            }  

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
                position.x = -3.15f;
                transform.position = position;
            }
            else if (panel_x == 2)
            {
                position.x = -1.89f;
                transform.position = position;
            }
            else if (panel_x == 3)
            {
                position.x = -0.63f;
                transform.position = position;
            }

            //This is for firing force shots
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (bullet_fired != 2)
                {
                    animator.SetTrigger("Firing");
                    Instantiate(forceshot, firepos, Quaternion.identity);
                    bullet_fired += 1;
                }
                if (bullet_fired == 2)
                {
                    Reload = true;
                }
            }
            if (Reload == true)
            {
                ReloadT += Time.deltaTime;
                if (ReloadT >= 0.5f)
                {
                    bullet_fired = 0;
                    Reload = false;
                    ReloadT = 0;
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "blast")
        {
            if(Quilt.difficulty != 10)
            {
                frozen = true;
            }
            else
            {
                hp -= 1;
            }           
        }
    }

    public void ChangeScene()
    {
        if(Quilt.difficulty < 5)
        {
            Application.LoadLevel("Stage");
        }
        
        else if(Quilt.difficulty >= 5 && Quilt.difficulty != 10)
        {
            Application.LoadLevel("Stage2");
        }
        else if(Quilt.difficulty == 10)
        {
            Application.LoadLevel("Stage3");
        }
    }

    public void Kill()
    {
        GameMan.Level10lose = true;
        Destroy(this.gameObject);
    }

}
