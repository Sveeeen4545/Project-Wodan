using Unity.Netcode;
using UnityEngine;


public class PlayerNetwork : NetworkBehaviour
{

    [SerializeField] private bool _serverauth;
    [SerializeField] private float _interpoliationTime = 0.1f;

    private NetworkVariable<PlayerNetworkState> _playerState;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        var permission = _serverauth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        _playerState = new NetworkVariable<PlayerNetworkState>(writePerm: permission); 
    }


    public override void  OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(transform.GetComponent<SceneObjDrag>()); 
    }


    private void Update()
    {
        if (IsOwner) TransmitState();
        else ConsumeState();
    }

    private void TransmitState()
    {
        var State = new PlayerNetworkState 
        {
            Position = _rb.position,
            Rotation = transform.rotation.eulerAngles,
        }; 

        if (IsServer || !_serverauth)
        {
            _playerState.Value = State; 
        }
        else
        {
            TransmitStateServerRPC(State); 
        }

    }

    [ServerRpc]
    private void TransmitStateServerRPC(PlayerNetworkState state)
    {
        _playerState.Value = state;
    }

    private Vector3 _posVel;
    private float _rotVelY;

    private void ConsumeState()
    {
        _rb.MovePosition(Vector3.SmoothDamp(_rb.position, _playerState.Value.Position, ref _posVel, _interpoliationTime));

        transform.position = _playerState.Value.Rotation; 
        
           
        
    }


    struct PlayerNetworkState : INetworkSerializable
    {
        private float _x, _z;
        private short _yRot;

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
            get => new Vector3(0, _yRot, 0);
            set => _yRot = (short)value.y;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _x);
            serializer.SerializeValue(ref _z);

            serializer.SerializeValue(ref _yRot);
        }
    }
}

//private Vector3 _vel;
//private float _rotVel;

//private readonly NetworkVariable<PlayerNetworkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);

//public void Updatenetwork()
//{
//    if (IsOwner)
//    {
//        debug.log(gameobject + "is the owner");
//        _netstate.value = new playernetworkdata()
//        {
//            position = transform.position,
//            rotation = transform.rotation.eulerangles
//        };

//    }
//    else
//    {
//        transform.position = vector3.smoothdamp(transform.position, _netstate.value.position, ref _vel, _interpoliationtime);
//        transform.rotation = quaternion.euler(
//        0,
//            mathf.smoothdampangle(transform.rotation.eulerangles.y, _netstate.value.rotation.y, ref _rotvel, _interpoliationtime),
//            0);
//    }
//}









