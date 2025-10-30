using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    /// <summary>
    /// 最低落下速度
    /// </summary>
    [SerializeField] private float fallSpeedMin_ = 1;
    /// <summary>
    /// 最高落下速度
    /// </summary>
    [SerializeField] private float fallSpeedMax_ = 3;

    /// <summary>
    /// 爆発プレハブ。生成元から受け取る
    /// </summary>
    private Explosion explosionPrefab_;
    /// <summary>
    /// 地面のコライダー。生成元から受け取る
    /// </summary>
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVlocity();
    }
    /// <summary>
    /// 生成元から必要な情報を引き継ぐ
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
    }

    /// <summary>
    /// 移動量の設定
    /// </summary>
    private void SetupVlocity()
    {
        // 地面の上下左右の位置を取得
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2;
        float bottom = groundCollider_.bounds.center.y - groundCollider_.bounds.size.y / 2;


        float targetX = Mathf.Lerp(left, right, Random.Range(0.0f, 1.0f));

        Vector3 target = new Vector3(targetX, top, 0);
        Vector3 direction = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallSpeedMin_, fallSpeedMax_);
        rb_.linearVelocity = direction * fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        { Explosion(); }
        if (collision.gameObject.CompareTag("Ground"))
        { Fall(); }
    }

    /// <summary>
    /// 爆発
    /// </summary>
    private void Explosion()
    {
        // GameManagerにscore加算を通知
        gameManager_.AddScore(100);
        // 爆発を生成し
        Instantiate(explosionPrefab_, transform.position,
          Quaternion.identity);
        // 自身を消滅させる
        Destroy(gameObject);
    }


    /// <summary>
    /// 地面に落下
    /// </summary>
    private void Fall()
    {
        // GameManagerにダメージの通知
        gameManager_.Damage(1);
        // 自身は消滅
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
}