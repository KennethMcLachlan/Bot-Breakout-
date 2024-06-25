using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroExplosions : MonoBehaviour
{
    [SerializeField] private GameObject[] _explosions;
    [SerializeField] private AudioSource _explosionSFX;
    private bool _titleIsActive;
    void OnEnable()
    {
        _titleIsActive = true;
        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        while (_titleIsActive == true)
        {
            int randomExplosionObject = Random.Range(0, _explosions.Length);
            float randomExplosionTime = Random.Range(0f, 5f);

            _explosions[randomExplosionObject].SetActive(true);
            _explosionSFX.Play();
            yield return new WaitForSeconds(3f);
            _explosions[randomExplosionObject].SetActive(false);
            yield return new WaitForSeconds(randomExplosionTime);

        }
    }
}
