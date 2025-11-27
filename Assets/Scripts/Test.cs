using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] int test = 0;

    void Start()
    {
        test = GameManager.gm.test;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
