using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

       public AudioSource gameMusic;

    // Start is called before the first frame update
    void Start()
    {
        gameMusic.Play(0);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
