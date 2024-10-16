using UnityEngine;

[RequireComponent( typeof( Rigidbody2D ) )]
public class MoveForward : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    public void SetVelocity( Vector2 velocity ){
        rb.velocity = velocity;
    }
}
