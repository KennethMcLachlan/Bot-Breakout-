using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 _cubeExtents;
    [SerializeField] private AudioSource _explosionSFX;
    [SerializeField] private GameObject _explosionPFX;
    public void NukeExplosion()
    {

        Collider[] hitColliders = Physics.OverlapBox(transform.position, _cubeExtents, Quaternion.identity);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<EnemyAI>().Damage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _cubeExtents * 2); // says that half Extents should be * 2
    }

    public void RecievePowerupNotification()
    {
        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        NukeExplosion();
        _explosionPFX.SetActive(true);
        _explosionSFX.Play();
        yield return new WaitForSeconds(3f);

        _explosionPFX.SetActive(false);
    }
}
    
