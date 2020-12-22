using UnityEngine;

public class InimigoSpawn : MonoBehaviour
{
    [SerializeField] private float minPositionZ = 0;
    [SerializeField] private float maxPositionZ = 0;
    [SerializeField] private float spawnTimer = 0;
    [SerializeField] private int numberOfEnemies = 0;
    [SerializeField] private GameObject[] objectEnemies;
    private int actualNumberOfEnemies = 0;

    private BoxCollider boxCollider;
    private ResetarCamera resetCamera;
    private CameraSeguidora followerCamera;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        resetCamera = FindObjectOfType<ResetarCamera>();
        followerCamera = FindObjectOfType<CameraSeguidora>();
    }

    private void Update()
    {
        if (actualNumberOfEnemies >= numberOfEnemies)
        {
            int numberOfEnemies = FindObjectsOfType<Inimigo>().Length;

            if (numberOfEnemies <= 0)
            {
                resetCamera.Ativar();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            var current = followerCamera.MaximoXY;
            current.x = transform.position.x;
            followerCamera.MaximoXY = current;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        bool isRight = (Random.Range(0, 2) == 0);
        Vector3 posicao = new Vector3(0, 0, 0);
        posicao.z = Random.Range(minPositionZ, maxPositionZ);
        float positionX = (transform.position.x + (isRight ? +10 : -10));
        posicao = new Vector3(positionX, 0, posicao.z);

        Instantiate(objectEnemies[Random.Range(0, objectEnemies.Length)], posicao, Quaternion.identity);
        actualNumberOfEnemies++;

        if (actualNumberOfEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemies", spawnTimer);
        }
    }
}
