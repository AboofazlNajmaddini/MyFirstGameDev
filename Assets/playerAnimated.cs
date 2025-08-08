using UnityEngine;

public class playerAnimated : MonoBehaviour
{
    private player player;
    void Start() => player = GetComponentInParent<player>();


    private void AnimEventTrigerd() => player.AttackTrigerd();
}
