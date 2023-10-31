using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemonController : VersionedMonoBehaviour
{
    IAstarAI ai;

    //waypoints for patrolling of the demon/monster
    public PatrolGizmoScript patrol;

    private Transform[] waypoints;
    public float delay = 0;

    int index;
    public AudioClip screechSound;
    IAstarAI agent;
    float switchTime = float.PositiveInfinity;
    public TextDisplayController textDisplay;
    private Vector3 previousPosition;
    public Animator animator;
    public SoundManager soundManager;
    public Pathfinding.AIPath aiPath;
    private Transform playerTransform;
    public GameObject player;
    public Transform monsterTransform;
    public float visionRange = 10;
    public float isCloseRange = 5;

    public Slider fearBarSlider;

    public bool sensesPlayer = false;

    private bool playsSoundEffect = false;
    private bool waypointsSet = false;
    private bool isWaiting = false;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<IAstarAI>();
    }

    private bool setWaypoints()
    {
        waypoints = patrol.GetWaypoints();
        previousPosition = transform.position;
        return (waypoints != null);
    }

    void PatrolUpdate()
    {
        if (!waypointsSet)
        {
            waypointsSet = setWaypoints();
        }
        else if (!isWaiting)
        {
            if (waypoints.Length == 0) return;

            if (agent.reachedEndOfPath)
            {
                StartCoroutine(Wait());
            }

            bool search = false;

            // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
            // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
            if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
            {
                switchTime = Time.time + delay;
            }

            if (Time.time >= switchTime)
            {
                index = index + 1;
                search = true;
                switchTime = float.PositiveInfinity;
            }

            index = index % waypoints.Length;
            agent.destination = waypoints[index].position;

            if (search) agent.SearchPath();
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;  //set the bool to stop moving
        animator.SetBool("isWaiting", isWaiting);
        yield return new WaitForSeconds(3); // wait for 5 sec
        isWaiting = false; // set the bool to start moving
        animator.SetBool("isWaiting", isWaiting);
    }

    void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        // Update the destination right before searching for a path as well.
        // This is enough in theory, but this script will also update the destination every
        // frame as the destination is used for debugging and may be used for other things by other
        // scripts as well. So it makes sense that it is up to date every frame.
        if (ai != null && sensesPlayer) ai.onSearchPath += FollowingUpdate;
    }

    void OnDisable()
    {
        if (ai != null && sensesPlayer) ai.onSearchPath -= FollowingUpdate;
    }

    void FollowingUpdate()
    {
        if (playerTransform != null && ai != null) ai.destination = playerTransform.position;
    }

    IEnumerator PlaySoundAndWait(AudioClip sound)
    {

        playsSoundEffect = true;

        AudioSource.PlayClipAtPoint(sound, playerTransform.position);
        // Wait until the sound finishes playing
        yield return new WaitForSeconds(sound.length);
        playsSoundEffect = false;

    }   

    void FixedUpdate()
    {
        playerTransform = player.GetComponent<Transform>();
        visionRange = fearBarSlider.value;

        bool isHidden = player.GetComponent<Player>().isHidden;
        bool isClose = Vector3.Distance(playerTransform.position, monsterTransform.position) < isCloseRange;
        bool isInRange = Vector3.Distance(playerTransform.position, monsterTransform.position) < visionRange;


        //if ((isHidden && isClose) || (!isHidden && isInRange))
        if (!isHidden && isInRange)
        {
            Debug.Log("Senses Player");
            sensesPlayer = true;
            textDisplay.UpdateText("Monster senses you!");
            FollowingUpdate();
            if (!playsSoundEffect) 
            {
                StartCoroutine(PlaySoundAndWait(screechSound));
            }
        }
        else
        {
            sensesPlayer = false;
            PatrolUpdate();
        }


        if (transform.position.x != previousPosition.x)
        {
            isWaiting = false;
            //animator.SetFloat("HorizontalSpeed",1);
            setTurnSide();
            previousPosition = transform.position;
        }

        if (transform.position.y != previousPosition.y)
        {
            isWaiting = false;
            //animator.SetFloat("VerticalSpeed",1);
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
