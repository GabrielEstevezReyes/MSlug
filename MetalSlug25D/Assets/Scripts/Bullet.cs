using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float m_Speed = 2.1f;
    float time = 0;
    public CharacterMovement c;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position += transform.forward * m_Speed * Time.deltaTime;
        if(time >= 2)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SP")
        {
            c.currTargets++;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
