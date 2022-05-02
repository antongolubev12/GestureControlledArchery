using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private Arrow arrowPrefab;

    [SerializeField]
    private Transform spawnPoint;

    private Arrow currentArrow;

    private string enemyTag;

    private bool isReloading;

    [SerializeField]
    private float drawSpeed = 0.001f;
    
    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Reload()
    {
        if (isReloading || currentArrow != null) return;
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        currentArrow.SetEnemyType(enemyTag);
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentArrow == null) return;
        
        var force = transform.right*firePower;
        currentArrow.Fly(force);
        currentArrow = null;
        Reload();
    }

    public void Draw()
    {
        if (isReloading || currentArrow == null) return;
        
        Vector3 position = currentArrow.transform.position;
        //currentArrow.SetParent(null);
        if (transform.position.x >position.x-1)
        {
            position.x -= drawSpeed;
            currentArrow.transform.position = position;
        }
    }

    public bool IsReady()
    {
        return (!isReloading && currentArrow != null);
    }
}