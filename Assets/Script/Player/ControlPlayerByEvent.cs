using System.Collections;
using MyBox;
using UnityEngine;

public class ControlPlayerByEvent : MonoBehaviour
{
    [AutoProperty] [SerializeField] private PlayerControl _playerControl;
    private Vector3 startingPosition => _playerControl.transform.position;
    
    public Vector3 Destination;

    public void MoveToDestination(Transform destination)
    {
        Destination = destination.position;
        var distance = Vector3.Distance(startingPosition, destination.position);
        StartCoroutine(Moving(_playerControl.moveSpeed));
    }

    public void MoveAway(float magnitude)
    {
        Destination = new Vector3(startingPosition.x + magnitude, startingPosition.y, startingPosition.z);
        StartCoroutine(Moving(_playerControl.moveSpeed));
    }

    private IEnumerator Moving(float speed)
    {
        _playerControl.DisableControls();
        var _rb = _playerControl.PlayerBody;
        
        while (startingPosition != Destination)
        {
            var newPos = Vector3.MoveTowards(_rb.position, Destination, speed * Time.fixedDeltaTime);
            _rb.MovePosition(newPos);
            _rb.linearVelocity = ((Vector2)newPos - _rb.position)/Time.fixedDeltaTime;
            yield return null;
        }

        _playerControl.EnableControl();
        _playerControl.isDoingEvent = false;
    }
}