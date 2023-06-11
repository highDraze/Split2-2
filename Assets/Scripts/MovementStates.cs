using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStates : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    Player player;
    bool isDM;

    public GameObject visualPlayer;
    public GameObject visualDM;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.transform.parent.GetComponent<Rigidbody>();
        player = gameObject.GetComponentInParent<Player>();
        isDM = player.isDM;

        if(isDM){
            transform.Find("DMVisual").gameObject.SetActive(true);
            anim = visualDM.GetComponent<Animator>();
        } else {
            transform.Find("PlayerVisual").gameObject.SetActive(true);
            anim = visualPlayer.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // 0 3
        float velocity = Vector3.Distance(rb.velocity, Vector3.zero);
        float s = 1/Mathf.Sqrt(2);
        float right = Mathf.Sign(-s * rb.velocity.x + s * rb.velocity.z);

        if(right == 1 ){
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if(velocity > 0.05f){
            anim.SetBool("Walking", true);
            anim.SetFloat("Speed", velocity);
        } else {
            anim.SetBool("Walking", false);
            anim.SetFloat("Speed", 1.0f);
        }
    }
}
