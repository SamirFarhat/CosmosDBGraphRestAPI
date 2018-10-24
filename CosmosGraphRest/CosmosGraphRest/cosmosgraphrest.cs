using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Graphs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace cosmosgraphrest
{
    public static class cosmosgraphrest
    {

        static string endpoint = Environment.GetEnvironmentVariable("Endpoint");
        static string authKey = Environment.GetEnvironmentVariable("AuthKey");


        // open the client's connection
        private static DocumentClient client = new DocumentClient(new Uri(endpoint), authKey, new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp });
        
        [FunctionName("cosmosgraphrest")]

        public static async Task<HttpResponseMessage> Run(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req,
                    TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // the person objects will be free-form in structure
            List<dynamic> results = new List<dynamic>();



            {
                // get a reference to the database the console app created
                Database database = await client.CreateDatabaseIfNotExistsAsync(
                new Database
                {
                    Id = "GraphDB"
                });

                // get an instance of the database's graph

                DocumentCollection graph = await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri("GraphDB"),
                    new DocumentCollection { Id = "GraphName" },
                    new RequestOptions { OfferThroughput = 400 }
                    );

                // Get the gremlinquery from the headers
                IEnumerable<string> headerValues = req.Headers.GetValues("gremlinquery");
                var gremlinquery = headerValues.FirstOrDefault();
                //Make the query against the graph
                IDocumentQuery<dynamic> query = client.CreateGremlinQuery<dynamic>(graph, string.Format("{0}", gremlinquery));

                // iterate over all the results and add them to the list
                while (query.HasMoreResults)
                    foreach (dynamic result in await query.ExecuteNextAsync())
                        results.Add(result);
            }

            // return the list with an OK response
            return req.CreateResponse<List<dynamic>>(HttpStatusCode.OK, results);
        }
    }
}
