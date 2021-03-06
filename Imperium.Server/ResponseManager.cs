﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Imperium.Server
{
    public class ResponseManager<T>
    {
        public delegate NetData ResponseGenerator(Connection<T> connection, Dictionary<string, dynamic> args);

        public delegate ResponseGenerator ExceptionResponseGenerator(Exception ex);



        private readonly Dictionary<string, ResponsePair<T>> _responses;

        private readonly ResponseGenerator _permissionErrorResponse;

        private readonly ExceptionResponseGenerator _requestErrorResponse;



        [Obsolete("Testing ctor")]
        public ResponseManager() { }

        public ResponseManager(
            Dictionary<string, ResponsePair<T>> responses, 
            ResponseGenerator permissionErrorResponse, 
            ExceptionResponseGenerator requestErrorResponse)
        {
            _responses = responses;
            _permissionErrorResponse = permissionErrorResponse;
            _requestErrorResponse = requestErrorResponse;
        }


        public virtual string GetResponse(string request, Connection<T> connection)
        {
            JToken jrequest;

            ResponseGenerator generator;
            var args = new Dictionary<string, dynamic>();

            try
            {
                jrequest = JToken.Parse(request);

                try
                {
                    var pair = _responses[jrequest["type"].ToString()];

                    generator =
                        !pair.Groups.Any()
                        || (connection.Account?.Groups.Any(g => pair.Groups.Contains(g)) ?? false)
                            ? _responses[jrequest["type"].ToString()].ResponseGenerator
                            : _permissionErrorResponse;

                    args = jrequest["args"]?.ToObject<Dictionary<string, dynamic>>(Serializer.Current)
                           ?? new Dictionary<string, dynamic>();
                }
                catch (KeyNotFoundException ex)
                {
                    generator = _requestErrorResponse(ex);
                }
            }
            catch (JsonReaderException ex)
            {
                generator = _requestErrorResponse(ex);
            }
            
            return JToken.FromObject(
                generator(connection, args) ?? new Dictionary<string, dynamic>(),
                Serializer.Current)
                .ToString();
        }



        [Serializable]
        public class InvalidRequestException : Exception
        {
            public string ReceivedData { get; set; }



            public InvalidRequestException(string receivedData)
            {
                ReceivedData = receivedData;
            }

            public InvalidRequestException(string receivedData, string message) : base(message)
            {
                ReceivedData = receivedData;
            }

            public InvalidRequestException(string receivedData, string message, Exception inner) : base(message, inner)
            {
                ReceivedData = receivedData;
            }

            protected InvalidRequestException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
