using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    // private GameObject agent;
    [SerializeField] private Transform Agent;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        // get postion agent
        

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 desiredDirection = (Agent.transform.position - transform.position).normalized;
        Vector3 SteeringForce = (desiredDirection - enemyRb.velocity).normalized * speed;
        enemyRb.AddForce(SteeringForce * 0.5f);
    }
}
