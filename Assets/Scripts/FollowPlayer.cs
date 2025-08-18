using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}
