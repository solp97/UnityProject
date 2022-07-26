using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerShoter _shoter;
    private PlayerMovement _movement;

    public bool CanShot;
    public bool CanReload;
    public bool CanRecharge;


    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _shoter = GetComponent<PlayerShoter>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if (_input.Mouse)
        {          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (Vector3.Distance(transform.position, hit.point) > 0.1f)
                {
                    _shoter.shot(hit.point - transform.position);
                }
                else
                {
                    if(!_movement.isMove)
                        _movement.isCanMove(transform, hit.point);
                }
            }
        }
    }




}
