using System.Linq;
using UnityEngine;

namespace WreckItRoots.Views
{
    public class BreakableObject : MonoBehaviour
    {
        [SerializeField] private GameObject[] piecesRaw;

        private Piece[] _pieces;

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
            foreach (var piece in _pieces)
            {
                piece.Body.isKinematic = true;
                piece.Collider.enabled = false;
                piece.Transform.localPosition = piece.InitialPosition;
            }
        }

        public void Break()
        {
            foreach (var piece in _pieces)
            {
                piece.Body.isKinematic = false;
                piece.Collider.enabled = true;
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