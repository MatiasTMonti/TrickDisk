using UnityEngine;

public class Player : MonoBehaviour
{
    //SHOP
    [SerializeField] private SkinManager skinManager;
    [SerializeField] private GameObject bolaACambiarDeColor;

    [SerializeField] private GameObject bgObject;

    [SerializeField] private GameObject directionObject;

    [SerializeField] private GameObject greenExplosionPrefab;

    [SerializeField] private GameObject redExplosionPrefab;

    private Transform directionTransform;

    private bool hasGameStarted;

    private bool canRotate;

    [SerializeField] private float maxOffset;

    [SerializeField] private float baseTimeForHalfCycle = 1.0f;

    private float timeForHalfCycle;

    [SerializeField] private float intialOffset;

    private bool canShoot;

    private float shootCounter;

    private float shootCounterSpeed;

    private bool canMove;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float startPosY;

    private Vector3 moveDirection;

    [SerializeField] private AudioClip moveClip;

    [SerializeField] private AudioClip loseClip;

    [SerializeField] private AudioClip winClip;

    private bool canScore;

    private void Awake()
    {
        //
        Sprite selectedSprite = skinManager.GetSelectedSkin().sprite;

        if (selectedSprite != null)
        {
            bolaACambiarDeColor.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }
        else
        {
            Debug.LogError("No se encontró un Sprite seleccionado en el SkinManager.");
        }
        //Shop
        bgObject.SetActive(true);
        directionTransform = directionObject.transform;
        directionTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Start()
    {
        hasGameStarted = false;
        canRotate = true;

        canShoot = true;
        shootCounter = 0.5f;
        timeForHalfCycle = baseTimeForHalfCycle;
        shootCounterSpeed = 1 / baseTimeForHalfCycle;

        canMove = false;
        moveDirection = Vector3.zero;

        canScore = false;
    }

    private void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();

            Debug.Log(timeForHalfCycle);
        }
    }

    private void Shoot()
    {
        AudioManager.instance.PlaySound(moveClip);

        canRotate = false;
        hasGameStarted = true;
        canShoot = false;
        canMove = true;
        canScore = true;

        float angle = maxOffset * (shootCounter - 0.5f) * 2f - intialOffset;
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized;
        directionObject.SetActive(false);
        bgObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!hasGameStarted) return;

        if (canRotate)
        {
            shootCounter += Time.fixedDeltaTime * shootCounterSpeed;

            if (shootCounter > 1f || shootCounter < 0f)
            {
                shootCounterSpeed *= -1f;
            }

            float angle = maxOffset * (shootCounter - 0.5f) * 2f;
            directionTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (canMove)
        {
            Vector3 targetPos = transform.position + moveDirection;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

            if (transform.position.y < startPosY)
            {
                canMove = false;
                canRotate = true;
                canShoot = true;

                directionObject.SetActive(true);
                bgObject.SetActive(true);

                Vector3 tempPos = transform.position;
                tempPos.y = startPosY;
                transform.position = tempPos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAGS.DEATH))
        {
            AudioManager.instance.PlaySound(loseClip);
            Destroy(collision.gameObject);
            Instantiate(redExplosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.EndGame();
            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag(Constants.TAGS.GOAL))
        {
            if (!canScore) return;
            AudioManager.instance.PlaySound(winClip);
            Destroy(collision.gameObject);
            Instantiate(greenExplosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.UpdateScore();
            moveDirection *= -1f;
            timeForHalfCycle -= 0.1f;
            GlobalLogger.Log("Increse Move");
            GameManager.AddScore();
            return;
        }
    }
}
