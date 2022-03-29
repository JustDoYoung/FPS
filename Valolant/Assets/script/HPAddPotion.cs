using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAddPotion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        //만약 other가 플레이어라면(Collider or Character Controller)
        //name, tag, layer로 판별할 수 있다.
        // if(other.gameObject.name.Contains("Player"))
        // if(other.gameObject.CompareTag("Player"))
        // if(other.gameObject.layer == LayerMask.NameToLayer("Player"))

        if (other.gameObject.name.Contains("Player"))
        {
            //플레이어의 체력을 1증가시키고 싶다.
            PlayerHP php = other.gameObject.GetComponent<PlayerHP>();
            //null 체크로 보완할 수 있다.(오브젝트에 PlayerHP 컴포넌트가 없으면 null이니까 실행이 안 될 것임.)
            if (php != null)
            {
                php.AddHP(1);
                Destroy(gameObject);
            }
        }
    }
}
