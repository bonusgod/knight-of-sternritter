using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int score;
    private int enemiesOnScene;

    public static LevelController Instance {get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public virtual void EnemiesCount()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesOnScene = enemies.Length;
        Debug.Log(enemiesOnScene);

        if (enemiesOnScene == 0)
            Hero.Instance.Invoke("SetLosePanel", 1.1f);
    }
}
