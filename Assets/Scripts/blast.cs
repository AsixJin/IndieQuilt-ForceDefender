using UnityEngine;
using System.Collections;

public class blast : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(-1200, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -5f || transform.position.x < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player")
        {
            Destroy(this.gameObject);
        }
    }
}
