using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{

    private Vector3 previousPosition;
    public Animator animator;
    public Pathfinding.AIPath aiPath;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {


        if (transform.position.x != previousPosition.x)
        {
            
            animator.SetFloat("HorizontalSpeed",1);
            Debug.Log(aiPath.velocity.x);
            setTurnSide();
            previousPosition = transform.position;
        }

        if (transform.position.y != previousPosition.y)
        {
            
            animator.SetFloat("VerticalSpeed",1);
            previousPosition = transform.position;
        }

    }

    private void setTurnSide()
    {
        if (aiPath.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (aiPath.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
