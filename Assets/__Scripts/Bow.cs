using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField]  private float reloadTime;

    [SerializeField] private Arrow arrowPrefab;

    [SerializeField] private Transform spawnPoint;

    public Arrow currentArrow;

    private string enemyTag;

    private bool isReloading;

    [SerializeField] private float drawSpeed = 0.001f;
    
    private void Update() {
        if(currentArrow==null){
            Reload();
        }
    }
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

    //reload the bow after a short wait
    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        currentArrow.SetEnemyType(enemyTag);
        isReloading = false;
    }

    //fire the current arrow
    public void Fire(float firePower)
    {   
        //cant fire if reloading or there is no arrow
        if (isReloading || currentArrow == null) return;
        
        var force = transform.right*firePower;

        
        currentArrow.Fly(force);
        currentArrow = null;
        Reload();
    }


    public void Draw()
    {
        //cant draw if reloading or there is no arrow
        if (isReloading || currentArrow == null) return;
        
        //reference position
        Vector3 position = currentArrow.transform.position;
        
        //draw arrow back
        if (transform.position.x >position.x-1)
        {
            position.x -= drawSpeed;
            currentArrow.transform.position = position;
        }
    }
}