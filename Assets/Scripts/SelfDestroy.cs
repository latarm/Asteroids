using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SelfDestroy : MonoBehaviour
{
	void Start ()
    {
		Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
	}
}
