using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using AuditFunction.Models;

namespace AuditFunction
{
    public static class ClaimStatusUpdate
    {
        [FunctionName("ClaimStatusUpdate")]
        public static void Run([ServiceBusTrigger("auditqueue", Connection = "Endpoint=sb://auditqueue.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ahSn52duR61E/CN2/NoJKMpj6Vu6/UKLtTTf8NXD7uE=")] string myQueueItem, ILogger log)
        {
            var AuditRecieved = JsonConvert.DeserializeObject<Audit>(myQueueItem);
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
            {
                connection.Open();
                if (AuditRecieved != null)
                {
                    var query = $"INSERT INTO [ClaimAudit] (ClaimId,Timestamp,Operation) VALUES('{AuditRecieved.ClaimId}', '{AuditRecieved.Timestamp}' , '{AuditRecieved.Operation}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
