﻿namespace Loom.EventSourcing
{
    using System;

    public class StreamCommand<T>
    {
        public StreamCommand(Guid streamId, T payload)
            => (StreamId, Payload) = (streamId, payload);

        public Guid StreamId { get; }

        public T Payload { get; }
    }
}
