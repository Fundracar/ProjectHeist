using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourParticleSystem : MonoBehaviour
{
    public bool particleState;
    //public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        ActivateParticleSystem();
    }


    public void DestroyParticleSystem()
    {
        particleState = GameObject.FindGameObjectWithTag("ParticleSystem").GetComponent<ParticleSystem>().enableEmission = false;
        //test = GameObject.FindGameObjectWithTag("ParticleSystem").SetActive(false);
    }


    public void ActivateParticleSystem()
    {
        particleState = GameObject.FindGameObjectWithTag("ParticleSystem").GetComponent<ParticleSystem>().enableEmission = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
