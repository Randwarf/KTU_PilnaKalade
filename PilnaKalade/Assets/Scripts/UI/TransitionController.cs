using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static void TransitionTo(int sceneIndex) {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Image transitionPanel = SpawnTransitionPanel();
        transitionPanel.DOFade(1f, 0.5f).OnComplete(() => {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += (scene, mode) => {
                SceneManager.UnloadSceneAsync(currentSceneIndex).completed += (e) => {
                    transitionPanel = SpawnTransitionPanel();
                    transitionPanel.DOFade(1f, 0f);
                    transitionPanel.DOFade(0f, 0.5f)
                        .SetDelay(0.5f)
                        .OnComplete(() => Destroy(transitionPanel.gameObject));
                };
            };
        });
    }

    private static Image SpawnTransitionPanel() {
        GameObject panel = new GameObject("TransitionPanel");

        Canvas canvas = FindObjectOfType<Canvas>();
        panel.transform.SetParent(canvas.transform);

        RectTransform rt = panel.AddComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = new Vector2(0, 0);
        rt.offsetMax = new Vector2(0, 0);
        rt.transform.localScale = new Vector2(1, 1);

        panel.AddComponent<CanvasRenderer>();

        Image img = panel.AddComponent<Image>();
        img.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0);
        img.raycastTarget = false;

        return img;
    }
}
