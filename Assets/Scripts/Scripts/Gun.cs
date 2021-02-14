using UnityEngine;

public class Gun : MonoBehaviour
{
	[SerializeField]
	[Range(0.1f, 1.5f)]
	private float fireRate = 0.3f;

	[SerializeField]
	[Range(1, 10)]
	private int damage = 1;

	public GameObject thirdPersonCamera;
	public GameObject aimCamera;

	/*[SerializeField]
	private ParticleSystem muzzleParticle;*/

	//[SerializeField]
	//private AudioSource gunFireSource;

	private float timer;

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= fireRate)
		{
			if (Input.GetButton("Fire1"))
			{
				thirdPersonCamera.SetActive(false);
				aimCamera.SetActive(true);

				timer = 0f;
				FireGun();
			}
			else
			{
				thirdPersonCamera.SetActive(true);
				aimCamera.SetActive(false);
			}
		}
	}

	private void FireGun()
	{
		//Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);

		//muzzleParticle.Play();
		//gunFireSource.Play();

		Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, 100))
		{
			var health = hitInfo.collider.GetComponent<Health>();

			if (health != null)
				health.TakeDamage(damage);
		}
	}
}