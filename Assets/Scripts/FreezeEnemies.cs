using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private TowerData towerData;
    private GameObject weapon;
    private float enemySpeed;
    [SerializeField] float speedReduction = .5f;


    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = towerData.CurrentLevel.bullet;
        weapon = towerData.CurrentLevel.weapon;

        Vector3 startPosition = weapon.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;


        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;



    }


    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInChildren<TowerData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;

        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<PathFinder>().GetDistanceToGoal();
            enemySpeed = enemy.GetComponent<PathFinder>().GetEnemySpeed();
            enemy.GetComponent<PathFinder>().SetEnemySpeed(speedReduction);
            enemy.GetComponent<SpriteRenderer>().color = Color.cyan; 
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > towerData.CurrentLevel.fireRate)
            {
                //Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }

            weapon = towerData.CurrentLevel.weapon;

            Vector3 direction = weapon.transform.position - target.transform.position;
            weapon.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 - 90 / Mathf.PI, new Vector3(0, 0, 1));




        }

    }
}
