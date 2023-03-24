using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public GameManagerBehaviour gameManager;

    private float distance;
    private float startTime;







    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);

        Vector3 direction = gameObject.transform.position - target.transform.position;
        gameObject.transform.rotation = Quaternion.AngleAxis(
        Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI + 90,
        new Vector3(0, 0, 1));
        GameObject gm = GameObject.Find("UIManager");
        gameManager = gm.GetComponent<GameManagerBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
       
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

      
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
           
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);

             
                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    gameManager.Gold += 20;
                }
            }
            Destroy(gameObject);
            
        }



    }
}
