using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurretController : NetworkBehaviour
{
    public Transform PlayerCameraPosition;
    public GameObject Projectile;

    public int EnemySpawnMinTime;
    public int EnemySpawnMaxTime;
    public GameObject EnemySpawnObject;
    public Transform EnemySpawnLocation;
    private bool SpawningEnemies;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Camera.main.transform.parent = PlayerCameraPosition;
        Camera.main.transform.position = PlayerCameraPosition.position;
        SpawningEnemies = true;
        StartCoroutine(EnemySpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector3 _verticalTurn = new Vector3(-Input.GetAxis("Vertical"), 0, 0);
        transform.Rotate(_verticalTurn);
        Vector3 _horizontalTurn = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        transform.Rotate(_horizontalTurn, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdShootProjectile();
        }
    }

    [Command]
    void CmdShootProjectile()
    {
        var projectile = Instantiate(Projectile, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 50;
        NetworkServer.Spawn(projectile);
        Destroy(projectile, 4.0f);
    }

    [Command]
    void CmdSpawnEnemy()
    {
        var enemy = Instantiate(EnemySpawnObject, EnemySpawnLocation.position, EnemySpawnLocation.rotation);
        NetworkServer.Spawn(enemy);
    }

    public IEnumerator EnemySpawnTimer()
    {
        while (SpawningEnemies)
        {
            yield return new WaitForSeconds(Random.Range(EnemySpawnMinTime, EnemySpawnMaxTime));
            CmdSpawnEnemy();
        }
    }
}
