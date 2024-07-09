using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
    public enum State{
        Standing,
        Crouching,
    }
    public State state = State.Standing;
    public float HeighestHeight = 0;
    private Transform CameraTransform => Camera.main.transform;
    [SerializeField] private VelocityCalculator velocityCalculator;
    
    private void Update(){
        if (GetCurrentHeight() > HeighestHeight)
        {
            HeighestHeight = GetCurrentHeight();
        }

        if (GetCurrentHeight() < HeighestHeight * 0.8f)
        {
            state = State.Crouching;
        }
        else
        {
            state = State.Standing;
        }

    }
    public Vector3 GetPlayerCurrentVelocity()
    {
        return velocityCalculator.GetVelocity();
    }

    public Vector2 Get2DPosition(){
        return new Vector2(CameraTransform.localPosition.x, CameraTransform.localPosition.z);
    }

    public float GetCurrentHeight(){
        return CameraTransform.localPosition.y;
    }

    private Vector3 posOffset = new Vector3(0.5f, 0.0f, 1.0f);
    private Quaternion rotOffset = Quaternion.Euler(0, -90, 0);

    public void SetOffset(Vector2 offset){
        transform.localPosition = new Vector3(offset.x, 0, offset.y) + posOffset;
        transform.localRotation = rotOffset;
    }
    public void ResetOffset(){
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
