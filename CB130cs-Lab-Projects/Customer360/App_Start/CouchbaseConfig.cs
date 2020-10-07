using Couchbase;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Couchbase.Authentication;

namespace Customer360
{
    public static class CouchbaseConfig
    {
        public static void Setup()
        {
            // define settings for this Couchbase Cluster connection
            var config = new ClientConfiguration
            {
                // assign one or more Couchbase Server URIs available for bootstrap
                Servers = new List<Uri> {
                    new Uri("couchbase://127.0.0.1")
                },
                //new PasswordAuthenticator()
                UseSsl = false,
                BucketConfigs = new Dictionary<string, BucketConfiguration>
                {
                    {"Customer360", new BucketConfiguration
                        {
                            BucketName = "Customer360",
                            Password = "",
                            UseSsl = false
                        }
                    },
                    {"Default", new BucketConfiguration
                        {
                            BucketName = "Default",
                            Password = "",
                            UseSsl = false
                        }
                    }
                }
            };
            // initialize Cluster, to be built as a singleton
            ClusterHelper.Initialize(config);
        }
        public static void Cleanup()
        {
            // on shutdown, dispose of Cluster and clean up resources
            ClusterHelper.Close();
        }
    }
}