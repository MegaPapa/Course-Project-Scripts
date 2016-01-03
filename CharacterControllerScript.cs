using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
	//переменная для установки макс. скорости персонажа
	public float maxSpeed = 10f; 
	//переменная для определения направления персонажа вправо/влево
	private bool isFacingRight = true;
	//ссылка на компонент анимаций
	private Animator anim;
	//находится ли персонаж на земле или в прыжке?
	private bool isGrounded = false;
	//ссылка на компонент Transform объекта
	//для определения соприкосновения с землей
	public Transform groundCheck;
	//радиус определения соприкосновения с землей
	private float groundRadius = 0.2f;
	//ссылка на слой, представляющий землю
	public LayerMask whatIsGround;
	public bool bronzeKey = false;
	public bool silverKey = false;
	public bool goldenKey = false;
	public float spawnx, spawny;
	public int score = 0;
	public float timescore = 0;
	

	private void Start()
	{
		anim = GetComponent<Animator>();
		spawnx = transform.position.x;
		spawny = transform.position.y;
	}
	

	private void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
		anim.SetBool ("Ground", isGrounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		if (!isGrounded)
			return;

		float move = Input.GetAxis("Horizontal");
		

		anim.SetFloat("Speed", Mathf.Abs(move));
		

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		

		if(move > 0 && !isFacingRight)
			Flip();
		else if (move < 0 && isFacingRight)
			Flip();
	}
	
	private void Update()
	{
		//если персонаж на земле и нажат пробел...
		if (isGrounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			//устанавливаем в аниматоре переменную в false
			anim.SetBool("Ground", false);
			//прикладываем силу вверх, чтобы персонаж подпрыгнул
			rigidbody2D.AddForce(new Vector2(0, 600));				
		}
		timescore = timescore + 0.01f;
		if (Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel ("FirstChapter");
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit();

	}

	private void Flip()
	{
		//меняем направление движения персонажа
		isFacingRight = !isFacingRight;
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}

	public GameObject first_dessaw;
	public GameObject second_dessaw;
	public GameObject third_dessaw;
	public GameObject bigsaw;

	void OnTriggerEnter2D(Collider2D col)
	{
		//обрабатываем вхождения со всеми триггерами, с которыми может
		//взаимодействовать персонаж
		if (col.gameObject.name == "bronzeKey") 
		{
			Destroy(col.gameObject);
			bronzeKey = true;
		}

		if (col.gameObject.name == "silverKey") 
		{
			Destroy(col.gameObject);
			silverKey = true;
			Destroy(first_dessaw);
			Destroy(second_dessaw);
			Destroy(third_dessaw);
		}

		if (col.gameObject.name == "goldenKey") 
		{
			Destroy(col.gameObject);
			goldenKey = true;
		}

		if (col.gameObject.name == "dieCollider")
			transform.position = new Vector3 (spawnx, spawny, transform.position.z);

		if (col.gameObject.name == "GravityController")
		{
			rigidbody2D.gravityScale = 0.5f;
		}

		if (col.gameObject.name == "notaspace")
			rigidbody2D.gravityScale = 1f;
		

		if ((goldenKey) && (silverKey) && (bronzeKey))
			Destroy (bigsaw);

		if (col.gameObject.name == "blackhole")
			if (Application.loadedLevelName == "FirstChapter")
				Application.LoadLevel ("ChapterTwo");

		if ((col.gameObject.name == "checkPoint") && (Application.loadedLevelName == "FirstChapter")) 
		{
			spawnx = 85f;
			spawny = 32f;
		}

		if (col.gameObject.name == "star") 
		{
			Destroy(col.gameObject);
			score++;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if ((col.gameObject.name == "saw") || (col.gameObject.name == "dessaw") || (col.gameObject.name == "bigsaw") || (col.gameObject.name == "enemy"))
			transform.position = new Vector3 (spawnx, spawny, transform.position.z);
	}

	void OnGUI()
	{
		GUI.Box (new Rect(0, 0, 100, 100), "Score: " + 100*score);
	}
}