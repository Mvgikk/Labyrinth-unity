using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    public Player Player;
    SpriteRenderer sprite;
 
    // Use this for initialization
    void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.keys_collected == 3)
        {
            //zmiana koloru na zielony
        sprite.color = new Color (0, 1, 0, 1);
        }
    }
}
