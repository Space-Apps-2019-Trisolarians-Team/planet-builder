using UnityEngine;
using System;
using CI.HttpClient;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject loadingScreen;
    public GameObject planetBuilder;
    public GameObject starScreen;
    public StarConfiguration starConfig;

    private HttpClient client;
    void Start()
    {
        client = new HttpClient();
        GetInitialValues();
    }

    private void GetInitialValues() {
        client.Get(new Uri("http://67.207.90.121/stats/planets?fields=pl_rade,pl_ratdor"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            loadingScreen.SetActive(false);
            startScreen.SetActive(true);
            PlanetStatsResponse res = JsonUtility.FromJson<PlanetStatsResponse>(r.ReadAsString());
            Debug.Log(r.ReadAsString());
            Debug.Log(res.pl_rade.min);
            Constants.dataMinSize = res.pl_rade.min;
            Constants.dataMaxSize = res.pl_rade.max;
            Constants.dataMinDistance = res.pl_ratdor.min;
            Constants.dataMaxDIstance = res.pl_ratdor.max;
        });
        client.Get(new Uri("http://67.207.90.121/stats/stars"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            loadingScreen.SetActive(false);
            startScreen.SetActive(true);
            StarStatsResponse res = JsonUtility.FromJson<StarStatsResponse>(r.ReadAsString());
            starConfig.SetMinMaxValues(res);
            // CONFIGURAR LOS VALORES DE LAS CONSTANTES
        });
    }

    private void GetRandomStar()
    {
        client.Get(new Uri("http://67.207.90.121/random-star"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            // MOSTRAR LA ESTRELLA
            RandomStarResponse res = JsonUtility.FromJson<RandomStarResponse>(r.ReadAsString());
            starConfig.SetStarValues("NOMBRE", res.st_age, res.st_mass, res.st_teff, res.st_lum, res.st_rad, res.st_spstr);
            starScreen.SetActive(true);
        });
    }

    private void ShowStart()
    {

    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        GetRandomStar();
    }

    public void StartPlanetBuilder()
    {
        starScreen.SetActive(false);
        planetBuilder.SetActive(true);
    }
}
