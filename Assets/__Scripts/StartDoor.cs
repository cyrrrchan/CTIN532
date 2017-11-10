using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour {

    public GameObject door;

    public float offset;
    public float speed; //speed of door opening and closing

    private float t = 0.0f;

    float count = 0.0f;
    float duration = 1.0f;

    private bool doorClosed = false;
    public bool stepThroughDoor = false;

    void Start()
    {
        door.SetActive(false);
        //door.GetComponent<BoxCollider>().enabled = false;
        //door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + offset, door.transform.position.z);
    }

    void Update()
    {
        if (!doorClosed && stepThroughDoor) // close the door
        {
            door.SetActive(true);
            doorClosed = true;

            /*door.transform.position = new Vector3(door.transform.position.x, Mathf.Lerp(door.transform.position.y, door.transform.position.y - offset, t), door.transform.position.z);
            t += Time.deltaTime * speed;
            count += Time.deltaTime;

            if (count >= duration)
            {
                doorClosed = true;
                door.GetComponent<BoxCollider>().enabled = true;
                count = 0.0f;
                t = 0.0f;
            }*/
        }
    }

    private void OnTriggerEnter(Collider other) //turn on UI when inside collider
    {
        if (!stepThroughDoor)
            stepThroughDoor = true;
    }
}
