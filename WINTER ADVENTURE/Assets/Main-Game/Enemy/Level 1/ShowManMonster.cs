using UnityEngine;
using UnityEngine.AI;

public class ShowManMonster : MonoBehaviour
{
    private Transform player; 

    private Camera mainCamera; 
    private NavMeshAgent agent;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (player != null)
        {
            if (IsInCameraView())
            {
                agent.isStopped = true; // Зупинити рух
            }
            else
            {
                agent.isStopped = false; // Продовжити рух
                agent.SetDestination(player.position);
            }

            // Обертання ворога в напрямку до гравця
            LookAtPlayer();
        }
    }

    private bool IsInCameraView()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Об'єкт у полі зору камери, якщо:
        // - координати X і Y у діапазоні [0, 1]
        // - координата Z > 0 (об'єкт перед камерою)
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
               viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
               viewportPosition.z > 0;
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized; 
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerScreamer>().Screamer();
    }        
}
