using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _lookTurnSpeed = 360;
    [SerializeField] private float _strafeTurnSpeed = 1080;
    [SerializeField] private Camera _mainCamera;
    
    public void Move(Vector3 moveInput)
    {
        _rb.MovePosition(transform.position + (transform.forward * moveInput.magnitude) * _speed * Time.deltaTime);
    }

    // rotates the player to look the direction they want to move in
    public void Look(Vector3 moveInput)
    {
        
        if (moveInput != Vector3.zero) // if input vec is zero this would cause us to rotate back to forwads
        {
            // since we are in iso metric space, vert and horizontal are not relative to the screen, this matrix fixes that
            var mat = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            // mutiply out movement input by this rotation matrix to get isometric input
            var rotatedInput = mat.MultiplyPoint3x4(moveInput);

            // relative diff between where the rb is now and wants to be
            var relative = (transform.position + rotatedInput) - transform.position;
            // rotate along vec3.up
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            // rotate towards lerps the rotation so its sooth between rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _lookTurnSpeed * Time.deltaTime);
        }
    }

    public void StrafeMove(Vector3 moveInput)
    {

    }

    public void StrafeLook(Vector3 mouseScreenPos)
    {
        // convert screen pos to world pos
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPos);
        
        var mat = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        var rotatedInput = mat.MultiplyPoint3x4(mouseWorldPos);

        // relative diff between where the rb is now and wants to be
        var relative = (transform.position + rotatedInput) - transform.position;
        // rotate along vec3.up
        var rotation = Quaternion.LookRotation(rotatedInput, Vector3.up);

        // rotate towards lerps the rotation so its sooth between rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _strafeTurnSpeed * Time.deltaTime);
    }
}
