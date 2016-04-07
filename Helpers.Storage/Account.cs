using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using System.Diagnostics;

namespace Helpers.Storage
{
    public class Account
    {
        #pragma warning disable 0169, 0414

        #warning change DEV storage to false
        private static bool useDevStorage = false;

        private static CloudStorageAccount _cloudStorageAccount = null;

        private static string AccountKey1  = "iNf5Q5oHySoBVTMH3nTPrXv+rL5tg6GIVlUxcrDY85s5iHH8iPg9t0DzYPPZbUbtYSg9eIvxvzArPpXfdLrHZQ==";
        private static string AccountKey2  = "gwmJqj1F4myvNXR78xDkFYj9oPcHoS6rhOxRvWTdKIMD4RLsLW5Lwln2QAbL6XCkjo1F3WAvhnUd1z5lzaifKA==";
        private static string AccountName  = "personaloffertracker";
        private static string BlobStorage  = "personaloffertracker.blob.core.windows.net";
        private static string TableStorage = "personaloffertracker.table.core.windows.net";
        private static string QueueStorage = "personaloffertracker.queue.core.windows.net";

        public static CloudStorageAccount GetCloudStorageAccount()
        {
            lock( AccountKey1 )
            {
                if( _cloudStorageAccount == null )
                {
                    if( useDevStorage )
                    {
                        _cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
                    }
                    else
                    {
                        byte[] key = Convert.FromBase64String( AccountKey1 );
                        var storageCredentials = new StorageCredentialsAccountAndKey( AccountName, key );

                        _cloudStorageAccount = new CloudStorageAccount( storageCredentials, false );
                    }
                }
            }

            return _cloudStorageAccount;
        }
     }
}
