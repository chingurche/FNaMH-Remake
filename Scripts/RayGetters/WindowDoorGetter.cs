using UnityEngine;

public class WindowDoorGetter : MonoBehaviour, IRayGetter
{
    void IRayGetter.SendToReceiver()
    {
        if (FindObjectOfType<Telegram>().isTelegramWorking) { return; }
        
        GetComponent<Rotator>().isClosing = true;
    }
}
