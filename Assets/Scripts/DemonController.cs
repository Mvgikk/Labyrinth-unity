using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemonController : VersionedMonoBehaviour
{
    IAstarAI ai;

    //waypoints for patrolling of the demon
    public PatrolGizmoScript patrol;
    private Transform[] waypoints;
    /// <summary>Time in seconds to wait at each target</summary>
    public float delay = 0;

    /// <summary>Current target index</summary>
    int index;
    public AudioClip screechSound;
    IAstarAI agent;
    float switchTime = float.PositiveInfinity;
    public TextDisplayController textDisplay;
    private Vector3 previousPosition;
    public Animator animator;
    public SoundManager soundManager;
    public Pathfinding.AIPath aiPath;
    public Transform playerTransform;
    public Transform monsterTransform;
    public float visionRange = 10;

    public Slider fearBarSlider;

    public bool sensesPlayer = false;
    public bool debuffed = false;

    private bool playsSoundEffect = false;
    private bool waypointsSet = false;

    // Start is called before the first frame update
    void Start()
    {


    }

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

    /// <summary>Update is called once per frame</summary>
    void PatrolUpdate()
    {
        if (!waypointsSet)
        {
            waypointsSet = setWaypoints();
            if (waypointsSet)
            {
                Debug.Log("Ustawilem!");
            }
        }
        else
        {
            if (waypoints.Length == 0) return;

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

    /// <summary>Updates the AI's destination every frame</summary>
    void FollowingUpdate()
    {
        if (playerTransform != null && ai != null) ai.destination = playerTransform.position;
    }

    public void debuff()
    {
        debuffed = true;
        Debug.Log("Debuffed");
    }
    //private void PlayScreechSound()
   // {
      //  AudioSource.PlayClipAtPoint(screechSound, transform.position);
    //}
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

        visionRange = fearBarSlider.value;

        if(Vector3.Distance (playerTransform.position, monsterTransform.position) < visionRange)
        {
            Debug.Log("Senses Player");
            sensesPlayer = true;
            textDisplay.UpdateText("Monster senses you!");
            FollowingUpdate();
            //soundManager.PlayMonsterScreechEffect();
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
            
            animator.SetFloat("HorizontalSpeed",1);
            //Debug.Log(aiPath.velocity.x);
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
