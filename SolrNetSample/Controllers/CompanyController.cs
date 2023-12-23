using Microsoft.AspNetCore.Mvc;
using SolrNet;
using SolrNetSample.Models;

namespace SolrNetSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ISolrOperations<Company> _solrOperations;

        public CompanyController(ISolrOperations<Company> solrOperations)
        {
            _solrOperations = solrOperations;
        }

        [HttpGet(Name = "GetCompany")]
        public IEnumerable<Company> Get()
        {
            return _solrOperations.Query(new SolrQuery("*:*"))
                .ToList();
        }
    }
}
