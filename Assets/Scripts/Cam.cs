using UnityEngine;

public class Cam : MonoBehaviour
{
    private ObjectFader _fader;

    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            Vector3 dir = ball.transform.position - transform.position;
            Ray ray = new Ray(transform.position, dir);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider == null)
                {
                    return;
                }

                if(hit.collider.gameObject == ball)
                {
                    if(_fader != null)
                    {
                        _fader.DoFade = false;
                    }
                } else
                {
                    _fader = hit.collider.gameObject.GetComponent<ObjectFader>();
                    if(_fader != null)
                    {
                        _fader.DoFade = true;
                    }
                }
            }
        }
    }
}
