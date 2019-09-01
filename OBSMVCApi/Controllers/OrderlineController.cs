using OBSMVCApi.DAL;
using OBSMVCApi.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/OrderLine")]
    public class OrderlineController : ApiController
    {
        private IRepository<OrderLine> _irepo;
        private OrderlineRepository _repo;
        public OrderlineController(IRepository<OrderLine> repository, OrderlineRepository orderlineRepository)
        {
            _irepo = repository;
            _repo = orderlineRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var orderLines = await _irepo.Get();
            return Ok(orderLines);
        }

        [HttpGet,Route("id")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var orderLine = await _irepo.Get(id);
            return Ok(orderLine);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(OrderLine model)
        {
            var data= await _irepo.Post(model);
            if (data!=null)
            {
                return Ok(model);
            }

            return Ok("Stock Not Available");
        }

        [HttpPut, Route("id")]
        public async Task<IHttpActionResult> Put(int id, OrderLine model)
        {
            await _irepo.Put(id, model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var data= await _irepo.Delete(id);
            if (data!=null)
            {
                return Ok("Deleted");
            }
            return Ok("Something goes wrong");
        }

        //get orderlines by orderId
        [HttpGet,Route("GetByOrder/{orderId}")]
        public IHttpActionResult GetByOrderId(int orderId)
        {
            var orderLine = _repo.GetByOrderId(orderId);
            return Ok(orderLine);
        }


    }
}
