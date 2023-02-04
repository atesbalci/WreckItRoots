using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace WreckItRoots.Views
{
    public class TreeView : MonoBehaviour
    {
        private const float HeightMultiplier = 0.15f;
        
        [SerializeField] private Transform treesPivot;
        [SerializeField] private GameObject[] trees;

        private Tween _tween;
        
        public void Initialize(float xPos, float momentum)
        {
            transform.position = new Vector2(xPos, 0f);
            var currentTreeIndex = Random.Range(0, trees.Length);
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i].SetActive(currentTreeIndex == i);
            }
            
            treesPivot.localScale = Vector3.zero;
            _tween.Kill();
            _tween = treesPivot.DOScale(HeightMultiplier * momentum, 3f).SetEase(Ease.OutElastic);
        }

        private void OnDisable()
        {
            _tween.Kill();
        }
        
        public class Pool : MonoMemoryPool<TreeView> { }
    }
}