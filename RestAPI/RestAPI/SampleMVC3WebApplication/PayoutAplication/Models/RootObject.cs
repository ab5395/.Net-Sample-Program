using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayoutAplication.Models
{
    public class SenderBatchHeader
    {
        public string sender_batch_id { get; set; }
    }

    public class Amount
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class Fees
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class BatchHeader
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_completed { get; set; }
        public SenderBatchHeader sender_batch_header { get; set; }
        public Amount amount { get; set; }
        public Fees fees { get; set; }
        public int payments { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }

    public class Resource
    {
        public BatchHeader batch_header { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link2
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }

    public class RootObject
    {
        public string id { get; set; }
        public string event_version { get; set; }
        public DateTime create_time { get; set; }
        public string resource_type { get; set; }
        public string event_type { get; set; }
        public string summary { get; set; }
        public Resource resource { get; set; }
        public List<Link2> links { get; set; }
    }
}