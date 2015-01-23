using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

    public GameObject ForceDef;
    public FD_Ctrl ForceData;

	// Use this for initialization
	void Start () 
    {
        ForceDef = GameObject.Find("Force Defender");
        ForceData = ForceDef.GetComponent<FD_Ctrl>();
        this.rigidbody.AddForce(500, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(transform.position.y <= -5f || transform.position.x > 5f)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "xi" || other.transform.tag == "xia" || other.transform.tag == "xidef")
        {
            Destroy(this.gameObject);
        }
    }
}
