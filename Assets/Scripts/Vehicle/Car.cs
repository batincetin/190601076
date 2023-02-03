using UnityEngine;

public class Car : Movement
{
    [SerializeField] private WheelRotation[] _wheelRotation;

    protected override void MovementUpdate()
    {

        if (joystick == null)
            return;

        Vector2 direction = joystick.Direction;

        Vector3 movementVector = new (direction.x, 0, direction.y);

        transform.position = Vector3.Lerp(transform.position, transform.position + movementVector, Time.deltaTime * Speed);
        foreach (WheelRotation wheelRotation in _wheelRotation)
        {
            wheelRotation.SetSpeed(direction.magnitude * 20);
        }
        if (movementVector.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,  Quaternion.LookRotation(movementVector, Vector3.up), Time.deltaTime * RotationSpeed);
        }

    }
}