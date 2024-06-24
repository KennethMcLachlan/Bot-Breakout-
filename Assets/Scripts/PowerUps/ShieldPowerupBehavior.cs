using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _shieldContainer;

    [SerializeField] private AudioSource _shieldDown;
    [SerializeField] private AudioSource _shieldUp;

    public void RecieveShieldPowerupNotifictaion()
    {
        StartCoroutine(ShutDownAllShieldsRoutine());
    }
    private IEnumerator ShutDownAllShieldsRoutine()
    {
        _shieldDown.Play();
        _shieldContainer.SetActive(false);
        yield return new WaitForSeconds(8f);
        _shieldUp.Play();
        _shieldContainer.SetActive(true);
    }
}
