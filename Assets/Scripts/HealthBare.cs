using UnityEngine;

public class HealthBare : MonoBehaviour
{
    private Transform cam;
    Piece piece;
    public GameObject lifeBare;
    void Start()
    {
        cam = Camera.main.transform;
        piece = GetComponentInParent<Piece>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
        lifeBare.GetComponent<RectTransform>().transform.localScale = new Vector3((0.9f * piece.life) / piece.defLife, 1, 0.7f);
    }
}
