using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DialogController.dialogStart = true;
            Destroy(gameObject);
        }
    }
}
