using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ragdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rbArray;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private EnemyScript _enemyScript;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        DeactiveRagDoll();
    }
    public void activeRagDoll()
    {
        for (int i = 0; i < rbArray.Length; i++)
        {
            rbArray[i].isKinematic = false;
            colliders[i].enabled = true;
        }
    }
    public void DeactiveRagDoll()
    {
        for (int i = 0; i < rbArray.Length; i++)
        {
            rbArray[i].isKinematic = true;
            colliders[i].enabled = false;
        }
    }
    public void ApplyDamage()
    {
        _enemyScript.DealDamage();
    }

    public void StopAgent()
    {
        _enemyScript.agent.isStopped = true;
    }
    public void ResumeAgent()
    {
        _enemyScript.agent.isStopped = false;
    }

}
