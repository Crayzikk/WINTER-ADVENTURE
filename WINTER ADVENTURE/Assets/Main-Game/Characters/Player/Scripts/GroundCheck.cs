using UnityEngine;

public class GroundCheck : MonoBehaviour
{   
    private float sphereRadius = 0.2f;
    private Collider[] collidersGround;

    public bool checkGroundPlayer;

    void Update()
    {
        if(!checkGroundPlayer)
        {
            collidersGround = Physics.OverlapSphere(transform.position, sphereRadius);

            foreach (Collider collider in collidersGround)
            {
                if(collider.CompareTag("Ground"))
                {
                    checkGroundPlayer = true;
                    break;
                }
            }  
        }  
    }
}
