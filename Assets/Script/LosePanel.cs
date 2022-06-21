using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    // Start is called before the first frame update
    private void Awake()
    {
        losePanel.SetActive(false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void ToMenu()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}
