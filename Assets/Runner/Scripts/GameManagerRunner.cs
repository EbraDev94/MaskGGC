using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerRunner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject magnet;
    [SerializeField] private GameObject magnetPlayer;
    [SerializeField] private GameObject panelstart;
    [SerializeField] private GameObject panelGameOver;


    [SerializeField] private TextMeshProUGUI txFish;
    [SerializeField] private TextMeshProUGUI txBestFish;

    private int fish = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnStart()
    {
        panelstart.SetActive(false);
        _playerController.SetTurn();
    }
    public void BtnReStart()
    {
        SceneManager.LoadScene("Runner");
    }
    public void AddScore()
    {
        fish++;
        txFish.text = fish.ToString();
    }

    public void GameOver()
    {
        print("GameOver");
        if (fish > PlayerPrefs.GetInt("Top"))
        {
            PlayerPrefs.SetInt("Top", fish);
        }

        txBestFish.text = PlayerPrefs.GetInt("Top").ToString();
        panelGameOver.SetActive(true);

    }

    public void SetMagnet()
    {
        magnet.GetComponent<Image>().fillAmount = 1;
        magnet.SetActive(true);
        
        magnetPlayer.SetActive(true);
        StartCoroutine(IEMagnet());
    }

    IEnumerator IEMagnet()
    {
        yield return new WaitForFixedUpdate();
        magnet.GetComponent<Image>().fillAmount -= 0.007f;
        
        if (magnet.GetComponent<Image>().fillAmount>0)
        {
            StartCoroutine(IEMagnet());
        }
        else
        {
            magnetPlayer.SetActive(false);
            magnet.SetActive(false);
            magnet.GetComponent<Image>().fillAmount = 1;
        }
    }
    
}
