
using UnityEngine;
using UnityEngine.AI;
public class DoctorController : MonoBehaviour
{

    [SerializeField] private GameObject player;

    public NavMeshAgent agent;
    // Update is called once per frame
    private void Update()
    {
        agent.SetDestination(player.gameObject.transform.position);
    }
}
