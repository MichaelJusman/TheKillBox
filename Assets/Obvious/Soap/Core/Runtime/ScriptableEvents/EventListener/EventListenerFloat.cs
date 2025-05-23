﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Obvious.Soap
{
    /// <summary>
    /// A listener for a ScriptableEventFloat
    /// </summary>
    [AddComponentMenu("Soap/EventListeners/EventListenerFloat")]
    public class EventListenerFloat : EventListenerGeneric<float>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<float>[] EventResponses => _eventResponses;

        [System.Serializable]
        public class EventResponse : EventResponse<float>
        {
            [SerializeField] private ScriptableEventFloat _scriptableEvent = null;
            public override ScriptableEvent<float> ScriptableEvent => _scriptableEvent;
            
            [SerializeField] private FloatUnityEvent _response = null;
            public override UnityEvent<float> Response => _response;
        }

        [System.Serializable]
        public class FloatUnityEvent : UnityEvent<float>
        {
            
        }
    }
}