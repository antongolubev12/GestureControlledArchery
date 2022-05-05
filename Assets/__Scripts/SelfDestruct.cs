using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{   
   [SerializeField] float timer;

    //self destruct after certain amount of time
   private void Update() {
       timer-=Time.deltaTime;
       if(timer<=0){
           Destroy(gameObject);
       }
   }
}