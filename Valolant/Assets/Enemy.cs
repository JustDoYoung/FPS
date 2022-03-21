using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//목적지를 향해서 이동하고 싶다. 
//agent기능을 이용해서
public class Enemy : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //agnet에게 목적지를 계속 알려주고 싶다.(길 찾기 실행)
        agent.destination = target.transform.position;
    }
}
