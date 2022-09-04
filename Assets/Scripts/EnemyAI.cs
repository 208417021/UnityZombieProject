using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chancingRange = 5f;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false; // Enemy will see if target in range

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {   
        distanceToTarget = Vector3.Distance(target.position, transform.position); // the distance from Player to enemy
        
        if (isProvoked)
            EngageTarget();
        else if (distanceToTarget <= chancingRange)
            isProvoked = true;
        else
            isProvoked = false;
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
            /// navMeshAgent.stoppingDistance => native field in Nav Agent, can be see in Unity Editor.
            /// the distance between target and enemy is farther than stopping distance, then enemy will keep chancing.
            ChancingTarget();
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
            AttackTarget();
    }

    private void ChancingTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        Debug.LogError(name + " attacking " + target.name);
        /// name => which is using the component.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chancingRange);
    }
}
