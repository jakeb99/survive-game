using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector3 _input;
    [SerializeField] Movement playerMovement;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            playerMovement.StrafeLook(Input.mousePosition);
            GetMovementInput();
        } 
        else
        {
            GetMovementInput();
            playerMovement.Look(_input);
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) )
        {
            playerMovement.Move(_input);
        }
        else
        {
            playerMovement.Move(_input);
        }
    }
    private void GetMovementInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
}
