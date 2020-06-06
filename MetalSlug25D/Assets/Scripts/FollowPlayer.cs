using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    public float diff;

    // Start is called before the first frame update
    void Start()
    {
        diff = transform.position.x - target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + diff, transform.position.y, transform.position.z);
    }
}
