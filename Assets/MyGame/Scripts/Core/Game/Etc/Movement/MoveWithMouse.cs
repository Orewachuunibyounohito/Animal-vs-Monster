using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWithMouse : MonoBehaviour
{
    [ShowInInspector]
    public float MoveSpeed { get; private set; } = 5;

    private void FixedUpdate(){
        Movement();
    }

    private void Movement(){
        float   moveStep     = MoveSpeed * Time.fixedDeltaTime;
        Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(GameManager.Instance.CustomInput.Camera.ScreenPosition.ReadValue<Vector2>());
        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, mouseInWorld, moveStep));
    }
}
