using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    Vector3 RightFForward;
    Vector3 LeftFForward;
    Rigidbody m_Rigidbody;
    Animator anim;
    float m_Speed;
    float distToGround;
    public bool checkGround;
    float initTime = 20;
    public Text ttime;
    public Text ttarg;
    public int currTargets = 0;
    public int goalTargets = 10;
    public Text Win;
    public Text Loose;
    public GameObject bPref;
    public Transform bPos;
    bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        RightFForward = new Vector3(1, 0, 0);
        LeftFForward = new Vector3(-1, 0, 0);
        m_Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        m_Speed = 1.1f;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finish)
        {
            initTime -= Time.deltaTime;
            ttime.text = initTime.ToString();
            ttarg.text = currTargets.ToString() + " / " + goalTargets.ToString();
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("startRuning", true);
            transform.forward = RightFForward;
            if (m_Speed > m_Rigidbody.velocity.magnitude)
            {
                //m_Rigidbody.velocity += transform.forward * m_Speed;
                //m_Rigidbody.AddForce(transform.forward * m_Speed);
                transform.position += transform.forward * m_Speed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("startRuning", true);
            if (m_Speed > m_Rigidbody.velocity.magnitude)
            {
                transform.forward = LeftFForward;
                //m_Rigidbody.velocity += transform.forward * m_Speed;
                //m_Rigidbody.AddForce(transform.forward * m_Speed);
                transform.position += transform.forward * m_Speed * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("startRuning", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            anim.SetBool("jump", true);
            m_Rigidbody.AddForce(Vector3.up * m_Speed * 150);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("shoots", true);
            GameObject a = Instantiate(bPref, bPos.position, Quaternion.identity);
            a.transform.forward = transform.forward;
            a.GetComponent<Bullet>().c = this;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot")) // check if "bash" is playing...
        {
            anim.SetBool("shoots", false);
        }

        if (!IsGrounded())
        {
            anim.SetBool("jump", false);
        }

        if(goalTargets == currTargets)
        {
            Win.gameObject.SetActive(true);
            finish = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) // check if "bash" is playing...
        {
            anim.SetBool("Die", false);
        }

        if (initTime <= 0 && currTargets != goalTargets && !finish)
        {
            anim.SetBool("Die", true);
            Loose.gameObject.SetActive(true);
            finish = true;
        }
    }

    bool IsGrounded()
    {
        return !Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
