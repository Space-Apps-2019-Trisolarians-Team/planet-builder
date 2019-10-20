using UnityEngine;
using SimpleHTTP;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject planetBuilder;
    public GameObject starScreen;
    public StarConfiguration starConfig;
    public GameObject starView;
    public GameObject similarPlanetView;
    public SimilarExoplanet similarController;

    void Start()
    {
        StartCoroutine(GetInitialValues());
    }

    public IEnumerator GetInitialValues()
    {
        Request request = new Request("http://67.207.90.121/stats/planets?fields=pl_rade,pl_ratdor,pl_masse");
        Client http = new Client();
        yield return http.Send(request);
        if (http.IsSuccessful())
        {
            Response resp = http.Response();
            startScreen.SetActive(true);
            PlanetStatsResponse res = JsonUtility.FromJson<PlanetStatsResponse>(resp.Body());
            Constants.dataMinSize = res.pl_rade.min;
            Constants.dataMaxSize = res.pl_rade.max;
            Constants.dataMinDistance = res.pl_ratdor.min;
            Constants.dataMaxDIstance = res.pl_ratdor.max;
            Constants.dataMinMass = res.pl_masse.min;
            Constants.dataMaxMass = res.pl_masse.max;
        }
        
        Request request2 = new Request("http://67.207.90.121/stats/stars");
        Client http2 = new Client();
        yield return http2.Send(request2);
        if (http2.IsSuccessful())
        {
            Response resp = http2.Response();
            startScreen.SetActive(true);
            StarStatsResponse res = JsonUtility.FromJson<StarStatsResponse>(resp.Body());
            starConfig.SetMinMaxValues(res);
        }
    }

    public IEnumerator GetRandomStar()
    {
        Request request = new Request("http://67.207.90.121/random-star");
        Client http = new Client();
        yield return http.Send(request);
        if (http.IsSuccessful())
        {
            Response resp = http.Response();
            RandomStarResponse res = JsonUtility.FromJson<RandomStarResponse>(resp.Body());
            starConfig.SetStarValues(res.st_age, res.st_mass, res.st_teff, res.st_lum, res.st_rad);
            starScreen.SetActive(true);
            starView.SetActive(true);
        }
    }

    public void GetSimilarPlanet(float radius, float distance)
    {
        StartCoroutine(GetSimilarPlanetRequest(radius, distance));
    }

    public IEnumerator GetSimilarPlanetRequest(float radius, float distance)
    {
        Request request = new Request(string.Format("http://67.207.90.121/similar_exoplanet?pl_rade={0}&pl_distance={1}", radius, distance));
        Client http = new Client();
        yield return http.Send(request);
        if (http.IsSuccessful())
        {
            Response resp = http.Response();
            SimilarExoplanetResponse res = JsonUtility.FromJson<SimilarExoplanetResponse>(resp.Body());
            ShowSimilarPlanet(res);
        }
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
        StartCoroutine(GetRandomStar());
    }

    public void StartPlanetBuilder()
    {
        starScreen.SetActive(false);
        starView.SetActive(false);
        planetBuilder.SetActive(true);
    }
}
