using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : MonoBehaviour
{
    private Unit unit;

    private CharacterController characterController;

    //private Camera cam;

    public LayerMask enemyMask;

    public Team team = Team.Player;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        characterController = GetComponent<CharacterController>();
    }

    private void Move()
    {
        switch (team)
        {
            case Team.Player:
                if (characterController == null)
                {
                    characterController = GetComponent<CharacterController>();
                    return;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    characterController.Move(this.transform.forward *
                        Time.deltaTime * unit.unitInfo.moveSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    characterController.Move(this.transform.forward *
                        Time.deltaTime * unit.unitInfo.moveSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    characterController.Move(this.transform.forward *
                        Time.deltaTime * unit.unitInfo.moveSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    characterController.Move(this.transform.forward *
                        Time.deltaTime * unit.unitInfo.moveSpeed);
                }
                break;
        }
    }

    
    private void Update()
    {
        Move();
        Rotation();
    }


    void Rotation()
    {
        if (Time.timeScale < 1)
        {
            return;
        }
        switch (team)
        {
            case Team.Player:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                float rayLength;
                if (plane.Raycast(ray, out rayLength))
                {
                    Vector3 mousepoint = ray.GetPoint(rayLength);
                    this.transform.LookAt(new Vector3(mousepoint.x,
                        this.transform.position.y, mousepoint.z));

                }
                break;
        }

    }
}

