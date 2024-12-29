using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animatorDoor;
    private bool state = false;
    //isOpen_Obj_1

    void Start()
    {
        animatorDoor = GetComponentInParent<Animator>();
    }

    public void OpenOrCloseDoor()
    {
        animatorDoor.SetBool("isOpen_Obj_1", !state);
        state = !state;
    }
}
