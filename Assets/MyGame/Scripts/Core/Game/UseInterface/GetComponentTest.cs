using UnityEngine;

public class GetComponentTest : MonoBehaviour
{
    private NewAttacker attacker      => GetComponent<NewAttacker>();

    private void Awake() => Debug.Log( $"Can I catch the MeleeAttacker by NewAttacker? {attacker}" );
}
