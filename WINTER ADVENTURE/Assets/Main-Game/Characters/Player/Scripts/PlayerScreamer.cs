using UnityEngine;
using UnityEngine.UI;

public class PlayerScreamer : MonoBehaviour
{
    [SerializeField] private Image screamer;

    void Start()
    {
        screamer.enabled = false;
    }

    public void Screamer()
    {
        screamer.enabled = true;
    }
}
