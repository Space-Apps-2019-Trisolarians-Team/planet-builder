using UnityEngine;
using UnityEngine.UI;

public class StarConfiguration : MonoBehaviour
{
    public Slider ageSlider;
    public Slider massSlider;
    public Slider temperatureSlider;
    public Slider radiusSlider;

    public Text ageValue;
    public Text massValue;
    public Text temperatureValue;
    public Text radiusValue;

    public Transform star;
    public Transform starHalo;

    public void SetStarValues(float age, float mass, float temperature, float luminocity, float radius)
    {
        ageSlider.value = age;
        massSlider.value = mass;
        temperatureSlider.value = temperature;
        radiusSlider.value = radius;

        CalculateAgeValue();
        CalculateMassValue();
        CalculateTemperatureValue();
        CalculateRadiusValue();
    }

    public void SetMinMaxValues(StarStatsResponse res)
    {
        ageSlider.minValue = res.st_age.min;
        ageSlider.maxValue = res.st_age.max;
        massSlider.minValue = res.st_mass.min;
        massSlider.maxValue = res.st_mass.max;
        temperatureSlider.minValue = res.st_teff.min;
        temperatureSlider.maxValue = res.st_teff.max;
        radiusSlider.minValue = res.st_rad.min;
        radiusSlider.maxValue = res.st_rad.max;
    }

    public void CalculateAgeValue()
    {
        ageValue.text = string.Format("{0:0.00} billion years", ageSlider.value);
    }

    public void CalculateMassValue()
    {
        massValue.text = string.Format("{0:0.00} Suns", massSlider.value);
    }

    public void CalculateTemperatureValue()
    {
        temperatureValue.text = string.Format("{0:0} K", temperatureSlider.value);
        float scale = (Constants.maxStarHaloScale - Constants.minStarHaloScale) * temperatureSlider.normalizedValue + Constants.minStarHaloScale;
        starHalo.localScale = new Vector3(scale, scale, star.localScale.z);
    }

    public void CalculateRadiusValue()
    {
        radiusValue.text = string.Format("{0:0} km", radiusSlider.value * 695700);
        float scale = (Constants.maxStarScale - Constants.minStarScale) * radiusSlider.normalizedValue + Constants.minStarScale;
        star.localScale = new Vector3(scale, scale, star.localScale.z);
    }
}
