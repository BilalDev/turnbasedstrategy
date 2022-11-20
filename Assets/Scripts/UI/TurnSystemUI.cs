using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private TextMeshProUGUI turnNumberText;

    private void Start()
    {
        endTurnBtn.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, int turn)
    {
        UpdateTurnText(turn);
    }

    private void UpdateTurnText(int turn)
    {
        turnNumberText.text = "TURN " + turn.ToString();
    }
}