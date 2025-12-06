using UnityEngine;
using UnityEngine.UI;

public class GiveDataBack : MonoBehaviour
{
    public void Start()
    {
        int highscore = KeepData.highScoreValue;
        int damage = KeepData.damageValue; // save scores/stats between scenes
    }
}
