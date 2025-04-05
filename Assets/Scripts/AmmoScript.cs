using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField] float LifeTime;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable(LifeTime));
    }

    IEnumerator AutoDisable(float seg)
    {
        yield return new WaitForSeconds(seg);
        this.gameObject.SetActive(false);
    }
}
