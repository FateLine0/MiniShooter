using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent; 
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer; 

    //Pratrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //Attacking 
    public float timeBetweenAttacks; 
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange,PlayerInAttackingRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,whatIsPlayer); 
        PlayerInAttackingRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);

        if(!playerInSightRange && !PlayerInAttackingRange){
            Pratrolling();
        } 
           if(playerInSightRange && !PlayerInAttackingRange){
            Chasing();
        } 
            if(playerInSightRange && PlayerInAttackingRange){
            Attacking();
        } 

    }

    public void Awake(){
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Pratrolling(){
        if(!walkPointSet){
            SearchWalkPoint(); 
        }
         if(walkPointSet){
            agent.SetDestination(walkPoint); 
        }  
    }

    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange); 
        float randomX = Random.Range(-walkPointRange,walkPointRange); 

        walkPoint = new Vector3(transform.position.z+randomX,0.0f,transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        {
            walkPointSet = true;
        }

        Vector3 distanceToWalkPoint= transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false; 
        }
    }

     private void Attacking(){
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyAttacked){
            
            alreadyAttacked =true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    } 

    private void ResetAttack(){
        alreadyAttacked = false; 
    }
    private void Chasing(){
        agent.SetDestination(player.position);
    }
}
