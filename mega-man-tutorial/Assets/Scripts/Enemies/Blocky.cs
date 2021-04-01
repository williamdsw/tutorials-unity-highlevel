using Other;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Blocky : MonoBehaviour
    {
        [SerializeField] private Scroller scroller;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private GameObject[] blockies;
        [SerializeField] private Rigidbody2D prefab;

        private bool isWalking = true;

        private Animator animator;
        private Transform player;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            scroller.enabled = true;
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement)
            {
                player = playerMovement.transform;
                StartCoroutine(Behaviour());
            }
        }

        private void Update()
        {
            if (player) return;
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement)
            {
                player = playerMovement.transform;
                StartCoroutine(Behaviour());
            }
        }

        private IEnumerator Behaviour()
        {
            yield return StartCoroutine(Walk());
            yield return StartCoroutine(ReadyToShoot());
            yield return StartCoroutine(Shoot());
            yield return StartCoroutine(Reset());
            StartCoroutine(Behaviour());
        }

        private IEnumerator Walk()
        {
            while (isWalking)
            {
                Vector2 origin = new Vector2(transform.position.x, transform.position.y - 1);
                Vector2 destiny = Vector2.right * scroller.Speed.x;
                RaycastHit2D hit = Physics2D.Raycast(origin, destiny, 0.5f, groundLayer);
                Color color = (hit ? Color.red : Color.green);
                Debug.DrawRay(origin, destiny, color);

                scroller.enabled = !hit;
                float distance = Vector2.Distance(transform.position, player.position);
                if (distance < 10f)
                {
                    yield return new WaitForSeconds(1f);
                    isWalking = false;
                }

                yield return null;
            }
        }

        private IEnumerator ReadyToShoot()
        {
            animator.enabled = scroller.enabled = false;
            yield return new WaitForSeconds(1f);
        }

        private IEnumerator Shoot()
        {
            List<Rigidbody2D> listBlockies = new List<Rigidbody2D>();
            for (int index = 0; index < blockies.Length; index++)
            {
                blockies[index].SetActive(false);
                Rigidbody2D blocky = Instantiate(prefab, blockies[index].transform.position, Quaternion.identity);
                blocky.AddForce(new Vector2(Random.Range(-15f, -10f), 4), ForceMode2D.Impulse);
                listBlockies.Add(blocky);
            }

            yield return new WaitForSeconds(3f);

            for (int index = 0; index < listBlockies.Count; index++)
            {
                Destroy(listBlockies[index].gameObject);
            }
        }

        private IEnumerator Reset()
        {
            for (int index = 0; index < blockies.Length; index++)
            {
                blockies[index].SetActive(true);
            }

            animator.enabled = scroller.enabled = isWalking = true;
            yield return null;
        }
    }
}