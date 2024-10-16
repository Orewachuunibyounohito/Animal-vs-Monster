using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MouseClickToMove : MonoBehaviour
{
    public NavMeshAgent _agent;

    void Awake(){
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame){
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Road"));
            if(hit.collider != null){
                _agent.SetDestination(hit.collider.transform.position);
            }
        }
    }
}
