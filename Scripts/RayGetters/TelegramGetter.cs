using UnityEngine;

public class TelegramGetter : MonoBehaviour, IRayGetter
{
    private Telegram _Telegram;

    private void Awake()
    {
        _Telegram = FindObjectOfType<Telegram>();
    }

    void IRayGetter.SendToReceiver()
    {
        _Telegram.SwitchTelegramStatus(true);
    }
}
