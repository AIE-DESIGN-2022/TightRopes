using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public InputReader input;
    
    // Start is called before the first frame update
    void Start()
    {
        input = FindObjectOfType<InputReader>();
        animator=FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        idle();
        walk();
/*        Debug.Log(input.isWalking);
*/
    }

    void walk()
    {
        if (input.isWalking)
        {
            
            animator.SetBool("Walking", true);
            animator.SetBool("Idle",false);
        }
        else
        {
/*            Debug.Log("not walking");
*/            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
    }

    void idle()
    {
        if (!input.isWalking)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }
    }
}
