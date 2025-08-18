using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRb;
    PlayerMovement player;
    float moveSpeed = 10.0f;
    float targetTime = 7.0f;
    void Start()
    {
        bulletRb = this.GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector3 direction = player.transform.forward;
        bulletRb.AddForce(direction * Time.deltaTime * 50.0f, ForceMode.Impulse);

        if (targetTime >= 0)
        {
            targetTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Opp")
        {
            col.gameObject.GetComponent<Rigidbody>().
            AddForce(-col.gameObject.transform.forward * moveSpeed, ForceMode.Impulse);
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

}
