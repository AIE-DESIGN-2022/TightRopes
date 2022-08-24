using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Patrol,
    //PlayerSighted,
    //PlayerAttack
}
public class Enemy : MonoBehaviour
{
    public Transform[] moveToPositions;
    private Transform positionToMoveTo;
    private int currentPosition;
    public float speedMultiplier;
    bool reversePositions;

    private EnemyState currentState;
    // Start is called before the first frame update
    void Start()
    {
        MoveToNextPosition(); 
        currentState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, positionToMoveTo.position) < 0.1f)
        {
            MoveToNextPosition();
        }
        if (currentState == EnemyState.Patrol)
        {
            transform.LookAt(positionToMoveTo.position);
            transform.position = Vector3.MoveTowards(transform.position, positionToMoveTo.position, Time.deltaTime * speedMultiplier);
        }
    }
    void MoveToNextPosition()
    {
        if (currentPosition == moveToPositions.Length - 1)
        {
            reversePositions = true;
        }
        if(currentPosition == 0)
        {
            reversePositions = false;
        }
        if (!reversePositions)
        {
            currentPosition++; 
        }
        else
        {
            currentPosition--;    
        }
        positionToMoveTo = moveToPositions[currentPosition];
    }
}
