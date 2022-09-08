using Newtonsoft.Json;
using System.Collections.Generic;

namespace JFS_Task
{
    public static class JsonSerializerHelper
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type. This might actually happen here and we are aware of that.

        /// <summary>
        /// This will deserialize and return a list of objects from JSON file supplied via HTML form.
        /// </summary>
        /// <typeparam name="T">Type of object that we are expecting to find.</typeparam>
        /// <param name="listName">Name of a JSON list that will contain objects to deserialize.</param>
        /// <param name="formFile">File accessor.</param>
        /// <returns>List of deserialized objects.</returns>
        /// <exception cref="ArgumentException">In case we are not able to instantiate list of type <typeparamref name="T"/>.</exception>
        public static List<T>? DeserializeObjectsList<T>(string listName, IFormFile formFile)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.MissingMemberHandling = MissingMemberHandling.Error;

            Type genericListType = typeof(List<>).MakeGenericType(typeof(T));
            List<T> result = (List<T>) Activator.CreateInstance(genericListType);

            if (result == null)
            {
                throw new ArgumentException("List cannot be created for the type " + typeof(T).ToString());
            }

            try
            {
                using Stream s = formFile.OpenReadStream();
                using StreamReader sr = new(s);
                using JsonReader reader = new JsonTextReader(sr);

                JSONParserSM stateMachine = new() { ArrayName = listName, State = listName == "" ? State.LookingForObject : State.LookingForArray };

                while (reader.Read())
                {
                    switch (stateMachine.State)
                    {
                        // In case we are looking for a named array, wait for the correct array to appear
                        case State.LookingForArray:
                            if (reader.Value != null && reader.Value.ToString() == stateMachine.ArrayName)
                            {
                                stateMachine.State = State.LookingForObject;
                            }
                            break;

                        // We are inside an array of objects
                        case State.LookingForObject:
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                T deserializedObject = serializer.Deserialize<T>(reader);

                                if (deserializedObject == null)
                                {
                                    throw new NullReferenceException();
                                }

                                result.Add(deserializedObject);
                            }
                            else if (stateMachine.ArrayName != null && reader.TokenType == JsonToken.EndArray)
                            {
                                stateMachine.State = State.LookingForArray;
                            }
                            break;

                        // Nothing to do here
                        default:
                            break;
                    }
                }
            }
            catch
            {
                // In case of any error return null
                // TODO: possible to change this to throw a custom exception detailing the exact point at which the parse error occured
                return null;
            }

            return result;
        }
#pragma warning restore CS8600

    }
}
