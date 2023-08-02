using UnityEngine;

public class WindowDoorGetter : MonoBehaviour, IRayGetter
{
    private Rotator _rotator;
    private void Start()
    {
        _rotator = GetComponent<Rotator>();
    }
    void IRayGetter.SendToReceiver()
    {
        if (FindObjectOfType<Telegram>().isTelegramWorking
        || !_rotator.isOpening) { return; }
        
        _rotator.isClosing = true;
    }
}
