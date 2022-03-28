using Microsoft.AspNetCore.Mvc;

namespace UseCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static List<Cart> carts = new List<Cart>
        {
            new Cart
            {
                Id = 1,
                UserID = 1,
                ProductID = 1,
                Quantity = 1
            },
        };

        private readonly DataContext _context;
        public CartController(DataContext context)
        {
            _context= context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cart>>> Get()
        {
            return await _context.Carts.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Cart>>> GetByID(int id)
        {
            var res = await _context.Carts.FindAsync(id);
            if(res==null)
                return NotFound("cart not found");
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<List<Cart>>> AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return Ok(await _context.Carts.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Cart>>> UpdateCart(int id, Cart request)
        {
            var dbCart = await _context.Carts.FindAsync(id);
            if (dbCart == null)
                return BadRequest("cart not found");

            dbCart.UserID=request.UserID;
            dbCart.ProductID=request.ProductID;
            dbCart.Quantity=request.Quantity;

            await _context.SaveChangesAsync();
            return Ok(await _context.Carts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Cart>>> DeleteCart(int id)
        {
            var dbCart = await _context.Carts.FindAsync(id);
            if (dbCart == null)
                return BadRequest("cart not found");

            _context.Carts.Remove(dbCart);
            await _context.SaveChangesAsync();

            return Ok(await _context.Carts.ToListAsync());
        }
    }
}
