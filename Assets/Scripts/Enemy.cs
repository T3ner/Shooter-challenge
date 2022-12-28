using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameObject target;

    [SerializeField] Animator enemyAnim;
    [SerializeField] NavMeshAgent enemyAgent;

    int isMovingHash;

    void Awake()
    {
        isMovingHash = Animator.StringToHash("IsMoving");
    }
    void Update()
    {
        bool isMoving = enemyAnim.GetBool(isMovingHash);
        enemyAnim.Play("Run");
        enemyAgent.destination = target.transform.position;
    }
    public void Hurt(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
