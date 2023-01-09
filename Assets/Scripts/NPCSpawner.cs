using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;


    private void Start()
    {
        for (int i = 0; i < Random.Range(3,100); i++)
        {
            Vector3 pos = new Vector3(Random.Range(-transform.lossyScale.x/2, transform.lossyScale.x/2) + transform.position.x,
                3f,
                Random.Range(-transform.lossyScale.z/2, transform.lossyScale.z/2) + transform.position.z);
            
            Instantiate(_spawnObject, pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position,transform.localScale);
    }
}
