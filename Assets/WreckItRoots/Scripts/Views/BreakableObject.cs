using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace WreckItRoots.Views
{
    public class BreakableObject : MonoBehaviour
    {
        private const float ScaleDownDuration = 2f;
        
        [SerializeField] private GameObject[] piecesRaw;

        private Piece[] _pieces;
        private Tween _tween;

        private void Awake()
        {
            _pieces = piecesRaw.Select(p => new Piece
            {
                Body = p.GetComponent<Rigidbody>(),
                Collider = p.GetComponent<Collider>(),
                Transform = p.transform,
                InitialPosition = p.transform.localPosition
            }).ToArray();
        }

        public void ResetPieces()
        {
            _tween.Kill();
            foreach (var piece in _pieces)
            {
                piece.Body.isKinematic = true;
                piece.Collider.enabled = false;
                piece.Transform.localPosition = piece.InitialPosition;
                piece.Transform.localScale = Vector3.one;
            }
        }

        public void Break()
        {
            _tween.Kill();
            var sequence = DOTween.Sequence();
            _tween = sequence;
            sequence.Append(DOVirtual.DelayedCall(ScaleDownDuration, () => gameObject.SetActive(false)));
            foreach (var piece in _pieces)
            {
                piece.Body.isKinematic = false;
                piece.Collider.enabled = true;
                sequence.Join(piece.Transform.DOScale(0f, ScaleDownDuration));
            }
        }

        private class Piece
        {
            public Rigidbody Body;
            public Collider Collider;
            public Transform Transform;
            public Vector3 InitialPosition;
        }
    }
}