using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public InputReader input;

    private bool proning;
    private bool crouching;

    
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

    public void FlashOut()
    {
        animator.SetTrigger("FlashOut");
        animator.SetBool("FlashlightStillOut", true);
    }

    public void FlashIn()
    {
        animator.SetTrigger("FlashIn");
        animator.SetBool("FlashlightStillOut", false);
    }

    public void Prone()
    {
        if (crouching)
        {
            crouching = false;
            animator.SetBool("Crouching", crouching);
        }
        if (proning == false)
        {
            proning = true;
            animator.SetBool("Prone", proning);
        }
        else
        {
            proning = false;
            animator.SetBool("Prone", proning);
        }
        Debug.Log(proning);
    }

    public void Crouch()
    {
        if (proning)
        {
            proning = false;
            animator.SetBool("Prone", proning);
        }
        if (!crouching)
        {
            crouching = true;
            animator.SetBool("Crouching", crouching);
        }
        else
        {
            crouching = false;
            animator.SetBool("Crouching", crouching);

        }
    }

   public  IEnumerator Wait()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
    }
}
