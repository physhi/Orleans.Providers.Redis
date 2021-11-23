using Orleans.Serialization;
using System;
using System.Reflection;

namespace Orleans.Redis.Common
{
    public interface ISerializationManager
    {
        byte[] SerializeToByteArray(object raw);
        T DeserializeFromByteArray<T>(byte[] data);
        object DeserializeFromByteArray(Type type, byte[] data);
    }

    public class OrleansSerializationManager : ISerializationManager
    {
        private readonly SerializationManager _serializationManager;

        public OrleansSerializationManager(SerializationManager serializationManager)
        {
            _serializationManager = serializationManager;
        }

        public T DeserializeFromByteArray<T>(byte[] data)
        {
            return _serializationManager.DeserializeFromByteArray<T>(data);
        }

        public object DeserializeFromByteArray(Type type, byte[] data)
        {
            return _serializationManager.DeserializeFromByteArray(data, type);
        }

        public byte[] SerializeToByteArray(object raw)
        {
            return _serializationManager.SerializeToByteArray(raw);
        }
    }
}
