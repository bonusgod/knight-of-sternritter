using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    // Start is called before the first frame update
    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    public void SetPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
        public void ToMenu()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}
