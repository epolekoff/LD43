using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TitleText;
    public TMPro.TextMeshProUGUI CountText;
    public TMPro.TextMeshProUGUI TipText;

    private const string ExpertTitle = "<color=\"yellow\">Master</color>";
    private const string HeroTitle = "<color=\"green\">Hero</color>";
    private const string NeutralTitle = "<color=#5FF>Ehh...</color>";
    private const string SacrificerTitle = "<color=\"red\">Sacrificer</color>";
    private const string EmptyTitle = "<color=\"red\">Evil</color>";

    private const string ExpertTip = "Tip: Ayyyy, you're a master!\nGood job!";
    private const string HeroTip = "Tip: You did well!\nBut can you save more than 300?";
    private const string NeutralTip = "Tip: Try sacrificing golden scrimbles to get EVEN MORE scrimbles!";
    private const string SacrificerTip = "Tip: You must really hate scrimbles...";
    private const string EmptyTip = "Tip: Is this even possible? I guess so!";

    private const int UnitCountForMaster = 300;
    private const int UnitCountForHero = 250;
    private const int UnitCountForSacrificer = 100;
    private const int UnitCountForEmpty = 30;

    // Replay the game.
    public void OnReplayButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Set up all of the strings.
    /// </summary>
    public void InitializeForEndOfGame()
    {
        int scrimbleCount = int.Parse(GameObject.Find("CountText").GetComponent<TMPro.TextMeshProUGUI>().text);
        if (scrimbleCount >= UnitCountForMaster)
        {
            UpdateText(ExpertTitle, string.Format("<color=\"yellow\">{0}</color>", scrimbleCount), ExpertTip);
        }
        else if (scrimbleCount >= UnitCountForHero)
        {
            UpdateText(HeroTitle, string.Format("<color=\"green\">{0}</color>", scrimbleCount), HeroTip);
        }
        else if (scrimbleCount <= UnitCountForSacrificer)
        {
            UpdateText(SacrificerTitle, string.Format("<color=\"red\">{0}</color>", scrimbleCount), SacrificerTip);
        }
        else if (scrimbleCount <= UnitCountForEmpty)
        {
            UpdateText(EmptyTitle, string.Format("<color=\"red\">{0}</color>", scrimbleCount), EmptyTip);
        }
        else
        {
            UpdateText(NeutralTitle, string.Format("<color=#5FF>{0}</color>", scrimbleCount), NeutralTip);
        }
    }

    /// <summary>
    /// Set the text.
    /// </summary>
    private void UpdateText(string titleText, string countText, string tipText)
    {
        TitleText.text = "Your Title: " + titleText;
        CountText.text = "Your Scrimbles: " + countText;
        TipText.text = tipText;
    }
}
