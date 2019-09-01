using OBSMVCApi.DAL;
using OBSMVCApi.DTO;
using OBSMVCApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace OBSMVCApi.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private IRepository<Order> _repo;
        private OrderRepository repo;
        public OrderController(IRepository<Order> repository, OrderRepository orderRepository)
        {
            _repo = repository;
            repo = orderRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var author = await _repo.Get();
            return Ok(author);
        }

        [HttpGet,Route("GetByVM")]
        public async Task<IHttpActionResult> GetByVm()
        {
            var author = await repo.GetByVM();
            return Ok(author);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var order = await repo.GetById(id);
            return Ok(order);
        }

        [HttpGet,Route("GetById/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var order =  repo.GetByOrderId(id);
            return Ok(order);
        }



        [HttpPost]
        public async Task<IHttpActionResult> Post(Order model)
        {
            await _repo.Post(model);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, Order model)
        {
            await _repo.Put(id, model);
            return Ok(model);
        }


        [HttpPut,Route("UpdateOrderStatus/{id}")]
        public async Task<IHttpActionResult> Update(int id, Order model)
        {
            await repo.UpdateOrderStatus(id, model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var data = await _repo.Delete(id);
            if (data != null)
            {
                return Ok("Record Deleted");
            }
            return Ok("Order not found!!!");
        }


        //get all orders for loggedIn user
        [HttpGet, Route("GetByUser/{userId}")]
        public IHttpActionResult Get(string userId)
        {
            var order = repo.GetOrdersByUser(userId).ToList();

            if (order.Any())
            {
                return Ok(order);
            }

            return Ok("There is no order for the user");
        }

        [HttpPost, Route("Insert")]
        public async Task<IHttpActionResult> PostByVm(OrderViewModel model)
        {
            await repo.Insert(model);
            return Ok(model);
        }

        [HttpGet, Route("GetLastOrderId")]
        public int GetLastId()
        {
            return repo.GetLastId();
        }
    }
}
