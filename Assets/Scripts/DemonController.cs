using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{

    private Vector3 previousPosition;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x != previousPosition.x)
        {
            
            animator.SetFloat("HorizontalSpeed",1);


            previousPosition = transform.position;
        }

        if (transform.position.y != previousPosition.y)
        {
            
            animator.SetFloat("VerticalSpeed",1);


            previousPosition = transform.position;
        }

    }
}
