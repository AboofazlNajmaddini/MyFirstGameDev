using UnityEngine;

public class playerAnimated : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private player player;
    void Start()
    {
        player = GetComponentInParent<player>();
        
    }

    private void AnimEventTrigerd()
    {
        player.AttackTrigerd();
    }
}
