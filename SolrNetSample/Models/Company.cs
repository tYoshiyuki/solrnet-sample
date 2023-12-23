using SolrNet.Attributes;

namespace SolrNetSample.Models
{
    public class Company
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("compName_s")]
        public string CompName { get; set; }
    }
}
