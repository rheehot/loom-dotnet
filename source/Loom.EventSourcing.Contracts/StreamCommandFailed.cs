﻿namespace Loom.EventSourcing
{
    using System;
    using Loom.Messaging;

    [Obsolete("StreamCommandFailed<T> class is deprecated. Use your own types instead.")]
    public sealed class StreamCommandFailed<T>
    {
        public StreamCommandFailed(StreamCommand<T> command,
                                   HandlerError error,
                                   DateTime failedTimeUtc)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Error = error ?? throw new ArgumentNullException(nameof(error));
            FailedTimeUtc = failedTimeUtc;
        }

        public StreamCommand<T> Command { get; }

        public HandlerError Error { get; }

        public DateTime FailedTimeUtc { get; }
    }
}
