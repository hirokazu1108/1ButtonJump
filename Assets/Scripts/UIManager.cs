using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI
{
    FuelBar,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider fuelSlider;

    [SerializeField] Player player;

    public void updateUI(UI ui, float value)
    {
        switch (ui) {
            case UI.FuelBar:
                fuelSlider.value = value;
                break;
        }

    }

}
