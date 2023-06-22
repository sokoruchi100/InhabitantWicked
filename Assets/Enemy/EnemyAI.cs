using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private const string MOVE_TRIGGER = "move";
    private const string ATTACK_BOOL = "attack";
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private NavMeshAgent navMeshAgent;
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private CapsuleCollider capsuleCollider;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update() {
        

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked) {
            EngageTarget();
        } else if (distanceToTarget <= chaseRange) {
            isProvoked = true;
        }
    }

    private void OnDamageTaken() {
        isProvoked = true;
        if (enemyHealth.IsDead()) {
            navMeshAgent.enabled = false;
            capsuleCollider.enabled = false;
            enabled = false;
        }
    }

    private void EngageTarget() {
        FaceTarget();
        if (distanceToTarget  >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }

        if (distanceToTarget < navMeshAgent.stoppingDistance) {
            AttackTarget();
        }
    }

    private void ChaseTarget() {
        animator.SetBool(ATTACK_BOOL, false);
        animator.SetTrigger(MOVE_TRIGGER);
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget() {
        animator.SetBool(ATTACK_BOOL, true);
        Debug.Log("You have been hurt!");
    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
