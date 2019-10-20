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
    public GameObject starView;
    public GameObject similarPlanetView;
    public SimilarExoplanet similarController;

    private HttpClient client;
    void Start()
    {
        client = new HttpClient();
        GetInitialValues();
    }

    private void GetInitialValues() {
        client.Get(new Uri("http://67.207.90.121/stats/planets?fields=pl_rade,pl_ratdor,pl_masse"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            loadingScreen.SetActive(false);
            startScreen.SetActive(true);
            PlanetStatsResponse res = JsonUtility.FromJson<PlanetStatsResponse>(r.ReadAsString());
            Constants.dataMinSize = res.pl_rade.min;
            Constants.dataMaxSize = res.pl_rade.max;
            Constants.dataMinDistance = res.pl_ratdor.min;
            Constants.dataMaxDIstance = res.pl_ratdor.max;
            Constants.dataMinMass = res.pl_masse.min;
            Constants.dataMaxMass = res.pl_masse.max;
        });
        client.Get(new Uri("http://67.207.90.121/stats/stars"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            loadingScreen.SetActive(false);
            startScreen.SetActive(true);
            StarStatsResponse res = JsonUtility.FromJson<StarStatsResponse>(r.ReadAsString());
            starConfig.SetMinMaxValues(res);
        });
    }

    private void GetRandomStar()
    {
        client.Get(new Uri("http://67.207.90.121/random-star"), HttpCompletionOption.AllResponseContent, (r) =>
        {
            RandomStarResponse res = JsonUtility.FromJson<RandomStarResponse>(r.ReadAsString());
            starConfig.SetStarValues(res.st_age, res.st_mass, res.st_teff, res.st_lum, res.st_rad);
            starScreen.SetActive(true);
            starView.SetActive(true);
        });
    }

    public void GetSimilarPlanet(float radius, float distance)
    {
        Debug.Log(string.Format("http://67.207.90.121/similar_exoplanet?pl_rade={0}&pl_distance={1}", radius, distance));
        client.Get(new Uri(string.Format("http://67.207.90.121/similar_exoplanet?pl_rade={0}&pl_distance={1}", radius, distance)), HttpCompletionOption.AllResponseContent, (r) =>
        {
            Debug.Log(r.ReadAsString());
            SimilarExoplanetResponse res = JsonUtility.FromJson<SimilarExoplanetResponse>(r.ReadAsString());
            ShowSimilarPlanet(res);
        });
    }

    private void ShowSimilarPlanet(SimilarExoplanetResponse res)
    {
        planetBuilder.SetActive(false);
        similarPlanetView.SetActive(true);
        similarController.SetInfo(res.pl_hostname, res.pl_name, res.st_rade, res.pl_distance, res.pl_rade, true);
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        GetRandomStar();
    }

    public void StartPlanetBuilder()
    {
        starScreen.SetActive(false);
        starView.SetActive(false);
        planetBuilder.SetActive(true);
    }
}
