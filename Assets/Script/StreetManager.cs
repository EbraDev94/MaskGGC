using UnityEngine;

public class StreetManager : MonoBehaviour
{
    public GameObject player;
    public GameObject kadin;
    public GameObject man;
    public GameObject zengin;
    public GameObject dog;
    public GameObject kedi;
    public GameObject kediFake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetString("Street")=="Kadin")
        {
            kediFake.SetActive(true);
            
            zengin.SetActive(false);
        }
        else if (PlayerPrefs.GetString("Street")=="Zengin")
        {
            kediFake.SetActive(true);
            
            man.SetActive(false);
            kadin.SetActive(false);
        }
        else if (PlayerPrefs.GetString("Street")=="Kedi")
        {
            kedi.SetActive(true);
            dog.SetActive(true);
            
            player.SetActive(false);
            kediFake.SetActive(false);
            zengin.SetActive(false);
            man.SetActive(false);
            kadin.SetActive(false);
        }
        else
        {
            kedi.SetActive(true);
            dog.SetActive(true);
            
            player.SetActive(true);
            kediFake.SetActive(true);
            zengin.SetActive(true);
            man.SetActive(true);
            kadin.SetActive(true); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
