using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    [SerializeField] float damage;
    bool destroy_queue;
    private void FixedUpdate()
    {
        if (destroy_queue)
        {
            Destroy(gameObject);
            return;
        }
        RaycastHit hit;
        Ray ray = new Ray(transform.position, velocity);
        if (Physics.Raycast(ray, out hit, velocity.magnitude * 0.02f))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            destroy_queue = true;
        }
        else
        {
            transform.position += velocity * 0.02f;
        }
    }

    public static void CreateProjectile(Vector3 pos, Vector3 vel, float damage)
    {
        GameObject proj = Instantiate(Resources.Load("Projectile") as GameObject);
        Projectile _proj = proj.GetComponent<Projectile>();
        _proj.transform.position = pos;
        _proj.damage = damage;
        _proj.velocity = vel;
    }
}
