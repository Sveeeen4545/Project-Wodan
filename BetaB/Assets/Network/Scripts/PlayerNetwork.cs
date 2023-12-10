using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    private readonly NetworkVariable<PlayerNetworkData> _netState = new (writePerm: NetworkVariableWritePermission.Owner);

    private Vector3 _vel;
    private float _rotVel;
    [SerializeField] private float _interpoliationTime = 0.1f; 

    void Update()
    {
        if (IsOwner)
        {
            _netState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles
            };

        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _netState.Value.Position, ref _vel, _interpoliationTime);
            transform.rotation = Quaternion.Euler (
                0,
                Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _netState.Value.Rotation.y, ref _rotVel, _interpoliationTime),
                0); 
        }
    }


    struct PlayerNetworkData: INetworkSerializable
    {
        private float _x, _z;
        private float _yRot; 

        internal Vector3 Position
        {
            get => new Vector3(_x, 0, _z);
            set
            {
                _x = value.x;
                _z = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3 (0, _yRot, 0);
            set => _yRot = value.y;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _z);

            serializer.SerializeValue(ref _yRot);
        }
    }
}
