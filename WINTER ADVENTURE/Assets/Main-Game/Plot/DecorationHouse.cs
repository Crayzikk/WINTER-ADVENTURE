using UnityEngine;

public class DecorationHouse : MonoBehaviour
{
    [SerializeField] private GameObject chrismas;
    [SerializeField] private bool state;

    void Start()
    {   
        chrismas.SetActive(state);
    }

    public void ShowChrismas()
    {
        chrismas.SetActive(!state);
        Destroy(gameObject);
    }
}
