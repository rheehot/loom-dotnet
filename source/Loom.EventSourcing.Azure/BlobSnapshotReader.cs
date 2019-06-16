﻿namespace Loom.EventSourcing.Azure
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.Storage.Blob;
    using Newtonsoft.Json;

    public class BlobSnapshotReader<T> : ISnapshotReader<T>
    {
        private readonly CloudBlobContainer _container;

        public BlobSnapshotReader(CloudBlobContainer container)
        {
            _container = container;
        }

        public async Task<T> TryRestoreSnapshot(Guid streamId)
        {
            CloudBlockBlob blob = GetBlobReference(streamId);
            return await blob.ExistsAsync().ConfigureAwait(continueOnCapturedContext: false)
                 ? await RestoreSnapshot(blob).ConfigureAwait(continueOnCapturedContext: false)
                 : default;
        }

        private CloudBlockBlob GetBlobReference(Guid streamId)
        {
            return _container.GetBlockBlobReference($"{streamId}.json");
        }

        private static async Task<T> RestoreSnapshot(CloudBlockBlob blob)
        {
            using (var stream = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(stream).ConfigureAwait(continueOnCapturedContext: false);
                string content = Encoding.UTF8.GetString(stream.ToArray());
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
