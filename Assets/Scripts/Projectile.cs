using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    #region Fields

    public GameController GameController;

    public GameObject shoot_effect;
	public GameObject hit_effect;
    public GameObject _firing_ship;
    private ShipController _shipController;
    public float _moveSpeed;

    #endregion

    #region UnityMethods

    void Start ()
    {
        _shipController = _firing_ship.GetComponent<ShipController>();
		GameObject obj = Instantiate(shoot_effect, transform.position  - new Vector3(0,0,5), Quaternion.identity); //Spawn muzzle flash
		obj.transform.parent = _firing_ship.transform;
		Destroy(gameObject, 5f);    
	}

    void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime, Space.Self);
    }
	
	void OnTriggerEnter2D(Collider2D col)
    {		
		if (col.gameObject != _firing_ship && col.gameObject.tag != "Projectile")
        {
			Instantiate(hit_effect, transform.position, Quaternion.identity);
            GameController.LevelData.CurrentScore++;
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
