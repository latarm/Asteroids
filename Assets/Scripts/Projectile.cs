using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    #region Fields

    public GameObject ShootEffect;
	public GameObject HitEffect;
    public GameObject FiringShip;
    public float MoveSpeed;

    #endregion

    #region UnityMethods

    void Start ()
    {
		GameObject obj = Instantiate(ShootEffect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = FiringShip.transform;
		Destroy(gameObject, 5f);    
	}

    void Update()
    {
        transform.Translate(Vector3.up * MoveSpeed * Time.deltaTime, Space.Self);
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {		
		if (col.gameObject != FiringShip && col.gameObject.tag != "Projectile")
        {
			Instantiate(HitEffect, transform.position, Quaternion.identity);
            GameController.Instance.LevelData.CurrentScore++;
            InfoBarsController.Instance.UpdateScore();
			Destroy(gameObject);
            Destroy(col.gameObject);
		}
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    #endregion
}
