using UnityEngine;
using UnityEngine. SceneManagement;
public class Bird : MonoBehaviour
{
       [SerializeField] float maxDrageDistance = 4;
   [SerializeField] float launchPower = 150;
     LineRenderer lineRenderer;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent < LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.enabled = false;
        startingPosition = transform.position;
    }

void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startingPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionAndMagnitude * launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
void OnMouseDrag()
    {
    Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
destination.z = 0;
if ( Vector2.Distance(destination, startingPosition) > maxDrageDistance)
destination = Vector3.MoveTowards(startingPosition, destination, maxDrageDistance);

transform.position = destination;
lineRenderer.SetPosition(1, transform.position);
    }
    // Update is called once per frame
    void Update()
    {   
if (Input.GetKeyDown(KeyCode.Q))
    GetComponent<Rigidbody2D>().gravityScale = 1;     

if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude) == null)
        {
            Debug.Log("Game Over");

            int levelToLoad = 
            SceneManager.GetActiveScene(). buildIndex + 1;
            SceneManager.LoadScene(levelToLoad);
        }


    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(nameof(ReloadLevelAgain), 5);
    }


    void ReloadLevelAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

