using UnityEngine;

public class HitBox : MonoBehaviour
{
    public delegate void HitEnemy(NewEnemy target);
    public HitEnemy OnHitEnemy;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            var enemy = other.GetComponent<NewEnemy>();
            OnHitEnemy?.Invoke(enemy);
        }
    }
}
